using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace BrowserChoice
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(params string[] args)
        {
            if (args[0] == "--register")
            {
                Register();
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ChoiceForm(args[0]));
            }
        }

        private static void Register()
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var applicationName = assembly.ManifestModule.Name;
            var applicationFullPath = assembly.Location;

            string NoExe(string text) => text.Replace(".exe", string.Empty).Replace(".EXE", string.Empty);

            Registry.SetValue($@"HKEY_LOCAL_MACHINE\SOFTWARE\Classes\{NoExe(applicationName)}URL\DefaultIcon", string.Empty, $"{applicationFullPath}, 0", RegistryValueKind.String);
            Registry.SetValue($@"HKEY_LOCAL_MACHINE\SOFTWARE\Classes\{NoExe(applicationName)}URL\shell\open\command", string.Empty, $"\"{applicationFullPath}\" \"%1\"", RegistryValueKind.String);

            Registry.SetValue($@"HKEY_LOCAL_MACHINE\SOFTWARE\Clients\StartMenuInternet\{applicationName.ToUpperInvariant()}", string.Empty, "Browser Selector", RegistryValueKind.String);

            Registry.SetValue($@"HKEY_LOCAL_MACHINE\SOFTWARE\Clients\StartMenuInternet\{applicationName.ToUpperInvariant()}\DefaultIcon", string.Empty, $"{applicationFullPath}, 0", RegistryValueKind.String);
            Registry.SetValue($@"HKEY_LOCAL_MACHINE\SOFTWARE\Clients\StartMenuInternet\{applicationName.ToUpperInvariant()}\shell\open\command", string.Empty, $"\"{applicationFullPath}\" \"%1\"", RegistryValueKind.String);

            Registry.SetValue($@"HKEY_LOCAL_MACHINE\SOFTWARE\Clients\StartMenuInternet\{applicationName.ToUpperInvariant()}\Capabilities", "ApplicationIcon", $"{applicationFullPath}, 0", RegistryValueKind.String);
            Registry.SetValue($@"HKEY_LOCAL_MACHINE\SOFTWARE\Clients\StartMenuInternet\{applicationName.ToUpperInvariant()}\Capabilities", "ApplicationName", $"{NoExe(applicationName)}", RegistryValueKind.String);
            Registry.SetValue($@"HKEY_LOCAL_MACHINE\SOFTWARE\Clients\StartMenuInternet\{applicationName.ToUpperInvariant()}\Capabilities\StartMenu", "StartMenuInternet", $"{applicationName.ToUpperInvariant()}", RegistryValueKind.String);
            Registry.SetValue($@"HKEY_LOCAL_MACHINE\SOFTWARE\Clients\StartMenuInternet\{applicationName.ToUpperInvariant()}\Capabilities\URLAssociations", "http", $"{NoExe(applicationName)}URL", RegistryValueKind.String);
            Registry.SetValue($@"HKEY_LOCAL_MACHINE\SOFTWARE\Clients\StartMenuInternet\{applicationName.ToUpperInvariant()}\Capabilities\URLAssociations", "https", $"{NoExe(applicationName)}URL", RegistryValueKind.String);

            Registry.SetValue($@"HKEY_LOCAL_MACHINE\SOFTWARE\RegisteredApplications", NoExe(applicationName), $@"Software\Clients\StartMenuInternet\{applicationName.ToUpperInvariant()}\Capabilities", RegistryValueKind.String);
        }
    }
}
