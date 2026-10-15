using System;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using Microsoft.Win32;

public class AppSettings {
	public int MaxClosed { get; set; } = 100;           // 記録する閉じたウィンドウ数
	public int MaxHistoryPerWindow { get; set; } = 100;  // 1ウィンドウあたりの戻る/進む履歴数
	public int PollIntervalMs { get; set; } = 400;      // 監視間隔
	public bool RestoreNavHistory { get; set; } = true; // 復元時に戻る/進む履歴も再現する
	public bool HotkeyEnabled { get; set; } = true;
	public Keys Hotkey { get; set; } = Keys.Control | Keys.Alt | Keys.Z;
	public bool HotkeyWin { get; set; }
	public bool ShowNotification { get; set; } = false; // 閉じたウィンドウを記録したときに通知
	public string Language { get; set; } = "auto";      // auto / ja / en

	public static readonly string DataDir = Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ), "ReopenExplorer" );
	static readonly string FilePath = Path.Combine( DataDir, "settings.json" );
	const string RunKey = @"Software\Microsoft\Windows\CurrentVersion\Run";

	public static AppSettings Load() {
		try {
			if( File.Exists( FilePath ) ) {
				return JsonSerializer.Deserialize<AppSettings>( File.ReadAllText( FilePath ) ) ?? new AppSettings();
			}
		} catch { }
		return new AppSettings();
	}

	public void Save() {
		try { Directory.CreateDirectory( DataDir ); File.WriteAllText( FilePath, JsonSerializer.Serialize( this, new JsonSerializerOptions { WriteIndented = true } ) ); } catch { }
	}

	public AppSettings Clone() { return (AppSettings)MemberwiseClone(); }

	public string HotkeyText() {
		if( !HotkeyEnabled ) {
			return S.None;
		}

		return FormatHotkey( Hotkey, HotkeyWin );
	}

	public static string FormatHotkey( Keys k, bool win ) {
		var s = "";
		if( win ) {
			s += "Win+";
		}

		if( ( k & Keys.Control ) != 0 ) {
			s += "Ctrl+";
		}

		if( ( k & Keys.Alt ) != 0 ) {
			s += "Alt+";
		}

		if( ( k & Keys.Shift ) != 0 ) {
			s += "Shift+";
		}

		return s + ( k & Keys.KeyCode );
	}

	public static bool Startup {
		get {
			using( var k = Registry.CurrentUser.OpenSubKey( RunKey ) ) {
				return k != null && k.GetValue( "ReopenExplorer" ) != null;
			}
		}
		set {
			using( var k = Registry.CurrentUser.CreateSubKey( RunKey ) ) {
				if( value ) {
					k.SetValue( "ReopenExplorer", "\"" + Application.ExecutablePath + "\"" );
				} else {
					k.DeleteValue( "ReopenExplorer", false );
				}
			}
		}
	}
}
