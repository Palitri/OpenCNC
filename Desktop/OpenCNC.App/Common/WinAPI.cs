using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenCNC.App.Common
{
    public static class WinAPI
    {
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern IntPtr LoadImage(IntPtr hinst, string lpszName, uint uType, int cxDesired, int cyDesired, uint fuLoad);

        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        [DllImport("User32.dll")]
        private static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

        [DllImport("Shcore.dll")]
        private static extern int GetDpiForMonitor(IntPtr hmonitor, MonitorDpiType dpiType, out uint dpiX, out uint dpiY);

        private enum MonitorDpiType
        {
            MDT_EFFECTIVE_DPI = 0,
            MDT_ANGULAR_DPI = 1,
            MDT_RAW_DPI = 2,
            MDT_DEFAULT = MDT_EFFECTIVE_DPI
        }

        private const uint MONITOR_DEFAULTTONEAREST = 2;

        public static Cursor LoadCursor(string fileName)
        {
            const int IMAGE_CURSOR = 2; 
            const uint LR_LOADFROMFILE = 0x00000010;
            IntPtr ipImage = LoadImage(IntPtr.Zero, fileName, 
                IMAGE_CURSOR, 
                0, 
                0, 
                LR_LOADFROMFILE);

            return new Cursor(ipImage);
        }

        public static void SetDPIAware()
        {
            SetProcessDPIAware();
        }

        public static float GetScalingFactor(IntPtr hwnd)
        {
            IntPtr monitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);
            GetDpiForMonitor(monitor, MonitorDpiType.MDT_EFFECTIVE_DPI, out uint dpiX, out _);
            return dpiX / 96.0f; // 96 DPI = 100%
        }
    }
}
