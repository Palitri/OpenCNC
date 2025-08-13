using System.Runtime.InteropServices;

namespace Palitri.OpenCNC.App
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Set app to be DPI aware, so that monitor resolution gets properly reported even if windows resolution scaling is used. Needed for determining pixels physical size for 1:1 Display:Pysical
            //SetProcessDPIAware();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}