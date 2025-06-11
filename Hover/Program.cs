using System;
using System.Windows.Forms;
using System.Reflection;
using System.Globalization;

namespace Hover
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      AppDomain.CurrentDomain.AssemblyResolve += OnResolveAssembly;
      Application.Run(new LauncherForm());
    }
    private static Assembly OnResolveAssembly(object sender, ResolveEventArgs args)
    {
      var executingAssembly = Assembly.GetExecutingAssembly();
      var assemblyName = new AssemblyName(args.Name);

      var path = assemblyName.Name + ".dll";
      if (!assemblyName.CultureInfo.Equals(CultureInfo.InvariantCulture))
      {
        path = $"{assemblyName.CultureInfo}\\${path}";
      }

      var stream = executingAssembly.GetManifestResourceStream(path);
      if (stream == null)
        return null;

      var assemblyRawBytes = new byte[stream.Length];
      stream.Read(assemblyRawBytes, 0, assemblyRawBytes.Length);
      return Assembly.Load(assemblyRawBytes);
    }
  }
}
