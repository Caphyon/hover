using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Windowing;
using Windows.Storage.Pickers;
using Windows.Foundation;
using WinRT.Interop;

namespace ContainerHover;

public sealed partial class MainWindow : Window
{
    private readonly PowerShellService _powerShell = new();
    private readonly List<PackageInfo> _allMsixPackages = [];
    private readonly List<PackageInfo> _allAppVPackages = [];
    private readonly ObservableCollection<PackageInfo> _visiblePackages = [];
    private readonly ObservableCollection<LaunchTool> _tools = [];
    private readonly string _settingsPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ContainerHover", "tools.json");
    private bool _loading;
    private bool _splashDismissed;

    public MainWindow()
    {
        InitializeComponent();

        var windowHandle = WindowNative.GetWindowHandle(this);
        var windowId = Win32Interop.GetWindowIdFromWindow(windowHandle);
        if (AppWindow.GetFromWindowId(windowId).Presenter is OverlappedPresenter presenter)
        {
            presenter.PreferredMinimumWidth = 1600;
            presenter.PreferredMinimumHeight = 900;
        }

        SystemBackdrop = new DesktopAcrylicBackdrop();
        ExtendsContentIntoTitleBar = true;
        SetTitleBar(null);
        PackagesList.ItemsSource = _visiblePackages;
        ToolsList.ItemsSource = _tools;
        LoadTools();
        // Dismiss the splash overlay when the window is first activated.
        // WinUI Window does not expose a reliable Loaded event in some hosts.
        Activated += async (_, _) =>
        {
            if (!_splashDismissed)
            {
                _splashDismissed = true;
                await Task.Delay(1400);
                await AnimateSplashLogoAsync();
            }

            if (!_loading && _allMsixPackages.Count == 0 && _allAppVPackages.Count == 0)
                await RefreshPackagesAsync();
        };
    }

    private async Task AnimateSplashLogoAsync()
    {
        var sourceCenter = SplashLogo.TransformToVisual(RootGrid).TransformPoint(
            new Point(SplashLogo.ActualWidth / 2, SplashLogo.ActualHeight / 2));
        var targetCenter = HeaderLogo.TransformToVisual(RootGrid).TransformPoint(
            new Point(HeaderLogo.ActualWidth / 2, HeaderLogo.ActualHeight / 2));
        var transform = (CompositeTransform)SplashLogo.RenderTransform;
        var duration = new Duration(TimeSpan.FromMilliseconds(500));
        var easing = new CubicEase { EasingMode = EasingMode.EaseInOut };
        var storyboard = new Storyboard();

        AddSplashAnimation(storyboard, "(UIElement.RenderTransform).(CompositeTransform.ScaleX)",
            HeaderLogo.ActualWidth / SplashLogo.ActualWidth, duration, easing);
        AddSplashAnimation(storyboard, "(UIElement.RenderTransform).(CompositeTransform.ScaleY)",
            HeaderLogo.ActualHeight / SplashLogo.ActualHeight, duration, easing);
        AddSplashAnimation(storyboard, "(UIElement.RenderTransform).(CompositeTransform.TranslateX)",
            targetCenter.X - sourceCenter.X, duration, easing);
        AddSplashAnimation(storyboard, "(UIElement.RenderTransform).(CompositeTransform.TranslateY)",
            targetCenter.Y - sourceCenter.Y, duration, easing);

        var completed = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        storyboard.Completed += (_, _) => completed.TrySetResult(true);
        storyboard.Begin();
        await completed.Task;

        transform.ScaleX = HeaderLogo.ActualWidth / SplashLogo.ActualWidth;
        transform.ScaleY = HeaderLogo.ActualHeight / SplashLogo.ActualHeight;
        transform.TranslateX = targetCenter.X - sourceCenter.X;
        transform.TranslateY = targetCenter.Y - sourceCenter.Y;
        SplashOverlay.Visibility = Visibility.Collapsed;
        HeaderLogo.Opacity = 1;
    }

    private void AddSplashAnimation(Storyboard storyboard, string property, double to,
        Duration duration, EasingFunctionBase easing)
    {
        var animation = new DoubleAnimation { To = to, Duration = duration, EasingFunction = easing };
        Storyboard.SetTarget(animation, SplashLogo);
        Storyboard.SetTargetProperty(animation, property);
        storyboard.Children.Add(animation);
    }

    private async Task RefreshPackagesAsync()
    {
        if (_loading) return;
        _loading = true;
        PackageProgress.IsActive = true;
        StatusBar.IsOpen = false;
        try
        {
            var msixTask = _powerShell.GetMsixPackagesAsync();
            var appVTask = _powerShell.GetAppVPackagesAsync();

            // App-V is an optional Windows feature. Do not make its absence block
            // the primary MSIX workflow.
            var msixPackages = await msixTask;
            _allMsixPackages.Clear();
            _allMsixPackages.AddRange(msixPackages);

            try
            {
                var appVPackages = await appVTask;
                _allAppVPackages.Clear();
                _allAppVPackages.AddRange(appVPackages);
            }
            catch (Exception ex)
            {
                _allAppVPackages.Clear();
                ShowStatus("App-V packages unavailable", CleanPowerShellError(ex.Message), InfoBarSeverity.Warning);
            }

            ApplyPackageFilter();
        }
        catch (Exception ex)
        {
            ShowStatus("Couldn’t refresh packages", CleanPowerShellError(ex.Message), InfoBarSeverity.Error);
            ApplyPackageFilter();
        }
        finally
        {
            _loading = false;
            PackageProgress.IsActive = false;
        }
    }

    private void ApplyPackageFilter()
    {
        var source = PackagePivot.SelectedIndex == 1 ? _allAppVPackages : _allMsixPackages;
        var filter = SearchBox.Text.Trim();
        var filtered = string.IsNullOrWhiteSpace(filter) ? source : source.Where(p =>
            p.Name.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
            p.Publisher.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
            p.FullName.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
            p.FamilyName.Contains(filter, StringComparison.OrdinalIgnoreCase));
        _visiblePackages.Clear();
        foreach (var package in filtered) _visiblePackages.Add(package);
        PackagesList.SelectedIndex = _visiblePackages.Count > 0 ? 0 : -1;
    }

    private async Task SelectPackageAsync(PackageInfo? package)
    {
        if (package is null)
        {
            PackageDetails.Visibility = Visibility.Collapsed;
            NoPackageText.Visibility = Visibility.Visible;
            return;
        }

        PackageDetails.Visibility = Visibility.Visible;
        NoPackageText.Visibility = Visibility.Collapsed;
        if (package.Kind == PackageKind.Msix)
        {
            IdentityLabel.Text = "Package family name";
            VersionLabel.Text = "Version";
            IdentityText.Text = package.FamilyName;
            VersionText.Text = package.Version;
            AppIdText.Text = "Loading…";
            try
            {
                if (string.IsNullOrWhiteSpace(package.AppId))
                    package.AppId = await _powerShell.GetMsixAppIdAsync(package.Name);
                if (ReferenceEquals(package, PackagesList.SelectedItem)) AppIdText.Text = package.AppId;
            }
            catch
            {
                package.AppId = "App";
                if (ReferenceEquals(package, PackagesList.SelectedItem)) AppIdText.Text = package.AppId;
            }
        }
        else
        {
            IdentityLabel.Text = "Package ID";
            VersionLabel.Text = "Version ID";
            AppIdText.Text = package.Name;
            IdentityText.Text = package.PackageId;
            VersionText.Text = package.VersionId;
        }
    }

    private void LoadTools()
    {
        _tools.Clear();
        _tools.Add(new LaunchTool { Name = "Command Prompt", Path = "cmd.exe", ArgumentsTemplate = "/K \"\"c:&cd \"\"{0}\"\"\"\"", IsBuiltIn = true, Glyph = "\uE756" });
        _tools.Add(new LaunchTool { Name = "File Explorer", Path = "explorer.exe", ArgumentsTemplate = "{0}", IsBuiltIn = true, Glyph = "\uE8B7" });
        _tools.Add(new LaunchTool { Name = "PowerShell", Path = Path.Combine(Environment.SystemDirectory, "WindowsPowerShell\\v1.0\\powershell.exe"), ArgumentsTemplate = "-noexit -command \"cd '{0}'\"", IsBuiltIn = true, Glyph = "\uE756" });
        _tools.Add(new LaunchTool { Name = "Registry Editor", Path = "regedit.exe", ArgumentsTemplate = "", IsBuiltIn = true, Glyph = "\uE943" });

        try
        {
            if (!File.Exists(_settingsPath)) return;
            var settings = JsonSerializer.Deserialize<ToolSettings>(File.ReadAllText(_settingsPath));
            foreach (var path in settings?.CustomTools?.Where(File.Exists).Distinct(StringComparer.OrdinalIgnoreCase) ?? [])
                _tools.Add(CreateCustomTool(path));
        }
        catch
        {
            ShowStatus("Tool settings could not be read", "The built-in tools are still available.", InfoBarSeverity.Warning);
        }
    }

    private void SaveTools()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(_settingsPath)!);
        var setting = new ToolSettings { CustomTools = _tools.Where(t => !t.IsBuiltIn).Select(t => t.Path).ToList() };
        File.WriteAllText(_settingsPath, JsonSerializer.Serialize(setting, new JsonSerializerOptions { WriteIndented = true }));
    }

    private static LaunchTool CreateCustomTool(string path) => new()
    {
        Name = Path.GetFileNameWithoutExtension(path), Path = path, ArgumentsTemplate = "", IsBuiltIn = false, Glyph = "\uE756"
    };

    private async void RefreshButton_Click(object sender, RoutedEventArgs e) => await RefreshPackagesAsync();

    private void PackagePivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (SearchBox is null) return;
        SearchBox.Text = "";
        SearchBox.PlaceholderText = PackagePivot.SelectedIndex == 0 ? "Search MSIX packages" : "Search App-V packages";
        ApplyPackageFilter();
    }

    private void SearchBox_TextChanged(object sender, TextChangedEventArgs e) => ApplyPackageFilter();

    private async void PackagesList_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
        await SelectPackageAsync(PackagesList.SelectedItem as PackageInfo);

    private async void ToolsList_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (ToolsList.SelectedItem is not LaunchTool tool || PackagesList.SelectedItem is not PackageInfo package)
        {
            ShowStatus("Select a package first", "Choose the MSIX or App-V package that should host the tool.", InfoBarSeverity.Informational);
            return;
        }
        try
        {
            StatusBar.IsOpen = false;
            PackageProgress.IsActive = true;
            await _powerShell.LaunchAsync(package, tool);
            ShowStatus("Launch request sent", $"{tool.Name} is starting in {package.Name}.", InfoBarSeverity.Success);
        }
        catch (Exception ex)
        {
            var error = CleanPowerShellError(ex.Message);
            if (error.Contains("0x80070005", StringComparison.OrdinalIgnoreCase))
                error = "Access was denied. Enable Developer Mode in Windows Settings, then try again.";
            ShowStatus("Couldn’t launch the tool", error, InfoBarSeverity.Error);
        }
        finally { PackageProgress.IsActive = false; }
    }

    private void ToolsList_KeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (e.Key != Windows.System.VirtualKey.Delete || ToolsList.SelectedItem is not LaunchTool tool || tool.IsBuiltIn)
            return;
        _tools.Remove(tool);
        SaveTools();
        ShowStatus("Tool removed", $"{tool.Name} was removed from your custom tool list.", InfoBarSeverity.Success);
        e.Handled = true;
    }

    private async void AddToolButton_Click(object sender, RoutedEventArgs e)
    {
        var picker = new FileOpenPicker();
        picker.FileTypeFilter.Add(".exe");
        picker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
        InitializeWithWindow.Initialize(picker, WindowNative.GetWindowHandle(this));
        var file = await picker.PickSingleFileAsync();
        if (file is null) return;
        if (_tools.Any(t => string.Equals(t.Path, file.Path, StringComparison.OrdinalIgnoreCase)))
        {
            ShowStatus("That tool is already listed", file.Path, InfoBarSeverity.Informational);
            return;
        }
        _tools.Add(CreateCustomTool(file.Path));
        SaveTools();
        ShowStatus("Tool added", $"{file.Name} will be available the next time you open Container Hover.", InfoBarSeverity.Success);
    }

    private async void AboutButton_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new ContentDialog
        {
            XamlRoot = Content.XamlRoot,
            Title = "Container Hover",
            Content = "A companion for launching desktop tools inside MSIX and App-V containers. Powered by Advanced Installer.",
            CloseButtonText = "Close",
            SecondaryButtonText = "Learn more"
        };
        var result = await dialog.ShowAsync();
        if (result == ContentDialogResult.Secondary)
            Process.Start(new ProcessStartInfo("https://github.com/Caphyon/hover") { UseShellExecute = true });
    }

    private void ShowStatus(string title, string message, InfoBarSeverity severity)
    {
        StatusBar.Title = title;
        StatusBar.Message = message;
        StatusBar.Severity = severity;
        StatusBar.IsOpen = true;
    }

    private static string CleanPowerShellError(string error)
    {
        var lines = error.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries).Where(line => !line.StartsWith("At line:", StringComparison.OrdinalIgnoreCase)).Take(3);
        return string.Join(" ", lines);
    }
}
