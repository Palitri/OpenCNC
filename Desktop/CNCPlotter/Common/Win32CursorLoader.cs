using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNCPlotter.Common
{
    public static class Win32CursorLoader
    {
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern IntPtr LoadImage(IntPtr hinst, string lpszName, uint uType, int cxDesired, int cyDesired, uint fuLoad);

        static Cursor LoadCursor(string fileName)
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
    }
}
