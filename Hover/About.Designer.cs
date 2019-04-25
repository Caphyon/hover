namespace Hover
{
  partial class About
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
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
      this.labelVersion = new System.Windows.Forms.Label();
      this.labelDescription = new System.Windows.Forms.Label();
      this.linkLabel1 = new System.Windows.Forms.LinkLabel();
      this.labelCopyright = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.linkLabel2 = new System.Windows.Forms.LinkLabel();
      this.okButton = new System.Windows.Forms.Button();
      this.pictureBox2 = new System.Windows.Forms.PictureBox();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // labelVersion
      // 
      this.labelVersion.AutoSize = true;
      this.labelVersion.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.labelVersion.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
      this.labelVersion.Location = new System.Drawing.Point(190, 83);
      this.labelVersion.Name = "labelVersion";
      this.labelVersion.Size = new System.Drawing.Size(265, 19);
      this.labelVersion.TabIndex = 2;
      this.labelVersion.Text = "Version X.Y. Populated from assembly info";
      this.labelVersion.MouseDown += new System.Windows.Forms.MouseEventHandler(this.About_MouseDown);
      this.labelVersion.MouseMove += new System.Windows.Forms.MouseEventHandler(this.About_MouseMove);
      // 
      // labelDescription
      // 
      this.labelDescription.AutoSize = true;
      this.labelDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.labelDescription.Location = new System.Drawing.Point(190, 116);
      this.labelDescription.Name = "labelDescription";
      this.labelDescription.Size = new System.Drawing.Size(321, 38);
      this.labelDescription.TabIndex = 3;
      this.labelDescription.Text = "Hover enables you to launch an EXE in the context \r\nof an MSIX or App-V package. " +
    "";
      this.labelDescription.MouseDown += new System.Windows.Forms.MouseEventHandler(this.About_MouseDown);
      this.labelDescription.MouseMove += new System.Windows.Forms.MouseEventHandler(this.About_MouseMove);
      // 
      // linkLabel1
      // 
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.linkLabel1.Location = new System.Drawing.Point(379, 135);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new System.Drawing.Size(137, 19);
      this.linkLabel1.TabIndex = 4;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "Click for more details";
      this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
      // 
      // labelCopyright
      // 
      this.labelCopyright.AutoSize = true;
      this.labelCopyright.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.labelCopyright.Location = new System.Drawing.Point(189, 245);
      this.labelCopyright.Name = "labelCopyright";
      this.labelCopyright.Size = new System.Drawing.Size(259, 15);
      this.labelCopyright.TabIndex = 5;
      this.labelCopyright.Text = "copyright notice. Populated from assembly info";
      this.labelCopyright.MouseDown += new System.Windows.Forms.MouseEventHandler(this.About_MouseDown);
      this.labelCopyright.MouseMove += new System.Windows.Forms.MouseEventHandler(this.About_MouseMove);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.label4.Location = new System.Drawing.Point(189, 264);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(137, 15);
      this.label4.TabIndex = 6;
      this.label4.Text = "Built by the team behind";
      this.label4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.About_MouseDown);
      this.label4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.About_MouseMove);
      // 
      // linkLabel2
      // 
      this.linkLabel2.AutoSize = true;
      this.linkLabel2.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.linkLabel2.Location = new System.Drawing.Point(322, 264);
      this.linkLabel2.Name = "linkLabel2";
      this.linkLabel2.Size = new System.Drawing.Size(104, 15);
      this.linkLabel2.TabIndex = 7;
      this.linkLabel2.TabStop = true;
      this.linkLabel2.Text = "Advanced Installer";
      this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
      // 
      // okButton
      // 
      this.okButton.Location = new System.Drawing.Point(404, 299);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(113, 36);
      this.okButton.TabIndex = 0;
      this.okButton.Text = "OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // pictureBox2
      // 
      this.pictureBox2.Image = global::Hover.Properties.Resources.logoHover;
      this.pictureBox2.Location = new System.Drawing.Point(193, 12);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new System.Drawing.Size(198, 50);
      this.pictureBox2.TabIndex = 1;
      this.pictureBox2.TabStop = false;
      this.pictureBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.About_MouseDown);
      this.pictureBox2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.About_MouseMove);
      // 
      // pictureBox1
      // 
      this.pictureBox1.Image = global::Hover.Properties.Resources.lighter_InstallerSidebar;
      this.pictureBox1.Location = new System.Drawing.Point(12, 12);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(165, 324);
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.About_MouseDown);
      this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.About_MouseMove);
      // 
      // About
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
      this.ClientSize = new System.Drawing.Size(529, 347);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.linkLabel2);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.labelCopyright);
      this.Controls.Add(this.linkLabel1);
      this.Controls.Add(this.labelDescription);
      this.Controls.Add(this.labelVersion);
      this.Controls.Add(this.pictureBox2);
      this.Controls.Add(this.pictureBox1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "About";
      this.Padding = new System.Windows.Forms.Padding(9);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "About";
      this.Paint += new System.Windows.Forms.PaintEventHandler(this.About_Paint);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.About_KeyDown);
      this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.About_MouseDown);
      this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.About_MouseMove);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.PictureBox pictureBox2;
    private System.Windows.Forms.Label labelVersion;
    private System.Windows.Forms.Label labelDescription;
    private System.Windows.Forms.LinkLabel linkLabel1;
    private System.Windows.Forms.Label labelCopyright;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.LinkLabel linkLabel2;
    private System.Windows.Forms.Button okButton;
  }
}
