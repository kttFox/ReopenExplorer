partial class SettingsForm
{
	private System.ComponentModel.IContainer components = null;

	protected override void Dispose(bool disposing) {
		if (disposing && (components != null)) {
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Windows Form Designer generated code

	private void InitializeComponent() {
		this.table = new System.Windows.Forms.TableLayoutPanel();
		this.maxClosedLabel = new System.Windows.Forms.Label();
		this.maxClosed = new System.Windows.Forms.NumericUpDown();
		this.maxHistoryLabel = new System.Windows.Forms.Label();
		this.maxHistory = new System.Windows.Forms.NumericUpDown();
		this.pollLabel = new System.Windows.Forms.Label();
		this.poll = new System.Windows.Forms.NumericUpDown();
		this.languageLabel = new System.Windows.Forms.Label();
		this.language = new System.Windows.Forms.ComboBox();
		this.restoreNav = new System.Windows.Forms.CheckBox();
		this.notify = new System.Windows.Forms.CheckBox();
		this.startup = new System.Windows.Forms.CheckBox();
		this.hotkeyOn = new System.Windows.Forms.CheckBox();
		this.hotkeyLabel = new System.Windows.Forms.Label();
		this.hotkeyPanel = new System.Windows.Forms.FlowLayoutPanel();
		this.hotkeyWin = new System.Windows.Forms.CheckBox();
		this.hotkeyBox = new System.Windows.Forms.TextBox();
		this.hotkeyHint = new System.Windows.Forms.Label();
		this.buttonPanel = new System.Windows.Forms.FlowLayoutPanel();
		this.cancelButton = new System.Windows.Forms.Button();
		this.okButton = new System.Windows.Forms.Button();
		this.table.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)(this.maxClosed)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.maxHistory)).BeginInit();
		((System.ComponentModel.ISupportInitialize)(this.poll)).BeginInit();
		this.hotkeyPanel.SuspendLayout();
		this.buttonPanel.SuspendLayout();
		this.SuspendLayout();
		//
		// table
		//
		this.table.AutoSize = true;
		this.table.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.table.ColumnCount = 2;
		this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		this.table.Controls.Add(this.maxClosedLabel, 0, 0);
		this.table.Controls.Add(this.maxClosed, 1, 0);
		this.table.Controls.Add(this.maxHistoryLabel, 0, 1);
		this.table.Controls.Add(this.maxHistory, 1, 1);
		this.table.Controls.Add(this.pollLabel, 0, 2);
		this.table.Controls.Add(this.poll, 1, 2);
		this.table.Controls.Add(this.languageLabel, 0, 3);
		this.table.Controls.Add(this.language, 1, 3);
		this.table.Controls.Add(this.restoreNav, 0, 4);
		this.table.Controls.Add(this.notify, 0, 5);
		this.table.Controls.Add(this.startup, 0, 6);
		this.table.Controls.Add(this.hotkeyOn, 0, 7);
		this.table.Controls.Add(this.hotkeyLabel, 0, 8);
		this.table.Controls.Add(this.hotkeyPanel, 1, 8);
		this.table.Controls.Add(this.hotkeyHint, 0, 9);
		this.table.Controls.Add(this.buttonPanel, 0, 10);
		this.table.Location = new System.Drawing.Point(12, 12);
		this.table.Name = "table";
		this.table.RowCount = 11;
		this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
		this.table.TabIndex = 0;
		//
		// maxClosedLabel
		//
		this.maxClosedLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
		this.maxClosedLabel.AutoSize = true;
		this.maxClosedLabel.Margin = new System.Windows.Forms.Padding(3, 0, 12, 0);
		this.maxClosedLabel.Name = "maxClosedLabel";
		this.maxClosedLabel.Text = "Closed windows to keep";
		//
		// maxClosed
		//
		this.maxClosed.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
		this.maxClosed.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
		this.maxClosed.Name = "maxClosed";
		this.maxClosed.Size = new System.Drawing.Size(80, 23);
		this.maxClosed.TabIndex = 1;
		this.maxClosed.Value = new decimal(new int[] { 100, 0, 0, 0 });
		//
		// maxHistoryLabel
		//
		this.maxHistoryLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
		this.maxHistoryLabel.AutoSize = true;
		this.maxHistoryLabel.Margin = new System.Windows.Forms.Padding(3, 0, 12, 0);
		this.maxHistoryLabel.Name = "maxHistoryLabel";
		this.maxHistoryLabel.Text = "History entries per window";
		//
		// maxHistory
		//
		this.maxHistory.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
		this.maxHistory.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
		this.maxHistory.Name = "maxHistory";
		this.maxHistory.Size = new System.Drawing.Size(80, 23);
		this.maxHistory.TabIndex = 2;
		this.maxHistory.Value = new decimal(new int[] { 50, 0, 0, 0 });
		//
		// pollLabel
		//
		this.pollLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
		this.pollLabel.AutoSize = true;
		this.pollLabel.Margin = new System.Windows.Forms.Padding(3, 0, 12, 0);
		this.pollLabel.Name = "pollLabel";
		this.pollLabel.Text = "Polling interval (ms)";
		//
		// poll
		//
		this.poll.Increment = new decimal(new int[] { 100, 0, 0, 0 });
		this.poll.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
		this.poll.Minimum = new decimal(new int[] { 100, 0, 0, 0 });
		this.poll.Name = "poll";
		this.poll.Size = new System.Drawing.Size(80, 23);
		this.poll.TabIndex = 3;
		this.poll.Value = new decimal(new int[] { 400, 0, 0, 0 });
		//
		// languageLabel
		//
		this.languageLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
		this.languageLabel.AutoSize = true;
		this.languageLabel.Margin = new System.Windows.Forms.Padding(3, 0, 12, 0);
		this.languageLabel.Name = "languageLabel";
		this.languageLabel.Text = "Language";
		//
		// language
		//
		this.language.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.language.Name = "language";
		this.language.Size = new System.Drawing.Size(200, 23);
		this.language.TabIndex = 4;
		//
		// restoreNav
		//
		this.restoreNav.AutoSize = true;
		this.table.SetColumnSpan(this.restoreNav, 2);
		this.restoreNav.Name = "restoreNav";
		this.restoreNav.TabIndex = 5;
		this.restoreNav.Text = "Restore back/forward history when reopening";
		//
		// notify
		//
		this.notify.AutoSize = true;
		this.table.SetColumnSpan(this.notify, 2);
		this.notify.Name = "notify";
		this.notify.TabIndex = 6;
		this.notify.Text = "Notify when a window is closed";
		//
		// startup
		//
		this.startup.AutoSize = true;
		this.table.SetColumnSpan(this.startup, 2);
		this.startup.Name = "startup";
		this.startup.TabIndex = 7;
		this.startup.Text = "Run at Windows startup";
		//
		// hotkeyOn
		//
		this.hotkeyOn.AutoSize = true;
		this.table.SetColumnSpan(this.hotkeyOn, 2);
		this.hotkeyOn.Name = "hotkeyOn";
		this.hotkeyOn.TabIndex = 8;
		this.hotkeyOn.Text = "Use hotkey";
		this.hotkeyOn.CheckedChanged += new System.EventHandler(this.hotkeyOn_CheckedChanged);
		//
		// hotkeyLabel
		//
		this.hotkeyLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
		this.hotkeyLabel.AutoSize = true;
		this.hotkeyLabel.Margin = new System.Windows.Forms.Padding(3, 0, 12, 0);
		this.hotkeyLabel.Name = "hotkeyLabel";
		this.hotkeyLabel.Text = "Reopen last closed";
		//
		// hotkeyPanel
		//
		this.hotkeyPanel.AutoSize = true;
		this.hotkeyPanel.Controls.Add(this.hotkeyWin);
		this.hotkeyPanel.Controls.Add(this.hotkeyBox);
		this.hotkeyPanel.Margin = new System.Windows.Forms.Padding(0);
		this.hotkeyPanel.Name = "hotkeyPanel";
		this.hotkeyPanel.TabIndex = 9;
		this.hotkeyPanel.WrapContents = false;
		//
		// hotkeyWin
		//
		this.hotkeyWin.Anchor = System.Windows.Forms.AnchorStyles.Left;
		this.hotkeyWin.AutoSize = true;
		this.hotkeyWin.Name = "hotkeyWin";
		this.hotkeyWin.TabIndex = 0;
		this.hotkeyWin.Text = "Win";
		//
		// hotkeyBox
		//
		this.hotkeyBox.BackColor = System.Drawing.SystemColors.Window;
		this.hotkeyBox.Name = "hotkeyBox";
		this.hotkeyBox.ReadOnly = true;
		this.hotkeyBox.Size = new System.Drawing.Size(160, 23);
		this.hotkeyBox.TabIndex = 1;
		this.hotkeyBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.hotkeyBox_KeyDown);
		//
		// hotkeyHint
		//
		this.hotkeyHint.AutoSize = true;
		this.table.SetColumnSpan(this.hotkeyHint, 2);
		this.hotkeyHint.ForeColor = System.Drawing.SystemColors.GrayText;
		this.hotkeyHint.Name = "hotkeyHint";
		this.hotkeyHint.Text = "Click the key box and press the key combination";
		//
		// buttonPanel
		//
		this.buttonPanel.AutoSize = true;
		this.table.SetColumnSpan(this.buttonPanel, 2);
		this.buttonPanel.Controls.Add(this.cancelButton);
		this.buttonPanel.Controls.Add(this.okButton);
		this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.buttonPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
		this.buttonPanel.Margin = new System.Windows.Forms.Padding(0, 12, 0, 0);
		this.buttonPanel.Name = "buttonPanel";
		this.buttonPanel.TabIndex = 10;
		//
		// cancelButton
		//
		this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cancelButton.Name = "cancelButton";
		this.cancelButton.Size = new System.Drawing.Size(90, 27);
		this.cancelButton.TabIndex = 1;
		this.cancelButton.Text = "Cancel";
		//
		// okButton
		//
		this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.okButton.Name = "okButton";
		this.okButton.Size = new System.Drawing.Size(90, 27);
		this.okButton.TabIndex = 0;
		this.okButton.Text = "OK";
		this.okButton.Click += new System.EventHandler(this.okButton_Click);
		//
		// SettingsForm
		//
		this.AcceptButton = this.okButton;
		this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
		this.AutoSize = true;
		this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
		this.CancelButton = this.cancelButton;
		this.ClientSize = new System.Drawing.Size(420, 380);
		this.Controls.Add(this.table);
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		this.MaximizeBox = false;
		this.MinimizeBox = false;
		this.Name = "SettingsForm";
		this.Padding = new System.Windows.Forms.Padding(12);
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Settings";
		this.table.ResumeLayout(false);
		this.table.PerformLayout();
		((System.ComponentModel.ISupportInitialize)(this.maxClosed)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.maxHistory)).EndInit();
		((System.ComponentModel.ISupportInitialize)(this.poll)).EndInit();
		this.hotkeyPanel.ResumeLayout(false);
		this.hotkeyPanel.PerformLayout();
		this.buttonPanel.ResumeLayout(false);
		this.ResumeLayout(false);
		this.PerformLayout();
	}

	#endregion

	private System.Windows.Forms.TableLayoutPanel table;
	private System.Windows.Forms.Label maxClosedLabel;
	private System.Windows.Forms.NumericUpDown maxClosed;
	private System.Windows.Forms.Label maxHistoryLabel;
	private System.Windows.Forms.NumericUpDown maxHistory;
	private System.Windows.Forms.Label pollLabel;
	private System.Windows.Forms.NumericUpDown poll;
	private System.Windows.Forms.Label languageLabel;
	private System.Windows.Forms.ComboBox language;
	private System.Windows.Forms.CheckBox restoreNav;
	private System.Windows.Forms.CheckBox notify;
	private System.Windows.Forms.CheckBox startup;
	private System.Windows.Forms.CheckBox hotkeyOn;
	private System.Windows.Forms.Label hotkeyLabel;
	private System.Windows.Forms.FlowLayoutPanel hotkeyPanel;
	private System.Windows.Forms.CheckBox hotkeyWin;
	private System.Windows.Forms.TextBox hotkeyBox;
	private System.Windows.Forms.Label hotkeyHint;
	private System.Windows.Forms.FlowLayoutPanel buttonPanel;
	private System.Windows.Forms.Button cancelButton;
	private System.Windows.Forms.Button okButton;
}
