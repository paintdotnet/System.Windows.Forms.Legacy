// System.Windows.Forms.NativeMethods
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#pragma warning disable 0649

namespace System.Windows.Forms
{
    internal static class NativeMethods
    {
        public enum RegionFlags
        {
            ERROR,
            NULLREGION,
            SIMPLEREGION,
            COMPLEXREGION
        }

        [StructLayout(LayoutKind.Sequential)]
        [CLSCompliant(false)]
        public class OLECMD
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cmdID;

            [MarshalAs(UnmanagedType.U4)]
            public int cmdf;
        }

        [ComImport]
        [ComVisible(true)]
        [Guid("B722BCCB-4E68-101B-A2BC-00AA00404770")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [CLSCompliant(false)]
        public interface IOleCommandTarget
        {
            [PreserveSig]
            [return: MarshalAs(UnmanagedType.I4)]
            int QueryStatus(ref Guid pguidCmdGroup, int cCmds, [In][Out] OLECMD prgCmds, [In][Out] IntPtr pCmdText);

            [PreserveSig]
            [return: MarshalAs(UnmanagedType.I4)]
            int Exec(ref Guid pguidCmdGroup, int nCmdID, int nCmdexecopt, [In][MarshalAs(UnmanagedType.LPArray)] object[] pvaIn, int pvaOut);
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public class FONTDESC
        {
            public int cbSizeOfStruct = Marshal.SizeOf(typeof(FONTDESC));

            public string lpstrName;

            public long cySize;

            public short sWeight;

            public short sCharset;

            public bool fItalic;

            public bool fUnderline;

            public bool fStrikethrough;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class PICTDESCbmp
        {
            internal int cbSizeOfStruct = Marshal.SizeOf(typeof(PICTDESCbmp));

            internal int picType = 1;

            internal IntPtr hbitmap = IntPtr.Zero;

            internal IntPtr hpalette = IntPtr.Zero;

            internal int unused;

            public PICTDESCbmp(Bitmap bitmap)
            {
                hbitmap = bitmap.GetHbitmap();
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public class PICTDESCicon
        {
            internal int cbSizeOfStruct = Marshal.SizeOf(typeof(PICTDESCicon));

            internal int picType = 3;

            internal IntPtr hicon = IntPtr.Zero;

            internal int unused1;

            internal int unused2;

            public PICTDESCicon(Icon icon)
            {
                hicon = SafeNativeMethods.CopyImage(new HandleRef(icon, icon.Handle), 1, icon.Size.Width, icon.Size.Height, 0);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public class PICTDESCemf
        {
            internal int cbSizeOfStruct = Marshal.SizeOf(typeof(PICTDESCemf));

            internal int picType = 4;

            internal IntPtr hemf = IntPtr.Zero;

            internal int unused1;

            internal int unused2;

            public PICTDESCemf(Metafile metafile)
            {
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public class USEROBJECTFLAGS
        {
            public int fInherit;

            public int fReserved;

            public int dwFlags;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal class SYSTEMTIMEARRAY
        {
            public short wYear1;

            public short wMonth1;

            public short wDayOfWeek1;

            public short wDay1;

            public short wHour1;

            public short wMinute1;

            public short wSecond1;

            public short wMilliseconds1;

            public short wYear2;

            public short wMonth2;

            public short wDayOfWeek2;

            public short wDay2;

            public short wHour2;

            public short wMinute2;

            public short wSecond2;

            public short wMilliseconds2;
        }

        public delegate bool EnumChildrenCallback(IntPtr hwnd, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class HH_AKLINK
        {
            internal int cbStruct = Marshal.SizeOf(typeof(HH_AKLINK));

            internal bool fReserved;

            internal string pszKeywords;

            internal string pszUrl;

            internal string pszMsgText;

            internal string pszMsgTitle;

            internal string pszWindow;

            internal bool fIndexOnFail;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class HH_POPUP
        {
            internal int cbStruct = Marshal.SizeOf(typeof(HH_POPUP));

            internal IntPtr hinst = IntPtr.Zero;

            internal int idString;

            internal IntPtr pszText;

            internal POINT pt;

            internal int clrForeground = -1;

            internal int clrBackground = -1;

            internal RECT rcMargins = RECT.FromXYWH(-1, -1, -1, -1);

            internal string pszFont;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class HH_FTS_QUERY
        {
            internal int cbStruct = Marshal.SizeOf(typeof(HH_FTS_QUERY));

            internal bool fUniCodeStrings;

            [MarshalAs(UnmanagedType.LPStr)]
            internal string pszSearchQuery;

            internal int iProximity = -1;

            internal bool fStemmedSearch;

            internal bool fTitleOnly;

            internal bool fExecute = true;

            [MarshalAs(UnmanagedType.LPStr)]
            internal string pszWindow;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
        public class MONITORINFOEX
        {
            internal int cbSize = Marshal.SizeOf(typeof(MONITORINFOEX));

            internal RECT rcMonitor;

            internal RECT rcWork;

            internal int dwFlags;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            internal char[] szDevice = new char[32];
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
        public class MONITORINFO
        {
            internal int cbSize = Marshal.SizeOf(typeof(MONITORINFO));

            internal RECT rcMonitor;

            internal RECT rcWork;

            internal int dwFlags;
        }

        public delegate int EditStreamCallback(IntPtr dwCookie, IntPtr buf, int cb, out int transferred);

        [StructLayout(LayoutKind.Sequential)]
        public class EDITSTREAM
        {
            public IntPtr dwCookie = IntPtr.Zero;

            public int dwError;

            public EditStreamCallback pfnCallback;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class EDITSTREAM64
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] contents = new byte[20];
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct DEVMODE
        {
            private const int CCHDEVICENAME = 32;

            private const int CCHFORMNAME = 32;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmDeviceName;

            public short dmSpecVersion;

            public short dmDriverVersion;

            public short dmSize;

            public short dmDriverExtra;

            public int dmFields;

            public int dmPositionX;

            public int dmPositionY;

            public ScreenOrientation dmDisplayOrientation;

            public int dmDisplayFixedOutput;

            public short dmColor;

            public short dmDuplex;

            public short dmYResolution;

            public short dmTTOption;

            public short dmCollate;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmFormName;

            public short dmLogPixels;

            public int dmBitsPerPel;

            public int dmPelsWidth;

            public int dmPelsHeight;

            public int dmDisplayFlags;

            public int dmDisplayFrequency;

            public int dmICMMethod;

            public int dmICMIntent;

            public int dmMediaType;

            public int dmDitherType;

            public int dmReserved1;

            public int dmReserved2;

            public int dmPanningWidth;

            public int dmPanningHeight;
        }

        [ComImport]
        [Guid("0FF510A3-5FA5-49F1-8CCC-190D71083F3E")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IVsPerPropertyBrowsing
        {
            [PreserveSig]
            int HideProperty(int dispid, ref bool pfHide);

            [PreserveSig]
            int DisplayChildProperties(int dispid, ref bool pfDisplay);

            [PreserveSig]
            int GetLocalizedPropertyInfo(int dispid, int localeID, [Out][MarshalAs(UnmanagedType.LPArray)] string[] pbstrLocalizedName, [Out][MarshalAs(UnmanagedType.LPArray)] string[] pbstrLocalizeDescription);

            [PreserveSig]
            int HasDefaultValue(int dispid, ref bool fDefault);

            [PreserveSig]
            int IsPropertyReadOnly(int dispid, ref bool fReadOnly);

            [PreserveSig]
            int GetClassName([In][Out] ref string pbstrClassName);

            [PreserveSig]
            int CanResetPropertyValue(int dispid, [In][Out] ref bool pfCanReset);

            [PreserveSig]
            int ResetPropertyValue(int dispid);
        }

        [ComImport]
        [Guid("7494683C-37A0-11d2-A273-00C04F8EF4FF")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IManagedPerPropertyBrowsing
        {
            [PreserveSig]
            int GetPropertyAttributes(int dispid, ref int pcAttributes, ref IntPtr pbstrAttrNames, ref IntPtr pvariantInitValues);
        }

        [ComImport]
        [Guid("33C0C1D8-33CF-11d3-BFF2-00C04F990235")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IProvidePropertyBuilder
        {
            [PreserveSig]
            int MapPropertyToBuilder(int dispid, [In][Out][MarshalAs(UnmanagedType.LPArray)] int[] pdwCtlBldType, [In][Out][MarshalAs(UnmanagedType.LPArray)] string[] pbstrGuidBldr, [In][Out][MarshalAs(UnmanagedType.Bool)] ref bool builderAvailable);

            [PreserveSig]
            int ExecuteBuilder(int dispid, [In][MarshalAs(UnmanagedType.BStr)] string bstrGuidBldr, [In][MarshalAs(UnmanagedType.Interface)] object pdispApp, HandleRef hwndBldrOwner, [In][Out][MarshalAs(UnmanagedType.Struct)] ref object pvarValue, [In][Out][MarshalAs(UnmanagedType.Bool)] ref bool actionCommitted);
        }

        [StructLayout(LayoutKind.Sequential)]
        public class INITCOMMONCONTROLSEX
        {
            public int dwSize = 8;

            public int dwICC;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class IMAGELISTDRAWPARAMS
        {
            public int cbSize = Marshal.SizeOf(typeof(IMAGELISTDRAWPARAMS));

            public IntPtr himl = IntPtr.Zero;

            public int i;

            public IntPtr hdcDst = IntPtr.Zero;

            public int x;

            public int y;

            public int cx;

            public int cy;

            public int xBitmap;

            public int yBitmap;

            public int rgbBk;

            public int rgbFg;

            public int fStyle;

            public int dwRop;

            public int fState;

            public int Frame;

            public int crEffect;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class IMAGEINFO
        {
            public IntPtr hbmImage = IntPtr.Zero;

            public IntPtr hbmMask = IntPtr.Zero;

            public int Unused1;

            public int Unused2;

            public int rcImage_left;

            public int rcImage_top;

            public int rcImage_right;

            public int rcImage_bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class TRACKMOUSEEVENT
        {
            public int cbSize = Marshal.SizeOf(typeof(TRACKMOUSEEVENT));

            public int dwFlags;

            public IntPtr hwndTrack;

            public int dwHoverTime = 100;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class POINT
        {
            public int x;

            public int y;

            public POINT()
            {
            }

            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public struct POINTSTRUCT
        {
            public int x;

            public int y;

            public POINTSTRUCT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public delegate IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        public struct RECT
        {
            public int left;

            public int top;

            public int right;

            public int bottom;

            public Size Size => new Size(right - left, bottom - top);

            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public RECT(Rectangle r)
            {
                left = r.Left;
                top = r.Top;
                right = r.Right;
                bottom = r.Bottom;
            }

            public static RECT FromXYWH(int x, int y, int width, int height)
            {
                return new RECT(x, y, x + width, y + height);
            }
        }

        public struct MARGINS
        {
            public int cxLeftWidth;

            public int cxRightWidth;

            public int cyTopHeight;

            public int cyBottomHeight;
        }

        public delegate int ListViewCompareCallback(IntPtr lParam1, IntPtr lParam2, IntPtr lParamSort);

        public delegate int TreeViewCompareCallback(IntPtr lParam1, IntPtr lParam2, IntPtr lParamSort);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class WNDCLASS_I
        {
            public int style;

            public IntPtr lpfnWndProc = IntPtr.Zero;

            public int cbClsExtra;

            public int cbWndExtra;

            public IntPtr hInstance = IntPtr.Zero;

            public IntPtr hIcon = IntPtr.Zero;

            public IntPtr hCursor = IntPtr.Zero;

            public IntPtr hbrBackground = IntPtr.Zero;

            public IntPtr lpszMenuName = IntPtr.Zero;

            public IntPtr lpszClassName = IntPtr.Zero;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class NONCLIENTMETRICS
        {
            public int cbSize = Marshal.SizeOf(typeof(NONCLIENTMETRICS));

            public int iBorderWidth;

            public int iScrollWidth;

            public int iScrollHeight;

            public int iCaptionWidth;

            public int iCaptionHeight;

            [MarshalAs(UnmanagedType.Struct)]
            public LOGFONT lfCaptionFont;

            public int iSmCaptionWidth;

            public int iSmCaptionHeight;

            [MarshalAs(UnmanagedType.Struct)]
            public LOGFONT lfSmCaptionFont;

            public int iMenuWidth;

            public int iMenuHeight;

            [MarshalAs(UnmanagedType.Struct)]
            public LOGFONT lfMenuFont;

            [MarshalAs(UnmanagedType.Struct)]
            public LOGFONT lfStatusFont;

            [MarshalAs(UnmanagedType.Struct)]
            public LOGFONT lfMessageFont;

            public int iPaddedBorderWidth;
        }

        [Serializable]
        public struct MSG
        {
            public IntPtr hwnd;

            public int message;

            public IntPtr wParam;

            public IntPtr lParam;

            public int time;

            public int pt_x;

            public int pt_y;
        }

        public struct PAINTSTRUCT
        {
            public IntPtr hdc;

            public bool fErase;

            public int rcPaint_left;

            public int rcPaint_top;

            public int rcPaint_right;

            public int rcPaint_bottom;

            public bool fRestore;

            public bool fIncUpdate;

            public int reserved1;

            public int reserved2;

            public int reserved3;

            public int reserved4;

            public int reserved5;

            public int reserved6;

            public int reserved7;

            public int reserved8;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class SCROLLINFO
        {
            public int cbSize = Marshal.SizeOf(typeof(SCROLLINFO));

            public int fMask;

            public int nMin;

            public int nMax;

            public int nPage;

            public int nPos;

            public int nTrackPos;

            public SCROLLINFO()
            {
            }

            public SCROLLINFO(int mask, int min, int max, int page, int pos)
            {
                fMask = mask;
                nMin = min;
                nMax = max;
                nPage = page;
                nPos = pos;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public class TPMPARAMS
        {
            public int cbSize = Marshal.SizeOf(typeof(TPMPARAMS));

            public int rcExclude_left;

            public int rcExclude_top;

            public int rcExclude_right;

            public int rcExclude_bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class SIZE
        {
            public int cx;

            public int cy;

            public SIZE()
            {
            }

            public SIZE(int cx, int cy)
            {
                this.cx = cx;
                this.cy = cy;
            }
        }

        public struct WINDOWPLACEMENT
        {
            public int length;

            public int flags;

            public int showCmd;

            public int ptMinPosition_x;

            public int ptMinPosition_y;

            public int ptMaxPosition_x;

            public int ptMaxPosition_y;

            public int rcNormalPosition_left;

            public int rcNormalPosition_top;

            public int rcNormalPosition_right;

            public int rcNormalPosition_bottom;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class STARTUPINFO_I
        {
            public int cb;

            public IntPtr lpReserved = IntPtr.Zero;

            public IntPtr lpDesktop = IntPtr.Zero;

            public IntPtr lpTitle = IntPtr.Zero;

            public int dwX;

            public int dwY;

            public int dwXSize;

            public int dwYSize;

            public int dwXCountChars;

            public int dwYCountChars;

            public int dwFillAttribute;

            public int dwFlags;

            public short wShowWindow;

            public short cbReserved2;

            public IntPtr lpReserved2 = IntPtr.Zero;

            public IntPtr hStdInput = IntPtr.Zero;

            public IntPtr hStdOutput = IntPtr.Zero;

            public IntPtr hStdError = IntPtr.Zero;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class PAGESETUPDLG
        {
            public int lStructSize;

            public IntPtr hwndOwner;

            public IntPtr hDevMode;

            public IntPtr hDevNames;

            public int Flags;

            public int paperSizeX;

            public int paperSizeY;

            public int minMarginLeft;

            public int minMarginTop;

            public int minMarginRight;

            public int minMarginBottom;

            public int marginLeft;

            public int marginTop;

            public int marginRight;

            public int marginBottom;

            public IntPtr hInstance = IntPtr.Zero;

            public IntPtr lCustData = IntPtr.Zero;

            public WndProc lpfnPageSetupHook;

            public WndProc lpfnPagePaintHook;

            public string lpPageSetupTemplateName;

            public IntPtr hPageSetupTemplate = IntPtr.Zero;
        }

        public interface PRINTDLG
        {
            int lStructSize { get; set; }

            IntPtr hwndOwner { get; set; }

            IntPtr hDevMode { get; set; }

            IntPtr hDevNames { get; set; }

            IntPtr hDC { get; set; }

            int Flags { get; set; }

            short nFromPage { get; set; }

            short nToPage { get; set; }

            short nMinPage { get; set; }

            short nMaxPage { get; set; }

            short nCopies { get; set; }

            IntPtr hInstance { get; set; }

            IntPtr lCustData { get; set; }

            WndProc lpfnPrintHook { get; set; }

            WndProc lpfnSetupHook { get; set; }

            string lpPrintTemplateName { get; set; }

            string lpSetupTemplateName { get; set; }

            IntPtr hPrintTemplate { get; set; }

            IntPtr hSetupTemplate { get; set; }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
        public class PRINTDLG_32 : PRINTDLG
        {
            private int m_lStructSize;

            private IntPtr m_hwndOwner;

            private IntPtr m_hDevMode;

            private IntPtr m_hDevNames;

            private IntPtr m_hDC;

            private int m_Flags;

            private short m_nFromPage;

            private short m_nToPage;

            private short m_nMinPage;

            private short m_nMaxPage;

            private short m_nCopies;

            private IntPtr m_hInstance;

            private IntPtr m_lCustData;

            private WndProc m_lpfnPrintHook;

            private WndProc m_lpfnSetupHook;

            private string m_lpPrintTemplateName;

            private string m_lpSetupTemplateName;

            private IntPtr m_hPrintTemplate;

            private IntPtr m_hSetupTemplate;

            public int lStructSize
            {
                get
                {
                    return m_lStructSize;
                }
                set
                {
                    m_lStructSize = value;
                }
            }

            public IntPtr hwndOwner
            {
                get
                {
                    return m_hwndOwner;
                }
                set
                {
                    m_hwndOwner = value;
                }
            }

            public IntPtr hDevMode
            {
                get
                {
                    return m_hDevMode;
                }
                set
                {
                    m_hDevMode = value;
                }
            }

            public IntPtr hDevNames
            {
                get
                {
                    return m_hDevNames;
                }
                set
                {
                    m_hDevNames = value;
                }
            }

            public IntPtr hDC
            {
                get
                {
                    return m_hDC;
                }
                set
                {
                    m_hDC = value;
                }
            }

            public int Flags
            {
                get
                {
                    return m_Flags;
                }
                set
                {
                    m_Flags = value;
                }
            }

            public short nFromPage
            {
                get
                {
                    return m_nFromPage;
                }
                set
                {
                    m_nFromPage = value;
                }
            }

            public short nToPage
            {
                get
                {
                    return m_nToPage;
                }
                set
                {
                    m_nToPage = value;
                }
            }

            public short nMinPage
            {
                get
                {
                    return m_nMinPage;
                }
                set
                {
                    m_nMinPage = value;
                }
            }

            public short nMaxPage
            {
                get
                {
                    return m_nMaxPage;
                }
                set
                {
                    m_nMaxPage = value;
                }
            }

            public short nCopies
            {
                get
                {
                    return m_nCopies;
                }
                set
                {
                    m_nCopies = value;
                }
            }

            public IntPtr hInstance
            {
                get
                {
                    return m_hInstance;
                }
                set
                {
                    m_hInstance = value;
                }
            }

            public IntPtr lCustData
            {
                get
                {
                    return m_lCustData;
                }
                set
                {
                    m_lCustData = value;
                }
            }

            public WndProc lpfnPrintHook
            {
                get
                {
                    return m_lpfnPrintHook;
                }
                set
                {
                    m_lpfnPrintHook = value;
                }
            }

            public WndProc lpfnSetupHook
            {
                get
                {
                    return m_lpfnSetupHook;
                }
                set
                {
                    m_lpfnSetupHook = value;
                }
            }

            public string lpPrintTemplateName
            {
                get
                {
                    return m_lpPrintTemplateName;
                }
                set
                {
                    m_lpPrintTemplateName = value;
                }
            }

            public string lpSetupTemplateName
            {
                get
                {
                    return m_lpSetupTemplateName;
                }
                set
                {
                    m_lpSetupTemplateName = value;
                }
            }

            public IntPtr hPrintTemplate
            {
                get
                {
                    return m_hPrintTemplate;
                }
                set
                {
                    m_hPrintTemplate = value;
                }
            }

            public IntPtr hSetupTemplate
            {
                get
                {
                    return m_hSetupTemplate;
                }
                set
                {
                    m_hSetupTemplate = value;
                }
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class PRINTDLG_64 : PRINTDLG
        {
            private int m_lStructSize;

            private IntPtr m_hwndOwner;

            private IntPtr m_hDevMode;

            private IntPtr m_hDevNames;

            private IntPtr m_hDC;

            private int m_Flags;

            private short m_nFromPage;

            private short m_nToPage;

            private short m_nMinPage;

            private short m_nMaxPage;

            private short m_nCopies;

            private IntPtr m_hInstance;

            private IntPtr m_lCustData;

            private WndProc m_lpfnPrintHook;

            private WndProc m_lpfnSetupHook;

            private string m_lpPrintTemplateName;

            private string m_lpSetupTemplateName;

            private IntPtr m_hPrintTemplate;

            private IntPtr m_hSetupTemplate;

            public int lStructSize
            {
                get
                {
                    return m_lStructSize;
                }
                set
                {
                    m_lStructSize = value;
                }
            }

            public IntPtr hwndOwner
            {
                get
                {
                    return m_hwndOwner;
                }
                set
                {
                    m_hwndOwner = value;
                }
            }

            public IntPtr hDevMode
            {
                get
                {
                    return m_hDevMode;
                }
                set
                {
                    m_hDevMode = value;
                }
            }

            public IntPtr hDevNames
            {
                get
                {
                    return m_hDevNames;
                }
                set
                {
                    m_hDevNames = value;
                }
            }

            public IntPtr hDC
            {
                get
                {
                    return m_hDC;
                }
                set
                {
                    m_hDC = value;
                }
            }

            public int Flags
            {
                get
                {
                    return m_Flags;
                }
                set
                {
                    m_Flags = value;
                }
            }

            public short nFromPage
            {
                get
                {
                    return m_nFromPage;
                }
                set
                {
                    m_nFromPage = value;
                }
            }

            public short nToPage
            {
                get
                {
                    return m_nToPage;
                }
                set
                {
                    m_nToPage = value;
                }
            }

            public short nMinPage
            {
                get
                {
                    return m_nMinPage;
                }
                set
                {
                    m_nMinPage = value;
                }
            }

            public short nMaxPage
            {
                get
                {
                    return m_nMaxPage;
                }
                set
                {
                    m_nMaxPage = value;
                }
            }

            public short nCopies
            {
                get
                {
                    return m_nCopies;
                }
                set
                {
                    m_nCopies = value;
                }
            }

            public IntPtr hInstance
            {
                get
                {
                    return m_hInstance;
                }
                set
                {
                    m_hInstance = value;
                }
            }

            public IntPtr lCustData
            {
                get
                {
                    return m_lCustData;
                }
                set
                {
                    m_lCustData = value;
                }
            }

            public WndProc lpfnPrintHook
            {
                get
                {
                    return m_lpfnPrintHook;
                }
                set
                {
                    m_lpfnPrintHook = value;
                }
            }

            public WndProc lpfnSetupHook
            {
                get
                {
                    return m_lpfnSetupHook;
                }
                set
                {
                    m_lpfnSetupHook = value;
                }
            }

            public string lpPrintTemplateName
            {
                get
                {
                    return m_lpPrintTemplateName;
                }
                set
                {
                    m_lpPrintTemplateName = value;
                }
            }

            public string lpSetupTemplateName
            {
                get
                {
                    return m_lpSetupTemplateName;
                }
                set
                {
                    m_lpSetupTemplateName = value;
                }
            }

            public IntPtr hPrintTemplate
            {
                get
                {
                    return m_hPrintTemplate;
                }
                set
                {
                    m_hPrintTemplate = value;
                }
            }

            public IntPtr hSetupTemplate
            {
                get
                {
                    return m_hSetupTemplate;
                }
                set
                {
                    m_hSetupTemplate = value;
                }
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class PRINTDLGEX
        {
            public int lStructSize;

            public IntPtr hwndOwner;

            public IntPtr hDevMode;

            public IntPtr hDevNames;

            public IntPtr hDC;

            public int Flags;

            public int Flags2;

            public int ExclusionFlags;

            public int nPageRanges;

            public int nMaxPageRanges;

            public IntPtr pageRanges;

            public int nMinPage;

            public int nMaxPage;

            public int nCopies;

            public IntPtr hInstance;

            [MarshalAs(UnmanagedType.LPStr)]
            public string lpPrintTemplateName;

            public WndProc lpCallback;

            public int nPropertyPages;

            public IntPtr lphPropertyPages;

            public int nStartPage;

            public int dwResultAction;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
        public class PRINTPAGERANGE
        {
            public int nFromPage;

            public int nToPage;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class PICTDESC
        {
            internal int cbSizeOfStruct;

            public int picType;

            internal IntPtr union1;

            internal int union2;

            internal int union3;

            public static PICTDESC CreateBitmapPICTDESC(IntPtr hbitmap, IntPtr hpal)
            {
                PICTDESC pICTDESC = new PICTDESC();
                pICTDESC.cbSizeOfStruct = 16;
                pICTDESC.picType = 1;
                pICTDESC.union1 = hbitmap;
                pICTDESC.union2 = (int)((long)hpal & 0xFFFFFFFFu);
                pICTDESC.union3 = (int)((long)hpal >> 32);
                return pICTDESC;
            }

            public static PICTDESC CreateIconPICTDESC(IntPtr hicon)
            {
                PICTDESC pICTDESC = new PICTDESC();
                pICTDESC.cbSizeOfStruct = 12;
                pICTDESC.picType = 3;
                pICTDESC.union1 = hicon;
                return pICTDESC;
            }

            public virtual IntPtr GetHandle()
            {
                return union1;
            }

            public virtual IntPtr GetHPal()
            {
                if (picType == 1)
                {
                    return (IntPtr)((uint)union2 | ((long)union3 << 32));
                }
                return IntPtr.Zero;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public sealed class tagFONTDESC
        {
            public int cbSizeofstruct = Marshal.SizeOf(typeof(tagFONTDESC));

            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpstrName;

            [MarshalAs(UnmanagedType.U8)]
            public long cySize;

            [MarshalAs(UnmanagedType.U2)]
            public short sWeight;

            [MarshalAs(UnmanagedType.U2)]
            public short sCharset;

            [MarshalAs(UnmanagedType.Bool)]
            public bool fItalic;

            [MarshalAs(UnmanagedType.Bool)]
            public bool fUnderline;

            [MarshalAs(UnmanagedType.Bool)]
            public bool fStrikethrough;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class CHOOSECOLOR
        {
            public int lStructSize = Marshal.SizeOf(typeof(CHOOSECOLOR));

            public IntPtr hwndOwner;

            public IntPtr hInstance;

            public int rgbResult;

            public IntPtr lpCustColors;

            public int Flags;

            public IntPtr lCustData = IntPtr.Zero;

            public WndProc lpfnHook;

            public string lpTemplateName;
        }

        public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        public class BITMAP
        {
            public int bmType;

            public int bmWidth;

            public int bmHeight;

            public int bmWidthBytes;

            public short bmPlanes;

            public short bmBitsPixel;

            public IntPtr bmBits = IntPtr.Zero;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class ICONINFO
        {
            public int fIcon;

            public int xHotspot;

            public int yHotspot;

            public IntPtr hbmMask = IntPtr.Zero;

            public IntPtr hbmColor = IntPtr.Zero;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class LOGPEN
        {
            public int lopnStyle;

            public int lopnWidth_x;

            public int lopnWidth_y;

            public int lopnColor;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class LOGBRUSH
        {
            public int lbStyle;

            public int lbColor;

            public IntPtr lbHatch;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class LOGFONT
        {
            public int lfHeight;

            public int lfWidth;

            public int lfEscapement;

            public int lfOrientation;

            public int lfWeight;

            public byte lfItalic;

            public byte lfUnderline;

            public byte lfStrikeOut;

            public byte lfCharSet;

            public byte lfOutPrecision;

            public byte lfClipPrecision;

            public byte lfQuality;

            public byte lfPitchAndFamily;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string lfFaceName;

            public LOGFONT()
            {
            }

            public LOGFONT(LOGFONT lf)
            {
                lfHeight = lf.lfHeight;
                lfWidth = lf.lfWidth;
                lfEscapement = lf.lfEscapement;
                lfOrientation = lf.lfOrientation;
                lfWeight = lf.lfWeight;
                lfItalic = lf.lfItalic;
                lfUnderline = lf.lfUnderline;
                lfStrikeOut = lf.lfStrikeOut;
                lfCharSet = lf.lfCharSet;
                lfOutPrecision = lf.lfOutPrecision;
                lfClipPrecision = lf.lfClipPrecision;
                lfQuality = lf.lfQuality;
                lfPitchAndFamily = lf.lfPitchAndFamily;
                lfFaceName = lf.lfFaceName;
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct TEXTMETRIC
        {
            public int tmHeight;

            public int tmAscent;

            public int tmDescent;

            public int tmInternalLeading;

            public int tmExternalLeading;

            public int tmAveCharWidth;

            public int tmMaxCharWidth;

            public int tmWeight;

            public int tmOverhang;

            public int tmDigitizedAspectX;

            public int tmDigitizedAspectY;

            public char tmFirstChar;

            public char tmLastChar;

            public char tmDefaultChar;

            public char tmBreakChar;

            public byte tmItalic;

            public byte tmUnderlined;

            public byte tmStruckOut;

            public byte tmPitchAndFamily;

            public byte tmCharSet;
        }

        public struct TEXTMETRICA
        {
            public int tmHeight;

            public int tmAscent;

            public int tmDescent;

            public int tmInternalLeading;

            public int tmExternalLeading;

            public int tmAveCharWidth;

            public int tmMaxCharWidth;

            public int tmWeight;

            public int tmOverhang;

            public int tmDigitizedAspectX;

            public int tmDigitizedAspectY;

            public byte tmFirstChar;

            public byte tmLastChar;

            public byte tmDefaultChar;

            public byte tmBreakChar;

            public byte tmItalic;

            public byte tmUnderlined;

            public byte tmStruckOut;

            public byte tmPitchAndFamily;

            public byte tmCharSet;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class NOTIFYICONDATA
        {
            public int cbSize = Marshal.SizeOf(typeof(NOTIFYICONDATA));

            public IntPtr hWnd;

            public int uID;

            public int uFlags;

            public int uCallbackMessage;

            public IntPtr hIcon;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szTip;

            public int dwState;

            public int dwStateMask;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szInfo;

            public int uTimeoutOrVersion;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szInfoTitle;

            public int dwInfoFlags;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MENUITEMINFO_T
        {
            public int cbSize = Marshal.SizeOf(typeof(MENUITEMINFO_T));

            public int fMask;

            public int fType;

            public int fState;

            public int wID;

            public IntPtr hSubMenu;

            public IntPtr hbmpChecked;

            public IntPtr hbmpUnchecked;

            public IntPtr dwItemData;

            public string dwTypeData;

            public int cch;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MENUITEMINFO_T_RW
        {
            public int cbSize = Marshal.SizeOf(typeof(MENUITEMINFO_T_RW));

            public int fMask;

            public int fType;

            public int fState;

            public int wID;

            public IntPtr hSubMenu = IntPtr.Zero;

            public IntPtr hbmpChecked = IntPtr.Zero;

            public IntPtr hbmpUnchecked = IntPtr.Zero;

            public IntPtr dwItemData = IntPtr.Zero;

            public IntPtr dwTypeData = IntPtr.Zero;

            public int cch;

            public IntPtr hbmpItem = IntPtr.Zero;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct MSAAMENUINFO
        {
            public int dwMSAASignature;

            public int cchWText;

            public string pszWText;

            public MSAAMENUINFO(string text)
            {
                dwMSAASignature = -1441927155;
                cchWText = text.Length;
                pszWText = text;
            }
        }

        public delegate bool EnumThreadWindowsCallback(IntPtr hWnd, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class OPENFILENAME_I
        {
            public int lStructSize = Marshal.SizeOf(typeof(OPENFILENAME_I));

            public IntPtr hwndOwner;

            public IntPtr hInstance;

            public string lpstrFilter;

            public IntPtr lpstrCustomFilter = IntPtr.Zero;

            public int nMaxCustFilter;

            public int nFilterIndex;

            public IntPtr lpstrFile;

            public int nMaxFile = 260;

            public IntPtr lpstrFileTitle = IntPtr.Zero;

            public int nMaxFileTitle = 260;

            public string lpstrInitialDir;

            public string lpstrTitle;

            public int Flags;

            public short nFileOffset;

            public short nFileExtension;

            public string lpstrDefExt;

            public IntPtr lCustData = IntPtr.Zero;

            public WndProc lpfnHook;

            public string lpTemplateName;

            public IntPtr pvReserved = IntPtr.Zero;

            public int dwReserved;

            public int FlagsEx;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        [CLSCompliant(false)]
        public class CHOOSEFONT
        {
            public int lStructSize = Marshal.SizeOf(typeof(CHOOSEFONT));

            public IntPtr hwndOwner;

            public IntPtr hDC;

            public IntPtr lpLogFont;

            public int iPointSize;

            public int Flags;

            public int rgbColors;

            public IntPtr lCustData = IntPtr.Zero;

            public WndProc lpfnHook;

            public string lpTemplateName;

            public IntPtr hInstance;

            public string lpszStyle;

            public short nFontType;

            public short ___MISSING_ALIGNMENT__;

            public int nSizeMin;

            public int nSizeMax;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class BITMAPINFO
        {
            public int bmiHeader_biSize = 40;

            public int bmiHeader_biWidth;

            public int bmiHeader_biHeight;

            public short bmiHeader_biPlanes;

            public short bmiHeader_biBitCount;

            public int bmiHeader_biCompression;

            public int bmiHeader_biSizeImage;

            public int bmiHeader_biXPelsPerMeter;

            public int bmiHeader_biYPelsPerMeter;

            public int bmiHeader_biClrUsed;

            public int bmiHeader_biClrImportant;

            public byte bmiColors_rgbBlue;

            public byte bmiColors_rgbGreen;

            public byte bmiColors_rgbRed;

            public byte bmiColors_rgbReserved;

            private BITMAPINFO()
            {
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public class BITMAPINFOHEADER
        {
            public int biSize = 40;

            public int biWidth;

            public int biHeight;

            public short biPlanes;

            public short biBitCount;

            public int biCompression;

            public int biSizeImage;

            public int biXPelsPerMeter;

            public int biYPelsPerMeter;

            public int biClrUsed;

            public int biClrImportant;
        }

        public class Ole
        {
            public const int PICTYPE_UNINITIALIZED = -1;

            public const int PICTYPE_NONE = 0;

            public const int PICTYPE_BITMAP = 1;

            public const int PICTYPE_METAFILE = 2;

            public const int PICTYPE_ICON = 3;

            public const int PICTYPE_ENHMETAFILE = 4;

            public const int STATFLAG_DEFAULT = 0;

            public const int STATFLAG_NONAME = 1;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class STATSTG
        {
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pwcsName;

            public int type;

            [MarshalAs(UnmanagedType.I8)]
            public long cbSize;

            [MarshalAs(UnmanagedType.I8)]
            public long mtime;

            [MarshalAs(UnmanagedType.I8)]
            public long ctime;

            [MarshalAs(UnmanagedType.I8)]
            public long atime;

            [MarshalAs(UnmanagedType.I4)]
            public int grfMode;

            [MarshalAs(UnmanagedType.I4)]
            public int grfLocksSupported;

            public int clsid_data1;

            [MarshalAs(UnmanagedType.I2)]
            public short clsid_data2;

            [MarshalAs(UnmanagedType.I2)]
            public short clsid_data3;

            [MarshalAs(UnmanagedType.U1)]
            public byte clsid_b0;

            [MarshalAs(UnmanagedType.U1)]
            public byte clsid_b1;

            [MarshalAs(UnmanagedType.U1)]
            public byte clsid_b2;

            [MarshalAs(UnmanagedType.U1)]
            public byte clsid_b3;

            [MarshalAs(UnmanagedType.U1)]
            public byte clsid_b4;

            [MarshalAs(UnmanagedType.U1)]
            public byte clsid_b5;

            [MarshalAs(UnmanagedType.U1)]
            public byte clsid_b6;

            [MarshalAs(UnmanagedType.U1)]
            public byte clsid_b7;

            [MarshalAs(UnmanagedType.I4)]
            public int grfStateBits;

            [MarshalAs(UnmanagedType.I4)]
            public int reserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class FILETIME
        {
            public uint dwLowDateTime;

            public uint dwHighDateTime;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class SYSTEMTIME
        {
            public short wYear;

            public short wMonth;

            public short wDayOfWeek;

            public short wDay;

            public short wHour;

            public short wMinute;

            public short wSecond;

            public short wMilliseconds;

            public override string ToString()
            {
                return "[SYSTEMTIME: " + wDay.ToString(CultureInfo.InvariantCulture) + "/" + wMonth.ToString(CultureInfo.InvariantCulture) + "/" + wYear.ToString(CultureInfo.InvariantCulture) + " " + wHour.ToString(CultureInfo.InvariantCulture) + ":" + wMinute.ToString(CultureInfo.InvariantCulture) + ":" + wSecond.ToString(CultureInfo.InvariantCulture) + "]";
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        [CLSCompliant(false)]
        public sealed class _POINTL
        {
            public int x;

            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public sealed class tagSIZE
        {
            public int cx;

            public int cy;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class COMRECT
        {
            public int left;

            public int top;

            public int right;

            public int bottom;

            public COMRECT()
            {
            }

            public COMRECT(Rectangle r)
            {
                left = r.X;
                top = r.Y;
                right = r.Right;
                bottom = r.Bottom;
            }

            public COMRECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public static COMRECT FromXYWH(int x, int y, int width, int height)
            {
                return new COMRECT(x, y, x + width, y + height);
            }

            public override string ToString()
            {
                return "Left = " + left + " Top " + top + " Right = " + right + " Bottom = " + bottom;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public sealed class tagOleMenuGroupWidths
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public int[] widths = new int[6];
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public class MSOCRINFOSTRUCT
        {
            public int cbSize = Marshal.SizeOf(typeof(MSOCRINFOSTRUCT));

            public int uIdleTimeInterval;

            public int grfcrf;

            public int grfcadvf;
        }

        public struct NMLISTVIEW
        {
            public NMHDR hdr;

            public int iItem;

            public int iSubItem;

            public int uNewState;

            public int uOldState;

            public int uChanged;

            public IntPtr lParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        public sealed class tagPOINTF
        {
            [MarshalAs(UnmanagedType.R4)]
            public float x;

            [MarshalAs(UnmanagedType.R4)]
            public float y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public sealed class tagOIFI
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cb;

            public bool fMDIApp;

            public IntPtr hwndFrame;

            public IntPtr hAccel;

            [MarshalAs(UnmanagedType.U4)]
            public int cAccelEntries;
        }

        public struct NMLVFINDITEM
        {
            public NMHDR hdr;

            public int iStart;

            public LVFINDINFO lvfi;
        }

        public struct NMHDR
        {
            public IntPtr hwndFrom;

            public IntPtr idFrom;

            public int code;
        }

        [ComVisible(true)]
        [Guid("626FC520-A41E-11CF-A731-00A0C9082637")]
        [InterfaceType(ComInterfaceType.InterfaceIsDual)]
        internal interface IHTMLDocument
        {
            [return: MarshalAs(UnmanagedType.Interface)]
            object GetScript();
        }

        [ComImport]
        [Guid("376BD3AA-3845-101B-84ED-08002B2EC713")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IPerPropertyBrowsing
        {
            [PreserveSig]
            int GetDisplayString(int dispID, [Out][MarshalAs(UnmanagedType.LPArray)] string[] pBstr);

            [PreserveSig]
            int MapPropertyToPage(int dispID, out Guid pGuid);

            [PreserveSig]
            int GetPredefinedStrings(int dispID, [Out] CA_STRUCT pCaStringsOut, [Out] CA_STRUCT pCaCookiesOut);

            [PreserveSig]
            int GetPredefinedValue(int dispID, [In][MarshalAs(UnmanagedType.U4)] int dwCookie, [Out] VARIANT pVarOut);
        }

        [ComImport]
        [Guid("4D07FC10-F931-11CE-B001-00AA006884E5")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface ICategorizeProperties
        {
            [PreserveSig]
            int MapPropertyToCategory(int dispID, ref int categoryID);

            [PreserveSig]
            int GetCategoryName(int propcat, [In][MarshalAs(UnmanagedType.U4)] int lcid, out string categoryName);
        }

        [StructLayout(LayoutKind.Sequential)]
        public sealed class tagSIZEL
        {
            public int cx;

            public int cy;
        }

        [StructLayout(LayoutKind.Sequential)]
        public sealed class tagOLEVERB
        {
            public int lVerb;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpszVerbName;

            [MarshalAs(UnmanagedType.U4)]
            public int fuFlags;

            [MarshalAs(UnmanagedType.U4)]
            public int grfAttribs;
        }

        [StructLayout(LayoutKind.Sequential)]
        public sealed class tagLOGPALETTE
        {
            [MarshalAs(UnmanagedType.U2)]
            public short palVersion;

            [MarshalAs(UnmanagedType.U2)]
            public short palNumEntries;
        }

        [StructLayout(LayoutKind.Sequential)]
        public sealed class tagCONTROLINFO
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cb = Marshal.SizeOf(typeof(tagCONTROLINFO));

            public IntPtr hAccel;

            [MarshalAs(UnmanagedType.U2)]
            public short cAccel;

            [MarshalAs(UnmanagedType.U4)]
            public int dwFlags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public sealed class CA_STRUCT
        {
            public int cElems;

            public IntPtr pElems = IntPtr.Zero;
        }

        [StructLayout(LayoutKind.Sequential)]
        public sealed class VARIANT
        {
            [MarshalAs(UnmanagedType.I2)]
            public short vt;

            [MarshalAs(UnmanagedType.I2)]
            public short reserved1;

            [MarshalAs(UnmanagedType.I2)]
            public short reserved2;

            [MarshalAs(UnmanagedType.I2)]
            public short reserved3;

            public IntPtr data1;

            public IntPtr data2;

            public bool Byref => (vt & 0x4000) != 0;

            private static bool Is64bitProcess => IntPtr.Size == 8;

            public void Clear()
            {
                if ((vt == 13 || vt == 9) && data1 != IntPtr.Zero)
                {
                    Marshal.Release(data1);
                }
                if (vt == 8 && data1 != IntPtr.Zero)
                {
                    SysFreeString(data1);
                }
                data1 = (data2 = IntPtr.Zero);
                vt = 0;
            }

            ~VARIANT()
            {
                Clear();
            }

            public static VARIANT FromObject(object var)
            {
                VARIANT vARIANT = new VARIANT();
                if (var == null)
                {
                    vARIANT.vt = 0;
                }
                else if (!Convert.IsDBNull(var))
                {
                    Type type = var.GetType();
                    if (type == typeof(bool))
                    {
                        vARIANT.vt = 11;
                    }
                    else if (type == typeof(byte))
                    {
                        vARIANT.vt = 17;
                        vARIANT.data1 = (IntPtr)Convert.ToByte(var, CultureInfo.InvariantCulture);
                    }
                    else if (type == typeof(char))
                    {
                        vARIANT.vt = 18;
                        vARIANT.data1 = (IntPtr)Convert.ToChar(var, CultureInfo.InvariantCulture);
                    }
                    else if (type == typeof(string))
                    {
                        vARIANT.vt = 8;
                        vARIANT.data1 = SysAllocString(Convert.ToString(var, CultureInfo.InvariantCulture));
                    }
                    else if (type == typeof(short))
                    {
                        vARIANT.vt = 2;
                        vARIANT.data1 = (IntPtr)Convert.ToInt16(var, CultureInfo.InvariantCulture);
                    }
                    else if (type == typeof(int))
                    {
                        vARIANT.vt = 3;
                        vARIANT.data1 = (IntPtr)Convert.ToInt32(var, CultureInfo.InvariantCulture);
                    }
                    else if (type == typeof(long))
                    {
                        vARIANT.vt = 20;
                        vARIANT.SetLong(Convert.ToInt64(var, CultureInfo.InvariantCulture));
                    }
                    else if (type == typeof(decimal))
                    {
                        vARIANT.vt = 6;
                        decimal d = (decimal)var;
                        vARIANT.SetLong(decimal.ToInt64(d));
                    }
                    else if (type == typeof(decimal))
                    {
                        vARIANT.vt = 14;
                        decimal d2 = Convert.ToDecimal(var, CultureInfo.InvariantCulture);
                        vARIANT.SetLong(decimal.ToInt64(d2));
                    }
                    else if (type == typeof(double))
                    {
                        vARIANT.vt = 5;
                    }
                    else if (type == typeof(float) || type == typeof(float))
                    {
                        vARIANT.vt = 4;
                    }
                    else if (type == typeof(DateTime))
                    {
                        vARIANT.vt = 7;
                        vARIANT.SetLong(Convert.ToDateTime(var, CultureInfo.InvariantCulture).ToFileTime());
                    }
                    else if (type == typeof(sbyte))
                    {
                        vARIANT.vt = 16;
                        vARIANT.data1 = (IntPtr)Convert.ToSByte(var, CultureInfo.InvariantCulture);
                    }
                    else if (type == typeof(ushort))
                    {
                        vARIANT.vt = 18;
                        vARIANT.data1 = (IntPtr)Convert.ToUInt16(var, CultureInfo.InvariantCulture);
                    }
                    else if (type == typeof(uint))
                    {
                        vARIANT.vt = 19;
                        vARIANT.data1 = (IntPtr)Convert.ToUInt32(var, CultureInfo.InvariantCulture);
                    }
                    else if (type == typeof(ulong))
                    {
                        vARIANT.vt = 21;
                        vARIANT.SetLong((long)Convert.ToUInt64(var, CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        if (!(type == typeof(object)) && !(type == typeof(UnsafeNativeMethods.IDispatch)) && !type.IsCOMObject)
                        {
                            throw new ArgumentException("ConnPointUnhandledType");
                        }
                        vARIANT.vt = (short)((type == typeof(UnsafeNativeMethods.IDispatch)) ? 9 : 13);
                        vARIANT.data1 = Marshal.GetIUnknownForObject(var);
                    }
                }
                return vARIANT;
            }

            [DllImport("oleaut32.dll", CharSet = CharSet.Auto)]
            private static extern IntPtr SysAllocString([In][MarshalAs(UnmanagedType.LPWStr)] string s);

            [DllImport("oleaut32.dll", CharSet = CharSet.Auto)]
            private static extern void SysFreeString(IntPtr pbstr);

            public void SetLong(long lVal)
            {
                data1 = (IntPtr)(lVal & 0xFFFFFFFFu);
                data2 = (IntPtr)((lVal >> 32) & 0xFFFFFFFFu);
            }

            public IntPtr ToCoTaskMemPtr()
            {
                IntPtr intPtr = Marshal.AllocCoTaskMem(16);
                Marshal.WriteInt16(intPtr, vt);
                Marshal.WriteInt16(intPtr, 2, reserved1);
                Marshal.WriteInt16(intPtr, 4, reserved2);
                Marshal.WriteInt16(intPtr, 6, reserved3);
                Marshal.WriteInt32(intPtr, 8, (int)data1);
                Marshal.WriteInt32(intPtr, 12, (int)data2);
                return intPtr;
            }

            private static int Lo32(IntPtr val)
            {
                if (Is64bitProcess)
                {
                    return (int)(long)val;
                }
                return (int)val;
            }

            private static IntPtr Lo32AsIntPtr(IntPtr val)
            {
                if (Is64bitProcess)
                {
                    return new IntPtr(Lo32(val));
                }
                return val;
            }

            private static IntPtr GetRefInt32(IntPtr val)
            {
                if (Is64bitProcess)
                {
                    return (IntPtr)Marshal.ReadInt32(val);
                }
                return GetRefInt(val);
            }

            private static long Lo64(IntPtr data1, IntPtr data2)
            {
                if (Is64bitProcess)
                {
                    long num = (long)data1;
                    IntPtr intPtr = new IntPtr((int)num);
                    IntPtr intPtr2 = new IntPtr((int)(num >> 32));
                    return (uint)(((int)intPtr & -1) | (int)intPtr2);
                }
                return (uint)(((int)data1 & -1) | (int)data2);
            }

            public object ToObject()
            {
                IntPtr intPtr = data1;
                int num = vt & 0xFFF;
                switch (num)
                {
                    case 0:
                        return null;
                    case 1:
                        return Convert.DBNull;
                    case 16:
                        if (Byref)
                        {
                            intPtr = (IntPtr)Marshal.ReadByte(intPtr);
                        }
                        return (sbyte)(0xFF & (sbyte)Lo32(intPtr));
                    case 17:
                        if (Byref)
                        {
                            intPtr = (IntPtr)Marshal.ReadByte(intPtr);
                        }
                        return (byte)(0xFFu & (byte)Lo32(intPtr));
                    case 2:
                        if (Byref)
                        {
                            intPtr = (IntPtr)Marshal.ReadInt16(intPtr);
                        }
                        return (short)(0xFFFF & (short)Lo32(intPtr));
                    case 18:
                        if (Byref)
                        {
                            intPtr = (IntPtr)Marshal.ReadInt16(intPtr);
                        }
                        return (ushort)(0xFFFFu & (ushort)Lo32(intPtr));
                    case 3:
                    case 22:
                        if (Byref)
                        {
                            intPtr = (IntPtr)Marshal.ReadInt32(intPtr);
                        }
                        return Lo32(intPtr);
                    case 19:
                    case 23:
                        if (Byref)
                        {
                            intPtr = (IntPtr)Marshal.ReadInt32(intPtr);
                        }
                        return (uint)Lo32(intPtr);
                    case 20:
                    case 21:
                        {
                            long ticks = ((!Byref) ? Lo64(data1, data2) : Marshal.ReadInt64(intPtr));
                            if (vt == 20)
                            {
                                return ticks;
                            }
                            return (ulong)ticks;
                        }
                    default:
                        if (Byref)
                        {
                            IntPtr refInt = GetRefInt32(intPtr);
                        }
                        switch (num)
                        {
                            case 4:
                            case 5:
                                throw new FormatException("CannotConvertIntToFloat");
                            case 6:
                                {
                                    long ticks = Lo64(data1, data2);
                                    return new decimal(ticks);
                                }
                            case 7:
                                throw new FormatException("CannotConvertDoubleToDate");
                            case 8:
                            case 31:
                                if (Byref)
                                {
                                    intPtr = GetRefInt(intPtr);
                                }
                                return Marshal.PtrToStringUni(intPtr);
                            case 30:
                                if (Byref)
                                {
                                    intPtr = GetRefInt(intPtr);
                                }
                                return Marshal.PtrToStringAnsi(intPtr);
                            case 9:
                            case 13:
                                if (Byref)
                                {
                                    intPtr = GetRefInt(intPtr);
                                }
                                return Marshal.GetObjectForIUnknown(intPtr);
                            case 25:
                                if (Byref)
                                {
                                    intPtr = GetRefInt32(intPtr);
                                }
                                return Lo32AsIntPtr(intPtr);
                            case 14:
                                {
                                    long ticks = Lo64(data1, data2);
                                    return new decimal(ticks);
                                }
                            case 11:
                                if (Byref)
                                {
                                    intPtr = GetRefInt32(intPtr);
                                }
                                return Lo32AsIntPtr(intPtr) != IntPtr.Zero;
                            case 12:
                                {
                                    if (Byref)
                                    {
                                        intPtr = GetRefInt(intPtr);
                                    }
                                    VARIANT vARIANT = (VARIANT)UnsafeNativeMethods.PtrToStructure(intPtr, typeof(VARIANT));
                                    return vARIANT.ToObject();
                                }
                            case 72:
                                {
                                    if (Byref)
                                    {
                                        intPtr = GetRefInt(intPtr);
                                    }
                                    Guid guid = (Guid)UnsafeNativeMethods.PtrToStructure(intPtr, typeof(Guid));
                                    return guid;
                                }
                            case 64:
                                {
                                    long ticks = Lo64(data1, data2);
                                    return new DateTime(ticks);
                                }
                            case 29:
                                throw new ArgumentException("COM2UnhandledVT -- VT_USERDEFINED");
                            default:
                                {
                                    int num2 = vt;
                                    throw new ArgumentException("COM2UnhandledVT");
                                }
                        }
                }
            }

            private static IntPtr GetRefInt(IntPtr value)
            {
                return Marshal.ReadIntPtr(value);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public sealed class tagLICINFO
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cbLicInfo = Marshal.SizeOf(typeof(tagLICINFO));

            public int fRuntimeAvailable;

            public int fLicVerified;
        }

        public enum tagVT
        {
            VT_EMPTY = 0,
            VT_NULL = 1,
            VT_I2 = 2,
            VT_I4 = 3,
            VT_R4 = 4,
            VT_R8 = 5,
            VT_CY = 6,
            VT_DATE = 7,
            VT_BSTR = 8,
            VT_DISPATCH = 9,
            VT_ERROR = 10,
            VT_BOOL = 11,
            VT_VARIANT = 12,
            VT_UNKNOWN = 13,
            VT_DECIMAL = 14,
            VT_I1 = 0x10,
            VT_UI1 = 17,
            VT_UI2 = 18,
            VT_UI4 = 19,
            VT_I8 = 20,
            VT_UI8 = 21,
            VT_INT = 22,
            VT_UINT = 23,
            VT_VOID = 24,
            VT_HRESULT = 25,
            VT_PTR = 26,
            VT_SAFEARRAY = 27,
            VT_CARRAY = 28,
            VT_USERDEFINED = 29,
            VT_LPSTR = 30,
            VT_LPWSTR = 0x1F,
            VT_RECORD = 36,
            VT_FILETIME = 0x40,
            VT_BLOB = 65,
            VT_STREAM = 66,
            VT_STORAGE = 67,
            VT_STREAMED_OBJECT = 68,
            VT_STORED_OBJECT = 69,
            VT_BLOB_OBJECT = 70,
            VT_CF = 71,
            VT_CLSID = 72,
            VT_BSTR_BLOB = 0xFFF,
            VT_VECTOR = 0x1000,
            VT_ARRAY = 0x2000,
            VT_BYREF = 0x4000,
            VT_RESERVED = 0x8000,
            VT_ILLEGAL = 0xFFFF,
            VT_ILLEGALMASKED = 0xFFF,
            VT_TYPEMASK = 0xFFF
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class WNDCLASS_D
        {
            public int style;

            public WndProc lpfnWndProc;

            public int cbClsExtra;

            public int cbWndExtra;

            public IntPtr hInstance = IntPtr.Zero;

            public IntPtr hIcon = IntPtr.Zero;

            public IntPtr hCursor = IntPtr.Zero;

            public IntPtr hbrBackground = IntPtr.Zero;

            public string lpszMenuName;

            public string lpszClassName;
        }

        public class MSOCM
        {
            public const int msocrfNeedIdleTime = 1;

            public const int msocrfNeedPeriodicIdleTime = 2;

            public const int msocrfPreTranslateKeys = 4;

            public const int msocrfPreTranslateAll = 8;

            public const int msocrfNeedSpecActiveNotifs = 16;

            public const int msocrfNeedAllActiveNotifs = 32;

            public const int msocrfExclusiveBorderSpace = 64;

            public const int msocrfExclusiveActivation = 128;

            public const int msocrfNeedAllMacEvents = 256;

            public const int msocrfMaster = 512;

            public const int msocadvfModal = 1;

            public const int msocadvfRedrawOff = 2;

            public const int msocadvfWarningsOff = 4;

            public const int msocadvfRecording = 8;

            public const int msochostfExclusiveBorderSpace = 1;

            public const int msoidlefPeriodic = 1;

            public const int msoidlefNonPeriodic = 2;

            public const int msoidlefPriority = 4;

            public const int msoidlefAll = -1;

            public const int msoloopDoEventsModal = -2;

            public const int msoloopMain = -1;

            public const int msoloopFocusWait = 1;

            public const int msoloopDoEvents = 2;

            public const int msoloopDebug = 3;

            public const int msoloopModalForm = 4;

            public const int msoloopModalAlert = 5;

            public const int msocstateModal = 1;

            public const int msocstateRedrawOff = 2;

            public const int msocstateWarningsOff = 3;

            public const int msocstateRecording = 4;

            public const int msoccontextAll = 0;

            public const int msoccontextMine = 1;

            public const int msoccontextOthers = 2;

            public const int msogacActive = 0;

            public const int msogacTracking = 1;

            public const int msogacTrackingOrActive = 2;

            public const int msocWindowFrameToplevel = 0;

            public const int msocWindowFrameOwner = 1;

            public const int msocWindowComponent = 2;

            public const int msocWindowDlgOwner = 3;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class TOOLINFO_T
        {
            public int cbSize = Marshal.SizeOf(typeof(TOOLINFO_T));

            public int uFlags;

            public IntPtr hwnd;

            public IntPtr uId;

            public RECT rect;

            public IntPtr hinst = IntPtr.Zero;

            public string lpszText;

            public IntPtr lParam = IntPtr.Zero;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class TOOLINFO_TOOLTIP
        {
            public int cbSize = Marshal.SizeOf(typeof(TOOLINFO_TOOLTIP));

            public int uFlags;

            public IntPtr hwnd;

            public IntPtr uId;

            public RECT rect;

            public IntPtr hinst = IntPtr.Zero;

            public IntPtr lpszText;

            public IntPtr lParam = IntPtr.Zero;
        }

        [StructLayout(LayoutKind.Sequential)]
        public sealed class tagDVTARGETDEVICE
        {
            [MarshalAs(UnmanagedType.U4)]
            public int tdSize;

            [MarshalAs(UnmanagedType.U2)]
            public short tdDriverNameOffset;

            [MarshalAs(UnmanagedType.U2)]
            public short tdDeviceNameOffset;

            [MarshalAs(UnmanagedType.U2)]
            public short tdPortNameOffset;

            [MarshalAs(UnmanagedType.U2)]
            public short tdExtDevmodeOffset;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct TV_ITEM
        {
            public int mask;

            public IntPtr hItem;

            public int state;

            public int stateMask;

            public IntPtr pszText;

            public int cchTextMax;

            public int iImage;

            public int iSelectedImage;

            public int cChildren;

            public IntPtr lParam;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct TVSORTCB
        {
            public IntPtr hParent;

            public TreeViewCompareCallback lpfnCompare;

            public IntPtr lParam;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct TV_INSERTSTRUCT
        {
            public IntPtr hParent;

            public IntPtr hInsertAfter;

            public int item_mask;

            public IntPtr item_hItem;

            public int item_state;

            public int item_stateMask;

            public IntPtr item_pszText;

            public int item_cchTextMax;

            public int item_iImage;

            public int item_iSelectedImage;

            public int item_cChildren;

            public IntPtr item_lParam;

            public int item_iIntegral;
        }

        public struct NMTREEVIEW
        {
            public NMHDR nmhdr;

            public int action;

            public TV_ITEM itemOld;

            public TV_ITEM itemNew;

            public int ptDrag_X;

            public int ptDrag_Y;
        }

        public struct NMTVGETINFOTIP
        {
            public NMHDR nmhdr;

            public string pszText;

            public int cchTextMax;

            public IntPtr item;

            public IntPtr lParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class NMTVDISPINFO
        {
            public NMHDR hdr;

            public TV_ITEM item;
        }

        [StructLayout(LayoutKind.Sequential)]
        public sealed class POINTL
        {
            public int x;

            public int y;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct HIGHCONTRAST
        {
            public int cbSize;

            public int dwFlags;

            public string lpszDefaultScheme;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct HIGHCONTRAST_I
        {
            public int cbSize;

            public int dwFlags;

            public IntPtr lpszDefaultScheme;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class TCITEM_T
        {
            public int mask;

            public int dwState;

            public int dwStateMask;

            public string pszText;

            public int cchTextMax;

            public int iImage;

            public IntPtr lParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        public sealed class tagDISPPARAMS
        {
            public IntPtr rgvarg;

            public IntPtr rgdispidNamedArgs;

            [MarshalAs(UnmanagedType.U4)]
            public int cArgs;

            [MarshalAs(UnmanagedType.U4)]
            public int cNamedArgs;
        }

        public enum tagINVOKEKIND
        {
            INVOKE_FUNC = 1,
            INVOKE_PROPERTYGET = 2,
            INVOKE_PROPERTYPUT = 4,
            INVOKE_PROPERTYPUTREF = 8
        }

        [StructLayout(LayoutKind.Sequential)]
        public class tagEXCEPINFO
        {
            [MarshalAs(UnmanagedType.U2)]
            public short wCode;

            [MarshalAs(UnmanagedType.U2)]
            public short wReserved;

            [MarshalAs(UnmanagedType.BStr)]
            public string bstrSource;

            [MarshalAs(UnmanagedType.BStr)]
            public string bstrDescription;

            [MarshalAs(UnmanagedType.BStr)]
            public string bstrHelpFile;

            [MarshalAs(UnmanagedType.U4)]
            public int dwHelpContext;

            public IntPtr pvReserved = IntPtr.Zero;

            public IntPtr pfnDeferredFillIn = IntPtr.Zero;

            [MarshalAs(UnmanagedType.U4)]
            public int scode;
        }

        public enum tagDESCKIND
        {
            DESCKIND_NONE,
            DESCKIND_FUNCDESC,
            DESCKIND_VARDESC,
            DESCKIND_TYPECOMP,
            DESCKIND_IMPLICITAPPOBJ,
            DESCKIND_MAX
        }

        [StructLayout(LayoutKind.Sequential)]
        public sealed class tagFUNCDESC
        {
            public int memid;

            public IntPtr lprgscode = IntPtr.Zero;

            public IntPtr lprgelemdescParam = IntPtr.Zero;

            public int funckind;

            public int invkind;

            public int callconv;

            [MarshalAs(UnmanagedType.I2)]
            public short cParams;

            [MarshalAs(UnmanagedType.I2)]
            public short cParamsOpt;

            [MarshalAs(UnmanagedType.I2)]
            public short oVft;

            [MarshalAs(UnmanagedType.I2)]
            public short cScodesi;

            public value_tagELEMDESC elemdescFunc;

            [MarshalAs(UnmanagedType.U2)]
            public short wFuncFlags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public sealed class tagVARDESC
        {
            public int memid;

            public IntPtr lpstrSchema = IntPtr.Zero;

            public IntPtr unionMember = IntPtr.Zero;

            public value_tagELEMDESC elemdescVar;

            [MarshalAs(UnmanagedType.U2)]
            public short wVarFlags;

            public int varkind;
        }

        public struct value_tagELEMDESC
        {
            public tagTYPEDESC tdesc;

            public tagPARAMDESC paramdesc;
        }

        public struct WINDOWPOS
        {
            public IntPtr hwnd;

            public IntPtr hwndInsertAfter;

            public int x;

            public int y;

            public int cx;

            public int cy;

            public int flags;
        }

        public struct HDLAYOUT
        {
            public IntPtr prc;

            public IntPtr pwpos;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class DRAWITEMSTRUCT
        {
            public int CtlType;

            public int CtlID;

            public int itemID;

            public int itemAction;

            public int itemState;

            public IntPtr hwndItem = IntPtr.Zero;

            public IntPtr hDC = IntPtr.Zero;

            public RECT rcItem;

            public IntPtr itemData = IntPtr.Zero;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class MEASUREITEMSTRUCT
        {
            public int CtlType;

            public int CtlID;

            public int itemID;

            public int itemWidth;

            public int itemHeight;

            public IntPtr itemData = IntPtr.Zero;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class HELPINFO
        {
            public int cbSize = Marshal.SizeOf(typeof(HELPINFO));

            public int iContextType;

            public int iCtrlId;

            public IntPtr hItemHandle = IntPtr.Zero;

            public IntPtr dwContextId = IntPtr.Zero;

            public POINT MousePos;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class ACCEL
        {
            public byte fVirt;

            public short key;

            public short cmd;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class MINMAXINFO
        {
            public POINT ptReserved;

            public POINT ptMaxSize;

            public POINT ptMaxPosition;

            public POINT ptMinTrackSize;

            public POINT ptMaxTrackSize;
        }

        [ComImport]
        [Guid("B196B28B-BAB4-101A-B69C-00AA00341D07")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface ISpecifyPropertyPages
        {
            void GetPages([Out] tagCAUUID pPages);
        }

        [StructLayout(LayoutKind.Sequential)]
        public sealed class tagCAUUID
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cElems;

            public IntPtr pElems = IntPtr.Zero;
        }

        public struct NMTOOLBAR
        {
            public NMHDR hdr;

            public int iItem;

            public TBBUTTON tbButton;

            public int cchText;

            public IntPtr pszText;
        }

        public struct TBBUTTON
        {
            public int iBitmap;

            public int idCommand;

            public byte fsState;

            public byte fsStyle;

            public byte bReserved0;

            public byte bReserved1;

            public IntPtr dwData;

            public IntPtr iString;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class TOOLTIPTEXT
        {
            public NMHDR hdr;

            public string lpszText;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szText;

            public IntPtr hinst;

            public int uFlags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class TOOLTIPTEXTA
        {
            public NMHDR hdr;

            public string lpszText;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szText;

            public IntPtr hinst;

            public int uFlags;
        }

        public struct NMTBHOTITEM
        {
            public NMHDR hdr;

            public int idOld;

            public int idNew;

            public int dwFlags;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class HDITEM2
        {
            public int mask;

            public int cxy;

            public IntPtr pszText_notUsed = IntPtr.Zero;

            public IntPtr hbm = IntPtr.Zero;

            public int cchTextMax;

            public int fmt;

            public IntPtr lParam = IntPtr.Zero;

            public int iImage;

            public int iOrder;

            public int type;

            public IntPtr pvFilter = IntPtr.Zero;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct TBBUTTONINFO
        {
            public int cbSize;

            public int dwMask;

            public int idCommand;

            public int iImage;

            public byte fsState;

            public byte fsStyle;

            public short cx;

            public IntPtr lParam;

            public IntPtr pszText;

            public int cchTest;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class TV_HITTESTINFO
        {
            public int pt_x;

            public int pt_y;

            public int flags;

            public IntPtr hItem = IntPtr.Zero;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class NMTVCUSTOMDRAW
        {
            public NMCUSTOMDRAW nmcd;

            public int clrText;

            public int clrTextBk;

            public int iLevel;
        }

        public struct NMCUSTOMDRAW
        {
            public NMHDR nmcd;

            public int dwDrawStage;

            public IntPtr hdc;

            public RECT rc;

            public IntPtr dwItemSpec;

            public int uItemState;

            public IntPtr lItemlParam;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MCHITTESTINFO
        {
            public int cbSize = Marshal.SizeOf(typeof(MCHITTESTINFO));

            public int pt_x;

            public int pt_y;

            public int uHit;

            public short st_wYear;

            public short st_wMonth;

            public short st_wDayOfWeek;

            public short st_wDay;

            public short st_wHour;

            public short st_wMinute;

            public short st_wSecond;

            public short st_wMilliseconds;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class NMSELCHANGE
        {
            public NMHDR nmhdr;

            public SYSTEMTIME stSelStart;

            public SYSTEMTIME stSelEnd;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class NMDAYSTATE
        {
            public NMHDR nmhdr;

            public SYSTEMTIME stStart;

            public int cDayState;

            public IntPtr prgDayState;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class NMVIEWCHANGE
        {
            public NMHDR nmhdr;

            public uint uOldView;

            public uint uNewView;
        }

        public struct NMLVCUSTOMDRAW
        {
            public NMCUSTOMDRAW nmcd;

            public int clrText;

            public int clrTextBk;

            public int iSubItem;

            public int dwItemType;

            public int clrFace;

            public int iIconEffect;

            public int iIconPhase;

            public int iPartId;

            public int iStateId;

            public RECT rcText;

            public uint uAlign;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class NMLVGETINFOTIP
        {
            public NMHDR nmhdr;

            public int flags;

            public IntPtr lpszText = IntPtr.Zero;

            public int cchTextMax;

            public int item;

            public int subItem;

            public IntPtr lParam = IntPtr.Zero;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class NMLVKEYDOWN
        {
            public NMHDR hdr;

            public short wVKey;

            public uint flags;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class LVHITTESTINFO
        {
            public int pt_x;

            public int pt_y;

            public int flags;

            public int iItem;

            public int iSubItem;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class LVBKIMAGE
        {
            public int ulFlags;

            public IntPtr hBmp = IntPtr.Zero;

            public string pszImage;

            public int cchImageMax;

            public int xOffset;

            public int yOffset;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class LVCOLUMN_T
        {
            public int mask;

            public int fmt;

            public int cx;

            public string pszText;

            public int cchTextMax;

            public int iSubItem;

            public int iImage;

            public int iOrder;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct LVFINDINFO
        {
            public int flags;

            public string psz;

            public IntPtr lParam;

            public int ptX;

            public int ptY;

            public int vkDirection;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct LVITEM
        {
            public int mask;

            public int iItem;

            public int iSubItem;

            public int state;

            public int stateMask;

            public string pszText;

            public int cchTextMax;

            public int iImage;

            public IntPtr lParam;

            public int iIndent;

            public int iGroupId;

            public int cColumns;

            public IntPtr puColumns;

            public void Reset()
            {
                pszText = null;
                mask = 0;
                iItem = 0;
                iSubItem = 0;
                stateMask = 0;
                state = 0;
                cchTextMax = 0;
                iImage = 0;
                lParam = IntPtr.Zero;
                iIndent = 0;
                iGroupId = 0;
                cColumns = 0;
                puColumns = IntPtr.Zero;
            }

            public override string ToString()
            {
                return "LVITEM: pszText = " + pszText + ", iItem = " + iItem.ToString(CultureInfo.InvariantCulture) + ", iSubItem = " + iSubItem.ToString(CultureInfo.InvariantCulture) + ", state = " + state.ToString(CultureInfo.InvariantCulture) + ", iGroupId = " + iGroupId.ToString(CultureInfo.InvariantCulture) + ", cColumns = " + cColumns.ToString(CultureInfo.InvariantCulture);
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct LVITEM_NOTEXT
        {
            public int mask;

            public int iItem;

            public int iSubItem;

            public int state;

            public int stateMask;

            public IntPtr pszText;

            public int cchTextMax;

            public int iImage;

            public IntPtr lParam;

            public int iIndent;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class LVCOLUMN
        {
            public int mask;

            public int fmt;

            public int cx;

            public IntPtr pszText;

            public int cchTextMax;

            public int iSubItem;

            public int iImage;

            public int iOrder;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public class LVGROUP
        {
            public uint cbSize = (uint)Marshal.SizeOf(typeof(LVGROUP));

            public uint mask;

            public IntPtr pszHeader;

            public int cchHeader;

            public IntPtr pszFooter = IntPtr.Zero;

            public int cchFooter;

            public int iGroupId;

            public uint stateMask;

            public uint state;

            public uint uAlign;

            public override string ToString()
            {
                return "LVGROUP: header = " + pszHeader + ", iGroupId = " + iGroupId.ToString(CultureInfo.InvariantCulture);
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class LVINSERTMARK
        {
            public uint cbSize = (uint)Marshal.SizeOf(typeof(LVINSERTMARK));

            public int dwFlags;

            public int iItem;

            public int dwReserved;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class LVTILEVIEWINFO
        {
            public uint cbSize = (uint)Marshal.SizeOf(typeof(LVTILEVIEWINFO));

            public int dwMask;

            public int dwFlags;

            public SIZE sizeTile;

            public int cLines;

            public RECT rcLabelMargin;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class NMLVCACHEHINT
        {
            public NMHDR hdr;

            public int iFrom;

            public int iTo;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class NMLVDISPINFO
        {
            public NMHDR hdr;

            public LVITEM item;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class NMLVDISPINFO_NOTEXT
        {
            public NMHDR hdr;

            public LVITEM_NOTEXT item;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class NMLVODSTATECHANGE
        {
            public NMHDR hdr;

            public int iFrom;

            public int iTo;

            public int uNewState;

            public int uOldState;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class CLIENTCREATESTRUCT
        {
            public IntPtr hWindowMenu;

            public int idFirstChild;

            public CLIENTCREATESTRUCT(IntPtr hmenu, int idFirst)
            {
                hWindowMenu = hmenu;
                idFirstChild = idFirst;
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class NMDATETIMECHANGE
        {
            public NMHDR nmhdr;

            public int dwFlags;

            public SYSTEMTIME st;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class COPYDATASTRUCT
        {
            public int dwData;

            public int cbData;

            public IntPtr lpData = IntPtr.Zero;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class NMHEADER
        {
            public NMHDR nmhdr;

            public int iItem;

            public int iButton;

            public IntPtr pItem = IntPtr.Zero;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class MOUSEHOOKSTRUCT
        {
            public int pt_x;

            public int pt_y;

            public IntPtr hWnd = IntPtr.Zero;

            public int wHitTestCode;

            public int dwExtraInfo;
        }

        public struct MOUSEINPUT
        {
            public int dx;

            public int dy;

            public int mouseData;

            public int dwFlags;

            public int time;

            public IntPtr dwExtraInfo;
        }

        public struct KEYBDINPUT
        {
            public short wVk;

            public short wScan;

            public int dwFlags;

            public int time;

            public IntPtr dwExtraInfo;
        }

        public struct HARDWAREINPUT
        {
            public int uMsg;

            public short wParamL;

            public short wParamH;
        }

        public struct INPUT
        {
            public int type;

            public INPUTUNION inputUnion;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct INPUTUNION
        {
            [FieldOffset(0)]
            public MOUSEINPUT mi;

            [FieldOffset(0)]
            public KEYBDINPUT ki;

            [FieldOffset(0)]
            public HARDWAREINPUT hi;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class CHARRANGE
        {
            public int cpMin;

            public int cpMax;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class CHARFORMATW
        {
            public int cbSize = Marshal.SizeOf(typeof(CHARFORMATW));

            public int dwMask;

            public int dwEffects;

            public int yHeight;

            public int yOffset;

            public int crTextColor;

            public byte bCharSet;

            public byte bPitchAndFamily;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
            public byte[] szFaceName = new byte[64];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class CHARFORMATA
        {
            public int cbSize = Marshal.SizeOf(typeof(CHARFORMATA));

            public int dwMask;

            public int dwEffects;

            public int yHeight;

            public int yOffset;

            public int crTextColor;

            public byte bCharSet;

            public byte bPitchAndFamily;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] szFaceName = new byte[32];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class CHARFORMAT2A
        {
            public int cbSize = Marshal.SizeOf(typeof(CHARFORMAT2A));

            public int dwMask;

            public int dwEffects;

            public int yHeight;

            public int yOffset;

            public int crTextColor;

            public byte bCharSet;

            public byte bPitchAndFamily;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] szFaceName = new byte[32];

            public short wWeight;

            public short sSpacing;

            public int crBackColor;

            public int lcid;

            public int dwReserved;

            public short sStyle;

            public short wKerning;

            public byte bUnderlineType;

            public byte bAnimation;

            public byte bRevAuthor;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class TEXTRANGE
        {
            public CHARRANGE chrg;

            public IntPtr lpstrText;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class GETTEXTLENGTHEX
        {
            public uint flags;

            public uint codepage;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public class SELCHANGE
        {
            public NMHDR nmhdr;

            public CHARRANGE chrg;

            public int seltyp;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class PARAFORMAT
        {
            public int cbSize = Marshal.SizeOf(typeof(PARAFORMAT));

            public int dwMask;

            public short wNumbering;

            public short wReserved;

            public int dxStartIndent;

            public int dxRightIndent;

            public int dxOffset;

            public short wAlignment;

            public short cTabCount;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public int[] rgxTabs;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class FINDTEXT
        {
            public CHARRANGE chrg;

            public string lpstrText;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class REPASTESPECIAL
        {
            public int dwAspect;

            public int dwParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class ENLINK
        {
            public NMHDR nmhdr;

            public int msg;

            public IntPtr wParam = IntPtr.Zero;

            public IntPtr lParam = IntPtr.Zero;

            public CHARRANGE charrange;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class ENLINK64
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 56)]
            public byte[] contents = new byte[56];
        }

        public struct RGNDATAHEADER
        {
            public int cbSizeOfStruct;

            public int iType;

            public int nCount;

            public int nRgnSize;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public class OCPFIPARAMS
        {
            public int cbSizeOfStruct = Marshal.SizeOf(typeof(OCPFIPARAMS));

            public IntPtr hwndOwner;

            public int x;

            public int y;

            public string lpszCaption;

            public int cObjects = 1;

            public IntPtr ppUnk;

            public int pageCount = 1;

            public IntPtr uuid;

            public int lcid = Application.CurrentCulture.LCID;

            public int dispidInitial;
        }

        [StructLayout(LayoutKind.Sequential)]
        [ComVisible(true)]
        public class DOCHOSTUIINFO
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize = Marshal.SizeOf(typeof(DOCHOSTUIINFO));

            [MarshalAs(UnmanagedType.I4)]
            public int dwFlags;

            [MarshalAs(UnmanagedType.I4)]
            public int dwDoubleClick;

            [MarshalAs(UnmanagedType.I4)]
            public int dwReserved1;

            [MarshalAs(UnmanagedType.I4)]
            public int dwReserved2;
        }

        public enum DOCHOSTUIFLAG
        {
            DIALOG = 1,
            DISABLE_HELP_MENU = 2,
            NO3DBORDER = 4,
            SCROLL_NO = 8,
            DISABLE_SCRIPT_INACTIVE = 0x10,
            OPENNEWWIN = 0x20,
            DISABLE_OFFSCREEN = 0x40,
            FLAT_SCROLLBAR = 0x80,
            DIV_BLOCKDEFAULT = 0x100,
            ACTIVATE_CLIENTHIT_ONLY = 0x200,
            NO3DOUTERBORDER = 0x200000,
            THEME = 0x40000,
            NOTHEME = 0x80000,
            DISABLE_COOKIE = 0x400
        }

        public enum DOCHOSTUIDBLCLICK
        {
            DEFAULT,
            SHOWPROPERTIES,
            SHOWCODE
        }

        public enum OLECMDID
        {
            OLECMDID_SAVEAS = 4,
            OLECMDID_PRINT = 6,
            OLECMDID_PRINTPREVIEW = 7,
            OLECMDID_PAGESETUP = 8,
            OLECMDID_PROPERTIES = 10
        }

        public enum OLECMDEXECOPT
        {
            OLECMDEXECOPT_DODEFAULT,
            OLECMDEXECOPT_PROMPTUSER,
            OLECMDEXECOPT_DONTPROMPTUSER,
            OLECMDEXECOPT_SHOWHELP
        }

        public enum OLECMDF
        {
            OLECMDF_SUPPORTED = 1,
            OLECMDF_ENABLED = 2,
            OLECMDF_LATCHED = 4,
            OLECMDF_NINCHED = 8,
            OLECMDF_INVISIBLE = 0x10,
            OLECMDF_DEFHIDEONCTXTMENU = 0x20
        }

        [StructLayout(LayoutKind.Sequential)]
        public class ENDROPFILES
        {
            public NMHDR nmhdr;

            public IntPtr hDrop = IntPtr.Zero;

            public int cp;

            public bool fProtected;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class REQRESIZE
        {
            public NMHDR nmhdr;

            public RECT rc;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class ENPROTECTED
        {
            public NMHDR nmhdr;

            public int msg;

            public IntPtr wParam;

            public IntPtr lParam;

            public CHARRANGE chrg;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class ENPROTECTED64
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 56)]
            public byte[] contents = new byte[56];
        }

        public class ActiveX
        {
            public const int OCM__BASE = 8192;

            public const int DISPID_VALUE = 0;

            public const int DISPID_UNKNOWN = -1;

            public const int DISPID_AUTOSIZE = -500;

            public const int DISPID_BACKCOLOR = -501;

            public const int DISPID_BACKSTYLE = -502;

            public const int DISPID_BORDERCOLOR = -503;

            public const int DISPID_BORDERSTYLE = -504;

            public const int DISPID_BORDERWIDTH = -505;

            public const int DISPID_DRAWMODE = -507;

            public const int DISPID_DRAWSTYLE = -508;

            public const int DISPID_DRAWWIDTH = -509;

            public const int DISPID_FILLCOLOR = -510;

            public const int DISPID_FILLSTYLE = -511;

            public const int DISPID_FONT = -512;

            public const int DISPID_FORECOLOR = -513;

            public const int DISPID_ENABLED = -514;

            public const int DISPID_HWND = -515;

            public const int DISPID_TABSTOP = -516;

            public const int DISPID_TEXT = -517;

            public const int DISPID_CAPTION = -518;

            public const int DISPID_BORDERVISIBLE = -519;

            public const int DISPID_APPEARANCE = -520;

            public const int DISPID_MOUSEPOINTER = -521;

            public const int DISPID_MOUSEICON = -522;

            public const int DISPID_PICTURE = -523;

            public const int DISPID_VALID = -524;

            public const int DISPID_READYSTATE = -525;

            public const int DISPID_REFRESH = -550;

            public const int DISPID_DOCLICK = -551;

            public const int DISPID_ABOUTBOX = -552;

            public const int DISPID_CLICK = -600;

            public const int DISPID_DBLCLICK = -601;

            public const int DISPID_KEYDOWN = -602;

            public const int DISPID_KEYPRESS = -603;

            public const int DISPID_KEYUP = -604;

            public const int DISPID_MOUSEDOWN = -605;

            public const int DISPID_MOUSEMOVE = -606;

            public const int DISPID_MOUSEUP = -607;

            public const int DISPID_ERROREVENT = -608;

            public const int DISPID_RIGHTTOLEFT = -611;

            public const int DISPID_READYSTATECHANGE = -609;

            public const int DISPID_AMBIENT_BACKCOLOR = -701;

            public const int DISPID_AMBIENT_DISPLAYNAME = -702;

            public const int DISPID_AMBIENT_FONT = -703;

            public const int DISPID_AMBIENT_FORECOLOR = -704;

            public const int DISPID_AMBIENT_LOCALEID = -705;

            public const int DISPID_AMBIENT_MESSAGEREFLECT = -706;

            public const int DISPID_AMBIENT_SCALEUNITS = -707;

            public const int DISPID_AMBIENT_TEXTALIGN = -708;

            public const int DISPID_AMBIENT_USERMODE = -709;

            public const int DISPID_AMBIENT_UIDEAD = -710;

            public const int DISPID_AMBIENT_SHOWGRABHANDLES = -711;

            public const int DISPID_AMBIENT_SHOWHATCHING = -712;

            public const int DISPID_AMBIENT_DISPLAYASDEFAULT = -713;

            public const int DISPID_AMBIENT_SUPPORTSMNEMONICS = -714;

            public const int DISPID_AMBIENT_AUTOCLIP = -715;

            public const int DISPID_AMBIENT_APPEARANCE = -716;

            public const int DISPID_AMBIENT_PALETTE = -726;

            public const int DISPID_AMBIENT_TRANSFERPRIORITY = -728;

            public const int DISPID_AMBIENT_RIGHTTOLEFT = -732;

            public const int DISPID_Name = -800;

            public const int DISPID_Delete = -801;

            public const int DISPID_Object = -802;

            public const int DISPID_Parent = -803;

            public const int DVASPECT_CONTENT = 1;

            public const int DVASPECT_THUMBNAIL = 2;

            public const int DVASPECT_ICON = 4;

            public const int DVASPECT_DOCPRINT = 8;

            public const int OLEMISC_RECOMPOSEONRESIZE = 1;

            public const int OLEMISC_ONLYICONIC = 2;

            public const int OLEMISC_INSERTNOTREPLACE = 4;

            public const int OLEMISC_STATIC = 8;

            public const int OLEMISC_CANTLINKINSIDE = 16;

            public const int OLEMISC_CANLINKBYOLE1 = 32;

            public const int OLEMISC_ISLINKOBJECT = 64;

            public const int OLEMISC_INSIDEOUT = 128;

            public const int OLEMISC_ACTIVATEWHENVISIBLE = 256;

            public const int OLEMISC_RENDERINGISDEVICEINDEPENDENT = 512;

            public const int OLEMISC_INVISIBLEATRUNTIME = 1024;

            public const int OLEMISC_ALWAYSRUN = 2048;

            public const int OLEMISC_ACTSLIKEBUTTON = 4096;

            public const int OLEMISC_ACTSLIKELABEL = 8192;

            public const int OLEMISC_NOUIACTIVATE = 16384;

            public const int OLEMISC_ALIGNABLE = 32768;

            public const int OLEMISC_SIMPLEFRAME = 65536;

            public const int OLEMISC_SETCLIENTSITEFIRST = 131072;

            public const int OLEMISC_IMEMODE = 262144;

            public const int OLEMISC_IGNOREACTIVATEWHENVISIBLE = 524288;

            public const int OLEMISC_WANTSTOMENUMERGE = 1048576;

            public const int OLEMISC_SUPPORTSMULTILEVELUNDO = 2097152;

            public const int QACONTAINER_SHOWHATCHING = 1;

            public const int QACONTAINER_SHOWGRABHANDLES = 2;

            public const int QACONTAINER_USERMODE = 4;

            public const int QACONTAINER_DISPLAYASDEFAULT = 8;

            public const int QACONTAINER_UIDEAD = 16;

            public const int QACONTAINER_AUTOCLIP = 32;

            public const int QACONTAINER_MESSAGEREFLECT = 64;

            public const int QACONTAINER_SUPPORTSMNEMONICS = 128;

            public const int XFORMCOORDS_POSITION = 1;

            public const int XFORMCOORDS_SIZE = 2;

            public const int XFORMCOORDS_HIMETRICTOCONTAINER = 4;

            public const int XFORMCOORDS_CONTAINERTOHIMETRIC = 8;

            public const int PROPCAT_Nil = -1;

            public const int PROPCAT_Misc = -2;

            public const int PROPCAT_Font = -3;

            public const int PROPCAT_Position = -4;

            public const int PROPCAT_Appearance = -5;

            public const int PROPCAT_Behavior = -6;

            public const int PROPCAT_Data = -7;

            public const int PROPCAT_List = -8;

            public const int PROPCAT_Text = -9;

            public const int PROPCAT_Scale = -10;

            public const int PROPCAT_DDE = -11;

            public const int GC_WCH_SIBLING = 1;

            public const int GC_WCH_CONTAINER = 2;

            public const int GC_WCH_CONTAINED = 3;

            public const int GC_WCH_ALL = 4;

            public const int GC_WCH_FREVERSEDIR = 134217728;

            public const int GC_WCH_FONLYNEXT = 268435456;

            public const int GC_WCH_FONLYPREV = 536870912;

            public const int GC_WCH_FSELECTED = 1073741824;

            public const int OLECONTF_EMBEDDINGS = 1;

            public const int OLECONTF_LINKS = 2;

            public const int OLECONTF_OTHERS = 4;

            public const int OLECONTF_ONLYUSER = 8;

            public const int OLECONTF_ONLYIFRUNNING = 16;

            public const int ALIGN_MIN = 0;

            public const int ALIGN_NO_CHANGE = 0;

            public const int ALIGN_TOP = 1;

            public const int ALIGN_BOTTOM = 2;

            public const int ALIGN_LEFT = 3;

            public const int ALIGN_RIGHT = 4;

            public const int ALIGN_MAX = 4;

            public const int OLEVERBATTRIB_NEVERDIRTIES = 1;

            public const int OLEVERBATTRIB_ONCONTAINERMENU = 2;

            public static Guid IID_IUnknown = new Guid("{00000000-0000-0000-C000-000000000046}");

            private ActiveX()
            {
            }
        }

        public static class Util
        {
            public static int MAKELONG(int low, int high)
            {
                return (high << 16) | (low & 0xFFFF);
            }

            public static IntPtr MAKELPARAM(int low, int high)
            {
                return (IntPtr)((high << 16) | (low & 0xFFFF));
            }

            public static int HIWORD(int n)
            {
                return (n >> 16) & 0xFFFF;
            }

            public static int HIWORD(IntPtr n)
            {
                return HIWORD((int)(long)n);
            }

            public static int LOWORD(int n)
            {
                return n & 0xFFFF;
            }

            public static int LOWORD(IntPtr n)
            {
                return LOWORD((int)(long)n);
            }

            public static int SignedHIWORD(IntPtr n)
            {
                return SignedHIWORD((int)(long)n);
            }

            public static int SignedLOWORD(IntPtr n)
            {
                return SignedLOWORD((int)(long)n);
            }

            public static int SignedHIWORD(int n)
            {
                return (short)((n >> 16) & 0xFFFF);
            }

            public static int SignedLOWORD(int n)
            {
                return (short)(n & 0xFFFF);
            }

            public static int GetPInvokeStringLength(string s)
            {
                if (s == null)
                {
                    return 0;
                }
                if (Marshal.SystemDefaultCharSize == 2)
                {
                    return s.Length;
                }
                if (s.Length == 0)
                {
                    return 0;
                }
                if (s.IndexOf('\0') > -1)
                {
                    return GetEmbeddedNullStringLengthAnsi(s);
                }
                return lstrlen(s);
            }

            private static int GetEmbeddedNullStringLengthAnsi(string s)
            {
                int num = s.IndexOf('\0');
                if (num > -1)
                {
                    string s2 = s.Substring(0, num);
                    string s3 = s.Substring(num + 1);
                    return GetPInvokeStringLength(s2) + GetEmbeddedNullStringLengthAnsi(s3) + 1;
                }
                return GetPInvokeStringLength(s);
            }

            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            private static extern int lstrlen(string s);
        }

        public enum tagTYPEKIND
        {
            TKIND_ENUM,
            TKIND_RECORD,
            TKIND_MODULE,
            TKIND_INTERFACE,
            TKIND_DISPATCH,
            TKIND_COCLASS,
            TKIND_ALIAS,
            TKIND_UNION,
            TKIND_MAX
        }

        [StructLayout(LayoutKind.Sequential)]
        public class tagTYPEDESC
        {
            public IntPtr unionMember;

            public short vt;
        }

        public struct tagPARAMDESC
        {
            public IntPtr pparamdescex;

            [MarshalAs(UnmanagedType.U2)]
            public short wParamFlags;
        }

        public sealed class CommonHandles
        {
            public static readonly int Accelerator;

            public static readonly int Cursor;

            public static readonly int EMF;

            public static readonly int Find;

            public static readonly int GDI;

            public static readonly int HDC;

            public static readonly int CompatibleHDC;

            public static readonly int Icon;

            public static readonly int Kernel;

            public static readonly int Menu;

            public static readonly int Window;

            static CommonHandles()
            {
                Accelerator = System.Internal.HandleCollector.RegisterType("Accelerator", 80, 50);
                Cursor = System.Internal.HandleCollector.RegisterType("Cursor", 20, 500);
                EMF = System.Internal.HandleCollector.RegisterType("EnhancedMetaFile", 20, 500);
                Find = System.Internal.HandleCollector.RegisterType("Find", 0, 1000);
                GDI = System.Internal.HandleCollector.RegisterType("GDI", 50, 500);
                HDC = System.Internal.HandleCollector.RegisterType("HDC", 100, 2);
                CompatibleHDC = System.Internal.HandleCollector.RegisterType("ComptibleHDC", 50, 50);
                Icon = System.Internal.HandleCollector.RegisterType("Icon", 20, 500);
                Kernel = System.Internal.HandleCollector.RegisterType("Kernel", 0, 1000);
                Menu = System.Internal.HandleCollector.RegisterType("Menu", 30, 1000);
                Window = System.Internal.HandleCollector.RegisterType("Window", 5, 1000);
            }
        }

        public enum tagSYSKIND
        {
            SYS_WIN16 = 0,
            SYS_MAC = 2
        }

        public delegate bool MonitorEnumProc(IntPtr monitor, IntPtr hdc, IntPtr lprcMonitor, IntPtr lParam);

        [ComImport]
        [Guid("A7ABA9C1-8983-11cf-8F20-00805F2CD064")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IProvideMultipleClassInfo
        {
            [PreserveSig]
            UnsafeNativeMethods.ITypeInfo GetClassInfo();

            [PreserveSig]
            int GetGUID(int dwGuidKind, [In][Out] ref Guid pGuid);

            [PreserveSig]
            int GetMultiTypeInfoCount([In][Out] ref int pcti);

            [PreserveSig]
            int GetInfoOfIndex(int iti, int dwFlags, [In][Out] ref UnsafeNativeMethods.ITypeInfo pTypeInfo, int pTIFlags, int pcdispidReserved, IntPtr piidPrimary, IntPtr piidSource);
        }

        [StructLayout(LayoutKind.Sequential)]
        public class EVENTMSG
        {
            public int message;

            public int paramL;

            public int paramH;

            public int time;

            public IntPtr hwnd;
        }

        [ComImport]
        [Guid("B196B283-BAB4-101A-B69C-00AA00341D07")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IProvideClassInfo
        {
            [return: MarshalAs(UnmanagedType.Interface)]
            UnsafeNativeMethods.ITypeInfo GetClassInfo();
        }

        [StructLayout(LayoutKind.Sequential)]
        public sealed class tagTYPEATTR
        {
            public Guid guid;

            [MarshalAs(UnmanagedType.U4)]
            public int lcid;

            [MarshalAs(UnmanagedType.U4)]
            public int dwReserved;

            public int memidConstructor;

            public int memidDestructor;

            public IntPtr lpstrSchema = IntPtr.Zero;

            [MarshalAs(UnmanagedType.U4)]
            public int cbSizeInstance;

            public int typekind;

            [MarshalAs(UnmanagedType.U2)]
            public short cFuncs;

            [MarshalAs(UnmanagedType.U2)]
            public short cVars;

            [MarshalAs(UnmanagedType.U2)]
            public short cImplTypes;

            [MarshalAs(UnmanagedType.U2)]
            public short cbSizeVft;

            [MarshalAs(UnmanagedType.U2)]
            public short cbAlignment;

            [MarshalAs(UnmanagedType.U2)]
            public short wTypeFlags;

            [MarshalAs(UnmanagedType.U2)]
            public short wMajorVerNum;

            [MarshalAs(UnmanagedType.U2)]
            public short wMinorVerNum;

            [MarshalAs(UnmanagedType.U4)]
            public int tdescAlias_unionMember;

            [MarshalAs(UnmanagedType.U2)]
            public short tdescAlias_vt;

            [MarshalAs(UnmanagedType.U4)]
            public int idldescType_dwReserved;

            [MarshalAs(UnmanagedType.U2)]
            public short idldescType_wIDLFlags;

            public tagTYPEDESC Get_tdescAlias()
            {
                tagTYPEDESC tagTYPEDESC = new tagTYPEDESC();
                tagTYPEDESC.unionMember = (IntPtr)tdescAlias_unionMember;
                tagTYPEDESC.vt = tdescAlias_vt;
                return tagTYPEDESC;
            }

            public tagIDLDESC Get_idldescType()
            {
                tagIDLDESC result = default(tagIDLDESC);
                result.dwReserved = idldescType_dwReserved;
                result.wIDLFlags = idldescType_wIDLFlags;
                return result;
            }
        }

        public enum tagVARFLAGS
        {
            VARFLAG_FREADONLY = 1,
            VARFLAG_FSOURCE = 2,
            VARFLAG_FBINDABLE = 4,
            VARFLAG_FREQUESTEDIT = 8,
            VARFLAG_FDISPLAYBIND = 0x10,
            VARFLAG_FDEFAULTBIND = 0x20,
            VARFLAG_FHIDDEN = 0x40,
            VARFLAG_FDEFAULTCOLLELEM = 0x100,
            VARFLAG_FUIDEFAULT = 0x200,
            VARFLAG_FNONBROWSABLE = 0x400,
            VARFLAG_FREPLACEABLE = 0x800,
            VARFLAG_FIMMEDIATEBIND = 0x1000
        }

        [StructLayout(LayoutKind.Sequential)]
        public sealed class tagELEMDESC
        {
            public tagTYPEDESC tdesc;

            public tagPARAMDESC paramdesc;
        }

        public enum tagVARKIND
        {
            VAR_PERINSTANCE,
            VAR_STATIC,
            VAR_CONST,
            VAR_DISPATCH
        }

        public struct tagIDLDESC
        {
            [MarshalAs(UnmanagedType.U4)]
            public int dwReserved;

            [MarshalAs(UnmanagedType.U2)]
            public short wIDLFlags;
        }

        public struct RGBQUAD
        {
            public byte rgbBlue;

            public byte rgbGreen;

            public byte rgbRed;

            public byte rgbReserved;
        }

        public struct PALETTEENTRY
        {
            public byte peRed;

            public byte peGreen;

            public byte peBlue;

            public byte peFlags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct BITMAPINFO_FLAT
        {
            public int bmiHeader_biSize;

            public int bmiHeader_biWidth;

            public int bmiHeader_biHeight;

            public short bmiHeader_biPlanes;

            public short bmiHeader_biBitCount;

            public int bmiHeader_biCompression;

            public int bmiHeader_biSizeImage;

            public int bmiHeader_biXPelsPerMeter;

            public int bmiHeader_biYPelsPerMeter;

            public int bmiHeader_biClrUsed;

            public int bmiHeader_biClrImportant;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
            public byte[] bmiColors;
        }

        public struct SYSTEM_POWER_STATUS
        {
            public byte ACLineStatus;

            public byte BatteryFlag;

            public byte BatteryLifePercent;

            public byte Reserved1;

            public int BatteryLifeTime;

            public int BatteryFullLifeTime;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal class DLLVERSIONINFO
        {
            internal uint cbSize;

            internal uint dwMajorVersion;

            internal uint dwMinorVersion;

            internal uint dwBuildNumber;

            internal uint dwPlatformID;
        }

        public enum OLERENDER
        {
            OLERENDER_NONE,
            OLERENDER_DRAW,
            OLERENDER_FORMAT,
            OLERENDER_ASIS
        }

        public enum PROCESS_DPI_AWARENESS
        {
            PROCESS_DPI_UNINITIALIZED = -1,
            PROCESS_DPI_UNAWARE,
            PROCESS_SYSTEM_DPI_AWARE,
            PROCESS_PER_MONITOR_DPI_AWARE
        }

        public enum MONTCALENDAR_VIEW_MODE
        {
            MCMV_MONTH,
            MCMV_YEAR,
            MCMV_DECADE,
            MCMV_CENTURY
        }

        public struct UiaRect
        {
            public double left;

            public double top;

            public double width;

            public double height;

            public UiaRect(Rectangle r)
            {
                left = r.Left;
                top = r.Top;
                width = r.Width;
                height = r.Height;
            }
        }

        public static IntPtr InvalidIntPtr;

        public static IntPtr LPSTR_TEXTCALLBACK;

        public static HandleRef NullHandleRef;

        public const int BITMAPINFO_MAX_COLORSIZE = 256;

        public const int BI_BITFIELDS = 3;

        public const int STATUS_PENDING = 259;

        public const int DESKTOP_SWITCHDESKTOP = 256;

        public const int ERROR_ACCESS_DENIED = 5;

        public const int FW_DONTCARE = 0;

        public const int FW_NORMAL = 400;

        public const int FW_BOLD = 700;

        public const int ANSI_CHARSET = 0;

        public const int DEFAULT_CHARSET = 1;

        public const int OUT_DEFAULT_PRECIS = 0;

        public const int OUT_TT_PRECIS = 4;

        public const int OUT_TT_ONLY_PRECIS = 7;

        public const int ALTERNATE = 1;

        public const int WINDING = 2;

        public const int TA_DEFAULT = 0;

        public const int BS_SOLID = 0;

        public const int HOLLOW_BRUSH = 5;

        public const int R2_BLACK = 1;

        public const int R2_NOTMERGEPEN = 2;

        public const int R2_MASKNOTPEN = 3;

        public const int R2_NOTCOPYPEN = 4;

        public const int R2_MASKPENNOT = 5;

        public const int R2_NOT = 6;

        public const int R2_XORPEN = 7;

        public const int R2_NOTMASKPEN = 8;

        public const int R2_MASKPEN = 9;

        public const int R2_NOTXORPEN = 10;

        public const int R2_NOP = 11;

        public const int R2_MERGENOTPEN = 12;

        public const int R2_COPYPEN = 13;

        public const int R2_MERGEPENNOT = 14;

        public const int R2_MERGEPEN = 15;

        public const int R2_WHITE = 16;

        public const int GM_COMPATIBLE = 1;

        public const int GM_ADVANCED = 2;

        public const int MWT_IDENTITY = 1;

        public const int PAGE_READONLY = 2;

        public const int PAGE_READWRITE = 4;

        public const int PAGE_WRITECOPY = 8;

        public const int FILE_MAP_COPY = 1;

        public const int FILE_MAP_WRITE = 2;

        public const int FILE_MAP_READ = 4;

        public const int SHGFI_ICON = 256;

        public const int SHGFI_DISPLAYNAME = 512;

        public const int SHGFI_TYPENAME = 1024;

        public const int SHGFI_ATTRIBUTES = 2048;

        public const int SHGFI_ICONLOCATION = 4096;

        public const int SHGFI_EXETYPE = 8192;

        public const int SHGFI_SYSICONINDEX = 16384;

        public const int SHGFI_LINKOVERLAY = 32768;

        public const int SHGFI_SELECTED = 65536;

        public const int SHGFI_ATTR_SPECIFIED = 131072;

        public const int SHGFI_LARGEICON = 0;

        public const int SHGFI_SMALLICON = 1;

        public const int SHGFI_OPENICON = 2;

        public const int SHGFI_SHELLICONSIZE = 4;

        public const int SHGFI_PIDL = 8;

        public const int SHGFI_USEFILEATTRIBUTES = 16;

        public const int SHGFI_ADDOVERLAYS = 32;

        public const int SHGFI_OVERLAYINDEX = 64;

        public const int DM_DISPLAYORIENTATION = 128;

        public const int AUTOSUGGEST = 268435456;

        public const int AUTOSUGGEST_OFF = 536870912;

        public const int AUTOAPPEND = 1073741824;

        public const int AUTOAPPEND_OFF = int.MinValue;

        public const int ARW_BOTTOMLEFT = 0;

        public const int ARW_BOTTOMRIGHT = 1;

        public const int ARW_TOPLEFT = 2;

        public const int ARW_TOPRIGHT = 3;

        public const int ARW_LEFT = 0;

        public const int ARW_RIGHT = 0;

        public const int ARW_UP = 4;

        public const int ARW_DOWN = 4;

        public const int ARW_HIDE = 8;

        public const int ACM_OPENA = 1124;

        public const int ACM_OPENW = 1127;

        public const int ADVF_NODATA = 1;

        public const int ADVF_ONLYONCE = 4;

        public const int ADVF_PRIMEFIRST = 2;

        public const int BCM_GETIDEALSIZE = 5633;

        public const int BI_RGB = 0;

        public const int BS_PATTERN = 3;

        public const int BITSPIXEL = 12;

        public const int BDR_RAISEDOUTER = 1;

        public const int BDR_SUNKENOUTER = 2;

        public const int BDR_RAISEDINNER = 4;

        public const int BDR_SUNKENINNER = 8;

        public const int BDR_RAISED = 5;

        public const int BDR_SUNKEN = 10;

        public const int BF_LEFT = 1;

        public const int BF_TOP = 2;

        public const int BF_RIGHT = 4;

        public const int BF_BOTTOM = 8;

        public const int BF_ADJUST = 8192;

        public const int BF_FLAT = 16384;

        public const int BF_MIDDLE = 2048;

        public const int BFFM_INITIALIZED = 1;

        public const int BFFM_SELCHANGED = 2;

        public const int BFFM_SETSELECTIONA = 1126;

        public const int BFFM_SETSELECTIONW = 1127;

        public const int BFFM_ENABLEOK = 1125;

        public const int BS_PUSHBUTTON = 0;

        public const int BS_DEFPUSHBUTTON = 1;

        public const int BS_MULTILINE = 8192;

        public const int BS_PUSHLIKE = 4096;

        public const int BS_OWNERDRAW = 11;

        public const int BS_RADIOBUTTON = 4;

        public const int BS_3STATE = 5;

        public const int BS_GROUPBOX = 7;

        public const int BS_LEFT = 256;

        public const int BS_RIGHT = 512;

        public const int BS_CENTER = 768;

        public const int BS_TOP = 1024;

        public const int BS_BOTTOM = 2048;

        public const int BS_VCENTER = 3072;

        public const int BS_RIGHTBUTTON = 32;

        public const int BN_CLICKED = 0;

        public const int BM_SETCHECK = 241;

        public const int BM_SETSTATE = 243;

        public const int BM_CLICK = 245;

        public const int CDERR_DIALOGFAILURE = 65535;

        public const int CDERR_STRUCTSIZE = 1;

        public const int CDERR_INITIALIZATION = 2;

        public const int CDERR_NOTEMPLATE = 3;

        public const int CDERR_NOHINSTANCE = 4;

        public const int CDERR_LOADSTRFAILURE = 5;

        public const int CDERR_FINDRESFAILURE = 6;

        public const int CDERR_LOADRESFAILURE = 7;

        public const int CDERR_LOCKRESFAILURE = 8;

        public const int CDERR_MEMALLOCFAILURE = 9;

        public const int CDERR_MEMLOCKFAILURE = 10;

        public const int CDERR_NOHOOK = 11;

        public const int CDERR_REGISTERMSGFAIL = 12;

        public const int CFERR_NOFONTS = 8193;

        public const int CFERR_MAXLESSTHANMIN = 8194;

        public const int CC_RGBINIT = 1;

        public const int CC_FULLOPEN = 2;

        public const int CC_PREVENTFULLOPEN = 4;

        public const int CC_SHOWHELP = 8;

        public const int CC_ENABLEHOOK = 16;

        public const int CC_SOLIDCOLOR = 128;

        public const int CC_ANYCOLOR = 256;

        public const int CF_SCREENFONTS = 1;

        public const int CF_SHOWHELP = 4;

        public const int CF_ENABLEHOOK = 8;

        public const int CF_INITTOLOGFONTSTRUCT = 64;

        public const int CF_EFFECTS = 256;

        public const int CF_APPLY = 512;

        public const int CF_SCRIPTSONLY = 1024;

        public const int CF_NOVECTORFONTS = 2048;

        public const int CF_NOSIMULATIONS = 4096;

        public const int CF_LIMITSIZE = 8192;

        public const int CF_FIXEDPITCHONLY = 16384;

        public const int CF_FORCEFONTEXIST = 65536;

        public const int CF_TTONLY = 262144;

        public const int CF_SELECTSCRIPT = 4194304;

        public const int CF_NOVERTFONTS = 16777216;

        public const int CP_WINANSI = 1004;

        public const int cmb4 = 1139;

        public const int CS_DBLCLKS = 8;

        public const int CS_DROPSHADOW = 131072;

        public const int CS_SAVEBITS = 2048;

        public const int CF_TEXT = 1;

        public const int CF_BITMAP = 2;

        public const int CF_METAFILEPICT = 3;

        public const int CF_SYLK = 4;

        public const int CF_DIF = 5;

        public const int CF_TIFF = 6;

        public const int CF_OEMTEXT = 7;

        public const int CF_DIB = 8;

        public const int CF_PALETTE = 9;

        public const int CF_PENDATA = 10;

        public const int CF_RIFF = 11;

        public const int CF_WAVE = 12;

        public const int CF_UNICODETEXT = 13;

        public const int CF_ENHMETAFILE = 14;

        public const int CF_HDROP = 15;

        public const int CF_LOCALE = 16;

        public const int CLSCTX_INPROC_SERVER = 1;

        public const int CLSCTX_LOCAL_SERVER = 4;

        public const int CW_USEDEFAULT = int.MinValue;

        public const int CWP_SKIPINVISIBLE = 1;

        public const int COLOR_WINDOW = 5;

        public const int CB_ERR = -1;

        public const int CBN_SELCHANGE = 1;

        public const int CBN_DBLCLK = 2;

        public const int CBN_EDITCHANGE = 5;

        public const int CBN_EDITUPDATE = 6;

        public const int CBN_DROPDOWN = 7;

        public const int CBN_CLOSEUP = 8;

        public const int CBN_SELENDOK = 9;

        public const int CBS_SIMPLE = 1;

        public const int CBS_DROPDOWN = 2;

        public const int CBS_DROPDOWNLIST = 3;

        public const int CBS_OWNERDRAWFIXED = 16;

        public const int CBS_OWNERDRAWVARIABLE = 32;

        public const int CBS_AUTOHSCROLL = 64;

        public const int CBS_HASSTRINGS = 512;

        public const int CBS_NOINTEGRALHEIGHT = 1024;

        public const int CB_GETEDITSEL = 320;

        public const int CB_LIMITTEXT = 321;

        public const int CB_SETEDITSEL = 322;

        public const int CB_ADDSTRING = 323;

        public const int CB_DELETESTRING = 324;

        public const int CB_GETCURSEL = 327;

        public const int CB_GETLBTEXT = 328;

        public const int CB_GETLBTEXTLEN = 329;

        public const int CB_INSERTSTRING = 330;

        public const int CB_RESETCONTENT = 331;

        public const int CB_FINDSTRING = 332;

        public const int CB_SETCURSEL = 334;

        public const int CB_SHOWDROPDOWN = 335;

        public const int CB_GETITEMDATA = 336;

        public const int CB_SETITEMHEIGHT = 339;

        public const int CB_GETITEMHEIGHT = 340;

        public const int CB_GETDROPPEDSTATE = 343;

        public const int CB_FINDSTRINGEXACT = 344;

        public const int CB_GETTOPINDEX = 347;

        public const int CB_SETTOPINDEX = 348;

        public const int CB_GETDROPPEDWIDTH = 351;

        public const int CB_SETDROPPEDWIDTH = 352;

        public const int CDRF_DODEFAULT = 0;

        public const int CDRF_NEWFONT = 2;

        public const int CDRF_SKIPDEFAULT = 4;

        public const int CDRF_NOTIFYPOSTPAINT = 16;

        public const int CDRF_NOTIFYITEMDRAW = 32;

        public const int CDRF_NOTIFYSUBITEMDRAW = 32;

        public const int CDDS_PREPAINT = 1;

        public const int CDDS_POSTPAINT = 2;

        public const int CDDS_ITEM = 65536;

        public const int CDDS_SUBITEM = 131072;

        public const int CDDS_ITEMPREPAINT = 65537;

        public const int CDDS_ITEMPOSTPAINT = 65538;

        public const int CDIS_SELECTED = 1;

        public const int CDIS_GRAYED = 2;

        public const int CDIS_DISABLED = 4;

        public const int CDIS_CHECKED = 8;

        public const int CDIS_FOCUS = 16;

        public const int CDIS_DEFAULT = 32;

        public const int CDIS_HOT = 64;

        public const int CDIS_MARKED = 128;

        public const int CDIS_INDETERMINATE = 256;

        public const int CDIS_SHOWKEYBOARDCUES = 512;

        public const int CLR_NONE = -1;

        public const int CLR_DEFAULT = -16777216;

        public const int CCM_SETVERSION = 8199;

        public const int CCM_GETVERSION = 8200;

        public const int CCS_NORESIZE = 4;

        public const int CCS_NOPARENTALIGN = 8;

        public const int CCS_NODIVIDER = 64;

        public const int CBEM_INSERTITEMA = 1025;

        public const int CBEM_GETITEMA = 1028;

        public const int CBEM_SETITEMA = 1029;

        public const int CBEM_INSERTITEMW = 1035;

        public const int CBEM_SETITEMW = 1036;

        public const int CBEM_GETITEMW = 1037;

        public const int CBEN_ENDEDITA = -805;

        public const int CBEN_ENDEDITW = -806;

        public const int CONNECT_E_NOCONNECTION = -2147220992;

        public const int CONNECT_E_CANNOTCONNECT = -2147220990;

        public const int CTRLINFO_EATS_RETURN = 1;

        public const int CTRLINFO_EATS_ESCAPE = 2;

        public const int CSIDL_DESKTOP = 0;

        public const int CSIDL_INTERNET = 1;

        public const int CSIDL_PROGRAMS = 2;

        public const int CSIDL_PERSONAL = 5;

        public const int CSIDL_FAVORITES = 6;

        public const int CSIDL_STARTUP = 7;

        public const int CSIDL_RECENT = 8;

        public const int CSIDL_SENDTO = 9;

        public const int CSIDL_STARTMENU = 11;

        public const int CSIDL_DESKTOPDIRECTORY = 16;

        public const int CSIDL_TEMPLATES = 21;

        public const int CSIDL_APPDATA = 26;

        public const int CSIDL_LOCAL_APPDATA = 28;

        public const int CSIDL_INTERNET_CACHE = 32;

        public const int CSIDL_COOKIES = 33;

        public const int CSIDL_HISTORY = 34;

        public const int CSIDL_COMMON_APPDATA = 35;

        public const int CSIDL_SYSTEM = 37;

        public const int CSIDL_PROGRAM_FILES = 38;

        public const int CSIDL_PROGRAM_FILES_COMMON = 43;

        public const int DUPLICATE = 6;

        public const int DISPID_UNKNOWN = -1;

        public const int DISPID_PROPERTYPUT = -3;

        public const int DISPATCH_METHOD = 1;

        public const int DISPATCH_PROPERTYGET = 2;

        public const int DISPATCH_PROPERTYPUT = 4;

        public const int DV_E_DVASPECT = -2147221397;

        public const int DISP_E_MEMBERNOTFOUND = -2147352573;

        public const int DISP_E_PARAMNOTFOUND = -2147352572;

        public const int DISP_E_EXCEPTION = -2147352567;

        public const int DEFAULT_GUI_FONT = 17;

        public const int DIB_RGB_COLORS = 0;

        public const int DRAGDROP_E_NOTREGISTERED = -2147221248;

        public const int DRAGDROP_E_ALREADYREGISTERED = -2147221247;

        public const int DUPLICATE_SAME_ACCESS = 2;

        public const int DFC_CAPTION = 1;

        public const int DFC_MENU = 2;

        public const int DFC_SCROLL = 3;

        public const int DFC_BUTTON = 4;

        public const int DFCS_CAPTIONCLOSE = 0;

        public const int DFCS_CAPTIONMIN = 1;

        public const int DFCS_CAPTIONMAX = 2;

        public const int DFCS_CAPTIONRESTORE = 3;

        public const int DFCS_CAPTIONHELP = 4;

        public const int DFCS_MENUARROW = 0;

        public const int DFCS_MENUCHECK = 1;

        public const int DFCS_MENUBULLET = 2;

        public const int DFCS_SCROLLUP = 0;

        public const int DFCS_SCROLLDOWN = 1;

        public const int DFCS_SCROLLLEFT = 2;

        public const int DFCS_SCROLLRIGHT = 3;

        public const int DFCS_SCROLLCOMBOBOX = 5;

        public const int DFCS_BUTTONCHECK = 0;

        public const int DFCS_BUTTONRADIO = 4;

        public const int DFCS_BUTTON3STATE = 8;

        public const int DFCS_BUTTONPUSH = 16;

        public const int DFCS_INACTIVE = 256;

        public const int DFCS_PUSHED = 512;

        public const int DFCS_CHECKED = 1024;

        public const int DFCS_FLAT = 16384;

        public const int DCX_WINDOW = 1;

        public const int DCX_CACHE = 2;

        public const int DCX_LOCKWINDOWUPDATE = 1024;

        public const int DCX_INTERSECTRGN = 128;

        public const int DI_NORMAL = 3;

        public const int DLGC_WANTARROWS = 1;

        public const int DLGC_WANTTAB = 2;

        public const int DLGC_WANTALLKEYS = 4;

        public const int DLGC_WANTCHARS = 128;

        public const int DLGC_WANTMESSAGE = 4;

        public const int DLGC_HASSETSEL = 8;

        public const int DTM_GETSYSTEMTIME = 4097;

        public const int DTM_SETSYSTEMTIME = 4098;

        public const int DTM_SETRANGE = 4100;

        public const int DTM_SETFORMATA = 4101;

        public const int DTM_SETFORMATW = 4146;

        public const int DTM_SETMCCOLOR = 4102;

        public const int DTM_GETMONTHCAL = 4104;

        public const int DTM_SETMCFONT = 4105;

        public const int DTS_UPDOWN = 1;

        public const int DTS_SHOWNONE = 2;

        public const int DTS_LONGDATEFORMAT = 4;

        public const int DTS_TIMEFORMAT = 9;

        public const int DTS_RIGHTALIGN = 32;

        public const int DTN_DATETIMECHANGE = -759;

        public const int DTN_USERSTRINGA = -758;

        public const int DTN_USERSTRINGW = -745;

        public const int DTN_WMKEYDOWNA = -757;

        public const int DTN_WMKEYDOWNW = -744;

        public const int DTN_FORMATA = -756;

        public const int DTN_FORMATW = -743;

        public const int DTN_FORMATQUERYA = -755;

        public const int DTN_FORMATQUERYW = -742;

        public const int DTN_DROPDOWN = -754;

        public const int DTN_CLOSEUP = -753;

        public const int DVASPECT_CONTENT = 1;

        public const int DVASPECT_TRANSPARENT = 32;

        public const int DVASPECT_OPAQUE = 16;

        public const int E_NOTIMPL = -2147467263;

        public const int E_OUTOFMEMORY = -2147024882;

        public const int E_INVALIDARG = -2147024809;

        public const int E_NOINTERFACE = -2147467262;

        public const int E_POINTER = -2147467261;

        public const int E_FAIL = -2147467259;

        public const int E_ABORT = -2147467260;

        public const int E_UNEXPECTED = -2147418113;

        public const int INET_E_DEFAULT_ACTION = -2146697199;

        public const int ETO_OPAQUE = 2;

        public const int ETO_CLIPPED = 4;

        public const int EMR_POLYTEXTOUTA = 96;

        public const int EMR_POLYTEXTOUTW = 97;

        public const int EDGE_RAISED = 5;

        public const int EDGE_SUNKEN = 10;

        public const int EDGE_ETCHED = 6;

        public const int EDGE_BUMP = 9;

        public const int ES_LEFT = 0;

        public const int ES_CENTER = 1;

        public const int ES_RIGHT = 2;

        public const int ES_MULTILINE = 4;

        public const int ES_UPPERCASE = 8;

        public const int ES_LOWERCASE = 16;

        public const int ES_AUTOVSCROLL = 64;

        public const int ES_AUTOHSCROLL = 128;

        public const int ES_NOHIDESEL = 256;

        public const int ES_READONLY = 2048;

        public const int ES_PASSWORD = 32;

        public const int EN_CHANGE = 768;

        public const int EN_UPDATE = 1024;

        public const int EN_HSCROLL = 1537;

        public const int EN_VSCROLL = 1538;

        public const int EN_ALIGN_LTR_EC = 1792;

        public const int EN_ALIGN_RTL_EC = 1793;

        public const int EC_LEFTMARGIN = 1;

        public const int EC_RIGHTMARGIN = 2;

        public const int EM_GETSEL = 176;

        public const int EM_SETSEL = 177;

        public const int EM_SCROLL = 181;

        public const int EM_SCROLLCARET = 183;

        public const int EM_GETMODIFY = 184;

        public const int EM_SETMODIFY = 185;

        public const int EM_GETLINECOUNT = 186;

        public const int EM_REPLACESEL = 194;

        public const int EM_GETLINE = 196;

        public const int EM_LIMITTEXT = 197;

        public const int EM_CANUNDO = 198;

        public const int EM_UNDO = 199;

        public const int EM_SETPASSWORDCHAR = 204;

        public const int EM_GETPASSWORDCHAR = 210;

        public const int EM_EMPTYUNDOBUFFER = 205;

        public const int EM_SETREADONLY = 207;

        public const int EM_SETMARGINS = 211;

        public const int EM_POSFROMCHAR = 214;

        public const int EM_CHARFROMPOS = 215;

        public const int EM_LINEFROMCHAR = 201;

        public const int EM_GETFIRSTVISIBLELINE = 206;

        public const int EM_LINEINDEX = 187;

        public const int ERROR_INVALID_HANDLE = 6;

        public const int ERROR_CLASS_ALREADY_EXISTS = 1410;

        public const int FNERR_SUBCLASSFAILURE = 12289;

        public const int FNERR_INVALIDFILENAME = 12290;

        public const int FNERR_BUFFERTOOSMALL = 12291;

        public const int FRERR_BUFFERLENGTHZERO = 16385;

        public const int FADF_BSTR = 256;

        public const int FADF_UNKNOWN = 512;

        public const int FADF_DISPATCH = 1024;

        public const int FADF_VARIANT = 2048;

        public const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;

        public const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;

        public const int FVIRTKEY = 1;

        public const int FSHIFT = 4;

        public const int FALT = 16;

        public const int GMEM_FIXED = 0;

        public const int GMEM_MOVEABLE = 2;

        public const int GMEM_NOCOMPACT = 16;

        public const int GMEM_NODISCARD = 32;

        public const int GMEM_ZEROINIT = 64;

        public const int GMEM_MODIFY = 128;

        public const int GMEM_DISCARDABLE = 256;

        public const int GMEM_NOT_BANKED = 4096;

        public const int GMEM_SHARE = 8192;

        public const int GMEM_DDESHARE = 8192;

        public const int GMEM_NOTIFY = 16384;

        public const int GMEM_LOWER = 4096;

        public const int GMEM_VALID_FLAGS = 32626;

        public const int GMEM_INVALID_HANDLE = 32768;

        public const int GHND = 66;

        public const int GPTR = 64;

        public const int GCL_WNDPROC = -24;

        public const int GWL_WNDPROC = -4;

        public const int GWL_HWNDPARENT = -8;

        public const int GWL_STYLE = -16;

        public const int GWL_EXSTYLE = -20;

        public const int GWL_ID = -12;

        public const int GW_HWNDFIRST = 0;

        public const int GW_HWNDLAST = 1;

        public const int GW_HWNDNEXT = 2;

        public const int GW_HWNDPREV = 3;

        public const int GW_CHILD = 5;

        public const int GMR_VISIBLE = 0;

        public const int GMR_DAYSTATE = 1;

        public const int GDI_ERROR = -1;

        public const int GDTR_MIN = 1;

        public const int GDTR_MAX = 2;

        public const int GDT_VALID = 0;

        public const int GDT_NONE = 1;

        public const int GA_PARENT = 1;

        public const int GA_ROOT = 2;

        public const int GCS_COMPSTR = 8;

        public const int GCS_COMPATTR = 16;

        public const int GCS_RESULTSTR = 2048;

        public const int ATTR_INPUT = 0;

        public const int ATTR_TARGET_CONVERTED = 1;

        public const int ATTR_CONVERTED = 2;

        public const int ATTR_TARGET_NOTCONVERTED = 3;

        public const int ATTR_INPUT_ERROR = 4;

        public const int ATTR_FIXEDCONVERTED = 5;

        public const int NI_COMPOSITIONSTR = 21;

        public const int CPS_COMPLETE = 1;

        public const int CPS_CANCEL = 4;

        public const int HC_ACTION = 0;

        public const int HC_GETNEXT = 1;

        public const int HC_SKIP = 2;

        public const int HTTRANSPARENT = -1;

        public const int HTNOWHERE = 0;

        public const int HTCLIENT = 1;

        public const int HTLEFT = 10;

        public const int HTBOTTOM = 15;

        public const int HTBOTTOMLEFT = 16;

        public const int HTBOTTOMRIGHT = 17;

        public const int HTBORDER = 18;

        public const int HELPINFO_WINDOW = 1;

        public const int HCF_HIGHCONTRASTON = 1;

        public const int HDI_ORDER = 128;

        public const int HDI_WIDTH = 1;

        public const int HDM_GETITEMCOUNT = 4608;

        public const int HDM_INSERTITEMA = 4609;

        public const int HDM_INSERTITEMW = 4618;

        public const int HDM_GETITEMA = 4611;

        public const int HDM_GETITEMW = 4619;

        public const int HDM_LAYOUT = 4613;

        public const int HDM_SETITEMA = 4612;

        public const int HDM_SETITEMW = 4620;

        public const int HDN_ITEMCHANGINGA = -300;

        public const int HDN_ITEMCHANGINGW = -320;

        public const int HDN_ITEMCHANGEDA = -301;

        public const int HDN_ITEMCHANGEDW = -321;

        public const int HDN_ITEMCLICKA = -302;

        public const int HDN_ITEMCLICKW = -322;

        public const int HDN_ITEMDBLCLICKA = -303;

        public const int HDN_ITEMDBLCLICKW = -323;

        public const int HDN_DIVIDERDBLCLICKA = -305;

        public const int HDN_DIVIDERDBLCLICKW = -325;

        public const int HDN_BEGINTDRAG = -310;

        public const int HDN_BEGINTRACKA = -306;

        public const int HDN_BEGINTRACKW = -326;

        public const int HDN_ENDDRAG = -311;

        public const int HDN_ENDTRACKA = -307;

        public const int HDN_ENDTRACKW = -327;

        public const int HDN_TRACKA = -308;

        public const int HDN_TRACKW = -328;

        public const int HDN_GETDISPINFOA = -309;

        public const int HDN_GETDISPINFOW = -329;

        public const int HDS_FULLDRAG = 128;

        public const int HBMMENU_CALLBACK = -1;

        public const int HBMMENU_SYSTEM = 1;

        public const int HBMMENU_MBAR_RESTORE = 2;

        public const int HBMMENU_MBAR_MINIMIZE = 3;

        public const int HBMMENU_MBAR_CLOSE = 5;

        public const int HBMMENU_MBAR_CLOSE_D = 6;

        public const int HBMMENU_MBAR_MINIMIZE_D = 7;

        public const int HBMMENU_POPUP_CLOSE = 8;

        public const int HBMMENU_POPUP_RESTORE = 9;

        public const int HBMMENU_POPUP_MAXIMIZE = 10;

        public const int HBMMENU_POPUP_MINIMIZE = 11;

        public static HandleRef HWND_TOP;

        public static HandleRef HWND_BOTTOM;

        public static HandleRef HWND_TOPMOST;

        public static HandleRef HWND_NOTOPMOST;

        public static HandleRef HWND_MESSAGE;

        public const int IME_CMODE_NATIVE = 1;

        public const int IME_CMODE_KATAKANA = 2;

        public const int IME_CMODE_FULLSHAPE = 8;

        public const int INPLACE_E_NOTOOLSPACE = -2147221087;

        public const int ICON_SMALL = 0;

        public const int ICON_BIG = 1;

        public const int IDC_ARROW = 32512;

        public const int IDC_IBEAM = 32513;

        public const int IDC_WAIT = 32514;

        public const int IDC_CROSS = 32515;

        public const int IDC_SIZEALL = 32646;

        public const int IDC_SIZENWSE = 32642;

        public const int IDC_SIZENESW = 32643;

        public const int IDC_SIZEWE = 32644;

        public const int IDC_SIZENS = 32645;

        public const int IDC_UPARROW = 32516;

        public const int IDC_NO = 32648;

        public const int IDC_APPSTARTING = 32650;

        public const int IDC_HELP = 32651;

        public const int IMAGE_ICON = 1;

        public const int IMAGE_CURSOR = 2;

        public const int ICC_LISTVIEW_CLASSES = 1;

        public const int ICC_TREEVIEW_CLASSES = 2;

        public const int ICC_BAR_CLASSES = 4;

        public const int ICC_TAB_CLASSES = 8;

        public const int ICC_PROGRESS_CLASS = 32;

        public const int ICC_DATE_CLASSES = 256;

        public const int ILC_MASK = 1;

        public const int ILC_COLOR = 0;

        public const int ILC_COLOR4 = 4;

        public const int ILC_COLOR8 = 8;

        public const int ILC_COLOR16 = 16;

        public const int ILC_COLOR24 = 24;

        public const int ILC_COLOR32 = 32;

        public const int ILC_MIRROR = 8192;

        public const int ILD_NORMAL = 0;

        public const int ILD_TRANSPARENT = 1;

        public const int ILD_MASK = 16;

        public const int ILD_ROP = 64;

        public const int ILP_NORMAL = 0;

        public const int ILP_DOWNLEVEL = 1;

        public const int ILS_NORMAL = 0;

        public const int ILS_GLOW = 1;

        public const int ILS_SHADOW = 2;

        public const int ILS_SATURATE = 4;

        public const int ILS_ALPHA = 8;

        public const int IDM_PRINT = 27;

        public const int IDM_PAGESETUP = 2004;

        public const int IDM_PRINTPREVIEW = 2003;

        public const int IDM_PROPERTIES = 28;

        public const int IDM_SAVEAS = 71;

        public const int CSC_NAVIGATEFORWARD = 1;

        public const int CSC_NAVIGATEBACK = 2;

        public const int STG_E_INVALIDFUNCTION = -2147287039;

        public const int STG_E_FILENOTFOUND = -2147287038;

        public const int STG_E_PATHNOTFOUND = -2147287037;

        public const int STG_E_TOOMANYOPENFILES = -2147287036;

        public const int STG_E_ACCESSDENIED = -2147287035;

        public const int STG_E_INVALIDHANDLE = -2147287034;

        public const int STG_E_INSUFFICIENTMEMORY = -2147287032;

        public const int STG_E_INVALIDPOINTER = -2147287031;

        public const int STG_E_NOMOREFILES = -2147287022;

        public const int STG_E_DISKISWRITEPROTECTED = -2147287021;

        public const int STG_E_SEEKERROR = -2147287015;

        public const int STG_E_WRITEFAULT = -2147287011;

        public const int STG_E_READFAULT = -2147287010;

        public const int STG_E_SHAREVIOLATION = -2147287008;

        public const int STG_E_LOCKVIOLATION = -2147287007;

        public const int INPUT_KEYBOARD = 1;

        public const int KEYEVENTF_EXTENDEDKEY = 1;

        public const int KEYEVENTF_KEYUP = 2;

        public const int KEYEVENTF_UNICODE = 4;

        public const int LOGPIXELSX = 88;

        public const int LOGPIXELSY = 90;

        public const int LB_ERR = -1;

        public const int LB_ERRSPACE = -2;

        public const int LBN_SELCHANGE = 1;

        public const int LBN_DBLCLK = 2;

        public const int LB_ADDSTRING = 384;

        public const int LB_INSERTSTRING = 385;

        public const int LB_DELETESTRING = 386;

        public const int LB_RESETCONTENT = 388;

        public const int LB_SETSEL = 389;

        public const int LB_SETCURSEL = 390;

        public const int LB_GETSEL = 391;

        public const int LB_GETCARETINDEX = 415;

        public const int LB_GETCURSEL = 392;

        public const int LB_GETTEXT = 393;

        public const int LB_GETTEXTLEN = 394;

        public const int LB_GETTOPINDEX = 398;

        public const int LB_FINDSTRING = 399;

        public const int LB_GETSELCOUNT = 400;

        public const int LB_GETSELITEMS = 401;

        public const int LB_SETTABSTOPS = 402;

        public const int LB_SETHORIZONTALEXTENT = 404;

        public const int LB_SETCOLUMNWIDTH = 405;

        public const int LB_SETTOPINDEX = 407;

        public const int LB_GETITEMRECT = 408;

        public const int LB_SETITEMHEIGHT = 416;

        public const int LB_GETITEMHEIGHT = 417;

        public const int LB_FINDSTRINGEXACT = 418;

        public const int LB_ITEMFROMPOINT = 425;

        public const int LB_SETLOCALE = 421;

        public const int LBS_NOTIFY = 1;

        public const int LBS_MULTIPLESEL = 8;

        public const int LBS_OWNERDRAWFIXED = 16;

        public const int LBS_OWNERDRAWVARIABLE = 32;

        public const int LBS_HASSTRINGS = 64;

        public const int LBS_USETABSTOPS = 128;

        public const int LBS_NOINTEGRALHEIGHT = 256;

        public const int LBS_MULTICOLUMN = 512;

        public const int LBS_WANTKEYBOARDINPUT = 1024;

        public const int LBS_EXTENDEDSEL = 2048;

        public const int LBS_DISABLENOSCROLL = 4096;

        public const int LBS_NOSEL = 16384;

        public const int LOCK_WRITE = 1;

        public const int LOCK_EXCLUSIVE = 2;

        public const int LOCK_ONLYONCE = 4;

        public const int LV_VIEW_TILE = 4;

        public const int LVBKIF_SOURCE_NONE = 0;

        public const int LVBKIF_SOURCE_URL = 2;

        public const int LVBKIF_STYLE_NORMAL = 0;

        public const int LVBKIF_STYLE_TILE = 16;

        public const int LVS_ICON = 0;

        public const int LVS_REPORT = 1;

        public const int LVS_SMALLICON = 2;

        public const int LVS_LIST = 3;

        public const int LVS_SINGLESEL = 4;

        public const int LVS_SHOWSELALWAYS = 8;

        public const int LVS_SORTASCENDING = 16;

        public const int LVS_SORTDESCENDING = 32;

        public const int LVS_SHAREIMAGELISTS = 64;

        public const int LVS_NOLABELWRAP = 128;

        public const int LVS_AUTOARRANGE = 256;

        public const int LVS_EDITLABELS = 512;

        public const int LVS_NOSCROLL = 8192;

        public const int LVS_ALIGNTOP = 0;

        public const int LVS_ALIGNLEFT = 2048;

        public const int LVS_NOCOLUMNHEADER = 16384;

        public const int LVS_NOSORTHEADER = 32768;

        public const int LVS_OWNERDATA = 4096;

        public const int LVSCW_AUTOSIZE = -1;

        public const int LVSCW_AUTOSIZE_USEHEADER = -2;

        public const int LVM_REDRAWITEMS = 4117;

        public const int LVM_SCROLL = 4116;

        public const int LVM_SETBKCOLOR = 4097;

        public const int LVM_SETBKIMAGEA = 4164;

        public const int LVM_SETBKIMAGEW = 4234;

        public const int LVM_SETCALLBACKMASK = 4107;

        public const int LVM_GETCALLBACKMASK = 4106;

        public const int LVM_GETCOLUMNORDERARRAY = 4155;

        public const int LVM_GETITEMCOUNT = 4100;

        public const int LVM_SETCOLUMNORDERARRAY = 4154;

        public const int LVM_SETINFOTIP = 4269;

        public const int LVSIL_NORMAL = 0;

        public const int LVSIL_SMALL = 1;

        public const int LVSIL_STATE = 2;

        public const int LVM_SETIMAGELIST = 4099;

        public const int LVM_SETSELECTIONMARK = 4163;

        public const int LVM_SETTOOLTIPS = 4170;

        public const int LVIF_TEXT = 1;

        public const int LVIF_IMAGE = 2;

        public const int LVIF_INDENT = 16;

        public const int LVIF_PARAM = 4;

        public const int LVIF_STATE = 8;

        public const int LVIF_GROUPID = 256;

        public const int LVIF_COLUMNS = 512;

        public const int LVIS_FOCUSED = 1;

        public const int LVIS_SELECTED = 2;

        public const int LVIS_CUT = 4;

        public const int LVIS_DROPHILITED = 8;

        public const int LVIS_OVERLAYMASK = 3840;

        public const int LVIS_STATEIMAGEMASK = 61440;

        public const int LVM_GETITEMA = 4101;

        public const int LVM_GETITEMW = 4171;

        public const int LVM_SETITEMA = 4102;

        public const int LVM_SETITEMW = 4172;

        public const int LVM_SETITEMPOSITION32 = 4145;

        public const int LVM_INSERTITEMA = 4103;

        public const int LVM_INSERTITEMW = 4173;

        public const int LVM_DELETEITEM = 4104;

        public const int LVM_DELETECOLUMN = 4124;

        public const int LVM_DELETEALLITEMS = 4105;

        public const int LVM_UPDATE = 4138;

        public const int LVNI_FOCUSED = 1;

        public const int LVNI_SELECTED = 2;

        public const int LVM_GETNEXTITEM = 4108;

        public const int LVFI_PARAM = 1;

        public const int LVFI_NEARESTXY = 64;

        public const int LVFI_PARTIAL = 8;

        public const int LVFI_STRING = 2;

        public const int LVM_FINDITEMA = 4109;

        public const int LVM_FINDITEMW = 4179;

        public const int LVIR_BOUNDS = 0;

        public const int LVIR_ICON = 1;

        public const int LVIR_LABEL = 2;

        public const int LVIR_SELECTBOUNDS = 3;

        public const int LVM_GETITEMPOSITION = 4112;

        public const int LVM_GETITEMRECT = 4110;

        public const int LVM_GETSUBITEMRECT = 4152;

        public const int LVM_GETSTRINGWIDTHA = 4113;

        public const int LVM_GETSTRINGWIDTHW = 4183;

        public const int LVHT_NOWHERE = 1;

        public const int LVHT_ONITEMICON = 2;

        public const int LVHT_ONITEMLABEL = 4;

        public const int LVHT_ABOVE = 8;

        public const int LVHT_BELOW = 16;

        public const int LVHT_RIGHT = 32;

        public const int LVHT_LEFT = 64;

        public const int LVHT_ONITEM = 14;

        public const int LVHT_ONITEMSTATEICON = 8;

        public const int LVM_SUBITEMHITTEST = 4153;

        public const int LVM_HITTEST = 4114;

        public const int LVM_ENSUREVISIBLE = 4115;

        public const int LVA_DEFAULT = 0;

        public const int LVA_ALIGNLEFT = 1;

        public const int LVA_ALIGNTOP = 2;

        public const int LVA_SNAPTOGRID = 5;

        public const int LVM_ARRANGE = 4118;

        public const int LVM_EDITLABELA = 4119;

        public const int LVM_EDITLABELW = 4214;

        public const int LVCDI_ITEM = 0;

        public const int LVCDI_GROUP = 1;

        public const int LVCF_FMT = 1;

        public const int LVCF_WIDTH = 2;

        public const int LVCF_TEXT = 4;

        public const int LVCF_SUBITEM = 8;

        public const int LVCF_IMAGE = 16;

        public const int LVCF_ORDER = 32;

        public const int LVCFMT_IMAGE = 2048;

        public const int LVGA_HEADER_LEFT = 1;

        public const int LVGA_HEADER_CENTER = 2;

        public const int LVGA_HEADER_RIGHT = 4;

        public const int LVGA_FOOTER_LEFT = 8;

        public const int LVGA_FOOTER_CENTER = 16;

        public const int LVGA_FOOTER_RIGHT = 32;

        public const int LVGF_NONE = 0;

        public const int LVGF_HEADER = 1;

        public const int LVGF_FOOTER = 2;

        public const int LVGF_STATE = 4;

        public const int LVGF_ALIGN = 8;

        public const int LVGF_GROUPID = 16;

        public const int LVGS_NORMAL = 0;

        public const int LVGS_COLLAPSED = 1;

        public const int LVGS_HIDDEN = 2;

        public const int LVIM_AFTER = 1;

        public const int LVTVIF_FIXEDSIZE = 3;

        public const int LVTVIM_TILESIZE = 1;

        public const int LVTVIM_COLUMNS = 2;

        public const int LVM_ENABLEGROUPVIEW = 4253;

        public const int LVM_MOVEITEMTOGROUP = 4250;

        public const int LVM_GETCOLUMNA = 4121;

        public const int LVM_GETCOLUMNW = 4191;

        public const int LVM_SETCOLUMNA = 4122;

        public const int LVM_SETCOLUMNW = 4192;

        public const int LVM_INSERTCOLUMNA = 4123;

        public const int LVM_INSERTCOLUMNW = 4193;

        public const int LVM_INSERTGROUP = 4241;

        public const int LVM_REMOVEGROUP = 4246;

        public const int LVM_INSERTMARKHITTEST = 4264;

        public const int LVM_REMOVEALLGROUPS = 4256;

        public const int LVM_GETCOLUMNWIDTH = 4125;

        public const int LVM_SETCOLUMNWIDTH = 4126;

        public const int LVM_SETINSERTMARK = 4262;

        public const int LVM_GETHEADER = 4127;

        public const int LVM_SETTEXTCOLOR = 4132;

        public const int LVM_SETTEXTBKCOLOR = 4134;

        public const int LVM_GETTOPINDEX = 4135;

        public const int LVM_SETITEMPOSITION = 4111;

        public const int LVM_SETITEMSTATE = 4139;

        public const int LVM_GETITEMSTATE = 4140;

        public const int LVM_GETITEMTEXTA = 4141;

        public const int LVM_GETITEMTEXTW = 4211;

        public const int LVM_GETHOTITEM = 4157;

        public const int LVM_SETITEMTEXTA = 4142;

        public const int LVM_SETITEMTEXTW = 4212;

        public const int LVM_SETITEMCOUNT = 4143;

        public const int LVM_SORTITEMS = 4144;

        public const int LVM_GETSELECTEDCOUNT = 4146;

        public const int LVM_GETISEARCHSTRINGA = 4148;

        public const int LVM_GETISEARCHSTRINGW = 4213;

        public const int LVM_SETEXTENDEDLISTVIEWSTYLE = 4150;

        public const int LVM_SETVIEW = 4238;

        public const int LVM_GETGROUPINFO = 4245;

        public const int LVM_SETGROUPINFO = 4243;

        public const int LVM_HASGROUP = 4257;

        public const int LVM_SETTILEVIEWINFO = 4258;

        public const int LVM_GETTILEVIEWINFO = 4259;

        public const int LVM_GETINSERTMARK = 4263;

        public const int LVM_GETINSERTMARKRECT = 4265;

        public const int LVM_SETINSERTMARKCOLOR = 4266;

        public const int LVM_GETINSERTMARKCOLOR = 4267;

        public const int LVM_ISGROUPVIEWENABLED = 4271;

        public const int LVS_EX_GRIDLINES = 1;

        public const int LVS_EX_CHECKBOXES = 4;

        public const int LVS_EX_TRACKSELECT = 8;

        public const int LVS_EX_HEADERDRAGDROP = 16;

        public const int LVS_EX_FULLROWSELECT = 32;

        public const int LVS_EX_ONECLICKACTIVATE = 64;

        public const int LVS_EX_TWOCLICKACTIVATE = 128;

        public const int LVS_EX_INFOTIP = 1024;

        public const int LVS_EX_UNDERLINEHOT = 2048;

        public const int LVS_EX_DOUBLEBUFFER = 65536;

        public const int LVN_ITEMCHANGING = -100;

        public const int LVN_ITEMCHANGED = -101;

        public const int LVN_BEGINLABELEDITA = -105;

        public const int LVN_BEGINLABELEDITW = -175;

        public const int LVN_ENDLABELEDITA = -106;

        public const int LVN_ENDLABELEDITW = -176;

        public const int LVN_COLUMNCLICK = -108;

        public const int LVN_BEGINDRAG = -109;

        public const int LVN_BEGINRDRAG = -111;

        public const int LVN_ODFINDITEMA = -152;

        public const int LVN_ODFINDITEMW = -179;

        public const int LVN_ITEMACTIVATE = -114;

        public const int LVN_GETDISPINFOA = -150;

        public const int LVN_GETDISPINFOW = -177;

        public const int LVN_ODCACHEHINT = -113;

        public const int LVN_ODSTATECHANGED = -115;

        public const int LVN_SETDISPINFOA = -151;

        public const int LVN_SETDISPINFOW = -178;

        public const int LVN_GETINFOTIPA = -157;

        public const int LVN_GETINFOTIPW = -158;

        public const int LVN_KEYDOWN = -155;

        public const int LWA_COLORKEY = 1;

        public const int LWA_ALPHA = 2;

        public const int LANG_NEUTRAL = 0;

        public const int LOCALE_IFIRSTDAYOFWEEK = 4108;

        public const int LOCALE_IMEASURE = 13;

        public static readonly int LOCALE_USER_DEFAULT;

        public static readonly int LANG_USER_DEFAULT;

        public const int MEMBERID_NIL = -1;

        public const int MAX_PATH = 260;

        public const int MAX_UNICODESTRING_LEN = 32767;

        public const int ERROR_INSUFFICIENT_BUFFER = 122;

        public const int MA_ACTIVATE = 1;

        public const int MA_ACTIVATEANDEAT = 2;

        public const int MA_NOACTIVATE = 3;

        public const int MA_NOACTIVATEANDEAT = 4;

        public const int MM_TEXT = 1;

        public const int MM_ANISOTROPIC = 8;

        public const int MK_LBUTTON = 1;

        public const int MK_RBUTTON = 2;

        public const int MK_SHIFT = 4;

        public const int MK_CONTROL = 8;

        public const int MK_MBUTTON = 16;

        public const int MNC_EXECUTE = 2;

        public const int MNC_SELECT = 3;

        public const int MIIM_STATE = 1;

        public const int MIIM_ID = 2;

        public const int MIIM_SUBMENU = 4;

        public const int MIIM_TYPE = 16;

        public const int MIIM_DATA = 32;

        public const int MIIM_STRING = 64;

        public const int MIIM_BITMAP = 128;

        public const int MIIM_FTYPE = 256;

        public const int MB_OK = 0;

        public const int MF_BYCOMMAND = 0;

        public const int MF_BYPOSITION = 1024;

        public const int MF_ENABLED = 0;

        public const int MF_GRAYED = 1;

        public const int MF_POPUP = 16;

        public const int MF_SYSMENU = 8192;

        public const int MFS_DISABLED = 3;

        public const int MFT_MENUBREAK = 64;

        public const int MFT_SEPARATOR = 2048;

        public const int MFT_RIGHTORDER = 8192;

        public const int MFT_RIGHTJUSTIFY = 16384;

        public const int MDIS_ALLCHILDSTYLES = 1;

        public const int MDITILE_VERTICAL = 0;

        public const int MDITILE_HORIZONTAL = 1;

        public const int MDITILE_SKIPDISABLED = 2;

        public const int MCM_SETMAXSELCOUNT = 4100;

        public const int MCM_SETSELRANGE = 4102;

        public const int MCM_GETMONTHRANGE = 4103;

        public const int MCM_GETMINREQRECT = 4105;

        public const int MCM_SETCOLOR = 4106;

        public const int MCM_SETTODAY = 4108;

        public const int MCM_GETTODAY = 4109;

        public const int MCM_HITTEST = 4110;

        public const int MCM_SETFIRSTDAYOFWEEK = 4111;

        public const int MCM_SETRANGE = 4114;

        public const int MCM_SETMONTHDELTA = 4116;

        public const int MCM_GETMAXTODAYWIDTH = 4117;

        public const int MCHT_TITLE = 65536;

        public const int MCHT_CALENDAR = 131072;

        public const int MCHT_TODAYLINK = 196608;

        public const int MCHT_TITLEBK = 65536;

        public const int MCHT_TITLEMONTH = 65537;

        public const int MCHT_TITLEYEAR = 65538;

        public const int MCHT_TITLEBTNNEXT = 16842755;

        public const int MCHT_TITLEBTNPREV = 33619971;

        public const int MCHT_CALENDARBK = 131072;

        public const int MCHT_CALENDARDATE = 131073;

        public const int MCHT_CALENDARDATENEXT = 16908289;

        public const int MCHT_CALENDARDATEPREV = 33685505;

        public const int MCHT_CALENDARDAY = 131074;

        public const int MCHT_CALENDARWEEKNUM = 131075;

        public const int MCSC_TEXT = 1;

        public const int MCSC_TITLEBK = 2;

        public const int MCSC_TITLETEXT = 3;

        public const int MCSC_MONTHBK = 4;

        public const int MCSC_TRAILINGTEXT = 5;

        public const int MCN_VIEWCHANGE = -750;

        public const int MCN_SELCHANGE = -749;

        public const int MCN_GETDAYSTATE = -747;

        public const int MCN_SELECT = -746;

        public const int MCS_DAYSTATE = 1;

        public const int MCS_MULTISELECT = 2;

        public const int MCS_WEEKNUMBERS = 4;

        public const int MCS_NOTODAYCIRCLE = 8;

        public const int MCS_NOTODAY = 16;

        public const int MSAA_MENU_SIG = -1441927155;

        public const int NIM_ADD = 0;

        public const int NIM_MODIFY = 1;

        public const int NIM_DELETE = 2;

        public const int NIF_MESSAGE = 1;

        public const int NIM_SETVERSION = 4;

        public const int NIF_ICON = 2;

        public const int NIF_INFO = 16;

        public const int NIF_TIP = 4;

        public const int NIIF_NONE = 0;

        public const int NIIF_INFO = 1;

        public const int NIIF_WARNING = 2;

        public const int NIIF_ERROR = 3;

        public const int NIN_BALLOONSHOW = 1026;

        public const int NIN_BALLOONHIDE = 1027;

        public const int NIN_BALLOONTIMEOUT = 1028;

        public const int NIN_BALLOONUSERCLICK = 1029;

        public const int NFR_ANSI = 1;

        public const int NFR_UNICODE = 2;

        public const int NM_CLICK = -2;

        public const int NM_DBLCLK = -3;

        public const int NM_RCLICK = -5;

        public const int NM_RDBLCLK = -6;

        public const int NM_CUSTOMDRAW = -12;

        public const int NM_RELEASEDCAPTURE = -16;

        public const int NONANTIALIASED_QUALITY = 3;

        public const int OFN_READONLY = 1;

        public const int OFN_OVERWRITEPROMPT = 2;

        public const int OFN_HIDEREADONLY = 4;

        public const int OFN_NOCHANGEDIR = 8;

        public const int OFN_SHOWHELP = 16;

        public const int OFN_ENABLEHOOK = 32;

        public const int OFN_NOVALIDATE = 256;

        public const int OFN_ALLOWMULTISELECT = 512;

        public const int OFN_PATHMUSTEXIST = 2048;

        public const int OFN_FILEMUSTEXIST = 4096;

        public const int OFN_CREATEPROMPT = 8192;

        public const int OFN_EXPLORER = 524288;

        public const int OFN_NODEREFERENCELINKS = 1048576;

        public const int OFN_ENABLESIZING = 8388608;

        public const int OFN_USESHELLITEM = 16777216;

        public const int OLEIVERB_PRIMARY = 0;

        public const int OLEIVERB_SHOW = -1;

        public const int OLEIVERB_HIDE = -3;

        public const int OLEIVERB_UIACTIVATE = -4;

        public const int OLEIVERB_INPLACEACTIVATE = -5;

        public const int OLEIVERB_DISCARDUNDOSTATE = -6;

        public const int OLEIVERB_PROPERTIES = -7;

        public const int OLE_E_INVALIDRECT = -2147221491;

        public const int OLE_E_NOCONNECTION = -2147221500;

        public const int OLE_E_PROMPTSAVECANCELLED = -2147221492;

        public const int OLEMISC_RECOMPOSEONRESIZE = 1;

        public const int OLEMISC_INSIDEOUT = 128;

        public const int OLEMISC_ACTIVATEWHENVISIBLE = 256;

        public const int OLEMISC_ACTSLIKEBUTTON = 4096;

        public const int OLEMISC_SETCLIENTSITEFIRST = 131072;

        public const int OBJ_PEN = 1;

        public const int OBJ_BRUSH = 2;

        public const int OBJ_DC = 3;

        public const int OBJ_METADC = 4;

        public const int OBJ_PAL = 5;

        public const int OBJ_FONT = 6;

        public const int OBJ_BITMAP = 7;

        public const int OBJ_REGION = 8;

        public const int OBJ_METAFILE = 9;

        public const int OBJ_MEMDC = 10;

        public const int OBJ_EXTPEN = 11;

        public const int OBJ_ENHMETADC = 12;

        public const int ODS_CHECKED = 8;

        public const int ODS_COMBOBOXEDIT = 4096;

        public const int ODS_DEFAULT = 32;

        public const int ODS_DISABLED = 4;

        public const int ODS_FOCUS = 16;

        public const int ODS_GRAYED = 2;

        public const int ODS_HOTLIGHT = 64;

        public const int ODS_INACTIVE = 128;

        public const int ODS_NOACCEL = 256;

        public const int ODS_NOFOCUSRECT = 512;

        public const int ODS_SELECTED = 1;

        public const int OLECLOSE_SAVEIFDIRTY = 0;

        public const int OLECLOSE_PROMPTSAVE = 2;

        public const int PDERR_SETUPFAILURE = 4097;

        public const int PDERR_PARSEFAILURE = 4098;

        public const int PDERR_RETDEFFAILURE = 4099;

        public const int PDERR_LOADDRVFAILURE = 4100;

        public const int PDERR_GETDEVMODEFAIL = 4101;

        public const int PDERR_INITFAILURE = 4102;

        public const int PDERR_NODEVICES = 4103;

        public const int PDERR_NODEFAULTPRN = 4104;

        public const int PDERR_DNDMMISMATCH = 4105;

        public const int PDERR_CREATEICFAILURE = 4106;

        public const int PDERR_PRINTERNOTFOUND = 4107;

        public const int PDERR_DEFAULTDIFFERENT = 4108;

        public const int PD_ALLPAGES = 0;

        public const int PD_SELECTION = 1;

        public const int PD_PAGENUMS = 2;

        public const int PD_NOSELECTION = 4;

        public const int PD_NOPAGENUMS = 8;

        public const int PD_COLLATE = 16;

        public const int PD_PRINTTOFILE = 32;

        public const int PD_PRINTSETUP = 64;

        public const int PD_NOWARNING = 128;

        public const int PD_RETURNDC = 256;

        public const int PD_RETURNIC = 512;

        public const int PD_RETURNDEFAULT = 1024;

        public const int PD_SHOWHELP = 2048;

        public const int PD_ENABLEPRINTHOOK = 4096;

        public const int PD_ENABLESETUPHOOK = 8192;

        public const int PD_ENABLEPRINTTEMPLATE = 16384;

        public const int PD_ENABLESETUPTEMPLATE = 32768;

        public const int PD_ENABLEPRINTTEMPLATEHANDLE = 65536;

        public const int PD_ENABLESETUPTEMPLATEHANDLE = 131072;

        public const int PD_USEDEVMODECOPIES = 262144;

        public const int PD_USEDEVMODECOPIESANDCOLLATE = 262144;

        public const int PD_DISABLEPRINTTOFILE = 524288;

        public const int PD_HIDEPRINTTOFILE = 1048576;

        public const int PD_NONETWORKBUTTON = 2097152;

        public const int PD_CURRENTPAGE = 4194304;

        public const int PD_NOCURRENTPAGE = 8388608;

        public const int PD_EXCLUSIONFLAGS = 16777216;

        public const int PD_USELARGETEMPLATE = 268435456;

        public const int PSD_MINMARGINS = 1;

        public const int PSD_MARGINS = 2;

        public const int PSD_INHUNDREDTHSOFMILLIMETERS = 8;

        public const int PSD_DISABLEMARGINS = 16;

        public const int PSD_DISABLEPRINTER = 32;

        public const int PSD_DISABLEORIENTATION = 256;

        public const int PSD_DISABLEPAPER = 512;

        public const int PSD_SHOWHELP = 2048;

        public const int PSD_ENABLEPAGESETUPHOOK = 8192;

        public const int PSD_NONETWORKBUTTON = 2097152;

        public const int PS_SOLID = 0;

        public const int PS_DOT = 2;

        public const int PLANES = 14;

        public const int PRF_CHECKVISIBLE = 1;

        public const int PRF_NONCLIENT = 2;

        public const int PRF_CLIENT = 4;

        public const int PRF_ERASEBKGND = 8;

        public const int PRF_CHILDREN = 16;

        public const int PM_NOREMOVE = 0;

        public const int PM_REMOVE = 1;

        public const int PM_NOYIELD = 2;

        public const int PBM_SETRANGE = 1025;

        public const int PBM_SETPOS = 1026;

        public const int PBM_SETSTEP = 1028;

        public const int PBM_SETRANGE32 = 1030;

        public const int PBM_SETBARCOLOR = 1033;

        public const int PBM_SETMARQUEE = 1034;

        public const int PBM_SETBKCOLOR = 8193;

        public const int PSM_SETTITLEA = 1135;

        public const int PSM_SETTITLEW = 1144;

        public const int PSM_SETFINISHTEXTA = 1139;

        public const int PSM_SETFINISHTEXTW = 1145;

        public const int PATCOPY = 15728673;

        public const int PATINVERT = 5898313;

        public const int PBS_SMOOTH = 1;

        public const int PBS_MARQUEE = 8;

        public const int QS_KEY = 1;

        public const int QS_MOUSEMOVE = 2;

        public const int QS_MOUSEBUTTON = 4;

        public const int QS_POSTMESSAGE = 8;

        public const int QS_TIMER = 16;

        public const int QS_PAINT = 32;

        public const int QS_SENDMESSAGE = 64;

        public const int QS_HOTKEY = 128;

        public const int QS_ALLPOSTMESSAGE = 256;

        public const int QS_MOUSE = 6;

        public const int QS_INPUT = 7;

        public const int QS_ALLEVENTS = 191;

        public const int QS_ALLINPUT = 255;

        public const int MWMO_INPUTAVAILABLE = 4;

        public const int RECO_DROP = 1;

        public const int RPC_E_CHANGED_MODE = -2147417850;

        public const int RPC_E_CANTCALLOUT_ININPUTSYNCCALL = -2147417843;

        public const int RGN_AND = 1;

        public const int RGN_XOR = 3;

        public const int RGN_DIFF = 4;

        public const int RDW_INVALIDATE = 1;

        public const int RDW_ERASE = 4;

        public const int RDW_ALLCHILDREN = 128;

        public const int RDW_ERASENOW = 512;

        public const int RDW_UPDATENOW = 256;

        public const int RDW_FRAME = 1024;

        public const int RB_INSERTBANDA = 1025;

        public const int RB_INSERTBANDW = 1034;

        public const int stc4 = 1091;

        public const int SHGFP_TYPE_CURRENT = 0;

        public const int STGM_READ = 0;

        public const int STGM_WRITE = 1;

        public const int STGM_READWRITE = 2;

        public const int STGM_SHARE_EXCLUSIVE = 16;

        public const int STGM_CREATE = 4096;

        public const int STGM_TRANSACTED = 65536;

        public const int STGM_CONVERT = 131072;

        public const int STGM_DELETEONRELEASE = 67108864;

        public const int STARTF_USESHOWWINDOW = 1;

        public const int SB_HORZ = 0;

        public const int SB_VERT = 1;

        public const int SB_CTL = 2;

        public const int SB_LINEUP = 0;

        public const int SB_LINELEFT = 0;

        public const int SB_LINEDOWN = 1;

        public const int SB_LINERIGHT = 1;

        public const int SB_PAGEUP = 2;

        public const int SB_PAGELEFT = 2;

        public const int SB_PAGEDOWN = 3;

        public const int SB_PAGERIGHT = 3;

        public const int SB_THUMBPOSITION = 4;

        public const int SB_THUMBTRACK = 5;

        public const int SB_LEFT = 6;

        public const int SB_RIGHT = 7;

        public const int SB_ENDSCROLL = 8;

        public const int SB_TOP = 6;

        public const int SB_BOTTOM = 7;

        public const int SIZE_RESTORED = 0;

        public const int SIZE_MAXIMIZED = 2;

        public const int ESB_ENABLE_BOTH = 0;

        public const int ESB_DISABLE_BOTH = 3;

        public const int SORT_DEFAULT = 0;

        public const int SUBLANG_DEFAULT = 1;

        public const int SW_HIDE = 0;

        public const int SW_NORMAL = 1;

        public const int SW_SHOWMINIMIZED = 2;

        public const int SW_SHOWMAXIMIZED = 3;

        public const int SW_MAXIMIZE = 3;

        public const int SW_SHOWNOACTIVATE = 4;

        public const int SW_SHOW = 5;

        public const int SW_MINIMIZE = 6;

        public const int SW_SHOWMINNOACTIVE = 7;

        public const int SW_SHOWNA = 8;

        public const int SW_RESTORE = 9;

        public const int SW_MAX = 10;

        public const int SWP_NOSIZE = 1;

        public const int SWP_NOMOVE = 2;

        public const int SWP_NOZORDER = 4;

        public const int SWP_NOACTIVATE = 16;

        public const int SWP_SHOWWINDOW = 64;

        public const int SWP_HIDEWINDOW = 128;

        public const int SWP_DRAWFRAME = 32;

        public const int SWP_NOOWNERZORDER = 512;

        public const int SM_CXSCREEN = 0;

        public const int SM_CYSCREEN = 1;

        public const int SM_CXVSCROLL = 2;

        public const int SM_CYHSCROLL = 3;

        public const int SM_CYCAPTION = 4;

        public const int SM_CXBORDER = 5;

        public const int SM_CYBORDER = 6;

        public const int SM_CYVTHUMB = 9;

        public const int SM_CXHTHUMB = 10;

        public const int SM_CXICON = 11;

        public const int SM_CYICON = 12;

        public const int SM_CXCURSOR = 13;

        public const int SM_CYCURSOR = 14;

        public const int SM_CYMENU = 15;

        public const int SM_CYKANJIWINDOW = 18;

        public const int SM_MOUSEPRESENT = 19;

        public const int SM_CYVSCROLL = 20;

        public const int SM_CXHSCROLL = 21;

        public const int SM_DEBUG = 22;

        public const int SM_SWAPBUTTON = 23;

        public const int SM_CXMIN = 28;

        public const int SM_CYMIN = 29;

        public const int SM_CXSIZE = 30;

        public const int SM_CYSIZE = 31;

        public const int SM_CXFRAME = 32;

        public const int SM_CYFRAME = 33;

        public const int SM_CXMINTRACK = 34;

        public const int SM_CYMINTRACK = 35;

        public const int SM_CXDOUBLECLK = 36;

        public const int SM_CYDOUBLECLK = 37;

        public const int SM_CXICONSPACING = 38;

        public const int SM_CYICONSPACING = 39;

        public const int SM_MENUDROPALIGNMENT = 40;

        public const int SM_PENWINDOWS = 41;

        public const int SM_DBCSENABLED = 42;

        public const int SM_CMOUSEBUTTONS = 43;

        public const int SM_CXFIXEDFRAME = 7;

        public const int SM_CYFIXEDFRAME = 8;

        public const int SM_SECURE = 44;

        public const int SM_CXEDGE = 45;

        public const int SM_CYEDGE = 46;

        public const int SM_CXMINSPACING = 47;

        public const int SM_CYMINSPACING = 48;

        public const int SM_CXSMICON = 49;

        public const int SM_CYSMICON = 50;

        public const int SM_CYSMCAPTION = 51;

        public const int SM_CXSMSIZE = 52;

        public const int SM_CYSMSIZE = 53;

        public const int SM_CXMENUSIZE = 54;

        public const int SM_CYMENUSIZE = 55;

        public const int SM_ARRANGE = 56;

        public const int SM_CXMINIMIZED = 57;

        public const int SM_CYMINIMIZED = 58;

        public const int SM_CXMAXTRACK = 59;

        public const int SM_CYMAXTRACK = 60;

        public const int SM_CXMAXIMIZED = 61;

        public const int SM_CYMAXIMIZED = 62;

        public const int SM_NETWORK = 63;

        public const int SM_CLEANBOOT = 67;

        public const int SM_CXDRAG = 68;

        public const int SM_CYDRAG = 69;

        public const int SM_SHOWSOUNDS = 70;

        public const int SM_CXMENUCHECK = 71;

        public const int SM_CYMENUCHECK = 72;

        public const int SM_MIDEASTENABLED = 74;

        public const int SM_MOUSEWHEELPRESENT = 75;

        public const int SM_XVIRTUALSCREEN = 76;

        public const int SM_YVIRTUALSCREEN = 77;

        public const int SM_CXVIRTUALSCREEN = 78;

        public const int SM_CYVIRTUALSCREEN = 79;

        public const int SM_CMONITORS = 80;

        public const int SM_SAMEDISPLAYFORMAT = 81;

        public const int SM_REMOTESESSION = 4096;

        public const int HLP_FILE = 1;

        public const int HLP_KEYWORD = 2;

        public const int HLP_NAVIGATOR = 3;

        public const int HLP_OBJECT = 4;

        public const int SW_SCROLLCHILDREN = 1;

        public const int SW_INVALIDATE = 2;

        public const int SW_ERASE = 4;

        public const int SW_SMOOTHSCROLL = 16;

        public const int SC_SIZE = 61440;

        public const int SC_MINIMIZE = 61472;

        public const int SC_MAXIMIZE = 61488;

        public const int SC_CLOSE = 61536;

        public const int SC_KEYMENU = 61696;

        public const int SC_RESTORE = 61728;

        public const int SC_MOVE = 61456;

        public const int SC_CONTEXTHELP = 61824;

        public const int SS_LEFT = 0;

        public const int SS_CENTER = 1;

        public const int SS_RIGHT = 2;

        public const int SS_OWNERDRAW = 13;

        public const int SS_NOPREFIX = 128;

        public const int SS_SUNKEN = 4096;

        public const int SBS_HORZ = 0;

        public const int SBS_VERT = 1;

        public const int SIF_RANGE = 1;

        public const int SIF_PAGE = 2;

        public const int SIF_POS = 4;

        public const int SIF_TRACKPOS = 16;

        public const int SIF_ALL = 23;

        public const int SPI_GETFONTSMOOTHING = 74;

        public const int SPI_GETDROPSHADOW = 4132;

        public const int SPI_GETFLATMENU = 4130;

        public const int SPI_GETFONTSMOOTHINGTYPE = 8202;

        public const int SPI_GETFONTSMOOTHINGCONTRAST = 8204;

        public const int SPI_ICONHORIZONTALSPACING = 13;

        public const int SPI_ICONVERTICALSPACING = 24;

        public const int SPI_GETICONTITLEWRAP = 25;

        public const int SPI_GETICONTITLELOGFONT = 31;

        public const int SPI_GETKEYBOARDCUES = 4106;

        public const int SPI_GETKEYBOARDDELAY = 22;

        public const int SPI_GETKEYBOARDPREF = 68;

        public const int SPI_GETKEYBOARDSPEED = 10;

        public const int SPI_GETMOUSEHOVERWIDTH = 98;

        public const int SPI_GETMOUSEHOVERHEIGHT = 100;

        public const int SPI_GETMOUSEHOVERTIME = 102;

        public const int SPI_GETMOUSESPEED = 112;

        public const int SPI_GETMENUDROPALIGNMENT = 27;

        public const int SPI_GETMENUFADE = 4114;

        public const int SPI_GETMENUSHOWDELAY = 106;

        public const int SPI_GETCOMBOBOXANIMATION = 4100;

        public const int SPI_GETGRADIENTCAPTIONS = 4104;

        public const int SPI_GETHOTTRACKING = 4110;

        public const int SPI_GETLISTBOXSMOOTHSCROLLING = 4102;

        public const int SPI_GETMENUANIMATION = 4098;

        public const int SPI_GETSELECTIONFADE = 4116;

        public const int SPI_GETTOOLTIPANIMATION = 4118;

        public const int SPI_GETUIEFFECTS = 4158;

        public const int SPI_GETACTIVEWINDOWTRACKING = 4096;

        public const int SPI_GETACTIVEWNDTRKTIMEOUT = 8194;

        public const int SPI_GETANIMATION = 72;

        public const int SPI_GETBORDER = 5;

        public const int SPI_GETCARETWIDTH = 8198;

        public const int SM_CYFOCUSBORDER = 84;

        public const int SM_CXFOCUSBORDER = 83;

        public const int SM_CYSIZEFRAME = 33;

        public const int SM_CXSIZEFRAME = 32;

        public const int SPI_GETDRAGFULLWINDOWS = 38;

        public const int SPI_GETNONCLIENTMETRICS = 41;

        public const int SPI_GETWORKAREA = 48;

        public const int SPI_GETHIGHCONTRAST = 66;

        public const int SPI_GETDEFAULTINPUTLANG = 89;

        public const int SPI_GETSNAPTODEFBUTTON = 95;

        public const int SPI_GETWHEELSCROLLLINES = 104;

        public const int SBARS_SIZEGRIP = 256;

        public const int SB_SETTEXTA = 1025;

        public const int SB_SETTEXTW = 1035;

        public const int SB_GETTEXTA = 1026;

        public const int SB_GETTEXTW = 1037;

        public const int SB_GETTEXTLENGTHA = 1027;

        public const int SB_GETTEXTLENGTHW = 1036;

        public const int SB_SETPARTS = 1028;

        public const int SB_SIMPLE = 1033;

        public const int SB_GETRECT = 1034;

        public const int SB_SETICON = 1039;

        public const int SB_SETTIPTEXTA = 1040;

        public const int SB_SETTIPTEXTW = 1041;

        public const int SB_GETTIPTEXTA = 1042;

        public const int SB_GETTIPTEXTW = 1043;

        public const int SBT_OWNERDRAW = 4096;

        public const int SBT_NOBORDERS = 256;

        public const int SBT_POPOUT = 512;

        public const int SBT_RTLREADING = 1024;

        public const int SRCCOPY = 13369376;

        public const int SRCAND = 8913094;

        public const int SRCPAINT = 15597702;

        public const int NOTSRCCOPY = 3342344;

        public const int STATFLAG_DEFAULT = 0;

        public const int STATFLAG_NONAME = 1;

        public const int STATFLAG_NOOPEN = 2;

        public const int STGC_DEFAULT = 0;

        public const int STGC_OVERWRITE = 1;

        public const int STGC_ONLYIFCURRENT = 2;

        public const int STGC_DANGEROUSLYCOMMITMERELYTODISKCACHE = 4;

        public const int STREAM_SEEK_SET = 0;

        public const int STREAM_SEEK_CUR = 1;

        public const int STREAM_SEEK_END = 2;

        public const int S_OK = 0;

        public const int S_FALSE = 1;

        public const int TRANSPARENT = 1;

        public const int OPAQUE = 2;

        public const int TME_HOVER = 1;

        public const int TME_LEAVE = 2;

        public const int TPM_LEFTBUTTON = 0;

        public const int TPM_RIGHTBUTTON = 2;

        public const int TPM_LEFTALIGN = 0;

        public const int TPM_RIGHTALIGN = 8;

        public const int TPM_VERTICAL = 64;

        public const int TV_FIRST = 4352;

        public const int TBSTATE_CHECKED = 1;

        public const int TBSTATE_ENABLED = 4;

        public const int TBSTATE_HIDDEN = 8;

        public const int TBSTATE_INDETERMINATE = 16;

        public const int TBSTYLE_BUTTON = 0;

        public const int TBSTYLE_SEP = 1;

        public const int TBSTYLE_CHECK = 2;

        public const int TBSTYLE_DROPDOWN = 8;

        public const int TBSTYLE_TOOLTIPS = 256;

        public const int TBSTYLE_FLAT = 2048;

        public const int TBSTYLE_LIST = 4096;

        public const int TBSTYLE_EX_DRAWDDARROWS = 1;

        public const int TB_ENABLEBUTTON = 1025;

        public const int TB_ISBUTTONCHECKED = 1034;

        public const int TB_ISBUTTONINDETERMINATE = 1037;

        public const int TB_ADDBUTTONSA = 1044;

        public const int TB_ADDBUTTONSW = 1092;

        public const int TB_INSERTBUTTONA = 1045;

        public const int TB_INSERTBUTTONW = 1091;

        public const int TB_DELETEBUTTON = 1046;

        public const int TB_GETBUTTON = 1047;

        public const int TB_SAVERESTOREA = 1050;

        public const int TB_SAVERESTOREW = 1100;

        public const int TB_ADDSTRINGA = 1052;

        public const int TB_ADDSTRINGW = 1101;

        public const int TB_BUTTONSTRUCTSIZE = 1054;

        public const int TB_SETBUTTONSIZE = 1055;

        public const int TB_AUTOSIZE = 1057;

        public const int TB_GETROWS = 1064;

        public const int TB_GETBUTTONTEXTA = 1069;

        public const int TB_GETBUTTONTEXTW = 1099;

        public const int TB_SETIMAGELIST = 1072;

        public const int TB_GETRECT = 1075;

        public const int TB_GETBUTTONSIZE = 1082;

        public const int TB_GETBUTTONINFOW = 1087;

        public const int TB_SETBUTTONINFOW = 1088;

        public const int TB_GETBUTTONINFOA = 1089;

        public const int TB_SETBUTTONINFOA = 1090;

        public const int TB_MAPACCELERATORA = 1102;

        public const int TB_SETEXTENDEDSTYLE = 1108;

        public const int TB_MAPACCELERATORW = 1114;

        public const int TB_GETTOOLTIPS = 1059;

        public const int TB_SETTOOLTIPS = 1060;

        public const int TBIF_IMAGE = 1;

        public const int TBIF_TEXT = 2;

        public const int TBIF_STATE = 4;

        public const int TBIF_STYLE = 8;

        public const int TBIF_COMMAND = 32;

        public const int TBIF_SIZE = 64;

        public const int TBN_GETBUTTONINFOA = -700;

        public const int TBN_GETBUTTONINFOW = -720;

        public const int TBN_QUERYINSERT = -706;

        public const int TBN_DROPDOWN = -710;

        public const int TBN_HOTITEMCHANGE = -713;

        public const int TBN_GETDISPINFOA = -716;

        public const int TBN_GETDISPINFOW = -717;

        public const int TBN_GETINFOTIPA = -718;

        public const int TBN_GETINFOTIPW = -719;

        public const int TTS_ALWAYSTIP = 1;

        public const int TTS_NOPREFIX = 2;

        public const int TTS_NOANIMATE = 16;

        public const int TTS_NOFADE = 32;

        public const int TTS_BALLOON = 64;

        public const int TTI_WARNING = 2;

        public const int TTF_IDISHWND = 1;

        public const int TTF_RTLREADING = 4;

        public const int TTF_TRACK = 32;

        public const int TTF_CENTERTIP = 2;

        public const int TTF_SUBCLASS = 16;

        public const int TTF_TRANSPARENT = 256;

        public const int TTF_ABSOLUTE = 128;

        public const int TTDT_AUTOMATIC = 0;

        public const int TTDT_RESHOW = 1;

        public const int TTDT_AUTOPOP = 2;

        public const int TTDT_INITIAL = 3;

        public const int TTM_TRACKACTIVATE = 1041;

        public const int TTM_TRACKPOSITION = 1042;

        public const int TTM_ACTIVATE = 1025;

        public const int TTM_POP = 1052;

        public const int TTM_ADJUSTRECT = 1055;

        public const int TTM_SETDELAYTIME = 1027;

        public const int TTM_SETTITLEA = 1056;

        public const int TTM_SETTITLEW = 1057;

        public const int TTM_ADDTOOLA = 1028;

        public const int TTM_ADDTOOLW = 1074;

        public const int TTM_DELTOOLA = 1029;

        public const int TTM_DELTOOLW = 1075;

        public const int TTM_NEWTOOLRECTA = 1030;

        public const int TTM_NEWTOOLRECTW = 1076;

        public const int TTM_RELAYEVENT = 1031;

        public const int TTM_GETTIPBKCOLOR = 1046;

        public const int TTM_SETTIPBKCOLOR = 1043;

        public const int TTM_SETTIPTEXTCOLOR = 1044;

        public const int TTM_GETTIPTEXTCOLOR = 1047;

        public const int TTM_GETTOOLINFOA = 1032;

        public const int TTM_GETTOOLINFOW = 1077;

        public const int TTM_SETTOOLINFOA = 1033;

        public const int TTM_SETTOOLINFOW = 1078;

        public const int TTM_HITTESTA = 1034;

        public const int TTM_HITTESTW = 1079;

        public const int TTM_GETTEXTA = 1035;

        public const int TTM_GETTEXTW = 1080;

        public const int TTM_UPDATE = 1053;

        public const int TTM_UPDATETIPTEXTA = 1036;

        public const int TTM_UPDATETIPTEXTW = 1081;

        public const int TTM_ENUMTOOLSA = 1038;

        public const int TTM_ENUMTOOLSW = 1082;

        public const int TTM_GETCURRENTTOOLA = 1039;

        public const int TTM_GETCURRENTTOOLW = 1083;

        public const int TTM_WINDOWFROMPOINT = 1040;

        public const int TTM_GETDELAYTIME = 1045;

        public const int TTM_SETMAXTIPWIDTH = 1048;

        public const int TTM_GETBUBBLESIZE = 1054;

        public const int TTN_GETDISPINFOA = -520;

        public const int TTN_GETDISPINFOW = -530;

        public const int TTN_SHOW = -521;

        public const int TTN_POP = -522;

        public const int TTN_NEEDTEXTA = -520;

        public const int TTN_NEEDTEXTW = -530;

        public const int TBS_AUTOTICKS = 1;

        public const int TBS_VERT = 2;

        public const int TBS_TOP = 4;

        public const int TBS_BOTTOM = 0;

        public const int TBS_BOTH = 8;

        public const int TBS_NOTICKS = 16;

        public const int TBM_GETPOS = 1024;

        public const int TBM_SETTIC = 1028;

        public const int TBM_SETPOS = 1029;

        public const int TBM_SETRANGE = 1030;

        public const int TBM_SETRANGEMIN = 1031;

        public const int TBM_SETRANGEMAX = 1032;

        public const int TBM_SETTICFREQ = 1044;

        public const int TBM_SETPAGESIZE = 1045;

        public const int TBM_SETLINESIZE = 1047;

        public const int TB_LINEUP = 0;

        public const int TB_LINEDOWN = 1;

        public const int TB_PAGEUP = 2;

        public const int TB_PAGEDOWN = 3;

        public const int TB_THUMBPOSITION = 4;

        public const int TB_THUMBTRACK = 5;

        public const int TB_TOP = 6;

        public const int TB_BOTTOM = 7;

        public const int TB_ENDTRACK = 8;

        public const int TVS_HASBUTTONS = 1;

        public const int TVS_HASLINES = 2;

        public const int TVS_LINESATROOT = 4;

        public const int TVS_EDITLABELS = 8;

        public const int TVS_SHOWSELALWAYS = 32;

        public const int TVS_RTLREADING = 64;

        public const int TVS_CHECKBOXES = 256;

        public const int TVS_TRACKSELECT = 512;

        public const int TVS_FULLROWSELECT = 4096;

        public const int TVS_NONEVENHEIGHT = 16384;

        public const int TVS_INFOTIP = 2048;

        public const int TVS_NOTOOLTIPS = 128;

        public const int TVIF_TEXT = 1;

        public const int TVIF_IMAGE = 2;

        public const int TVIF_PARAM = 4;

        public const int TVIF_STATE = 8;

        public const int TVIF_HANDLE = 16;

        public const int TVIF_SELECTEDIMAGE = 32;

        public const int TVIS_SELECTED = 2;

        public const int TVIS_EXPANDED = 32;

        public const int TVIS_EXPANDEDONCE = 64;

        public const int TVIS_STATEIMAGEMASK = 61440;

        public const int TVI_ROOT = -65536;

        public const int TVI_FIRST = -65535;

        public const int TVM_INSERTITEMA = 4352;

        public const int TVM_INSERTITEMW = 4402;

        public const int TVM_DELETEITEM = 4353;

        public const int TVM_EXPAND = 4354;

        public const int TVE_COLLAPSE = 1;

        public const int TVE_EXPAND = 2;

        public const int TVM_GETITEMRECT = 4356;

        public const int TVM_GETINDENT = 4358;

        public const int TVM_SETINDENT = 4359;

        public const int TVM_GETIMAGELIST = 4360;

        public const int TVM_SETIMAGELIST = 4361;

        public const int TVM_GETNEXTITEM = 4362;

        public const int TVGN_NEXT = 1;

        public const int TVGN_PREVIOUS = 2;

        public const int TVGN_FIRSTVISIBLE = 5;

        public const int TVGN_NEXTVISIBLE = 6;

        public const int TVGN_PREVIOUSVISIBLE = 7;

        public const int TVGN_DROPHILITE = 8;

        public const int TVGN_CARET = 9;

        public const int TVM_SELECTITEM = 4363;

        public const int TVM_GETITEMA = 4364;

        public const int TVM_GETITEMW = 4414;

        public const int TVM_SETITEMA = 4365;

        public const int TVM_SETITEMW = 4415;

        public const int TVM_EDITLABELA = 4366;

        public const int TVM_EDITLABELW = 4417;

        public const int TVM_GETEDITCONTROL = 4367;

        public const int TVM_GETVISIBLECOUNT = 4368;

        public const int TVM_HITTEST = 4369;

        public const int TVM_ENSUREVISIBLE = 4372;

        public const int TVM_ENDEDITLABELNOW = 4374;

        public const int TVM_GETISEARCHSTRINGA = 4375;

        public const int TVM_GETISEARCHSTRINGW = 4416;

        public const int TVM_SETITEMHEIGHT = 4379;

        public const int TVM_GETITEMHEIGHT = 4380;

        public const int TVN_SELCHANGINGA = -401;

        public const int TVN_SELCHANGINGW = -450;

        public const int TVN_GETINFOTIPA = -413;

        public const int TVN_GETINFOTIPW = -414;

        public const int TVN_SELCHANGEDA = -402;

        public const int TVN_SELCHANGEDW = -451;

        public const int TVC_UNKNOWN = 0;

        public const int TVC_BYMOUSE = 1;

        public const int TVC_BYKEYBOARD = 2;

        public const int TVN_GETDISPINFOA = -403;

        public const int TVN_GETDISPINFOW = -452;

        public const int TVN_SETDISPINFOA = -404;

        public const int TVN_SETDISPINFOW = -453;

        public const int TVN_ITEMEXPANDINGA = -405;

        public const int TVN_ITEMEXPANDINGW = -454;

        public const int TVN_ITEMEXPANDEDA = -406;

        public const int TVN_ITEMEXPANDEDW = -455;

        public const int TVN_BEGINDRAGA = -407;

        public const int TVN_BEGINDRAGW = -456;

        public const int TVN_BEGINRDRAGA = -408;

        public const int TVN_BEGINRDRAGW = -457;

        public const int TVN_BEGINLABELEDITA = -410;

        public const int TVN_BEGINLABELEDITW = -459;

        public const int TVN_ENDLABELEDITA = -411;

        public const int TVN_ENDLABELEDITW = -460;

        public const int TCS_BOTTOM = 2;

        public const int TCS_RIGHT = 2;

        public const int TCS_FLATBUTTONS = 8;

        public const int TCS_HOTTRACK = 64;

        public const int TCS_VERTICAL = 128;

        public const int TCS_TABS = 0;

        public const int TCS_BUTTONS = 256;

        public const int TCS_MULTILINE = 512;

        public const int TCS_RIGHTJUSTIFY = 0;

        public const int TCS_FIXEDWIDTH = 1024;

        public const int TCS_RAGGEDRIGHT = 2048;

        public const int TCS_OWNERDRAWFIXED = 8192;

        public const int TCS_TOOLTIPS = 16384;

        public const int TCM_SETIMAGELIST = 4867;

        public const int TCIF_TEXT = 1;

        public const int TCIF_IMAGE = 2;

        public const int TCM_GETITEMA = 4869;

        public const int TCM_GETITEMW = 4924;

        public const int TCM_SETITEMA = 4870;

        public const int TCM_SETITEMW = 4925;

        public const int TCM_INSERTITEMA = 4871;

        public const int TCM_INSERTITEMW = 4926;

        public const int TCM_DELETEITEM = 4872;

        public const int TCM_DELETEALLITEMS = 4873;

        public const int TCM_GETITEMRECT = 4874;

        public const int TCM_GETCURSEL = 4875;

        public const int TCM_SETCURSEL = 4876;

        public const int TCM_ADJUSTRECT = 4904;

        public const int TCM_SETITEMSIZE = 4905;

        public const int TCM_SETPADDING = 4907;

        public const int TCM_GETROWCOUNT = 4908;

        public const int TCM_GETTOOLTIPS = 4909;

        public const int TCM_SETTOOLTIPS = 4910;

        public const int TCN_SELCHANGE = -551;

        public const int TCN_SELCHANGING = -552;

        public const int TBSTYLE_WRAPPABLE = 512;

        public const int TVM_SETBKCOLOR = 4381;

        public const int TVM_SETTEXTCOLOR = 4382;

        public const int TYMED_NULL = 0;

        public const int TVM_GETLINECOLOR = 4393;

        public const int TVM_SETLINECOLOR = 4392;

        public const int TVM_SETTOOLTIPS = 4376;

        public const int TVSIL_STATE = 2;

        public const int TVM_SORTCHILDRENCB = 4373;

        public const int TMPF_FIXED_PITCH = 1;

        public const int TVHT_NOWHERE = 1;

        public const int TVHT_ONITEMICON = 2;

        public const int TVHT_ONITEMLABEL = 4;

        public const int TVHT_ONITEM = 70;

        public const int TVHT_ONITEMINDENT = 8;

        public const int TVHT_ONITEMBUTTON = 16;

        public const int TVHT_ONITEMRIGHT = 32;

        public const int TVHT_ONITEMSTATEICON = 64;

        public const int TVHT_ABOVE = 256;

        public const int TVHT_BELOW = 512;

        public const int TVHT_TORIGHT = 1024;

        public const int TVHT_TOLEFT = 2048;

        public const int UIS_SET = 1;

        public const int UIS_CLEAR = 2;

        public const int UIS_INITIALIZE = 3;

        public const int UISF_HIDEFOCUS = 1;

        public const int UISF_HIDEACCEL = 2;

        public const int USERCLASSTYPE_FULL = 1;

        public const int USERCLASSTYPE_SHORT = 2;

        public const int USERCLASSTYPE_APPNAME = 3;

        public const int UOI_FLAGS = 1;

        public const int VIEW_E_DRAW = -2147221184;

        public const int VK_PRIOR = 33;

        public const int VK_NEXT = 34;

        public const int VK_LEFT = 37;

        public const int VK_UP = 38;

        public const int VK_RIGHT = 39;

        public const int VK_DOWN = 40;

        public const int VK_TAB = 9;

        public const int VK_SHIFT = 16;

        public const int VK_CONTROL = 17;

        public const int VK_MENU = 18;

        public const int VK_CAPITAL = 20;

        public const int VK_KANA = 21;

        public const int VK_ESCAPE = 27;

        public const int VK_END = 35;

        public const int VK_HOME = 36;

        public const int VK_NUMLOCK = 144;

        public const int VK_SCROLL = 145;

        public const int VK_INSERT = 45;

        public const int VK_DELETE = 46;

        public const int WH_JOURNALPLAYBACK = 1;

        public const int WH_GETMESSAGE = 3;

        public const int WH_MOUSE = 7;

        public const int WSF_VISIBLE = 1;

        public const int WM_NULL = 0;

        public const int WM_CREATE = 1;

        public const int WM_DELETEITEM = 45;

        public const int WM_DESTROY = 2;

        public const int WM_MOVE = 3;

        public const int WM_SIZE = 5;

        public const int WM_ACTIVATE = 6;

        public const int WA_INACTIVE = 0;

        public const int WA_ACTIVE = 1;

        public const int WA_CLICKACTIVE = 2;

        public const int WM_SETFOCUS = 7;

        public const int WM_KILLFOCUS = 8;

        public const int WM_ENABLE = 10;

        public const int WM_SETREDRAW = 11;

        public const int WM_SETTEXT = 12;

        public const int WM_GETTEXT = 13;

        public const int WM_GETTEXTLENGTH = 14;

        public const int WM_PAINT = 15;

        public const int WM_CLOSE = 16;

        public const int WM_QUERYENDSESSION = 17;

        public const int WM_QUIT = 18;

        public const int WM_QUERYOPEN = 19;

        public const int WM_ERASEBKGND = 20;

        public const int WM_SYSCOLORCHANGE = 21;

        public const int WM_ENDSESSION = 22;

        public const int WM_SHOWWINDOW = 24;

        public const int WM_WININICHANGE = 26;

        public const int WM_SETTINGCHANGE = 26;

        public const int WM_DEVMODECHANGE = 27;

        public const int WM_ACTIVATEAPP = 28;

        public const int WM_FONTCHANGE = 29;

        public const int WM_TIMECHANGE = 30;

        public const int WM_CANCELMODE = 31;

        public const int WM_SETCURSOR = 32;

        public const int WM_MOUSEACTIVATE = 33;

        public const int WM_CHILDACTIVATE = 34;

        public const int WM_QUEUESYNC = 35;

        public const int WM_GETMINMAXINFO = 36;

        public const int WM_PAINTICON = 38;

        public const int WM_ICONERASEBKGND = 39;

        public const int WM_NEXTDLGCTL = 40;

        public const int WM_SPOOLERSTATUS = 42;

        public const int WM_DRAWITEM = 43;

        public const int WM_MEASUREITEM = 44;

        public const int WM_VKEYTOITEM = 46;

        public const int WM_CHARTOITEM = 47;

        public const int WM_SETFONT = 48;

        public const int WM_GETFONT = 49;

        public const int WM_SETHOTKEY = 50;

        public const int WM_GETHOTKEY = 51;

        public const int WM_QUERYDRAGICON = 55;

        public const int WM_COMPAREITEM = 57;

        public const int WM_GETOBJECT = 61;

        public const int WM_COMPACTING = 65;

        public const int WM_COMMNOTIFY = 68;

        public const int WM_WINDOWPOSCHANGING = 70;

        public const int WM_WINDOWPOSCHANGED = 71;

        public const int WM_POWER = 72;

        public const int WM_COPYDATA = 74;

        public const int WM_CANCELJOURNAL = 75;

        public const int WM_NOTIFY = 78;

        public const int WM_INPUTLANGCHANGEREQUEST = 80;

        public const int WM_INPUTLANGCHANGE = 81;

        public const int WM_TCARD = 82;

        public const int WM_HELP = 83;

        public const int WM_USERCHANGED = 84;

        public const int WM_NOTIFYFORMAT = 85;

        public const int WM_CONTEXTMENU = 123;

        public const int WM_STYLECHANGING = 124;

        public const int WM_STYLECHANGED = 125;

        public const int WM_DISPLAYCHANGE = 126;

        public const int WM_GETICON = 127;

        public const int WM_SETICON = 128;

        public const int WM_NCCREATE = 129;

        public const int WM_NCDESTROY = 130;

        public const int WM_NCCALCSIZE = 131;

        public const int WM_NCHITTEST = 132;

        public const int WM_NCPAINT = 133;

        public const int WM_NCACTIVATE = 134;

        public const int WM_GETDLGCODE = 135;

        public const int WM_NCMOUSEMOVE = 160;

        public const int WM_NCMOUSELEAVE = 674;

        public const int WM_NCLBUTTONDOWN = 161;

        public const int WM_NCLBUTTONUP = 162;

        public const int WM_NCLBUTTONDBLCLK = 163;

        public const int WM_NCRBUTTONDOWN = 164;

        public const int WM_NCRBUTTONUP = 165;

        public const int WM_NCRBUTTONDBLCLK = 166;

        public const int WM_NCMBUTTONDOWN = 167;

        public const int WM_NCMBUTTONUP = 168;

        public const int WM_NCMBUTTONDBLCLK = 169;

        public const int WM_NCXBUTTONDOWN = 171;

        public const int WM_NCXBUTTONUP = 172;

        public const int WM_NCXBUTTONDBLCLK = 173;

        public const int WM_KEYFIRST = 256;

        public const int WM_KEYDOWN = 256;

        public const int WM_KEYUP = 257;

        public const int WM_CHAR = 258;

        public const int WM_DEADCHAR = 259;

        public const int WM_CTLCOLOR = 25;

        public const int WM_SYSKEYDOWN = 260;

        public const int WM_SYSKEYUP = 261;

        public const int WM_SYSCHAR = 262;

        public const int WM_SYSDEADCHAR = 263;

        public const int WM_KEYLAST = 264;

        public const int WM_IME_STARTCOMPOSITION = 269;

        public const int WM_IME_ENDCOMPOSITION = 270;

        public const int WM_IME_COMPOSITION = 271;

        public const int WM_IME_KEYLAST = 271;

        public const int WM_INITDIALOG = 272;

        public const int WM_COMMAND = 273;

        public const int WM_SYSCOMMAND = 274;

        public const int WM_TIMER = 275;

        public const int WM_HSCROLL = 276;

        public const int WM_VSCROLL = 277;

        public const int WM_INITMENU = 278;

        public const int WM_INITMENUPOPUP = 279;

        public const int WM_MENUSELECT = 287;

        public const int WM_MENUCHAR = 288;

        public const int WM_ENTERIDLE = 289;

        public const int WM_UNINITMENUPOPUP = 293;

        public const int WM_CHANGEUISTATE = 295;

        public const int WM_UPDATEUISTATE = 296;

        public const int WM_QUERYUISTATE = 297;

        public const int WM_CTLCOLORMSGBOX = 306;

        public const int WM_CTLCOLOREDIT = 307;

        public const int WM_CTLCOLORLISTBOX = 308;

        public const int WM_CTLCOLORBTN = 309;

        public const int WM_CTLCOLORDLG = 310;

        public const int WM_CTLCOLORSCROLLBAR = 311;

        public const int WM_CTLCOLORSTATIC = 312;

        public const int WM_MOUSEFIRST = 512;

        public const int WM_MOUSEMOVE = 512;

        public const int WM_LBUTTONDOWN = 513;

        public const int WM_LBUTTONUP = 514;

        public const int WM_LBUTTONDBLCLK = 515;

        public const int WM_RBUTTONDOWN = 516;

        public const int WM_RBUTTONUP = 517;

        public const int WM_RBUTTONDBLCLK = 518;

        public const int WM_MBUTTONDOWN = 519;

        public const int WM_MBUTTONUP = 520;

        public const int WM_MBUTTONDBLCLK = 521;

        public const int WM_XBUTTONDOWN = 523;

        public const int WM_XBUTTONUP = 524;

        public const int WM_XBUTTONDBLCLK = 525;

        public const int WM_MOUSEWHEEL = 522;

        public const int WM_MOUSELAST = 522;

        public const int WHEEL_DELTA = 120;

        public const int WM_PARENTNOTIFY = 528;

        public const int WM_ENTERMENULOOP = 529;

        public const int WM_EXITMENULOOP = 530;

        public const int WM_NEXTMENU = 531;

        public const int WM_SIZING = 532;

        public const int WM_CAPTURECHANGED = 533;

        public const int WM_MOVING = 534;

        public const int WM_POWERBROADCAST = 536;

        public const int WM_DEVICECHANGE = 537;

        public const int WM_IME_SETCONTEXT = 641;

        public const int WM_IME_NOTIFY = 642;

        public const int WM_IME_CONTROL = 643;

        public const int WM_IME_COMPOSITIONFULL = 644;

        public const int WM_IME_SELECT = 645;

        public const int WM_IME_CHAR = 646;

        public const int WM_IME_KEYDOWN = 656;

        public const int WM_IME_KEYUP = 657;

        public const int WM_MDICREATE = 544;

        public const int WM_MDIDESTROY = 545;

        public const int WM_MDIACTIVATE = 546;

        public const int WM_MDIRESTORE = 547;

        public const int WM_MDINEXT = 548;

        public const int WM_MDIMAXIMIZE = 549;

        public const int WM_MDITILE = 550;

        public const int WM_MDICASCADE = 551;

        public const int WM_MDIICONARRANGE = 552;

        public const int WM_MDIGETACTIVE = 553;

        public const int WM_MDISETMENU = 560;

        public const int WM_ENTERSIZEMOVE = 561;

        public const int WM_EXITSIZEMOVE = 562;

        public const int WM_DROPFILES = 563;

        public const int WM_MDIREFRESHMENU = 564;

        public const int WM_MOUSEHOVER = 673;

        public const int WM_MOUSELEAVE = 675;

        public const int WM_DPICHANGED = 736;

        public const int WM_GETDPISCALEDSIZE = 737;

        public const int WM_DPICHANGED_BEFOREPARENT = 738;

        public const int WM_DPICHANGED_AFTERPARENT = 739;

        public const int WM_CUT = 768;

        public const int WM_COPY = 769;

        public const int WM_PASTE = 770;

        public const int WM_CLEAR = 771;

        public const int WM_UNDO = 772;

        public const int WM_RENDERFORMAT = 773;

        public const int WM_RENDERALLFORMATS = 774;

        public const int WM_DESTROYCLIPBOARD = 775;

        public const int WM_DRAWCLIPBOARD = 776;

        public const int WM_PAINTCLIPBOARD = 777;

        public const int WM_VSCROLLCLIPBOARD = 778;

        public const int WM_SIZECLIPBOARD = 779;

        public const int WM_ASKCBFORMATNAME = 780;

        public const int WM_CHANGECBCHAIN = 781;

        public const int WM_HSCROLLCLIPBOARD = 782;

        public const int WM_QUERYNEWPALETTE = 783;

        public const int WM_PALETTEISCHANGING = 784;

        public const int WM_PALETTECHANGED = 785;

        public const int WM_HOTKEY = 786;

        public const int WM_PRINT = 791;

        public const int WM_PRINTCLIENT = 792;

        public const int WM_THEMECHANGED = 794;

        public const int WM_HANDHELDFIRST = 856;

        public const int WM_HANDHELDLAST = 863;

        public const int WM_AFXFIRST = 864;

        public const int WM_AFXLAST = 895;

        public const int WM_PENWINFIRST = 896;

        public const int WM_PENWINLAST = 911;

        public const int WM_APP = 32768;

        public const int WM_USER = 1024;

        public const int WM_REFLECT = 8192;

        public const int WS_OVERLAPPED = 0;

        public const int WS_POPUP = int.MinValue;

        public const int WS_CHILD = 1073741824;

        public const int WS_MINIMIZE = 536870912;

        public const int WS_VISIBLE = 268435456;

        public const int WS_DISABLED = 134217728;

        public const int WS_CLIPSIBLINGS = 67108864;

        public const int WS_CLIPCHILDREN = 33554432;

        public const int WS_MAXIMIZE = 16777216;

        public const int WS_CAPTION = 12582912;

        public const int WS_BORDER = 8388608;

        public const int WS_DLGFRAME = 4194304;

        public const int WS_VSCROLL = 2097152;

        public const int WS_HSCROLL = 1048576;

        public const int WS_SYSMENU = 524288;

        public const int WS_THICKFRAME = 262144;

        public const int WS_TABSTOP = 65536;

        public const int WS_MINIMIZEBOX = 131072;

        public const int WS_MAXIMIZEBOX = 65536;

        public const int WS_EX_DLGMODALFRAME = 1;

        public const int WS_EX_MDICHILD = 64;

        public const int WS_EX_TOOLWINDOW = 128;

        public const int WS_EX_CLIENTEDGE = 512;

        public const int WS_EX_CONTEXTHELP = 1024;

        public const int WS_EX_RIGHT = 4096;

        public const int WS_EX_LEFT = 0;

        public const int WS_EX_RTLREADING = 8192;

        public const int WS_EX_LEFTSCROLLBAR = 16384;

        public const int WS_EX_CONTROLPARENT = 65536;

        public const int WS_EX_STATICEDGE = 131072;

        public const int WS_EX_APPWINDOW = 262144;

        public const int WS_EX_LAYERED = 524288;

        public const int WS_EX_TOPMOST = 8;

        public const int WS_EX_LAYOUTRTL = 4194304;

        public const int WS_EX_NOINHERITLAYOUT = 1048576;

        public const int WPF_SETMINPOSITION = 1;

        public const int WM_CHOOSEFONT_GETLOGFONT = 1025;

        public const int IMN_OPENSTATUSWINDOW = 2;

        public const int IMN_SETCONVERSIONMODE = 6;

        public const int IMN_SETOPENSTATUS = 8;

        public static int START_PAGE_GENERAL;

        public const int PD_RESULT_CANCEL = 0;

        public const int PD_RESULT_PRINT = 1;

        public const int PD_RESULT_APPLY = 2;

        private static int wmMouseEnterMessage;

        private static int wmUnSubclass;

        public const int XBUTTON1 = 1;

        public const int XBUTTON2 = 2;

        public static readonly int BFFM_SETSELECTION;

        public static readonly int CBEM_GETITEM;

        public static readonly int CBEM_SETITEM;

        public static readonly int CBEN_ENDEDIT;

        public static readonly int CBEM_INSERTITEM;

        public static readonly int LVM_GETITEMTEXT;

        public static readonly int LVM_SETITEMTEXT;

        public static readonly int ACM_OPEN;

        public static readonly int DTM_SETFORMAT;

        public static readonly int DTN_USERSTRING;

        public static readonly int DTN_WMKEYDOWN;

        public static readonly int DTN_FORMAT;

        public static readonly int DTN_FORMATQUERY;

        public static readonly int EMR_POLYTEXTOUT;

        public static readonly int HDM_INSERTITEM;

        public static readonly int HDM_GETITEM;

        public static readonly int HDM_SETITEM;

        public static readonly int HDN_ITEMCHANGING;

        public static readonly int HDN_ITEMCHANGED;

        public static readonly int HDN_ITEMCLICK;

        public static readonly int HDN_ITEMDBLCLICK;

        public static readonly int HDN_DIVIDERDBLCLICK;

        public static readonly int HDN_BEGINTRACK;

        public static readonly int HDN_ENDTRACK;

        public static readonly int HDN_TRACK;

        public static readonly int HDN_GETDISPINFO;

        public static readonly int LVM_GETITEM;

        public static readonly int LVM_SETBKIMAGE;

        public static readonly int LVM_SETITEM;

        public static readonly int LVM_INSERTITEM;

        public static readonly int LVM_FINDITEM;

        public static readonly int LVM_GETSTRINGWIDTH;

        public static readonly int LVM_EDITLABEL;

        public static readonly int LVM_GETCOLUMN;

        public static readonly int LVM_SETCOLUMN;

        public static readonly int LVM_GETISEARCHSTRING;

        public static readonly int LVM_INSERTCOLUMN;

        public static readonly int LVN_BEGINLABELEDIT;

        public static readonly int LVN_ENDLABELEDIT;

        public static readonly int LVN_ODFINDITEM;

        public static readonly int LVN_GETDISPINFO;

        public static readonly int LVN_GETINFOTIP;

        public static readonly int LVN_SETDISPINFO;

        public static readonly int PSM_SETTITLE;

        public static readonly int PSM_SETFINISHTEXT;

        public static readonly int RB_INSERTBAND;

        public static readonly int SB_SETTEXT;

        public static readonly int SB_GETTEXT;

        public static readonly int SB_GETTEXTLENGTH;

        public static readonly int SB_SETTIPTEXT;

        public static readonly int SB_GETTIPTEXT;

        public static readonly int TB_SAVERESTORE;

        public static readonly int TB_ADDSTRING;

        public static readonly int TB_GETBUTTONTEXT;

        public static readonly int TB_MAPACCELERATOR;

        public static readonly int TB_GETBUTTONINFO;

        public static readonly int TB_SETBUTTONINFO;

        public static readonly int TB_INSERTBUTTON;

        public static readonly int TB_ADDBUTTONS;

        public static readonly int TBN_GETBUTTONINFO;

        public static readonly int TBN_GETINFOTIP;

        public static readonly int TBN_GETDISPINFO;

        public static readonly int TTM_ADDTOOL;

        public static readonly int TTM_SETTITLE;

        public static readonly int TTM_DELTOOL;

        public static readonly int TTM_NEWTOOLRECT;

        public static readonly int TTM_GETTOOLINFO;

        public static readonly int TTM_SETTOOLINFO;

        public static readonly int TTM_HITTEST;

        public static readonly int TTM_GETTEXT;

        public static readonly int TTM_UPDATETIPTEXT;

        public static readonly int TTM_ENUMTOOLS;

        public static readonly int TTM_GETCURRENTTOOL;

        public static readonly int TTN_GETDISPINFO;

        public static readonly int TTN_NEEDTEXT;

        public static readonly int TVM_INSERTITEM;

        public static readonly int TVM_GETITEM;

        public static readonly int TVM_SETITEM;

        public static readonly int TVM_EDITLABEL;

        public static readonly int TVM_GETISEARCHSTRING;

        public static readonly int TVN_SELCHANGING;

        public static readonly int TVN_SELCHANGED;

        public static readonly int TVN_GETDISPINFO;

        public static readonly int TVN_SETDISPINFO;

        public static readonly int TVN_ITEMEXPANDING;

        public static readonly int TVN_ITEMEXPANDED;

        public static readonly int TVN_BEGINDRAG;

        public static readonly int TVN_BEGINRDRAG;

        public static readonly int TVN_BEGINLABELEDIT;

        public static readonly int TVN_ENDLABELEDIT;

        public static readonly int TCM_GETITEM;

        public static readonly int TCM_SETITEM;

        public static readonly int TCM_INSERTITEM;

        public const string TOOLTIPS_CLASS = "tooltips_class32";

        public const string WC_DATETIMEPICK = "SysDateTimePick32";

        public const string WC_LISTVIEW = "SysListView32";

        public const string WC_MONTHCAL = "SysMonthCal32";

        public const string WC_PROGRESS = "msctls_progress32";

        public const string WC_STATUSBAR = "msctls_statusbar32";

        public const string WC_TOOLBAR = "ToolbarWindow32";

        public const string WC_TRACKBAR = "msctls_trackbar32";

        public const string WC_TREEVIEW = "SysTreeView32";

        public const string WC_TABCONTROL = "SysTabControl32";

        public const string MSH_MOUSEWHEEL = "MSWHEEL_ROLLMSG";

        public const string MSH_SCROLL_LINES = "MSH_SCROLL_LINES_MSG";

        public const string MOUSEZ_CLASSNAME = "MouseZ";

        public const string MOUSEZ_TITLE = "Magellan MSWHEEL";

        public const int CHILDID_SELF = 0;

        public const int OBJID_QUERYCLASSNAMEIDX = -12;

        public const int OBJID_CLIENT = -4;

        public const int OBJID_WINDOW = 0;

        public const int UiaRootObjectId = -25;

        public const int UiaAppendRuntimeId = 3;

        public const string uuid_IAccessible = "{618736E0-3C3D-11CF-810C-00AA00389B71}";

        public const string uuid_IEnumVariant = "{00020404-0000-0000-C000-000000000046}";

        public const int HH_FTS_DEFAULT_PROXIMITY = -1;

        public const int HICF_OTHER = 0;

        public const int HICF_MOUSE = 1;

        public const int HICF_ARROWKEYS = 2;

        public const int HICF_ACCELERATOR = 4;

        public const int HICF_DUPACCEL = 8;

        public const int HICF_ENTERING = 16;

        public const int HICF_LEAVING = 32;

        public const int HICF_RESELECT = 64;

        public const int HICF_LMOUSE = 128;

        public const int HICF_TOGGLEDROPDOWN = 256;

        public const int DPI_AWARENESS_CONTEXT_UNAWARE = -1;

        public const int DPI_AWARENESS_CONTEXT_SYSTEM_AWARE = -2;

        public const int DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE = -3;

        public const int DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2 = -4;

        public const int STAP_ALLOW_NONCLIENT = 1;

        public const int STAP_ALLOW_CONTROLS = 2;

        public const int STAP_ALLOW_WEBCONTENT = 4;

        public const int PS_NULL = 5;

        public const int PS_INSIDEFRAME = 6;

        public const int PS_GEOMETRIC = 65536;

        public const int PS_ENDCAP_SQUARE = 256;

        public const int NULL_BRUSH = 5;

        public const int MM_HIMETRIC = 3;

        public const uint STILL_ACTIVE = 259u;

        internal const int UIA_InvokePatternId = 10000;

        internal const int UIA_SelectionPatternId = 10001;

        internal const int UIA_ValuePatternId = 10002;

        internal const int UIA_RangeValuePatternId = 10003;

        internal const int UIA_ScrollPatternId = 10004;

        internal const int UIA_ExpandCollapsePatternId = 10005;

        internal const int UIA_GridPatternId = 10006;

        internal const int UIA_GridItemPatternId = 10007;

        internal const int UIA_MultipleViewPatternId = 10008;

        internal const int UIA_WindowPatternId = 10009;

        internal const int UIA_SelectionItemPatternId = 10010;

        internal const int UIA_DockPatternId = 10011;

        internal const int UIA_TablePatternId = 10012;

        internal const int UIA_TableItemPatternId = 10013;

        internal const int UIA_TextPatternId = 10014;

        internal const int UIA_TogglePatternId = 10015;

        internal const int UIA_TransformPatternId = 10016;

        internal const int UIA_ScrollItemPatternId = 10017;

        internal const int UIA_LegacyIAccessiblePatternId = 10018;

        internal const int UIA_ItemContainerPatternId = 10019;

        internal const int UIA_VirtualizedItemPatternId = 10020;

        internal const int UIA_SynchronizedInputPatternId = 10021;

        internal const int UIA_ObjectModelPatternId = 10022;

        internal const int UIA_AnnotationPatternId = 10023;

        internal const int UIA_TextPattern2Id = 10024;

        internal const int UIA_StylesPatternId = 10025;

        internal const int UIA_SpreadsheetPatternId = 10026;

        internal const int UIA_SpreadsheetItemPatternId = 10027;

        internal const int UIA_TransformPattern2Id = 10028;

        internal const int UIA_TextChildPatternId = 10029;

        internal const int UIA_DragPatternId = 10030;

        internal const int UIA_DropTargetPatternId = 10031;

        internal const int UIA_TextEditPatternId = 10032;

        internal const int UIA_CustomNavigationPatternId = 10033;

        internal const int UIA_ToolTipOpenedEventId = 20000;

        internal const int UIA_ToolTipClosedEventId = 20001;

        internal const int UIA_StructureChangedEventId = 20002;

        internal const int UIA_MenuOpenedEventId = 20003;

        internal const int UIA_AutomationPropertyChangedEventId = 20004;

        internal const int UIA_AutomationFocusChangedEventId = 20005;

        internal const int UIA_AsyncContentLoadedEventId = 20006;

        internal const int UIA_MenuClosedEventId = 20007;

        internal const int UIA_LayoutInvalidatedEventId = 20008;

        internal const int UIA_Invoke_InvokedEventId = 20009;

        internal const int UIA_SelectionItem_ElementAddedToSelectionEventId = 20010;

        internal const int UIA_SelectionItem_ElementRemovedFromSelectionEventId = 20011;

        internal const int UIA_SelectionItem_ElementSelectedEventId = 20012;

        internal const int UIA_Selection_InvalidatedEventId = 20013;

        internal const int UIA_Text_TextSelectionChangedEventId = 20014;

        internal const int UIA_Text_TextChangedEventId = 20015;

        internal const int UIA_Window_WindowOpenedEventId = 20016;

        internal const int UIA_Window_WindowClosedEventId = 20017;

        internal const int UIA_MenuModeStartEventId = 20018;

        internal const int UIA_MenuModeEndEventId = 20019;

        internal const int UIA_InputReachedTargetEventId = 20020;

        internal const int UIA_InputReachedOtherElementEventId = 20021;

        internal const int UIA_InputDiscardedEventId = 20022;

        internal const int UIA_SystemAlertEventId = 20023;

        internal const int UIA_LiveRegionChangedEventId = 20024;

        internal const int UIA_HostedFragmentRootsInvalidatedEventId = 20025;

        internal const int UIA_Drag_DragStartEventId = 20026;

        internal const int UIA_Drag_DragCancelEventId = 20027;

        internal const int UIA_Drag_DragCompleteEventId = 20028;

        internal const int UIA_DropTarget_DragEnterEventId = 20029;

        internal const int UIA_DropTarget_DragLeaveEventId = 20030;

        internal const int UIA_DropTarget_DroppedEventId = 20031;

        internal const int UIA_TextEdit_TextChangedEventId = 20032;

        internal const int UIA_TextEdit_ConversionTargetChangedEventId = 20033;

        internal const int UIA_ChangesEventId = 20034;

        internal const int UIA_RuntimeIdPropertyId = 30000;

        internal const int UIA_BoundingRectanglePropertyId = 30001;

        internal const int UIA_ProcessIdPropertyId = 30002;

        internal const int UIA_ControlTypePropertyId = 30003;

        internal const int UIA_LocalizedControlTypePropertyId = 30004;

        internal const int UIA_NamePropertyId = 30005;

        internal const int UIA_AcceleratorKeyPropertyId = 30006;

        internal const int UIA_AccessKeyPropertyId = 30007;

        internal const int UIA_HasKeyboardFocusPropertyId = 30008;

        internal const int UIA_IsKeyboardFocusablePropertyId = 30009;

        internal const int UIA_IsEnabledPropertyId = 30010;

        internal const int UIA_AutomationIdPropertyId = 30011;

        internal const int UIA_ClassNamePropertyId = 30012;

        internal const int UIA_HelpTextPropertyId = 30013;

        internal const int UIA_ClickablePointPropertyId = 30014;

        internal const int UIA_CulturePropertyId = 30015;

        internal const int UIA_IsControlElementPropertyId = 30016;

        internal const int UIA_IsContentElementPropertyId = 30017;

        internal const int UIA_LabeledByPropertyId = 30018;

        internal const int UIA_IsPasswordPropertyId = 30019;

        internal const int UIA_NativeWindowHandlePropertyId = 30020;

        internal const int UIA_ItemTypePropertyId = 30021;

        internal const int UIA_IsOffscreenPropertyId = 30022;

        internal const int UIA_OrientationPropertyId = 30023;

        internal const int UIA_FrameworkIdPropertyId = 30024;

        internal const int UIA_IsRequiredForFormPropertyId = 30025;

        internal const int UIA_ItemStatusPropertyId = 30026;

        internal const int UIA_IsDockPatternAvailablePropertyId = 30027;

        internal const int UIA_IsExpandCollapsePatternAvailablePropertyId = 30028;

        internal const int UIA_IsGridItemPatternAvailablePropertyId = 30029;

        internal const int UIA_IsGridPatternAvailablePropertyId = 30030;

        internal const int UIA_IsInvokePatternAvailablePropertyId = 30031;

        internal const int UIA_IsMultipleViewPatternAvailablePropertyId = 30032;

        internal const int UIA_IsRangeValuePatternAvailablePropertyId = 30033;

        internal const int UIA_IsScrollPatternAvailablePropertyId = 30034;

        internal const int UIA_IsScrollItemPatternAvailablePropertyId = 30035;

        internal const int UIA_IsSelectionItemPatternAvailablePropertyId = 30036;

        internal const int UIA_IsSelectionPatternAvailablePropertyId = 30037;

        internal const int UIA_IsTablePatternAvailablePropertyId = 30038;

        internal const int UIA_IsTableItemPatternAvailablePropertyId = 30039;

        internal const int UIA_IsTextPatternAvailablePropertyId = 30040;

        internal const int UIA_IsTogglePatternAvailablePropertyId = 30041;

        internal const int UIA_IsTransformPatternAvailablePropertyId = 30042;

        internal const int UIA_IsValuePatternAvailablePropertyId = 30043;

        internal const int UIA_IsWindowPatternAvailablePropertyId = 30044;

        internal const int UIA_ValueValuePropertyId = 30045;

        internal const int UIA_ValueIsReadOnlyPropertyId = 30046;

        internal const int UIA_RangeValueValuePropertyId = 30047;

        internal const int UIA_RangeValueIsReadOnlyPropertyId = 30048;

        internal const int UIA_RangeValueMinimumPropertyId = 30049;

        internal const int UIA_RangeValueMaximumPropertyId = 30050;

        internal const int UIA_RangeValueLargeChangePropertyId = 30051;

        internal const int UIA_RangeValueSmallChangePropertyId = 30052;

        internal const int UIA_ScrollHorizontalScrollPercentPropertyId = 30053;

        internal const int UIA_ScrollHorizontalViewSizePropertyId = 30054;

        internal const int UIA_ScrollVerticalScrollPercentPropertyId = 30055;

        internal const int UIA_ScrollVerticalViewSizePropertyId = 30056;

        internal const int UIA_ScrollHorizontallyScrollablePropertyId = 30057;

        internal const int UIA_ScrollVerticallyScrollablePropertyId = 30058;

        internal const int UIA_SelectionSelectionPropertyId = 30059;

        internal const int UIA_SelectionCanSelectMultiplePropertyId = 30060;

        internal const int UIA_SelectionIsSelectionRequiredPropertyId = 30061;

        internal const int UIA_GridRowCountPropertyId = 30062;

        internal const int UIA_GridColumnCountPropertyId = 30063;

        internal const int UIA_GridItemRowPropertyId = 30064;

        internal const int UIA_GridItemColumnPropertyId = 30065;

        internal const int UIA_GridItemRowSpanPropertyId = 30066;

        internal const int UIA_GridItemColumnSpanPropertyId = 30067;

        internal const int UIA_GridItemContainingGridPropertyId = 30068;

        internal const int UIA_DockDockPositionPropertyId = 30069;

        internal const int UIA_ExpandCollapseExpandCollapseStatePropertyId = 30070;

        internal const int UIA_MultipleViewCurrentViewPropertyId = 30071;

        internal const int UIA_MultipleViewSupportedViewsPropertyId = 30072;

        internal const int UIA_WindowCanMaximizePropertyId = 30073;

        internal const int UIA_WindowCanMinimizePropertyId = 30074;

        internal const int UIA_WindowWindowVisualStatePropertyId = 30075;

        internal const int UIA_WindowWindowInteractionStatePropertyId = 30076;

        internal const int UIA_WindowIsModalPropertyId = 30077;

        internal const int UIA_WindowIsTopmostPropertyId = 30078;

        internal const int UIA_SelectionItemIsSelectedPropertyId = 30079;

        internal const int UIA_SelectionItemSelectionContainerPropertyId = 30080;

        internal const int UIA_TableRowHeadersPropertyId = 30081;

        internal const int UIA_TableColumnHeadersPropertyId = 30082;

        internal const int UIA_TableRowOrColumnMajorPropertyId = 30083;

        internal const int UIA_TableItemRowHeaderItemsPropertyId = 30084;

        internal const int UIA_TableItemColumnHeaderItemsPropertyId = 30085;

        internal const int UIA_ToggleToggleStatePropertyId = 30086;

        internal const int UIA_TransformCanMovePropertyId = 30087;

        internal const int UIA_TransformCanResizePropertyId = 30088;

        internal const int UIA_TransformCanRotatePropertyId = 30089;

        internal const int UIA_IsLegacyIAccessiblePatternAvailablePropertyId = 30090;

        internal const int UIA_LegacyIAccessibleChildIdPropertyId = 30091;

        internal const int UIA_LegacyIAccessibleNamePropertyId = 30092;

        internal const int UIA_LegacyIAccessibleValuePropertyId = 30093;

        internal const int UIA_LegacyIAccessibleDescriptionPropertyId = 30094;

        internal const int UIA_LegacyIAccessibleRolePropertyId = 30095;

        internal const int UIA_LegacyIAccessibleStatePropertyId = 30096;

        internal const int UIA_LegacyIAccessibleHelpPropertyId = 30097;

        internal const int UIA_LegacyIAccessibleKeyboardShortcutPropertyId = 30098;

        internal const int UIA_LegacyIAccessibleSelectionPropertyId = 30099;

        internal const int UIA_LegacyIAccessibleDefaultActionPropertyId = 30100;

        internal const int UIA_AriaRolePropertyId = 30101;

        internal const int UIA_AriaPropertiesPropertyId = 30102;

        internal const int UIA_IsDataValidForFormPropertyId = 30103;

        internal const int UIA_ControllerForPropertyId = 30104;

        internal const int UIA_DescribedByPropertyId = 30105;

        internal const int UIA_FlowsToPropertyId = 30106;

        internal const int UIA_ProviderDescriptionPropertyId = 30107;

        internal const int UIA_IsItemContainerPatternAvailablePropertyId = 30108;

        internal const int UIA_IsVirtualizedItemPatternAvailablePropertyId = 30109;

        internal const int UIA_IsSynchronizedInputPatternAvailablePropertyId = 30110;

        internal const int UIA_OptimizeForVisualContentPropertyId = 30111;

        internal const int UIA_IsObjectModelPatternAvailablePropertyId = 30112;

        internal const int UIA_AnnotationAnnotationTypeIdPropertyId = 30113;

        internal const int UIA_AnnotationAnnotationTypeNamePropertyId = 30114;

        internal const int UIA_AnnotationAuthorPropertyId = 30115;

        internal const int UIA_AnnotationDateTimePropertyId = 30116;

        internal const int UIA_AnnotationTargetPropertyId = 30117;

        internal const int UIA_IsAnnotationPatternAvailablePropertyId = 30118;

        internal const int UIA_IsTextPattern2AvailablePropertyId = 30119;

        internal const int UIA_StylesStyleIdPropertyId = 30120;

        internal const int UIA_StylesStyleNamePropertyId = 30121;

        internal const int UIA_StylesFillColorPropertyId = 30122;

        internal const int UIA_StylesFillPatternStylePropertyId = 30123;

        internal const int UIA_StylesShapePropertyId = 30124;

        internal const int UIA_StylesFillPatternColorPropertyId = 30125;

        internal const int UIA_StylesExtendedPropertiesPropertyId = 30126;

        internal const int UIA_IsStylesPatternAvailablePropertyId = 30127;

        internal const int UIA_IsSpreadsheetPatternAvailablePropertyId = 30128;

        internal const int UIA_SpreadsheetItemFormulaPropertyId = 30129;

        internal const int UIA_SpreadsheetItemAnnotationObjectsPropertyId = 30130;

        internal const int UIA_SpreadsheetItemAnnotationTypesPropertyId = 30131;

        internal const int UIA_IsSpreadsheetItemPatternAvailablePropertyId = 30132;

        internal const int UIA_Transform2CanZoomPropertyId = 30133;

        internal const int UIA_IsTransformPattern2AvailablePropertyId = 30134;

        internal const int UIA_LiveSettingPropertyId = 30135;

        internal const int UIA_IsTextChildPatternAvailablePropertyId = 30136;

        internal const int UIA_IsDragPatternAvailablePropertyId = 30137;

        internal const int UIA_DragIsGrabbedPropertyId = 30138;

        internal const int UIA_DragDropEffectPropertyId = 30139;

        internal const int UIA_DragDropEffectsPropertyId = 30140;

        internal const int UIA_IsDropTargetPatternAvailablePropertyId = 30141;

        internal const int UIA_DropTargetDropTargetEffectPropertyId = 30142;

        internal const int UIA_DropTargetDropTargetEffectsPropertyId = 30143;

        internal const int UIA_DragGrabbedItemsPropertyId = 30144;

        internal const int UIA_Transform2ZoomLevelPropertyId = 30145;

        internal const int UIA_Transform2ZoomMinimumPropertyId = 30146;

        internal const int UIA_Transform2ZoomMaximumPropertyId = 30147;

        internal const int UIA_FlowsFromPropertyId = 30148;

        internal const int UIA_IsTextEditPatternAvailablePropertyId = 30149;

        internal const int UIA_IsPeripheralPropertyId = 30150;

        internal const int UIA_IsCustomNavigationPatternAvailablePropertyId = 30151;

        internal const int UIA_PositionInSetPropertyId = 30152;

        internal const int UIA_SizeOfSetPropertyId = 30153;

        internal const int UIA_LevelPropertyId = 30154;

        internal const int UIA_AnnotationTypesPropertyId = 30155;

        internal const int UIA_AnnotationObjectsPropertyId = 30156;

        internal const int UIA_LandmarkTypePropertyId = 30157;

        internal const int UIA_LocalizedLandmarkTypePropertyId = 30158;

        internal const int UIA_FullDescriptionPropertyId = 30159;

        internal const int UIA_FillColorPropertyId = 30160;

        internal const int UIA_OutlineColorPropertyId = 30161;

        internal const int UIA_FillTypePropertyId = 30162;

        internal const int UIA_VisualEffectsPropertyId = 30163;

        internal const int UIA_OutlineThicknessPropertyId = 30164;

        internal const int UIA_CenterPointPropertyId = 30165;

        internal const int UIA_RotationPropertyId = 30166;

        internal const int UIA_SizePropertyId = 30167;

        internal const int UIA_ButtonControlTypeId = 50000;

        internal const int UIA_CalendarControlTypeId = 50001;

        internal const int UIA_CheckBoxControlTypeId = 50002;

        internal const int UIA_ComboBoxControlTypeId = 50003;

        internal const int UIA_EditControlTypeId = 50004;

        internal const int UIA_HyperlinkControlTypeId = 50005;

        internal const int UIA_ImageControlTypeId = 50006;

        internal const int UIA_ListItemControlTypeId = 50007;

        internal const int UIA_ListControlTypeId = 50008;

        internal const int UIA_MenuControlTypeId = 50009;

        internal const int UIA_MenuBarControlTypeId = 50010;

        internal const int UIA_MenuItemControlTypeId = 50011;

        internal const int UIA_ProgressBarControlTypeId = 50012;

        internal const int UIA_RadioButtonControlTypeId = 50013;

        internal const int UIA_ScrollBarControlTypeId = 50014;

        internal const int UIA_SliderControlTypeId = 50015;

        internal const int UIA_SpinnerControlTypeId = 50016;

        internal const int UIA_StatusBarControlTypeId = 50017;

        internal const int UIA_TabControlTypeId = 50018;

        internal const int UIA_TabItemControlTypeId = 50019;

        internal const int UIA_TextControlTypeId = 50020;

        internal const int UIA_ToolBarControlTypeId = 50021;

        internal const int UIA_ToolTipControlTypeId = 50022;

        internal const int UIA_TreeControlTypeId = 50023;

        internal const int UIA_TreeItemControlTypeId = 50024;

        internal const int UIA_CustomControlTypeId = 50025;

        internal const int UIA_GroupControlTypeId = 50026;

        internal const int UIA_ThumbControlTypeId = 50027;

        internal const int UIA_DataGridControlTypeId = 50028;

        internal const int UIA_DataItemControlTypeId = 50029;

        internal const int UIA_DocumentControlTypeId = 50030;

        internal const int UIA_SplitButtonControlTypeId = 50031;

        internal const int UIA_WindowControlTypeId = 50032;

        internal const int UIA_PaneControlTypeId = 50033;

        internal const int UIA_HeaderControlTypeId = 50034;

        internal const int UIA_HeaderItemControlTypeId = 50035;

        internal const int UIA_TableControlTypeId = 50036;

        internal const int UIA_TitleBarControlTypeId = 50037;

        internal const int UIA_SeparatorControlTypeId = 50038;

        internal const int UIA_SemanticZoomControlTypeId = 50039;

        internal const int UIA_AppBarControlTypeId = 50040;

        internal const int LOAD_LIBRARY_SEARCH_SYSTEM32 = 2048;

        public static int WM_MOUSEENTER
        {
            get
            {
                if (wmMouseEnterMessage == -1)
                {
                    wmMouseEnterMessage = SafeNativeMethods.RegisterWindowMessage("WinFormsMouseEnter");
                }
                return wmMouseEnterMessage;
            }
        }

        public static int WM_UIUNSUBCLASS
        {
            get
            {
                if (wmUnSubclass == -1)
                {
                    wmUnSubclass = SafeNativeMethods.RegisterWindowMessage("WinFormsUnSubclass");
                }
                return wmUnSubclass;
            }
        }

        public static int MAKELANGID(int primary, int sub)
        {
            return ((ushort)sub << 10) | (ushort)primary;
        }

        public static int MAKELCID(int lgid)
        {
            return MAKELCID(lgid, 0);
        }

        public static int MAKELCID(int lgid, int sort)
        {
            return (0xFFFF & lgid) | ((0xF & sort) << 16);
        }

        public static bool Succeeded(int hr)
        {
            return hr >= 0;
        }

        public static bool Failed(int hr)
        {
            return hr < 0;
        }

        static NativeMethods()
        {
            InvalidIntPtr = (IntPtr)(-1);
            LPSTR_TEXTCALLBACK = (IntPtr)(-1);
            NullHandleRef = new HandleRef(null, IntPtr.Zero);
            HWND_TOP = new HandleRef(null, (IntPtr)0);
            HWND_BOTTOM = new HandleRef(null, (IntPtr)1);
            HWND_TOPMOST = new HandleRef(null, new IntPtr(-1));
            HWND_NOTOPMOST = new HandleRef(null, new IntPtr(-2));
            HWND_MESSAGE = new HandleRef(null, new IntPtr(-3));
            LOCALE_USER_DEFAULT = MAKELCID(LANG_USER_DEFAULT);
            LANG_USER_DEFAULT = MAKELANGID(0, 1);
            START_PAGE_GENERAL = -1;
            wmMouseEnterMessage = -1;
            wmUnSubclass = -1;
            if (Marshal.SystemDefaultCharSize == 1)
            {
                BFFM_SETSELECTION = 1126;
                CBEM_GETITEM = 1028;
                CBEM_SETITEM = 1029;
                CBEN_ENDEDIT = -805;
                CBEM_INSERTITEM = 1025;
                LVM_GETITEMTEXT = 4141;
                LVM_SETITEMTEXT = 4142;
                ACM_OPEN = 1124;
                DTM_SETFORMAT = 4101;
                DTN_USERSTRING = -758;
                DTN_WMKEYDOWN = -757;
                DTN_FORMAT = -756;
                DTN_FORMATQUERY = -755;
                EMR_POLYTEXTOUT = 96;
                HDM_INSERTITEM = 4609;
                HDM_GETITEM = 4611;
                HDM_SETITEM = 4612;
                HDN_ITEMCHANGING = -300;
                HDN_ITEMCHANGED = -301;
                HDN_ITEMCLICK = -302;
                HDN_ITEMDBLCLICK = -303;
                HDN_DIVIDERDBLCLICK = -305;
                HDN_BEGINTRACK = -306;
                HDN_ENDTRACK = -307;
                HDN_TRACK = -308;
                HDN_GETDISPINFO = -309;
                LVM_SETBKIMAGE = 4164;
                LVM_GETITEM = 4101;
                LVM_SETITEM = 4102;
                LVM_INSERTITEM = 4103;
                LVM_FINDITEM = 4109;
                LVM_GETSTRINGWIDTH = 4113;
                LVM_EDITLABEL = 4119;
                LVM_GETCOLUMN = 4121;
                LVM_SETCOLUMN = 4122;
                LVM_GETISEARCHSTRING = 4148;
                LVM_INSERTCOLUMN = 4123;
                LVN_BEGINLABELEDIT = -105;
                LVN_ENDLABELEDIT = -106;
                LVN_ODFINDITEM = -152;
                LVN_GETDISPINFO = -150;
                LVN_GETINFOTIP = -157;
                LVN_SETDISPINFO = -151;
                PSM_SETTITLE = 1135;
                PSM_SETFINISHTEXT = 1139;
                RB_INSERTBAND = 1025;
                SB_SETTEXT = 1025;
                SB_GETTEXT = 1026;
                SB_GETTEXTLENGTH = 1027;
                SB_SETTIPTEXT = 1040;
                SB_GETTIPTEXT = 1042;
                TB_SAVERESTORE = 1050;
                TB_ADDSTRING = 1052;
                TB_GETBUTTONTEXT = 1069;
                TB_MAPACCELERATOR = 1102;
                TB_GETBUTTONINFO = 1089;
                TB_SETBUTTONINFO = 1090;
                TB_INSERTBUTTON = 1045;
                TB_ADDBUTTONS = 1044;
                TBN_GETBUTTONINFO = -700;
                TBN_GETINFOTIP = -718;
                TBN_GETDISPINFO = -716;
                TTM_ADDTOOL = 1028;
                TTM_SETTITLE = 1056;
                TTM_DELTOOL = 1029;
                TTM_NEWTOOLRECT = 1030;
                TTM_GETTOOLINFO = 1032;
                TTM_SETTOOLINFO = 1033;
                TTM_HITTEST = 1034;
                TTM_GETTEXT = 1035;
                TTM_UPDATETIPTEXT = 1036;
                TTM_ENUMTOOLS = 1038;
                TTM_GETCURRENTTOOL = 1039;
                TTN_GETDISPINFO = -520;
                TTN_NEEDTEXT = -520;
                TVM_INSERTITEM = 4352;
                TVM_GETITEM = 4364;
                TVM_SETITEM = 4365;
                TVM_EDITLABEL = 4366;
                TVM_GETISEARCHSTRING = 4375;
                TVN_SELCHANGING = -401;
                TVN_SELCHANGED = -402;
                TVN_GETDISPINFO = -403;
                TVN_SETDISPINFO = -404;
                TVN_ITEMEXPANDING = -405;
                TVN_ITEMEXPANDED = -406;
                TVN_BEGINDRAG = -407;
                TVN_BEGINRDRAG = -408;
                TVN_BEGINLABELEDIT = -410;
                TVN_ENDLABELEDIT = -411;
                TCM_GETITEM = 4869;
                TCM_SETITEM = 4870;
                TCM_INSERTITEM = 4871;
            }
            else
            {
                BFFM_SETSELECTION = 1127;
                CBEM_GETITEM = 1037;
                CBEM_SETITEM = 1036;
                CBEN_ENDEDIT = -806;
                CBEM_INSERTITEM = 1035;
                LVM_GETITEMTEXT = 4211;
                LVM_SETITEMTEXT = 4212;
                ACM_OPEN = 1127;
                DTM_SETFORMAT = 4146;
                DTN_USERSTRING = -745;
                DTN_WMKEYDOWN = -744;
                DTN_FORMAT = -743;
                DTN_FORMATQUERY = -742;
                EMR_POLYTEXTOUT = 97;
                HDM_INSERTITEM = 4618;
                HDM_GETITEM = 4619;
                HDM_SETITEM = 4620;
                HDN_ITEMCHANGING = -320;
                HDN_ITEMCHANGED = -321;
                HDN_ITEMCLICK = -322;
                HDN_ITEMDBLCLICK = -323;
                HDN_DIVIDERDBLCLICK = -325;
                HDN_BEGINTRACK = -326;
                HDN_ENDTRACK = -327;
                HDN_TRACK = -328;
                HDN_GETDISPINFO = -329;
                LVM_SETBKIMAGE = 4234;
                LVM_GETITEM = 4171;
                LVM_SETITEM = 4172;
                LVM_INSERTITEM = 4173;
                LVM_FINDITEM = 4179;
                LVM_GETSTRINGWIDTH = 4183;
                LVM_EDITLABEL = 4214;
                LVM_GETCOLUMN = 4191;
                LVM_SETCOLUMN = 4192;
                LVM_GETISEARCHSTRING = 4213;
                LVM_INSERTCOLUMN = 4193;
                LVN_BEGINLABELEDIT = -175;
                LVN_ENDLABELEDIT = -176;
                LVN_ODFINDITEM = -179;
                LVN_GETDISPINFO = -177;
                LVN_GETINFOTIP = -158;
                LVN_SETDISPINFO = -178;
                PSM_SETTITLE = 1144;
                PSM_SETFINISHTEXT = 1145;
                RB_INSERTBAND = 1034;
                SB_SETTEXT = 1035;
                SB_GETTEXT = 1037;
                SB_GETTEXTLENGTH = 1036;
                SB_SETTIPTEXT = 1041;
                SB_GETTIPTEXT = 1043;
                TB_SAVERESTORE = 1100;
                TB_ADDSTRING = 1101;
                TB_GETBUTTONTEXT = 1099;
                TB_MAPACCELERATOR = 1114;
                TB_GETBUTTONINFO = 1087;
                TB_SETBUTTONINFO = 1088;
                TB_INSERTBUTTON = 1091;
                TB_ADDBUTTONS = 1092;
                TBN_GETBUTTONINFO = -720;
                TBN_GETINFOTIP = -719;
                TBN_GETDISPINFO = -717;
                TTM_ADDTOOL = 1074;
                TTM_SETTITLE = 1057;
                TTM_DELTOOL = 1075;
                TTM_NEWTOOLRECT = 1076;
                TTM_GETTOOLINFO = 1077;
                TTM_SETTOOLINFO = 1078;
                TTM_HITTEST = 1079;
                TTM_GETTEXT = 1080;
                TTM_UPDATETIPTEXT = 1081;
                TTM_ENUMTOOLS = 1082;
                TTM_GETCURRENTTOOL = 1083;
                TTN_GETDISPINFO = -530;
                TTN_NEEDTEXT = -530;
                TVM_INSERTITEM = 4402;
                TVM_GETITEM = 4414;
                TVM_SETITEM = 4415;
                TVM_EDITLABEL = 4417;
                TVM_GETISEARCHSTRING = 4416;
                TVN_SELCHANGING = -450;
                TVN_SELCHANGED = -451;
                TVN_GETDISPINFO = -452;
                TVN_SETDISPINFO = -453;
                TVN_ITEMEXPANDING = -454;
                TVN_ITEMEXPANDED = -455;
                TVN_BEGINDRAG = -456;
                TVN_BEGINRDRAG = -457;
                TVN_BEGINLABELEDIT = -459;
                TVN_ENDLABELEDIT = -460;
                TCM_GETITEM = 4924;
                TCM_SETITEM = 4925;
                TCM_INSERTITEM = 4926;
            }
        }

        internal static string GetLocalPath(string fileName)
        {
            Uri uri = new Uri(fileName);
            return uri.LocalPath + uri.Fragment;
        }
    }
}
