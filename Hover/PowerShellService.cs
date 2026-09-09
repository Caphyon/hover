using System.Diagnostics;
using System.Text.Json;

namespace ContainerHover;

public sealed class PowerShellService
{
    public async Task<IReadOnlyList<PackageInfo>> GetMsixPackagesAsync()
    {
        const string script = "Get-AppxPackage | Select-Object Name,Publisher,PackageFullName,PackageFamilyName,Version,InstallLocation | ConvertTo-Json -Depth 3 -Compress";
        var items = await RunJsonAsync<MsixDto>(script);
        return items
            .Where(p => !string.Equals(p.Name, "Windows.MiracastView", StringComparison.OrdinalIgnoreCase))
            .Select(p => new PackageInfo
            {
                Kind = PackageKind.Msix,
                Name = p.Name ?? string.Empty,
                Publisher = p.Publisher ?? string.Empty,
                FullName = p.PackageFullName ?? string.Empty,
                FamilyName = p.PackageFamilyName ?? string.Empty,
                Version = p.Version ?? string.Empty,
                InstallLocation = p.InstallLocation ?? string.Empty
            })
            .OrderBy(p => p.Name, StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    public async Task<IReadOnlyList<PackageInfo>> GetAppVPackagesAsync()
    {
        const string script = "Import-Module AppVClient -ErrorAction Stop; Get-AppvClientPackage -All | Where-Object {$_.IsPublishedToUser} | Select-Object Name,PackageId,VersionId | ConvertTo-Json -Compress";
        var items = await RunJsonAsync<AppVDto>(script);
        return items.Select(p => new PackageInfo
        {
            Kind = PackageKind.AppV,
            Name = p.Name ?? string.Empty,
            PackageId = p.PackageId ?? string.Empty,
            VersionId = p.VersionId ?? string.Empty
        }).OrderBy(p => p.Name, StringComparer.OrdinalIgnoreCase).ToList();
    }

    public async Task<string> GetMsixAppIdAsync(string packageName)
    {
        var output = await RunAsync($"(Get-AppxPackage -Name {Quote(packageName)} | Get-AppxPackageManifest).Package.Applications.Application.Id");
        return output.StandardOutput.Trim().Split('\r', '\n', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? "App";
    }

    public async Task LaunchAsync(PackageInfo package, LaunchTool tool)
    {
        string script;
        if (package.Kind == PackageKind.Msix)
        {
            var appId = string.IsNullOrWhiteSpace(package.AppId) ? "App" : package.AppId;
            var workingFolder = tool.IsBuiltIn && !string.Equals(tool.Name, "Registry Editor", StringComparison.Ordinal) ? package.InstallLocation : string.Empty;
            var args = string.Format(tool.ArgumentsTemplate, workingFolder);
            script = $"Invoke-CommandInDesktopPackage -PackageFamilyName {Quote(package.FamilyName)} -appid {Quote(appId)} -command {Quote(tool.Path)} -args {Quote(args)} -preventBreakaway";
        }
        else
        {
            script = $"$pkg = Get-AppvClientPackage -PackageId {Quote(package.PackageId)} -VersionId {Quote(package.VersionId)}; Start-AppvVirtualProcess -AppvClientObject $pkg {Quote(tool.Path)}";
        }

        var result = await RunAsync(script);
        if (result.ExitCode != 0)
            throw new InvalidOperationException(string.IsNullOrWhiteSpace(result.StandardError) ? "PowerShell could not launch the selected tool." : result.StandardError.Trim());
    }

    private static async Task<List<T>> RunJsonAsync<T>(string script)
    {
        var result = await RunAsync(script);
        if (result.ExitCode != 0)
            throw new InvalidOperationException(string.IsNullOrWhiteSpace(result.StandardError) ? "PowerShell did not return package information." : result.StandardError.Trim());
        if (string.IsNullOrWhiteSpace(result.StandardOutput)) return [];

        using var doc = JsonDocument.Parse(result.StandardOutput);
        var json = doc.RootElement.ValueKind == JsonValueKind.Array ? result.StandardOutput : $"[{result.StandardOutput}]";
        return JsonSerializer.Deserialize<List<T>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? [];
    }

    private static async Task<ProcessResult> RunAsync(string script)
    {
        using var process = new Process();
        process.StartInfo.FileName = "powershell.exe";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.ArgumentList.Add("-NoProfile");
        process.StartInfo.ArgumentList.Add("-NonInteractive");
        process.StartInfo.ArgumentList.Add("-Command");
        process.StartInfo.ArgumentList.Add(script);
        process.Start();
        var output = process.StandardOutput.ReadToEndAsync();
        var error = process.StandardError.ReadToEndAsync();
        await process.WaitForExitAsync();
        return new ProcessResult(process.ExitCode, await output, await error);
    }

    private static string Quote(string value) => "'" + value.Replace("'", "''") + "'";
    private sealed record ProcessResult(int ExitCode, string StandardOutput, string StandardError);
    private sealed class MsixDto { public string? Name { get; set; } public string? Publisher { get; set; } public string? PackageFullName { get; set; } public string? PackageFamilyName { get; set; } public string? Version { get; set; } public string? InstallLocation { get; set; } }
    private sealed class AppVDto { public string? Name { get; set; } public string? PackageId { get; set; } public string? VersionId { get; set; } }
}
