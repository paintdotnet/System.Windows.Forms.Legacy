using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms
{
    internal static class ControlPrivate
    {
        // internal Control.CheckParentingCycle()
        public static void CheckParentingCycle(Control bottom, Control toFind)
        {
            // do nothing
        }

        public static IntPtr SetUpPalette(IntPtr dc, bool force, bool realizePalette)
        {
            IntPtr halftonePalette = Graphics.GetHalftonePalette();
            IntPtr intPtr = SafeNativeMethods.SelectPalette(new HandleRef(null, dc), new HandleRef(null, halftonePalette), (!force) ? 1 : 0);
            if (intPtr != IntPtr.Zero && realizePalette)
            {
                SafeNativeMethods.RealizePalette(new HandleRef(null, dc));
            }
            return intPtr;
        }
    }
}
