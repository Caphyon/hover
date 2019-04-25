using System;

namespace Hover
{
  public class AppvPackageInfo
  {
    public string Name { get; private set; }
    public Guid PackageId { get; private set; }
    public Guid VersionId { get; private set; }

    public AppvPackageInfo(string aName, Guid aPkgId, Guid aVerId)
    {
      Name = aName;
      PackageId = aPkgId;
      VersionId = aVerId;
    }
    public override string ToString()
    {
      return Name;
    }
  }
}
