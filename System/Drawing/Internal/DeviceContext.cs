// System.Drawing.Internal.DeviceContext
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Internal;
using System.Runtime.InteropServices;

namespace System.Drawing.Internal
{
    internal sealed class DeviceContext : MarshalByRefObject, IDeviceContext, IDisposable
    {
        internal class GraphicsState
        {
            internal IntPtr hBrush;

            internal IntPtr hFont;

            internal IntPtr hPen;

            internal IntPtr hBitmap;
        }

        private IntPtr hDC;

        private DeviceContextType dcType;

        private bool disposed;

        private IntPtr hWnd = (IntPtr)(-1);

        private IntPtr hInitialPen;

        private IntPtr hInitialBrush;

        private IntPtr hInitialBmp;

        private IntPtr hInitialFont;

        private IntPtr hCurrentPen;

        private IntPtr hCurrentBrush;

        private IntPtr hCurrentBmp;

        private IntPtr hCurrentFont;

        private Stack contextStack;

        public DeviceContextType DeviceContextType => dcType;

        public IntPtr Hdc
        {
            get
            {
                if (hDC == IntPtr.Zero && dcType == DeviceContextType.Display)
                {
                    hDC = ((IDeviceContext)this).GetHdc();
                    CacheInitialState();
                }
                return hDC;
            }
        }

        public DeviceContextGraphicsMode GraphicsMode => (DeviceContextGraphicsMode)IntUnsafeNativeMethods.GetGraphicsMode(new HandleRef(this, Hdc));

        public event EventHandler Disposing;

        private void CacheInitialState()
        {
            hCurrentPen = (hInitialPen = IntUnsafeNativeMethods.GetCurrentObject(new HandleRef(this, hDC), 1));
            hCurrentBrush = (hInitialBrush = IntUnsafeNativeMethods.GetCurrentObject(new HandleRef(this, hDC), 2));
            hCurrentBmp = (hInitialBmp = IntUnsafeNativeMethods.GetCurrentObject(new HandleRef(this, hDC), 7));
            hCurrentFont = (hInitialFont = IntUnsafeNativeMethods.GetCurrentObject(new HandleRef(this, hDC), 6));
        }

        public void DeleteObject(IntPtr handle, GdiObjectType type)
        {
            IntPtr handle2 = IntPtr.Zero;
            switch (type)
            {
                case GdiObjectType.Pen:
                    if (handle == hCurrentPen)
                    {
                        IntPtr intPtr2 = IntUnsafeNativeMethods.SelectObject(new HandleRef(this, Hdc), new HandleRef(this, hInitialPen));
                        hCurrentPen = IntPtr.Zero;
                    }
                    handle2 = handle;
                    break;
                case GdiObjectType.Brush:
                    if (handle == hCurrentBrush)
                    {
                        IntPtr intPtr3 = IntUnsafeNativeMethods.SelectObject(new HandleRef(this, Hdc), new HandleRef(this, hInitialBrush));
                        hCurrentBrush = IntPtr.Zero;
                    }
                    handle2 = handle;
                    break;
                case GdiObjectType.Bitmap:
                    if (handle == hCurrentBmp)
                    {
                        IntPtr intPtr = IntUnsafeNativeMethods.SelectObject(new HandleRef(this, Hdc), new HandleRef(this, hInitialBmp));
                        hCurrentBmp = IntPtr.Zero;
                    }
                    handle2 = handle;
                    break;
            }
            IntUnsafeNativeMethods.DeleteObject(new HandleRef(this, handle2));
        }

        private DeviceContext(IntPtr hWnd)
        {
            this.hWnd = hWnd;
            dcType = DeviceContextType.Display;
            DeviceContexts.AddDeviceContext(this);
        }

        private DeviceContext(IntPtr hDC, DeviceContextType dcType)
        {
            this.hDC = hDC;
            this.dcType = dcType;
            CacheInitialState();
            DeviceContexts.AddDeviceContext(this);
            if (dcType == DeviceContextType.Display)
            {
                hWnd = IntUnsafeNativeMethods.WindowFromDC(new HandleRef(this, this.hDC));
            }
        }

        public static DeviceContext CreateDC(string driverName, string deviceName, string fileName, HandleRef devMode)
        {
            IntPtr intPtr = IntUnsafeNativeMethods.CreateDC(driverName, deviceName, fileName, devMode);
            return new DeviceContext(intPtr, DeviceContextType.NamedDevice);
        }

        public static DeviceContext CreateIC(string driverName, string deviceName, string fileName, HandleRef devMode)
        {
            IntPtr intPtr = IntUnsafeNativeMethods.CreateIC(driverName, deviceName, fileName, devMode);
            return new DeviceContext(intPtr, DeviceContextType.Information);
        }

        public static DeviceContext FromCompatibleDC(IntPtr hdc)
        {
            IntPtr intPtr = IntUnsafeNativeMethods.CreateCompatibleDC(new HandleRef(null, hdc));
            return new DeviceContext(intPtr, DeviceContextType.Memory);
        }

        public static DeviceContext FromHdc(IntPtr hdc)
        {
            return new DeviceContext(hdc, DeviceContextType.Unknown);
        }

        public static DeviceContext FromHwnd(IntPtr hwnd)
        {
            return new DeviceContext(hwnd);
        }

        ~DeviceContext()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        internal void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (this.Disposing != null)
                {
                    this.Disposing(this, EventArgs.Empty);
                }
                disposed = true;
                switch (dcType)
                {
                    case DeviceContextType.Display:
                        ((IDeviceContext)this).ReleaseHdc();
                        break;
                    case DeviceContextType.NamedDevice:
                    case DeviceContextType.Information:
                        IntUnsafeNativeMethods.DeleteHDC(new HandleRef(this, hDC));
                        hDC = IntPtr.Zero;
                        break;
                    case DeviceContextType.Memory:
                        IntUnsafeNativeMethods.DeleteDC(new HandleRef(this, hDC));
                        hDC = IntPtr.Zero;
                        break;
                    case DeviceContextType.Unknown:
                    case DeviceContextType.NCWindow:
                        break;
                }
            }
        }

        IntPtr IDeviceContext.GetHdc()
        {
            if (hDC == IntPtr.Zero)
            {
                hDC = IntUnsafeNativeMethods.GetDC(new HandleRef(this, hWnd));
            }
            return hDC;
        }

        void IDeviceContext.ReleaseHdc()
        {
            if (hDC != IntPtr.Zero && dcType == DeviceContextType.Display)
            {
                IntUnsafeNativeMethods.ReleaseDC(new HandleRef(this, hWnd), new HandleRef(this, hDC));
                hDC = IntPtr.Zero;
            }
        }

        public DeviceContextGraphicsMode SetGraphicsMode(DeviceContextGraphicsMode newMode)
        {
            return (DeviceContextGraphicsMode)IntUnsafeNativeMethods.SetGraphicsMode(new HandleRef(this, Hdc), (int)newMode);
        }

        public void RestoreHdc()
        {
            IntUnsafeNativeMethods.RestoreDC(new HandleRef(this, hDC), -1);
            if (contextStack != null)
            {
                GraphicsState graphicsState = (GraphicsState)contextStack.Pop();
                hCurrentBmp = graphicsState.hBitmap;
                hCurrentBrush = graphicsState.hBrush;
                hCurrentPen = graphicsState.hPen;
                hCurrentFont = graphicsState.hFont;
            }
        }

        public int SaveHdc()
        {
            HandleRef handleRef = new HandleRef(this, Hdc);
            int result = IntUnsafeNativeMethods.SaveDC(handleRef);
            if (contextStack == null)
            {
                contextStack = new Stack();
            }
            GraphicsState graphicsState = new GraphicsState();
            graphicsState.hBitmap = hCurrentBmp;
            graphicsState.hBrush = hCurrentBrush;
            graphicsState.hPen = hCurrentPen;
            graphicsState.hFont = hCurrentFont;
            contextStack.Push(graphicsState);
            return result;
        }

        public void SetClip(WindowsRegion region)
        {
            HandleRef handleRef = new HandleRef(this, Hdc);
            HandleRef hRgn = new HandleRef(region, region.HRegion);
            IntUnsafeNativeMethods.SelectClipRgn(handleRef, hRgn);
        }

        public void IntersectClip(WindowsRegion wr)
        {
            if (wr.HRegion == IntPtr.Zero)
            {
                return;
            }
            WindowsRegion windowsRegion = new WindowsRegion(0, 0, 0, 0);
            try
            {
                int clipRgn = IntUnsafeNativeMethods.GetClipRgn(new HandleRef(this, Hdc), new HandleRef(windowsRegion, windowsRegion.HRegion));
                if (clipRgn == 1)
                {
                    wr.CombineRegion(windowsRegion, wr, RegionCombineMode.AND);
                }
                SetClip(wr);
            }
            finally
            {
                windowsRegion.Dispose();
            }
        }

        public void TranslateTransform(int dx, int dy)
        {
            IntNativeMethods.POINT point = new IntNativeMethods.POINT();
            IntUnsafeNativeMethods.OffsetViewportOrgEx(new HandleRef(this, Hdc), dx, dy, point);
        }

        public override bool Equals(object obj)
        {
            DeviceContext deviceContext = obj as DeviceContext;
            if (deviceContext == this)
            {
                return true;
            }
            if (deviceContext == null)
            {
                return false;
            }
            return deviceContext.Hdc == Hdc;
        }

        public override int GetHashCode()
        {
            return Hdc.GetHashCode();
        }
    }
}
