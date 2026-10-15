using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;

public class ClosedEntry {
	public string Title { get; set; }
	public List<string> History { get; set; } // 戻る/進む履歴（古い順）
	public int Position { get; set; }         // History内の現在位置
	public DateTime ClosedAt { get; set; }
	public bool Favorite { get; set; }        // お気に入り（上限で消えず、復元しても残る）
	[JsonIgnore] public string Current { get { return History[Position]; } }
}

class Tracked {
	public List<string> History = new List<string>();
	public int Position = -1;
	public string Title = "";

	public void Visit( string path, int max ) {
		if( Position >= 0 && History[Position] == path ) { return; }
		if( Position > 0 && History[Position - 1] == path ) { Position--; return; }          // 戻る
		if( Position + 1 < History.Count && History[Position + 1] == path ) { Position++; return; } // 進む
		if( Position + 1 < History.Count ) {
			History.RemoveRange( Position + 1, History.Count - Position - 1 );
		}

		History.Add( path );
		Position = History.Count - 1;
		while( History.Count > max ) { History.RemoveAt( 0 ); Position--; }
	}
}

// ホットキー受信用の非表示ウィンドウ兼アプリ本体
[System.ComponentModel.DesignerCategory( "" )]
class App : Form {
	const int HotkeyId = 1;
	[DllImport( "user32.dll" )] static extern bool RegisterHotKey( IntPtr h, int id, uint mod, uint vk );
	[DllImport( "user32.dll" )] static extern bool UnregisterHotKey( IntPtr h, int id );

	static readonly string DataFile = Path.Combine( AppSettings.DataDir, "closed.json" );
	public static readonly Icon AppIcon = new Icon( typeof( App ).Assembly.GetManifestResourceStream( "app.ico" ) );

	readonly dynamic shell = Activator.CreateInstance( Type.GetTypeFromProgID( "Shell.Application" ) );
	readonly Dictionary<long, Tracked> windows = new Dictionary<long, Tracked>();
	readonly HashSet<long> restoring = new HashSet<long>();
	List<ClosedEntry> closed = new List<ClosedEntry>();
	readonly NotifyIcon tray = new NotifyIcon();
	readonly Timer timer = new Timer();
	readonly ToolStripMenuItem restoreLatestItem = new ToolStripMenuItem();
	readonly ToolStripMenuItem recentMenu = new ToolStripMenuItem();
	readonly ToolStripMenuItem favoritesMenu = new ToolStripMenuItem();
	readonly ToolStripMenuItem showHistoryItem = new ToolStripMenuItem();
	readonly ToolStripMenuItem settingsItem = new ToolStripMenuItem();
	readonly ToolStripMenuItem exitItem = new ToolStripMenuItem();
	HistoryForm historyForm;
	SettingsForm settingsForm;

	public AppSettings Settings { get; private set; } = AppSettings.Load();
	public IReadOnlyList<ClosedEntry> ClosedEntries { get { return closed; } }

	App() {
		ShowInTaskbar = false;

		var menu = new ContextMenuStrip();
		showHistoryItem.Font = new Font( menu.Font, FontStyle.Bold );
		showHistoryItem.Click += ( s, e ) => ShowHistory();
		restoreLatestItem.Click += ( s, e ) => RestoreLatest();
		settingsItem.Click += ( s, e ) => ShowSettings( null );
		exitItem.Click += ( s, e ) => { tray.Visible = false; Application.Exit(); };
		menu.Items.Add( showHistoryItem );
		menu.Items.Add( restoreLatestItem );
		menu.Items.Add( recentMenu );
		menu.Items.Add( favoritesMenu );
		menu.Items.Add( new ToolStripSeparator() );
		menu.Items.Add( settingsItem );
		menu.Items.Add( exitItem );
		menu.Opening += ( s, e ) => BuildRecentMenu();
		tray.ContextMenuStrip = menu;
		tray.Icon = new Icon( AppIcon, SystemInformation.SmallIconSize );
		tray.Text = "ReopenExplorer";
		tray.Visible = true;
		tray.DoubleClick += ( s, e ) => ShowHistory();

		LoadClosed();
		ApplySettings();
		Poll();
		timer.Tick += ( s, e ) => Poll();
		timer.Start();
	}

	protected override void SetVisibleCore( bool value ) {
		if( !IsHandleCreated ) {
			CreateHandle();
		}

		base.SetVisibleCore( false );
	}

	protected override void WndProc( ref Message m ) {
		if( m.Msg == 0x0312 && m.WParam.ToInt32() == HotkeyId ) {
			RestoreLatest();
		}

		base.WndProc( ref m );
	}

	protected override void OnHandleDestroyed( EventArgs e ) { UnregisterHotKey( Handle, HotkeyId ); base.OnHandleDestroyed( e ); }

	void ApplySettings() {
		timer.Interval = Math.Max( 100, Settings.PollIntervalMs );
		S.Init( Settings.Language ); // 言語が変わると S.LanguageChanged で各画面が更新される
		showHistoryItem.Text = S.ShowHistory;
		recentMenu.Text = S.RecentlyClosed;
		favoritesMenu.Text = S.Favorites;
		settingsItem.Text = S.SettingsMenu;
		exitItem.Text = S.Exit;
		restoreLatestItem.Text = S.RestoreLatest + ( Settings.HotkeyEnabled ? " (" + Settings.HotkeyText() + ")" : "" );
		TrimClosed();

		UnregisterHotKey( Handle, HotkeyId );
		if( Settings.HotkeyEnabled ) {
			var k = Settings.Hotkey;
			uint mod = 0x4000; // MOD_NOREPEAT
			if( ( k & Keys.Alt ) != 0 ) {
				mod |= 1;
			}

			if( ( k & Keys.Control ) != 0 ) {
				mod |= 2;
			}

			if( ( k & Keys.Shift ) != 0 ) {
				mod |= 4;
			}

			if( Settings.HotkeyWin ) {
				mod |= 8;
			}

			if( !RegisterHotKey( Handle, HotkeyId, mod, (uint)( k & Keys.KeyCode ) ) ) {
				tray.ShowBalloonTip( 3000, "ReopenExplorer", S.HotkeyInUse( Settings.HotkeyText() ), ToolTipIcon.Warning );
			}
		}
	}

	public void ShowSettings( IWin32Window owner ) {
		if( settingsForm != null ) { settingsForm.Activate(); return; }
		using( settingsForm = new SettingsForm( Settings ) ) {
			if( settingsForm.ShowDialog( owner ) == DialogResult.OK ) {
				Settings = settingsForm.Result;
				Settings.Save();
				ApplySettings();
				Changed();
			}
		}
		settingsForm = null;
	}

	void ShowHistory() {
		if( historyForm == null || historyForm.IsDisposed ) {
			historyForm = new HistoryForm( this );
			historyForm.Show();
		} else {
			historyForm.RefreshList();
			if( historyForm.WindowState == FormWindowState.Minimized ) {
				historyForm.WindowState = FormWindowState.Normal;
			}

			historyForm.Activate();
		}
	}

	void BuildRecentMenu() {
		recentMenu.DropDownItems.Clear();
		foreach( var e in closed.Where( c => !c.Favorite ).Take( 15 ) ) {
			recentMenu.DropDownItems.Add( MenuItemOf( e ) );
		}
		favoritesMenu.DropDownItems.Clear();
		foreach( var e in closed.Where( c => c.Favorite ) ) {
			favoritesMenu.DropDownItems.Add( MenuItemOf( e ) );
		}
		recentMenu.Enabled = recentMenu.DropDownItems.Count > 0;
		favoritesMenu.Enabled = favoritesMenu.DropDownItems.Count > 0;
		restoreLatestItem.Enabled = recentMenu.Enabled;
	}

	ToolStripMenuItem MenuItemOf( ClosedEntry e ) {
		return new ToolStripMenuItem( e.Title + "  —  " + e.Current, null, ( s, a ) => Restore( e ) ) { ToolTipText = e.ClosedAt.ToString( S.DateFormat ) };
	}

	// ---- 監視 ----
	IEnumerable<KeyValuePair<long, dynamic>> ExplorerWindows() {
		var result = new List<KeyValuePair<long, dynamic>>();
		dynamic wins;
		try { wins = shell.Windows(); } catch { return result; }
		int n = 0;
		try { n = wins.Count; } catch { }
		for( int i = 0; i < n; i++ ) {
			try {
				dynamic w = wins.Item( i );
				if( w == null ) {
					continue;
				}

				string full = w.FullName;
				if( !full.EndsWith( "explorer.exe", StringComparison.OrdinalIgnoreCase ) ) {
					continue;
				}

				result.Add( new KeyValuePair<long, dynamic>( (long)w.HWND, w ) );
			} catch { }
		}
		return result;
	}

	static string PathOf( dynamic w ) {
		try { string p = w.Document.Folder.Self.Path; if( !string.IsNullOrEmpty( p ) ) { return p; } } catch { }
		return null;
	}

	void Poll() {
		var seen = new HashSet<long>();
		foreach( var kv in ExplorerWindows() ) {
			seen.Add( kv.Key );
			if( restoring.Contains( kv.Key ) ) {
				continue;
			}

			string p = PathOf( kv.Value );
			if( p == null ) {
				continue;
			}

			Tracked t;
			if( !windows.TryGetValue( kv.Key, out t ) ) {
				windows[kv.Key] = t = new Tracked();
			}

			t.Visit( p, Math.Max( 1, Settings.MaxHistoryPerWindow ) );
			try { t.Title = kv.Value.LocationName; } catch { }
		}
		foreach( var h in windows.Keys.Where( h => !seen.Contains( h ) ).ToList() ) {
			var t = windows[h];
			windows.Remove( h );
			if( t.Position < 0 ) {
				continue;
			}

			var entry = new ClosedEntry { Title = t.Title, History = t.History, Position = t.Position, ClosedAt = DateTime.Now };
			closed.Insert( 0, entry );
			TrimClosed();
			Changed();
			if( Settings.ShowNotification ) {
				tray.ShowBalloonTip( 2000, S.ClosedRecorded, entry.Current, ToolTipIcon.Info );
			}
		}
	}

	void TrimClosed() {
		// お気に入りは上限の対象外
		int max = Math.Max( 1, Settings.MaxClosed );
		var over = closed.Where( c => !c.Favorite ).Skip( max ).ToList();
		closed.RemoveAll( over.Contains );
	}

	void Changed() {
		SaveClosed();
		if( historyForm != null && !historyForm.IsDisposed ) {
			historyForm.RefreshList();
		}
	}

	// ---- 復元 ----
	public void SetFavorite( IEnumerable<ClosedEntry> entries, bool favorite ) {
		foreach( var e in entries ) {
			e.Favorite = favorite;
		}

		TrimClosed();
		Changed();
	}

	public void Remove( IEnumerable<ClosedEntry> entries ) {
		foreach( var e in entries.ToList() ) {
			closed.Remove( e );
		}

		Changed();
	}

	void RestoreLatest() {
		var e = closed.FirstOrDefault( c => !c.Favorite );
		if( e != null ) {
			Restore( e );
		}
	}

	public void OpenFolder( string path ) {
		try { Process.Start( "explorer.exe", "\"" + path + "\"" ); } catch( Exception ex ) { tray.ShowBalloonTip( 3000, S.OpenFailed, ex.Message, ToolTipIcon.Error ); }
	}

	public async void Restore( ClosedEntry e ) {
		if( !e.Favorite ) { closed.Remove( e ); Changed(); }
		bool withHistory = Settings.RestoreNavHistory && e.History.Count > 1;
		string first = withHistory ? e.History[0] : e.Current;

		var before = new HashSet<long>( ExplorerWindows().Select( kv => kv.Key ) );
		try { Process.Start( "explorer.exe", "\"" + first + "\"" ); } catch( Exception ex ) { tray.ShowBalloonTip( 3000, S.RestoreFailed, ex.Message, ToolTipIcon.Error ); return; }
		if( !withHistory ) {
			return;
		}

		// 新しいウィンドウを待つ
		long hwnd = 0; dynamic win = null;
		for( int i = 0; i < 50 && win == null; i++ ) {
			await Task.Delay( 100 );
			foreach( var kv in ExplorerWindows() ) {
				if( !before.Contains( kv.Key ) ) { hwnd = kv.Key; win = kv.Value; break; }
			}
		}
		if( win == null ) {
			return;
		}

		restoring.Add( hwnd );
		try {
			await WaitPath( win, e.History[0] );
			// 履歴を順にたどって「戻る」スタックを再構築
			for( int i = 1; i < e.History.Count; i++ ) {
				try { win.Navigate2( e.History[i] ); } catch { continue; }
				await WaitPath( win, e.History[i] );
			}
			// 「進む」側を作るために戻る
			for( int i = e.History.Count - 1; i > e.Position; i-- ) {
				try { win.GoBack(); } catch { break; }
				await WaitPath( win, e.History[i - 1] );
			}
			windows[hwnd] = new Tracked { History = new List<string>( e.History ), Position = e.Position, Title = e.Title };
		} catch { } finally { restoring.Remove( hwnd ); }
	}

	static async Task WaitPath( dynamic win, string path ) {
		for( int i = 0; i < 40; i++ ) {
			await Task.Delay( 75 );
			string p = PathOf( win );
			if( p != null && string.Equals( p, path, StringComparison.OrdinalIgnoreCase ) ) { await Task.Delay( 50 ); return; }
		}
	}

	// ---- 永続化 ----
	void LoadClosed() {
		try { if( File.Exists( DataFile ) ) { closed = JsonSerializer.Deserialize<List<ClosedEntry>>( File.ReadAllText( DataFile ) ) ?? new List<ClosedEntry>(); } } catch { closed = new List<ClosedEntry>(); }
		closed.RemoveAll( c => c.History == null || c.History.Count == 0 );
		foreach( var c in closed ) { c.Position = Math.Max( 0, Math.Min( c.Position, c.History.Count - 1 ) ); c.ClosedAt = c.ClosedAt.ToLocalTime(); }
	}

	void SaveClosed() {
		try { Directory.CreateDirectory( AppSettings.DataDir ); File.WriteAllText( DataFile, JsonSerializer.Serialize( closed ) ); } catch { }
	}

	[STAThread]
	static void Main() {
		bool created;
		using( new System.Threading.Mutex( true, "ReopenExplorer_SingleInstance", out created ) ) {
			if( !created ) {
				return;
			}

			S.Init( AppSettings.Load().Language );
			Application.EnableVisualStyles();
			Application.SetHighDpiMode( HighDpiMode.PerMonitorV2 );
			Application.Run( new App() );
		}
	}
}
