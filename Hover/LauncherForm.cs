using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Management.Automation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;

namespace Hover
{
  public partial class LauncherForm : Form
  {
    const string sIniFile                  = "Hover.ini";
    const string sInterrogateScript        = "Import-Module AppVClient; Get-AppvClientPackage -all";
    const string sInterrogateScriptMSIX    = "Get-AppXPackage";
    const string sLaunchExeScript          = "$pkg = Get-AppvClientPackage -PackageId {0} -VersionId {1}; Start-AppvVirtualProcess -AppvClientObject $pkg \"{2}\"";
    const string sLaunchExeScriptMSIX      = "Invoke-CommandInDesktopPackage -PackageFamilyName \"{0}\" -appid \"{1}\" -command \"{2}\" -args \"{3}\" -preventBreakaway";
    const string sInterrogateMSIXAppID     = "(Get-AppxPackage '{0}' | Get-AppxPackageManifest).package.applications.application.id";
    const string sPickNewTool              = "<Add>";
    const int kTabIndexMSIX                = 0;
    const int kTabIndexAppV                = 1;

    PowerShell mPowershellWorker           = PowerShell.Create();
    BindingList<AppvPackageInfo> mAppvApps = new BindingList<AppvPackageInfo>();
    BindingList<MSIXPackageInfo> mMSIXApps = new BindingList<MSIXPackageInfo>();
    Dictionary<string, ExternalExeInfo> mTools = new Dictionary<string, ExternalExeInfo>();

    delegate void UpdateAppVListControlDelegate(AppvPackageInfo aAppInfo);
    delegate void UpdateMSIXListControlDelegate(MSIXPackageInfo aAppInfo);
    delegate void UpdateControlDelegate();

    bool refreshInProgress = false;

    //-------------------------------------------------------------------------

    /// <summary>
    /// Constructor
    /// </summary>
    public LauncherForm()
    {
      InitializeComponent();
    }

    private void PopulateMSIX()
    {
      if (System.Environment.OSVersion.Version.Major < 6 || System.Environment.OSVersion.Version.Minor < 2)
      {
        return;
      }


      refreshInProgress = true;
      var pkgMSIX = ScanMSIXSystem();

      string filter = txtFilter.Text.Trim().ToLower();

      mMSIXApps.Clear();

      // Now that we have the MSIX information, we must inform the UI (on its thread)
      foreach (MSIXPackageInfo v in pkgMSIX)
      {
        bool keep = filter.Length == 0;

        if (!keep && v.Publisher.ToLower().Contains(filter))
          keep = true;
        if (!keep && v.PackageFamilyName.ToLower().Contains(filter))
          keep = true;
        if (!keep && v.Name.ToLower().Contains(filter))
          keep = true;
        if (!keep && v.FullName.ToLower().Contains(filter))
          keep = true;

        if (!keep)
          continue;

        listMSIXApps.Invoke(new UpdateMSIXListControlDelegate(UpdateListMSIXApps), v);
      }

      refreshInProgress = false;
      listMSIXApps.Invoke(new UpdateControlDelegate(() => { listMSIXApps_SelectedIndexChanged(listMSIXApps, new EventArgs()); }));
    }

    private void PopulateAppV()
    {
      mAppvApps.Clear();

      // Scan system for AppV Packages
      var pkgAppV = ScanAppVSystem();

      // Now that we have the AppV information, we must inform the UI (on its thread)
      foreach (var v in pkgAppV)
      {
        listAppvApps.Invoke(new UpdateAppVListControlDelegate(UpdateListAppvApps), v);
      }
    }

    /// <summary>
    /// Initializes form. This should be called asynchronously, as it takes a while.
    /// </summary>
    private void InitializeMSIXAppV()
    {
      // load any data from INI
      LoadData();

      // detect tools and their paths
      LoadExeToolList();

      PopulateMSIX();

      PopulateAppV();

      progressBar.Invoke(new UpdateControlDelegate(() =>
      {
        if (packageTab.SelectedIndex == kTabIndexMSIX && listMSIXApps.Items.Count > 0)
          listMSIXApps.SetSelected(0, true);

        if (packageTab.SelectedIndex == kTabIndexAppV && listAppvApps.Items.Count > 0)
            listAppvApps.SetSelected(0, true);

        StartStopProgressAnimation(false);
      }));
    }

    private void LoadPredefinedExes()
    {
      List<ExternalExeInfo> predefinedTools = new List<ExternalExeInfo>
      {
         new ExternalExeInfo("Cmd.exe", @"/K """"c:&cd """"{0}""""""""", true)
       , new ExternalExeInfo("Explorer.exe", @"{0}", true)
       , new ExternalExeInfo(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), 
                                                    @"WindowsPowerShell\v1.0\PowerShell.exe"),
                                                    @"-noexit -command """"cd '{0}'""""", true)
       , new ExternalExeInfo("Regedit.exe", "", true)
      };

      foreach (var tool in predefinedTools)
      {
        if (!mTools.ContainsKey(tool.Path))
          AddToolToUI(tool);
      }
    }

    private void LoadExeToolList()
    {
      listProcesses.Invoke(new UpdateControlDelegate(() =>
      {
        listProcesses.Clear();
        listProcesses.Refresh();
      }));

      LoadPredefinedExes(); // load cmd, regedit, etc

      foreach (var exeTool in mTools.Values)
      {
        if (!string.IsNullOrEmpty(exeTool.Path) && !exeTool.IsPredefined)
          AddToolToUI(exeTool);
      }

      mTools[sPickNewTool] = new ExternalExeInfo("", "", true);
      listProcesses.Invoke(new UpdateControlDelegate(() =>
      {
        ListViewItem newItem = new ListViewItem(sPickNewTool, 0, null);
        newItem.ToolTipText = "Add new EXE to this list";
        newItem.Name = sPickNewTool;
        listProcesses.Items.Add(newItem);
      }));
    }

    private void AddToolToUI(ExternalExeInfo aToolInfo)
    {
      if (!mTools.ContainsKey(aToolInfo.Path))
        mTools[aToolInfo.Path] = aToolInfo;
      
      listProcesses.Invoke(new UpdateControlDelegate( ()=>
      {
        mImageList.Images.Add(aToolInfo.Icon.ToBitmap());

        string displayName = System.IO.Path.GetFileNameWithoutExtension(aToolInfo.Path);
        ListViewItem newItem = new ListViewItem(displayName, mImageList.Images.Count - 1, null);
        newItem.Name = aToolInfo.Path;
        newItem.ToolTipText = aToolInfo.Path;
        listProcesses.Items.Add(newItem);
      }));
    }

    /// <summary>
    /// Enables/Disables UI elements that should be disabled when no App-V
    /// package is selected.
    /// </summary>
    /// <param name="aEnable">What to do</param>
    private void EnableDisableUI(bool aEnable)
    {
      grpPackageInfo.Enabled = aEnable;
      listProcesses.Enabled  = aEnable;
      lblProcess.Enabled     = aEnable;
    }

    private void UpdateListAppvApps(AppvPackageInfo aAppInfo)
    {
      mAppvApps.Add(aAppInfo);
    }

    private void UpdateListMSIXApps(MSIXPackageInfo aAppInfo)
    {
      mMSIXApps.Add(aAppInfo);
    }

    private void StartStopProgressAnimation(bool aStart)
    {
      progressBar.Visible = aStart;
    }
    private void listMSIXApps_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (refreshInProgress)
        return;

      var selItem = ((ListBox)sender).SelectedItem;
      if (selItem == null)
      {
        EnableDisableUI(false);
        return;
      }

      EnableDisableUI(true);

      var pkgInfo = (MSIXPackageInfo)(selItem);

      if (string.IsNullOrEmpty(pkgInfo.AppID))
      {
        // Detect what the appid is. We can't afford do this 
        // when applications starts for all apps because it would be too slow.

        ResetPowerShellEngine();
        mPowershellWorker.AddScript(String.Format(sInterrogateMSIXAppID, pkgInfo.Name));

        var appIdInvoke = mPowershellWorker.Invoke();
        if (mPowershellWorker.HadErrors)
        {
          // There are some packages (e.g. Windows.MiracastView) which fail the AppID interrogation. 
          pkgInfo.AppID = "App";
        }
        else
        {
          pkgInfo.AppID = (appIdInvoke.Count > 0 && appIdInvoke[0] != null) ? appIdInvoke[0].ToString() : "App";
        }
      }

      editPkgName.Text = pkgInfo.AppID;
      editPkgId.Text = pkgInfo.PackageFamilyName;
      editVerId.Text = pkgInfo.Version;
    }

    /// <summary>
    /// Handler for when user changes the AppV package selection.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void listAppvApps_SelectedIndexChanged(object sender, EventArgs e)
    {
      var selItem = ((ListBox)sender).SelectedItem;
      if (selItem == null)
      {
        EnableDisableUI(false);
        return;
      }

      EnableDisableUI(true);

      var pkgInfo = (AppvPackageInfo)( selItem );
      editPkgName.Text = pkgInfo.Name;
      editPkgId.Text   = pkgInfo.PackageId.ToString();
      editVerId.Text   = pkgInfo.VersionId.ToString();
    }

    private void UpdatePackageInfoUI(bool isMSIX)
    {
      if (isMSIX)
      {
        lblPkgName.Text = "AppID:";
        lblPkgId.Text   = "Family Name:";
        lblVerId.Text   = "Version:";
      }
      else
      {
        lblPkgName.Text = "Name:";
        lblPkgId.Text   = "Id:";
        lblVerId.Text   = "Version Id:";
      }
      editPkgId.Text = "";
      editPkgName.Text = "";
      editVerId.Text = "";
    }

    /// <summary>
    /// Clears the PowerShell execution pipeline of the worker object.
    /// If this cleanup is not done, when running new script all the older ones 
    /// will be also executed.
    /// </summary>
    private void ResetPowerShellEngine()
    {
      mPowershellWorker.Streams.ClearStreams();
      mPowershellWorker.Commands.Clear();
    }

    /// <summary>
    /// Scans the machine for Deployed AppV Packages (via PowerShell).
    /// </summary>
    /// <returns>Collection containing AppV-Packages Information</returns>
    private IEnumerable<AppvPackageInfo> ScanAppVSystem()
    {
      ResetPowerShellEngine();
      
      mPowershellWorker.AddScript(sInterrogateScript);
      var resObjects = mPowershellWorker.Invoke();

      if (mPowershellWorker.HadErrors && mPowershellWorker.Streams.Error[0].ErrorDetails != null)
      {
        string error = mPowershellWorker.Streams.Error[0].ErrorDetails.Message;
        string stack = mPowershellWorker.Streams.Error[0].ScriptStackTrace.ToString();
        string invocation = mPowershellWorker.Streams.Error[0].InvocationInfo.ToString() ;

        MessageBox.Show(error + "\r\n" + stack + "\r\n" + invocation, 
                        "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        Application.Exit();
      }

      foreach (var obj in resObjects)
      {
        if ((bool)(obj.Members["IsPublishedToUser"].Value))
        {
          yield return new AppvPackageInfo((string)obj.Members["Name"].Value,
                                           (Guid)obj.Members["PackageId"].Value,
                                           (Guid)obj.Members["VersionId"].Value);
        }
      }
    }

    /// <summary>
    /// Scans the machine for Deployed MSIX Packages (via PowerShell).
    /// </summary>
    /// <returns>Collection containing Packages Information</returns>
    private IEnumerable<MSIXPackageInfo> ScanMSIXSystem()
    {
      ResetPowerShellEngine();

      mPowershellWorker.AddScript(sInterrogateScriptMSIX);
      var resObjects = mPowershellWorker.Invoke();

      if (mPowershellWorker.HadErrors && mPowershellWorker.Streams.Error[0].ErrorDetails != null)
      {
        string error = mPowershellWorker.Streams.Error[0].ErrorDetails.Message;
        string stack = mPowershellWorker.Streams.Error[0].ScriptStackTrace.ToString();
        string invocation = mPowershellWorker.Streams.Error[0].InvocationInfo.ToString();

        MessageBox.Show(error + "\r\n" + stack + "\r\n" + invocation,
                        "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        Application.Exit();
      }

      foreach (var obj in resObjects)
      {
        var package = (Microsoft.Windows.Appx.PackageManager.Commands.AppxPackage)obj.BaseObject;

        // MiracastView is causing a lot of troubles at the moment. Ignore it until a better solution comes about.
        if (package.Name != "Windows.MiracastView")
        {
          string installLocation = "";
          try
          {
            // there are some packages which throw exception when accessing this field. 
            installLocation = package.InstallLocation;
          }
          catch(Exception)
          {
          }

          yield return new MSIXPackageInfo(package.Name, package.Publisher, package.PackageFullName, package.PackageFamilyName, package.Version, installLocation);
        }
      }
    }

    private void LaunchTool(AppvPackageInfo aAppvPkg, ExternalExeInfo aToolInfo)
    {
      ResetPowerShellEngine();

      string script = String.Format(sLaunchExeScript, aAppvPkg.PackageId, 
                                    aAppvPkg.VersionId, aToolInfo.Path);
      mPowershellWorker.AddScript(script);

      StartStopProgressAnimation(true);

      BackgroundWorker bw = new BackgroundWorker();
      bw.DoWork += (s, e) => 
      {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        mPowershellWorker.Invoke();
        sw.Stop();

        if (sw.ElapsedMilliseconds < 1000)
          System.Threading.Thread.Sleep(1000 - (int)sw.ElapsedMilliseconds);

        progressBar.Invoke(new UpdateControlDelegate(() =>
        {
          StartStopProgressAnimation(false);
        }));
      };
      bw.RunWorkerAsync();
    }

    public bool IsElevated()
    {
      try
      {
        WindowsIdentity user = WindowsIdentity.GetCurrent();
        WindowsPrincipal principal = new WindowsPrincipal(user);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
      }
      catch (Exception)
      {
        return false;
      }
    }


    private void RestartAsAdmin()
    {
      ProcessStartInfo proc = new ProcessStartInfo();
      proc.UseShellExecute = true;
      proc.WorkingDirectory = Environment.CurrentDirectory;
      proc.FileName = Application.ExecutablePath;
      proc.Verb = "runas";

      try
      {
        // spawn new elevated instance
        Process.Start(proc);
      }
      catch
      {
        // elevation prompt can be discarded, don't kill the app in this case
        return;
      }

      // kill current instance
      Application.Exit();
    }

    private void LaunchTool(MSIXPackageInfo aMSIXPkg, ExternalExeInfo aToolInfo)
    {
      StartStopProgressAnimation(true);
      if (aToolInfo.ShortName == "Explorer")
      {
        MessageBox.Show(this, "Due to a limitation in Windows 10, Explorer.exe will not launch in the context of the MSIX package container.\n\nThis will get fixed in a future Windows update.", "Hover limitation", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }

      BackgroundWorker bw = new BackgroundWorker();
      bw.DoWork += (s, e) =>
      {
        Stopwatch sw = new Stopwatch();
        sw.Start();
                     
        ResetPowerShellEngine();
        
        string args = "";
        if (aToolInfo.IsPredefined && aToolInfo.ShortName != "Regedit")
          args = aMSIXPkg.InstallLocation;

        string script = String.Format(sLaunchExeScriptMSIX, aMSIXPkg.PackageFamilyName,
                                      aMSIXPkg.AppID, aToolInfo.Path, string.Format(aToolInfo.ArgFormatStr, args));
        mPowershellWorker.AddScript(script);

        mPowershellWorker.Invoke();
        if (mPowershellWorker.HadErrors)
        {
          int hres = mPowershellWorker.Streams.Error[0].Exception.HResult;
          if (hres == -2147023673)
          {
            // -2147023673(0x800704C7) is the HRESULT for ERROR_CANCELLED. 
            // Give a more meaningful message instead.

            if (!IsElevated())
            {
              if (DialogResult.Yes == MessageBox.Show(this, aToolInfo.Path + " has closed prematurely.\r\n\r\nDo you want to elevate and retry as an Administrator?", "Can't launch executable", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                RestartAsAdmin();
            }
            else
            {
              MessageBox.Show(this, aToolInfo.Path + " has closed prematurely.", "Can't launch executable", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
          }
          else if (hres == -2147024891)
          {
            // 0x80070005 E_ACCESSDENIED
            MessageBox.Show(this, "Cannot execute command.\r\n\r\nPlease enable 'Developer mode' in \nWindows Settings > Update & Security > For developers.", "Access denied", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Process.Start("ms-settings:");
          }
          else
          {
            MessageBox.Show(this, mPowershellWorker.Streams.Error[0].Exception.Message, "Can't launch executable", MessageBoxButtons.OK, MessageBoxIcon.Error);
          }
        }

        sw.Stop();

        if (sw.ElapsedMilliseconds < 1000)
          System.Threading.Thread.Sleep(1000 - (int)sw.ElapsedMilliseconds);

        progressBar.Invoke(new UpdateControlDelegate(() =>
        {
          StartStopProgressAnimation(false);
        }));
      };
      bw.RunWorkerAsync();
    }

    private void listProcesses_ItemActivate(object sender, EventArgs e)
    {
      var selectedItems = listProcesses.SelectedItems;
      if (selectedItems.Count == 0)
        return;
      
      string name = selectedItems[0].Name;
      if (name == sPickNewTool)
      {
        mExePicker.Title = "Pick an executable file";
        mExePicker.Filter = "EXE Files|*.exe";

        if (mExePicker.ShowDialog(this) == DialogResult.OK)
        {
          string selectedFile = mExePicker.FileName;
          if (mTools.ContainsKey(selectedFile))
            return;

          SaveData();
          LoadData();
          mTools[selectedFile] = new ExternalExeInfo(selectedFile, "", false);

          LoadExeToolList();

          listProcesses.Items[listProcesses.Items.Count - 2].Selected = true;
        }
        return;
      }

      ExternalExeInfo toolInfo = mTools[name];

      if (packageTab.SelectedIndex == kTabIndexMSIX)
      {
        // MSIX
        if (listMSIXApps.SelectedItem != null)
        {
          var msixPkgInfo = (MSIXPackageInfo)(listMSIXApps.SelectedItem);
          LaunchTool(msixPkgInfo, toolInfo);
        }
      }
      else
      {
        // AppV
        if (listAppvApps.SelectedItem != null)
        {
          var appvPkgInfo = (AppvPackageInfo)(listAppvApps.SelectedItem);
          LaunchTool(appvPkgInfo, toolInfo);
        }
      }
    }

    private void SaveData()
    {
      var toSave = from c in mTools.Values where !c.IsPredefined select c;
      if (toSave.Count() == 0)
        return; // nothing to save

      StreamWriter sw = new StreamWriter(sIniFile, false);
      foreach (var tool in toSave)
        sw.WriteLine(tool.Path);

      sw.Close();
    }

    private void LoadData()
    {
      if ( ! File.Exists(sIniFile))
        return;

      mTools.Clear();

      StreamReader sr = new StreamReader(sIniFile);
      while ( ! sr.EndOfStream)
      {
        string currentPath = sr.ReadLine();
        mTools[currentPath] = new ExternalExeInfo(currentPath, "", false);
      }
      sr.Close();
    }

    private void LauncherForm_Leave(object sender, EventArgs e)
    {
      SaveData(); // save to INI
    }

    private void LauncherForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      SaveData(); // save to INI
    }

    private void listProcesses_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.F5 && packageTab.SelectedIndex == kTabIndexMSIX)
      {
        RefreshDataMSIX();
      }

      if (e.KeyCode != Keys.Delete)
        return;

      var selected = listProcesses.SelectedItems;
      if (selected.Count == 0)
        return;
      
      string name = selected[0].Name;
      if (mTools.ContainsKey(name) == false)
        return;

      if (mTools[name].IsPredefined)
        return;

      if (MessageBox.Show(string.Format("Are you sure you want to remove {0}?", name),
                          "Confirm app entry removal", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) != DialogResult.Yes)
                     
      {
        return;
      }

      int selectedIndex = selected[0].Index;

      mTools.Remove(name);

      SaveData();
      LoadData();
      LoadExeToolList();

      listProcesses.Items[selectedIndex].Selected = true;
    }

    private void packageTab_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (packageTab.SelectedIndex == kTabIndexMSIX)
      {
        // MSIX
        UpdatePackageInfoUI(true);

        if (listMSIXApps.Items.Count > 0)
          listMSIXApps.SetSelected(0, true);

        EnableDisableUI(listMSIXApps.SelectedItem != null);
      }
      else
      {
        // AppV
        UpdatePackageInfoUI(false);

        if (listAppvApps.Items.Count > 0)
          listAppvApps.SetSelected(0, true);

        EnableDisableUI(listAppvApps.SelectedItem != null);
      }
    }

    private void LauncherForm_Load(object sender, EventArgs e)
    {
      if (IsElevated())
      {
        this.Text = "[Administrator] " + this.Text;
      }

      StartStopProgressAnimation(true);

      listAppvApps.DataSource = mAppvApps;
      listMSIXApps.DataSource = mMSIXApps;

      BackgroundWorker bw = new BackgroundWorker();
      bw.DoWork += (s, ev) => { InitializeMSIXAppV(); };
      bw.RunWorkerAsync();
      
    }

    private void listMSIXApps_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.F5)
      {
        RefreshDataMSIX();
      }
    }

    void RefreshDataMSIX()
    {
      PopulateMSIX();

      listMSIXApps.Focus();
      if (listMSIXApps.Items.Count > 0)
        listMSIXApps.SetSelected(0, true);
    }

    void RefreshDataAppV()
    {
      PopulateAppV();

      listAppvApps.Focus();
      if (listAppvApps.Items.Count > 0)
        listAppvApps.SetSelected(0, true);
    }

    private void textBox1_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        e.SuppressKeyPress = true;
      }
    }

    private void txtFilter_TextChanged(object sender, EventArgs e)
    {
      if (timerFilter.Enabled)
        timerFilter.Enabled = false;

      timerFilter.Enabled = true;
    }

    private void btnClearFilter_Click(object sender, EventArgs e)
    {
      txtFilter.Text = "";
    }

    private void btnRefresh_Click(object sender, EventArgs e)
    {
      if (packageTab.SelectedIndex == kTabIndexMSIX)
        RefreshDataMSIX();
      if (packageTab.SelectedIndex == kTabIndexAppV)
        RefreshDataAppV();
    }

    private void imgAdvInstLogo_Click(object sender, EventArgs e)
    {
      Process.Start("https://www.advancedinstaller.com/msix-packaging.html");
    }

    private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == 13) // enter
      {
        e.Handled = true;
      }

      if (e.KeyChar == 27) // escape
      {
        e.Handled = true;
        txtFilter.Text = "";
      }
    }

    private void listMSIXApps_KeyPress(object sender, KeyPressEventArgs e)
    {
      if ( (e.KeyChar >= 'a' && e.KeyChar <= 'z') ||
           (e.KeyChar >= 'A' && e.KeyChar <= 'Z') )
      {
        txtFilter.Text += e.KeyChar;
        e.Handled = true;
        txtFilter.Focus();
        txtFilter.Select(txtFilter.Text.Length, 0);
      }
    }

    private void txtFilter_KeyDown(object sender, KeyEventArgs e)
    {
      int selIndex = listMSIXApps.SelectedIndex;

      if (e.KeyCode == Keys.Up)
      {
        e.Handled = true;
        if (selIndex > 0)
        {
          listMSIXApps.SetSelected(selIndex - 1, true);
        }
      }

      if (e.KeyCode == Keys.Down)
      {
        e.Handled = true;
        if (selIndex < listMSIXApps.Items.Count - 1)
        {
          listMSIXApps.SetSelected(selIndex + 1, true);
        }
      }

      if (e.KeyCode == Keys.F5)
      {
        RefreshDataMSIX();
      }      
    }

    private void menuHelpAbout_Click(object sender, EventArgs e)
    {
      About about = new About();
      about.ShowDialog();
    }

    private void menuHelpLearnMore_Click(object sender, EventArgs e)
    {
      Process.Start("https://www.advancedinstaller.com/hover");
    }

    private void btnHelp_MouseUp(object sender, MouseEventArgs e)
    {
      if (e.Button is MouseButtons.Left)
      {
        btnHelp.ContextMenuStrip.Show(btnHelp, new System.Drawing.Point(e.X, e.Y));
      }
    }

    private void timerFilter_Tick(object sender, EventArgs e)
    {
      timerFilter.Enabled = false;

      string filter = txtFilter.Text.Trim();
      btnClearFilter.Visible = filter.Length > 0;

      PopulateMSIX();

      if (listMSIXApps.Items.Count > 0)
        listMSIXApps.SetSelected(0, true);
    }
  }
}
