// System.Drawing.Internal.IntSafeNativeMethods
using System;
using System.Drawing.Internal;
using System.Internal;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Drawing.Internal
{
    [SuppressUnmanagedCodeSecurity]
    internal static class IntSafeNativeMethods
    {
        public sealed class CommonHandles
        {
            public static readonly int EMF;

            public static readonly int GDI;

            public static readonly int HDC;

            static CommonHandles()
            {
                EMF = System.Internal.HandleCollector.RegisterType("EnhancedMetaFile", 20, 500);
                GDI = System.Internal.HandleCollector.RegisterType("GDI", 90, 50);
                HDC = System.Internal.HandleCollector.RegisterType("HDC", 100, 2);
            }
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateSolidBrush", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr IntCreateSolidBrush(int crColor);

        public static IntPtr CreateSolidBrush(int crColor)
        {
            return System.Internal.HandleCollector.Add(IntCreateSolidBrush(crColor), CommonHandles.GDI);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreatePen", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr IntCreatePen(int fnStyle, int nWidth, int crColor);

        public static IntPtr CreatePen(int fnStyle, int nWidth, int crColor)
        {
            return System.Internal.HandleCollector.Add(IntCreatePen(fnStyle, nWidth, crColor), CommonHandles.GDI);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "ExtCreatePen", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr IntExtCreatePen(int fnStyle, int dwWidth, IntNativeMethods.LOGBRUSH lplb, int dwStyleCount, [MarshalAs(UnmanagedType.LPArray)] int[] lpStyle);

        public static IntPtr ExtCreatePen(int fnStyle, int dwWidth, IntNativeMethods.LOGBRUSH lplb, int dwStyleCount, int[] lpStyle)
        {
            return System.Internal.HandleCollector.Add(IntExtCreatePen(fnStyle, dwWidth, lplb, dwStyleCount, lpStyle), CommonHandles.GDI);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateRectRgn", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr IntCreateRectRgn(int x1, int y1, int x2, int y2);

        public static IntPtr CreateRectRgn(int x1, int y1, int x2, int y2)
        {
            return System.Internal.HandleCollector.Add(IntCreateRectRgn(x1, y1, x2, y2), CommonHandles.GDI);
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetUserDefaultLCID();

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern bool GdiFlush();
    }
}
