partial class HistoryForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.toolStrip = new System.Windows.Forms.ToolStrip();
        this.restoreButton = new System.Windows.Forms.ToolStripButton();
        this.openLocationButton = new System.Windows.Forms.ToolStripButton();
        this.copyPathButton = new System.Windows.Forms.ToolStripButton();
        this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
        this.deleteButton = new System.Windows.Forms.ToolStripButton();
        this.clearAllButton = new System.Windows.Forms.ToolStripButton();
        this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
        this.settingsButton = new System.Windows.Forms.ToolStripButton();
        this.searchPanel = new System.Windows.Forms.TableLayoutPanel();
        this.searchLabel = new System.Windows.Forms.Label();
        this.searchBox = new System.Windows.Forms.TextBox();
        this.split = new System.Windows.Forms.SplitContainer();
        this.list = new System.Windows.Forms.ListView();
        this.colName = new System.Windows.Forms.ColumnHeader();
        this.colLocation = new System.Windows.Forms.ColumnHeader();
        this.colCount = new System.Windows.Forms.ColumnHeader();
        this.colClosedAt = new System.Windows.Forms.ColumnHeader();
        this.detail = new System.Windows.Forms.ListView();
        this.colMark = new System.Windows.Forms.ColumnHeader();
        this.colIndex = new System.Windows.Forms.ColumnHeader();
        this.colDetailLocation = new System.Windows.Forms.ColumnHeader();
        this.detailLabel = new System.Windows.Forms.Label();
        this.statusLabel = new System.Windows.Forms.Label();
        this.toolStrip.SuspendLayout();
        this.searchPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.split)).BeginInit();
        this.split.Panel1.SuspendLayout();
        this.split.Panel2.SuspendLayout();
        this.split.SuspendLayout();
        this.SuspendLayout();
        //
        // toolStrip
        //
        this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
        this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restoreButton,
            this.openLocationButton,
            this.copyPathButton,
            this.toolStripSeparator1,
            this.deleteButton,
            this.clearAllButton,
            this.toolStripSeparator2,
            this.settingsButton});
        this.toolStrip.Location = new System.Drawing.Point(0, 0);
        this.toolStrip.Name = "toolStrip";
        this.toolStrip.Size = new System.Drawing.Size(884, 25);
        this.toolStrip.TabIndex = 0;
        //
        // restoreButton
        //
        this.restoreButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        this.restoreButton.Name = "restoreButton";
        this.restoreButton.Text = "復元";
        this.restoreButton.Click += new System.EventHandler(this.restoreButton_Click);
        //
        // openLocationButton
        //
        this.openLocationButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        this.openLocationButton.Name = "openLocationButton";
        this.openLocationButton.Text = "場所だけ開く";
        this.openLocationButton.Click += new System.EventHandler(this.openLocationButton_Click);
        //
        // copyPathButton
        //
        this.copyPathButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        this.copyPathButton.Name = "copyPathButton";
        this.copyPathButton.Text = "パスをコピー";
        this.copyPathButton.Click += new System.EventHandler(this.copyPathButton_Click);
        //
        // toolStripSeparator1
        //
        this.toolStripSeparator1.Name = "toolStripSeparator1";
        //
        // deleteButton
        //
        this.deleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        this.deleteButton.Name = "deleteButton";
        this.deleteButton.Text = "削除";
        this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
        //
        // clearAllButton
        //
        this.clearAllButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        this.clearAllButton.Name = "clearAllButton";
        this.clearAllButton.Text = "すべてクリア";
        this.clearAllButton.Click += new System.EventHandler(this.clearAllButton_Click);
        //
        // toolStripSeparator2
        //
        this.toolStripSeparator2.Name = "toolStripSeparator2";
        //
        // settingsButton
        //
        this.settingsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        this.settingsButton.Name = "settingsButton";
        this.settingsButton.Text = "設定...";
        this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
        //
        // searchPanel
        //
        this.searchPanel.ColumnCount = 2;
        this.searchPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
        this.searchPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.searchPanel.Controls.Add(this.searchLabel, 0, 0);
        this.searchPanel.Controls.Add(this.searchBox, 1, 0);
        this.searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
        this.searchPanel.Location = new System.Drawing.Point(0, 25);
        this.searchPanel.Name = "searchPanel";
        this.searchPanel.Padding = new System.Windows.Forms.Padding(4, 3, 4, 0);
        this.searchPanel.RowCount = 1;
        this.searchPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        this.searchPanel.Size = new System.Drawing.Size(884, 32);
        this.searchPanel.TabIndex = 1;
        //
        // searchLabel
        //
        this.searchLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
        this.searchLabel.AutoSize = true;
        this.searchLabel.Name = "searchLabel";
        this.searchLabel.TabIndex = 0;
        this.searchLabel.Text = "検索:";
        //
        // searchBox
        //
        this.searchBox.Dock = System.Windows.Forms.DockStyle.Fill;
        this.searchBox.Name = "searchBox";
        this.searchBox.PlaceholderText = "名前・パスで絞り込み";
        this.searchBox.TabIndex = 1;
        this.searchBox.TextChanged += new System.EventHandler(this.searchBox_TextChanged);
        //
        // split
        //
        this.split.Dock = System.Windows.Forms.DockStyle.Fill;
        this.split.Location = new System.Drawing.Point(0, 57);
        this.split.Name = "split";
        this.split.Orientation = System.Windows.Forms.Orientation.Horizontal;
        //
        // split.Panel1
        //
        this.split.Panel1.Controls.Add(this.list);
        //
        // split.Panel2
        //
        this.split.Panel2.Controls.Add(this.detail);
        this.split.Panel2.Controls.Add(this.detailLabel);
        this.split.Size = new System.Drawing.Size(884, 482);
        this.split.SplitterDistance = 300;
        this.split.TabIndex = 2;
        //
        // list
        //
        this.list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colLocation,
            this.colCount,
            this.colClosedAt});
        this.list.Dock = System.Windows.Forms.DockStyle.Fill;
        this.list.FullRowSelect = true;
        this.list.HideSelection = false;
        this.list.Name = "list";
        this.list.TabIndex = 0;
        this.list.UseCompatibleStateImageBehavior = false;
        this.list.View = System.Windows.Forms.View.Details;
        this.list.SelectedIndexChanged += new System.EventHandler(this.list_SelectedIndexChanged);
        this.list.DoubleClick += new System.EventHandler(this.list_DoubleClick);
        this.list.KeyDown += new System.Windows.Forms.KeyEventHandler(this.list_KeyDown);
        //
        // colName
        //
        this.colName.Text = "名前";
        this.colName.Width = 180;
        //
        // colLocation
        //
        this.colLocation.Text = "場所";
        this.colLocation.Width = 420;
        //
        // colCount
        //
        this.colCount.Text = "履歴数";
        this.colCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        this.colCount.Width = 60;
        //
        // colClosedAt
        //
        this.colClosedAt.Text = "閉じた時刻";
        this.colClosedAt.Width = 140;
        //
        // detail
        //
        this.detail.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colMark,
            this.colIndex,
            this.colDetailLocation});
        this.detail.Dock = System.Windows.Forms.DockStyle.Fill;
        this.detail.FullRowSelect = true;
        this.detail.HideSelection = false;
        this.detail.Name = "detail";
        this.detail.TabIndex = 1;
        this.detail.UseCompatibleStateImageBehavior = false;
        this.detail.View = System.Windows.Forms.View.Details;
        this.detail.DoubleClick += new System.EventHandler(this.detail_DoubleClick);
        this.detail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.detail_KeyDown);
        //
        // colMark
        //
        this.colMark.Text = "";
        this.colMark.Width = 24;
        //
        // colIndex
        //
        this.colIndex.Text = "#";
        this.colIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        this.colIndex.Width = 40;
        //
        // colDetailLocation
        //
        this.colDetailLocation.Text = "場所";
        this.colDetailLocation.Width = 760;
        //
        // detailLabel
        //
        this.detailLabel.Dock = System.Windows.Forms.DockStyle.Top;
        this.detailLabel.Name = "detailLabel";
        this.detailLabel.Size = new System.Drawing.Size(884, 20);
        this.detailLabel.TabIndex = 0;
        this.detailLabel.Text = "戻る/進む履歴";
        this.detailLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        //
        // statusLabel
        //
        this.statusLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.statusLabel.Location = new System.Drawing.Point(0, 539);
        this.statusLabel.Name = "statusLabel";
        this.statusLabel.Size = new System.Drawing.Size(884, 22);
        this.statusLabel.TabIndex = 3;
        this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        //
        // HistoryForm
        //
        this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        this.ClientSize = new System.Drawing.Size(884, 561);
        this.Controls.Add(this.split);
        this.Controls.Add(this.searchPanel);
        this.Controls.Add(this.toolStrip);
        this.Controls.Add(this.statusLabel);
        this.KeyPreview = true;
        this.Name = "HistoryForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "閉じたエクスプローラーの履歴";
        this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HistoryForm_KeyDown);
        this.toolStrip.ResumeLayout(false);
        this.toolStrip.PerformLayout();
        this.searchPanel.ResumeLayout(false);
        this.searchPanel.PerformLayout();
        this.split.Panel1.ResumeLayout(false);
        this.split.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.split)).EndInit();
        this.split.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStrip;
    private System.Windows.Forms.ToolStripButton restoreButton;
    private System.Windows.Forms.ToolStripButton openLocationButton;
    private System.Windows.Forms.ToolStripButton copyPathButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton deleteButton;
    private System.Windows.Forms.ToolStripButton clearAllButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripButton settingsButton;
    private System.Windows.Forms.TableLayoutPanel searchPanel;
    private System.Windows.Forms.Label searchLabel;
    private System.Windows.Forms.TextBox searchBox;
    private System.Windows.Forms.SplitContainer split;
    private System.Windows.Forms.ListView list;
    private System.Windows.Forms.ColumnHeader colName;
    private System.Windows.Forms.ColumnHeader colLocation;
    private System.Windows.Forms.ColumnHeader colCount;
    private System.Windows.Forms.ColumnHeader colClosedAt;
    private System.Windows.Forms.ListView detail;
    private System.Windows.Forms.ColumnHeader colMark;
    private System.Windows.Forms.ColumnHeader colIndex;
    private System.Windows.Forms.ColumnHeader colDetailLocation;
    private System.Windows.Forms.Label detailLabel;
    private System.Windows.Forms.Label statusLabel;
}
