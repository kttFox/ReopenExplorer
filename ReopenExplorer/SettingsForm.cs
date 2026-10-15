using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

partial class SettingsForm : Form
{
	readonly List<string> languageCodes = new List<string>();
	readonly AppSettings source;
	Keys hotkey;

	public AppSettings Result { get; private set; }

	// デザイナー用
	public SettingsForm() {
		InitializeComponent();
	}

	public SettingsForm(AppSettings s) : this() {
		source = s;
		Font = SystemFonts.MessageBoxFont;
		Icon = App.AppIcon;
		ApplyText();

		maxClosed.Value = Math.Max(1, Math.Min(1000, s.MaxClosed));
		maxHistory.Value = Math.Max(1, Math.Min(500, s.MaxHistoryPerWindow));
		poll.Value = Math.Max(100, Math.Min(5000, s.PollIntervalMs));
		language.SelectedIndex = Math.Max(0, languageCodes.IndexOf(s.Language));
		restoreNav.Checked = s.RestoreNavHistory;
		notify.Checked = s.ShowNotification;
		startup.Checked = AppSettings.Startup;
		hotkeyOn.Checked = s.HotkeyEnabled;
		hotkeyWin.Checked = s.HotkeyWin;
		hotkey = s.Hotkey;
		hotkeyBox.Text = AppSettings.FormatHotkey(hotkey, false);
		UpdateHotkeyEnabled();

		// 選んだ言語をすぐに反映し、キャンセル時は元に戻す
		language.SelectedIndexChanged += language_SelectedIndexChanged;
		FormClosed += (o, e) => {
			if (DialogResult != DialogResult.OK) {
				S.Init(source.Language);
			}
		};
	}

	void language_SelectedIndexChanged(object sender, EventArgs e) {
		if (language.SelectedIndex < 0) {
			return;
		}
		int index = language.SelectedIndex;
		S.Init(languageCodes[index]);
		language.SelectedIndexChanged -= language_SelectedIndexChanged;
		ApplyText();
		language.SelectedIndex = index;
		language.SelectedIndexChanged += language_SelectedIndexChanged;
	}

	void ApplyText() {
		Text = S.SettingsTitle;
		maxClosedLabel.Text = S.MaxClosed;
		maxHistoryLabel.Text = S.MaxHistory;
		pollLabel.Text = S.PollInterval;
		languageLabel.Text = S.Language;
		language.Items.Clear();
		languageCodes.Clear();
		language.Items.Add(S.LangAuto);
		languageCodes.Add(S.Auto);
		foreach (var l in S.Languages()) {
			language.Items.Add(l.Value);
			languageCodes.Add(l.Key);
		}
		restoreNav.Text = S.RestoreNav;
		notify.Text = S.Notify;
		startup.Text = S.Startup;
		hotkeyOn.Text = S.UseHotkey;
		hotkeyLabel.Text = S.HotkeyLabel;
		hotkeyHint.Text = S.HotkeyHint;
		cancelButton.Text = S.Cancel;
	}

	void UpdateHotkeyEnabled() {
		hotkeyBox.Enabled = hotkeyOn.Checked;
		hotkeyWin.Enabled = hotkeyOn.Checked;
	}

	void hotkeyOn_CheckedChanged(object sender, EventArgs e) {
		UpdateHotkeyEnabled();
	}

	void hotkeyBox_KeyDown(object sender, KeyEventArgs e) {
		e.SuppressKeyPress = true;
		var code = e.KeyCode;
		if (code == Keys.ControlKey || code == Keys.ShiftKey || code == Keys.Menu || code == Keys.LWin || code == Keys.RWin) {
			return;
		}
		hotkey = e.KeyData;
		hotkeyBox.Text = AppSettings.FormatHotkey(hotkey, false);
	}

	void okButton_Click(object sender, EventArgs e) {
		if (hotkeyOn.Checked && (hotkey & Keys.Modifiers) == 0 && !hotkeyWin.Checked) {
			MessageBox.Show(this, S.HotkeyNeedsModifier, Text);
			DialogResult = DialogResult.None;
			return;
		}

		Result = source.Clone();
		Result.MaxClosed = (int)maxClosed.Value;
		Result.MaxHistoryPerWindow = (int)maxHistory.Value;
		Result.PollIntervalMs = (int)poll.Value;
		Result.Language = languageCodes[Math.Max(0, language.SelectedIndex)];
		Result.RestoreNavHistory = restoreNav.Checked;
		Result.ShowNotification = notify.Checked;
		Result.HotkeyEnabled = hotkeyOn.Checked;
		Result.HotkeyWin = hotkeyWin.Checked;
		Result.Hotkey = hotkey;
		try {
			AppSettings.Startup = startup.Checked;
		} catch (Exception ex) {
			MessageBox.Show(this, ex.Message, Text);
		}
	}
}
