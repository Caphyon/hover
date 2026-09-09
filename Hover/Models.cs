using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ContainerHover;

public enum PackageKind { Msix, AppV }

public sealed class PackageInfo : INotifyPropertyChanged
{
    private string _appId = string.Empty;

    public PackageKind Kind { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Publisher { get; init; } = string.Empty;
    public string FullName { get; init; } = string.Empty;
    public string FamilyName { get; init; } = string.Empty;
    public string Version { get; init; } = string.Empty;
    public string InstallLocation { get; init; } = string.Empty;
    public string PackageId { get; init; } = string.Empty;
    public string VersionId { get; init; } = string.Empty;
    public string AppId
    {
        get => _appId;
        set { _appId = value; OnPropertyChanged(); OnPropertyChanged(nameof(SecondaryText)); }
    }

    public string SecondaryText => Kind == PackageKind.Msix
        ? string.IsNullOrWhiteSpace(Publisher) ? FamilyName : Publisher
        : $"Package ID: {PackageId}";

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}

public sealed class LaunchTool
{
    public string Name { get; init; } = string.Empty;
    public string Path { get; init; } = string.Empty;
    public string ArgumentsTemplate { get; init; } = string.Empty;
    public bool IsBuiltIn { get; init; }
    public string Glyph { get; init; } = "\uE756";
    public string Description => IsBuiltIn ? Path : "Custom executable";
}

public sealed class ToolSettings
{
    public List<string> CustomTools { get; init; } = [];
}
