// System.Windows.Forms.SafeNativeMethods
using System;
using System.Drawing;
using System.Internal;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
    [SuppressUnmanagedCodeSecurity]
    internal static class SafeNativeMethods
    {
        internal delegate bool EnumThreadWindowsCallback(IntPtr hWnd, IntPtr lParam);

        [ComImport]
        [Guid("BEF6E003-A874-101A-8BBA-00AA00300CAB")]
        [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        public interface IFontDisp
        {
            string Name { get; set; }

            long Size { get; set; }

            bool Bold { get; set; }

            bool Italic { get; set; }

            bool Underline { get; set; }

            bool Strikethrough { get; set; }

            short Weight { get; set; }

            short Charset { get; set; }
        }

        [DllImport("shlwapi.dll")]
        public static extern int SHAutoComplete(HandleRef hwndEdit, int flags);

        [DllImport("user32.dll")]
        public static extern int OemKeyScan(short wAsciiVal);

        [DllImport("gdi32.dll")]
        public static extern int GetSystemPaletteEntries(HandleRef hdc, int iStartIndex, int nEntries, byte[] lppe);

        [DllImport("gdi32.dll")]
        public static extern int GetDIBits(HandleRef hdc, HandleRef hbm, int uStartScan, int cScanLines, byte[] lpvBits, ref NativeMethods.BITMAPINFO_FLAT bmi, int uUsage);

        [DllImport("gdi32.dll")]
        public static extern int StretchDIBits(HandleRef hdc, int XDest, int YDest, int nDestWidth, int nDestHeight, int XSrc, int YSrc, int nSrcWidth, int nSrcHeight, byte[] lpBits, ref NativeMethods.BITMAPINFO_FLAT lpBitsInfo, int iUsage, int dwRop);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateCompatibleBitmap", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr IntCreateCompatibleBitmap(HandleRef hDC, int width, int height);

        public static IntPtr CreateCompatibleBitmap(HandleRef hDC, int width, int height)
        {
            return System.Internal.HandleCollector.Add(IntCreateCompatibleBitmap(hDC, width, height), NativeMethods.CommonHandles.GDI);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool GetScrollInfo(HandleRef hWnd, int fnBar, [In][Out] NativeMethods.SCROLLINFO si);

        [DllImport("ole32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool IsAccelerator(HandleRef hAccel, int cAccelEntries, [In] ref NativeMethods.MSG lpMsg, short[] lpwCmd);

        [DllImport("comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool ChooseFont([In][Out] NativeMethods.CHOOSEFONT cf);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int GetBitmapBits(HandleRef hbmp, int cbBuffer, byte[] lpvBits);

        [DllImport("comdlg32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int CommDlgExtendedError();

        [DllImport("oleaut32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern void SysFreeString(HandleRef bstr);

        [DllImport("oleaut32.dll", PreserveSig = false)]
        public static extern void OleCreatePropertyFrame(HandleRef hwndOwner, int x, int y, [MarshalAs(UnmanagedType.LPWStr)] string caption, int objects, [MarshalAs(UnmanagedType.Interface)] ref object pobjs, int pages, HandleRef pClsid, int locale, int reserved1, IntPtr reserved2);

        [DllImport("oleaut32.dll", PreserveSig = false)]
        public static extern void OleCreatePropertyFrame(HandleRef hwndOwner, int x, int y, [MarshalAs(UnmanagedType.LPWStr)] string caption, int objects, [MarshalAs(UnmanagedType.Interface)] ref object pobjs, int pages, Guid[] pClsid, int locale, int reserved1, IntPtr reserved2);

        [DllImport("oleaut32.dll", PreserveSig = false)]
        public static extern void OleCreatePropertyFrame(HandleRef hwndOwner, int x, int y, [MarshalAs(UnmanagedType.LPWStr)] string caption, int objects, HandleRef lplpobjs, int pages, HandleRef pClsid, int locale, int reserved1, IntPtr reserved2);

        [DllImport("hhctrl.ocx", CharSet = CharSet.Auto)]
        public static extern int HtmlHelp(HandleRef hwndCaller, [MarshalAs(UnmanagedType.LPTStr)] string pszFile, int uCommand, int dwData);

        [DllImport("hhctrl.ocx", CharSet = CharSet.Auto)]
        public static extern int HtmlHelp(HandleRef hwndCaller, [MarshalAs(UnmanagedType.LPTStr)] string pszFile, int uCommand, string dwData);

        [DllImport("hhctrl.ocx", CharSet = CharSet.Auto)]
        public static extern int HtmlHelp(HandleRef hwndCaller, [MarshalAs(UnmanagedType.LPTStr)] string pszFile, int uCommand, [MarshalAs(UnmanagedType.LPStruct)] NativeMethods.HH_POPUP dwData);

        [DllImport("hhctrl.ocx", CharSet = CharSet.Auto)]
        public static extern int HtmlHelp(HandleRef hwndCaller, [MarshalAs(UnmanagedType.LPTStr)] string pszFile, int uCommand, [MarshalAs(UnmanagedType.LPStruct)] NativeMethods.HH_FTS_QUERY dwData);

        [DllImport("hhctrl.ocx", CharSet = CharSet.Auto)]
        public static extern int HtmlHelp(HandleRef hwndCaller, [MarshalAs(UnmanagedType.LPTStr)] string pszFile, int uCommand, [MarshalAs(UnmanagedType.LPStruct)] NativeMethods.HH_AKLINK dwData);

        [DllImport("oleaut32.dll")]
        public static extern void VariantInit(HandleRef pObject);

        [DllImport("oleaut32.dll", PreserveSig = false)]
        public static extern void VariantClear(HandleRef pObject);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern bool LineTo(HandleRef hdc, int x, int y);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern bool MoveToEx(HandleRef hdc, int x, int y, NativeMethods.POINT pt);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern bool Rectangle(HandleRef hdc, int left, int top, int right, int bottom);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern bool PatBlt(HandleRef hdc, int left, int top, int width, int height, int rop);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, EntryPoint = "GetThreadLocale")]
        public static extern int GetThreadLCID();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int GetMessagePos();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int RegisterClipboardFormat(string format);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClipboardFormatName(int format, StringBuilder lpString, int cchMax);

        [DllImport("comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool ChooseColor([In][Out] NativeMethods.CHOOSECOLOR cc);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int RegisterWindowMessage(string msg);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "DeleteObject", ExactSpelling = true, SetLastError = true)]
        public static extern bool ExternalDeleteObject(HandleRef hObject);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "DeleteObject", ExactSpelling = true, SetLastError = true)]
        internal static extern bool IntDeleteObject(HandleRef hObject);

        public static bool DeleteObject(HandleRef hObject)
        {
            System.Internal.HandleCollector.Remove((IntPtr)hObject, NativeMethods.CommonHandles.GDI);
            return IntDeleteObject(hObject);
        }

        [DllImport("oleaut32.dll", EntryPoint = "OleCreateFontIndirect", ExactSpelling = true, PreserveSig = false)]
        public static extern IFontDisp OleCreateIFontDispIndirect(NativeMethods.FONTDESC fd, ref Guid iid);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateSolidBrush", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr IntCreateSolidBrush(int crColor);

        public static IntPtr CreateSolidBrush(int crColor)
        {
            return System.Internal.HandleCollector.Add(IntCreateSolidBrush(crColor), NativeMethods.CommonHandles.GDI);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern bool SetWindowExtEx(HandleRef hDC, int x, int y, [In][Out] NativeMethods.SIZE size);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int FormatMessage(int dwFlags, HandleRef lpSource, int dwMessageId, int dwLanguageId, StringBuilder lpBuffer, int nSize, HandleRef arguments);

        [DllImport("comctl32.dll")]
        public static extern void InitCommonControls();

        [DllImport("comctl32.dll")]
        public static extern bool InitCommonControlsEx(NativeMethods.INITCOMMONCONTROLSEX icc);

        [DllImport("comctl32.dll")]
        public static extern IntPtr ImageList_Create(int cx, int cy, int flags, int cInitial, int cGrow);

        [DllImport("comctl32.dll")]
        public static extern bool ImageList_Destroy(HandleRef himl);

        [DllImport("comctl32.dll", EntryPoint = "ImageList_Destroy")]
        public static extern bool ImageList_Destroy_Native(HandleRef himl);

        [DllImport("comctl32.dll")]
        public static extern int ImageList_GetImageCount(HandleRef himl);

        [DllImport("comctl32.dll")]
        public static extern int ImageList_Add(HandleRef himl, HandleRef hbmImage, HandleRef hbmMask);

        [DllImport("comctl32.dll")]
        public static extern int ImageList_ReplaceIcon(HandleRef himl, int index, HandleRef hicon);

        [DllImport("comctl32.dll")]
        public static extern int ImageList_SetBkColor(HandleRef himl, int clrBk);

        [DllImport("comctl32.dll")]
        public static extern bool ImageList_Draw(HandleRef himl, int i, HandleRef hdcDst, int x, int y, int fStyle);

        [DllImport("comctl32.dll")]
        public static extern bool ImageList_Replace(HandleRef himl, int i, HandleRef hbmImage, HandleRef hbmMask);

        [DllImport("comctl32.dll")]
        public static extern bool ImageList_DrawEx(HandleRef himl, int i, HandleRef hdcDst, int x, int y, int dx, int dy, int rgbBk, int rgbFg, int fStyle);

        [DllImport("comctl32.dll")]
        public static extern bool ImageList_GetIconSize(HandleRef himl, out int x, out int y);

        [DllImport("comctl32.dll")]
        public static extern IntPtr ImageList_Duplicate(HandleRef himl);

        [DllImport("comctl32.dll")]
        public static extern bool ImageList_Remove(HandleRef himl, int i);

        [DllImport("comctl32.dll")]
        public static extern bool ImageList_GetImageInfo(HandleRef himl, int i, NativeMethods.IMAGEINFO pImageInfo);

        [DllImport("comctl32.dll")]
        public static extern IntPtr ImageList_Read(UnsafeNativeMethods.IStream pstm);

        [DllImport("comctl32.dll")]
        public static extern bool ImageList_Write(HandleRef himl, UnsafeNativeMethods.IStream pstm);

        [DllImport("comctl32.dll")]
        public static extern int ImageList_WriteEx(HandleRef himl, int dwFlags, UnsafeNativeMethods.IStream pstm);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool TrackPopupMenuEx(HandleRef hmenu, int fuFlags, int x, int y, HandleRef hwnd, NativeMethods.TPMPARAMS tpm);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetKeyboardLayout(int dwLayout);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr ActivateKeyboardLayout(HandleRef hkl, int uFlags);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int GetKeyboardLayoutList(int size, [Out][MarshalAs(UnmanagedType.LPArray)] IntPtr[] hkls);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref NativeMethods.DEVMODE lpDevMode);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetMonitorInfo(HandleRef hmonitor, [In][Out] NativeMethods.MONITORINFOEX info);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern IntPtr MonitorFromPoint(NativeMethods.POINTSTRUCT pt, int flags);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern IntPtr MonitorFromRect(ref NativeMethods.RECT rect, int flags);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern IntPtr MonitorFromWindow(HandleRef handle, int flags);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern bool EnumDisplayMonitors(HandleRef hdc, NativeMethods.COMRECT rcClip, NativeMethods.MonitorEnumProc lpfnEnum, IntPtr dwData);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateHalftonePalette", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr IntCreateHalftonePalette(HandleRef hdc);

        public static IntPtr CreateHalftonePalette(HandleRef hdc)
        {
            return System.Internal.HandleCollector.Add(IntCreateHalftonePalette(hdc), NativeMethods.CommonHandles.GDI);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int GetPaletteEntries(HandleRef hpal, int iStartIndex, int nEntries, int[] lppe);

        [DllImport("gdi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        public static extern int GetTextMetricsW(HandleRef hDC, [In][Out] ref NativeMethods.TEXTMETRIC lptm);

        [DllImport("gdi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern int GetTextMetricsA(HandleRef hDC, [In][Out] ref NativeMethods.TEXTMETRICA lptm);

        public static int GetTextMetrics(HandleRef hDC, ref NativeMethods.TEXTMETRIC lptm)
        {
            if (Marshal.SystemDefaultCharSize == 1)
            {
                NativeMethods.TEXTMETRICA lptm2 = default(NativeMethods.TEXTMETRICA);
                int textMetricsA = GetTextMetricsA(hDC, ref lptm2);
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
                return textMetricsA;
            }
            return GetTextMetricsW(hDC, ref lptm);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateDIBSection", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr IntCreateDIBSection(HandleRef hdc, HandleRef pbmi, int iUsage, byte[] ppvBits, IntPtr hSection, int dwOffset);

        public static IntPtr CreateDIBSection(HandleRef hdc, HandleRef pbmi, int iUsage, byte[] ppvBits, IntPtr hSection, int dwOffset)
        {
            return System.Internal.HandleCollector.Add(IntCreateDIBSection(hdc, pbmi, iUsage, ppvBits, hSection, dwOffset), NativeMethods.CommonHandles.GDI);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateBitmap", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr IntCreateBitmap(int nWidth, int nHeight, int nPlanes, int nBitsPerPixel, IntPtr lpvBits);

        public static IntPtr CreateBitmap(int nWidth, int nHeight, int nPlanes, int nBitsPerPixel, IntPtr lpvBits)
        {
            return System.Internal.HandleCollector.Add(IntCreateBitmap(nWidth, nHeight, nPlanes, nBitsPerPixel, lpvBits), NativeMethods.CommonHandles.GDI);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateBitmap", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr IntCreateBitmapShort(int nWidth, int nHeight, int nPlanes, int nBitsPerPixel, short[] lpvBits);

        public static IntPtr CreateBitmap(int nWidth, int nHeight, int nPlanes, int nBitsPerPixel, short[] lpvBits)
        {
            return System.Internal.HandleCollector.Add(IntCreateBitmapShort(nWidth, nHeight, nPlanes, nBitsPerPixel, lpvBits), NativeMethods.CommonHandles.GDI);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateBitmap", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr IntCreateBitmapByte(int nWidth, int nHeight, int nPlanes, int nBitsPerPixel, byte[] lpvBits);

        public static IntPtr CreateBitmap(int nWidth, int nHeight, int nPlanes, int nBitsPerPixel, byte[] lpvBits)
        {
            return System.Internal.HandleCollector.Add(IntCreateBitmapByte(nWidth, nHeight, nPlanes, nBitsPerPixel, lpvBits), NativeMethods.CommonHandles.GDI);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreatePatternBrush", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr IntCreatePatternBrush(HandleRef hbmp);

        public static IntPtr CreatePatternBrush(HandleRef hbmp)
        {
            return System.Internal.HandleCollector.Add(IntCreatePatternBrush(hbmp), NativeMethods.CommonHandles.GDI);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateBrushIndirect", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr IntCreateBrushIndirect(NativeMethods.LOGBRUSH lb);

        public static IntPtr CreateBrushIndirect(NativeMethods.LOGBRUSH lb)
        {
            return System.Internal.HandleCollector.Add(IntCreateBrushIndirect(lb), NativeMethods.CommonHandles.GDI);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreatePen", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr IntCreatePen(int nStyle, int nWidth, int crColor);

        public static IntPtr CreatePen(int nStyle, int nWidth, int crColor)
        {
            return System.Internal.HandleCollector.Add(IntCreatePen(nStyle, nWidth, crColor), NativeMethods.CommonHandles.GDI);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern bool SetViewportExtEx(HandleRef hDC, int x, int y, NativeMethods.SIZE size);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr LoadCursor(HandleRef hInst, int iconId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool GetClipCursor([In][Out] ref NativeMethods.RECT lpRect);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetCursor();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool GetIconInfo(HandleRef hIcon, [In][Out] NativeMethods.ICONINFO info);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int IntersectClipRect(HandleRef hDC, int x1, int y1, int x2, int y2);

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "CopyImage", ExactSpelling = true)]
        private static extern IntPtr IntCopyImage(HandleRef hImage, int uType, int cxDesired, int cyDesired, int fuFlags);

        public static IntPtr CopyImage(HandleRef hImage, int uType, int cxDesired, int cyDesired, int fuFlags)
        {
            return System.Internal.HandleCollector.Add(IntCopyImage(hImage, uType, cxDesired, cyDesired, fuFlags), NativeMethods.CommonHandles.GDI);
        }

        public static IntPtr CopyImageAsCursor(HandleRef hImage, int uType, int cxDesired, int cyDesired, int fuFlags)
        {
            return System.Internal.HandleCollector.Add(IntCopyImage(hImage, uType, cxDesired, cyDesired, fuFlags), NativeMethods.CommonHandles.Cursor);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool AdjustWindowRectEx(ref NativeMethods.RECT lpRect, int dwStyle, bool bMenu, int dwExStyle);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool AdjustWindowRectExForDpi(ref NativeMethods.RECT lpRect, int dwStyle, bool bMenu, int dwExStyle, uint dpi);

        [DllImport("ole32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int DoDragDrop(System.Runtime.InteropServices.ComTypes.IDataObject dataObject, UnsafeNativeMethods.IOleDropSource dropSource, int allowedEffects, int[] finalEffect);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetSysColorBrush(int nIndex);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool EnableWindow(HandleRef hWnd, bool enable);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool GetClientRect(HandleRef hWnd, [In][Out] ref NativeMethods.RECT rect);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int GetDoubleClickTime();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int GetUpdateRgn(HandleRef hwnd, HandleRef hrgn, bool fErase);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool ValidateRect(HandleRef hWnd, [In][Out] ref NativeMethods.RECT rect);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int FillRect(HandleRef hdc, [In] ref NativeMethods.RECT rect, HandleRef hbrush);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int GetTextColor(HandleRef hDC);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int GetBkColor(HandleRef hDC);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int SetTextColor(HandleRef hDC, int crColor);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int SetBkColor(HandleRef hDC, int clr);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr SelectPalette(HandleRef hdc, HandleRef hpal, int bForceBackground);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern bool SetViewportOrgEx(HandleRef hDC, int x, int y, [In][Out] NativeMethods.POINT point);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateRectRgn", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr IntCreateRectRgn(int x1, int y1, int x2, int y2);

        public static IntPtr CreateRectRgn(int x1, int y1, int x2, int y2)
        {
            return System.Internal.HandleCollector.Add(IntCreateRectRgn(x1, y1, x2, y2), NativeMethods.CommonHandles.GDI);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int CombineRgn(HandleRef hRgn, HandleRef hRgn1, HandleRef hRgn2, int nCombineMode);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int RealizePalette(HandleRef hDC);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern bool LPtoDP(HandleRef hDC, [In][Out] ref NativeMethods.RECT lpRect, int nCount);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern bool SetWindowOrgEx(HandleRef hDC, int x, int y, [In][Out] NativeMethods.POINT point);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern bool GetViewportOrgEx(HandleRef hDC, [In][Out] NativeMethods.POINT point);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int SetMapMode(HandleRef hDC, int nMapMode);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool IsWindowEnabled(HandleRef hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool IsWindowVisible(HandleRef hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool ReleaseCapture();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int GetCurrentThreadId();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool EnumWindows(EnumThreadWindowsCallback callback, IntPtr extraData);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int GetWindowThreadProcessId(HandleRef hWnd, out int lpdwProcessId);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetExitCodeThread(HandleRef hWnd, out int lpdwExitCode);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool ShowWindow(HandleRef hWnd, int nCmdShow);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetWindowPos(HandleRef hWnd, HandleRef hWndInsertAfter, int x, int y, int cx, int cy, int flags);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(HandleRef hWnd);

        [DllImport("comctl32.dll", ExactSpelling = true)]
        [CLSCompliant(false)]
        private static extern bool _TrackMouseEvent(NativeMethods.TRACKMOUSEEVENT tme);

        public static bool TrackMouseEvent(NativeMethods.TRACKMOUSEEVENT tme)
        {
            return _TrackMouseEvent(tme);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool RedrawWindow(HandleRef hwnd, ref NativeMethods.RECT rcUpdate, HandleRef hrgnUpdate, int flags);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool RedrawWindow(HandleRef hwnd, NativeMethods.COMRECT rcUpdate, HandleRef hrgnUpdate, int flags);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool InvalidateRect(HandleRef hWnd, ref NativeMethods.RECT rect, bool erase);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool InvalidateRect(HandleRef hWnd, NativeMethods.COMRECT rect, bool erase);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool InvalidateRgn(HandleRef hWnd, HandleRef hrgn, bool erase);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool UpdateWindow(HandleRef hWnd);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int GetCurrentProcessId();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int ScrollWindowEx(HandleRef hWnd, int nXAmount, int nYAmount, NativeMethods.COMRECT rectScrollRegion, ref NativeMethods.RECT rectClip, HandleRef hrgnUpdate, ref NativeMethods.RECT prcUpdate, int flags);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int GetThreadLocale();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool MessageBeep(int type);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool DrawMenuBar(HandleRef hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool IsChild(HandleRef parent, HandleRef child);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr SetTimer(HandleRef hWnd, int nIDEvent, int uElapse, IntPtr lpTimerFunc);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool KillTimer(HandleRef hwnd, int idEvent);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int MessageBox(HandleRef hWnd, string text, string caption, int type);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr SelectObject(HandleRef hDC, HandleRef hObject);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int GetTickCount();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool ScrollWindow(HandleRef hWnd, int nXAmount, int nYAmount, ref NativeMethods.RECT rectScrollRegion, ref NativeMethods.RECT rectClip);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetCurrentProcess();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetCurrentThread();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetThreadLocale(int Locale);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool IsWindowUnicode(HandleRef hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool DrawEdge(HandleRef hDC, ref NativeMethods.RECT rect, int edge, int flags);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool DrawFrameControl(HandleRef hDC, ref NativeMethods.RECT rect, int type, int state);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int GetClipRgn(HandleRef hDC, HandleRef hRgn);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int GetRgnBox(HandleRef hRegion, ref NativeMethods.RECT clipRect);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int SelectClipRgn(HandleRef hDC, HandleRef hRgn);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int SetROP2(HandleRef hDC, int nDrawMode);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool DrawIcon(HandleRef hDC, int x, int y, HandleRef hIcon);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool DrawIconEx(HandleRef hDC, int x, int y, HandleRef hIcon, int width, int height, int iStepIfAniCursor, HandleRef hBrushFlickerFree, int diFlags);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int SetBkMode(HandleRef hDC, int nBkMode);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern bool BitBlt(HandleRef hDC, int x, int y, int nWidth, int nHeight, HandleRef hSrcDC, int xSrc, int ySrc, int dwRop);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool ShowCaret(HandleRef hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool HideCaret(HandleRef hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern uint GetCaretBlinkTime();

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern bool IsAppThemed();

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int GetThemeAppProperties();

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern void SetThemeAppProperties(int Flags);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr OpenThemeData(HandleRef hwnd, [MarshalAs(UnmanagedType.LPWStr)] string pszClassList);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int CloseThemeData(HandleRef hTheme);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int GetCurrentThemeName(StringBuilder pszThemeFileName, int dwMaxNameChars, StringBuilder pszColorBuff, int dwMaxColorChars, StringBuilder pszSizeBuff, int cchMaxSizeChars);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern bool IsThemePartDefined(HandleRef hTheme, int iPartId, int iStateId);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int DrawThemeBackground(HandleRef hTheme, HandleRef hdc, int partId, int stateId, [In] NativeMethods.COMRECT pRect, [In] NativeMethods.COMRECT pClipRect);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int DrawThemeEdge(HandleRef hTheme, HandleRef hdc, int iPartId, int iStateId, [In] NativeMethods.COMRECT pDestRect, int uEdge, int uFlags, [Out] NativeMethods.COMRECT pContentRect);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int DrawThemeParentBackground(HandleRef hwnd, HandleRef hdc, [In] NativeMethods.COMRECT prc);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int DrawThemeText(HandleRef hTheme, HandleRef hdc, int iPartId, int iStateId, [MarshalAs(UnmanagedType.LPWStr)] string pszText, int iCharCount, int dwTextFlags, int dwTextFlags2, [In] NativeMethods.COMRECT pRect);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int GetThemeBackgroundContentRect(HandleRef hTheme, HandleRef hdc, int iPartId, int iStateId, [In] NativeMethods.COMRECT pBoundingRect, [Out] NativeMethods.COMRECT pContentRect);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int GetThemeBackgroundExtent(HandleRef hTheme, HandleRef hdc, int iPartId, int iStateId, [In] NativeMethods.COMRECT pContentRect, [Out] NativeMethods.COMRECT pExtentRect);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int GetThemeBackgroundRegion(HandleRef hTheme, HandleRef hdc, int iPartId, int iStateId, [In] NativeMethods.COMRECT pRect, ref IntPtr pRegion);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int GetThemeBool(HandleRef hTheme, int iPartId, int iStateId, int iPropId, ref bool pfVal);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int GetThemeColor(HandleRef hTheme, int iPartId, int iStateId, int iPropId, ref int pColor);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int GetThemeEnumValue(HandleRef hTheme, int iPartId, int iStateId, int iPropId, ref int piVal);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int GetThemeFilename(HandleRef hTheme, int iPartId, int iStateId, int iPropId, StringBuilder pszThemeFilename, int cchMaxBuffChars);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int GetThemeFont(HandleRef hTheme, HandleRef hdc, int iPartId, int iStateId, int iPropId, NativeMethods.LOGFONT pFont);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int GetThemeInt(HandleRef hTheme, int iPartId, int iStateId, int iPropId, ref int piVal);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int GetThemePartSize(HandleRef hTheme, HandleRef hdc, int iPartId, int iStateId, [In] NativeMethods.COMRECT prc, ThemeSizeType eSize, [Out] NativeMethods.SIZE psz);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int GetThemePosition(HandleRef hTheme, int iPartId, int iStateId, int iPropId, [Out] NativeMethods.POINT pPoint);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int GetThemeMargins(HandleRef hTheme, HandleRef hDC, int iPartId, int iStateId, int iPropId, ref NativeMethods.MARGINS margins);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int GetThemeString(HandleRef hTheme, int iPartId, int iStateId, int iPropId, StringBuilder pszBuff, int cchMaxBuffChars);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int GetThemeDocumentationProperty([MarshalAs(UnmanagedType.LPWStr)] string pszThemeName, [MarshalAs(UnmanagedType.LPWStr)] string pszPropertyName, StringBuilder pszValueBuff, int cchMaxValChars);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int GetThemeTextExtent(HandleRef hTheme, HandleRef hdc, int iPartId, int iStateId, [MarshalAs(UnmanagedType.LPWStr)] string pszText, int iCharCount, int dwTextFlags, [In] NativeMethods.COMRECT pBoundingRect, [Out] NativeMethods.COMRECT pExtentRect);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int GetThemeTextMetrics(HandleRef hTheme, HandleRef hdc, int iPartId, int iStateId, ref TextMetrics ptm);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int HitTestThemeBackground(HandleRef hTheme, HandleRef hdc, int iPartId, int iStateId, int dwOptions, [In] NativeMethods.COMRECT pRect, HandleRef hrgn, [In] NativeMethods.POINTSTRUCT ptTest, ref int pwHitTestCode);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern bool IsThemeBackgroundPartiallyTransparent(HandleRef hTheme, int iPartId, int iStateId);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern bool GetThemeSysBool(HandleRef hTheme, int iBoolId);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int GetThemeSysInt(HandleRef hTheme, int iIntId, ref int piValue);

        [DllImport("user32.dll")]
        public static extern IntPtr OpenInputDesktop(int dwFlags, [MarshalAs(UnmanagedType.Bool)] bool fInherit, int dwDesiredAccess);

        [DllImport("user32.dll")]
        public static extern bool CloseDesktop(IntPtr hDesktop);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetProcessDPIAware();

        [DllImport("SHCore.dll", SetLastError = true)]
        public static extern int SetProcessDpiAwareness(NativeMethods.PROCESS_DPI_AWARENESS awareness);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetProcessDpiAwarenessContext(int dpiFlag);

        [DllImport("user32.dll")]
        public static extern int GetThreadDpiAwarenessContext();

        [DllImport("user32.dll")]
        public static extern bool AreDpiAwarenessContextsEqual(int dpiContextA, int dpiContextB);

        public static int RGBToCOLORREF(int rgbValue)
        {
            int num = (rgbValue & 0xFF) << 16;
            rgbValue &= 0xFFFF00;
            rgbValue |= (rgbValue >> 16) & 0xFF;
            rgbValue &= 0xFFFF;
            rgbValue |= num;
            return rgbValue;
        }

        public static Color ColorFromCOLORREF(int colorref)
        {
            int red = colorref & 0xFF;
            int green = (colorref >> 8) & 0xFF;
            int blue = (colorref >> 16) & 0xFF;
            return Color.FromArgb(red, green, blue);
        }

        public static int ColorToCOLORREF(Color color)
        {
            return color.R | (color.G << 8) | (color.B << 16);
        }
    }
}
