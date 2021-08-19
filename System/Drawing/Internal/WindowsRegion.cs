// System.Drawing.Internal.WindowsRegion
using System;
using System.Drawing;
using System.Drawing.Internal;
using System.Runtime.InteropServices;

namespace System.Drawing.Internal
{
    internal sealed class WindowsRegion : MarshalByRefObject, ICloneable, IDisposable
    {
        private IntPtr nativeHandle;

        private bool ownHandle;

        public IntPtr HRegion => nativeHandle;

        public bool IsInfinite => nativeHandle == IntPtr.Zero;

        private WindowsRegion()
        {
        }

        public WindowsRegion(Rectangle rect)
        {
            CreateRegion(rect);
        }

        public WindowsRegion(int x, int y, int width, int height)
        {
            CreateRegion(new Rectangle(x, y, width, height));
        }

        public static WindowsRegion FromHregion(IntPtr hRegion, bool takeOwnership)
        {
            WindowsRegion windowsRegion = new WindowsRegion();
            if (hRegion != IntPtr.Zero)
            {
                windowsRegion.nativeHandle = hRegion;
                if (takeOwnership)
                {
                    windowsRegion.ownHandle = true;
                    System.Internal.HandleCollector.Add(hRegion, IntSafeNativeMethods.CommonHandles.GDI);
                }
            }
            return windowsRegion;
        }

        public static WindowsRegion FromRegion(Region region, Graphics g)
        {
            if (region.IsInfinite(g))
            {
                return new WindowsRegion();
            }
            return FromHregion(region.GetHrgn(g), takeOwnership: true);
        }

        public object Clone()
        {
            if (!IsInfinite)
            {
                return new WindowsRegion(ToRectangle());
            }
            return new WindowsRegion();
        }

        public IntNativeMethods.RegionFlags CombineRegion(WindowsRegion region1, WindowsRegion region2, RegionCombineMode mode)
        {
            return IntUnsafeNativeMethods.CombineRgn(new HandleRef(this, HRegion), new HandleRef(region1, region1.HRegion), new HandleRef(region2, region2.HRegion), mode);
        }

        private void CreateRegion(Rectangle rect)
        {
            nativeHandle = IntSafeNativeMethods.CreateRectRgn(rect.X, rect.Y, rect.X + rect.Width, rect.Y + rect.Height);
            ownHandle = true;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
        }

        public void Dispose(bool disposing)
        {
            if (nativeHandle != IntPtr.Zero)
            {
                if (ownHandle)
                {
                    IntUnsafeNativeMethods.DeleteObject(new HandleRef(this, nativeHandle));
                }
                nativeHandle = IntPtr.Zero;
                if (disposing)
                {
                    GC.SuppressFinalize(this);
                }
            }
        }

        ~WindowsRegion()
        {
            Dispose(disposing: false);
        }

        public Rectangle ToRectangle()
        {
            if (IsInfinite)
            {
                return new Rectangle(-2147483647, -2147483647, int.MaxValue, int.MaxValue);
            }
            IntNativeMethods.RECT clipRect = default(IntNativeMethods.RECT);
            IntUnsafeNativeMethods.GetRgnBox(new HandleRef(this, nativeHandle), ref clipRect);
            return new Rectangle(new Point(clipRect.left, clipRect.top), clipRect.Size);
        }
    }
}
