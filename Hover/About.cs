using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Hover
{
  partial class About : Form
  {
    private Point moveStartLocation = new Point();

    public About()
    {
      InitializeComponent();
      this.Text = String.Format("About {0}", AssemblyTitle);

      this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
      this.labelCopyright.Text = AssemblyCopyright;
    }

    #region Assembly Attribute Accessors

    public string AssemblyTitle
    {
      get
      {
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
        if (attributes.Length > 0)
        {
          AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
          if (titleAttribute.Title != "")
          {
            return titleAttribute.Title;
          }
        }
        return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
      }
    }

    public string AssemblyVersion
    {
      get
      {
        return Assembly.GetExecutingAssembly().GetName().Version.ToString(2);
      }
    }

    public string AssemblyCopyright
    {
      get
      {
        object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
        if (attributes.Length == 0)
        {
          return "";
        }
        return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
      }
    }

    #endregion

    private void okButton_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void About_MouseDown(object sender, MouseEventArgs e)
    {
      moveStartLocation = e.Location;
    }

    private void About_MouseMove(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        Left += e.X - moveStartLocation.X;
        Top += e.Y - moveStartLocation.Y;
      }
    }

    private void About_Paint(object sender, PaintEventArgs e)
    {
      Graphics g = e.Graphics;
      g.DrawRectangle(new Pen(Color.FromArgb(24, 131, 215), 1), 0, 0, this.Width - 1, this.Height - 1);
    }

    private void About_KeyDown(object sender, KeyEventArgs e)
    {

      if (e.KeyCode == Keys.Escape)
        Close();
    }

    private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("https://www.advancedinstaller.com");
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("https://www.advancedinstaller.com/hover");
    }
  }
}
