namespace Hover
{
  partial class LauncherForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LauncherForm));
      this.mImageList = new System.Windows.Forms.ImageList(this.components);
      this.grpPackageInfo = new System.Windows.Forms.GroupBox();
      this.editVerId = new System.Windows.Forms.TextBox();
      this.editPkgId = new System.Windows.Forms.TextBox();
      this.editPkgName = new System.Windows.Forms.TextBox();
      this.lblVerId = new System.Windows.Forms.Label();
      this.lblPkgId = new System.Windows.Forms.Label();
      this.lblPkgName = new System.Windows.Forms.Label();
      this.mToolTipSupport = new System.Windows.Forms.ToolTip(this.components);
      this.txtFilter = new System.Windows.Forms.TextBox();
      this.btnRefresh = new System.Windows.Forms.Button();
      this.btnClearFilter = new System.Windows.Forms.Button();
      this.btnHelp = new System.Windows.Forms.Button();
      this.contextMenuHelp = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.menuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
      this.menuHelpLearnMore = new System.Windows.Forms.ToolStripMenuItem();
      this.tabMSIX = new System.Windows.Forms.TabPage();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.panelFilter = new System.Windows.Forms.Panel();
      this.listMSIXApps = new System.Windows.Forms.ListBox();
      this.mExePicker = new System.Windows.Forms.OpenFileDialog();
      this.tableLayoutTools = new System.Windows.Forms.TableLayoutPanel();
      this.panelAppList = new System.Windows.Forms.Panel();
      this.packageTab = new System.Windows.Forms.TabControl();
      this.tabAPPV = new System.Windows.Forms.TabPage();
      this.listAppvApps = new System.Windows.Forms.ListBox();
      this.panelExe = new System.Windows.Forms.Panel();
      this.progressBar = new System.Windows.Forms.ProgressBar();
      this.lblProcess = new System.Windows.Forms.Label();
      this.listProcesses = new System.Windows.Forms.ListView();
      this.imgAdvInstLogo = new System.Windows.Forms.PictureBox();
      this.timerFilter = new System.Windows.Forms.Timer(this.components);
      this.grpPackageInfo.SuspendLayout();
      this.contextMenuHelp.SuspendLayout();
      this.tabMSIX.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.tableLayoutTools.SuspendLayout();
      this.panelAppList.SuspendLayout();
      this.packageTab.SuspendLayout();
      this.tabAPPV.SuspendLayout();
      this.panelExe.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.imgAdvInstLogo)).BeginInit();
      this.SuspendLayout();
      // 
      // mImageList
      // 
      this.mImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("mImageList.ImageStream")));
      this.mImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.mImageList.Images.SetKeyName(0, "plus32.ico");
      // 
      // grpPackageInfo
      // 
      this.grpPackageInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.grpPackageInfo.BackColor = System.Drawing.SystemColors.Window;
      this.grpPackageInfo.Controls.Add(this.editVerId);
      this.grpPackageInfo.Controls.Add(this.editPkgId);
      this.grpPackageInfo.Controls.Add(this.editPkgName);
      this.grpPackageInfo.Controls.Add(this.lblVerId);
      this.grpPackageInfo.Controls.Add(this.lblPkgId);
      this.grpPackageInfo.Controls.Add(this.lblPkgName);
      this.grpPackageInfo.Enabled = false;
      this.grpPackageInfo.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.grpPackageInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.grpPackageInfo.Location = new System.Drawing.Point(12, 303);
      this.grpPackageInfo.Name = "grpPackageInfo";
      this.grpPackageInfo.Size = new System.Drawing.Size(720, 118);
      this.grpPackageInfo.TabIndex = 8;
      this.grpPackageInfo.TabStop = false;
      this.grpPackageInfo.Text = "Package Information";
      // 
      // editVerId
      // 
      this.editVerId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.editVerId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.editVerId.Location = new System.Drawing.Point(96, 80);
      this.editVerId.Name = "editVerId";
      this.editVerId.ReadOnly = true;
      this.editVerId.Size = new System.Drawing.Size(610, 23);
      this.editVerId.TabIndex = 5;
      // 
      // editPkgId
      // 
      this.editPkgId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.editPkgId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.editPkgId.Location = new System.Drawing.Point(96, 51);
      this.editPkgId.Name = "editPkgId";
      this.editPkgId.ReadOnly = true;
      this.editPkgId.Size = new System.Drawing.Size(610, 23);
      this.editPkgId.TabIndex = 4;
      // 
      // editPkgName
      // 
      this.editPkgName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.editPkgName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.editPkgName.Location = new System.Drawing.Point(96, 22);
      this.editPkgName.Name = "editPkgName";
      this.editPkgName.ReadOnly = true;
      this.editPkgName.Size = new System.Drawing.Size(610, 23);
      this.editPkgName.TabIndex = 3;
      // 
      // lblVerId
      // 
      this.lblVerId.AutoSize = true;
      this.lblVerId.Location = new System.Drawing.Point(6, 87);
      this.lblVerId.Name = "lblVerId";
      this.lblVerId.Size = new System.Drawing.Size(48, 15);
      this.lblVerId.TabIndex = 2;
      this.lblVerId.Text = "Version:";
      // 
      // lblPkgId
      // 
      this.lblPkgId.AutoSize = true;
      this.lblPkgId.Location = new System.Drawing.Point(6, 55);
      this.lblPkgId.Name = "lblPkgId";
      this.lblPkgId.Size = new System.Drawing.Size(80, 15);
      this.lblPkgId.TabIndex = 1;
      this.lblPkgId.Text = "Family Name:";
      // 
      // lblPkgName
      // 
      this.lblPkgName.AutoSize = true;
      this.lblPkgName.Location = new System.Drawing.Point(6, 25);
      this.lblPkgName.Name = "lblPkgName";
      this.lblPkgName.Size = new System.Drawing.Size(46, 15);
      this.lblPkgName.TabIndex = 0;
      this.lblPkgName.Text = "AppID: ";
      // 
      // txtFilter
      // 
      this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtFilter.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtFilter.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtFilter.Location = new System.Drawing.Point(26, 242);
      this.txtFilter.Margin = new System.Windows.Forms.Padding(0);
      this.txtFilter.MaxLength = 40;
      this.txtFilter.Multiline = true;
      this.txtFilter.Name = "txtFilter";
      this.txtFilter.Size = new System.Drawing.Size(385, 16);
      this.txtFilter.TabIndex = 11;
      this.mToolTipSupport.SetToolTip(this.txtFilter, "Filter package list");
      this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
      this.txtFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFilter_KeyDown);
      this.txtFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilter_KeyPress);
      // 
      // btnRefresh
      // 
      this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnRefresh.BackColor = System.Drawing.SystemColors.Window;
      this.btnRefresh.BackgroundImage = global::Hover.Properties.Resources.refresh;
      this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
      this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.SystemColors.Window;
      this.btnRefresh.FlatAppearance.BorderSize = 0;
      this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnRefresh.ForeColor = System.Drawing.Color.Red;
      this.btnRefresh.Location = new System.Drawing.Point(404, 5);
      this.btnRefresh.Margin = new System.Windows.Forms.Padding(1);
      this.btnRefresh.Name = "btnRefresh";
      this.btnRefresh.Size = new System.Drawing.Size(19, 19);
      this.btnRefresh.TabIndex = 16;
      this.btnRefresh.TabStop = false;
      this.mToolTipSupport.SetToolTip(this.btnRefresh, "Refresh package list (F5)");
      this.btnRefresh.UseVisualStyleBackColor = false;
      this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
      // 
      // btnClearFilter
      // 
      this.btnClearFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClearFilter.BackColor = System.Drawing.SystemColors.Window;
      this.btnClearFilter.BackgroundImage = global::Hover.Properties.Resources.close;
      this.btnClearFilter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
      this.btnClearFilter.FlatAppearance.BorderColor = System.Drawing.SystemColors.Window;
      this.btnClearFilter.FlatAppearance.BorderSize = 0;
      this.btnClearFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnClearFilter.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnClearFilter.ForeColor = System.Drawing.Color.Red;
      this.btnClearFilter.Location = new System.Drawing.Point(393, 240);
      this.btnClearFilter.Margin = new System.Windows.Forms.Padding(1);
      this.btnClearFilter.Name = "btnClearFilter";
      this.btnClearFilter.Size = new System.Drawing.Size(19, 19);
      this.btnClearFilter.TabIndex = 12;
      this.btnClearFilter.TabStop = false;
      this.mToolTipSupport.SetToolTip(this.btnClearFilter, "Clear package filter (ESC)");
      this.btnClearFilter.UseVisualStyleBackColor = false;
      this.btnClearFilter.Visible = false;
      this.btnClearFilter.Click += new System.EventHandler(this.btnClearFilter_Click);
      // 
      // btnHelp
      // 
      this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnHelp.BackgroundImage = global::Hover.Properties.Resources.help;
      this.btnHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
      this.btnHelp.ContextMenuStrip = this.contextMenuHelp;
      this.btnHelp.FlatAppearance.BorderSize = 0;
      this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnHelp.Location = new System.Drawing.Point(834, 0);
      this.btnHelp.Name = "btnHelp";
      this.btnHelp.Size = new System.Drawing.Size(21, 21);
      this.btnHelp.TabIndex = 13;
      this.mToolTipSupport.SetToolTip(this.btnHelp, "Help");
      this.btnHelp.UseVisualStyleBackColor = true;
      this.btnHelp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnHelp_MouseUp);
      // 
      // contextMenuHelp
      // 
      this.contextMenuHelp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuHelpAbout,
            this.menuHelpLearnMore});
      this.contextMenuHelp.Name = "contextMenuHelp";
      this.contextMenuHelp.Size = new System.Drawing.Size(135, 48);
      // 
      // menuHelpAbout
      // 
      this.menuHelpAbout.Name = "menuHelpAbout";
      this.menuHelpAbout.ShortcutKeys = System.Windows.Forms.Keys.F1;
      this.menuHelpAbout.Size = new System.Drawing.Size(134, 22);
      this.menuHelpAbout.Text = "About";
      this.menuHelpAbout.Click += new System.EventHandler(this.menuHelpAbout_Click);
      // 
      // menuHelpLearnMore
      // 
      this.menuHelpLearnMore.Name = "menuHelpLearnMore";
      this.menuHelpLearnMore.Size = new System.Drawing.Size(134, 22);
      this.menuHelpLearnMore.Text = "Learn More";
      this.menuHelpLearnMore.Click += new System.EventHandler(this.menuHelpLearnMore_Click);
      // 
      // tabMSIX
      // 
      this.tabMSIX.Controls.Add(this.btnClearFilter);
      this.tabMSIX.Controls.Add(this.pictureBox1);
      this.tabMSIX.Controls.Add(this.txtFilter);
      this.tabMSIX.Controls.Add(this.panelFilter);
      this.tabMSIX.Controls.Add(this.listMSIXApps);
      this.tabMSIX.Location = new System.Drawing.Point(4, 26);
      this.tabMSIX.Name = "tabMSIX";
      this.tabMSIX.Padding = new System.Windows.Forms.Padding(3);
      this.tabMSIX.Size = new System.Drawing.Size(417, 264);
      this.tabMSIX.TabIndex = 0;
      this.tabMSIX.Text = "MSIX";
      this.tabMSIX.UseVisualStyleBackColor = true;
      // 
      // pictureBox1
      // 
      this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
      this.pictureBox1.Image = global::Hover.Properties.Resources.magnifier;
      this.pictureBox1.Location = new System.Drawing.Point(11, 243);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(16, 17);
      this.pictureBox1.TabIndex = 13;
      this.pictureBox1.TabStop = false;
      // 
      // panelFilter
      // 
      this.panelFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.panelFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panelFilter.Location = new System.Drawing.Point(3, 238);
      this.panelFilter.Name = "panelFilter";
      this.panelFilter.Size = new System.Drawing.Size(410, 23);
      this.panelFilter.TabIndex = 14;
      // 
      // listMSIXApps
      // 
      this.listMSIXApps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.listMSIXApps.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.listMSIXApps.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.listMSIXApps.FormattingEnabled = true;
      this.listMSIXApps.ItemHeight = 17;
      this.listMSIXApps.Location = new System.Drawing.Point(9, 6);
      this.listMSIXApps.Name = "listMSIXApps";
      this.listMSIXApps.Size = new System.Drawing.Size(404, 221);
      this.listMSIXApps.TabIndex = 0;
      this.listMSIXApps.SelectedIndexChanged += new System.EventHandler(this.listMSIXApps_SelectedIndexChanged);
      this.listMSIXApps.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listMSIXApps_KeyDown);
      this.listMSIXApps.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listMSIXApps_KeyPress);
      // 
      // tableLayoutTools
      // 
      this.tableLayoutTools.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tableLayoutTools.ColumnCount = 2;
      this.tableLayoutTools.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.84428F));
      this.tableLayoutTools.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.15572F));
      this.tableLayoutTools.Controls.Add(this.panelAppList, 0, 0);
      this.tableLayoutTools.Controls.Add(this.panelExe, 1, 0);
      this.tableLayoutTools.Location = new System.Drawing.Point(9, -1);
      this.tableLayoutTools.Name = "tableLayoutTools";
      this.tableLayoutTools.RowCount = 1;
      this.tableLayoutTools.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutTools.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 303F));
      this.tableLayoutTools.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 303F));
      this.tableLayoutTools.Size = new System.Drawing.Size(849, 303);
      this.tableLayoutTools.TabIndex = 12;
      // 
      // panelAppList
      // 
      this.panelAppList.Controls.Add(this.btnRefresh);
      this.panelAppList.Controls.Add(this.packageTab);
      this.panelAppList.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelAppList.Location = new System.Drawing.Point(3, 3);
      this.panelAppList.Name = "panelAppList";
      this.panelAppList.Size = new System.Drawing.Size(425, 297);
      this.panelAppList.TabIndex = 6;
      // 
      // packageTab
      // 
      this.packageTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.packageTab.Controls.Add(this.tabMSIX);
      this.packageTab.Controls.Add(this.tabAPPV);
      this.packageTab.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.packageTab.Location = new System.Drawing.Point(0, 3);
      this.packageTab.Name = "packageTab";
      this.packageTab.SelectedIndex = 0;
      this.packageTab.Size = new System.Drawing.Size(425, 294);
      this.packageTab.TabIndex = 2;
      this.packageTab.TabStop = false;
      this.packageTab.SelectedIndexChanged += new System.EventHandler(this.packageTab_SelectedIndexChanged);
      // 
      // tabAPPV
      // 
      this.tabAPPV.Controls.Add(this.listAppvApps);
      this.tabAPPV.Location = new System.Drawing.Point(4, 26);
      this.tabAPPV.Name = "tabAPPV";
      this.tabAPPV.Padding = new System.Windows.Forms.Padding(3);
      this.tabAPPV.Size = new System.Drawing.Size(417, 264);
      this.tabAPPV.TabIndex = 1;
      this.tabAPPV.Text = "App-V";
      this.tabAPPV.UseVisualStyleBackColor = true;
      // 
      // listAppvApps
      // 
      this.listAppvApps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
      this.listAppvApps.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.listAppvApps.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.listAppvApps.FormattingEnabled = true;
      this.listAppvApps.ItemHeight = 17;
      this.listAppvApps.Location = new System.Drawing.Point(9, 6);
      this.listAppvApps.Name = "listAppvApps";
      this.listAppvApps.Size = new System.Drawing.Size(402, 238);
      this.listAppvApps.TabIndex = 7;
      this.listAppvApps.SelectedIndexChanged += new System.EventHandler(this.listAppvApps_SelectedIndexChanged);
      // 
      // panelExe
      // 
      this.panelExe.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.panelExe.AutoSize = true;
      this.panelExe.Controls.Add(this.progressBar);
      this.panelExe.Controls.Add(this.lblProcess);
      this.panelExe.Controls.Add(this.listProcesses);
      this.panelExe.Location = new System.Drawing.Point(434, 3);
      this.panelExe.Name = "panelExe";
      this.panelExe.Size = new System.Drawing.Size(412, 297);
      this.panelExe.TabIndex = 13;
      // 
      // progressBar
      // 
      this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.progressBar.BackColor = System.Drawing.SystemColors.Control;
      this.progressBar.Location = new System.Drawing.Point(4, 273);
      this.progressBar.Margin = new System.Windows.Forms.Padding(4);
      this.progressBar.MarqueeAnimationSpeed = 10;
      this.progressBar.Name = "progressBar";
      this.progressBar.Size = new System.Drawing.Size(408, 23);
      this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
      this.progressBar.TabIndex = 10;
      // 
      // lblProcess
      // 
      this.lblProcess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lblProcess.AutoSize = true;
      this.lblProcess.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblProcess.Location = new System.Drawing.Point(2, 7);
      this.lblProcess.Name = "lblProcess";
      this.lblProcess.Size = new System.Drawing.Size(255, 17);
      this.lblProcess.TabIndex = 6;
      this.lblProcess.Text = "Executable to launch in package container:";
      // 
      // listProcesses
      // 
      this.listProcesses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.listProcesses.BackColor = System.Drawing.SystemColors.Window;
      this.listProcesses.Enabled = false;
      this.listProcesses.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.listProcesses.GridLines = true;
      this.listProcesses.LargeImageList = this.mImageList;
      this.listProcesses.Location = new System.Drawing.Point(4, 27);
      this.listProcesses.MultiSelect = false;
      this.listProcesses.Name = "listProcesses";
      this.listProcesses.ShowItemToolTips = true;
      this.listProcesses.Size = new System.Drawing.Size(408, 269);
      this.listProcesses.TabIndex = 1;
      this.listProcesses.TileSize = new System.Drawing.Size(300, 80);
      this.listProcesses.UseCompatibleStateImageBehavior = false;
      this.listProcesses.ItemActivate += new System.EventHandler(this.listProcesses_ItemActivate);
      this.listProcesses.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listProcesses_KeyDown);
      // 
      // imgAdvInstLogo
      // 
      this.imgAdvInstLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.imgAdvInstLogo.BackgroundImage = global::Hover.Properties.Resources.PoweredByAdvancedInstaller;
      this.imgAdvInstLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.imgAdvInstLogo.Cursor = System.Windows.Forms.Cursors.Hand;
      this.imgAdvInstLogo.Location = new System.Drawing.Point(743, 308);
      this.imgAdvInstLogo.Name = "imgAdvInstLogo";
      this.imgAdvInstLogo.Size = new System.Drawing.Size(115, 113);
      this.imgAdvInstLogo.TabIndex = 11;
      this.imgAdvInstLogo.TabStop = false;
      this.imgAdvInstLogo.Click += new System.EventHandler(this.imgAdvInstLogo_Click);
      // 
      // timerFilter
      // 
      this.timerFilter.Interval = 250;
      this.timerFilter.Tick += new System.EventHandler(this.timerFilter_Tick);
      // 
      // LauncherForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.Window;
      this.ClientSize = new System.Drawing.Size(867, 432);
      this.Controls.Add(this.btnHelp);
      this.Controls.Add(this.tableLayoutTools);
      this.Controls.Add(this.imgAdvInstLogo);
      this.Controls.Add(this.grpPackageInfo);
      this.DoubleBuffered = true;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MinimumSize = new System.Drawing.Size(883, 471);
      this.Name = "LauncherForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Hover by Advanced Installer";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LauncherForm_FormClosing);
      this.Load += new System.EventHandler(this.LauncherForm_Load);
      this.Leave += new System.EventHandler(this.LauncherForm_Leave);
      this.grpPackageInfo.ResumeLayout(false);
      this.grpPackageInfo.PerformLayout();
      this.contextMenuHelp.ResumeLayout(false);
      this.tabMSIX.ResumeLayout(false);
      this.tabMSIX.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.tableLayoutTools.ResumeLayout(false);
      this.tableLayoutTools.PerformLayout();
      this.panelAppList.ResumeLayout(false);
      this.packageTab.ResumeLayout(false);
      this.tabAPPV.ResumeLayout(false);
      this.panelExe.ResumeLayout(false);
      this.panelExe.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.imgAdvInstLogo)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.ImageList mImageList;
    private System.Windows.Forms.GroupBox grpPackageInfo;
    private System.Windows.Forms.TextBox editVerId;
    private System.Windows.Forms.TextBox editPkgId;
    private System.Windows.Forms.TextBox editPkgName;
    private System.Windows.Forms.Label lblVerId;
    private System.Windows.Forms.Label lblPkgId;
    private System.Windows.Forms.Label lblPkgName;
    private System.Windows.Forms.ToolTip mToolTipSupport;
    private System.Windows.Forms.OpenFileDialog mExePicker;
    private System.Windows.Forms.PictureBox imgAdvInstLogo;
    private System.Windows.Forms.TableLayoutPanel tableLayoutTools;
    private System.Windows.Forms.Panel panelExe;
    private System.Windows.Forms.Label lblProcess;
    private System.Windows.Forms.ListView listProcesses;
    private System.Windows.Forms.ProgressBar progressBar;
    private System.Windows.Forms.Panel panelAppList;
    private System.Windows.Forms.TabControl packageTab;
    private System.Windows.Forms.TabPage tabMSIX;
    private System.Windows.Forms.Button btnClearFilter;
    private System.Windows.Forms.ListBox listMSIXApps;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.TextBox txtFilter;
    private System.Windows.Forms.TabPage tabAPPV;
    private System.Windows.Forms.ListBox listAppvApps;
    private System.Windows.Forms.Button btnRefresh;
    private System.Windows.Forms.Panel panelFilter;
    private System.Windows.Forms.Button btnHelp;
    private System.Windows.Forms.ContextMenuStrip contextMenuHelp;
    private System.Windows.Forms.ToolStripMenuItem menuHelpAbout;
    private System.Windows.Forms.ToolStripMenuItem menuHelpLearnMore;
    private System.Windows.Forms.Timer timerFilter;
  }
}

