namespace Hover
{
  public class MSIXPackageInfo
  {
    public string Name { get; private set; }
    public string AppID { get; set; }
    public string Publisher { get; private set; }
    public string FullName { get; private set; }
    public string PackageFamilyName { get; private set; }
    public string Version { get; private set; }
    public string InstallLocation { get; private set; }

    public MSIXPackageInfo(string aName, string aPublisher, string aFullName, string aPackageFamilyName, string aVersion, string aInstallLocation)
    {
      Name = aName;
      Publisher = aPublisher;
      FullName = aFullName;
      PackageFamilyName = aPackageFamilyName;
      Version = aVersion;
      InstallLocation = aInstallLocation;
    }
    public override string ToString()
    {
      return Name;
    }
  }
}
