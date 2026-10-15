using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

// 閉じたウィンドウの一覧と、選択したウィンドウの戻る/進む履歴を表示する画面
partial class HistoryForm : Form
{
	readonly App app;

	// デザイナー用
	public HistoryForm() {
		InitializeComponent();
	}

	public HistoryForm(App app) : this() {
		this.app = app;
		Icon = App.AppIcon;
		Font = SystemFonts.MessageBoxFont;
		ApplyText();
		RefreshList();
		S.LanguageChanged += OnLanguageChanged;
		FormClosed += (s, e) => S.LanguageChanged -= OnLanguageChanged;
	}

	void OnLanguageChanged(object sender, EventArgs e) {
		ApplyText();
		RefreshList();
	}

	void ApplyText() {
		Text = S.HistoryTitle;
		restoreButton.Text = S.Restore;
		restoreButton.ToolTipText = S.RestoreTip;
		openLocationButton.Text = S.OpenLocation;
		openLocationButton.ToolTipText = S.OpenLocationTip;
		copyPathButton.Text = S.CopyPath;
		deleteButton.Text = S.Delete;
		deleteButton.ToolTipText = S.DeleteTip;
		clearAllButton.Text = S.ClearAll;
		settingsButton.Text = S.SettingsMenu;
		searchLabel.Text = S.Search;
		searchBox.PlaceholderText = S.SearchPlaceholder;
		colName.Text = S.ColName;
		colLocation.Text = S.ColLocation;
		colCount.Text = S.ColHistoryCount;
		colClosedAt.Text = S.ColClosedAt;
		colDetailLocation.Text = S.ColLocation;
		detailLabel.Text = S.DetailCaption;
	}

	List<ClosedEntry> Selected() {
		return list.SelectedItems.Cast<ListViewItem>().Select(i => (ClosedEntry)i.Tag).ToList();
	}

	public void RefreshList() {
		if (app == null) {
			return;
		}

		var keep = new HashSet<ClosedEntry>(Selected());
		string q = searchBox.Text.Trim();
		var items = app.ClosedEntries.Where(e => q == "" ||
			(e.Title ?? "").IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0 ||
			e.History.Any(h => h.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0));

		list.BeginUpdate();
		list.Items.Clear();
		foreach (var e in items) {
			var it = new ListViewItem(new[] { e.Title, e.Current, e.History.Count.ToString(), e.ClosedAt.ToString(S.DateFormat) }) { Tag = e };
			it.Selected = keep.Contains(e);
			list.Items.Add(it);
		}
		list.EndUpdate();
		statusLabel.Text = S.Status(list.Items.Count, app.ClosedEntries.Count, app.Settings.MaxClosed, app.Settings.HotkeyText());
		ShowDetail();
	}

	void ShowDetail() {
		detail.BeginUpdate();
		detail.Items.Clear();
		var e = list.SelectedItems.Count == 1 ? (ClosedEntry)list.SelectedItems[0].Tag : null;
		if (e != null) {
			for (int i = 0; i < e.History.Count; i++) {
				var it = new ListViewItem(new[] { i == e.Position ? "▶" : "", (i + 1).ToString(), e.History[i] }) { Tag = e.History[i] };
				if (i == e.Position) {
					it.Font = new Font(detail.Font, FontStyle.Bold);
				} else if (i > e.Position) {
					it.ForeColor = SystemColors.GrayText; // 進む側
				}
				detail.Items.Add(it);
			}
		}
		detail.EndUpdate();
	}

	void RestoreSelected() {
		foreach (var e in Selected()) {
			app.Restore(e);
		}
	}

	void OpenDetailFolder() {
		foreach (ListViewItem it in detail.SelectedItems) {
			app.OpenFolder((string)it.Tag);
		}
	}

	// ---- イベント ----
	void restoreButton_Click(object sender, EventArgs e) {
		RestoreSelected();
	}

	void openLocationButton_Click(object sender, EventArgs e) {
		foreach (var c in Selected()) {
			app.OpenFolder(c.Current);
		}
	}

	void copyPathButton_Click(object sender, EventArgs e) {
		var text = string.Join(Environment.NewLine, Selected().Select(c => c.Current));
		if (text != "") {
			Clipboard.SetText(text);
		}
	}

	void deleteButton_Click(object sender, EventArgs e) {
		app.Remove(Selected());
	}

	void clearAllButton_Click(object sender, EventArgs e) {
		if (MessageBox.Show(this, S.ClearAllConfirm, Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) {
			app.Remove(app.ClosedEntries.ToList());
		}
	}

	void settingsButton_Click(object sender, EventArgs e) {
		app.ShowSettings(this);
	}

	void searchBox_TextChanged(object sender, EventArgs e) {
		RefreshList();
	}

	void list_SelectedIndexChanged(object sender, EventArgs e) {
		ShowDetail();
	}

	void list_DoubleClick(object sender, EventArgs e) {
		RestoreSelected();
	}

	void list_KeyDown(object sender, KeyEventArgs e) {
		if (e.KeyCode == Keys.Enter) {
			RestoreSelected();
		}
		if (e.KeyCode == Keys.Delete) {
			app.Remove(Selected());
		}
		if (e.KeyCode == Keys.A && e.Control) {
			foreach (ListViewItem it in list.Items) {
				it.Selected = true;
			}
		}
	}

	void detail_DoubleClick(object sender, EventArgs e) {
		OpenDetailFolder();
	}

	void detail_KeyDown(object sender, KeyEventArgs e) {
		if (e.KeyCode == Keys.Enter) {
			OpenDetailFolder();
		}
	}

	void HistoryForm_KeyDown(object sender, KeyEventArgs e) {
		if (e.KeyCode == Keys.F && e.Control) {
			searchBox.Focus();
			searchBox.SelectAll();
		}
		if (e.KeyCode == Keys.Escape) {
			Close();
		}
	}
}
