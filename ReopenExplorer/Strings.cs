using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;

// UI文字列。実行ファイルと同じ場所の Lang\{言語コード}.json から読み込む
// 言語を追加するときは Lang\en.json をコピーして翻訳し、"_name" に表示名を書く
static class S
{
	public const string Auto = "auto";
	const string FallbackCode = "en";

	static readonly string LangDir = Path.Combine( AppContext.BaseDirectory, "Lang" );
	static Dictionary<string, string> current = new Dictionary<string, string>();
	static Dictionary<string, string> fallback = new Dictionary<string, string>();

	public static string Code { get; private set; }

	// 言語が切り替わったときに発生する
	public static event EventHandler LanguageChanged;

	// lang: "auto" または言語コード（ファイル名）
	public static void Init( string lang ) {
		var code = lang;
		if( string.IsNullOrEmpty( code ) || code == Auto || !File.Exists( PathOf( code ) ) ) {
			code = Detect();
		}
		if( code == Code ) {
			return;
		}

		fallback = Read( FallbackCode );
		current = code == FallbackCode ? fallback : Read( code );
		bool first = Code == null;
		Code = code;
		if( !first ) {
			LanguageChanged?.Invoke( null, EventArgs.Empty );
		}
	}

	// Windows の表示言語に合う言語ファイルを探す（ja-JP → ja-JP.json → ja.json）
	static string Detect() {
		var c = CultureInfo.CurrentUICulture;
		foreach( var code in new[] { c.Name, c.TwoLetterISOLanguageName } ) {
			if( File.Exists( PathOf( code ) ) ) {
				return code;
			}
		}
		return FallbackCode;
	}

	// 利用できる言語の一覧（コード, 表示名）
	public static List<KeyValuePair<string, string>> Languages() {
		var list = new List<KeyValuePair<string, string>>();
		if( !Directory.Exists( LangDir ) ) {
			return list;
		}
		foreach( var f in Directory.GetFiles( LangDir, "*.json" ) ) {
			var code = Path.GetFileNameWithoutExtension( f );
			var d = Read( code );
			list.Add( new KeyValuePair<string, string>( code, d.TryGetValue( "_name", out var n ) ? n : code ) );
		}
		list.Sort( ( a, b ) => string.Compare( a.Value, b.Value, StringComparison.CurrentCulture ) );
		return list;
	}

	static string PathOf( string code ) {
		return Path.Combine( LangDir, code + ".json" );
	}

	static Dictionary<string, string> Read( string code ) {
		try {
			return JsonSerializer.Deserialize<Dictionary<string, string>>( File.ReadAllText( PathOf( code ) ) ) ?? new Dictionary<string, string>();
		} catch {
			return new Dictionary<string, string>();
		}
	}

	// 現在の言語 → 英語 → キー名 の順で探す
	static string G( [CallerMemberName] string key = null ) {
		if( current.TryGetValue( key, out var v ) ) {
			return v;
		}
		if( fallback.TryGetValue( key, out v ) ) {
			return v;
		}
		return key;
	}

	static string F( string key, params object[] args ) {
		try {
			return string.Format( G( key ), args );
		} catch( FormatException ) {
			return G( key );
		}
	}

	// トレイ
	public static string ShowHistory => G();
	public static string RestoreLatest => G();
	public static string RecentlyClosed => G();
	public static string Favorites => G();
	public static string SettingsMenu => G();
	public static string Exit => G();
	public static string HotkeyInUse( string k ) => F( nameof( HotkeyInUse ), k );
	public static string ClosedRecorded => G();
	public static string OpenFailed => G();
	public static string RestoreFailed => G();

	// 履歴一覧
	public static string HistoryTitle => G();
	public static string SearchPlaceholder => G();
	public static string Search => G();
	public static string ColName => G();
	public static string ColLocation => G();
	public static string ColHistoryCount => G();
	public static string ColClosedAt => G();
	public static string DetailCaption => G();
	public static string Restore => G();
	public static string RestoreTip => G();
	public static string OpenLocation => G();
	public static string OpenLocationTip => G();
	public static string CopyPath => G();
	public static string Favorite => G();
	public static string FavoriteTip => G();
	public static string Delete => G();
	public static string DeleteTip => G();
	public static string ClearAll => G();
	public static string ClearAllConfirm => G();
	public static string Status( int shown, int total, int max, string hotkey ) => F( nameof( Status ), shown, total, max, hotkey );
	public static string DateFormat => G();

	// 設定
	public static string SettingsTitle => G();
	public static string MaxClosed => G();
	public static string MaxHistory => G();
	public static string PollInterval => G();
	public static string RestoreNav => G();
	public static string Notify => G();
	public static string Startup => G();
	public static string UseHotkey => G();
	public static string HotkeyLabel => G();
	public static string HotkeyHint => G();
	public static string HotkeyNeedsModifier => G();
	public static string None => G();
	public static string Language => G();
	public static string LangAuto => G();
	public static string Cancel => G();
}
