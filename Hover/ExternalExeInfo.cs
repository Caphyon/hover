using System;
using System.Drawing;
using System.IO;

namespace Hover
{
  public class ExternalExeInfo
  {
    public string Name { get; private set; }
    public string ShortName
    {
      get
      {
        return System.IO.Path.GetFileNameWithoutExtension(Path);
      }
    }
    public string Path { get; private set; }
    public string ArgFormatStr { get; private set; }
    public Icon Icon { get; private set; }

    public bool IsPredefined { get; private set; }

    private string mWinFolder = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
    private string mSys32Folder = Environment.GetFolderPath(Environment.SpecialFolder.System);

    public ExternalExeInfo(string aName, string aArgFormatStr, bool aPredefined)
    {
      IsPredefined = aPredefined;
      ArgFormatStr = aArgFormatStr;

      if (aName.Length == 0)
        return;

      Name = aName;

      if (aName.Contains("\\"))
      {
        Path = aName;
      }
      else
      {
        // now we must detect the tool path and icon
        Path = mSys32Folder + "\\" + aName;

        if (!File.Exists(Path))
          Path = mWinFolder + "\\" + aName;

        if (!File.Exists(Path))
          throw new IOException("Required file does not exist");
      }

      Icon = System.Drawing.Icon.ExtractAssociatedIcon(Path);
    }

    public override string ToString()
    {
      return Name + " (64 bit)";
    }
  }
}
