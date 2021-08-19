// System.Drawing.Internal.IntUnsafeNativeMethods
using System;
using System.Drawing.Internal;
using System.Internal;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Drawing.Internal
{
    [SuppressUnmanagedCodeSecurity]
    internal static class IntUnsafeNativeMethods
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetDC", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr IntGetDC(HandleRef hWnd);

        public static IntPtr GetDC(HandleRef hWnd)
        {
            return System.Internal.HandleCollector.Add(IntGetDC(hWnd), IntSafeNativeMethods.CommonHandles.HDC);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "DeleteDC", ExactSpelling = true, SetLastError = true)]
        public static extern bool IntDeleteDC(HandleRef hDC);

        public static bool DeleteDC(HandleRef hDC)
        {
            System.Internal.HandleCollector.Remove((IntPtr)hDC, IntSafeNativeMethods.CommonHandles.GDI);
            return IntDeleteDC(hDC);
        }

        public static bool DeleteHDC(HandleRef hDC)
        {
            System.Internal.HandleCollector.Remove((IntPtr)hDC, IntSafeNativeMethods.CommonHandles.HDC);
            return IntDeleteDC(hDC);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "ReleaseDC", ExactSpelling = true, SetLastError = true)]
        public static extern int IntReleaseDC(HandleRef hWnd, HandleRef hDC);

        public static int ReleaseDC(HandleRef hWnd, HandleRef hDC)
        {
            System.Internal.HandleCollector.Remove((IntPtr)hDC, IntSafeNativeMethods.CommonHandles.HDC);
            return IntReleaseDC(hWnd, hDC);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateDC", SetLastError = true)]
        public static extern IntPtr IntCreateDC(string lpszDriverName, string lpszDeviceName, string lpszOutput, HandleRef lpInitData);

        public static IntPtr CreateDC(string lpszDriverName, string lpszDeviceName, string lpszOutput, HandleRef lpInitData)
        {
            return System.Internal.HandleCollector.Add(IntCreateDC(lpszDriverName, lpszDeviceName, lpszOutput, lpInitData), IntSafeNativeMethods.CommonHandles.HDC);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateIC", SetLastError = true)]
        public static extern IntPtr IntCreateIC(string lpszDriverName, string lpszDeviceName, string lpszOutput, HandleRef lpInitData);

        public static IntPtr CreateIC(string lpszDriverName, string lpszDeviceName, string lpszOutput, HandleRef lpInitData)
        {
            return System.Internal.HandleCollector.Add(IntCreateIC(lpszDriverName, lpszDeviceName, lpszOutput, lpInitData), IntSafeNativeMethods.CommonHandles.HDC);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateCompatibleDC", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr IntCreateCompatibleDC(HandleRef hDC);

        public static IntPtr CreateCompatibleDC(HandleRef hDC)
        {
            return System.Internal.HandleCollector.Add(IntCreateCompatibleDC(hDC), IntSafeNativeMethods.CommonHandles.GDI);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "SaveDC", ExactSpelling = true, SetLastError = true)]
        public static extern int IntSaveDC(HandleRef hDC);

        public static int SaveDC(HandleRef hDC)
        {
            return IntSaveDC(hDC);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "RestoreDC", ExactSpelling = true, SetLastError = true)]
        public static extern bool IntRestoreDC(HandleRef hDC, int nSavedDC);

        public static bool RestoreDC(HandleRef hDC, int nSavedDC)
        {
            return IntRestoreDC(hDC, nSavedDC);
        }

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr WindowFromDC(HandleRef hDC);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int GetDeviceCaps(HandleRef hDC, int nIndex);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "OffsetViewportOrgEx", ExactSpelling = true, SetLastError = true)]
        public static extern bool IntOffsetViewportOrgEx(HandleRef hDC, int nXOffset, int nYOffset, [In][Out] IntNativeMethods.POINT point);

        public static bool OffsetViewportOrgEx(HandleRef hDC, int nXOffset, int nYOffset, [In][Out] IntNativeMethods.POINT point)
        {
            return IntOffsetViewportOrgEx(hDC, nXOffset, nYOffset, point);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "SetGraphicsMode", ExactSpelling = true, SetLastError = true)]
        public static extern int IntSetGraphicsMode(HandleRef hDC, int iMode);

        public static int SetGraphicsMode(HandleRef hDC, int iMode)
        {
            iMode = IntSetGraphicsMode(hDC, iMode);
            return iMode;
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int GetGraphicsMode(HandleRef hDC);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern int GetROP2(HandleRef hdc);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int SetROP2(HandleRef hDC, int nDrawMode);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CombineRgn", ExactSpelling = true, SetLastError = true)]
        public static extern IntNativeMethods.RegionFlags IntCombineRgn(HandleRef hRgnDest, HandleRef hRgnSrc1, HandleRef hRgnSrc2, RegionCombineMode combineMode);

        public static IntNativeMethods.RegionFlags CombineRgn(HandleRef hRgnDest, HandleRef hRgnSrc1, HandleRef hRgnSrc2, RegionCombineMode combineMode)
        {
            if (hRgnDest.Wrapper == null || hRgnSrc1.Wrapper == null || hRgnSrc2.Wrapper == null)
            {
                return IntNativeMethods.RegionFlags.ERROR;
            }
            return IntCombineRgn(hRgnDest, hRgnSrc1, hRgnSrc2, combineMode);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "GetClipRgn", ExactSpelling = true, SetLastError = true)]
        public static extern int IntGetClipRgn(HandleRef hDC, HandleRef hRgn);

        public static int GetClipRgn(HandleRef hDC, HandleRef hRgn)
        {
            return IntGetClipRgn(hDC, hRgn);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "SelectClipRgn", ExactSpelling = true, SetLastError = true)]
        public static extern IntNativeMethods.RegionFlags IntSelectClipRgn(HandleRef hDC, HandleRef hRgn);

        public static IntNativeMethods.RegionFlags SelectClipRgn(HandleRef hDC, HandleRef hRgn)
        {
            return IntSelectClipRgn(hDC, hRgn);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "GetRgnBox", ExactSpelling = true, SetLastError = true)]
        public static extern IntNativeMethods.RegionFlags IntGetRgnBox(HandleRef hRgn, [In][Out] ref IntNativeMethods.RECT clipRect);

        public static IntNativeMethods.RegionFlags GetRgnBox(HandleRef hRgn, [In][Out] ref IntNativeMethods.RECT clipRect)
        {
            return IntGetRgnBox(hRgn, ref clipRect);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "DeleteObject", ExactSpelling = true, SetLastError = true)]
        public static extern bool IntDeleteObject(HandleRef hObject);

        public static bool DeleteObject(HandleRef hObject)
        {
            System.Internal.HandleCollector.Remove((IntPtr)hObject, IntSafeNativeMethods.CommonHandles.GDI);
            return IntDeleteObject(hObject);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "GetObject", SetLastError = true)]
        public static extern int IntGetObject(HandleRef hBrush, int nSize, [In][Out] IntNativeMethods.LOGBRUSH lb);

        public static int GetObject(HandleRef hBrush, IntNativeMethods.LOGBRUSH lb)
        {
            return IntGetObject(hBrush, Marshal.SizeOf(typeof(IntNativeMethods.LOGBRUSH)), lb);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "GetObject", SetLastError = true)]
        public static extern int IntGetObject(HandleRef hFont, int nSize, [In][Out] IntNativeMethods.LOGFONT lf);

        public static int GetObject(HandleRef hFont, IntNativeMethods.LOGFONT lp)
        {
            return IntGetObject(hFont, Marshal.SizeOf(typeof(IntNativeMethods.LOGFONT)), lp);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "SelectObject", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr IntSelectObject(HandleRef hdc, HandleRef obj);

        public static IntPtr SelectObject(HandleRef hdc, HandleRef obj)
        {
            return IntSelectObject(hdc, obj);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "GetCurrentObject", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr IntGetCurrentObject(HandleRef hDC, int uObjectType);

        public static IntPtr GetCurrentObject(HandleRef hDC, int uObjectType)
        {
            return IntGetCurrentObject(hDC, uObjectType);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "GetStockObject", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr IntGetStockObject(int nIndex);

        public static IntPtr GetStockObject(int nIndex)
        {
            return IntGetStockObject(nIndex);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int GetNearestColor(HandleRef hDC, int color);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int SetTextColor(HandleRef hDC, int crColor);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int GetTextAlign(HandleRef hdc);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int GetTextColor(HandleRef hDC);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int SetBkColor(HandleRef hDC, int clr);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "SetBkMode", ExactSpelling = true, SetLastError = true)]
        public static extern int IntSetBkMode(HandleRef hDC, int nBkMode);

        public static int SetBkMode(HandleRef hDC, int nBkMode)
        {
            return IntSetBkMode(hDC, nBkMode);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "GetBkMode", ExactSpelling = true, SetLastError = true)]
        public static extern int IntGetBkMode(HandleRef hDC);

        public static int GetBkMode(HandleRef hDC)
        {
            return IntGetBkMode(hDC);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int GetBkColor(HandleRef hDC);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        public static extern int DrawTextW(HandleRef hDC, string lpszString, int nCount, ref IntNativeMethods.RECT lpRect, int nFormat);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern int DrawTextA(HandleRef hDC, byte[] lpszString, int byteCount, ref IntNativeMethods.RECT lpRect, int nFormat);

        public static int DrawText(HandleRef hDC, string text, ref IntNativeMethods.RECT lpRect, int nFormat)
        {
            if (Marshal.SystemDefaultCharSize == 1)
            {
                lpRect.top = Math.Min(32767, lpRect.top);
                lpRect.left = Math.Min(32767, lpRect.left);
                lpRect.right = Math.Min(32767, lpRect.right);
                lpRect.bottom = Math.Min(32767, lpRect.bottom);
                int num = WideCharToMultiByte(0, 0, text, text.Length, null, 0, IntPtr.Zero, IntPtr.Zero);
                byte[] array = new byte[num];
                WideCharToMultiByte(0, 0, text, text.Length, array, array.Length, IntPtr.Zero, IntPtr.Zero);
                num = Math.Min(num, 8192);
                return DrawTextA(hDC, array, num, ref lpRect, nFormat);
            }
            return DrawTextW(hDC, text, text.Length, ref lpRect, nFormat);
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int DrawTextExW(HandleRef hDC, string lpszString, int nCount, ref IntNativeMethods.RECT lpRect, int nFormat, [In][Out] IntNativeMethods.DRAWTEXTPARAMS lpDTParams);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int DrawTextExA(HandleRef hDC, byte[] lpszString, int byteCount, ref IntNativeMethods.RECT lpRect, int nFormat, [In][Out] IntNativeMethods.DRAWTEXTPARAMS lpDTParams);

        public static int DrawTextEx(HandleRef hDC, string text, ref IntNativeMethods.RECT lpRect, int nFormat, [In][Out] IntNativeMethods.DRAWTEXTPARAMS lpDTParams)
        {
            if (Marshal.SystemDefaultCharSize == 1)
            {
                lpRect.top = Math.Min(32767, lpRect.top);
                lpRect.left = Math.Min(32767, lpRect.left);
                lpRect.right = Math.Min(32767, lpRect.right);
                lpRect.bottom = Math.Min(32767, lpRect.bottom);
                int num = WideCharToMultiByte(0, 0, text, text.Length, null, 0, IntPtr.Zero, IntPtr.Zero);
                byte[] array = new byte[num];
                WideCharToMultiByte(0, 0, text, text.Length, array, array.Length, IntPtr.Zero, IntPtr.Zero);
                num = Math.Min(num, 8192);
                return DrawTextExA(hDC, array, num, ref lpRect, nFormat, lpDTParams);
            }
            return DrawTextExW(hDC, text, text.Length, ref lpRect, nFormat, lpDTParams);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        public static extern int GetTextExtentPoint32W(HandleRef hDC, string text, int len, [In][Out] IntNativeMethods.SIZE size);

        [DllImport("gdi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern int GetTextExtentPoint32A(HandleRef hDC, byte[] lpszString, int byteCount, [In][Out] IntNativeMethods.SIZE size);

        public static int GetTextExtentPoint32(HandleRef hDC, string text, [In][Out] IntNativeMethods.SIZE size)
        {
            int length = text.Length;
            if (Marshal.SystemDefaultCharSize == 1)
            {
                length = WideCharToMultiByte(0, 0, text, text.Length, null, 0, IntPtr.Zero, IntPtr.Zero);
                byte[] array = new byte[length];
                WideCharToMultiByte(0, 0, text, text.Length, array, array.Length, IntPtr.Zero, IntPtr.Zero);
                length = Math.Min(text.Length, 8192);
                return GetTextExtentPoint32A(hDC, array, length, size);
            }
            return GetTextExtentPoint32W(hDC, text, text.Length, size);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool ExtTextOut(HandleRef hdc, int x, int y, int options, ref IntNativeMethods.RECT rect, string str, int length, int[] spacing);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "LineTo", ExactSpelling = true, SetLastError = true)]
        public static extern bool IntLineTo(HandleRef hdc, int x, int y);

        public static bool LineTo(HandleRef hdc, int x, int y)
        {
            return IntLineTo(hdc, x, y);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "MoveToEx", ExactSpelling = true, SetLastError = true)]
        public static extern bool IntMoveToEx(HandleRef hdc, int x, int y, IntNativeMethods.POINT pt);

        public static bool MoveToEx(HandleRef hdc, int x, int y, IntNativeMethods.POINT pt)
        {
            return IntMoveToEx(hdc, x, y, pt);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "Rectangle", ExactSpelling = true, SetLastError = true)]
        public static extern bool IntRectangle(HandleRef hdc, int left, int top, int right, int bottom);

        public static bool Rectangle(HandleRef hdc, int left, int top, int right, int bottom)
        {
            return IntRectangle(hdc, left, top, right, bottom);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "FillRect", ExactSpelling = true, SetLastError = true)]
        public static extern bool IntFillRect(HandleRef hdc, [In] ref IntNativeMethods.RECT rect, HandleRef hbrush);

        public static bool FillRect(HandleRef hDC, [In] ref IntNativeMethods.RECT rect, HandleRef hbrush)
        {
            return IntFillRect(hDC, ref rect, hbrush);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "SetMapMode", ExactSpelling = true, SetLastError = true)]
        public static extern int IntSetMapMode(HandleRef hDC, int nMapMode);

        public static int SetMapMode(HandleRef hDC, int nMapMode)
        {
            return IntSetMapMode(hDC, nMapMode);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "GetMapMode", ExactSpelling = true, SetLastError = true)]
        public static extern int IntGetMapMode(HandleRef hDC);

        public static int GetMapMode(HandleRef hDC)
        {
            return IntGetMapMode(hDC);
        }

        [DllImport("gdi32.dll", EntryPoint = "GetViewportExtEx", ExactSpelling = true, SetLastError = true)]
        public static extern bool IntGetViewportExtEx(HandleRef hdc, [In][Out] IntNativeMethods.SIZE lpSize);

        public static bool GetViewportExtEx(HandleRef hdc, [In][Out] IntNativeMethods.SIZE lpSize)
        {
            return IntGetViewportExtEx(hdc, lpSize);
        }

        [DllImport("gdi32.dll", EntryPoint = "GetViewportOrgEx", ExactSpelling = true, SetLastError = true)]
        public static extern bool IntGetViewportOrgEx(HandleRef hdc, [In][Out] IntNativeMethods.POINT lpPoint);

        public static bool GetViewportOrgEx(HandleRef hdc, [In][Out] IntNativeMethods.POINT lpPoint)
        {
            return IntGetViewportOrgEx(hdc, lpPoint);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "SetViewportExtEx", ExactSpelling = true, SetLastError = true)]
        public static extern bool IntSetViewportExtEx(HandleRef hDC, int x, int y, [In][Out] IntNativeMethods.SIZE size);

        public static bool SetViewportExtEx(HandleRef hDC, int x, int y, [In][Out] IntNativeMethods.SIZE size)
        {
            return IntSetViewportExtEx(hDC, x, y, size);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "SetViewportOrgEx", ExactSpelling = true, SetLastError = true)]
        public static extern bool IntSetViewportOrgEx(HandleRef hDC, int x, int y, [In][Out] IntNativeMethods.POINT point);

        public static bool SetViewportOrgEx(HandleRef hDC, int x, int y, [In][Out] IntNativeMethods.POINT point)
        {
            return IntSetViewportOrgEx(hDC, x, y, point);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        public static extern int GetTextMetricsW(HandleRef hDC, [In][Out] ref IntNativeMethods.TEXTMETRIC lptm);

        [DllImport("gdi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern int GetTextMetricsA(HandleRef hDC, [In][Out] ref IntNativeMethods.TEXTMETRICA lptm);

        public static int GetTextMetrics(HandleRef hDC, ref IntNativeMethods.TEXTMETRIC lptm)
        {
            int result;
            if (Marshal.SystemDefaultCharSize == 1)
            {
                IntNativeMethods.TEXTMETRICA lptm2 = default(IntNativeMethods.TEXTMETRICA);
                result = GetTextMetricsA(hDC, ref lptm2);
                lptm.tmHeight = lptm2.tmHeight;
                lptm.tmAscent = lptm2.tmAscent;
                lptm.tmDescent = lptm2.tmDescent;
                lptm.tmInternalLeading = lptm2.tmInternalLeading;
                lptm.tmExternalLeading = lptm2.tmExternalLeading;
                lptm.tmAveCharWidth = lptm2.tmAveCharWidth;
                lptm.tmMaxCharWidth = lptm2.tmMaxCharWidth;
                lptm.tmWeight = lptm2.tmWeight;
                lptm.tmOverhang = lptm2.tmOverhang;
                lptm.tmDigitizedAspectX = lptm2.tmDigitizedAspectX;
                lptm.tmDigitizedAspectY = lptm2.tmDigitizedAspectY;
                lptm.tmFirstChar = (char)lptm2.tmFirstChar;
                lptm.tmLastChar = (char)lptm2.tmLastChar;
                lptm.tmDefaultChar = (char)lptm2.tmDefaultChar;
                lptm.tmBreakChar = (char)lptm2.tmBreakChar;
                lptm.tmItalic = lptm2.tmItalic;
                lptm.tmUnderlined = lptm2.tmUnderlined;
                lptm.tmStruckOut = lptm2.tmStruckOut;
                lptm.tmPitchAndFamily = lptm2.tmPitchAndFamily;
                lptm.tmCharSet = lptm2.tmCharSet;
            }
            else
            {
                result = GetTextMetricsW(hDC, ref lptm);
            }
            return result;
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "BeginPath", ExactSpelling = true, SetLastError = true)]
        public static extern bool IntBeginPath(HandleRef hDC);

        public static bool BeginPath(HandleRef hDC)
        {
            return IntBeginPath(hDC);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "EndPath", ExactSpelling = true, SetLastError = true)]
        public static extern bool IntEndPath(HandleRef hDC);

        public static bool EndPath(HandleRef hDC)
        {
            return IntEndPath(hDC);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "StrokePath", ExactSpelling = true, SetLastError = true)]
        public static extern bool IntStrokePath(HandleRef hDC);

        public static bool StrokePath(HandleRef hDC)
        {
            return IntStrokePath(hDC);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "AngleArc", ExactSpelling = true, SetLastError = true)]
        public static extern bool IntAngleArc(HandleRef hDC, int x, int y, int radius, float startAngle, float endAngle);

        public static bool AngleArc(HandleRef hDC, int x, int y, int radius, float startAngle, float endAngle)
        {
            return IntAngleArc(hDC, x, y, radius, startAngle, endAngle);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "Arc", ExactSpelling = true, SetLastError = true)]
        public static extern bool IntArc(HandleRef hDC, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nXStartArc, int nYStartArc, int nXEndArc, int nYEndArc);

        public static bool Arc(HandleRef hDC, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nXStartArc, int nYStartArc, int nXEndArc, int nYEndArc)
        {
            return IntArc(hDC, nLeftRect, nTopRect, nRightRect, nBottomRect, nXStartArc, nYStartArc, nXEndArc, nYEndArc);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int SetTextAlign(HandleRef hDC, int nMode);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "Ellipse", ExactSpelling = true, SetLastError = true)]
        public static extern bool IntEllipse(HandleRef hDc, int x1, int y1, int x2, int y2);

        public static bool Ellipse(HandleRef hDc, int x1, int y1, int x2, int y2)
        {
            return IntEllipse(hDc, x1, y1, x2, y2);
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern int WideCharToMultiByte(int codePage, int flags, [MarshalAs(UnmanagedType.LPWStr)] string wideStr, int chars, [In][Out] byte[] pOutBytes, int bufferBytes, IntPtr defaultChar, IntPtr pDefaultUsed);
    }
}
