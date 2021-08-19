// System.Windows.Forms.UnsafeNativeMethods
using System;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Automation;
using Accessibility;

namespace System.Windows.Forms
{
    [SuppressUnmanagedCodeSecurity]
    internal static class UnsafeNativeMethods
    {
        internal struct POINTSTRUCT
        {
            public int x;

            public int y;

            public POINTSTRUCT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [ComImport]
        [Guid("00000122-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleDropTarget
        {
            [PreserveSig]
            int OleDragEnter([In][MarshalAs(UnmanagedType.Interface)] object pDataObj, [In][MarshalAs(UnmanagedType.U4)] int grfKeyState, [In] POINTSTRUCT pt, [In][Out] ref int pdwEffect);

            [PreserveSig]
            int OleDragOver([In][MarshalAs(UnmanagedType.U4)] int grfKeyState, [In] POINTSTRUCT pt, [In][Out] ref int pdwEffect);

            [PreserveSig]
            int OleDragLeave();

            [PreserveSig]
            int OleDrop([In][MarshalAs(UnmanagedType.Interface)] object pDataObj, [In][MarshalAs(UnmanagedType.U4)] int grfKeyState, [In] POINTSTRUCT pt, [In][Out] ref int pdwEffect);
        }

        [ComImport]
        [Guid("00000121-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleDropSource
        {
            [PreserveSig]
            int OleQueryContinueDrag(int fEscapePressed, [In][MarshalAs(UnmanagedType.U4)] int grfKeyState);

            [PreserveSig]
            int OleGiveFeedback([In][MarshalAs(UnmanagedType.U4)] int dwEffect);
        }

        [ComImport]
        [Guid("00000016-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleMessageFilter
        {
            [PreserveSig]
            int HandleInComingCall(int dwCallType, IntPtr hTaskCaller, int dwTickCount, IntPtr lpInterfaceInfo);

            [PreserveSig]
            int RetryRejectedCall(IntPtr hTaskCallee, int dwTickCount, int dwRejectType);

            [PreserveSig]
            int MessagePending(IntPtr hTaskCallee, int dwTickCount, int dwPendingType);
        }

        [ComImport]
        [Guid("B196B289-BAB4-101A-B69C-00AA00341D07")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleControlSite
        {
            [PreserveSig]
            int OnControlInfoChanged();

            [PreserveSig]
            int LockInPlaceActive(int fLock);

            [PreserveSig]
            int GetExtendedControl([MarshalAs(UnmanagedType.IDispatch)] out object ppDisp);

            [PreserveSig]
            int TransformCoords([In][Out] NativeMethods._POINTL pPtlHimetric, [In][Out] NativeMethods.tagPOINTF pPtfContainer, [In][MarshalAs(UnmanagedType.U4)] int dwFlags);

            [PreserveSig]
            int TranslateAccelerator([In] ref NativeMethods.MSG pMsg, [In][MarshalAs(UnmanagedType.U4)] int grfModifiers);

            [PreserveSig]
            int OnFocus(int fGotFocus);

            [PreserveSig]
            int ShowPropertyFrame();
        }

        [ComImport]
        [Guid("00000118-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleClientSite
        {
            [PreserveSig]
            int SaveObject();

            [PreserveSig]
            int GetMoniker([In][MarshalAs(UnmanagedType.U4)] int dwAssign, [In][MarshalAs(UnmanagedType.U4)] int dwWhichMoniker, [MarshalAs(UnmanagedType.Interface)] out object moniker);

            [PreserveSig]
            int GetContainer(out IOleContainer container);

            [PreserveSig]
            int ShowObject();

            [PreserveSig]
            int OnShowWindow(int fShow);

            [PreserveSig]
            int RequestNewObjectLayout();
        }

        [ComImport]
        [Guid("00000119-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleInPlaceSite
        {
            IntPtr GetWindow();

            [PreserveSig]
            int ContextSensitiveHelp(int fEnterMode);

            [PreserveSig]
            int CanInPlaceActivate();

            [PreserveSig]
            int OnInPlaceActivate();

            [PreserveSig]
            int OnUIActivate();

            [PreserveSig]
            int GetWindowContext([MarshalAs(UnmanagedType.Interface)] out IOleInPlaceFrame ppFrame, [MarshalAs(UnmanagedType.Interface)] out IOleInPlaceUIWindow ppDoc, [Out] NativeMethods.COMRECT lprcPosRect, [Out] NativeMethods.COMRECT lprcClipRect, [In][Out] NativeMethods.tagOIFI lpFrameInfo);

            [PreserveSig]
            int Scroll(NativeMethods.tagSIZE scrollExtant);

            [PreserveSig]
            int OnUIDeactivate(int fUndoable);

            [PreserveSig]
            int OnInPlaceDeactivate();

            [PreserveSig]
            int DiscardUndoState();

            [PreserveSig]
            int DeactivateAndUndo();

            [PreserveSig]
            int OnPosRectChange([In] NativeMethods.COMRECT lprcPosRect);
        }

        [ComImport]
        [Guid("742B0E01-14E6-101B-914E-00AA00300CAB")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface ISimpleFrameSite
        {
            [PreserveSig]
            int PreMessageFilter(IntPtr hwnd, [In][MarshalAs(UnmanagedType.U4)] int msg, IntPtr wp, IntPtr lp, [In][Out] ref IntPtr plResult, [In][Out][MarshalAs(UnmanagedType.U4)] ref int pdwCookie);

            [PreserveSig]
            int PostMessageFilter(IntPtr hwnd, [In][MarshalAs(UnmanagedType.U4)] int msg, IntPtr wp, IntPtr lp, [In][Out] ref IntPtr plResult, [In][MarshalAs(UnmanagedType.U4)] int dwCookie);
        }

        [ComImport]
        [Guid("40A050A0-3C31-101B-A82E-08002B2B2337")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IVBGetControl
        {
            [PreserveSig]
            int EnumControls(int dwOleContF, int dwWhich, out IEnumUnknown ppenum);
        }

        [ComImport]
        [Guid("91733A60-3F4C-101B-A3F6-00AA0034E4E9")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IGetVBAObject
        {
            [PreserveSig]
            int GetObject([In] ref Guid riid, [Out][MarshalAs(UnmanagedType.LPArray)] IVBFormat[] rval, int dwReserved);
        }

        [ComImport]
        [Guid("9BFBBC02-EFF1-101A-84ED-00AA00341D07")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IPropertyNotifySink
        {
            void OnChanged(int dispID);

            [PreserveSig]
            int OnRequestEdit(int dispID);
        }

        [ComImport]
        [Guid("9849FD60-3768-101B-8D72-AE6164FFE3CF")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IVBFormat
        {
            [PreserveSig]
            int Format([In] ref object var, IntPtr pszFormat, IntPtr lpBuffer, short cpBuffer, int lcid, short firstD, short firstW, [Out][MarshalAs(UnmanagedType.LPArray)] short[] result);
        }

        [ComImport]
        [Guid("00000100-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IEnumUnknown
        {
            [PreserveSig]
            int Next([In][MarshalAs(UnmanagedType.U4)] int celt, [Out] IntPtr rgelt, IntPtr pceltFetched);

            [PreserveSig]
            int Skip([In][MarshalAs(UnmanagedType.U4)] int celt);

            void Reset();

            void Clone(out IEnumUnknown ppenum);
        }

        [ComImport]
        [Guid("0000011B-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleContainer
        {
            [PreserveSig]
            int ParseDisplayName([In][MarshalAs(UnmanagedType.Interface)] object pbc, [In][MarshalAs(UnmanagedType.BStr)] string pszDisplayName, [Out][MarshalAs(UnmanagedType.LPArray)] int[] pchEaten, [Out][MarshalAs(UnmanagedType.LPArray)] object[] ppmkOut);

            [PreserveSig]
            int EnumObjects([In][MarshalAs(UnmanagedType.U4)] int grfFlags, out IEnumUnknown ppenum);

            [PreserveSig]
            int LockContainer(bool fLock);
        }

        [ComImport]
        [Guid("00000116-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleInPlaceFrame
        {
            IntPtr GetWindow();

            [PreserveSig]
            int ContextSensitiveHelp(int fEnterMode);

            [PreserveSig]
            int GetBorder([Out] NativeMethods.COMRECT lprectBorder);

            [PreserveSig]
            int RequestBorderSpace([In] NativeMethods.COMRECT pborderwidths);

            [PreserveSig]
            int SetBorderSpace([In] NativeMethods.COMRECT pborderwidths);

            [PreserveSig]
            int SetActiveObject([In][MarshalAs(UnmanagedType.Interface)] IOleInPlaceActiveObject pActiveObject, [In][MarshalAs(UnmanagedType.LPWStr)] string pszObjName);

            [PreserveSig]
            int InsertMenus([In] IntPtr hmenuShared, [In][Out] NativeMethods.tagOleMenuGroupWidths lpMenuWidths);

            [PreserveSig]
            int SetMenu([In] IntPtr hmenuShared, [In] IntPtr holemenu, [In] IntPtr hwndActiveObject);

            [PreserveSig]
            int RemoveMenus([In] IntPtr hmenuShared);

            [PreserveSig]
            int SetStatusText([In][MarshalAs(UnmanagedType.LPWStr)] string pszStatusText);

            [PreserveSig]
            int EnableModeless(bool fEnable);

            [PreserveSig]
            int TranslateAccelerator([In] ref NativeMethods.MSG lpmsg, [In][MarshalAs(UnmanagedType.U2)] short wID);
        }

        [ComImport]
        [ComVisible(true)]
        [Guid("BD3F23C0-D43E-11CF-893B-00AA00BDCE1A")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IDocHostUIHandler
        {
            [PreserveSig]
            [return: MarshalAs(UnmanagedType.I4)]
            int ShowContextMenu([In][MarshalAs(UnmanagedType.U4)] int dwID, [In] NativeMethods.POINT pt, [In][MarshalAs(UnmanagedType.Interface)] object pcmdtReserved, [In][MarshalAs(UnmanagedType.Interface)] object pdispReserved);

            [PreserveSig]
            [return: MarshalAs(UnmanagedType.I4)]
            int GetHostInfo([In][Out] NativeMethods.DOCHOSTUIINFO info);

            [PreserveSig]
            [return: MarshalAs(UnmanagedType.I4)]
            int ShowUI([In][MarshalAs(UnmanagedType.I4)] int dwID, [In] IOleInPlaceActiveObject activeObject, [In] NativeMethods.IOleCommandTarget commandTarget, [In] IOleInPlaceFrame frame, [In] IOleInPlaceUIWindow doc);

            [PreserveSig]
            [return: MarshalAs(UnmanagedType.I4)]
            int HideUI();

            [PreserveSig]
            [return: MarshalAs(UnmanagedType.I4)]
            int UpdateUI();

            [PreserveSig]
            [return: MarshalAs(UnmanagedType.I4)]
            int EnableModeless([In][MarshalAs(UnmanagedType.Bool)] bool fEnable);

            [PreserveSig]
            [return: MarshalAs(UnmanagedType.I4)]
            int OnDocWindowActivate([In][MarshalAs(UnmanagedType.Bool)] bool fActivate);

            [PreserveSig]
            [return: MarshalAs(UnmanagedType.I4)]
            int OnFrameWindowActivate([In][MarshalAs(UnmanagedType.Bool)] bool fActivate);

            [PreserveSig]
            [return: MarshalAs(UnmanagedType.I4)]
            int ResizeBorder([In] NativeMethods.COMRECT rect, [In] IOleInPlaceUIWindow doc, bool fFrameWindow);

            [PreserveSig]
            [return: MarshalAs(UnmanagedType.I4)]
            int TranslateAccelerator([In] ref NativeMethods.MSG msg, [In] ref Guid group, [In][MarshalAs(UnmanagedType.I4)] int nCmdID);

            [PreserveSig]
            [return: MarshalAs(UnmanagedType.I4)]
            int GetOptionKeyPath([Out][MarshalAs(UnmanagedType.LPArray)] string[] pbstrKey, [In][MarshalAs(UnmanagedType.U4)] int dw);

            [PreserveSig]
            [return: MarshalAs(UnmanagedType.I4)]
            int GetDropTarget([In][MarshalAs(UnmanagedType.Interface)] IOleDropTarget pDropTarget, [MarshalAs(UnmanagedType.Interface)] out IOleDropTarget ppDropTarget);

            [PreserveSig]
            [return: MarshalAs(UnmanagedType.I4)]
            int GetExternal([MarshalAs(UnmanagedType.Interface)] out object ppDispatch);

            [PreserveSig]
            [return: MarshalAs(UnmanagedType.I4)]
            int TranslateUrl([In][MarshalAs(UnmanagedType.U4)] int dwTranslate, [In][MarshalAs(UnmanagedType.LPWStr)] string strURLIn, [MarshalAs(UnmanagedType.LPWStr)] out string pstrURLOut);

            [PreserveSig]
            [return: MarshalAs(UnmanagedType.I4)]
            int FilterDataObject(System.Runtime.InteropServices.ComTypes.IDataObject pDO, out System.Runtime.InteropServices.ComTypes.IDataObject ppDORet);
        }

        [ComImport]
        [SuppressUnmanagedCodeSecurity]
        [Guid("D30C1661-CDAF-11d0-8A3E-00C04FC9E26E")]
        [TypeLibType(TypeLibTypeFlags.FHidden | TypeLibTypeFlags.FDual | TypeLibTypeFlags.FOleAutomation)]
        public interface IWebBrowser2
        {
            [DispId(200)]
            object Application
            {
                [return: MarshalAs(UnmanagedType.IDispatch)]
                get;
            }

            [DispId(201)]
            object Parent
            {
                [return: MarshalAs(UnmanagedType.IDispatch)]
                get;
            }

            [DispId(202)]
            object Container
            {
                [return: MarshalAs(UnmanagedType.IDispatch)]
                get;
            }

            [DispId(203)]
            object Document
            {
                [return: MarshalAs(UnmanagedType.IDispatch)]
                get;
            }

            [DispId(204)]
            bool TopLevelContainer { get; }

            [DispId(205)]
            string Type { get; }

            [DispId(206)]
            int Left { get; set; }

            [DispId(207)]
            int Top { get; set; }

            [DispId(208)]
            int Width { get; set; }

            [DispId(209)]
            int Height { get; set; }

            [DispId(210)]
            string LocationName { get; }

            [DispId(211)]
            string LocationURL { get; }

            [DispId(212)]
            bool Busy { get; }

            [DispId(0)]
            string Name { get; }

            [DispId(-515)]
            int HWND { get; }

            [DispId(400)]
            string FullName { get; }

            [DispId(401)]
            string Path { get; }

            [DispId(402)]
            bool Visible { get; set; }

            [DispId(403)]
            bool StatusBar { get; set; }

            [DispId(404)]
            string StatusText { get; set; }

            [DispId(405)]
            int ToolBar { get; set; }

            [DispId(406)]
            bool MenuBar { get; set; }

            [DispId(407)]
            bool FullScreen { get; set; }

            [DispId(-525)]
            WebBrowserReadyState ReadyState { get; }

            [DispId(550)]
            bool Offline { get; set; }

            [DispId(551)]
            bool Silent { get; set; }

            [DispId(552)]
            bool RegisterAsBrowser { get; set; }

            [DispId(553)]
            bool RegisterAsDropTarget { get; set; }

            [DispId(554)]
            bool TheaterMode { get; set; }

            [DispId(555)]
            bool AddressBar { get; set; }

            [DispId(556)]
            bool Resizable { get; set; }

            [DispId(100)]
            void GoBack();

            [DispId(101)]
            void GoForward();

            [DispId(102)]
            void GoHome();

            [DispId(103)]
            void GoSearch();

            [DispId(104)]
            void Navigate([In] string Url, [In] ref object flags, [In] ref object targetFrameName, [In] ref object postData, [In] ref object headers);

            [DispId(-550)]
            void Refresh();

            [DispId(105)]
            void Refresh2([In] ref object level);

            [DispId(106)]
            void Stop();

            [DispId(300)]
            void Quit();

            [DispId(301)]
            void ClientToWindow(out int pcx, out int pcy);

            [DispId(302)]
            void PutProperty([In] string property, [In] object vtValue);

            [DispId(303)]
            object GetProperty([In] string property);

            [DispId(500)]
            void Navigate2([In] ref object URL, [In] ref object flags, [In] ref object targetFrameName, [In] ref object postData, [In] ref object headers);

            [DispId(501)]
            NativeMethods.OLECMDF QueryStatusWB([In] NativeMethods.OLECMDID cmdID);

            [DispId(502)]
            void ExecWB([In] NativeMethods.OLECMDID cmdID, [In] NativeMethods.OLECMDEXECOPT cmdexecopt, ref object pvaIn, IntPtr pvaOut);

            [DispId(503)]
            void ShowBrowserBar([In] ref object pvaClsid, [In] ref object pvarShow, [In] ref object pvarSize);
        }

        [ComImport]
        [Guid("34A715A0-6587-11D0-924A-0020AFC7AC4D")]
        [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        [TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DWebBrowserEvents2
        {
            [DispId(102)]
            void StatusTextChange([In] string text);

            [DispId(108)]
            void ProgressChange([In] int progress, [In] int progressMax);

            [DispId(105)]
            void CommandStateChange([In] long command, [In] bool enable);

            [DispId(106)]
            void DownloadBegin();

            [DispId(104)]
            void DownloadComplete();

            [DispId(113)]
            void TitleChange([In] string text);

            [DispId(112)]
            void PropertyChange([In] string szProperty);

            [DispId(250)]
            void BeforeNavigate2([In][MarshalAs(UnmanagedType.IDispatch)] object pDisp, [In] ref object URL, [In] ref object flags, [In] ref object targetFrameName, [In] ref object postData, [In] ref object headers, [In][Out] ref bool cancel);

            [DispId(251)]
            void NewWindow2([In][Out][MarshalAs(UnmanagedType.IDispatch)] ref object pDisp, [In][Out] ref bool cancel);

            [DispId(252)]
            void NavigateComplete2([In][MarshalAs(UnmanagedType.IDispatch)] object pDisp, [In] ref object URL);

            [DispId(259)]
            void DocumentComplete([In][MarshalAs(UnmanagedType.IDispatch)] object pDisp, [In] ref object URL);

            [DispId(253)]
            void OnQuit();

            [DispId(254)]
            void OnVisible([In] bool visible);

            [DispId(255)]
            void OnToolBar([In] bool toolBar);

            [DispId(256)]
            void OnMenuBar([In] bool menuBar);

            [DispId(257)]
            void OnStatusBar([In] bool statusBar);

            [DispId(258)]
            void OnFullScreen([In] bool fullScreen);

            [DispId(260)]
            void OnTheaterMode([In] bool theaterMode);

            [DispId(262)]
            void WindowSetResizable([In] bool resizable);

            [DispId(264)]
            void WindowSetLeft([In] int left);

            [DispId(265)]
            void WindowSetTop([In] int top);

            [DispId(266)]
            void WindowSetWidth([In] int width);

            [DispId(267)]
            void WindowSetHeight([In] int height);

            [DispId(263)]
            void WindowClosing([In] bool isChildWindow, [In][Out] ref bool cancel);

            [DispId(268)]
            void ClientToHostWindow([In][Out] ref long cx, [In][Out] ref long cy);

            [DispId(269)]
            void SetSecureLockIcon([In] int secureLockIcon);

            [DispId(270)]
            void FileDownload([In][Out] ref bool cancel);

            [DispId(271)]
            void NavigateError([In][MarshalAs(UnmanagedType.IDispatch)] object pDisp, [In] ref object URL, [In] ref object frame, [In] ref object statusCode, [In][Out] ref bool cancel);

            [DispId(225)]
            void PrintTemplateInstantiation([In][MarshalAs(UnmanagedType.IDispatch)] object pDisp);

            [DispId(226)]
            void PrintTemplateTeardown([In][MarshalAs(UnmanagedType.IDispatch)] object pDisp);

            [DispId(227)]
            void UpdatePageStatus([In][MarshalAs(UnmanagedType.IDispatch)] object pDisp, [In] ref object nPage, [In] ref object fDone);

            [DispId(272)]
            void PrivacyImpactedStateChange([In] bool bImpacted);
        }

        [SuppressUnmanagedCodeSecurity]
        [ComVisible(true)]
        [Guid("626FC520-A41E-11cf-A731-00A0C9082637")]
        [InterfaceType(ComInterfaceType.InterfaceIsDual)]
        internal interface IHTMLDocument
        {
            [return: MarshalAs(UnmanagedType.IDispatch)]
            object GetScript();
        }

        [SuppressUnmanagedCodeSecurity]
        [ComVisible(true)]
        [Guid("332C4425-26CB-11D0-B483-00C04FD90119")]
        [InterfaceType(ComInterfaceType.InterfaceIsDual)]
        internal interface IHTMLDocument2
        {
            [return: MarshalAs(UnmanagedType.IDispatch)]
            object GetScript();

            IHTMLElementCollection GetAll();

            IHTMLElement GetBody();

            IHTMLElement GetActiveElement();

            IHTMLElementCollection GetImages();

            IHTMLElementCollection GetApplets();

            IHTMLElementCollection GetLinks();

            IHTMLElementCollection GetForms();

            IHTMLElementCollection GetAnchors();

            void SetTitle(string p);

            string GetTitle();

            IHTMLElementCollection GetScripts();

            void SetDesignMode(string p);

            string GetDesignMode();

            [return: MarshalAs(UnmanagedType.Interface)]
            object GetSelection();

            string GetReadyState();

            [return: MarshalAs(UnmanagedType.Interface)]
            object GetFrames();

            IHTMLElementCollection GetEmbeds();

            IHTMLElementCollection GetPlugins();

            void SetAlinkColor(object c);

            object GetAlinkColor();

            void SetBgColor(object c);

            object GetBgColor();

            void SetFgColor(object c);

            object GetFgColor();

            void SetLinkColor(object c);

            object GetLinkColor();

            void SetVlinkColor(object c);

            object GetVlinkColor();

            string GetReferrer();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLLocation GetLocation();

            string GetLastModified();

            void SetUrl(string p);

            string GetUrl();

            void SetDomain(string p);

            string GetDomain();

            void SetCookie(string p);

            string GetCookie();

            void SetExpando(bool p);

            bool GetExpando();

            void SetCharset(string p);

            string GetCharset();

            void SetDefaultCharset(string p);

            string GetDefaultCharset();

            string GetMimeType();

            string GetFileSize();

            string GetFileCreatedDate();

            string GetFileModifiedDate();

            string GetFileUpdatedDate();

            string GetSecurity();

            string GetProtocol();

            string GetNameProp();

            int Write([In][MarshalAs(UnmanagedType.SafeArray)] object[] psarray);

            int WriteLine([In][MarshalAs(UnmanagedType.SafeArray)] object[] psarray);

            [return: MarshalAs(UnmanagedType.Interface)]
            object Open(string mimeExtension, object name, object features, object replace);

            void Close();

            void Clear();

            bool QueryCommandSupported(string cmdID);

            bool QueryCommandEnabled(string cmdID);

            bool QueryCommandState(string cmdID);

            bool QueryCommandIndeterm(string cmdID);

            string QueryCommandText(string cmdID);

            object QueryCommandValue(string cmdID);

            bool ExecCommand(string cmdID, bool showUI, object value);

            bool ExecCommandShowHelp(string cmdID);

            IHTMLElement CreateElement(string eTag);

            void SetOnhelp(object p);

            object GetOnhelp();

            void SetOnclick(object p);

            object GetOnclick();

            void SetOndblclick(object p);

            object GetOndblclick();

            void SetOnkeyup(object p);

            object GetOnkeyup();

            void SetOnkeydown(object p);

            object GetOnkeydown();

            void SetOnkeypress(object p);

            object GetOnkeypress();

            void SetOnmouseup(object p);

            object GetOnmouseup();

            void SetOnmousedown(object p);

            object GetOnmousedown();

            void SetOnmousemove(object p);

            object GetOnmousemove();

            void SetOnmouseout(object p);

            object GetOnmouseout();

            void SetOnmouseover(object p);

            object GetOnmouseover();

            void SetOnreadystatechange(object p);

            object GetOnreadystatechange();

            void SetOnafterupdate(object p);

            object GetOnafterupdate();

            void SetOnrowexit(object p);

            object GetOnrowexit();

            void SetOnrowenter(object p);

            object GetOnrowenter();

            void SetOndragstart(object p);

            object GetOndragstart();

            void SetOnselectstart(object p);

            object GetOnselectstart();

            IHTMLElement ElementFromPoint(int x, int y);

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLWindow2 GetParentWindow();

            [return: MarshalAs(UnmanagedType.Interface)]
            object GetStyleSheets();

            void SetOnbeforeupdate(object p);

            object GetOnbeforeupdate();

            void SetOnerrorupdate(object p);

            object GetOnerrorupdate();

            string toString();

            [return: MarshalAs(UnmanagedType.Interface)]
            object CreateStyleSheet(string bstrHref, int lIndex);
        }

        [SuppressUnmanagedCodeSecurity]
        [ComVisible(true)]
        [Guid("3050F485-98B5-11CF-BB82-00AA00BDCE0B")]
        [InterfaceType(ComInterfaceType.InterfaceIsDual)]
        internal interface IHTMLDocument3
        {
            void ReleaseCapture();

            void Recalc([In] bool fForce);

            object CreateTextNode([In] string text);

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElement GetDocumentElement();

            string GetUniqueID();

            bool AttachEvent([In] string ev, [In][MarshalAs(UnmanagedType.IDispatch)] object pdisp);

            void DetachEvent([In] string ev, [In][MarshalAs(UnmanagedType.IDispatch)] object pdisp);

            void SetOnrowsdelete([In] object p);

            object GetOnrowsdelete();

            void SetOnrowsinserted([In] object p);

            object GetOnrowsinserted();

            void SetOncellchange([In] object p);

            object GetOncellchange();

            void SetOndatasetchanged([In] object p);

            object GetOndatasetchanged();

            void SetOndataavailable([In] object p);

            object GetOndataavailable();

            void SetOndatasetcomplete([In] object p);

            object GetOndatasetcomplete();

            void SetOnpropertychange([In] object p);

            object GetOnpropertychange();

            void SetDir([In] string p);

            string GetDir();

            void SetOncontextmenu([In] object p);

            object GetOncontextmenu();

            void SetOnstop([In] object p);

            object GetOnstop();

            object CreateDocumentFragment();

            object GetParentDocument();

            void SetEnableDownload([In] bool p);

            bool GetEnableDownload();

            void SetBaseUrl([In] string p);

            string GetBaseUrl();

            [return: MarshalAs(UnmanagedType.IDispatch)]
            object GetChildNodes();

            void SetInheritStyleSheets([In] bool p);

            bool GetInheritStyleSheets();

            void SetOnbeforeeditfocus([In] object p);

            object GetOnbeforeeditfocus();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElementCollection GetElementsByName([In] string v);

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElement GetElementById([In] string v);

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElementCollection GetElementsByTagName([In] string v);
        }

        [SuppressUnmanagedCodeSecurity]
        [ComVisible(true)]
        [Guid("3050F69A-98B5-11CF-BB82-00AA00BDCE0B")]
        [InterfaceType(ComInterfaceType.InterfaceIsDual)]
        internal interface IHTMLDocument4
        {
            void Focus();

            bool HasFocus();

            void SetOnselectionchange(object p);

            object GetOnselectionchange();

            object GetNamespaces();

            object createDocumentFromUrl(string bstrUrl, string bstrOptions);

            void SetMedia(string bstrMedia);

            string GetMedia();

            object CreateEventObject([Optional][In] ref object eventObject);

            bool FireEvent(string eventName);

            object CreateRenderStyle(string bstr);

            void SetOncontrolselect(object p);

            object GetOncontrolselect();

            string GetURLUnencoded();
        }

        [ComImport]
        [Guid("3050f613-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        [TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLDocumentEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(IHTMLEventObj evtObj);

            [DispId(-600)]
            bool onclick(IHTMLEventObj evtObj);

            [DispId(-601)]
            bool ondblclick(IHTMLEventObj evtObj);

            [DispId(-602)]
            void onkeydown(IHTMLEventObj evtObj);

            [DispId(-604)]
            void onkeyup(IHTMLEventObj evtObj);

            [DispId(-603)]
            bool onkeypress(IHTMLEventObj evtObj);

            [DispId(-605)]
            void onmousedown(IHTMLEventObj evtObj);

            [DispId(-606)]
            void onmousemove(IHTMLEventObj evtObj);

            [DispId(-607)]
            void onmouseup(IHTMLEventObj evtObj);

            [DispId(-2147418103)]
            void onmouseout(IHTMLEventObj evtObj);

            [DispId(-2147418104)]
            void onmouseover(IHTMLEventObj evtObj);

            [DispId(-609)]
            void onreadystatechange(IHTMLEventObj evtObj);

            [DispId(-2147418108)]
            bool onbeforeupdate(IHTMLEventObj evtObj);

            [DispId(-2147418107)]
            void onafterupdate(IHTMLEventObj evtObj);

            [DispId(-2147418106)]
            bool onrowexit(IHTMLEventObj evtObj);

            [DispId(-2147418105)]
            void onrowenter(IHTMLEventObj evtObj);

            [DispId(-2147418101)]
            bool ondragstart(IHTMLEventObj evtObj);

            [DispId(-2147418100)]
            bool onselectstart(IHTMLEventObj evtObj);

            [DispId(-2147418099)]
            bool onerrorupdate(IHTMLEventObj evtObj);

            [DispId(1023)]
            bool oncontextmenu(IHTMLEventObj evtObj);

            [DispId(1026)]
            bool onstop(IHTMLEventObj evtObj);

            [DispId(-2147418080)]
            void onrowsdelete(IHTMLEventObj evtObj);

            [DispId(-2147418079)]
            void onrowsinserted(IHTMLEventObj evtObj);

            [DispId(-2147418078)]
            void oncellchange(IHTMLEventObj evtObj);

            [DispId(-2147418093)]
            void onpropertychange(IHTMLEventObj evtObj);

            [DispId(-2147418098)]
            void ondatasetchanged(IHTMLEventObj evtObj);

            [DispId(-2147418097)]
            void ondataavailable(IHTMLEventObj evtObj);

            [DispId(-2147418096)]
            void ondatasetcomplete(IHTMLEventObj evtObj);

            [DispId(1027)]
            void onbeforeeditfocus(IHTMLEventObj evtObj);

            [DispId(1037)]
            void onselectionchange(IHTMLEventObj evtObj);

            [DispId(1036)]
            bool oncontrolselect(IHTMLEventObj evtObj);

            [DispId(1033)]
            bool onmousewheel(IHTMLEventObj evtObj);

            [DispId(1048)]
            void onfocusin(IHTMLEventObj evtObj);

            [DispId(1049)]
            void onfocusout(IHTMLEventObj evtObj);

            [DispId(1044)]
            void onactivate(IHTMLEventObj evtObj);

            [DispId(1045)]
            void ondeactivate(IHTMLEventObj evtObj);

            [DispId(1047)]
            bool onbeforeactivate(IHTMLEventObj evtObj);

            [DispId(1034)]
            bool onbeforedeactivate(IHTMLEventObj evtObj);
        }

        [SuppressUnmanagedCodeSecurity]
        [ComVisible(true)]
        [Guid("332C4426-26CB-11D0-B483-00C04FD90119")]
        [InterfaceType(ComInterfaceType.InterfaceIsDual)]
        internal interface IHTMLFramesCollection2
        {
            object Item(ref object idOrName);

            int GetLength();
        }

        [SuppressUnmanagedCodeSecurity]
        [ComVisible(true)]
        [Guid("332C4427-26CB-11D0-B483-00C04FD90119")]
        [InterfaceType(ComInterfaceType.InterfaceIsDual)]
        public interface IHTMLWindow2
        {
            [return: MarshalAs(UnmanagedType.IDispatch)]
            object Item([In] ref object pvarIndex);

            int GetLength();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLFramesCollection2 GetFrames();

            void SetDefaultStatus([In] string p);

            string GetDefaultStatus();

            void SetStatus([In] string p);

            string GetStatus();

            int SetTimeout([In] string expression, [In] int msec, [In] ref object language);

            void ClearTimeout([In] int timerID);

            void Alert([In] string message);

            bool Confirm([In] string message);

            [return: MarshalAs(UnmanagedType.Struct)]
            object Prompt([In] string message, [In] string defstr);

            object GetImage();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLLocation GetLocation();

            [return: MarshalAs(UnmanagedType.Interface)]
            IOmHistory GetHistory();

            void Close();

            void SetOpener([In] object p);

            [return: MarshalAs(UnmanagedType.IDispatch)]
            object GetOpener();

            [return: MarshalAs(UnmanagedType.Interface)]
            IOmNavigator GetNavigator();

            void SetName([In] string p);

            string GetName();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLWindow2 GetParent();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLWindow2 Open([In] string URL, [In] string name, [In] string features, [In] bool replace);

            object GetSelf();

            object GetTop();

            object GetWindow();

            void Navigate([In] string URL);

            void SetOnfocus([In] object p);

            object GetOnfocus();

            void SetOnblur([In] object p);

            object GetOnblur();

            void SetOnload([In] object p);

            object GetOnload();

            void SetOnbeforeunload(object p);

            object GetOnbeforeunload();

            void SetOnunload([In] object p);

            object GetOnunload();

            void SetOnhelp(object p);

            object GetOnhelp();

            void SetOnerror([In] object p);

            object GetOnerror();

            void SetOnresize([In] object p);

            object GetOnresize();

            void SetOnscroll([In] object p);

            object GetOnscroll();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLDocument2 GetDocument();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLEventObj GetEvent();

            object Get_newEnum();

            object ShowModalDialog([In] string dialog, [In] ref object varArgIn, [In] ref object varOptions);

            void ShowHelp([In] string helpURL, [In] object helpArg, [In] string features);

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLScreen GetScreen();

            object GetOption();

            void Focus();

            bool GetClosed();

            void Blur();

            void Scroll([In] int x, [In] int y);

            object GetClientInformation();

            int SetInterval([In] string expression, [In] int msec, [In] ref object language);

            void ClearInterval([In] int timerID);

            void SetOffscreenBuffering([In] object p);

            object GetOffscreenBuffering();

            [return: MarshalAs(UnmanagedType.Struct)]
            object ExecScript([In] string code, [In] string language);

            string toString();

            void ScrollBy([In] int x, [In] int y);

            void ScrollTo([In] int x, [In] int y);

            void MoveTo([In] int x, [In] int y);

            void MoveBy([In] int x, [In] int y);

            void ResizeTo([In] int x, [In] int y);

            void ResizeBy([In] int x, [In] int y);

            object GetExternal();
        }

        [SuppressUnmanagedCodeSecurity]
        [ComVisible(true)]
        [Guid("3050f4ae-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsDual)]
        public interface IHTMLWindow3
        {
            int GetScreenLeft();

            int GetScreenTop();

            bool AttachEvent(string ev, [In][MarshalAs(UnmanagedType.IDispatch)] object pdisp);

            void DetachEvent(string ev, [In][MarshalAs(UnmanagedType.IDispatch)] object pdisp);

            int SetTimeout([In] ref object expression, int msec, [In] ref object language);

            int SetInterval([In] ref object expression, int msec, [In] ref object language);

            void Print();

            void SetBeforePrint(object o);

            object GetBeforePrint();

            void SetAfterPrint(object o);

            object GetAfterPrint();

            object GetClipboardData();

            object ShowModelessDialog(string url, object varArgIn, object options);
        }

        [SuppressUnmanagedCodeSecurity]
        [ComVisible(true)]
        [Guid("3050f6cf-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsDual)]
        public interface IHTMLWindow4
        {
            [return: MarshalAs(UnmanagedType.IDispatch)]
            object CreatePopup([In] ref object reserved);

            [return: MarshalAs(UnmanagedType.Interface)]
            object frameElement();
        }

        [ComImport]
        [Guid("3050f625-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        [TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLWindowEvents2
        {
            [DispId(1003)]
            void onload(IHTMLEventObj evtObj);

            [DispId(1008)]
            void onunload(IHTMLEventObj evtObj);

            [DispId(-2147418102)]
            bool onhelp(IHTMLEventObj evtObj);

            [DispId(-2147418111)]
            void onfocus(IHTMLEventObj evtObj);

            [DispId(-2147418112)]
            void onblur(IHTMLEventObj evtObj);

            [DispId(1002)]
            bool onerror(string description, string url, int line);

            [DispId(1016)]
            void onresize(IHTMLEventObj evtObj);

            [DispId(1014)]
            void onscroll(IHTMLEventObj evtObj);

            [DispId(1017)]
            void onbeforeunload(IHTMLEventObj evtObj);

            [DispId(1024)]
            void onbeforeprint(IHTMLEventObj evtObj);

            [DispId(1025)]
            void onafterprint(IHTMLEventObj evtObj);
        }

        [SuppressUnmanagedCodeSecurity]
        [ComVisible(true)]
        [Guid("3050f666-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsDual)]
        public interface IHTMLPopup
        {
            void show(int x, int y, int w, int h, ref object element);

            void hide();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLDocument GetDocument();

            bool IsOpen();
        }

        [SuppressUnmanagedCodeSecurity]
        [ComVisible(true)]
        [Guid("3050f35c-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsDual)]
        public interface IHTMLScreen
        {
            int GetColorDepth();

            void SetBufferDepth(int d);

            int GetBufferDepth();

            int GetWidth();

            int GetHeight();

            void SetUpdateInterval(int i);

            int GetUpdateInterval();

            int GetAvailHeight();

            int GetAvailWidth();

            bool GetFontSmoothingEnabled();
        }

        [SuppressUnmanagedCodeSecurity]
        [ComVisible(true)]
        [Guid("163BB1E0-6E00-11CF-837A-48DC04C10000")]
        [InterfaceType(ComInterfaceType.InterfaceIsDual)]
        internal interface IHTMLLocation
        {
            void SetHref([In] string p);

            string GetHref();

            void SetProtocol([In] string p);

            string GetProtocol();

            void SetHost([In] string p);

            string GetHost();

            void SetHostname([In] string p);

            string GetHostname();

            void SetPort([In] string p);

            string GetPort();

            void SetPathname([In] string p);

            string GetPathname();

            void SetSearch([In] string p);

            string GetSearch();

            void SetHash([In] string p);

            string GetHash();

            void Reload([In] bool flag);

            void Replace([In] string bstr);

            void Assign([In] string bstr);
        }

        [SuppressUnmanagedCodeSecurity]
        [ComVisible(true)]
        [Guid("FECEAAA2-8405-11CF-8BA1-00AA00476DA6")]
        [InterfaceType(ComInterfaceType.InterfaceIsDual)]
        internal interface IOmHistory
        {
            short GetLength();

            void Back();

            void Forward();

            void Go([In] ref object pvargdistance);
        }

        [SuppressUnmanagedCodeSecurity]
        [ComVisible(true)]
        [Guid("FECEAAA5-8405-11CF-8BA1-00AA00476DA6")]
        [InterfaceType(ComInterfaceType.InterfaceIsDual)]
        internal interface IOmNavigator
        {
            string GetAppCodeName();

            string GetAppName();

            string GetAppVersion();

            string GetUserAgent();

            bool JavaEnabled();

            bool TaintEnabled();

            object GetMimeTypes();

            object GetPlugins();

            bool GetCookieEnabled();

            object GetOpsProfile();

            string GetCpuClass();

            string GetSystemLanguage();

            string GetBrowserLanguage();

            string GetUserLanguage();

            string GetPlatform();

            string GetAppMinorVersion();

            int GetConnectionSpeed();

            bool GetOnLine();

            object GetUserProfile();
        }

        [SuppressUnmanagedCodeSecurity]
        [ComVisible(true)]
        [Guid("3050F32D-98B5-11CF-BB82-00AA00BDCE0B")]
        [InterfaceType(ComInterfaceType.InterfaceIsDual)]
        internal interface IHTMLEventObj
        {
            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElement GetSrcElement();

            bool GetAltKey();

            bool GetCtrlKey();

            bool GetShiftKey();

            void SetReturnValue(object p);

            object GetReturnValue();

            void SetCancelBubble(bool p);

            bool GetCancelBubble();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElement GetFromElement();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElement GetToElement();

            void SetKeyCode([In] int p);

            int GetKeyCode();

            int GetButton();

            string GetEventType();

            string GetQualifier();

            int GetReason();

            int GetX();

            int GetY();

            int GetClientX();

            int GetClientY();

            int GetOffsetX();

            int GetOffsetY();

            int GetScreenX();

            int GetScreenY();

            object GetSrcFilter();
        }

        [SuppressUnmanagedCodeSecurity]
        [ComVisible(true)]
        [Guid("3050f48B-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsDual)]
        internal interface IHTMLEventObj2
        {
            void SetAttribute(string attributeName, object attributeValue, int lFlags);

            object GetAttribute(string attributeName, int lFlags);

            bool RemoveAttribute(string attributeName, int lFlags);

            void SetPropertyName(string name);

            string GetPropertyName();

            void SetBookmarks(ref object bm);

            object GetBookmarks();

            void SetRecordset(ref object rs);

            object GetRecordset();

            void SetDataFld(string df);

            string GetDataFld();

            void SetBoundElements(ref object be);

            object GetBoundElements();

            void SetRepeat(bool r);

            bool GetRepeat();

            void SetSrcUrn(string urn);

            string GetSrcUrn();

            void SetSrcElement(ref object se);

            object GetSrcElement();

            void SetAltKey(bool alt);

            bool GetAltKey();

            void SetCtrlKey(bool ctrl);

            bool GetCtrlKey();

            void SetShiftKey(bool shift);

            bool GetShiftKey();

            void SetFromElement(ref object element);

            object GetFromElement();

            void SetToElement(ref object element);

            object GetToElement();

            void SetButton(int b);

            int GetButton();

            void SetType(string type);

            string GetType();

            void SetQualifier(string q);

            string GetQualifier();

            void SetReason(int r);

            int GetReason();

            void SetX(int x);

            int GetX();

            void SetY(int y);

            int GetY();

            void SetClientX(int x);

            int GetClientX();

            void SetClientY(int y);

            int GetClientY();

            void SetOffsetX(int x);

            int GetOffsetX();

            void SetOffsetY(int y);

            int GetOffsetY();

            void SetScreenX(int x);

            int GetScreenX();

            void SetScreenY(int y);

            int GetScreenY();

            void SetSrcFilter(ref object filter);

            object GetSrcFilter();

            object GetDataTransfer();
        }

        [SuppressUnmanagedCodeSecurity]
        [ComVisible(true)]
        [Guid("3050f814-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsDual)]
        internal interface IHTMLEventObj4
        {
            int GetWheelDelta();
        }

        [SuppressUnmanagedCodeSecurity]
        [ComVisible(true)]
        [Guid("3050F21F-98B5-11CF-BB82-00AA00BDCE0B")]
        [InterfaceType(ComInterfaceType.InterfaceIsDual)]
        internal interface IHTMLElementCollection
        {
            string toString();

            void SetLength(int p);

            int GetLength();

            [return: MarshalAs(UnmanagedType.Interface)]
            object Get_newEnum();

            [return: MarshalAs(UnmanagedType.IDispatch)]
            object Item(object idOrName, object index);

            [return: MarshalAs(UnmanagedType.Interface)]
            object Tags(object tagName);
        }

        [SuppressUnmanagedCodeSecurity]
        [ComVisible(true)]
        [Guid("3050F1FF-98B5-11CF-BB82-00AA00BDCE0B")]
        [InterfaceType(ComInterfaceType.InterfaceIsDual)]
        internal interface IHTMLElement
        {
            void SetAttribute(string attributeName, object attributeValue, int lFlags);

            object GetAttribute(string attributeName, int lFlags);

            bool RemoveAttribute(string strAttributeName, int lFlags);

            void SetClassName(string p);

            string GetClassName();

            void SetId(string p);

            string GetId();

            string GetTagName();

            IHTMLElement GetParentElement();

            IHTMLStyle GetStyle();

            void SetOnhelp(object p);

            object GetOnhelp();

            void SetOnclick(object p);

            object GetOnclick();

            void SetOndblclick(object p);

            object GetOndblclick();

            void SetOnkeydown(object p);

            object GetOnkeydown();

            void SetOnkeyup(object p);

            object GetOnkeyup();

            void SetOnkeypress(object p);

            object GetOnkeypress();

            void SetOnmouseout(object p);

            object GetOnmouseout();

            void SetOnmouseover(object p);

            object GetOnmouseover();

            void SetOnmousemove(object p);

            object GetOnmousemove();

            void SetOnmousedown(object p);

            object GetOnmousedown();

            void SetOnmouseup(object p);

            object GetOnmouseup();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLDocument2 GetDocument();

            void SetTitle(string p);

            string GetTitle();

            void SetLanguage(string p);

            string GetLanguage();

            void SetOnselectstart(object p);

            object GetOnselectstart();

            void ScrollIntoView(object varargStart);

            bool Contains(IHTMLElement pChild);

            int GetSourceIndex();

            object GetRecordNumber();

            void SetLang(string p);

            string GetLang();

            int GetOffsetLeft();

            int GetOffsetTop();

            int GetOffsetWidth();

            int GetOffsetHeight();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElement GetOffsetParent();

            void SetInnerHTML(string p);

            string GetInnerHTML();

            void SetInnerText(string p);

            string GetInnerText();

            void SetOuterHTML(string p);

            string GetOuterHTML();

            void SetOuterText(string p);

            string GetOuterText();

            void InsertAdjacentHTML(string where, string html);

            void InsertAdjacentText(string where, string text);

            IHTMLElement GetParentTextEdit();

            bool GetIsTextEdit();

            void Click();

            [return: MarshalAs(UnmanagedType.Interface)]
            object GetFilters();

            void SetOndragstart(object p);

            object GetOndragstart();

            string toString();

            void SetOnbeforeupdate(object p);

            object GetOnbeforeupdate();

            void SetOnafterupdate(object p);

            object GetOnafterupdate();

            void SetOnerrorupdate(object p);

            object GetOnerrorupdate();

            void SetOnrowexit(object p);

            object GetOnrowexit();

            void SetOnrowenter(object p);

            object GetOnrowenter();

            void SetOndatasetchanged(object p);

            object GetOndatasetchanged();

            void SetOndataavailable(object p);

            object GetOndataavailable();

            void SetOndatasetcomplete(object p);

            object GetOndatasetcomplete();

            void SetOnfilterchange(object p);

            object GetOnfilterchange();

            [return: MarshalAs(UnmanagedType.IDispatch)]
            object GetChildren();

            [return: MarshalAs(UnmanagedType.IDispatch)]
            object GetAll();
        }

        [SuppressUnmanagedCodeSecurity]
        [ComVisible(true)]
        [Guid("3050f434-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsDual)]
        internal interface IHTMLElement2
        {
            string ScopeName();

            void SetCapture(bool containerCapture);

            void ReleaseCapture();

            void SetOnLoseCapture(object v);

            object GetOnLoseCapture();

            string GetComponentFromPoint(int x, int y);

            void DoScroll(object component);

            void SetOnScroll(object v);

            object GetOnScroll();

            void SetOnDrag(object v);

            object GetOnDrag();

            void SetOnDragEnd(object v);

            object GetOnDragEnd();

            void SetOnDragEnter(object v);

            object GetOnDragEnter();

            void SetOnDragOver(object v);

            object GetOnDragOver();

            void SetOnDragleave(object v);

            object GetOnDragLeave();

            void SetOnDrop(object v);

            object GetOnDrop();

            void SetOnBeforeCut(object v);

            object GetOnBeforeCut();

            void SetOnCut(object v);

            object GetOnCut();

            void SetOnBeforeCopy(object v);

            object GetOnBeforeCopy();

            void SetOnCopy(object v);

            object GetOnCopy(object p);

            void SetOnBeforePaste(object v);

            object GetOnBeforePaste(object p);

            void SetOnPaste(object v);

            object GetOnPaste(object p);

            object GetCurrentStyle();

            void SetOnPropertyChange(object v);

            object GetOnPropertyChange(object p);

            object GetClientRects();

            object GetBoundingClientRect();

            void SetExpression(string propName, string expression, string language);

            object GetExpression(string propName);

            bool RemoveExpression(string propName);

            void SetTabIndex(int v);

            short GetTabIndex();

            void Focus();

            void SetAccessKey(string v);

            string GetAccessKey();

            void SetOnBlur(object v);

            object GetOnBlur();

            void SetOnFocus(object v);

            object GetOnFocus();

            void SetOnResize(object v);

            object GetOnResize();

            void Blur();

            void AddFilter(object pUnk);

            void RemoveFilter(object pUnk);

            int ClientHeight();

            int ClientWidth();

            int ClientTop();

            int ClientLeft();

            bool AttachEvent(string ev, [In][MarshalAs(UnmanagedType.IDispatch)] object pdisp);

            void DetachEvent(string ev, [In][MarshalAs(UnmanagedType.IDispatch)] object pdisp);

            object ReadyState();

            void SetOnReadyStateChange(object v);

            object GetOnReadyStateChange();

            void SetOnRowsDelete(object v);

            object GetOnRowsDelete();

            void SetOnRowsInserted(object v);

            object GetOnRowsInserted();

            void SetOnCellChange(object v);

            object GetOnCellChange();

            void SetDir(string v);

            string GetDir();

            object CreateControlRange();

            int GetScrollHeight();

            int GetScrollWidth();

            void SetScrollTop(int v);

            int GetScrollTop();

            void SetScrollLeft(int v);

            int GetScrollLeft();

            void ClearAttributes();

            void MergeAttributes(object mergeThis);

            void SetOnContextMenu(object v);

            object GetOnContextMenu();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElement InsertAdjacentElement(string where, [In][MarshalAs(UnmanagedType.Interface)] IHTMLElement insertedElement);

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElement applyElement([In][MarshalAs(UnmanagedType.Interface)] IHTMLElement apply, string where);

            string GetAdjacentText(string where);

            string ReplaceAdjacentText(string where, string newText);

            bool CanHaveChildren();

            int AddBehavior(string url, ref object oFactory);

            bool RemoveBehavior(int cookie);

            object GetRuntimeStyle();

            object GetBehaviorUrns();

            void SetTagUrn(string v);

            string GetTagUrn();

            void SetOnBeforeEditFocus(object v);

            object GetOnBeforeEditFocus();

            int GetReadyStateValue();

            [return: MarshalAs(UnmanagedType.Interface)]
            IHTMLElementCollection GetElementsByTagName(string v);
        }

        [SuppressUnmanagedCodeSecurity]
        [ComVisible(true)]
        [Guid("3050f673-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsDual)]
        internal interface IHTMLElement3
        {
            void MergeAttributes(object mergeThis, object pvarFlags);

            bool IsMultiLine();

            bool CanHaveHTML();

            void SetOnLayoutComplete(object v);

            object GetOnLayoutComplete();

            void SetOnPage(object v);

            object GetOnPage();

            void SetInflateBlock(bool v);

            bool GetInflateBlock();

            void SetOnBeforeDeactivate(object v);

            object GetOnBeforeDeactivate();

            void SetActive();

            void SetContentEditable(string v);

            string GetContentEditable();

            bool IsContentEditable();

            void SetHideFocus(bool v);

            bool GetHideFocus();

            void SetDisabled(bool v);

            bool GetDisabled();

            bool IsDisabled();

            void SetOnMove(object v);

            object GetOnMove();

            void SetOnControlSelect(object v);

            object GetOnControlSelect();

            bool FireEvent(string bstrEventName, IntPtr pvarEventObject);

            void SetOnResizeStart(object v);

            object GetOnResizeStart();

            void SetOnResizeEnd(object v);

            object GetOnResizeEnd();

            void SetOnMoveStart(object v);

            object GetOnMoveStart();

            void SetOnMoveEnd(object v);

            object GetOnMoveEnd();

            void SetOnMouseEnter(object v);

            object GetOnMouseEnter();

            void SetOnMouseLeave(object v);

            object GetOnMouseLeave();

            void SetOnActivate(object v);

            object GetOnActivate();

            void SetOnDeactivate(object v);

            object GetOnDeactivate();

            bool DragDrop();

            int GlyphMode();
        }

        [SuppressUnmanagedCodeSecurity]
        [ComVisible(true)]
        [Guid("3050f5da-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsDual)]
        public interface IHTMLDOMNode
        {
            long GetNodeType();

            IHTMLDOMNode GetParentNode();

            bool HasChildNodes();

            object GetChildNodes();

            object GetAttributes();

            IHTMLDOMNode InsertBefore(IHTMLDOMNode newChild, object refChild);

            IHTMLDOMNode RemoveChild(IHTMLDOMNode oldChild);

            IHTMLDOMNode ReplaceChild(IHTMLDOMNode newChild, IHTMLDOMNode oldChild);

            IHTMLDOMNode CloneNode(bool fDeep);

            IHTMLDOMNode RemoveNode(bool fDeep);

            IHTMLDOMNode SwapNode(IHTMLDOMNode otherNode);

            IHTMLDOMNode ReplaceNode(IHTMLDOMNode replacement);

            IHTMLDOMNode AppendChild(IHTMLDOMNode newChild);

            string NodeName();

            void SetNodeValue(object v);

            object GetNodeValue();

            IHTMLDOMNode FirstChild();

            IHTMLDOMNode LastChild();

            IHTMLDOMNode PreviousSibling();

            IHTMLDOMNode NextSibling();
        }

        [ComImport]
        [Guid("3050f60f-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        [TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLElementEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(IHTMLEventObj evtObj);

            [DispId(-600)]
            bool onclick(IHTMLEventObj evtObj);

            [DispId(-601)]
            bool ondblclick(IHTMLEventObj evtObj);

            [DispId(-603)]
            bool onkeypress(IHTMLEventObj evtObj);

            [DispId(-602)]
            void onkeydown(IHTMLEventObj evtObj);

            [DispId(-604)]
            void onkeyup(IHTMLEventObj evtObj);

            [DispId(-2147418103)]
            void onmouseout(IHTMLEventObj evtObj);

            [DispId(-2147418104)]
            void onmouseover(IHTMLEventObj evtObj);

            [DispId(-606)]
            void onmousemove(IHTMLEventObj evtObj);

            [DispId(-605)]
            void onmousedown(IHTMLEventObj evtObj);

            [DispId(-607)]
            void onmouseup(IHTMLEventObj evtObj);

            [DispId(-2147418100)]
            bool onselectstart(IHTMLEventObj evtObj);

            [DispId(-2147418095)]
            void onfilterchange(IHTMLEventObj evtObj);

            [DispId(-2147418101)]
            bool ondragstart(IHTMLEventObj evtObj);

            [DispId(-2147418108)]
            bool onbeforeupdate(IHTMLEventObj evtObj);

            [DispId(-2147418107)]
            void onafterupdate(IHTMLEventObj evtObj);

            [DispId(-2147418099)]
            bool onerrorupdate(IHTMLEventObj evtObj);

            [DispId(-2147418106)]
            bool onrowexit(IHTMLEventObj evtObj);

            [DispId(-2147418105)]
            void onrowenter(IHTMLEventObj evtObj);

            [DispId(-2147418098)]
            void ondatasetchanged(IHTMLEventObj evtObj);

            [DispId(-2147418097)]
            void ondataavailable(IHTMLEventObj evtObj);

            [DispId(-2147418096)]
            void ondatasetcomplete(IHTMLEventObj evtObj);

            [DispId(-2147418094)]
            void onlosecapture(IHTMLEventObj evtObj);

            [DispId(-2147418093)]
            void onpropertychange(IHTMLEventObj evtObj);

            [DispId(1014)]
            void onscroll(IHTMLEventObj evtObj);

            [DispId(-2147418111)]
            void onfocus(IHTMLEventObj evtObj);

            [DispId(-2147418112)]
            void onblur(IHTMLEventObj evtObj);

            [DispId(1016)]
            void onresize(IHTMLEventObj evtObj);

            [DispId(-2147418092)]
            bool ondrag(IHTMLEventObj evtObj);

            [DispId(-2147418091)]
            void ondragend(IHTMLEventObj evtObj);

            [DispId(-2147418090)]
            bool ondragenter(IHTMLEventObj evtObj);

            [DispId(-2147418089)]
            bool ondragover(IHTMLEventObj evtObj);

            [DispId(-2147418088)]
            void ondragleave(IHTMLEventObj evtObj);

            [DispId(-2147418087)]
            bool ondrop(IHTMLEventObj evtObj);

            [DispId(-2147418083)]
            bool onbeforecut(IHTMLEventObj evtObj);

            [DispId(-2147418086)]
            bool oncut(IHTMLEventObj evtObj);

            [DispId(-2147418082)]
            bool onbeforecopy(IHTMLEventObj evtObj);

            [DispId(-2147418085)]
            bool oncopy(IHTMLEventObj evtObj);

            [DispId(-2147418081)]
            bool onbeforepaste(IHTMLEventObj evtObj);

            [DispId(-2147418084)]
            bool onpaste(IHTMLEventObj evtObj);

            [DispId(1023)]
            bool oncontextmenu(IHTMLEventObj evtObj);

            [DispId(-2147418080)]
            void onrowsdelete(IHTMLEventObj evtObj);

            [DispId(-2147418079)]
            void onrowsinserted(IHTMLEventObj evtObj);

            [DispId(-2147418078)]
            void oncellchange(IHTMLEventObj evtObj);

            [DispId(-609)]
            void onreadystatechange(IHTMLEventObj evtObj);

            [DispId(1030)]
            void onlayoutcomplete(IHTMLEventObj evtObj);

            [DispId(1031)]
            void onpage(IHTMLEventObj evtObj);

            [DispId(1042)]
            void onmouseenter(IHTMLEventObj evtObj);

            [DispId(1043)]
            void onmouseleave(IHTMLEventObj evtObj);

            [DispId(1044)]
            void onactivate(IHTMLEventObj evtObj);

            [DispId(1045)]
            void ondeactivate(IHTMLEventObj evtObj);

            [DispId(1034)]
            bool onbeforedeactivate(IHTMLEventObj evtObj);

            [DispId(1047)]
            bool onbeforeactivate(IHTMLEventObj evtObj);

            [DispId(1048)]
            void onfocusin(IHTMLEventObj evtObj);

            [DispId(1049)]
            void onfocusout(IHTMLEventObj evtObj);

            [DispId(1035)]
            void onmove(IHTMLEventObj evtObj);

            [DispId(1036)]
            bool oncontrolselect(IHTMLEventObj evtObj);

            [DispId(1038)]
            bool onmovestart(IHTMLEventObj evtObj);

            [DispId(1039)]
            void onmoveend(IHTMLEventObj evtObj);

            [DispId(1040)]
            bool onresizestart(IHTMLEventObj evtObj);

            [DispId(1041)]
            void onresizeend(IHTMLEventObj evtObj);

            [DispId(1033)]
            bool onmousewheel(IHTMLEventObj evtObj);
        }

        [ComImport]
        [Guid("3050f610-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        [TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLAnchorEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(IHTMLEventObj evtObj);

            [DispId(-600)]
            bool onclick(IHTMLEventObj evtObj);

            [DispId(-601)]
            bool ondblclick(IHTMLEventObj evtObj);

            [DispId(-603)]
            bool onkeypress(IHTMLEventObj evtObj);

            [DispId(-602)]
            void onkeydown(IHTMLEventObj evtObj);

            [DispId(-604)]
            void onkeyup(IHTMLEventObj evtObj);

            [DispId(-2147418103)]
            void onmouseout(IHTMLEventObj evtObj);

            [DispId(-2147418104)]
            void onmouseover(IHTMLEventObj evtObj);

            [DispId(-606)]
            void onmousemove(IHTMLEventObj evtObj);

            [DispId(-605)]
            void onmousedown(IHTMLEventObj evtObj);

            [DispId(-607)]
            void onmouseup(IHTMLEventObj evtObj);

            [DispId(-2147418100)]
            bool onselectstart(IHTMLEventObj evtObj);

            [DispId(-2147418095)]
            void onfilterchange(IHTMLEventObj evtObj);

            [DispId(-2147418101)]
            bool ondragstart(IHTMLEventObj evtObj);

            [DispId(-2147418108)]
            bool onbeforeupdate(IHTMLEventObj evtObj);

            [DispId(-2147418107)]
            void onafterupdate(IHTMLEventObj evtObj);

            [DispId(-2147418099)]
            bool onerrorupdate(IHTMLEventObj evtObj);

            [DispId(-2147418106)]
            bool onrowexit(IHTMLEventObj evtObj);

            [DispId(-2147418105)]
            void onrowenter(IHTMLEventObj evtObj);

            [DispId(-2147418098)]
            void ondatasetchanged(IHTMLEventObj evtObj);

            [DispId(-2147418097)]
            void ondataavailable(IHTMLEventObj evtObj);

            [DispId(-2147418096)]
            void ondatasetcomplete(IHTMLEventObj evtObj);

            [DispId(-2147418094)]
            void onlosecapture(IHTMLEventObj evtObj);

            [DispId(-2147418093)]
            void onpropertychange(IHTMLEventObj evtObj);

            [DispId(1014)]
            void onscroll(IHTMLEventObj evtObj);

            [DispId(-2147418111)]
            void onfocus(IHTMLEventObj evtObj);

            [DispId(-2147418112)]
            void onblur(IHTMLEventObj evtObj);

            [DispId(1016)]
            void onresize(IHTMLEventObj evtObj);

            [DispId(-2147418092)]
            bool ondrag(IHTMLEventObj evtObj);

            [DispId(-2147418091)]
            void ondragend(IHTMLEventObj evtObj);

            [DispId(-2147418090)]
            bool ondragenter(IHTMLEventObj evtObj);

            [DispId(-2147418089)]
            bool ondragover(IHTMLEventObj evtObj);

            [DispId(-2147418088)]
            void ondragleave(IHTMLEventObj evtObj);

            [DispId(-2147418087)]
            bool ondrop(IHTMLEventObj evtObj);

            [DispId(-2147418083)]
            bool onbeforecut(IHTMLEventObj evtObj);

            [DispId(-2147418086)]
            bool oncut(IHTMLEventObj evtObj);

            [DispId(-2147418082)]
            bool onbeforecopy(IHTMLEventObj evtObj);

            [DispId(-2147418085)]
            bool oncopy(IHTMLEventObj evtObj);

            [DispId(-2147418081)]
            bool onbeforepaste(IHTMLEventObj evtObj);

            [DispId(-2147418084)]
            bool onpaste(IHTMLEventObj evtObj);

            [DispId(1023)]
            bool oncontextmenu(IHTMLEventObj evtObj);

            [DispId(-2147418080)]
            void onrowsdelete(IHTMLEventObj evtObj);

            [DispId(-2147418079)]
            void onrowsinserted(IHTMLEventObj evtObj);

            [DispId(-2147418078)]
            void oncellchange(IHTMLEventObj evtObj);

            [DispId(-609)]
            void onreadystatechange(IHTMLEventObj evtObj);

            [DispId(1030)]
            void onlayoutcomplete(IHTMLEventObj evtObj);

            [DispId(1031)]
            void onpage(IHTMLEventObj evtObj);

            [DispId(1042)]
            void onmouseenter(IHTMLEventObj evtObj);

            [DispId(1043)]
            void onmouseleave(IHTMLEventObj evtObj);

            [DispId(1044)]
            void onactivate(IHTMLEventObj evtObj);

            [DispId(1045)]
            void ondeactivate(IHTMLEventObj evtObj);

            [DispId(1034)]
            bool onbeforedeactivate(IHTMLEventObj evtObj);

            [DispId(1047)]
            bool onbeforeactivate(IHTMLEventObj evtObj);

            [DispId(1048)]
            void onfocusin(IHTMLEventObj evtObj);

            [DispId(1049)]
            void onfocusout(IHTMLEventObj evtObj);

            [DispId(1035)]
            void onmove(IHTMLEventObj evtObj);

            [DispId(1036)]
            bool oncontrolselect(IHTMLEventObj evtObj);

            [DispId(1038)]
            bool onmovestart(IHTMLEventObj evtObj);

            [DispId(1039)]
            void onmoveend(IHTMLEventObj evtObj);

            [DispId(1040)]
            bool onresizestart(IHTMLEventObj evtObj);

            [DispId(1041)]
            void onresizeend(IHTMLEventObj evtObj);

            [DispId(1033)]
            bool onmousewheel(IHTMLEventObj evtObj);
        }

        [ComImport]
        [Guid("3050f611-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        [TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLAreaEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(IHTMLEventObj evtObj);

            [DispId(-600)]
            bool onclick(IHTMLEventObj evtObj);

            [DispId(-601)]
            bool ondblclick(IHTMLEventObj evtObj);

            [DispId(-603)]
            bool onkeypress(IHTMLEventObj evtObj);

            [DispId(-602)]
            void onkeydown(IHTMLEventObj evtObj);

            [DispId(-604)]
            void onkeyup(IHTMLEventObj evtObj);

            [DispId(-2147418103)]
            void onmouseout(IHTMLEventObj evtObj);

            [DispId(-2147418104)]
            void onmouseover(IHTMLEventObj evtObj);

            [DispId(-606)]
            void onmousemove(IHTMLEventObj evtObj);

            [DispId(-605)]
            void onmousedown(IHTMLEventObj evtObj);

            [DispId(-607)]
            void onmouseup(IHTMLEventObj evtObj);

            [DispId(-2147418100)]
            bool onselectstart(IHTMLEventObj evtObj);

            [DispId(-2147418095)]
            void onfilterchange(IHTMLEventObj evtObj);

            [DispId(-2147418101)]
            bool ondragstart(IHTMLEventObj evtObj);

            [DispId(-2147418108)]
            bool onbeforeupdate(IHTMLEventObj evtObj);

            [DispId(-2147418107)]
            void onafterupdate(IHTMLEventObj evtObj);

            [DispId(-2147418099)]
            bool onerrorupdate(IHTMLEventObj evtObj);

            [DispId(-2147418106)]
            bool onrowexit(IHTMLEventObj evtObj);

            [DispId(-2147418105)]
            void onrowenter(IHTMLEventObj evtObj);

            [DispId(-2147418098)]
            void ondatasetchanged(IHTMLEventObj evtObj);

            [DispId(-2147418097)]
            void ondataavailable(IHTMLEventObj evtObj);

            [DispId(-2147418096)]
            void ondatasetcomplete(IHTMLEventObj evtObj);

            [DispId(-2147418094)]
            void onlosecapture(IHTMLEventObj evtObj);

            [DispId(-2147418093)]
            void onpropertychange(IHTMLEventObj evtObj);

            [DispId(1014)]
            void onscroll(IHTMLEventObj evtObj);

            [DispId(-2147418111)]
            void onfocus(IHTMLEventObj evtObj);

            [DispId(-2147418112)]
            void onblur(IHTMLEventObj evtObj);

            [DispId(1016)]
            void onresize(IHTMLEventObj evtObj);

            [DispId(-2147418092)]
            bool ondrag(IHTMLEventObj evtObj);

            [DispId(-2147418091)]
            void ondragend(IHTMLEventObj evtObj);

            [DispId(-2147418090)]
            bool ondragenter(IHTMLEventObj evtObj);

            [DispId(-2147418089)]
            bool ondragover(IHTMLEventObj evtObj);

            [DispId(-2147418088)]
            void ondragleave(IHTMLEventObj evtObj);

            [DispId(-2147418087)]
            bool ondrop(IHTMLEventObj evtObj);

            [DispId(-2147418083)]
            bool onbeforecut(IHTMLEventObj evtObj);

            [DispId(-2147418086)]
            bool oncut(IHTMLEventObj evtObj);

            [DispId(-2147418082)]
            bool onbeforecopy(IHTMLEventObj evtObj);

            [DispId(-2147418085)]
            bool oncopy(IHTMLEventObj evtObj);

            [DispId(-2147418081)]
            bool onbeforepaste(IHTMLEventObj evtObj);

            [DispId(-2147418084)]
            bool onpaste(IHTMLEventObj evtObj);

            [DispId(1023)]
            bool oncontextmenu(IHTMLEventObj evtObj);

            [DispId(-2147418080)]
            void onrowsdelete(IHTMLEventObj evtObj);

            [DispId(-2147418079)]
            void onrowsinserted(IHTMLEventObj evtObj);

            [DispId(-2147418078)]
            void oncellchange(IHTMLEventObj evtObj);

            [DispId(-609)]
            void onreadystatechange(IHTMLEventObj evtObj);

            [DispId(1030)]
            void onlayoutcomplete(IHTMLEventObj evtObj);

            [DispId(1031)]
            void onpage(IHTMLEventObj evtObj);

            [DispId(1042)]
            void onmouseenter(IHTMLEventObj evtObj);

            [DispId(1043)]
            void onmouseleave(IHTMLEventObj evtObj);

            [DispId(1044)]
            void onactivate(IHTMLEventObj evtObj);

            [DispId(1045)]
            void ondeactivate(IHTMLEventObj evtObj);

            [DispId(1034)]
            bool onbeforedeactivate(IHTMLEventObj evtObj);

            [DispId(1047)]
            bool onbeforeactivate(IHTMLEventObj evtObj);

            [DispId(1048)]
            void onfocusin(IHTMLEventObj evtObj);

            [DispId(1049)]
            void onfocusout(IHTMLEventObj evtObj);

            [DispId(1035)]
            void onmove(IHTMLEventObj evtObj);

            [DispId(1036)]
            bool oncontrolselect(IHTMLEventObj evtObj);

            [DispId(1038)]
            bool onmovestart(IHTMLEventObj evtObj);

            [DispId(1039)]
            void onmoveend(IHTMLEventObj evtObj);

            [DispId(1040)]
            bool onresizestart(IHTMLEventObj evtObj);

            [DispId(1041)]
            void onresizeend(IHTMLEventObj evtObj);

            [DispId(1033)]
            bool onmousewheel(IHTMLEventObj evtObj);
        }

        [ComImport]
        [Guid("3050f617-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        [TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLButtonElementEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(IHTMLEventObj evtObj);

            [DispId(-600)]
            bool onclick(IHTMLEventObj evtObj);

            [DispId(-601)]
            bool ondblclick(IHTMLEventObj evtObj);

            [DispId(-603)]
            bool onkeypress(IHTMLEventObj evtObj);

            [DispId(-602)]
            void onkeydown(IHTMLEventObj evtObj);

            [DispId(-604)]
            void onkeyup(IHTMLEventObj evtObj);

            [DispId(-2147418103)]
            void onmouseout(IHTMLEventObj evtObj);

            [DispId(-2147418104)]
            void onmouseover(IHTMLEventObj evtObj);

            [DispId(-606)]
            void onmousemove(IHTMLEventObj evtObj);

            [DispId(-605)]
            void onmousedown(IHTMLEventObj evtObj);

            [DispId(-607)]
            void onmouseup(IHTMLEventObj evtObj);

            [DispId(-2147418100)]
            bool onselectstart(IHTMLEventObj evtObj);

            [DispId(-2147418095)]
            void onfilterchange(IHTMLEventObj evtObj);

            [DispId(-2147418101)]
            bool ondragstart(IHTMLEventObj evtObj);

            [DispId(-2147418108)]
            bool onbeforeupdate(IHTMLEventObj evtObj);

            [DispId(-2147418107)]
            void onafterupdate(IHTMLEventObj evtObj);

            [DispId(-2147418099)]
            bool onerrorupdate(IHTMLEventObj evtObj);

            [DispId(-2147418106)]
            bool onrowexit(IHTMLEventObj evtObj);

            [DispId(-2147418105)]
            void onrowenter(IHTMLEventObj evtObj);

            [DispId(-2147418098)]
            void ondatasetchanged(IHTMLEventObj evtObj);

            [DispId(-2147418097)]
            void ondataavailable(IHTMLEventObj evtObj);

            [DispId(-2147418096)]
            void ondatasetcomplete(IHTMLEventObj evtObj);

            [DispId(-2147418094)]
            void onlosecapture(IHTMLEventObj evtObj);

            [DispId(-2147418093)]
            void onpropertychange(IHTMLEventObj evtObj);

            [DispId(1014)]
            void onscroll(IHTMLEventObj evtObj);

            [DispId(-2147418111)]
            void onfocus(IHTMLEventObj evtObj);

            [DispId(-2147418112)]
            void onblur(IHTMLEventObj evtObj);

            [DispId(1016)]
            void onresize(IHTMLEventObj evtObj);

            [DispId(-2147418092)]
            bool ondrag(IHTMLEventObj evtObj);

            [DispId(-2147418091)]
            void ondragend(IHTMLEventObj evtObj);

            [DispId(-2147418090)]
            bool ondragenter(IHTMLEventObj evtObj);

            [DispId(-2147418089)]
            bool ondragover(IHTMLEventObj evtObj);

            [DispId(-2147418088)]
            void ondragleave(IHTMLEventObj evtObj);

            [DispId(-2147418087)]
            bool ondrop(IHTMLEventObj evtObj);

            [DispId(-2147418083)]
            bool onbeforecut(IHTMLEventObj evtObj);

            [DispId(-2147418086)]
            bool oncut(IHTMLEventObj evtObj);

            [DispId(-2147418082)]
            bool onbeforecopy(IHTMLEventObj evtObj);

            [DispId(-2147418085)]
            bool oncopy(IHTMLEventObj evtObj);

            [DispId(-2147418081)]
            bool onbeforepaste(IHTMLEventObj evtObj);

            [DispId(-2147418084)]
            bool onpaste(IHTMLEventObj evtObj);

            [DispId(1023)]
            bool oncontextmenu(IHTMLEventObj evtObj);

            [DispId(-2147418080)]
            void onrowsdelete(IHTMLEventObj evtObj);

            [DispId(-2147418079)]
            void onrowsinserted(IHTMLEventObj evtObj);

            [DispId(-2147418078)]
            void oncellchange(IHTMLEventObj evtObj);

            [DispId(-609)]
            void onreadystatechange(IHTMLEventObj evtObj);

            [DispId(1030)]
            void onlayoutcomplete(IHTMLEventObj evtObj);

            [DispId(1031)]
            void onpage(IHTMLEventObj evtObj);

            [DispId(1042)]
            void onmouseenter(IHTMLEventObj evtObj);

            [DispId(1043)]
            void onmouseleave(IHTMLEventObj evtObj);

            [DispId(1044)]
            void onactivate(IHTMLEventObj evtObj);

            [DispId(1045)]
            void ondeactivate(IHTMLEventObj evtObj);

            [DispId(1034)]
            bool onbeforedeactivate(IHTMLEventObj evtObj);

            [DispId(1047)]
            bool onbeforeactivate(IHTMLEventObj evtObj);

            [DispId(1048)]
            void onfocusin(IHTMLEventObj evtObj);

            [DispId(1049)]
            void onfocusout(IHTMLEventObj evtObj);

            [DispId(1035)]
            void onmove(IHTMLEventObj evtObj);

            [DispId(1036)]
            bool oncontrolselect(IHTMLEventObj evtObj);

            [DispId(1038)]
            bool onmovestart(IHTMLEventObj evtObj);

            [DispId(1039)]
            void onmoveend(IHTMLEventObj evtObj);

            [DispId(1040)]
            bool onresizestart(IHTMLEventObj evtObj);

            [DispId(1041)]
            void onresizeend(IHTMLEventObj evtObj);

            [DispId(1033)]
            bool onmousewheel(IHTMLEventObj evtObj);
        }

        [ComImport]
        [Guid("3050f612-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        [TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLControlElementEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(IHTMLEventObj evtObj);

            [DispId(-600)]
            bool onclick(IHTMLEventObj evtObj);

            [DispId(-601)]
            bool ondblclick(IHTMLEventObj evtObj);

            [DispId(-603)]
            bool onkeypress(IHTMLEventObj evtObj);

            [DispId(-602)]
            void onkeydown(IHTMLEventObj evtObj);

            [DispId(-604)]
            void onkeyup(IHTMLEventObj evtObj);

            [DispId(-2147418103)]
            void onmouseout(IHTMLEventObj evtObj);

            [DispId(-2147418104)]
            void onmouseover(IHTMLEventObj evtObj);

            [DispId(-606)]
            void onmousemove(IHTMLEventObj evtObj);

            [DispId(-605)]
            void onmousedown(IHTMLEventObj evtObj);

            [DispId(-607)]
            void onmouseup(IHTMLEventObj evtObj);

            [DispId(-2147418100)]
            bool onselectstart(IHTMLEventObj evtObj);

            [DispId(-2147418095)]
            void onfilterchange(IHTMLEventObj evtObj);

            [DispId(-2147418101)]
            bool ondragstart(IHTMLEventObj evtObj);

            [DispId(-2147418108)]
            bool onbeforeupdate(IHTMLEventObj evtObj);

            [DispId(-2147418107)]
            void onafterupdate(IHTMLEventObj evtObj);

            [DispId(-2147418099)]
            bool onerrorupdate(IHTMLEventObj evtObj);

            [DispId(-2147418106)]
            bool onrowexit(IHTMLEventObj evtObj);

            [DispId(-2147418105)]
            void onrowenter(IHTMLEventObj evtObj);

            [DispId(-2147418098)]
            void ondatasetchanged(IHTMLEventObj evtObj);

            [DispId(-2147418097)]
            void ondataavailable(IHTMLEventObj evtObj);

            [DispId(-2147418096)]
            void ondatasetcomplete(IHTMLEventObj evtObj);

            [DispId(-2147418094)]
            void onlosecapture(IHTMLEventObj evtObj);

            [DispId(-2147418093)]
            void onpropertychange(IHTMLEventObj evtObj);

            [DispId(1014)]
            void onscroll(IHTMLEventObj evtObj);

            [DispId(-2147418111)]
            void onfocus(IHTMLEventObj evtObj);

            [DispId(-2147418112)]
            void onblur(IHTMLEventObj evtObj);

            [DispId(1016)]
            void onresize(IHTMLEventObj evtObj);

            [DispId(-2147418092)]
            bool ondrag(IHTMLEventObj evtObj);

            [DispId(-2147418091)]
            void ondragend(IHTMLEventObj evtObj);

            [DispId(-2147418090)]
            bool ondragenter(IHTMLEventObj evtObj);

            [DispId(-2147418089)]
            bool ondragover(IHTMLEventObj evtObj);

            [DispId(-2147418088)]
            void ondragleave(IHTMLEventObj evtObj);

            [DispId(-2147418087)]
            bool ondrop(IHTMLEventObj evtObj);

            [DispId(-2147418083)]
            bool onbeforecut(IHTMLEventObj evtObj);

            [DispId(-2147418086)]
            bool oncut(IHTMLEventObj evtObj);

            [DispId(-2147418082)]
            bool onbeforecopy(IHTMLEventObj evtObj);

            [DispId(-2147418085)]
            bool oncopy(IHTMLEventObj evtObj);

            [DispId(-2147418081)]
            bool onbeforepaste(IHTMLEventObj evtObj);

            [DispId(-2147418084)]
            bool onpaste(IHTMLEventObj evtObj);

            [DispId(1023)]
            bool oncontextmenu(IHTMLEventObj evtObj);

            [DispId(-2147418080)]
            void onrowsdelete(IHTMLEventObj evtObj);

            [DispId(-2147418079)]
            void onrowsinserted(IHTMLEventObj evtObj);

            [DispId(-2147418078)]
            void oncellchange(IHTMLEventObj evtObj);

            [DispId(-609)]
            void onreadystatechange(IHTMLEventObj evtObj);

            [DispId(1030)]
            void onlayoutcomplete(IHTMLEventObj evtObj);

            [DispId(1031)]
            void onpage(IHTMLEventObj evtObj);

            [DispId(1042)]
            void onmouseenter(IHTMLEventObj evtObj);

            [DispId(1043)]
            void onmouseleave(IHTMLEventObj evtObj);

            [DispId(1044)]
            void onactivate(IHTMLEventObj evtObj);

            [DispId(1045)]
            void ondeactivate(IHTMLEventObj evtObj);

            [DispId(1034)]
            bool onbeforedeactivate(IHTMLEventObj evtObj);

            [DispId(1047)]
            bool onbeforeactivate(IHTMLEventObj evtObj);

            [DispId(1048)]
            void onfocusin(IHTMLEventObj evtObj);

            [DispId(1049)]
            void onfocusout(IHTMLEventObj evtObj);

            [DispId(1035)]
            void onmove(IHTMLEventObj evtObj);

            [DispId(1036)]
            bool oncontrolselect(IHTMLEventObj evtObj);

            [DispId(1038)]
            bool onmovestart(IHTMLEventObj evtObj);

            [DispId(1039)]
            void onmoveend(IHTMLEventObj evtObj);

            [DispId(1040)]
            bool onresizestart(IHTMLEventObj evtObj);

            [DispId(1041)]
            void onresizeend(IHTMLEventObj evtObj);

            [DispId(1033)]
            bool onmousewheel(IHTMLEventObj evtObj);
        }

        [ComImport]
        [Guid("3050f614-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        [TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLFormElementEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(IHTMLEventObj evtObj);

            [DispId(-600)]
            bool onclick(IHTMLEventObj evtObj);

            [DispId(-601)]
            bool ondblclick(IHTMLEventObj evtObj);

            [DispId(-603)]
            bool onkeypress(IHTMLEventObj evtObj);

            [DispId(-602)]
            void onkeydown(IHTMLEventObj evtObj);

            [DispId(-604)]
            void onkeyup(IHTMLEventObj evtObj);

            [DispId(-2147418103)]
            void onmouseout(IHTMLEventObj evtObj);

            [DispId(-2147418104)]
            void onmouseover(IHTMLEventObj evtObj);

            [DispId(-606)]
            void onmousemove(IHTMLEventObj evtObj);

            [DispId(-605)]
            void onmousedown(IHTMLEventObj evtObj);

            [DispId(-607)]
            void onmouseup(IHTMLEventObj evtObj);

            [DispId(-2147418100)]
            bool onselectstart(IHTMLEventObj evtObj);

            [DispId(-2147418095)]
            void onfilterchange(IHTMLEventObj evtObj);

            [DispId(-2147418101)]
            bool ondragstart(IHTMLEventObj evtObj);

            [DispId(-2147418108)]
            bool onbeforeupdate(IHTMLEventObj evtObj);

            [DispId(-2147418107)]
            void onafterupdate(IHTMLEventObj evtObj);

            [DispId(-2147418099)]
            bool onerrorupdate(IHTMLEventObj evtObj);

            [DispId(-2147418106)]
            bool onrowexit(IHTMLEventObj evtObj);

            [DispId(-2147418105)]
            void onrowenter(IHTMLEventObj evtObj);

            [DispId(-2147418098)]
            void ondatasetchanged(IHTMLEventObj evtObj);

            [DispId(-2147418097)]
            void ondataavailable(IHTMLEventObj evtObj);

            [DispId(-2147418096)]
            void ondatasetcomplete(IHTMLEventObj evtObj);

            [DispId(-2147418094)]
            void onlosecapture(IHTMLEventObj evtObj);

            [DispId(-2147418093)]
            void onpropertychange(IHTMLEventObj evtObj);

            [DispId(1014)]
            void onscroll(IHTMLEventObj evtObj);

            [DispId(-2147418111)]
            void onfocus(IHTMLEventObj evtObj);

            [DispId(-2147418112)]
            void onblur(IHTMLEventObj evtObj);

            [DispId(1016)]
            void onresize(IHTMLEventObj evtObj);

            [DispId(-2147418092)]
            bool ondrag(IHTMLEventObj evtObj);

            [DispId(-2147418091)]
            void ondragend(IHTMLEventObj evtObj);

            [DispId(-2147418090)]
            bool ondragenter(IHTMLEventObj evtObj);

            [DispId(-2147418089)]
            bool ondragover(IHTMLEventObj evtObj);

            [DispId(-2147418088)]
            void ondragleave(IHTMLEventObj evtObj);

            [DispId(-2147418087)]
            bool ondrop(IHTMLEventObj evtObj);

            [DispId(-2147418083)]
            bool onbeforecut(IHTMLEventObj evtObj);

            [DispId(-2147418086)]
            bool oncut(IHTMLEventObj evtObj);

            [DispId(-2147418082)]
            bool onbeforecopy(IHTMLEventObj evtObj);

            [DispId(-2147418085)]
            bool oncopy(IHTMLEventObj evtObj);

            [DispId(-2147418081)]
            bool onbeforepaste(IHTMLEventObj evtObj);

            [DispId(-2147418084)]
            bool onpaste(IHTMLEventObj evtObj);

            [DispId(1023)]
            bool oncontextmenu(IHTMLEventObj evtObj);

            [DispId(-2147418080)]
            void onrowsdelete(IHTMLEventObj evtObj);

            [DispId(-2147418079)]
            void onrowsinserted(IHTMLEventObj evtObj);

            [DispId(-2147418078)]
            void oncellchange(IHTMLEventObj evtObj);

            [DispId(-609)]
            void onreadystatechange(IHTMLEventObj evtObj);

            [DispId(1030)]
            void onlayoutcomplete(IHTMLEventObj evtObj);

            [DispId(1031)]
            void onpage(IHTMLEventObj evtObj);

            [DispId(1042)]
            void onmouseenter(IHTMLEventObj evtObj);

            [DispId(1043)]
            void onmouseleave(IHTMLEventObj evtObj);

            [DispId(1044)]
            void onactivate(IHTMLEventObj evtObj);

            [DispId(1045)]
            void ondeactivate(IHTMLEventObj evtObj);

            [DispId(1034)]
            bool onbeforedeactivate(IHTMLEventObj evtObj);

            [DispId(1047)]
            bool onbeforeactivate(IHTMLEventObj evtObj);

            [DispId(1048)]
            void onfocusin(IHTMLEventObj evtObj);

            [DispId(1049)]
            void onfocusout(IHTMLEventObj evtObj);

            [DispId(1035)]
            void onmove(IHTMLEventObj evtObj);

            [DispId(1036)]
            bool oncontrolselect(IHTMLEventObj evtObj);

            [DispId(1038)]
            bool onmovestart(IHTMLEventObj evtObj);

            [DispId(1039)]
            void onmoveend(IHTMLEventObj evtObj);

            [DispId(1040)]
            bool onresizestart(IHTMLEventObj evtObj);

            [DispId(1041)]
            void onresizeend(IHTMLEventObj evtObj);

            [DispId(1033)]
            bool onmousewheel(IHTMLEventObj evtObj);

            [DispId(1007)]
            bool onsubmit(IHTMLEventObj evtObj);

            [DispId(1015)]
            bool onreset(IHTMLEventObj evtObj);
        }

        [ComImport]
        [Guid("3050f7ff-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        [TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLFrameSiteEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(IHTMLEventObj evtObj);

            [DispId(-600)]
            bool onclick(IHTMLEventObj evtObj);

            [DispId(-601)]
            bool ondblclick(IHTMLEventObj evtObj);

            [DispId(-603)]
            bool onkeypress(IHTMLEventObj evtObj);

            [DispId(-602)]
            void onkeydown(IHTMLEventObj evtObj);

            [DispId(-604)]
            void onkeyup(IHTMLEventObj evtObj);

            [DispId(-2147418103)]
            void onmouseout(IHTMLEventObj evtObj);

            [DispId(-2147418104)]
            void onmouseover(IHTMLEventObj evtObj);

            [DispId(-606)]
            void onmousemove(IHTMLEventObj evtObj);

            [DispId(-605)]
            void onmousedown(IHTMLEventObj evtObj);

            [DispId(-607)]
            void onmouseup(IHTMLEventObj evtObj);

            [DispId(-2147418100)]
            bool onselectstart(IHTMLEventObj evtObj);

            [DispId(-2147418095)]
            void onfilterchange(IHTMLEventObj evtObj);

            [DispId(-2147418101)]
            bool ondragstart(IHTMLEventObj evtObj);

            [DispId(-2147418108)]
            bool onbeforeupdate(IHTMLEventObj evtObj);

            [DispId(-2147418107)]
            void onafterupdate(IHTMLEventObj evtObj);

            [DispId(-2147418099)]
            bool onerrorupdate(IHTMLEventObj evtObj);

            [DispId(-2147418106)]
            bool onrowexit(IHTMLEventObj evtObj);

            [DispId(-2147418105)]
            void onrowenter(IHTMLEventObj evtObj);

            [DispId(-2147418098)]
            void ondatasetchanged(IHTMLEventObj evtObj);

            [DispId(-2147418097)]
            void ondataavailable(IHTMLEventObj evtObj);

            [DispId(-2147418096)]
            void ondatasetcomplete(IHTMLEventObj evtObj);

            [DispId(-2147418094)]
            void onlosecapture(IHTMLEventObj evtObj);

            [DispId(-2147418093)]
            void onpropertychange(IHTMLEventObj evtObj);

            [DispId(1014)]
            void onscroll(IHTMLEventObj evtObj);

            [DispId(-2147418111)]
            void onfocus(IHTMLEventObj evtObj);

            [DispId(-2147418112)]
            void onblur(IHTMLEventObj evtObj);

            [DispId(1016)]
            void onresize(IHTMLEventObj evtObj);

            [DispId(-2147418092)]
            bool ondrag(IHTMLEventObj evtObj);

            [DispId(-2147418091)]
            void ondragend(IHTMLEventObj evtObj);

            [DispId(-2147418090)]
            bool ondragenter(IHTMLEventObj evtObj);

            [DispId(-2147418089)]
            bool ondragover(IHTMLEventObj evtObj);

            [DispId(-2147418088)]
            void ondragleave(IHTMLEventObj evtObj);

            [DispId(-2147418087)]
            bool ondrop(IHTMLEventObj evtObj);

            [DispId(-2147418083)]
            bool onbeforecut(IHTMLEventObj evtObj);

            [DispId(-2147418086)]
            bool oncut(IHTMLEventObj evtObj);

            [DispId(-2147418082)]
            bool onbeforecopy(IHTMLEventObj evtObj);

            [DispId(-2147418085)]
            bool oncopy(IHTMLEventObj evtObj);

            [DispId(-2147418081)]
            bool onbeforepaste(IHTMLEventObj evtObj);

            [DispId(-2147418084)]
            bool onpaste(IHTMLEventObj evtObj);

            [DispId(1023)]
            bool oncontextmenu(IHTMLEventObj evtObj);

            [DispId(-2147418080)]
            void onrowsdelete(IHTMLEventObj evtObj);

            [DispId(-2147418079)]
            void onrowsinserted(IHTMLEventObj evtObj);

            [DispId(-2147418078)]
            void oncellchange(IHTMLEventObj evtObj);

            [DispId(-609)]
            void onreadystatechange(IHTMLEventObj evtObj);

            [DispId(1030)]
            void onlayoutcomplete(IHTMLEventObj evtObj);

            [DispId(1031)]
            void onpage(IHTMLEventObj evtObj);

            [DispId(1042)]
            void onmouseenter(IHTMLEventObj evtObj);

            [DispId(1043)]
            void onmouseleave(IHTMLEventObj evtObj);

            [DispId(1044)]
            void onactivate(IHTMLEventObj evtObj);

            [DispId(1045)]
            void ondeactivate(IHTMLEventObj evtObj);

            [DispId(1034)]
            bool onbeforedeactivate(IHTMLEventObj evtObj);

            [DispId(1047)]
            bool onbeforeactivate(IHTMLEventObj evtObj);

            [DispId(1048)]
            void onfocusin(IHTMLEventObj evtObj);

            [DispId(1049)]
            void onfocusout(IHTMLEventObj evtObj);

            [DispId(1035)]
            void onmove(IHTMLEventObj evtObj);

            [DispId(1036)]
            bool oncontrolselect(IHTMLEventObj evtObj);

            [DispId(1038)]
            bool onmovestart(IHTMLEventObj evtObj);

            [DispId(1039)]
            void onmoveend(IHTMLEventObj evtObj);

            [DispId(1040)]
            bool onresizestart(IHTMLEventObj evtObj);

            [DispId(1041)]
            void onresizeend(IHTMLEventObj evtObj);

            [DispId(1033)]
            bool onmousewheel(IHTMLEventObj evtObj);

            [DispId(1003)]
            void onload(IHTMLEventObj evtObj);
        }

        [ComImport]
        [Guid("3050f616-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        [TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLImgEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(IHTMLEventObj evtObj);

            [DispId(-600)]
            bool onclick(IHTMLEventObj evtObj);

            [DispId(-601)]
            bool ondblclick(IHTMLEventObj evtObj);

            [DispId(-603)]
            bool onkeypress(IHTMLEventObj evtObj);

            [DispId(-602)]
            void onkeydown(IHTMLEventObj evtObj);

            [DispId(-604)]
            void onkeyup(IHTMLEventObj evtObj);

            [DispId(-2147418103)]
            void onmouseout(IHTMLEventObj evtObj);

            [DispId(-2147418104)]
            void onmouseover(IHTMLEventObj evtObj);

            [DispId(-606)]
            void onmousemove(IHTMLEventObj evtObj);

            [DispId(-605)]
            void onmousedown(IHTMLEventObj evtObj);

            [DispId(-607)]
            void onmouseup(IHTMLEventObj evtObj);

            [DispId(-2147418100)]
            bool onselectstart(IHTMLEventObj evtObj);

            [DispId(-2147418095)]
            void onfilterchange(IHTMLEventObj evtObj);

            [DispId(-2147418101)]
            bool ondragstart(IHTMLEventObj evtObj);

            [DispId(-2147418108)]
            bool onbeforeupdate(IHTMLEventObj evtObj);

            [DispId(-2147418107)]
            void onafterupdate(IHTMLEventObj evtObj);

            [DispId(-2147418099)]
            bool onerrorupdate(IHTMLEventObj evtObj);

            [DispId(-2147418106)]
            bool onrowexit(IHTMLEventObj evtObj);

            [DispId(-2147418105)]
            void onrowenter(IHTMLEventObj evtObj);

            [DispId(-2147418098)]
            void ondatasetchanged(IHTMLEventObj evtObj);

            [DispId(-2147418097)]
            void ondataavailable(IHTMLEventObj evtObj);

            [DispId(-2147418096)]
            void ondatasetcomplete(IHTMLEventObj evtObj);

            [DispId(-2147418094)]
            void onlosecapture(IHTMLEventObj evtObj);

            [DispId(-2147418093)]
            void onpropertychange(IHTMLEventObj evtObj);

            [DispId(1014)]
            void onscroll(IHTMLEventObj evtObj);

            [DispId(-2147418111)]
            void onfocus(IHTMLEventObj evtObj);

            [DispId(-2147418112)]
            void onblur(IHTMLEventObj evtObj);

            [DispId(1016)]
            void onresize(IHTMLEventObj evtObj);

            [DispId(-2147418092)]
            bool ondrag(IHTMLEventObj evtObj);

            [DispId(-2147418091)]
            void ondragend(IHTMLEventObj evtObj);

            [DispId(-2147418090)]
            bool ondragenter(IHTMLEventObj evtObj);

            [DispId(-2147418089)]
            bool ondragover(IHTMLEventObj evtObj);

            [DispId(-2147418088)]
            void ondragleave(IHTMLEventObj evtObj);

            [DispId(-2147418087)]
            bool ondrop(IHTMLEventObj evtObj);

            [DispId(-2147418083)]
            bool onbeforecut(IHTMLEventObj evtObj);

            [DispId(-2147418086)]
            bool oncut(IHTMLEventObj evtObj);

            [DispId(-2147418082)]
            bool onbeforecopy(IHTMLEventObj evtObj);

            [DispId(-2147418085)]
            bool oncopy(IHTMLEventObj evtObj);

            [DispId(-2147418081)]
            bool onbeforepaste(IHTMLEventObj evtObj);

            [DispId(-2147418084)]
            bool onpaste(IHTMLEventObj evtObj);

            [DispId(1023)]
            bool oncontextmenu(IHTMLEventObj evtObj);

            [DispId(-2147418080)]
            void onrowsdelete(IHTMLEventObj evtObj);

            [DispId(-2147418079)]
            void onrowsinserted(IHTMLEventObj evtObj);

            [DispId(-2147418078)]
            void oncellchange(IHTMLEventObj evtObj);

            [DispId(-609)]
            void onreadystatechange(IHTMLEventObj evtObj);

            [DispId(1030)]
            void onlayoutcomplete(IHTMLEventObj evtObj);

            [DispId(1031)]
            void onpage(IHTMLEventObj evtObj);

            [DispId(1042)]
            void onmouseenter(IHTMLEventObj evtObj);

            [DispId(1043)]
            void onmouseleave(IHTMLEventObj evtObj);

            [DispId(1044)]
            void onactivate(IHTMLEventObj evtObj);

            [DispId(1045)]
            void ondeactivate(IHTMLEventObj evtObj);

            [DispId(1034)]
            bool onbeforedeactivate(IHTMLEventObj evtObj);

            [DispId(1047)]
            bool onbeforeactivate(IHTMLEventObj evtObj);

            [DispId(1048)]
            void onfocusin(IHTMLEventObj evtObj);

            [DispId(1049)]
            void onfocusout(IHTMLEventObj evtObj);

            [DispId(1035)]
            void onmove(IHTMLEventObj evtObj);

            [DispId(1036)]
            bool oncontrolselect(IHTMLEventObj evtObj);

            [DispId(1038)]
            bool onmovestart(IHTMLEventObj evtObj);

            [DispId(1039)]
            void onmoveend(IHTMLEventObj evtObj);

            [DispId(1040)]
            bool onresizestart(IHTMLEventObj evtObj);

            [DispId(1041)]
            void onresizeend(IHTMLEventObj evtObj);

            [DispId(1033)]
            bool onmousewheel(IHTMLEventObj evtObj);

            [DispId(1003)]
            void onload(IHTMLEventObj evtObj);

            [DispId(1002)]
            void onerror(IHTMLEventObj evtObj);

            [DispId(1000)]
            void onabort(IHTMLEventObj evtObj);
        }

        [ComImport]
        [Guid("3050f61a-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        [TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLInputFileElementEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(IHTMLEventObj evtObj);

            [DispId(-600)]
            bool onclick(IHTMLEventObj evtObj);

            [DispId(-601)]
            bool ondblclick(IHTMLEventObj evtObj);

            [DispId(-603)]
            bool onkeypress(IHTMLEventObj evtObj);

            [DispId(-602)]
            void onkeydown(IHTMLEventObj evtObj);

            [DispId(-604)]
            void onkeyup(IHTMLEventObj evtObj);

            [DispId(-2147418103)]
            void onmouseout(IHTMLEventObj evtObj);

            [DispId(-2147418104)]
            void onmouseover(IHTMLEventObj evtObj);

            [DispId(-606)]
            void onmousemove(IHTMLEventObj evtObj);

            [DispId(-605)]
            void onmousedown(IHTMLEventObj evtObj);

            [DispId(-607)]
            void onmouseup(IHTMLEventObj evtObj);

            [DispId(-2147418100)]
            bool onselectstart(IHTMLEventObj evtObj);

            [DispId(-2147418095)]
            void onfilterchange(IHTMLEventObj evtObj);

            [DispId(-2147418101)]
            bool ondragstart(IHTMLEventObj evtObj);

            [DispId(-2147418108)]
            bool onbeforeupdate(IHTMLEventObj evtObj);

            [DispId(-2147418107)]
            void onafterupdate(IHTMLEventObj evtObj);

            [DispId(-2147418099)]
            bool onerrorupdate(IHTMLEventObj evtObj);

            [DispId(-2147418106)]
            bool onrowexit(IHTMLEventObj evtObj);

            [DispId(-2147418105)]
            void onrowenter(IHTMLEventObj evtObj);

            [DispId(-2147418098)]
            void ondatasetchanged(IHTMLEventObj evtObj);

            [DispId(-2147418097)]
            void ondataavailable(IHTMLEventObj evtObj);

            [DispId(-2147418096)]
            void ondatasetcomplete(IHTMLEventObj evtObj);

            [DispId(-2147418094)]
            void onlosecapture(IHTMLEventObj evtObj);

            [DispId(-2147418093)]
            void onpropertychange(IHTMLEventObj evtObj);

            [DispId(1014)]
            void onscroll(IHTMLEventObj evtObj);

            [DispId(-2147418111)]
            void onfocus(IHTMLEventObj evtObj);

            [DispId(-2147418112)]
            void onblur(IHTMLEventObj evtObj);

            [DispId(1016)]
            void onresize(IHTMLEventObj evtObj);

            [DispId(-2147418092)]
            bool ondrag(IHTMLEventObj evtObj);

            [DispId(-2147418091)]
            void ondragend(IHTMLEventObj evtObj);

            [DispId(-2147418090)]
            bool ondragenter(IHTMLEventObj evtObj);

            [DispId(-2147418089)]
            bool ondragover(IHTMLEventObj evtObj);

            [DispId(-2147418088)]
            void ondragleave(IHTMLEventObj evtObj);

            [DispId(-2147418087)]
            bool ondrop(IHTMLEventObj evtObj);

            [DispId(-2147418083)]
            bool onbeforecut(IHTMLEventObj evtObj);

            [DispId(-2147418086)]
            bool oncut(IHTMLEventObj evtObj);

            [DispId(-2147418082)]
            bool onbeforecopy(IHTMLEventObj evtObj);

            [DispId(-2147418085)]
            bool oncopy(IHTMLEventObj evtObj);

            [DispId(-2147418081)]
            bool onbeforepaste(IHTMLEventObj evtObj);

            [DispId(-2147418084)]
            bool onpaste(IHTMLEventObj evtObj);

            [DispId(1023)]
            bool oncontextmenu(IHTMLEventObj evtObj);

            [DispId(-2147418080)]
            void onrowsdelete(IHTMLEventObj evtObj);

            [DispId(-2147418079)]
            void onrowsinserted(IHTMLEventObj evtObj);

            [DispId(-2147418078)]
            void oncellchange(IHTMLEventObj evtObj);

            [DispId(-609)]
            void onreadystatechange(IHTMLEventObj evtObj);

            [DispId(1030)]
            void onlayoutcomplete(IHTMLEventObj evtObj);

            [DispId(1031)]
            void onpage(IHTMLEventObj evtObj);

            [DispId(1042)]
            void onmouseenter(IHTMLEventObj evtObj);

            [DispId(1043)]
            void onmouseleave(IHTMLEventObj evtObj);

            [DispId(1044)]
            void onactivate(IHTMLEventObj evtObj);

            [DispId(1045)]
            void ondeactivate(IHTMLEventObj evtObj);

            [DispId(1034)]
            bool onbeforedeactivate(IHTMLEventObj evtObj);

            [DispId(1047)]
            bool onbeforeactivate(IHTMLEventObj evtObj);

            [DispId(1048)]
            void onfocusin(IHTMLEventObj evtObj);

            [DispId(1049)]
            void onfocusout(IHTMLEventObj evtObj);

            [DispId(1035)]
            void onmove(IHTMLEventObj evtObj);

            [DispId(1036)]
            bool oncontrolselect(IHTMLEventObj evtObj);

            [DispId(1038)]
            bool onmovestart(IHTMLEventObj evtObj);

            [DispId(1039)]
            void onmoveend(IHTMLEventObj evtObj);

            [DispId(1040)]
            bool onresizestart(IHTMLEventObj evtObj);

            [DispId(1041)]
            void onresizeend(IHTMLEventObj evtObj);

            [DispId(1033)]
            bool onmousewheel(IHTMLEventObj evtObj);

            [DispId(-2147412082)]
            bool onchange(IHTMLEventObj evtObj);

            [DispId(-2147412102)]
            void onselect(IHTMLEventObj evtObj);

            [DispId(1003)]
            void onload(IHTMLEventObj evtObj);

            [DispId(1002)]
            void onerror(IHTMLEventObj evtObj);

            [DispId(1000)]
            void onabort(IHTMLEventObj evtObj);
        }

        [ComImport]
        [Guid("3050f61b-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        [TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLInputImageEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(IHTMLEventObj evtObj);

            [DispId(-600)]
            bool onclick(IHTMLEventObj evtObj);

            [DispId(-601)]
            bool ondblclick(IHTMLEventObj evtObj);

            [DispId(-603)]
            bool onkeypress(IHTMLEventObj evtObj);

            [DispId(-602)]
            void onkeydown(IHTMLEventObj evtObj);

            [DispId(-604)]
            void onkeyup(IHTMLEventObj evtObj);

            [DispId(-2147418103)]
            void onmouseout(IHTMLEventObj evtObj);

            [DispId(-2147418104)]
            void onmouseover(IHTMLEventObj evtObj);

            [DispId(-606)]
            void onmousemove(IHTMLEventObj evtObj);

            [DispId(-605)]
            void onmousedown(IHTMLEventObj evtObj);

            [DispId(-607)]
            void onmouseup(IHTMLEventObj evtObj);

            [DispId(-2147418100)]
            bool onselectstart(IHTMLEventObj evtObj);

            [DispId(-2147418095)]
            void onfilterchange(IHTMLEventObj evtObj);

            [DispId(-2147418101)]
            bool ondragstart(IHTMLEventObj evtObj);

            [DispId(-2147418108)]
            bool onbeforeupdate(IHTMLEventObj evtObj);

            [DispId(-2147418107)]
            void onafterupdate(IHTMLEventObj evtObj);

            [DispId(-2147418099)]
            bool onerrorupdate(IHTMLEventObj evtObj);

            [DispId(-2147418106)]
            bool onrowexit(IHTMLEventObj evtObj);

            [DispId(-2147418105)]
            void onrowenter(IHTMLEventObj evtObj);

            [DispId(-2147418098)]
            void ondatasetchanged(IHTMLEventObj evtObj);

            [DispId(-2147418097)]
            void ondataavailable(IHTMLEventObj evtObj);

            [DispId(-2147418096)]
            void ondatasetcomplete(IHTMLEventObj evtObj);

            [DispId(-2147418094)]
            void onlosecapture(IHTMLEventObj evtObj);

            [DispId(-2147418093)]
            void onpropertychange(IHTMLEventObj evtObj);

            [DispId(1014)]
            void onscroll(IHTMLEventObj evtObj);

            [DispId(-2147418111)]
            void onfocus(IHTMLEventObj evtObj);

            [DispId(-2147418112)]
            void onblur(IHTMLEventObj evtObj);

            [DispId(1016)]
            void onresize(IHTMLEventObj evtObj);

            [DispId(-2147418092)]
            bool ondrag(IHTMLEventObj evtObj);

            [DispId(-2147418091)]
            void ondragend(IHTMLEventObj evtObj);

            [DispId(-2147418090)]
            bool ondragenter(IHTMLEventObj evtObj);

            [DispId(-2147418089)]
            bool ondragover(IHTMLEventObj evtObj);

            [DispId(-2147418088)]
            void ondragleave(IHTMLEventObj evtObj);

            [DispId(-2147418087)]
            bool ondrop(IHTMLEventObj evtObj);

            [DispId(-2147418083)]
            bool onbeforecut(IHTMLEventObj evtObj);

            [DispId(-2147418086)]
            bool oncut(IHTMLEventObj evtObj);

            [DispId(-2147418082)]
            bool onbeforecopy(IHTMLEventObj evtObj);

            [DispId(-2147418085)]
            bool oncopy(IHTMLEventObj evtObj);

            [DispId(-2147418081)]
            bool onbeforepaste(IHTMLEventObj evtObj);

            [DispId(-2147418084)]
            bool onpaste(IHTMLEventObj evtObj);

            [DispId(1023)]
            bool oncontextmenu(IHTMLEventObj evtObj);

            [DispId(-2147418080)]
            void onrowsdelete(IHTMLEventObj evtObj);

            [DispId(-2147418079)]
            void onrowsinserted(IHTMLEventObj evtObj);

            [DispId(-2147418078)]
            void oncellchange(IHTMLEventObj evtObj);

            [DispId(-609)]
            void onreadystatechange(IHTMLEventObj evtObj);

            [DispId(1030)]
            void onlayoutcomplete(IHTMLEventObj evtObj);

            [DispId(1031)]
            void onpage(IHTMLEventObj evtObj);

            [DispId(1042)]
            void onmouseenter(IHTMLEventObj evtObj);

            [DispId(1043)]
            void onmouseleave(IHTMLEventObj evtObj);

            [DispId(1044)]
            void onactivate(IHTMLEventObj evtObj);

            [DispId(1045)]
            void ondeactivate(IHTMLEventObj evtObj);

            [DispId(1034)]
            bool onbeforedeactivate(IHTMLEventObj evtObj);

            [DispId(1047)]
            bool onbeforeactivate(IHTMLEventObj evtObj);

            [DispId(1048)]
            void onfocusin(IHTMLEventObj evtObj);

            [DispId(1049)]
            void onfocusout(IHTMLEventObj evtObj);

            [DispId(1035)]
            void onmove(IHTMLEventObj evtObj);

            [DispId(1036)]
            bool oncontrolselect(IHTMLEventObj evtObj);

            [DispId(1038)]
            bool onmovestart(IHTMLEventObj evtObj);

            [DispId(1039)]
            void onmoveend(IHTMLEventObj evtObj);

            [DispId(1040)]
            bool onresizestart(IHTMLEventObj evtObj);

            [DispId(1041)]
            void onresizeend(IHTMLEventObj evtObj);

            [DispId(1033)]
            bool onmousewheel(IHTMLEventObj evtObj);

            [DispId(-2147412080)]
            void onload(IHTMLEventObj evtObj);

            [DispId(-2147412083)]
            void onerror(IHTMLEventObj evtObj);

            [DispId(-2147412084)]
            void onabort(IHTMLEventObj evtObj);
        }

        [ComImport]
        [Guid("3050f618-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        [TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLInputTextElementEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(IHTMLEventObj evtObj);

            [DispId(-600)]
            bool onclick(IHTMLEventObj evtObj);

            [DispId(-601)]
            bool ondblclick(IHTMLEventObj evtObj);

            [DispId(-603)]
            bool onkeypress(IHTMLEventObj evtObj);

            [DispId(-602)]
            void onkeydown(IHTMLEventObj evtObj);

            [DispId(-604)]
            void onkeyup(IHTMLEventObj evtObj);

            [DispId(-2147418103)]
            void onmouseout(IHTMLEventObj evtObj);

            [DispId(-2147418104)]
            void onmouseover(IHTMLEventObj evtObj);

            [DispId(-606)]
            void onmousemove(IHTMLEventObj evtObj);

            [DispId(-605)]
            void onmousedown(IHTMLEventObj evtObj);

            [DispId(-607)]
            void onmouseup(IHTMLEventObj evtObj);

            [DispId(-2147418100)]
            bool onselectstart(IHTMLEventObj evtObj);

            [DispId(-2147418095)]
            void onfilterchange(IHTMLEventObj evtObj);

            [DispId(-2147418101)]
            bool ondragstart(IHTMLEventObj evtObj);

            [DispId(-2147418108)]
            bool onbeforeupdate(IHTMLEventObj evtObj);

            [DispId(-2147418107)]
            void onafterupdate(IHTMLEventObj evtObj);

            [DispId(-2147418099)]
            bool onerrorupdate(IHTMLEventObj evtObj);

            [DispId(-2147418106)]
            bool onrowexit(IHTMLEventObj evtObj);

            [DispId(-2147418105)]
            void onrowenter(IHTMLEventObj evtObj);

            [DispId(-2147418098)]
            void ondatasetchanged(IHTMLEventObj evtObj);

            [DispId(-2147418097)]
            void ondataavailable(IHTMLEventObj evtObj);

            [DispId(-2147418096)]
            void ondatasetcomplete(IHTMLEventObj evtObj);

            [DispId(-2147418094)]
            void onlosecapture(IHTMLEventObj evtObj);

            [DispId(-2147418093)]
            void onpropertychange(IHTMLEventObj evtObj);

            [DispId(1014)]
            void onscroll(IHTMLEventObj evtObj);

            [DispId(-2147418111)]
            void onfocus(IHTMLEventObj evtObj);

            [DispId(-2147418112)]
            void onblur(IHTMLEventObj evtObj);

            [DispId(1016)]
            void onresize(IHTMLEventObj evtObj);

            [DispId(-2147418092)]
            bool ondrag(IHTMLEventObj evtObj);

            [DispId(-2147418091)]
            void ondragend(IHTMLEventObj evtObj);

            [DispId(-2147418090)]
            bool ondragenter(IHTMLEventObj evtObj);

            [DispId(-2147418089)]
            bool ondragover(IHTMLEventObj evtObj);

            [DispId(-2147418088)]
            void ondragleave(IHTMLEventObj evtObj);

            [DispId(-2147418087)]
            bool ondrop(IHTMLEventObj evtObj);

            [DispId(-2147418083)]
            bool onbeforecut(IHTMLEventObj evtObj);

            [DispId(-2147418086)]
            bool oncut(IHTMLEventObj evtObj);

            [DispId(-2147418082)]
            bool onbeforecopy(IHTMLEventObj evtObj);

            [DispId(-2147418085)]
            bool oncopy(IHTMLEventObj evtObj);

            [DispId(-2147418081)]
            bool onbeforepaste(IHTMLEventObj evtObj);

            [DispId(-2147418084)]
            bool onpaste(IHTMLEventObj evtObj);

            [DispId(1023)]
            bool oncontextmenu(IHTMLEventObj evtObj);

            [DispId(-2147418080)]
            void onrowsdelete(IHTMLEventObj evtObj);

            [DispId(-2147418079)]
            void onrowsinserted(IHTMLEventObj evtObj);

            [DispId(-2147418078)]
            void oncellchange(IHTMLEventObj evtObj);

            [DispId(-609)]
            void onreadystatechange(IHTMLEventObj evtObj);

            [DispId(1030)]
            void onlayoutcomplete(IHTMLEventObj evtObj);

            [DispId(1031)]
            void onpage(IHTMLEventObj evtObj);

            [DispId(1042)]
            void onmouseenter(IHTMLEventObj evtObj);

            [DispId(1043)]
            void onmouseleave(IHTMLEventObj evtObj);

            [DispId(1044)]
            void onactivate(IHTMLEventObj evtObj);

            [DispId(1045)]
            void ondeactivate(IHTMLEventObj evtObj);

            [DispId(1034)]
            bool onbeforedeactivate(IHTMLEventObj evtObj);

            [DispId(1047)]
            bool onbeforeactivate(IHTMLEventObj evtObj);

            [DispId(1048)]
            void onfocusin(IHTMLEventObj evtObj);

            [DispId(1049)]
            void onfocusout(IHTMLEventObj evtObj);

            [DispId(1035)]
            void onmove(IHTMLEventObj evtObj);

            [DispId(1036)]
            bool oncontrolselect(IHTMLEventObj evtObj);

            [DispId(1038)]
            bool onmovestart(IHTMLEventObj evtObj);

            [DispId(1039)]
            void onmoveend(IHTMLEventObj evtObj);

            [DispId(1040)]
            bool onresizestart(IHTMLEventObj evtObj);

            [DispId(1041)]
            void onresizeend(IHTMLEventObj evtObj);

            [DispId(1033)]
            bool onmousewheel(IHTMLEventObj evtObj);

            [DispId(1001)]
            bool onchange(IHTMLEventObj evtObj);

            [DispId(1006)]
            void onselect(IHTMLEventObj evtObj);

            [DispId(1003)]
            void onload(IHTMLEventObj evtObj);

            [DispId(1002)]
            void onerror(IHTMLEventObj evtObj);

            [DispId(1001)]
            void onabort(IHTMLEventObj evtObj);
        }

        [ComImport]
        [Guid("3050f61c-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        [TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLLabelEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(IHTMLEventObj evtObj);

            [DispId(-600)]
            bool onclick(IHTMLEventObj evtObj);

            [DispId(-601)]
            bool ondblclick(IHTMLEventObj evtObj);

            [DispId(-603)]
            bool onkeypress(IHTMLEventObj evtObj);

            [DispId(-602)]
            void onkeydown(IHTMLEventObj evtObj);

            [DispId(-604)]
            void onkeyup(IHTMLEventObj evtObj);

            [DispId(-2147418103)]
            void onmouseout(IHTMLEventObj evtObj);

            [DispId(-2147418104)]
            void onmouseover(IHTMLEventObj evtObj);

            [DispId(-606)]
            void onmousemove(IHTMLEventObj evtObj);

            [DispId(-605)]
            void onmousedown(IHTMLEventObj evtObj);

            [DispId(-607)]
            void onmouseup(IHTMLEventObj evtObj);

            [DispId(-2147418100)]
            bool onselectstart(IHTMLEventObj evtObj);

            [DispId(-2147418095)]
            void onfilterchange(IHTMLEventObj evtObj);

            [DispId(-2147418101)]
            bool ondragstart(IHTMLEventObj evtObj);

            [DispId(-2147418108)]
            bool onbeforeupdate(IHTMLEventObj evtObj);

            [DispId(-2147418107)]
            void onafterupdate(IHTMLEventObj evtObj);

            [DispId(-2147418099)]
            bool onerrorupdate(IHTMLEventObj evtObj);

            [DispId(-2147418106)]
            bool onrowexit(IHTMLEventObj evtObj);

            [DispId(-2147418105)]
            void onrowenter(IHTMLEventObj evtObj);

            [DispId(-2147418098)]
            void ondatasetchanged(IHTMLEventObj evtObj);

            [DispId(-2147418097)]
            void ondataavailable(IHTMLEventObj evtObj);

            [DispId(-2147418096)]
            void ondatasetcomplete(IHTMLEventObj evtObj);

            [DispId(-2147418094)]
            void onlosecapture(IHTMLEventObj evtObj);

            [DispId(-2147418093)]
            void onpropertychange(IHTMLEventObj evtObj);

            [DispId(1014)]
            void onscroll(IHTMLEventObj evtObj);

            [DispId(-2147418111)]
            void onfocus(IHTMLEventObj evtObj);

            [DispId(-2147418112)]
            void onblur(IHTMLEventObj evtObj);

            [DispId(1016)]
            void onresize(IHTMLEventObj evtObj);

            [DispId(-2147418092)]
            bool ondrag(IHTMLEventObj evtObj);

            [DispId(-2147418091)]
            void ondragend(IHTMLEventObj evtObj);

            [DispId(-2147418090)]
            bool ondragenter(IHTMLEventObj evtObj);

            [DispId(-2147418089)]
            bool ondragover(IHTMLEventObj evtObj);

            [DispId(-2147418088)]
            void ondragleave(IHTMLEventObj evtObj);

            [DispId(-2147418087)]
            bool ondrop(IHTMLEventObj evtObj);

            [DispId(-2147418083)]
            bool onbeforecut(IHTMLEventObj evtObj);

            [DispId(-2147418086)]
            bool oncut(IHTMLEventObj evtObj);

            [DispId(-2147418082)]
            bool onbeforecopy(IHTMLEventObj evtObj);

            [DispId(-2147418085)]
            bool oncopy(IHTMLEventObj evtObj);

            [DispId(-2147418081)]
            bool onbeforepaste(IHTMLEventObj evtObj);

            [DispId(-2147418084)]
            bool onpaste(IHTMLEventObj evtObj);

            [DispId(1023)]
            bool oncontextmenu(IHTMLEventObj evtObj);

            [DispId(-2147418080)]
            void onrowsdelete(IHTMLEventObj evtObj);

            [DispId(-2147418079)]
            void onrowsinserted(IHTMLEventObj evtObj);

            [DispId(-2147418078)]
            void oncellchange(IHTMLEventObj evtObj);

            [DispId(-609)]
            void onreadystatechange(IHTMLEventObj evtObj);

            [DispId(1030)]
            void onlayoutcomplete(IHTMLEventObj evtObj);

            [DispId(1031)]
            void onpage(IHTMLEventObj evtObj);

            [DispId(1042)]
            void onmouseenter(IHTMLEventObj evtObj);

            [DispId(1043)]
            void onmouseleave(IHTMLEventObj evtObj);

            [DispId(1044)]
            void onactivate(IHTMLEventObj evtObj);

            [DispId(1045)]
            void ondeactivate(IHTMLEventObj evtObj);

            [DispId(1034)]
            bool onbeforedeactivate(IHTMLEventObj evtObj);

            [DispId(1047)]
            bool onbeforeactivate(IHTMLEventObj evtObj);

            [DispId(1048)]
            void onfocusin(IHTMLEventObj evtObj);

            [DispId(1049)]
            void onfocusout(IHTMLEventObj evtObj);

            [DispId(1035)]
            void onmove(IHTMLEventObj evtObj);

            [DispId(1036)]
            bool oncontrolselect(IHTMLEventObj evtObj);

            [DispId(1038)]
            bool onmovestart(IHTMLEventObj evtObj);

            [DispId(1039)]
            void onmoveend(IHTMLEventObj evtObj);

            [DispId(1040)]
            bool onresizestart(IHTMLEventObj evtObj);

            [DispId(1041)]
            void onresizeend(IHTMLEventObj evtObj);

            [DispId(1033)]
            bool onmousewheel(IHTMLEventObj evtObj);
        }

        [ComImport]
        [Guid("3050f61d-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        [TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLLinkElementEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(IHTMLEventObj evtObj);

            [DispId(-600)]
            bool onclick(IHTMLEventObj evtObj);

            [DispId(-601)]
            bool ondblclick(IHTMLEventObj evtObj);

            [DispId(-603)]
            bool onkeypress(IHTMLEventObj evtObj);

            [DispId(-602)]
            void onkeydown(IHTMLEventObj evtObj);

            [DispId(-604)]
            void onkeyup(IHTMLEventObj evtObj);

            [DispId(-2147418103)]
            void onmouseout(IHTMLEventObj evtObj);

            [DispId(-2147418104)]
            void onmouseover(IHTMLEventObj evtObj);

            [DispId(-606)]
            void onmousemove(IHTMLEventObj evtObj);

            [DispId(-605)]
            void onmousedown(IHTMLEventObj evtObj);

            [DispId(-607)]
            void onmouseup(IHTMLEventObj evtObj);

            [DispId(-2147418100)]
            bool onselectstart(IHTMLEventObj evtObj);

            [DispId(-2147418095)]
            void onfilterchange(IHTMLEventObj evtObj);

            [DispId(-2147418101)]
            bool ondragstart(IHTMLEventObj evtObj);

            [DispId(-2147418108)]
            bool onbeforeupdate(IHTMLEventObj evtObj);

            [DispId(-2147418107)]
            void onafterupdate(IHTMLEventObj evtObj);

            [DispId(-2147418099)]
            bool onerrorupdate(IHTMLEventObj evtObj);

            [DispId(-2147418106)]
            bool onrowexit(IHTMLEventObj evtObj);

            [DispId(-2147418105)]
            void onrowenter(IHTMLEventObj evtObj);

            [DispId(-2147418098)]
            void ondatasetchanged(IHTMLEventObj evtObj);

            [DispId(-2147418097)]
            void ondataavailable(IHTMLEventObj evtObj);

            [DispId(-2147418096)]
            void ondatasetcomplete(IHTMLEventObj evtObj);

            [DispId(-2147418094)]
            void onlosecapture(IHTMLEventObj evtObj);

            [DispId(-2147418093)]
            void onpropertychange(IHTMLEventObj evtObj);

            [DispId(1014)]
            void onscroll(IHTMLEventObj evtObj);

            [DispId(-2147418111)]
            void onfocus(IHTMLEventObj evtObj);

            [DispId(-2147418112)]
            void onblur(IHTMLEventObj evtObj);

            [DispId(1016)]
            void onresize(IHTMLEventObj evtObj);

            [DispId(-2147418092)]
            bool ondrag(IHTMLEventObj evtObj);

            [DispId(-2147418091)]
            void ondragend(IHTMLEventObj evtObj);

            [DispId(-2147418090)]
            bool ondragenter(IHTMLEventObj evtObj);

            [DispId(-2147418089)]
            bool ondragover(IHTMLEventObj evtObj);

            [DispId(-2147418088)]
            void ondragleave(IHTMLEventObj evtObj);

            [DispId(-2147418087)]
            bool ondrop(IHTMLEventObj evtObj);

            [DispId(-2147418083)]
            bool onbeforecut(IHTMLEventObj evtObj);

            [DispId(-2147418086)]
            bool oncut(IHTMLEventObj evtObj);

            [DispId(-2147418082)]
            bool onbeforecopy(IHTMLEventObj evtObj);

            [DispId(-2147418085)]
            bool oncopy(IHTMLEventObj evtObj);

            [DispId(-2147418081)]
            bool onbeforepaste(IHTMLEventObj evtObj);

            [DispId(-2147418084)]
            bool onpaste(IHTMLEventObj evtObj);

            [DispId(1023)]
            bool oncontextmenu(IHTMLEventObj evtObj);

            [DispId(-2147418080)]
            void onrowsdelete(IHTMLEventObj evtObj);

            [DispId(-2147418079)]
            void onrowsinserted(IHTMLEventObj evtObj);

            [DispId(-2147418078)]
            void oncellchange(IHTMLEventObj evtObj);

            [DispId(-609)]
            void onreadystatechange(IHTMLEventObj evtObj);

            [DispId(1030)]
            void onlayoutcomplete(IHTMLEventObj evtObj);

            [DispId(1031)]
            void onpage(IHTMLEventObj evtObj);

            [DispId(1042)]
            void onmouseenter(IHTMLEventObj evtObj);

            [DispId(1043)]
            void onmouseleave(IHTMLEventObj evtObj);

            [DispId(1044)]
            void onactivate(IHTMLEventObj evtObj);

            [DispId(1045)]
            void ondeactivate(IHTMLEventObj evtObj);

            [DispId(1034)]
            bool onbeforedeactivate(IHTMLEventObj evtObj);

            [DispId(1047)]
            bool onbeforeactivate(IHTMLEventObj evtObj);

            [DispId(1048)]
            void onfocusin(IHTMLEventObj evtObj);

            [DispId(1049)]
            void onfocusout(IHTMLEventObj evtObj);

            [DispId(1035)]
            void onmove(IHTMLEventObj evtObj);

            [DispId(1036)]
            bool oncontrolselect(IHTMLEventObj evtObj);

            [DispId(1038)]
            bool onmovestart(IHTMLEventObj evtObj);

            [DispId(1039)]
            void onmoveend(IHTMLEventObj evtObj);

            [DispId(1040)]
            bool onresizestart(IHTMLEventObj evtObj);

            [DispId(1041)]
            void onresizeend(IHTMLEventObj evtObj);

            [DispId(1033)]
            bool onmousewheel(IHTMLEventObj evtObj);

            [DispId(-2147412080)]
            void onload(IHTMLEventObj evtObj);

            [DispId(-2147412083)]
            void onerror(IHTMLEventObj evtObj);
        }

        [ComImport]
        [Guid("3050f61e-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        [TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLMapEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(IHTMLEventObj evtObj);

            [DispId(-600)]
            bool onclick(IHTMLEventObj evtObj);

            [DispId(-601)]
            bool ondblclick(IHTMLEventObj evtObj);

            [DispId(-603)]
            bool onkeypress(IHTMLEventObj evtObj);

            [DispId(-602)]
            void onkeydown(IHTMLEventObj evtObj);

            [DispId(-604)]
            void onkeyup(IHTMLEventObj evtObj);

            [DispId(-2147418103)]
            void onmouseout(IHTMLEventObj evtObj);

            [DispId(-2147418104)]
            void onmouseover(IHTMLEventObj evtObj);

            [DispId(-606)]
            void onmousemove(IHTMLEventObj evtObj);

            [DispId(-605)]
            void onmousedown(IHTMLEventObj evtObj);

            [DispId(-607)]
            void onmouseup(IHTMLEventObj evtObj);

            [DispId(-2147418100)]
            bool onselectstart(IHTMLEventObj evtObj);

            [DispId(-2147418095)]
            void onfilterchange(IHTMLEventObj evtObj);

            [DispId(-2147418101)]
            bool ondragstart(IHTMLEventObj evtObj);

            [DispId(-2147418108)]
            bool onbeforeupdate(IHTMLEventObj evtObj);

            [DispId(-2147418107)]
            void onafterupdate(IHTMLEventObj evtObj);

            [DispId(-2147418099)]
            bool onerrorupdate(IHTMLEventObj evtObj);

            [DispId(-2147418106)]
            bool onrowexit(IHTMLEventObj evtObj);

            [DispId(-2147418105)]
            void onrowenter(IHTMLEventObj evtObj);

            [DispId(-2147418098)]
            void ondatasetchanged(IHTMLEventObj evtObj);

            [DispId(-2147418097)]
            void ondataavailable(IHTMLEventObj evtObj);

            [DispId(-2147418096)]
            void ondatasetcomplete(IHTMLEventObj evtObj);

            [DispId(-2147418094)]
            void onlosecapture(IHTMLEventObj evtObj);

            [DispId(-2147418093)]
            void onpropertychange(IHTMLEventObj evtObj);

            [DispId(1014)]
            void onscroll(IHTMLEventObj evtObj);

            [DispId(-2147418111)]
            void onfocus(IHTMLEventObj evtObj);

            [DispId(-2147418112)]
            void onblur(IHTMLEventObj evtObj);

            [DispId(1016)]
            void onresize(IHTMLEventObj evtObj);

            [DispId(-2147418092)]
            bool ondrag(IHTMLEventObj evtObj);

            [DispId(-2147418091)]
            void ondragend(IHTMLEventObj evtObj);

            [DispId(-2147418090)]
            bool ondragenter(IHTMLEventObj evtObj);

            [DispId(-2147418089)]
            bool ondragover(IHTMLEventObj evtObj);

            [DispId(-2147418088)]
            void ondragleave(IHTMLEventObj evtObj);

            [DispId(-2147418087)]
            bool ondrop(IHTMLEventObj evtObj);

            [DispId(-2147418083)]
            bool onbeforecut(IHTMLEventObj evtObj);

            [DispId(-2147418086)]
            bool oncut(IHTMLEventObj evtObj);

            [DispId(-2147418082)]
            bool onbeforecopy(IHTMLEventObj evtObj);

            [DispId(-2147418085)]
            bool oncopy(IHTMLEventObj evtObj);

            [DispId(-2147418081)]
            bool onbeforepaste(IHTMLEventObj evtObj);

            [DispId(-2147418084)]
            bool onpaste(IHTMLEventObj evtObj);

            [DispId(1023)]
            bool oncontextmenu(IHTMLEventObj evtObj);

            [DispId(-2147418080)]
            void onrowsdelete(IHTMLEventObj evtObj);

            [DispId(-2147418079)]
            void onrowsinserted(IHTMLEventObj evtObj);

            [DispId(-2147418078)]
            void oncellchange(IHTMLEventObj evtObj);

            [DispId(-609)]
            void onreadystatechange(IHTMLEventObj evtObj);

            [DispId(1030)]
            void onlayoutcomplete(IHTMLEventObj evtObj);

            [DispId(1031)]
            void onpage(IHTMLEventObj evtObj);

            [DispId(1042)]
            void onmouseenter(IHTMLEventObj evtObj);

            [DispId(1043)]
            void onmouseleave(IHTMLEventObj evtObj);

            [DispId(1044)]
            void onactivate(IHTMLEventObj evtObj);

            [DispId(1045)]
            void ondeactivate(IHTMLEventObj evtObj);

            [DispId(1034)]
            bool onbeforedeactivate(IHTMLEventObj evtObj);

            [DispId(1047)]
            bool onbeforeactivate(IHTMLEventObj evtObj);

            [DispId(1048)]
            void onfocusin(IHTMLEventObj evtObj);

            [DispId(1049)]
            void onfocusout(IHTMLEventObj evtObj);

            [DispId(1035)]
            void onmove(IHTMLEventObj evtObj);

            [DispId(1036)]
            bool oncontrolselect(IHTMLEventObj evtObj);

            [DispId(1038)]
            bool onmovestart(IHTMLEventObj evtObj);

            [DispId(1039)]
            void onmoveend(IHTMLEventObj evtObj);

            [DispId(1040)]
            bool onresizestart(IHTMLEventObj evtObj);

            [DispId(1041)]
            void onresizeend(IHTMLEventObj evtObj);

            [DispId(1033)]
            bool onmousewheel(IHTMLEventObj evtObj);
        }

        [ComImport]
        [Guid("3050f61f-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        [TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLMarqueeElementEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(IHTMLEventObj evtObj);

            [DispId(-600)]
            bool onclick(IHTMLEventObj evtObj);

            [DispId(-601)]
            bool ondblclick(IHTMLEventObj evtObj);

            [DispId(-603)]
            bool onkeypress(IHTMLEventObj evtObj);

            [DispId(-602)]
            void onkeydown(IHTMLEventObj evtObj);

            [DispId(-604)]
            void onkeyup(IHTMLEventObj evtObj);

            [DispId(-2147418103)]
            void onmouseout(IHTMLEventObj evtObj);

            [DispId(-2147418104)]
            void onmouseover(IHTMLEventObj evtObj);

            [DispId(-606)]
            void onmousemove(IHTMLEventObj evtObj);

            [DispId(-605)]
            void onmousedown(IHTMLEventObj evtObj);

            [DispId(-607)]
            void onmouseup(IHTMLEventObj evtObj);

            [DispId(-2147418100)]
            bool onselectstart(IHTMLEventObj evtObj);

            [DispId(-2147418095)]
            void onfilterchange(IHTMLEventObj evtObj);

            [DispId(-2147418101)]
            bool ondragstart(IHTMLEventObj evtObj);

            [DispId(-2147418108)]
            bool onbeforeupdate(IHTMLEventObj evtObj);

            [DispId(-2147418107)]
            void onafterupdate(IHTMLEventObj evtObj);

            [DispId(-2147418099)]
            bool onerrorupdate(IHTMLEventObj evtObj);

            [DispId(-2147418106)]
            bool onrowexit(IHTMLEventObj evtObj);

            [DispId(-2147418105)]
            void onrowenter(IHTMLEventObj evtObj);

            [DispId(-2147418098)]
            void ondatasetchanged(IHTMLEventObj evtObj);

            [DispId(-2147418097)]
            void ondataavailable(IHTMLEventObj evtObj);

            [DispId(-2147418096)]
            void ondatasetcomplete(IHTMLEventObj evtObj);

            [DispId(-2147418094)]
            void onlosecapture(IHTMLEventObj evtObj);

            [DispId(-2147418093)]
            void onpropertychange(IHTMLEventObj evtObj);

            [DispId(1014)]
            void onscroll(IHTMLEventObj evtObj);

            [DispId(-2147418111)]
            void onfocus(IHTMLEventObj evtObj);

            [DispId(-2147418112)]
            void onblur(IHTMLEventObj evtObj);

            [DispId(1016)]
            void onresize(IHTMLEventObj evtObj);

            [DispId(-2147418092)]
            bool ondrag(IHTMLEventObj evtObj);

            [DispId(-2147418091)]
            void ondragend(IHTMLEventObj evtObj);

            [DispId(-2147418090)]
            bool ondragenter(IHTMLEventObj evtObj);

            [DispId(-2147418089)]
            bool ondragover(IHTMLEventObj evtObj);

            [DispId(-2147418088)]
            void ondragleave(IHTMLEventObj evtObj);

            [DispId(-2147418087)]
            bool ondrop(IHTMLEventObj evtObj);

            [DispId(-2147418083)]
            bool onbeforecut(IHTMLEventObj evtObj);

            [DispId(-2147418086)]
            bool oncut(IHTMLEventObj evtObj);

            [DispId(-2147418082)]
            bool onbeforecopy(IHTMLEventObj evtObj);

            [DispId(-2147418085)]
            bool oncopy(IHTMLEventObj evtObj);

            [DispId(-2147418081)]
            bool onbeforepaste(IHTMLEventObj evtObj);

            [DispId(-2147418084)]
            bool onpaste(IHTMLEventObj evtObj);

            [DispId(1023)]
            bool oncontextmenu(IHTMLEventObj evtObj);

            [DispId(-2147418080)]
            void onrowsdelete(IHTMLEventObj evtObj);

            [DispId(-2147418079)]
            void onrowsinserted(IHTMLEventObj evtObj);

            [DispId(-2147418078)]
            void oncellchange(IHTMLEventObj evtObj);

            [DispId(-609)]
            void onreadystatechange(IHTMLEventObj evtObj);

            [DispId(1030)]
            void onlayoutcomplete(IHTMLEventObj evtObj);

            [DispId(1031)]
            void onpage(IHTMLEventObj evtObj);

            [DispId(1042)]
            void onmouseenter(IHTMLEventObj evtObj);

            [DispId(1043)]
            void onmouseleave(IHTMLEventObj evtObj);

            [DispId(1044)]
            void onactivate(IHTMLEventObj evtObj);

            [DispId(1045)]
            void ondeactivate(IHTMLEventObj evtObj);

            [DispId(1034)]
            bool onbeforedeactivate(IHTMLEventObj evtObj);

            [DispId(1047)]
            bool onbeforeactivate(IHTMLEventObj evtObj);

            [DispId(1048)]
            void onfocusin(IHTMLEventObj evtObj);

            [DispId(1049)]
            void onfocusout(IHTMLEventObj evtObj);

            [DispId(1035)]
            void onmove(IHTMLEventObj evtObj);

            [DispId(1036)]
            bool oncontrolselect(IHTMLEventObj evtObj);

            [DispId(1038)]
            bool onmovestart(IHTMLEventObj evtObj);

            [DispId(1039)]
            void onmoveend(IHTMLEventObj evtObj);

            [DispId(1040)]
            bool onresizestart(IHTMLEventObj evtObj);

            [DispId(1041)]
            void onresizeend(IHTMLEventObj evtObj);

            [DispId(1033)]
            bool onmousewheel(IHTMLEventObj evtObj);

            [DispId(-2147412092)]
            void onbounce(IHTMLEventObj evtObj);

            [DispId(-2147412086)]
            void onfinish(IHTMLEventObj evtObj);

            [DispId(-2147412085)]
            void onstart(IHTMLEventObj evtObj);
        }

        [ComImport]
        [Guid("3050f619-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        [TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLOptionButtonElementEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(IHTMLEventObj evtObj);

            [DispId(-600)]
            bool onclick(IHTMLEventObj evtObj);

            [DispId(-601)]
            bool ondblclick(IHTMLEventObj evtObj);

            [DispId(-603)]
            bool onkeypress(IHTMLEventObj evtObj);

            [DispId(-602)]
            void onkeydown(IHTMLEventObj evtObj);

            [DispId(-604)]
            void onkeyup(IHTMLEventObj evtObj);

            [DispId(-2147418103)]
            void onmouseout(IHTMLEventObj evtObj);

            [DispId(-2147418104)]
            void onmouseover(IHTMLEventObj evtObj);

            [DispId(-606)]
            void onmousemove(IHTMLEventObj evtObj);

            [DispId(-605)]
            void onmousedown(IHTMLEventObj evtObj);

            [DispId(-607)]
            void onmouseup(IHTMLEventObj evtObj);

            [DispId(-2147418100)]
            bool onselectstart(IHTMLEventObj evtObj);

            [DispId(-2147418095)]
            void onfilterchange(IHTMLEventObj evtObj);

            [DispId(-2147418101)]
            bool ondragstart(IHTMLEventObj evtObj);

            [DispId(-2147418108)]
            bool onbeforeupdate(IHTMLEventObj evtObj);

            [DispId(-2147418107)]
            void onafterupdate(IHTMLEventObj evtObj);

            [DispId(-2147418099)]
            bool onerrorupdate(IHTMLEventObj evtObj);

            [DispId(-2147418106)]
            bool onrowexit(IHTMLEventObj evtObj);

            [DispId(-2147418105)]
            void onrowenter(IHTMLEventObj evtObj);

            [DispId(-2147418098)]
            void ondatasetchanged(IHTMLEventObj evtObj);

            [DispId(-2147418097)]
            void ondataavailable(IHTMLEventObj evtObj);

            [DispId(-2147418096)]
            void ondatasetcomplete(IHTMLEventObj evtObj);

            [DispId(-2147418094)]
            void onlosecapture(IHTMLEventObj evtObj);

            [DispId(-2147418093)]
            void onpropertychange(IHTMLEventObj evtObj);

            [DispId(1014)]
            void onscroll(IHTMLEventObj evtObj);

            [DispId(-2147418111)]
            void onfocus(IHTMLEventObj evtObj);

            [DispId(-2147418112)]
            void onblur(IHTMLEventObj evtObj);

            [DispId(1016)]
            void onresize(IHTMLEventObj evtObj);

            [DispId(-2147418092)]
            bool ondrag(IHTMLEventObj evtObj);

            [DispId(-2147418091)]
            void ondragend(IHTMLEventObj evtObj);

            [DispId(-2147418090)]
            bool ondragenter(IHTMLEventObj evtObj);

            [DispId(-2147418089)]
            bool ondragover(IHTMLEventObj evtObj);

            [DispId(-2147418088)]
            void ondragleave(IHTMLEventObj evtObj);

            [DispId(-2147418087)]
            bool ondrop(IHTMLEventObj evtObj);

            [DispId(-2147418083)]
            bool onbeforecut(IHTMLEventObj evtObj);

            [DispId(-2147418086)]
            bool oncut(IHTMLEventObj evtObj);

            [DispId(-2147418082)]
            bool onbeforecopy(IHTMLEventObj evtObj);

            [DispId(-2147418085)]
            bool oncopy(IHTMLEventObj evtObj);

            [DispId(-2147418081)]
            bool onbeforepaste(IHTMLEventObj evtObj);

            [DispId(-2147418084)]
            bool onpaste(IHTMLEventObj evtObj);

            [DispId(1023)]
            bool oncontextmenu(IHTMLEventObj evtObj);

            [DispId(-2147418080)]
            void onrowsdelete(IHTMLEventObj evtObj);

            [DispId(-2147418079)]
            void onrowsinserted(IHTMLEventObj evtObj);

            [DispId(-2147418078)]
            void oncellchange(IHTMLEventObj evtObj);

            [DispId(-609)]
            void onreadystatechange(IHTMLEventObj evtObj);

            [DispId(1030)]
            void onlayoutcomplete(IHTMLEventObj evtObj);

            [DispId(1031)]
            void onpage(IHTMLEventObj evtObj);

            [DispId(1042)]
            void onmouseenter(IHTMLEventObj evtObj);

            [DispId(1043)]
            void onmouseleave(IHTMLEventObj evtObj);

            [DispId(1044)]
            void onactivate(IHTMLEventObj evtObj);

            [DispId(1045)]
            void ondeactivate(IHTMLEventObj evtObj);

            [DispId(1034)]
            bool onbeforedeactivate(IHTMLEventObj evtObj);

            [DispId(1047)]
            bool onbeforeactivate(IHTMLEventObj evtObj);

            [DispId(1048)]
            void onfocusin(IHTMLEventObj evtObj);

            [DispId(1049)]
            void onfocusout(IHTMLEventObj evtObj);

            [DispId(1035)]
            void onmove(IHTMLEventObj evtObj);

            [DispId(1036)]
            bool oncontrolselect(IHTMLEventObj evtObj);

            [DispId(1038)]
            bool onmovestart(IHTMLEventObj evtObj);

            [DispId(1039)]
            void onmoveend(IHTMLEventObj evtObj);

            [DispId(1040)]
            bool onresizestart(IHTMLEventObj evtObj);

            [DispId(1041)]
            void onresizeend(IHTMLEventObj evtObj);

            [DispId(1033)]
            bool onmousewheel(IHTMLEventObj evtObj);

            [DispId(-2147412082)]
            bool onchange(IHTMLEventObj evtObj);
        }

        [ComImport]
        [Guid("3050f622-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        [TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLSelectElementEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(IHTMLEventObj evtObj);

            [DispId(-600)]
            bool onclick(IHTMLEventObj evtObj);

            [DispId(-601)]
            bool ondblclick(IHTMLEventObj evtObj);

            [DispId(-603)]
            bool onkeypress(IHTMLEventObj evtObj);

            [DispId(-602)]
            void onkeydown(IHTMLEventObj evtObj);

            [DispId(-604)]
            void onkeyup(IHTMLEventObj evtObj);

            [DispId(-2147418103)]
            void onmouseout(IHTMLEventObj evtObj);

            [DispId(-2147418104)]
            void onmouseover(IHTMLEventObj evtObj);

            [DispId(-606)]
            void onmousemove(IHTMLEventObj evtObj);

            [DispId(-605)]
            void onmousedown(IHTMLEventObj evtObj);

            [DispId(-607)]
            void onmouseup(IHTMLEventObj evtObj);

            [DispId(-2147418100)]
            bool onselectstart(IHTMLEventObj evtObj);

            [DispId(-2147418095)]
            void onfilterchange(IHTMLEventObj evtObj);

            [DispId(-2147418101)]
            bool ondragstart(IHTMLEventObj evtObj);

            [DispId(-2147418108)]
            bool onbeforeupdate(IHTMLEventObj evtObj);

            [DispId(-2147418107)]
            void onafterupdate(IHTMLEventObj evtObj);

            [DispId(-2147418099)]
            bool onerrorupdate(IHTMLEventObj evtObj);

            [DispId(-2147418106)]
            bool onrowexit(IHTMLEventObj evtObj);

            [DispId(-2147418105)]
            void onrowenter(IHTMLEventObj evtObj);

            [DispId(-2147418098)]
            void ondatasetchanged(IHTMLEventObj evtObj);

            [DispId(-2147418097)]
            void ondataavailable(IHTMLEventObj evtObj);

            [DispId(-2147418096)]
            void ondatasetcomplete(IHTMLEventObj evtObj);

            [DispId(-2147418094)]
            void onlosecapture(IHTMLEventObj evtObj);

            [DispId(-2147418093)]
            void onpropertychange(IHTMLEventObj evtObj);

            [DispId(1014)]
            void onscroll(IHTMLEventObj evtObj);

            [DispId(-2147418111)]
            void onfocus(IHTMLEventObj evtObj);

            [DispId(-2147418112)]
            void onblur(IHTMLEventObj evtObj);

            [DispId(1016)]
            void onresize(IHTMLEventObj evtObj);

            [DispId(-2147418092)]
            bool ondrag(IHTMLEventObj evtObj);

            [DispId(-2147418091)]
            void ondragend(IHTMLEventObj evtObj);

            [DispId(-2147418090)]
            bool ondragenter(IHTMLEventObj evtObj);

            [DispId(-2147418089)]
            bool ondragover(IHTMLEventObj evtObj);

            [DispId(-2147418088)]
            void ondragleave(IHTMLEventObj evtObj);

            [DispId(-2147418087)]
            bool ondrop(IHTMLEventObj evtObj);

            [DispId(-2147418083)]
            bool onbeforecut(IHTMLEventObj evtObj);

            [DispId(-2147418086)]
            bool oncut(IHTMLEventObj evtObj);

            [DispId(-2147418082)]
            bool onbeforecopy(IHTMLEventObj evtObj);

            [DispId(-2147418085)]
            bool oncopy(IHTMLEventObj evtObj);

            [DispId(-2147418081)]
            bool onbeforepaste(IHTMLEventObj evtObj);

            [DispId(-2147418084)]
            bool onpaste(IHTMLEventObj evtObj);

            [DispId(1023)]
            bool oncontextmenu(IHTMLEventObj evtObj);

            [DispId(-2147418080)]
            void onrowsdelete(IHTMLEventObj evtObj);

            [DispId(-2147418079)]
            void onrowsinserted(IHTMLEventObj evtObj);

            [DispId(-2147418078)]
            void oncellchange(IHTMLEventObj evtObj);

            [DispId(-609)]
            void onreadystatechange(IHTMLEventObj evtObj);

            [DispId(1030)]
            void onlayoutcomplete(IHTMLEventObj evtObj);

            [DispId(1031)]
            void onpage(IHTMLEventObj evtObj);

            [DispId(1042)]
            void onmouseenter(IHTMLEventObj evtObj);

            [DispId(1043)]
            void onmouseleave(IHTMLEventObj evtObj);

            [DispId(1044)]
            void onactivate(IHTMLEventObj evtObj);

            [DispId(1045)]
            void ondeactivate(IHTMLEventObj evtObj);

            [DispId(1034)]
            bool onbeforedeactivate(IHTMLEventObj evtObj);

            [DispId(1047)]
            bool onbeforeactivate(IHTMLEventObj evtObj);

            [DispId(1048)]
            void onfocusin(IHTMLEventObj evtObj);

            [DispId(1049)]
            void onfocusout(IHTMLEventObj evtObj);

            [DispId(1035)]
            void onmove(IHTMLEventObj evtObj);

            [DispId(1036)]
            bool oncontrolselect(IHTMLEventObj evtObj);

            [DispId(1038)]
            bool onmovestart(IHTMLEventObj evtObj);

            [DispId(1039)]
            void onmoveend(IHTMLEventObj evtObj);

            [DispId(1040)]
            bool onresizestart(IHTMLEventObj evtObj);

            [DispId(1041)]
            void onresizeend(IHTMLEventObj evtObj);

            [DispId(1033)]
            bool onmousewheel(IHTMLEventObj evtObj);

            [DispId(-2147412082)]
            void onchange_void(IHTMLEventObj evtObj);
        }

        [ComImport]
        [Guid("3050f615-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        [TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLStyleElementEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(IHTMLEventObj evtObj);

            [DispId(-600)]
            bool onclick(IHTMLEventObj evtObj);

            [DispId(-601)]
            bool ondblclick(IHTMLEventObj evtObj);

            [DispId(-603)]
            bool onkeypress(IHTMLEventObj evtObj);

            [DispId(-602)]
            void onkeydown(IHTMLEventObj evtObj);

            [DispId(-604)]
            void onkeyup(IHTMLEventObj evtObj);

            [DispId(-2147418103)]
            void onmouseout(IHTMLEventObj evtObj);

            [DispId(-2147418104)]
            void onmouseover(IHTMLEventObj evtObj);

            [DispId(-606)]
            void onmousemove(IHTMLEventObj evtObj);

            [DispId(-605)]
            void onmousedown(IHTMLEventObj evtObj);

            [DispId(-607)]
            void onmouseup(IHTMLEventObj evtObj);

            [DispId(-2147418100)]
            bool onselectstart(IHTMLEventObj evtObj);

            [DispId(-2147418095)]
            void onfilterchange(IHTMLEventObj evtObj);

            [DispId(-2147418101)]
            bool ondragstart(IHTMLEventObj evtObj);

            [DispId(-2147418108)]
            bool onbeforeupdate(IHTMLEventObj evtObj);

            [DispId(-2147418107)]
            void onafterupdate(IHTMLEventObj evtObj);

            [DispId(-2147418099)]
            bool onerrorupdate(IHTMLEventObj evtObj);

            [DispId(-2147418106)]
            bool onrowexit(IHTMLEventObj evtObj);

            [DispId(-2147418105)]
            void onrowenter(IHTMLEventObj evtObj);

            [DispId(-2147418098)]
            void ondatasetchanged(IHTMLEventObj evtObj);

            [DispId(-2147418097)]
            void ondataavailable(IHTMLEventObj evtObj);

            [DispId(-2147418096)]
            void ondatasetcomplete(IHTMLEventObj evtObj);

            [DispId(-2147418094)]
            void onlosecapture(IHTMLEventObj evtObj);

            [DispId(-2147418093)]
            void onpropertychange(IHTMLEventObj evtObj);

            [DispId(1014)]
            void onscroll(IHTMLEventObj evtObj);

            [DispId(-2147418111)]
            void onfocus(IHTMLEventObj evtObj);

            [DispId(-2147418112)]
            void onblur(IHTMLEventObj evtObj);

            [DispId(1016)]
            void onresize(IHTMLEventObj evtObj);

            [DispId(-2147418092)]
            bool ondrag(IHTMLEventObj evtObj);

            [DispId(-2147418091)]
            void ondragend(IHTMLEventObj evtObj);

            [DispId(-2147418090)]
            bool ondragenter(IHTMLEventObj evtObj);

            [DispId(-2147418089)]
            bool ondragover(IHTMLEventObj evtObj);

            [DispId(-2147418088)]
            void ondragleave(IHTMLEventObj evtObj);

            [DispId(-2147418087)]
            bool ondrop(IHTMLEventObj evtObj);

            [DispId(-2147418083)]
            bool onbeforecut(IHTMLEventObj evtObj);

            [DispId(-2147418086)]
            bool oncut(IHTMLEventObj evtObj);

            [DispId(-2147418082)]
            bool onbeforecopy(IHTMLEventObj evtObj);

            [DispId(-2147418085)]
            bool oncopy(IHTMLEventObj evtObj);

            [DispId(-2147418081)]
            bool onbeforepaste(IHTMLEventObj evtObj);

            [DispId(-2147418084)]
            bool onpaste(IHTMLEventObj evtObj);

            [DispId(1023)]
            bool oncontextmenu(IHTMLEventObj evtObj);

            [DispId(-2147418080)]
            void onrowsdelete(IHTMLEventObj evtObj);

            [DispId(-2147418079)]
            void onrowsinserted(IHTMLEventObj evtObj);

            [DispId(-2147418078)]
            void oncellchange(IHTMLEventObj evtObj);

            [DispId(-609)]
            void onreadystatechange(IHTMLEventObj evtObj);

            [DispId(1030)]
            void onlayoutcomplete(IHTMLEventObj evtObj);

            [DispId(1031)]
            void onpage(IHTMLEventObj evtObj);

            [DispId(1042)]
            void onmouseenter(IHTMLEventObj evtObj);

            [DispId(1043)]
            void onmouseleave(IHTMLEventObj evtObj);

            [DispId(1044)]
            void onactivate(IHTMLEventObj evtObj);

            [DispId(1045)]
            void ondeactivate(IHTMLEventObj evtObj);

            [DispId(1034)]
            bool onbeforedeactivate(IHTMLEventObj evtObj);

            [DispId(1047)]
            bool onbeforeactivate(IHTMLEventObj evtObj);

            [DispId(1048)]
            void onfocusin(IHTMLEventObj evtObj);

            [DispId(1049)]
            void onfocusout(IHTMLEventObj evtObj);

            [DispId(1035)]
            void onmove(IHTMLEventObj evtObj);

            [DispId(1036)]
            bool oncontrolselect(IHTMLEventObj evtObj);

            [DispId(1038)]
            bool onmovestart(IHTMLEventObj evtObj);

            [DispId(1039)]
            void onmoveend(IHTMLEventObj evtObj);

            [DispId(1040)]
            bool onresizestart(IHTMLEventObj evtObj);

            [DispId(1041)]
            void onresizeend(IHTMLEventObj evtObj);

            [DispId(1033)]
            bool onmousewheel(IHTMLEventObj evtObj);

            [DispId(1003)]
            void onload(IHTMLEventObj evtObj);

            [DispId(1002)]
            void onerror(IHTMLEventObj evtObj);
        }

        [ComImport]
        [Guid("3050f623-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        [TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLTableEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(IHTMLEventObj evtObj);

            [DispId(-600)]
            bool onclick(IHTMLEventObj evtObj);

            [DispId(-601)]
            bool ondblclick(IHTMLEventObj evtObj);

            [DispId(-603)]
            bool onkeypress(IHTMLEventObj evtObj);

            [DispId(-602)]
            void onkeydown(IHTMLEventObj evtObj);

            [DispId(-604)]
            void onkeyup(IHTMLEventObj evtObj);

            [DispId(-2147418103)]
            void onmouseout(IHTMLEventObj evtObj);

            [DispId(-2147418104)]
            void onmouseover(IHTMLEventObj evtObj);

            [DispId(-606)]
            void onmousemove(IHTMLEventObj evtObj);

            [DispId(-605)]
            void onmousedown(IHTMLEventObj evtObj);

            [DispId(-607)]
            void onmouseup(IHTMLEventObj evtObj);

            [DispId(-2147418100)]
            bool onselectstart(IHTMLEventObj evtObj);

            [DispId(-2147418095)]
            void onfilterchange(IHTMLEventObj evtObj);

            [DispId(-2147418101)]
            bool ondragstart(IHTMLEventObj evtObj);

            [DispId(-2147418108)]
            bool onbeforeupdate(IHTMLEventObj evtObj);

            [DispId(-2147418107)]
            void onafterupdate(IHTMLEventObj evtObj);

            [DispId(-2147418099)]
            bool onerrorupdate(IHTMLEventObj evtObj);

            [DispId(-2147418106)]
            bool onrowexit(IHTMLEventObj evtObj);

            [DispId(-2147418105)]
            void onrowenter(IHTMLEventObj evtObj);

            [DispId(-2147418098)]
            void ondatasetchanged(IHTMLEventObj evtObj);

            [DispId(-2147418097)]
            void ondataavailable(IHTMLEventObj evtObj);

            [DispId(-2147418096)]
            void ondatasetcomplete(IHTMLEventObj evtObj);

            [DispId(-2147418094)]
            void onlosecapture(IHTMLEventObj evtObj);

            [DispId(-2147418093)]
            void onpropertychange(IHTMLEventObj evtObj);

            [DispId(1014)]
            void onscroll(IHTMLEventObj evtObj);

            [DispId(-2147418111)]
            void onfocus(IHTMLEventObj evtObj);

            [DispId(-2147418112)]
            void onblur(IHTMLEventObj evtObj);

            [DispId(1016)]
            void onresize(IHTMLEventObj evtObj);

            [DispId(-2147418092)]
            bool ondrag(IHTMLEventObj evtObj);

            [DispId(-2147418091)]
            void ondragend(IHTMLEventObj evtObj);

            [DispId(-2147418090)]
            bool ondragenter(IHTMLEventObj evtObj);

            [DispId(-2147418089)]
            bool ondragover(IHTMLEventObj evtObj);

            [DispId(-2147418088)]
            void ondragleave(IHTMLEventObj evtObj);

            [DispId(-2147418087)]
            bool ondrop(IHTMLEventObj evtObj);

            [DispId(-2147418083)]
            bool onbeforecut(IHTMLEventObj evtObj);

            [DispId(-2147418086)]
            bool oncut(IHTMLEventObj evtObj);

            [DispId(-2147418082)]
            bool onbeforecopy(IHTMLEventObj evtObj);

            [DispId(-2147418085)]
            bool oncopy(IHTMLEventObj evtObj);

            [DispId(-2147418081)]
            bool onbeforepaste(IHTMLEventObj evtObj);

            [DispId(-2147418084)]
            bool onpaste(IHTMLEventObj evtObj);

            [DispId(1023)]
            bool oncontextmenu(IHTMLEventObj evtObj);

            [DispId(-2147418080)]
            void onrowsdelete(IHTMLEventObj evtObj);

            [DispId(-2147418079)]
            void onrowsinserted(IHTMLEventObj evtObj);

            [DispId(-2147418078)]
            void oncellchange(IHTMLEventObj evtObj);

            [DispId(-609)]
            void onreadystatechange(IHTMLEventObj evtObj);

            [DispId(1030)]
            void onlayoutcomplete(IHTMLEventObj evtObj);

            [DispId(1031)]
            void onpage(IHTMLEventObj evtObj);

            [DispId(1042)]
            void onmouseenter(IHTMLEventObj evtObj);

            [DispId(1043)]
            void onmouseleave(IHTMLEventObj evtObj);

            [DispId(1044)]
            void onactivate(IHTMLEventObj evtObj);

            [DispId(1045)]
            void ondeactivate(IHTMLEventObj evtObj);

            [DispId(1034)]
            bool onbeforedeactivate(IHTMLEventObj evtObj);

            [DispId(1047)]
            bool onbeforeactivate(IHTMLEventObj evtObj);

            [DispId(1048)]
            void onfocusin(IHTMLEventObj evtObj);

            [DispId(1049)]
            void onfocusout(IHTMLEventObj evtObj);

            [DispId(1035)]
            void onmove(IHTMLEventObj evtObj);

            [DispId(1036)]
            bool oncontrolselect(IHTMLEventObj evtObj);

            [DispId(1038)]
            bool onmovestart(IHTMLEventObj evtObj);

            [DispId(1039)]
            void onmoveend(IHTMLEventObj evtObj);

            [DispId(1040)]
            bool onresizestart(IHTMLEventObj evtObj);

            [DispId(1041)]
            void onresizeend(IHTMLEventObj evtObj);

            [DispId(1033)]
            bool onmousewheel(IHTMLEventObj evtObj);
        }

        [ComImport]
        [Guid("3050f624-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        [TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLTextContainerEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(IHTMLEventObj evtObj);

            [DispId(-600)]
            bool onclick(IHTMLEventObj evtObj);

            [DispId(-601)]
            bool ondblclick(IHTMLEventObj evtObj);

            [DispId(-603)]
            bool onkeypress(IHTMLEventObj evtObj);

            [DispId(-602)]
            void onkeydown(IHTMLEventObj evtObj);

            [DispId(-604)]
            void onkeyup(IHTMLEventObj evtObj);

            [DispId(-2147418103)]
            void onmouseout(IHTMLEventObj evtObj);

            [DispId(-2147418104)]
            void onmouseover(IHTMLEventObj evtObj);

            [DispId(-606)]
            void onmousemove(IHTMLEventObj evtObj);

            [DispId(-605)]
            void onmousedown(IHTMLEventObj evtObj);

            [DispId(-607)]
            void onmouseup(IHTMLEventObj evtObj);

            [DispId(-2147418100)]
            bool onselectstart(IHTMLEventObj evtObj);

            [DispId(-2147418095)]
            void onfilterchange(IHTMLEventObj evtObj);

            [DispId(-2147418101)]
            bool ondragstart(IHTMLEventObj evtObj);

            [DispId(-2147418108)]
            bool onbeforeupdate(IHTMLEventObj evtObj);

            [DispId(-2147418107)]
            void onafterupdate(IHTMLEventObj evtObj);

            [DispId(-2147418099)]
            bool onerrorupdate(IHTMLEventObj evtObj);

            [DispId(-2147418106)]
            bool onrowexit(IHTMLEventObj evtObj);

            [DispId(-2147418105)]
            void onrowenter(IHTMLEventObj evtObj);

            [DispId(-2147418098)]
            void ondatasetchanged(IHTMLEventObj evtObj);

            [DispId(-2147418097)]
            void ondataavailable(IHTMLEventObj evtObj);

            [DispId(-2147418096)]
            void ondatasetcomplete(IHTMLEventObj evtObj);

            [DispId(-2147418094)]
            void onlosecapture(IHTMLEventObj evtObj);

            [DispId(-2147418093)]
            void onpropertychange(IHTMLEventObj evtObj);

            [DispId(1014)]
            void onscroll(IHTMLEventObj evtObj);

            [DispId(-2147418111)]
            void onfocus(IHTMLEventObj evtObj);

            [DispId(-2147418112)]
            void onblur(IHTMLEventObj evtObj);

            [DispId(1016)]
            void onresize(IHTMLEventObj evtObj);

            [DispId(-2147418092)]
            bool ondrag(IHTMLEventObj evtObj);

            [DispId(-2147418091)]
            void ondragend(IHTMLEventObj evtObj);

            [DispId(-2147418090)]
            bool ondragenter(IHTMLEventObj evtObj);

            [DispId(-2147418089)]
            bool ondragover(IHTMLEventObj evtObj);

            [DispId(-2147418088)]
            void ondragleave(IHTMLEventObj evtObj);

            [DispId(-2147418087)]
            bool ondrop(IHTMLEventObj evtObj);

            [DispId(-2147418083)]
            bool onbeforecut(IHTMLEventObj evtObj);

            [DispId(-2147418086)]
            bool oncut(IHTMLEventObj evtObj);

            [DispId(-2147418082)]
            bool onbeforecopy(IHTMLEventObj evtObj);

            [DispId(-2147418085)]
            bool oncopy(IHTMLEventObj evtObj);

            [DispId(-2147418081)]
            bool onbeforepaste(IHTMLEventObj evtObj);

            [DispId(-2147418084)]
            bool onpaste(IHTMLEventObj evtObj);

            [DispId(1023)]
            bool oncontextmenu(IHTMLEventObj evtObj);

            [DispId(-2147418080)]
            void onrowsdelete(IHTMLEventObj evtObj);

            [DispId(-2147418079)]
            void onrowsinserted(IHTMLEventObj evtObj);

            [DispId(-2147418078)]
            void oncellchange(IHTMLEventObj evtObj);

            [DispId(-609)]
            void onreadystatechange(IHTMLEventObj evtObj);

            [DispId(1030)]
            void onlayoutcomplete(IHTMLEventObj evtObj);

            [DispId(1031)]
            void onpage(IHTMLEventObj evtObj);

            [DispId(1042)]
            void onmouseenter(IHTMLEventObj evtObj);

            [DispId(1043)]
            void onmouseleave(IHTMLEventObj evtObj);

            [DispId(1044)]
            void onactivate(IHTMLEventObj evtObj);

            [DispId(1045)]
            void ondeactivate(IHTMLEventObj evtObj);

            [DispId(1034)]
            bool onbeforedeactivate(IHTMLEventObj evtObj);

            [DispId(1047)]
            bool onbeforeactivate(IHTMLEventObj evtObj);

            [DispId(1048)]
            void onfocusin(IHTMLEventObj evtObj);

            [DispId(1049)]
            void onfocusout(IHTMLEventObj evtObj);

            [DispId(1035)]
            void onmove(IHTMLEventObj evtObj);

            [DispId(1036)]
            bool oncontrolselect(IHTMLEventObj evtObj);

            [DispId(1038)]
            bool onmovestart(IHTMLEventObj evtObj);

            [DispId(1039)]
            void onmoveend(IHTMLEventObj evtObj);

            [DispId(1040)]
            bool onresizestart(IHTMLEventObj evtObj);

            [DispId(1041)]
            void onresizeend(IHTMLEventObj evtObj);

            [DispId(1033)]
            bool onmousewheel(IHTMLEventObj evtObj);

            [DispId(1001)]
            void onchange_void(IHTMLEventObj evtObj);

            [DispId(1006)]
            void onselect(IHTMLEventObj evtObj);
        }

        [ComImport]
        [Guid("3050f621-98b5-11cf-bb82-00aa00bdce0b")]
        [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        [TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DHTMLScriptEvents2
        {
            [DispId(-2147418102)]
            bool onhelp(IHTMLEventObj evtObj);

            [DispId(-600)]
            bool onclick(IHTMLEventObj evtObj);

            [DispId(-601)]
            bool ondblclick(IHTMLEventObj evtObj);

            [DispId(-603)]
            bool onkeypress(IHTMLEventObj evtObj);

            [DispId(-602)]
            void onkeydown(IHTMLEventObj evtObj);

            [DispId(-604)]
            void onkeyup(IHTMLEventObj evtObj);

            [DispId(-2147418103)]
            void onmouseout(IHTMLEventObj evtObj);

            [DispId(-2147418104)]
            void onmouseover(IHTMLEventObj evtObj);

            [DispId(-606)]
            void onmousemove(IHTMLEventObj evtObj);

            [DispId(-605)]
            void onmousedown(IHTMLEventObj evtObj);

            [DispId(-607)]
            void onmouseup(IHTMLEventObj evtObj);

            [DispId(-2147418100)]
            bool onselectstart(IHTMLEventObj evtObj);

            [DispId(-2147418095)]
            void onfilterchange(IHTMLEventObj evtObj);

            [DispId(-2147418101)]
            bool ondragstart(IHTMLEventObj evtObj);

            [DispId(-2147418108)]
            bool onbeforeupdate(IHTMLEventObj evtObj);

            [DispId(-2147418107)]
            void onafterupdate(IHTMLEventObj evtObj);

            [DispId(-2147418099)]
            bool onerrorupdate(IHTMLEventObj evtObj);

            [DispId(-2147418106)]
            bool onrowexit(IHTMLEventObj evtObj);

            [DispId(-2147418105)]
            void onrowenter(IHTMLEventObj evtObj);

            [DispId(-2147418098)]
            void ondatasetchanged(IHTMLEventObj evtObj);

            [DispId(-2147418097)]
            void ondataavailable(IHTMLEventObj evtObj);

            [DispId(-2147418096)]
            void ondatasetcomplete(IHTMLEventObj evtObj);

            [DispId(-2147418094)]
            void onlosecapture(IHTMLEventObj evtObj);

            [DispId(-2147418093)]
            void onpropertychange(IHTMLEventObj evtObj);

            [DispId(1014)]
            void onscroll(IHTMLEventObj evtObj);

            [DispId(-2147418111)]
            void onfocus(IHTMLEventObj evtObj);

            [DispId(-2147418112)]
            void onblur(IHTMLEventObj evtObj);

            [DispId(1016)]
            void onresize(IHTMLEventObj evtObj);

            [DispId(-2147418092)]
            bool ondrag(IHTMLEventObj evtObj);

            [DispId(-2147418091)]
            void ondragend(IHTMLEventObj evtObj);

            [DispId(-2147418090)]
            bool ondragenter(IHTMLEventObj evtObj);

            [DispId(-2147418089)]
            bool ondragover(IHTMLEventObj evtObj);

            [DispId(-2147418088)]
            void ondragleave(IHTMLEventObj evtObj);

            [DispId(-2147418087)]
            bool ondrop(IHTMLEventObj evtObj);

            [DispId(-2147418083)]
            bool onbeforecut(IHTMLEventObj evtObj);

            [DispId(-2147418086)]
            bool oncut(IHTMLEventObj evtObj);

            [DispId(-2147418082)]
            bool onbeforecopy(IHTMLEventObj evtObj);

            [DispId(-2147418085)]
            bool oncopy(IHTMLEventObj evtObj);

            [DispId(-2147418081)]
            bool onbeforepaste(IHTMLEventObj evtObj);

            [DispId(-2147418084)]
            bool onpaste(IHTMLEventObj evtObj);

            [DispId(1023)]
            bool oncontextmenu(IHTMLEventObj evtObj);

            [DispId(-2147418080)]
            void onrowsdelete(IHTMLEventObj evtObj);

            [DispId(-2147418079)]
            void onrowsinserted(IHTMLEventObj evtObj);

            [DispId(-2147418078)]
            void oncellchange(IHTMLEventObj evtObj);

            [DispId(-609)]
            void onreadystatechange(IHTMLEventObj evtObj);

            [DispId(1030)]
            void onlayoutcomplete(IHTMLEventObj evtObj);

            [DispId(1031)]
            void onpage(IHTMLEventObj evtObj);

            [DispId(1042)]
            void onmouseenter(IHTMLEventObj evtObj);

            [DispId(1043)]
            void onmouseleave(IHTMLEventObj evtObj);

            [DispId(1044)]
            void onactivate(IHTMLEventObj evtObj);

            [DispId(1045)]
            void ondeactivate(IHTMLEventObj evtObj);

            [DispId(1034)]
            bool onbeforedeactivate(IHTMLEventObj evtObj);

            [DispId(1047)]
            bool onbeforeactivate(IHTMLEventObj evtObj);

            [DispId(1048)]
            void onfocusin(IHTMLEventObj evtObj);

            [DispId(1049)]
            void onfocusout(IHTMLEventObj evtObj);

            [DispId(1035)]
            void onmove(IHTMLEventObj evtObj);

            [DispId(1036)]
            bool oncontrolselect(IHTMLEventObj evtObj);

            [DispId(1038)]
            bool onmovestart(IHTMLEventObj evtObj);

            [DispId(1039)]
            void onmoveend(IHTMLEventObj evtObj);

            [DispId(1040)]
            bool onresizestart(IHTMLEventObj evtObj);

            [DispId(1041)]
            void onresizeend(IHTMLEventObj evtObj);

            [DispId(1033)]
            bool onmousewheel(IHTMLEventObj evtObj);

            [DispId(1002)]
            void onerror(IHTMLEventObj evtObj);
        }

        [SuppressUnmanagedCodeSecurity]
        [ComVisible(true)]
        [Guid("3050F25E-98B5-11CF-BB82-00AA00BDCE0B")]
        [InterfaceType(ComInterfaceType.InterfaceIsDual)]
        internal interface IHTMLStyle
        {
            void SetFontFamily(string p);

            string GetFontFamily();

            void SetFontStyle(string p);

            string GetFontStyle();

            void SetFontObject(string p);

            string GetFontObject();

            void SetFontWeight(string p);

            string GetFontWeight();

            void SetFontSize(object p);

            object GetFontSize();

            void SetFont(string p);

            string GetFont();

            void SetColor(object p);

            object GetColor();

            void SetBackground(string p);

            string GetBackground();

            void SetBackgroundColor(object p);

            object GetBackgroundColor();

            void SetBackgroundImage(string p);

            string GetBackgroundImage();

            void SetBackgroundRepeat(string p);

            string GetBackgroundRepeat();

            void SetBackgroundAttachment(string p);

            string GetBackgroundAttachment();

            void SetBackgroundPosition(string p);

            string GetBackgroundPosition();

            void SetBackgroundPositionX(object p);

            object GetBackgroundPositionX();

            void SetBackgroundPositionY(object p);

            object GetBackgroundPositionY();

            void SetWordSpacing(object p);

            object GetWordSpacing();

            void SetLetterSpacing(object p);

            object GetLetterSpacing();

            void SetTextDecoration(string p);

            string GetTextDecoration();

            void SetTextDecorationNone(bool p);

            bool GetTextDecorationNone();

            void SetTextDecorationUnderline(bool p);

            bool GetTextDecorationUnderline();

            void SetTextDecorationOverline(bool p);

            bool GetTextDecorationOverline();

            void SetTextDecorationLineThrough(bool p);

            bool GetTextDecorationLineThrough();

            void SetTextDecorationBlink(bool p);

            bool GetTextDecorationBlink();

            void SetVerticalAlign(object p);

            object GetVerticalAlign();

            void SetTextTransform(string p);

            string GetTextTransform();

            void SetTextAlign(string p);

            string GetTextAlign();

            void SetTextIndent(object p);

            object GetTextIndent();

            void SetLineHeight(object p);

            object GetLineHeight();

            void SetMarginTop(object p);

            object GetMarginTop();

            void SetMarginRight(object p);

            object GetMarginRight();

            void SetMarginBottom(object p);

            object GetMarginBottom();

            void SetMarginLeft(object p);

            object GetMarginLeft();

            void SetMargin(string p);

            string GetMargin();

            void SetPaddingTop(object p);

            object GetPaddingTop();

            void SetPaddingRight(object p);

            object GetPaddingRight();

            void SetPaddingBottom(object p);

            object GetPaddingBottom();

            void SetPaddingLeft(object p);

            object GetPaddingLeft();

            void SetPadding(string p);

            string GetPadding();

            void SetBorder(string p);

            string GetBorder();

            void SetBorderTop(string p);

            string GetBorderTop();

            void SetBorderRight(string p);

            string GetBorderRight();

            void SetBorderBottom(string p);

            string GetBorderBottom();

            void SetBorderLeft(string p);

            string GetBorderLeft();

            void SetBorderColor(string p);

            string GetBorderColor();

            void SetBorderTopColor(object p);

            object GetBorderTopColor();

            void SetBorderRightColor(object p);

            object GetBorderRightColor();

            void SetBorderBottomColor(object p);

            object GetBorderBottomColor();

            void SetBorderLeftColor(object p);

            object GetBorderLeftColor();

            void SetBorderWidth(string p);

            string GetBorderWidth();

            void SetBorderTopWidth(object p);

            object GetBorderTopWidth();

            void SetBorderRightWidth(object p);

            object GetBorderRightWidth();

            void SetBorderBottomWidth(object p);

            object GetBorderBottomWidth();

            void SetBorderLeftWidth(object p);

            object GetBorderLeftWidth();

            void SetBorderStyle(string p);

            string GetBorderStyle();

            void SetBorderTopStyle(string p);

            string GetBorderTopStyle();

            void SetBorderRightStyle(string p);

            string GetBorderRightStyle();

            void SetBorderBottomStyle(string p);

            string GetBorderBottomStyle();

            void SetBorderLeftStyle(string p);

            string GetBorderLeftStyle();

            void SetWidth(object p);

            object GetWidth();

            void SetHeight(object p);

            object GetHeight();

            void SetStyleFloat(string p);

            string GetStyleFloat();

            void SetClear(string p);

            string GetClear();

            void SetDisplay(string p);

            string GetDisplay();

            void SetVisibility(string p);

            string GetVisibility();

            void SetListStyleType(string p);

            string GetListStyleType();

            void SetListStylePosition(string p);

            string GetListStylePosition();

            void SetListStyleImage(string p);

            string GetListStyleImage();

            void SetListStyle(string p);

            string GetListStyle();

            void SetWhiteSpace(string p);

            string GetWhiteSpace();

            void SetTop(object p);

            object GetTop();

            void SetLeft(object p);

            object GetLeft();

            string GetPosition();

            void SetZIndex(object p);

            object GetZIndex();

            void SetOverflow(string p);

            string GetOverflow();

            void SetPageBreakBefore(string p);

            string GetPageBreakBefore();

            void SetPageBreakAfter(string p);

            string GetPageBreakAfter();

            void SetCssText(string p);

            string GetCssText();

            void SetPixelTop(int p);

            int GetPixelTop();

            void SetPixelLeft(int p);

            int GetPixelLeft();

            void SetPixelWidth(int p);

            int GetPixelWidth();

            void SetPixelHeight(int p);

            int GetPixelHeight();

            void SetPosTop(float p);

            float GetPosTop();

            void SetPosLeft(float p);

            float GetPosLeft();

            void SetPosWidth(float p);

            float GetPosWidth();

            void SetPosHeight(float p);

            float GetPosHeight();

            void SetCursor(string p);

            string GetCursor();

            void SetClip(string p);

            string GetClip();

            void SetFilter(string p);

            string GetFilter();

            void SetAttribute(string strAttributeName, object AttributeValue, int lFlags);

            object GetAttribute(string strAttributeName, int lFlags);

            bool RemoveAttribute(string strAttributeName, int lFlags);
        }

        [ComImport]
        [Guid("39088D7E-B71E-11D1-8F39-00C04FD946D0")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IExtender
        {
            int Align { get; set; }

            bool Enabled { get; set; }

            int Height { get; set; }

            int Left { get; set; }

            bool TabStop { get; set; }

            int Top { get; set; }

            bool Visible { get; set; }

            int Width { get; set; }

            string Name
            {
                [return: MarshalAs(UnmanagedType.BStr)]
                get;
            }

            object Parent
            {
                [return: MarshalAs(UnmanagedType.Interface)]
                get;
            }

            IntPtr Hwnd { get; }

            object Container
            {
                [return: MarshalAs(UnmanagedType.Interface)]
                get;
            }

            void Move([In][MarshalAs(UnmanagedType.Interface)] object left, [In][MarshalAs(UnmanagedType.Interface)] object top, [In][MarshalAs(UnmanagedType.Interface)] object width, [In][MarshalAs(UnmanagedType.Interface)] object height);
        }

        [ComImport]
        [Guid("8A701DA0-4FEB-101B-A82E-08002B2B2337")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IGetOleObject
        {
            [return: MarshalAs(UnmanagedType.Interface)]
            object GetOleObject(ref Guid riid);
        }

        [ComImport]
        [Guid("CB2F6722-AB3A-11d2-9C40-00C04FA30A3E")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface ICorRuntimeHost
        {
            [PreserveSig]
            int CreateLogicalThreadState();

            [PreserveSig]
            int DeleteLogicalThreadState();

            [PreserveSig]
            int SwitchInLogicalThreadState([In] ref uint pFiberCookie);

            [PreserveSig]
            int SwitchOutLogicalThreadState(out uint FiberCookie);

            [PreserveSig]
            int LocksHeldByLogicalThread(out uint pCount);

            [PreserveSig]
            int MapFile(IntPtr hFile, out IntPtr hMapAddress);

            [PreserveSig]
            int GetConfiguration([MarshalAs(UnmanagedType.IUnknown)] out object pConfiguration);

            [PreserveSig]
            int Start();

            [PreserveSig]
            int Stop();

            [PreserveSig]
            int CreateDomain(string pwzFriendlyName, [MarshalAs(UnmanagedType.IUnknown)] object pIdentityArray, [MarshalAs(UnmanagedType.IUnknown)] out object pAppDomain);

            [PreserveSig]
            int GetDefaultDomain([MarshalAs(UnmanagedType.IUnknown)] out object pAppDomain);

            [PreserveSig]
            int EnumDomains(out IntPtr hEnum);

            [PreserveSig]
            int NextDomain(IntPtr hEnum, [MarshalAs(UnmanagedType.IUnknown)] out object pAppDomain);

            [PreserveSig]
            int CloseEnum(IntPtr hEnum);

            [PreserveSig]
            int CreateDomainEx(string pwzFriendlyName, [MarshalAs(UnmanagedType.IUnknown)] object pSetup, [MarshalAs(UnmanagedType.IUnknown)] object pEvidence, [MarshalAs(UnmanagedType.IUnknown)] out object pAppDomain);

            [PreserveSig]
            int CreateDomainSetup([MarshalAs(UnmanagedType.IUnknown)] out object pAppDomainSetup);

            [PreserveSig]
            int CreateEvidence([MarshalAs(UnmanagedType.IUnknown)] out object pEvidence);

            [PreserveSig]
            int UnloadDomain([MarshalAs(UnmanagedType.IUnknown)] object pAppDomain);

            [PreserveSig]
            int CurrentDomain([MarshalAs(UnmanagedType.IUnknown)] out object pAppDomain);
        }

        [ComImport]
        [Guid("000C0601-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IMsoComponentManager
        {
            [PreserveSig]
            int QueryService(ref Guid guidService, ref Guid iid, [MarshalAs(UnmanagedType.Interface)] out object ppvObj);

            [PreserveSig]
            bool FDebugMessage(IntPtr hInst, int msg, IntPtr wParam, IntPtr lParam);

            [PreserveSig]
            bool FRegisterComponent(IMsoComponent component, NativeMethods.MSOCRINFOSTRUCT pcrinfo, out IntPtr dwComponentID);

            [PreserveSig]
            bool FRevokeComponent(IntPtr dwComponentID);

            [PreserveSig]
            bool FUpdateComponentRegistration(IntPtr dwComponentID, NativeMethods.MSOCRINFOSTRUCT pcrinfo);

            [PreserveSig]
            bool FOnComponentActivate(IntPtr dwComponentID);

            [PreserveSig]
            bool FSetTrackingComponent(IntPtr dwComponentID, [In][MarshalAs(UnmanagedType.Bool)] bool fTrack);

            [PreserveSig]
            void OnComponentEnterState(IntPtr dwComponentID, int uStateID, int uContext, int cpicmExclude, int rgpicmExclude, int dwReserved);

            [PreserveSig]
            bool FOnComponentExitState(IntPtr dwComponentID, int uStateID, int uContext, int cpicmExclude, int rgpicmExclude);

            [PreserveSig]
            bool FInState(int uStateID, IntPtr pvoid);

            [PreserveSig]
            bool FContinueIdle();

            [PreserveSig]
            bool FPushMessageLoop(IntPtr dwComponentID, int uReason, int pvLoopData);

            [PreserveSig]
            bool FCreateSubComponentManager([MarshalAs(UnmanagedType.Interface)] object punkOuter, [MarshalAs(UnmanagedType.Interface)] object punkServProv, ref Guid riid, out IntPtr ppvObj);

            [PreserveSig]
            bool FGetParentComponentManager(out IMsoComponentManager ppicm);

            [PreserveSig]
            bool FGetActiveComponent(int dwgac, [Out][MarshalAs(UnmanagedType.LPArray)] IMsoComponent[] ppic, NativeMethods.MSOCRINFOSTRUCT pcrinfo, int dwReserved);
        }

        [ComImport]
        [Guid("000C0600-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IMsoComponent
        {
            [PreserveSig]
            bool FDebugMessage(IntPtr hInst, int msg, IntPtr wParam, IntPtr lParam);

            [PreserveSig]
            bool FPreTranslateMessage(ref NativeMethods.MSG msg);

            [PreserveSig]
            void OnEnterState(int uStateID, bool fEnter);

            [PreserveSig]
            void OnAppActivate(bool fActive, int dwOtherThreadID);

            [PreserveSig]
            void OnLoseActivation();

            [PreserveSig]
            void OnActivationChange(IMsoComponent component, bool fSameComponent, int pcrinfo, bool fHostIsActivating, int pchostinfo, int dwReserved);

            [PreserveSig]
            bool FDoIdle(int grfidlef);

            [PreserveSig]
            bool FContinueMessageLoop(int uReason, int pvLoopData, [MarshalAs(UnmanagedType.LPArray)] NativeMethods.MSG[] pMsgPeeked);

            [PreserveSig]
            bool FQueryTerminate(bool fPromptUser);

            [PreserveSig]
            void Terminate();

            [PreserveSig]
            IntPtr HwndGetWindow(int dwWhich, int dwReserved);
        }

        [ComVisible(true)]
        [Guid("8CC497C0-A1DF-11ce-8098-00AA0047BE5D")]
        [InterfaceType(ComInterfaceType.InterfaceIsDual)]
        [SuppressUnmanagedCodeSecurity]
        public interface ITextDocument
        {
            string GetName();

            object GetSelection();

            int GetStoryCount();

            object GetStoryRanges();

            int GetSaved();

            void SetSaved(int value);

            object GetDefaultTabStop();

            void SetDefaultTabStop(object value);

            void New();

            void Open(object pVar, int flags, int codePage);

            void Save(object pVar, int flags, int codePage);

            int Freeze();

            int Unfreeze();

            void BeginEditCollection();

            void EndEditCollection();

            int Undo(int count);

            int Redo(int count);

            [return: MarshalAs(UnmanagedType.Interface)]
            ITextRange Range(int cp1, int cp2);

            [return: MarshalAs(UnmanagedType.Interface)]
            ITextRange RangeFromPoint(int x, int y);
        }

        [ComVisible(true)]
        [Guid("8CC497C2-A1DF-11ce-8098-00AA0047BE5D")]
        [InterfaceType(ComInterfaceType.InterfaceIsDual)]
        [SuppressUnmanagedCodeSecurity]
        public interface ITextRange
        {
            string GetText();

            void SetText(string text);

            object GetChar();

            void SetChar(object ch);

            [return: MarshalAs(UnmanagedType.Interface)]
            ITextRange GetDuplicate();

            [return: MarshalAs(UnmanagedType.Interface)]
            ITextRange GetFormattedText();

            void SetFormattedText([In][MarshalAs(UnmanagedType.Interface)] ITextRange range);

            int GetStart();

            void SetStart(int cpFirst);

            int GetEnd();

            void SetEnd(int cpLim);

            object GetFont();

            void SetFont(object font);

            object GetPara();

            void SetPara(object para);

            int GetStoryLength();

            int GetStoryType();

            void Collapse(int start);

            int Expand(int unit);

            int GetIndex(int unit);

            void SetIndex(int unit, int index, int extend);

            void SetRange(int cpActive, int cpOther);

            int InRange([In][MarshalAs(UnmanagedType.Interface)] ITextRange range);

            int InStory([In][MarshalAs(UnmanagedType.Interface)] ITextRange range);

            int IsEqual([In][MarshalAs(UnmanagedType.Interface)] ITextRange range);

            void Select();

            int StartOf(int unit, int extend);

            int EndOf(int unit, int extend);

            int Move(int unit, int count);

            int MoveStart(int unit, int count);

            int MoveEnd(int unit, int count);

            int MoveWhile(object cset, int count);

            int MoveStartWhile(object cset, int count);

            int MoveEndWhile(object cset, int count);

            int MoveUntil(object cset, int count);

            int MoveStartUntil(object cset, int count);

            int MoveEndUntil(object cset, int count);

            int FindText(string text, int cch, int flags);

            int FindTextStart(string text, int cch, int flags);

            int FindTextEnd(string text, int cch, int flags);

            int Delete(int unit, int count);

            void Cut(out object pVar);

            void Copy(out object pVar);

            void Paste(object pVar, int format);

            int CanPaste(object pVar, int format);

            int CanEdit();

            void ChangeCase(int type);

            void GetPoint(int type, out int x, out int y);

            void SetPoint(int x, int y, int type, int extend);

            void ScrollIntoView(int value);

            object GetEmbeddedObject();
        }

        [ComImport]
        [Guid("00020D03-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IRichEditOleCallback
        {
            [PreserveSig]
            int GetNewStorage(out IStorage ret);

            [PreserveSig]
            int GetInPlaceContext(IntPtr lplpFrame, IntPtr lplpDoc, IntPtr lpFrameInfo);

            [PreserveSig]
            int ShowContainerUI(int fShow);

            [PreserveSig]
            int QueryInsertObject(ref Guid lpclsid, IntPtr lpstg, int cp);

            [PreserveSig]
            int DeleteObject(IntPtr lpoleobj);

            [PreserveSig]
            int QueryAcceptData(System.Runtime.InteropServices.ComTypes.IDataObject lpdataobj, IntPtr lpcfFormat, int reco, int fReally, IntPtr hMetaPict);

            [PreserveSig]
            int ContextSensitiveHelp(int fEnterMode);

            [PreserveSig]
            int GetClipboardData(NativeMethods.CHARRANGE lpchrg, int reco, IntPtr lplpdataobj);

            [PreserveSig]
            int GetDragDropEffect(bool fDrag, int grfKeyState, ref int pdwEffect);

            [PreserveSig]
            int GetContextMenu(short seltype, IntPtr lpoleobj, NativeMethods.CHARRANGE lpchrg, out IntPtr hmenu);
        }

        [ComImport]
        [Guid("00000115-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleInPlaceUIWindow
        {
            IntPtr GetWindow();

            [PreserveSig]
            int ContextSensitiveHelp(int fEnterMode);

            [PreserveSig]
            int GetBorder([Out] NativeMethods.COMRECT lprectBorder);

            [PreserveSig]
            int RequestBorderSpace([In] NativeMethods.COMRECT pborderwidths);

            [PreserveSig]
            int SetBorderSpace([In] NativeMethods.COMRECT pborderwidths);

            void SetActiveObject([In][MarshalAs(UnmanagedType.Interface)] IOleInPlaceActiveObject pActiveObject, [In][MarshalAs(UnmanagedType.LPWStr)] string pszObjName);
        }

        [ComImport]
        [SuppressUnmanagedCodeSecurity]
        [Guid("00000117-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleInPlaceActiveObject
        {
            [PreserveSig]
            int GetWindow(out IntPtr hwnd);

            void ContextSensitiveHelp(int fEnterMode);

            [PreserveSig]
            int TranslateAccelerator([In] ref NativeMethods.MSG lpmsg);

            void OnFrameWindowActivate(bool fActivate);

            void OnDocWindowActivate(int fActivate);

            void ResizeBorder([In] NativeMethods.COMRECT prcBorder, [In] IOleInPlaceUIWindow pUIWindow, bool fFrameWindow);

            void EnableModeless(int fEnable);
        }

        [ComImport]
        [Guid("00000114-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleWindow
        {
            [PreserveSig]
            int GetWindow(out IntPtr hwnd);

            void ContextSensitiveHelp(int fEnterMode);
        }

        [ComImport]
        [SuppressUnmanagedCodeSecurity]
        [Guid("00000113-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleInPlaceObject
        {
            [PreserveSig]
            int GetWindow(out IntPtr hwnd);

            void ContextSensitiveHelp(int fEnterMode);

            void InPlaceDeactivate();

            [PreserveSig]
            int UIDeactivate();

            void SetObjectRects([In] NativeMethods.COMRECT lprcPosRect, [In] NativeMethods.COMRECT lprcClipRect);

            void ReactivateAndUndo();
        }

        [ComImport]
        [SuppressUnmanagedCodeSecurity]
        [Guid("00000112-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleObject
        {
            [PreserveSig]
            int SetClientSite([In][MarshalAs(UnmanagedType.Interface)] IOleClientSite pClientSite);

            IOleClientSite GetClientSite();

            [PreserveSig]
            int SetHostNames([In][MarshalAs(UnmanagedType.LPWStr)] string szContainerApp, [In][MarshalAs(UnmanagedType.LPWStr)] string szContainerObj);

            [PreserveSig]
            int Close(int dwSaveOption);

            [PreserveSig]
            int SetMoniker([In][MarshalAs(UnmanagedType.U4)] int dwWhichMoniker, [In][MarshalAs(UnmanagedType.Interface)] object pmk);

            [PreserveSig]
            int GetMoniker([In][MarshalAs(UnmanagedType.U4)] int dwAssign, [In][MarshalAs(UnmanagedType.U4)] int dwWhichMoniker, [MarshalAs(UnmanagedType.Interface)] out object moniker);

            [PreserveSig]
            int InitFromData([In][MarshalAs(UnmanagedType.Interface)] System.Runtime.InteropServices.ComTypes.IDataObject pDataObject, int fCreation, [In][MarshalAs(UnmanagedType.U4)] int dwReserved);

            [PreserveSig]
            int GetClipboardData([In][MarshalAs(UnmanagedType.U4)] int dwReserved, out System.Runtime.InteropServices.ComTypes.IDataObject data);

            [PreserveSig]
            int DoVerb(int iVerb, [In] IntPtr lpmsg, [In][MarshalAs(UnmanagedType.Interface)] IOleClientSite pActiveSite, int lindex, IntPtr hwndParent, [In] NativeMethods.COMRECT lprcPosRect);

            [PreserveSig]
            int EnumVerbs(out IEnumOLEVERB e);

            [PreserveSig]
            int OleUpdate();

            [PreserveSig]
            int IsUpToDate();

            [PreserveSig]
            int GetUserClassID([In][Out] ref Guid pClsid);

            [PreserveSig]
            int GetUserType([In][MarshalAs(UnmanagedType.U4)] int dwFormOfType, [MarshalAs(UnmanagedType.LPWStr)] out string userType);

            [PreserveSig]
            int SetExtent([In][MarshalAs(UnmanagedType.U4)] int dwDrawAspect, [In] NativeMethods.tagSIZEL pSizel);

            [PreserveSig]
            int GetExtent([In][MarshalAs(UnmanagedType.U4)] int dwDrawAspect, [Out] NativeMethods.tagSIZEL pSizel);

            [PreserveSig]
            int Advise(IAdviseSink pAdvSink, out int cookie);

            [PreserveSig]
            int Unadvise([In][MarshalAs(UnmanagedType.U4)] int dwConnection);

            [PreserveSig]
            int EnumAdvise(out IEnumSTATDATA e);

            [PreserveSig]
            int GetMiscStatus([In][MarshalAs(UnmanagedType.U4)] int dwAspect, out int misc);

            [PreserveSig]
            int SetColorScheme([In] NativeMethods.tagLOGPALETTE pLogpal);
        }

        [ComImport]
        [Guid("1C2056CC-5EF4-101B-8BC8-00AA003E3B29")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleInPlaceObjectWindowless
        {
            [PreserveSig]
            int SetClientSite([In][MarshalAs(UnmanagedType.Interface)] IOleClientSite pClientSite);

            [PreserveSig]
            int GetClientSite(out IOleClientSite site);

            [PreserveSig]
            int SetHostNames([In][MarshalAs(UnmanagedType.LPWStr)] string szContainerApp, [In][MarshalAs(UnmanagedType.LPWStr)] string szContainerObj);

            [PreserveSig]
            int Close(int dwSaveOption);

            [PreserveSig]
            int SetMoniker([In][MarshalAs(UnmanagedType.U4)] int dwWhichMoniker, [In][MarshalAs(UnmanagedType.Interface)] object pmk);

            [PreserveSig]
            int GetMoniker([In][MarshalAs(UnmanagedType.U4)] int dwAssign, [In][MarshalAs(UnmanagedType.U4)] int dwWhichMoniker, [MarshalAs(UnmanagedType.Interface)] out object moniker);

            [PreserveSig]
            int InitFromData([In][MarshalAs(UnmanagedType.Interface)] System.Runtime.InteropServices.ComTypes.IDataObject pDataObject, int fCreation, [In][MarshalAs(UnmanagedType.U4)] int dwReserved);

            [PreserveSig]
            int GetClipboardData([In][MarshalAs(UnmanagedType.U4)] int dwReserved, out System.Runtime.InteropServices.ComTypes.IDataObject data);

            [PreserveSig]
            int DoVerb(int iVerb, [In] IntPtr lpmsg, [In][MarshalAs(UnmanagedType.Interface)] IOleClientSite pActiveSite, int lindex, IntPtr hwndParent, [In] NativeMethods.COMRECT lprcPosRect);

            [PreserveSig]
            int EnumVerbs(out IEnumOLEVERB e);

            [PreserveSig]
            int OleUpdate();

            [PreserveSig]
            int IsUpToDate();

            [PreserveSig]
            int GetUserClassID([In][Out] ref Guid pClsid);

            [PreserveSig]
            int GetUserType([In][MarshalAs(UnmanagedType.U4)] int dwFormOfType, [MarshalAs(UnmanagedType.LPWStr)] out string userType);

            [PreserveSig]
            int SetExtent([In][MarshalAs(UnmanagedType.U4)] int dwDrawAspect, [In] NativeMethods.tagSIZEL pSizel);

            [PreserveSig]
            int GetExtent([In][MarshalAs(UnmanagedType.U4)] int dwDrawAspect, [Out] NativeMethods.tagSIZEL pSizel);

            [PreserveSig]
            int Advise([In][MarshalAs(UnmanagedType.Interface)] IAdviseSink pAdvSink, out int cookie);

            [PreserveSig]
            int Unadvise([In][MarshalAs(UnmanagedType.U4)] int dwConnection);

            [PreserveSig]
            int EnumAdvise(out IEnumSTATDATA e);

            [PreserveSig]
            int GetMiscStatus([In][MarshalAs(UnmanagedType.U4)] int dwAspect, out int misc);

            [PreserveSig]
            int SetColorScheme([In] NativeMethods.tagLOGPALETTE pLogpal);

            [PreserveSig]
            int OnWindowMessage([In][MarshalAs(UnmanagedType.U4)] int msg, [In][MarshalAs(UnmanagedType.U4)] int wParam, [In][MarshalAs(UnmanagedType.U4)] int lParam, [Out][MarshalAs(UnmanagedType.U4)] int plResult);

            [PreserveSig]
            int GetDropTarget([Out][MarshalAs(UnmanagedType.Interface)] object ppDropTarget);
        }

        [ComImport]
        [SuppressUnmanagedCodeSecurity]
        [Guid("B196B288-BAB4-101A-B69C-00AA00341D07")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleControl
        {
            [PreserveSig]
            int GetControlInfo([Out] NativeMethods.tagCONTROLINFO pCI);

            [PreserveSig]
            int OnMnemonic([In] ref NativeMethods.MSG pMsg);

            [PreserveSig]
            int OnAmbientPropertyChange(int dispID);

            [PreserveSig]
            int FreezeEvents(int bFreeze);
        }

        [ComImport]
        [Guid("6D5140C1-7436-11CE-8034-00AA006009FA")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleServiceProvider
        {
            [PreserveSig]
            int QueryService([In] ref Guid guidService, [In] ref Guid riid, out IntPtr ppvObject);
        }

        [ComImport]
        [Guid("0000010d-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IViewObject
        {
            [PreserveSig]
            int Draw([In][MarshalAs(UnmanagedType.U4)] int dwDrawAspect, int lindex, IntPtr pvAspect, [In] NativeMethods.tagDVTARGETDEVICE ptd, IntPtr hdcTargetDev, IntPtr hdcDraw, [In] NativeMethods.COMRECT lprcBounds, [In] NativeMethods.COMRECT lprcWBounds, IntPtr pfnContinue, [In] int dwContinue);

            [PreserveSig]
            int GetColorSet([In][MarshalAs(UnmanagedType.U4)] int dwDrawAspect, int lindex, IntPtr pvAspect, [In] NativeMethods.tagDVTARGETDEVICE ptd, IntPtr hicTargetDev, [Out] NativeMethods.tagLOGPALETTE ppColorSet);

            [PreserveSig]
            int Freeze([In][MarshalAs(UnmanagedType.U4)] int dwDrawAspect, int lindex, IntPtr pvAspect, [Out] IntPtr pdwFreeze);

            [PreserveSig]
            int Unfreeze([In][MarshalAs(UnmanagedType.U4)] int dwFreeze);

            void SetAdvise([In][MarshalAs(UnmanagedType.U4)] int aspects, [In][MarshalAs(UnmanagedType.U4)] int advf, [In][MarshalAs(UnmanagedType.Interface)] IAdviseSink pAdvSink);

            void GetAdvise([In][Out][MarshalAs(UnmanagedType.LPArray)] int[] paspects, [In][Out][MarshalAs(UnmanagedType.LPArray)] int[] advf, [In][Out][MarshalAs(UnmanagedType.LPArray)] IAdviseSink[] pAdvSink);
        }

        [ComImport]
        [Guid("00000127-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IViewObject2
        {
            void Draw([In][MarshalAs(UnmanagedType.U4)] int dwDrawAspect, int lindex, IntPtr pvAspect, [In] NativeMethods.tagDVTARGETDEVICE ptd, IntPtr hdcTargetDev, IntPtr hdcDraw, [In] NativeMethods.COMRECT lprcBounds, [In] NativeMethods.COMRECT lprcWBounds, IntPtr pfnContinue, [In] int dwContinue);

            [PreserveSig]
            int GetColorSet([In][MarshalAs(UnmanagedType.U4)] int dwDrawAspect, int lindex, IntPtr pvAspect, [In] NativeMethods.tagDVTARGETDEVICE ptd, IntPtr hicTargetDev, [Out] NativeMethods.tagLOGPALETTE ppColorSet);

            [PreserveSig]
            int Freeze([In][MarshalAs(UnmanagedType.U4)] int dwDrawAspect, int lindex, IntPtr pvAspect, [Out] IntPtr pdwFreeze);

            [PreserveSig]
            int Unfreeze([In][MarshalAs(UnmanagedType.U4)] int dwFreeze);

            void SetAdvise([In][MarshalAs(UnmanagedType.U4)] int aspects, [In][MarshalAs(UnmanagedType.U4)] int advf, [In][MarshalAs(UnmanagedType.Interface)] IAdviseSink pAdvSink);

            void GetAdvise([In][Out][MarshalAs(UnmanagedType.LPArray)] int[] paspects, [In][Out][MarshalAs(UnmanagedType.LPArray)] int[] advf, [In][Out][MarshalAs(UnmanagedType.LPArray)] IAdviseSink[] pAdvSink);

            void GetExtent([In][MarshalAs(UnmanagedType.U4)] int dwDrawAspect, int lindex, [In] NativeMethods.tagDVTARGETDEVICE ptd, [Out] NativeMethods.tagSIZEL lpsizel);
        }

        [ComImport]
        [Guid("0000010C-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IPersist
        {
            [SuppressUnmanagedCodeSecurity]
            void GetClassID(out Guid pClassID);
        }

        [ComImport]
        [Guid("37D84F60-42CB-11CE-8135-00AA004BB851")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IPersistPropertyBag
        {
            void GetClassID(out Guid pClassID);

            void InitNew();

            void Load([In][MarshalAs(UnmanagedType.Interface)] IPropertyBag pPropBag, [In][MarshalAs(UnmanagedType.Interface)] IErrorLog pErrorLog);

            void Save([In][MarshalAs(UnmanagedType.Interface)] IPropertyBag pPropBag, [In][MarshalAs(UnmanagedType.Bool)] bool fClearDirty, [In][MarshalAs(UnmanagedType.Bool)] bool fSaveAllProperties);
        }

        [ComImport]
        [Guid("CF51ED10-62FE-11CF-BF86-00A0C9034836")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IQuickActivate
        {
            void QuickActivate([In] tagQACONTAINER pQaContainer, [Out] tagQACONTROL pQaControl);

            void SetContentExtent([In] NativeMethods.tagSIZEL pSizel);

            void GetContentExtent([Out] NativeMethods.tagSIZEL pSizel);
        }

        [ComImport]
        [Guid("55272A00-42CB-11CE-8135-00AA004BB851")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IPropertyBag
        {
            [PreserveSig]
            int Read([In][MarshalAs(UnmanagedType.LPWStr)] string pszPropName, [In][Out] ref object pVar, [In] IErrorLog pErrorLog);

            [PreserveSig]
            int Write([In][MarshalAs(UnmanagedType.LPWStr)] string pszPropName, [In] ref object pVar);
        }

        [ComImport]
        [Guid("3127CA40-446E-11CE-8135-00AA004BB851")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IErrorLog
        {
            void AddError([In][MarshalAs(UnmanagedType.LPWStr)] string pszPropName_p0, [In][MarshalAs(UnmanagedType.Struct)] NativeMethods.tagEXCEPINFO pExcepInfo_p1);
        }

        [ComImport]
        [Guid("00000109-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IPersistStream
        {
            void GetClassID(out Guid pClassId);

            [PreserveSig]
            int IsDirty();

            void Load([In][MarshalAs(UnmanagedType.Interface)] IStream pstm);

            void Save([In][MarshalAs(UnmanagedType.Interface)] IStream pstm, [In][MarshalAs(UnmanagedType.Bool)] bool fClearDirty);

            long GetSizeMax();
        }

        [ComImport]
        [SuppressUnmanagedCodeSecurity]
        [Guid("7FD52380-4E07-101B-AE2D-08002B2EC713")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IPersistStreamInit
        {
            void GetClassID(out Guid pClassID);

            [PreserveSig]
            int IsDirty();

            void Load([In][MarshalAs(UnmanagedType.Interface)] IStream pstm);

            void Save([In][MarshalAs(UnmanagedType.Interface)] IStream pstm, [In][MarshalAs(UnmanagedType.Bool)] bool fClearDirty);

            void GetSizeMax([Out][MarshalAs(UnmanagedType.LPArray)] long pcbSize);

            void InitNew();
        }

        [ComImport]
        [SuppressUnmanagedCodeSecurity]
        [Guid("B196B286-BAB4-101A-B69C-00AA00341D07")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IConnectionPoint
        {
            [PreserveSig]
            int GetConnectionInterface(out Guid iid);

            [PreserveSig]
            int GetConnectionPointContainer([MarshalAs(UnmanagedType.Interface)] ref IConnectionPointContainer pContainer);

            [PreserveSig]
            int Advise([In][MarshalAs(UnmanagedType.Interface)] object pUnkSink, ref int cookie);

            [PreserveSig]
            int Unadvise(int cookie);

            [PreserveSig]
            int EnumConnections(out object pEnum);
        }

        [ComImport]
        [Guid("0000010A-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IPersistStorage
        {
            void GetClassID(out Guid pClassID);

            [PreserveSig]
            int IsDirty();

            void InitNew(IStorage pstg);

            [PreserveSig]
            int Load(IStorage pstg);

            void Save(IStorage pStgSave, bool fSameAsLoad);

            void SaveCompleted(IStorage pStgNew);

            void HandsOffStorage();
        }

        [ComImport]
        [Guid("00020404-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IEnumVariant
        {
            [PreserveSig]
            int Next([In][MarshalAs(UnmanagedType.U4)] int celt, [In][Out] IntPtr rgvar, [Out][MarshalAs(UnmanagedType.LPArray)] int[] pceltFetched);

            void Skip([In][MarshalAs(UnmanagedType.U4)] int celt);

            void Reset();

            void Clone([Out][MarshalAs(UnmanagedType.LPArray)] IEnumVariant[] ppenum);
        }

        [ComImport]
        [Guid("00000104-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IEnumOLEVERB
        {
            [PreserveSig]
            int Next([MarshalAs(UnmanagedType.U4)] int celt, [Out] NativeMethods.tagOLEVERB rgelt, [Out][MarshalAs(UnmanagedType.LPArray)] int[] pceltFetched);

            [PreserveSig]
            int Skip([In][MarshalAs(UnmanagedType.U4)] int celt);

            void Reset();

            void Clone(out IEnumOLEVERB ppenum);
        }

        [ComImport]
        [SuppressUnmanagedCodeSecurity]
        [Guid("00bb2762-6a77-11d0-a535-00c04fd7d062")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IAutoComplete
        {
            int Init([In] HandleRef hwndEdit, [In] IEnumString punkACL, [In] string pwszRegKeyPath, [In] string pwszQuickComplete);

            void Enable([In] bool fEnable);
        }

        [ComImport]
        [SuppressUnmanagedCodeSecurity]
        [Guid("EAC04BC0-3791-11d2-BB95-0060977B464C")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IAutoComplete2
        {
            int Init([In] HandleRef hwndEdit, [In] IEnumString punkACL, [In] string pwszRegKeyPath, [In] string pwszQuickComplete);

            void Enable([In] bool fEnable);

            int SetOptions([In] int dwFlag);

            void GetOptions([Out] IntPtr pdwFlag);
        }

        [ComImport]
        [SuppressUnmanagedCodeSecurity]
        [Guid("0000000C-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IStream
        {
            int Read(IntPtr buf, int len);

            int Write(IntPtr buf, int len);

            [return: MarshalAs(UnmanagedType.I8)]
            long Seek([In][MarshalAs(UnmanagedType.I8)] long dlibMove, int dwOrigin);

            void SetSize([In][MarshalAs(UnmanagedType.I8)] long libNewSize);

            [return: MarshalAs(UnmanagedType.I8)]
            long CopyTo([In][MarshalAs(UnmanagedType.Interface)] IStream pstm, [In][MarshalAs(UnmanagedType.I8)] long cb, [Out][MarshalAs(UnmanagedType.LPArray)] long[] pcbRead);

            void Commit(int grfCommitFlags);

            void Revert();

            void LockRegion([In][MarshalAs(UnmanagedType.I8)] long libOffset, [In][MarshalAs(UnmanagedType.I8)] long cb, int dwLockType);

            void UnlockRegion([In][MarshalAs(UnmanagedType.I8)] long libOffset, [In][MarshalAs(UnmanagedType.I8)] long cb, int dwLockType);

            void Stat([Out] NativeMethods.STATSTG pStatstg, int grfStatFlag);

            [return: MarshalAs(UnmanagedType.Interface)]
            IStream Clone();
        }

        public abstract class CharBuffer
        {
            public static CharBuffer CreateBuffer(int size)
            {
                if (Marshal.SystemDefaultCharSize == 1)
                {
                    return new AnsiCharBuffer(size);
                }
                return new UnicodeCharBuffer(size);
            }

            public abstract IntPtr AllocCoTaskMem();

            public abstract string GetString();

            public abstract void PutCoTaskMem(IntPtr ptr);

            public abstract void PutString(string s);
        }

        public class AnsiCharBuffer : CharBuffer
        {
            internal byte[] buffer;

            internal int offset;

            public AnsiCharBuffer(int size)
            {
                buffer = new byte[size];
            }

            public override IntPtr AllocCoTaskMem()
            {
                IntPtr intPtr = Marshal.AllocCoTaskMem(buffer.Length);
                Marshal.Copy(buffer, 0, intPtr, buffer.Length);
                return intPtr;
            }

            public override string GetString()
            {
                int i;
                for (i = offset; i < buffer.Length && buffer[i] != 0; i++)
                {
                }
                string @string = Encoding.Default.GetString(buffer, offset, i - offset);
                if (i < buffer.Length)
                {
                    i++;
                }
                offset = i;
                return @string;
            }

            public override void PutCoTaskMem(IntPtr ptr)
            {
                Marshal.Copy(ptr, buffer, 0, buffer.Length);
                offset = 0;
            }

            public override void PutString(string s)
            {
                byte[] bytes = Encoding.Default.GetBytes(s);
                int num = Math.Min(bytes.Length, buffer.Length - offset);
                Array.Copy(bytes, 0, buffer, offset, num);
                offset += num;
                if (offset < buffer.Length)
                {
                    buffer[offset++] = 0;
                }
            }
        }

        public class UnicodeCharBuffer : CharBuffer
        {
            internal char[] buffer;

            internal int offset;

            public UnicodeCharBuffer(int size)
            {
                buffer = new char[size];
            }

            public override IntPtr AllocCoTaskMem()
            {
                IntPtr intPtr = Marshal.AllocCoTaskMem(buffer.Length * 2);
                Marshal.Copy(buffer, 0, intPtr, buffer.Length);
                return intPtr;
            }

            public override string GetString()
            {
                int i;
                for (i = offset; i < buffer.Length && buffer[i] != 0; i++)
                {
                }
                string result = new string(buffer, offset, i - offset);
                if (i < buffer.Length)
                {
                    i++;
                }
                offset = i;
                return result;
            }

            public override void PutCoTaskMem(IntPtr ptr)
            {
                Marshal.Copy(ptr, buffer, 0, buffer.Length);
                offset = 0;
            }

            public override void PutString(string s)
            {
                int num = Math.Min(s.Length, buffer.Length - offset);
                s.CopyTo(0, buffer, offset, num);
                offset += num;
                if (offset < buffer.Length)
                {
                    buffer[offset++] = '\0';
                }
            }
        }

        public class ComStreamFromDataStream : IStream
        {
            protected Stream dataStream;

            private long virtualPosition = -1L;

            public ComStreamFromDataStream(Stream dataStream)
            {
                if (dataStream == null)
                {
                    throw new ArgumentNullException("dataStream");
                }
                this.dataStream = dataStream;
            }

            private void ActualizeVirtualPosition()
            {
                if (virtualPosition != -1)
                {
                    if (virtualPosition > dataStream.Length)
                    {
                        dataStream.SetLength(virtualPosition);
                    }
                    dataStream.Position = virtualPosition;
                    virtualPosition = -1L;
                }
            }

            public IStream Clone()
            {
                NotImplemented();
                return null;
            }

            public void Commit(int grfCommitFlags)
            {
                dataStream.Flush();
                ActualizeVirtualPosition();
            }

            public long CopyTo(IStream pstm, long cb, long[] pcbRead)
            {
                int num = 4096;
                IntPtr intPtr = Marshal.AllocHGlobal(num);
                if (intPtr == IntPtr.Zero)
                {
                    throw new OutOfMemoryException();
                }
                long num2 = 0L;
                try
                {
                    int num4;
                    for (; num2 < cb; num2 += num4)
                    {
                        int num3 = num;
                        if (num2 + num3 > cb)
                        {
                            num3 = (int)(cb - num2);
                        }
                        num4 = Read(intPtr, num3);
                        if (num4 == 0)
                        {
                            break;
                        }
                        if (pstm.Write(intPtr, num4) != num4)
                        {
                            throw EFail("Wrote an incorrect number of bytes");
                        }
                    }
                }
                finally
                {
                    Marshal.FreeHGlobal(intPtr);
                }
                if (pcbRead != null && pcbRead.Length != 0)
                {
                    pcbRead[0] = num2;
                }
                return num2;
            }

            public Stream GetDataStream()
            {
                return dataStream;
            }

            public void LockRegion(long libOffset, long cb, int dwLockType)
            {
            }

            protected static ExternalException EFail(string msg)
            {
                ExternalException ex = new ExternalException(msg, -2147467259);
                throw ex;
            }

            protected static void NotImplemented()
            {
                ExternalException ex = new ExternalException("UnsafeNativeMethodsNotImplemented", -2147467263);
                throw ex;
            }

            public int Read(IntPtr buf, int length)
            {
                byte[] array = new byte[length];
                int num = Read(array, length);
                Marshal.Copy(array, 0, buf, num);
                return num;
            }

            public int Read(byte[] buffer, int length)
            {
                ActualizeVirtualPosition();
                return dataStream.Read(buffer, 0, length);
            }

            public void Revert()
            {
                NotImplemented();
            }

            public long Seek(long offset, int origin)
            {
                long position = virtualPosition;
                if (virtualPosition == -1)
                {
                    position = dataStream.Position;
                }
                long length = dataStream.Length;
                switch (origin)
                {
                    case 0:
                        if (offset <= length)
                        {
                            dataStream.Position = offset;
                            virtualPosition = -1L;
                        }
                        else
                        {
                            virtualPosition = offset;
                        }
                        break;
                    case 2:
                        if (offset <= 0)
                        {
                            dataStream.Position = length + offset;
                            virtualPosition = -1L;
                        }
                        else
                        {
                            virtualPosition = length + offset;
                        }
                        break;
                    case 1:
                        if (offset + position <= length)
                        {
                            dataStream.Position = position + offset;
                            virtualPosition = -1L;
                        }
                        else
                        {
                            virtualPosition = offset + position;
                        }
                        break;
                }
                if (virtualPosition != -1)
                {
                    return virtualPosition;
                }
                return dataStream.Position;
            }

            public void SetSize(long value)
            {
                dataStream.SetLength(value);
            }

            public void Stat(NativeMethods.STATSTG pstatstg, int grfStatFlag)
            {
                pstatstg.type = 2;
                pstatstg.cbSize = dataStream.Length;
                pstatstg.grfLocksSupported = 2;
            }

            public void UnlockRegion(long libOffset, long cb, int dwLockType)
            {
            }

            public int Write(IntPtr buf, int length)
            {
                byte[] array = new byte[length];
                Marshal.Copy(buf, array, 0, length);
                return Write(array, length);
            }

            public int Write(byte[] buffer, int length)
            {
                ActualizeVirtualPosition();
                dataStream.Write(buffer, 0, length);
                return length;
            }
        }

        [ComImport]
        [Guid("0000000B-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IStorage
        {
            [return: MarshalAs(UnmanagedType.Interface)]
            IStream CreateStream([In][MarshalAs(UnmanagedType.BStr)] string pwcsName, [In][MarshalAs(UnmanagedType.U4)] int grfMode, [In][MarshalAs(UnmanagedType.U4)] int reserved1, [In][MarshalAs(UnmanagedType.U4)] int reserved2);

            [return: MarshalAs(UnmanagedType.Interface)]
            IStream OpenStream([In][MarshalAs(UnmanagedType.BStr)] string pwcsName, IntPtr reserved1, [In][MarshalAs(UnmanagedType.U4)] int grfMode, [In][MarshalAs(UnmanagedType.U4)] int reserved2);

            [return: MarshalAs(UnmanagedType.Interface)]
            IStorage CreateStorage([In][MarshalAs(UnmanagedType.BStr)] string pwcsName, [In][MarshalAs(UnmanagedType.U4)] int grfMode, [In][MarshalAs(UnmanagedType.U4)] int reserved1, [In][MarshalAs(UnmanagedType.U4)] int reserved2);

            [return: MarshalAs(UnmanagedType.Interface)]
            IStorage OpenStorage([In][MarshalAs(UnmanagedType.BStr)] string pwcsName, IntPtr pstgPriority, [In][MarshalAs(UnmanagedType.U4)] int grfMode, IntPtr snbExclude, [In][MarshalAs(UnmanagedType.U4)] int reserved);

            void CopyTo(int ciidExclude, [In][MarshalAs(UnmanagedType.LPArray)] Guid[] pIIDExclude, IntPtr snbExclude, [In][MarshalAs(UnmanagedType.Interface)] IStorage stgDest);

            void MoveElementTo([In][MarshalAs(UnmanagedType.BStr)] string pwcsName, [In][MarshalAs(UnmanagedType.Interface)] IStorage stgDest, [In][MarshalAs(UnmanagedType.BStr)] string pwcsNewName, [In][MarshalAs(UnmanagedType.U4)] int grfFlags);

            void Commit(int grfCommitFlags);

            void Revert();

            void EnumElements([In][MarshalAs(UnmanagedType.U4)] int reserved1, IntPtr reserved2, [In][MarshalAs(UnmanagedType.U4)] int reserved3, [MarshalAs(UnmanagedType.Interface)] out object ppVal);

            void DestroyElement([In][MarshalAs(UnmanagedType.BStr)] string pwcsName);

            void RenameElement([In][MarshalAs(UnmanagedType.BStr)] string pwcsOldName, [In][MarshalAs(UnmanagedType.BStr)] string pwcsNewName);

            void SetElementTimes([In][MarshalAs(UnmanagedType.BStr)] string pwcsName, [In] NativeMethods.FILETIME pctime, [In] NativeMethods.FILETIME patime, [In] NativeMethods.FILETIME pmtime);

            void SetClass([In] ref Guid clsid);

            void SetStateBits(int grfStateBits, int grfMask);

            void Stat([Out] NativeMethods.STATSTG pStatStg, int grfStatFlag);
        }

        [ComImport]
        [Guid("B196B28F-BAB4-101A-B69C-00AA00341D07")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IClassFactory2
        {
            void CreateInstance([In][MarshalAs(UnmanagedType.Interface)] object unused, [In] ref Guid refiid, [Out][MarshalAs(UnmanagedType.LPArray)] object[] ppunk);

            void LockServer(int fLock);

            void GetLicInfo([Out] NativeMethods.tagLICINFO licInfo);

            void RequestLicKey([In][MarshalAs(UnmanagedType.U4)] int dwReserved, [Out][MarshalAs(UnmanagedType.LPArray)] string[] pBstrKey);

            void CreateInstanceLic([In][MarshalAs(UnmanagedType.Interface)] object pUnkOuter, [In][MarshalAs(UnmanagedType.Interface)] object pUnkReserved, [In] ref Guid riid, [In][MarshalAs(UnmanagedType.BStr)] string bstrKey, [MarshalAs(UnmanagedType.Interface)] out object ppVal);
        }

        [ComImport]
        [SuppressUnmanagedCodeSecurity]
        [Guid("B196B284-BAB4-101A-B69C-00AA00341D07")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IConnectionPointContainer
        {
            [return: MarshalAs(UnmanagedType.Interface)]
            object EnumConnectionPoints();

            [PreserveSig]
            int FindConnectionPoint([In] ref Guid guid, [MarshalAs(UnmanagedType.Interface)] out IConnectionPoint ppCP);
        }

        [ComImport]
        [Guid("B196B285-BAB4-101A-B69C-00AA00341D07")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IEnumConnectionPoints
        {
            [PreserveSig]
            int Next(int cConnections, out IConnectionPoint pCp, out int pcFetched);

            [PreserveSig]
            int Skip(int cSkip);

            void Reset();

            IEnumConnectionPoints Clone();
        }

        [ComImport]
        [Guid("00020400-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IDispatch
        {
            int GetTypeInfoCount();

            [return: MarshalAs(UnmanagedType.Interface)]
            ITypeInfo GetTypeInfo([In][MarshalAs(UnmanagedType.U4)] int iTInfo, [In][MarshalAs(UnmanagedType.U4)] int lcid);

            [PreserveSig]
            int GetIDsOfNames([In] ref Guid riid, [In][MarshalAs(UnmanagedType.LPArray)] string[] rgszNames, [In][MarshalAs(UnmanagedType.U4)] int cNames, [In][MarshalAs(UnmanagedType.U4)] int lcid, [Out][MarshalAs(UnmanagedType.LPArray)] int[] rgDispId);

            [PreserveSig]
            int Invoke(int dispIdMember, [In] ref Guid riid, [In][MarshalAs(UnmanagedType.U4)] int lcid, [In][MarshalAs(UnmanagedType.U4)] int dwFlags, [In][Out] NativeMethods.tagDISPPARAMS pDispParams, [Out][MarshalAs(UnmanagedType.LPArray)] object[] pVarResult, [In][Out] NativeMethods.tagEXCEPINFO pExcepInfo, [Out][MarshalAs(UnmanagedType.LPArray)] IntPtr[] pArgErr);
        }

        [ComImport]
        [Guid("00020401-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface ITypeInfo
        {
            [PreserveSig]
            int GetTypeAttr(ref IntPtr pTypeAttr);

            [PreserveSig]
            int GetTypeComp([Out][MarshalAs(UnmanagedType.LPArray)] ITypeComp[] ppTComp);

            [PreserveSig]
            int GetFuncDesc([In][MarshalAs(UnmanagedType.U4)] int index, ref IntPtr pFuncDesc);

            [PreserveSig]
            int GetVarDesc([In][MarshalAs(UnmanagedType.U4)] int index, ref IntPtr pVarDesc);

            [PreserveSig]
            int GetNames(int memid, [Out][MarshalAs(UnmanagedType.LPArray)] string[] rgBstrNames, [In][MarshalAs(UnmanagedType.U4)] int cMaxNames, [Out][MarshalAs(UnmanagedType.LPArray)] int[] pcNames);

            [PreserveSig]
            int GetRefTypeOfImplType([In][MarshalAs(UnmanagedType.U4)] int index, [Out][MarshalAs(UnmanagedType.LPArray)] int[] pRefType);

            [PreserveSig]
            int GetImplTypeFlags([In][MarshalAs(UnmanagedType.U4)] int index, [Out][MarshalAs(UnmanagedType.LPArray)] int[] pImplTypeFlags);

            [PreserveSig]
            int GetIDsOfNames(IntPtr rgszNames, int cNames, IntPtr pMemId);

            [PreserveSig]
            int Invoke();

            [PreserveSig]
            int GetDocumentation(int memid, ref string pBstrName, ref string pBstrDocString, [Out][MarshalAs(UnmanagedType.LPArray)] int[] pdwHelpContext, [Out][MarshalAs(UnmanagedType.LPArray)] string[] pBstrHelpFile);

            [PreserveSig]
            int GetDllEntry(int memid, NativeMethods.tagINVOKEKIND invkind, [Out][MarshalAs(UnmanagedType.LPArray)] string[] pBstrDllName, [Out][MarshalAs(UnmanagedType.LPArray)] string[] pBstrName, [Out][MarshalAs(UnmanagedType.LPArray)] short[] pwOrdinal);

            [PreserveSig]
            int GetRefTypeInfo(IntPtr hreftype, ref ITypeInfo pTypeInfo);

            [PreserveSig]
            int AddressOfMember();

            [PreserveSig]
            int CreateInstance([In] ref Guid riid, [Out][MarshalAs(UnmanagedType.LPArray)] object[] ppvObj);

            [PreserveSig]
            int GetMops(int memid, [Out][MarshalAs(UnmanagedType.LPArray)] string[] pBstrMops);

            [PreserveSig]
            int GetContainingTypeLib([Out][MarshalAs(UnmanagedType.LPArray)] ITypeLib[] ppTLib, [Out][MarshalAs(UnmanagedType.LPArray)] int[] pIndex);

            [PreserveSig]
            void ReleaseTypeAttr(IntPtr typeAttr);

            [PreserveSig]
            void ReleaseFuncDesc(IntPtr funcDesc);

            [PreserveSig]
            void ReleaseVarDesc(IntPtr varDesc);
        }

        [ComImport]
        [Guid("00020403-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface ITypeComp
        {
            void RemoteBind([In][MarshalAs(UnmanagedType.LPWStr)] string szName, [In][MarshalAs(UnmanagedType.U4)] int lHashVal, [In][MarshalAs(UnmanagedType.U2)] short wFlags, [Out][MarshalAs(UnmanagedType.LPArray)] ITypeInfo[] ppTInfo, [Out][MarshalAs(UnmanagedType.LPArray)] NativeMethods.tagDESCKIND[] pDescKind, [Out][MarshalAs(UnmanagedType.LPArray)] NativeMethods.tagFUNCDESC[] ppFuncDesc, [Out][MarshalAs(UnmanagedType.LPArray)] NativeMethods.tagVARDESC[] ppVarDesc, [Out][MarshalAs(UnmanagedType.LPArray)] ITypeComp[] ppTypeComp, [Out][MarshalAs(UnmanagedType.LPArray)] int[] pDummy);

            void RemoteBindType([In][MarshalAs(UnmanagedType.LPWStr)] string szName, [In][MarshalAs(UnmanagedType.U4)] int lHashVal, [Out][MarshalAs(UnmanagedType.LPArray)] ITypeInfo[] ppTInfo);
        }

        [ComImport]
        [Guid("00020402-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface ITypeLib
        {
            void RemoteGetTypeInfoCount([Out][MarshalAs(UnmanagedType.LPArray)] int[] pcTInfo);

            void GetTypeInfo([In][MarshalAs(UnmanagedType.U4)] int index, [Out][MarshalAs(UnmanagedType.LPArray)] ITypeInfo[] ppTInfo);

            void GetTypeInfoType([In][MarshalAs(UnmanagedType.U4)] int index, [Out][MarshalAs(UnmanagedType.LPArray)] NativeMethods.tagTYPEKIND[] pTKind);

            void GetTypeInfoOfGuid([In] ref Guid guid, [Out][MarshalAs(UnmanagedType.LPArray)] ITypeInfo[] ppTInfo);

            void RemoteGetLibAttr(IntPtr ppTLibAttr, [Out][MarshalAs(UnmanagedType.LPArray)] int[] pDummy);

            void GetTypeComp([Out][MarshalAs(UnmanagedType.LPArray)] ITypeComp[] ppTComp);

            void RemoteGetDocumentation(int index, [In][MarshalAs(UnmanagedType.U4)] int refPtrFlags, [Out][MarshalAs(UnmanagedType.LPArray)] string[] pBstrName, [Out][MarshalAs(UnmanagedType.LPArray)] string[] pBstrDocString, [Out][MarshalAs(UnmanagedType.LPArray)] int[] pdwHelpContext, [Out][MarshalAs(UnmanagedType.LPArray)] string[] pBstrHelpFile);

            void RemoteIsName([In][MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, [In][MarshalAs(UnmanagedType.U4)] int lHashVal, [Out][MarshalAs(UnmanagedType.LPArray)] IntPtr[] pfName, [Out][MarshalAs(UnmanagedType.LPArray)] string[] pBstrLibName);

            void RemoteFindName([In][MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, [In][MarshalAs(UnmanagedType.U4)] int lHashVal, [Out][MarshalAs(UnmanagedType.LPArray)] ITypeInfo[] ppTInfo, [Out][MarshalAs(UnmanagedType.LPArray)] int[] rgMemId, [In][Out][MarshalAs(UnmanagedType.LPArray)] short[] pcFound, [Out][MarshalAs(UnmanagedType.LPArray)] string[] pBstrLibName);

            void LocalReleaseTLibAttr();
        }

        [ComImport]
        [Guid("DF0B3D60-548F-101B-8E65-08002B2BD119")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface ISupportErrorInfo
        {
            int InterfaceSupportsErrorInfo([In] ref Guid riid);
        }

        [ComImport]
        [Guid("1CF2B120-547D-101B-8E65-08002B2BD119")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IErrorInfo
        {
            [PreserveSig]
            [SuppressUnmanagedCodeSecurity]
            int GetGUID(out Guid pguid);

            [PreserveSig]
            [SuppressUnmanagedCodeSecurity]
            int GetSource([In][Out][MarshalAs(UnmanagedType.BStr)] ref string pBstrSource);

            [PreserveSig]
            [SuppressUnmanagedCodeSecurity]
            int GetDescription([In][Out][MarshalAs(UnmanagedType.BStr)] ref string pBstrDescription);

            [PreserveSig]
            [SuppressUnmanagedCodeSecurity]
            int GetHelpFile([In][Out][MarshalAs(UnmanagedType.BStr)] ref string pBstrHelpFile);

            [PreserveSig]
            [SuppressUnmanagedCodeSecurity]
            int GetHelpContext([In][Out][MarshalAs(UnmanagedType.U4)] ref int pdwHelpContext);
        }

        [StructLayout(LayoutKind.Sequential)]
        public sealed class tagQACONTAINER
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize = Marshal.SizeOf(typeof(tagQACONTAINER));

            public IOleClientSite pClientSite;

            [MarshalAs(UnmanagedType.Interface)]
            public object pAdviseSink;

            public IPropertyNotifySink pPropertyNotifySink;

            [MarshalAs(UnmanagedType.Interface)]
            public object pUnkEventSink;

            [MarshalAs(UnmanagedType.U4)]
            public int dwAmbientFlags;

            [MarshalAs(UnmanagedType.U4)]
            public uint colorFore;

            [MarshalAs(UnmanagedType.U4)]
            public uint colorBack;

            [MarshalAs(UnmanagedType.Interface)]
            public object pFont;

            [MarshalAs(UnmanagedType.Interface)]
            public object pUndoMgr;

            [MarshalAs(UnmanagedType.U4)]
            public int dwAppearance;

            public int lcid;

            public IntPtr hpal = IntPtr.Zero;

            [MarshalAs(UnmanagedType.Interface)]
            public object pBindHost;
        }

        [StructLayout(LayoutKind.Sequential)]
        public sealed class tagQACONTROL
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize = Marshal.SizeOf(typeof(tagQACONTROL));

            [MarshalAs(UnmanagedType.U4)]
            public int dwMiscStatus;

            [MarshalAs(UnmanagedType.U4)]
            public int dwViewStatus;

            [MarshalAs(UnmanagedType.U4)]
            public int dwEventCookie;

            [MarshalAs(UnmanagedType.U4)]
            public int dwPropNotifyCookie;

            [MarshalAs(UnmanagedType.U4)]
            public int dwPointerActivationPolicy;
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("E44C3566-915D-4070-99C6-047BFF5A08F5")]
        [ComVisible(true)]
        public interface ILegacyIAccessibleProvider
        {
            int ChildId { get; }

            string Name { get; }

            string Value { get; }

            string Description { get; }

            uint Role { get; }

            uint State { get; }

            string Help { get; }

            string KeyboardShortcut { get; }

            string DefaultAction { get; }

            void Select(int flagsSelect);

            void DoDefaultAction();

            void SetValue([MarshalAs(UnmanagedType.LPWStr)] string szValue);

            [return: MarshalAs(UnmanagedType.Interface)]
            IAccessible GetIAccessible();

            object[] GetSelection();
        }

        [ComImport]
        [Guid("0000000A-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface ILockBytes
        {
            void ReadAt([In][MarshalAs(UnmanagedType.U8)] long ulOffset, [Out] IntPtr pv, [In][MarshalAs(UnmanagedType.U4)] int cb, [Out][MarshalAs(UnmanagedType.LPArray)] int[] pcbRead);

            void WriteAt([In][MarshalAs(UnmanagedType.U8)] long ulOffset, IntPtr pv, [In][MarshalAs(UnmanagedType.U4)] int cb, [Out][MarshalAs(UnmanagedType.LPArray)] int[] pcbWritten);

            void Flush();

            void SetSize([In][MarshalAs(UnmanagedType.U8)] long cb);

            void LockRegion([In][MarshalAs(UnmanagedType.U8)] long libOffset, [In][MarshalAs(UnmanagedType.U8)] long cb, [In][MarshalAs(UnmanagedType.U4)] int dwLockType);

            void UnlockRegion([In][MarshalAs(UnmanagedType.U8)] long libOffset, [In][MarshalAs(UnmanagedType.U8)] long cb, [In][MarshalAs(UnmanagedType.U4)] int dwLockType);

            void Stat([Out] NativeMethods.STATSTG pstatstg, [In][MarshalAs(UnmanagedType.U4)] int grfStatFlag);
        }

        [StructLayout(LayoutKind.Sequential)]
        [SuppressUnmanagedCodeSecurity]
        public class OFNOTIFY
        {
            public IntPtr hdr_hwndFrom = IntPtr.Zero;

            public IntPtr hdr_idFrom = IntPtr.Zero;

            public int hdr_code;

            public IntPtr lpOFN = IntPtr.Zero;

            public IntPtr pszFile = IntPtr.Zero;
        }

        public delegate int BrowseCallbackProc(IntPtr hwnd, int msg, IntPtr lParam, IntPtr lpData);

        [Flags]
        public enum BrowseInfos
        {
            NewDialogStyle = 0x40,
            HideNewFolderButton = 0x200
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class BROWSEINFO
        {
            public IntPtr hwndOwner;

            public IntPtr pidlRoot;

            public IntPtr pszDisplayName;

            public string lpszTitle;

            public int ulFlags;

            public BrowseCallbackProc lpfn;

            public IntPtr lParam;

            public int iImage;
        }

        [SuppressUnmanagedCodeSecurity]
        internal class Shell32
        {
            [DllImport("shell32.dll")]
            public static extern int SHGetSpecialFolderLocation(IntPtr hwnd, int csidl, ref IntPtr ppidl);

            [DllImport("shell32.dll", CharSet = CharSet.Auto)]
            private static extern bool SHGetPathFromIDListEx(IntPtr pidl, IntPtr pszPath, int cchPath, int flags);

            public static bool SHGetPathFromIDListLongPath(IntPtr pidl, ref IntPtr pszPath)
            {
                int num = 1;
                int num2 = 260 * Marshal.SystemDefaultCharSize;
                int num3 = 260;
                bool flag = false;
                while (!(flag = SHGetPathFromIDListEx(pidl, pszPath, num3, 0)) && num3 < 32767)
                {
                    string text = Marshal.PtrToStringAuto(pszPath);
                    if (text.Length != 0 && text.Length < num3)
                    {
                        break;
                    }
                    num += 2;
                    num3 = ((num * num3 >= 32767) ? 32767 : (num * num3));
                    pszPath = Marshal.ReAllocHGlobal(pszPath, (IntPtr)((num3 + 1) * Marshal.SystemDefaultCharSize));
                }
                return flag;
            }

            [DllImport("shell32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr SHBrowseForFolder([In] BROWSEINFO lpbi);

            [DllImport("shell32.dll")]
            public static extern int SHGetMalloc([Out][MarshalAs(UnmanagedType.LPArray)] IMalloc[] ppMalloc);

            [DllImport("shell32.dll")]
            private static extern int SHGetKnownFolderPath(ref Guid rfid, uint dwFlags, IntPtr hToken, out IntPtr pszPath);

            public static int SHGetFolderPathEx(ref Guid rfid, uint dwFlags, IntPtr hToken, StringBuilder pszPath)
            {
                if (IsVista)
                {
                    IntPtr pszPath2 = IntPtr.Zero;
                    int num = -1;
                    if ((num = SHGetKnownFolderPath(ref rfid, dwFlags, hToken, out pszPath2)) == 0)
                    {
                        pszPath.Append(Marshal.PtrToStringAuto(pszPath2));
                        CoTaskMemFree(pszPath2);
                    }
                    return num;
                }
                throw new NotSupportedException();
            }

            [DllImport("shell32.dll")]
            public static extern int SHILCreateFromPath([MarshalAs(UnmanagedType.LPWStr)] string pszPath, out IntPtr ppIdl, ref uint rgflnOut);
        }

        [ComImport]
        [Guid("00000002-0000-0000-c000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [SuppressUnmanagedCodeSecurity]
        public interface IMalloc
        {
            [PreserveSig]
            IntPtr Alloc(int cb);

            [PreserveSig]
            IntPtr Realloc(IntPtr pv, int cb);

            [PreserveSig]
            void Free(IntPtr pv);

            [PreserveSig]
            int GetSize(IntPtr pv);

            [PreserveSig]
            int DidAlloc(IntPtr pv);

            [PreserveSig]
            void HeapMinimize();
        }

        [ComImport]
        [Guid("00000126-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IRunnableObject
        {
            void GetRunningClass(out Guid guid);

            [PreserveSig]
            int Run(IntPtr lpBindContext);

            bool IsRunning();

            void LockRunning(bool fLock, bool fLastUnlockCloses);

            void SetContainedObject(bool fContained);
        }

        [ComImport]
        [ComVisible(true)]
        [Guid("B722BCC7-4E68-101B-A2BC-00AA00404770")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleDocumentSite
        {
            [PreserveSig]
            [return: MarshalAs(UnmanagedType.I4)]
            int ActivateMe([In][MarshalAs(UnmanagedType.Interface)] IOleDocumentView pViewToActivate);
        }

        [ComVisible(true)]
        [Guid("B722BCC6-4E68-101B-A2BC-00AA00404770")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleDocumentView
        {
            void SetInPlaceSite([In][MarshalAs(UnmanagedType.Interface)] IOleInPlaceSite pIPSite);

            [return: MarshalAs(UnmanagedType.Interface)]
            IOleInPlaceSite GetInPlaceSite();

            [return: MarshalAs(UnmanagedType.Interface)]
            object GetDocument();

            void SetRect([In] ref NativeMethods.RECT prcView);

            void GetRect([In][Out] ref NativeMethods.RECT prcView);

            void SetRectComplex([In] NativeMethods.RECT prcView, [In] NativeMethods.RECT prcHScroll, [In] NativeMethods.RECT prcVScroll, [In] NativeMethods.RECT prcSizeBox);

            void Show(bool fShow);

            [PreserveSig]
            int UIActivate(bool fUIActivate);

            void Open();

            [PreserveSig]
            int Close([In][MarshalAs(UnmanagedType.U4)] int dwReserved);

            void SaveViewState([In][MarshalAs(UnmanagedType.Interface)] IStream pstm);

            void ApplyViewState([In][MarshalAs(UnmanagedType.Interface)] IStream pstm);

            void Clone([In][MarshalAs(UnmanagedType.Interface)] IOleInPlaceSite pIPSiteNew, [Out][MarshalAs(UnmanagedType.LPArray)] IOleDocumentView[] ppViewNew);
        }

        [ComImport]
        [Guid("b722bcc5-4e68-101b-a2bc-00aa00404770")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleDocument
        {
            [PreserveSig]
            int CreateView(IOleInPlaceSite pIPSite, IStream pstm, int dwReserved, out IOleDocumentView ppView);

            [PreserveSig]
            int GetDocMiscStatus(out int pdwStatus);

            int EnumViews(out object ppEnum, out IOleDocumentView ppView);
        }

        [ComImport]
        [Guid("0000011e-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IOleCache
        {
            int Cache(ref FORMATETC pformatetc, int advf);

            void Uncache(int dwConnection);

            object EnumCache();

            void InitCache(System.Runtime.InteropServices.ComTypes.IDataObject pDataObject);

            void SetData(ref FORMATETC pformatetc, ref STGMEDIUM pmedium, bool fRelease);
        }

        [ComImport]
        [TypeLibType(4176)]
        [Guid("618736E0-3C3D-11CF-810C-00AA00389B71")]
        public interface IAccessibleInternal
        {
            [DispId(-5000)]
            [TypeLibFunc(64)]
            [return: MarshalAs(UnmanagedType.IDispatch)]
            object get_accParent();

            [DispId(-5001)]
            [TypeLibFunc(64)]
            int get_accChildCount();

            [TypeLibFunc(64)]
            [DispId(-5002)]
            [return: MarshalAs(UnmanagedType.IDispatch)]
            object get_accChild([In][MarshalAs(UnmanagedType.Struct)] object varChild);

            [DispId(-5003)]
            [TypeLibFunc(64)]
            [return: MarshalAs(UnmanagedType.BStr)]
            string get_accName([Optional][In][MarshalAs(UnmanagedType.Struct)] object varChild);

            [TypeLibFunc(64)]
            [DispId(-5004)]
            [return: MarshalAs(UnmanagedType.BStr)]
            string get_accValue([Optional][In][MarshalAs(UnmanagedType.Struct)] object varChild);

            [DispId(-5005)]
            [TypeLibFunc(64)]
            [return: MarshalAs(UnmanagedType.BStr)]
            string get_accDescription([Optional][In][MarshalAs(UnmanagedType.Struct)] object varChild);

            [DispId(-5006)]
            [TypeLibFunc(64)]
            [return: MarshalAs(UnmanagedType.Struct)]
            object get_accRole([Optional][In][MarshalAs(UnmanagedType.Struct)] object varChild);

            [TypeLibFunc(64)]
            [DispId(-5007)]
            [return: MarshalAs(UnmanagedType.Struct)]
            object get_accState([Optional][In][MarshalAs(UnmanagedType.Struct)] object varChild);

            [TypeLibFunc(64)]
            [DispId(-5008)]
            [return: MarshalAs(UnmanagedType.BStr)]
            string get_accHelp([Optional][In][MarshalAs(UnmanagedType.Struct)] object varChild);

            [DispId(-5009)]
            [TypeLibFunc(64)]
            int get_accHelpTopic([MarshalAs(UnmanagedType.BStr)] out string pszHelpFile, [Optional][In][MarshalAs(UnmanagedType.Struct)] object varChild);

            [DispId(-5010)]
            [TypeLibFunc(64)]
            [return: MarshalAs(UnmanagedType.BStr)]
            string get_accKeyboardShortcut([Optional][In][MarshalAs(UnmanagedType.Struct)] object varChild);

            [DispId(-5011)]
            [TypeLibFunc(64)]
            [return: MarshalAs(UnmanagedType.Struct)]
            object get_accFocus();

            [DispId(-5012)]
            [TypeLibFunc(64)]
            [return: MarshalAs(UnmanagedType.Struct)]
            object get_accSelection();

            [TypeLibFunc(64)]
            [DispId(-5013)]
            [return: MarshalAs(UnmanagedType.BStr)]
            string get_accDefaultAction([Optional][In][MarshalAs(UnmanagedType.Struct)] object varChild);

            [DispId(-5014)]
            [TypeLibFunc(64)]
            void accSelect([In] int flagsSelect, [Optional][In][MarshalAs(UnmanagedType.Struct)] object varChild);

            [DispId(-5015)]
            [TypeLibFunc(64)]
            void accLocation(out int pxLeft, out int pyTop, out int pcxWidth, out int pcyHeight, [Optional][In][MarshalAs(UnmanagedType.Struct)] object varChild);

            [TypeLibFunc(64)]
            [DispId(-5016)]
            [return: MarshalAs(UnmanagedType.Struct)]
            object accNavigate([In] int navDir, [Optional][In][MarshalAs(UnmanagedType.Struct)] object varStart);

            [TypeLibFunc(64)]
            [DispId(-5017)]
            [return: MarshalAs(UnmanagedType.Struct)]
            object accHitTest([In] int xLeft, [In] int yTop);

            [TypeLibFunc(64)]
            [DispId(-5018)]
            void accDoDefaultAction([Optional][In][MarshalAs(UnmanagedType.Struct)] object varChild);

            [TypeLibFunc(64)]
            [DispId(-5003)]
            void set_accName([Optional][In][MarshalAs(UnmanagedType.Struct)] object varChild, [In][MarshalAs(UnmanagedType.BStr)] string pszName);

            [TypeLibFunc(64)]
            [DispId(-5004)]
            void set_accValue([Optional][In][MarshalAs(UnmanagedType.Struct)] object varChild, [In][MarshalAs(UnmanagedType.BStr)] string pszValue);
        }

        [ComImport]
        [Guid("BEF6E002-A874-101A-8BBA-00AA00300CAB")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IFont
        {
            [return: MarshalAs(UnmanagedType.BStr)]
            string GetName();

            void SetName([In][MarshalAs(UnmanagedType.BStr)] string pname);

            [return: MarshalAs(UnmanagedType.U8)]
            long GetSize();

            void SetSize([In][MarshalAs(UnmanagedType.U8)] long psize);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetBold();

            void SetBold([In][MarshalAs(UnmanagedType.Bool)] bool pbold);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetItalic();

            void SetItalic([In][MarshalAs(UnmanagedType.Bool)] bool pitalic);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetUnderline();

            void SetUnderline([In][MarshalAs(UnmanagedType.Bool)] bool punderline);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetStrikethrough();

            void SetStrikethrough([In][MarshalAs(UnmanagedType.Bool)] bool pstrikethrough);

            [return: MarshalAs(UnmanagedType.I2)]
            short GetWeight();

            void SetWeight([In][MarshalAs(UnmanagedType.I2)] short pweight);

            [return: MarshalAs(UnmanagedType.I2)]
            short GetCharset();

            void SetCharset([In][MarshalAs(UnmanagedType.I2)] short pcharset);

            IntPtr GetHFont();

            void Clone(out IFont ppfont);

            [PreserveSig]
            int IsEqual([In][MarshalAs(UnmanagedType.Interface)] IFont pfontOther);

            void SetRatio(int cyLogical, int cyHimetric);

            void QueryTextMetrics(out IntPtr ptm);

            void AddRefHfont(IntPtr hFont);

            void ReleaseHfont(IntPtr hFont);

            void SetHdc(IntPtr hdc);
        }

        [ComImport]
        [Guid("7BF80980-BF32-101A-8BBB-00AA00300CAB")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IPicture
        {
            IntPtr GetHandle();

            IntPtr GetHPal();

            [return: MarshalAs(UnmanagedType.I2)]
            short GetPictureType();

            int GetWidth();

            int GetHeight();

            void Render(IntPtr hDC, int x, int y, int cx, int cy, int xSrc, int ySrc, int cxSrc, int cySrc, IntPtr rcBounds);

            void SetHPal(IntPtr phpal);

            IntPtr GetCurDC();

            void SelectPicture(IntPtr hdcIn, [Out][MarshalAs(UnmanagedType.LPArray)] IntPtr[] phdcOut, [Out][MarshalAs(UnmanagedType.LPArray)] IntPtr[] phbmpOut);

            [return: MarshalAs(UnmanagedType.Bool)]
            bool GetKeepOriginalFormat();

            void SetKeepOriginalFormat([In][MarshalAs(UnmanagedType.Bool)] bool pfkeep);

            void PictureChanged();

            [PreserveSig]
            int SaveAsFile([In][MarshalAs(UnmanagedType.Interface)] IStream pstm, int fSaveMemCopy, out int pcbSize);

            int GetAttributes();
        }

        [ComImport]
        [Guid("7BF80981-BF32-101A-8BBB-00AA00300CAB")]
        [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        public interface IPictureDisp
        {
            IntPtr Handle { get; }

            IntPtr HPal { get; }

            short PictureType { get; }

            int Width { get; }

            int Height { get; }

            void Render(IntPtr hdc, int x, int y, int cx, int cy, int xSrc, int ySrc, int cxSrc, int cySrc);
        }

        [SuppressUnmanagedCodeSecurity]
        internal class ThemingScope
        {
            private struct ACTCTX
            {
                public int cbSize;

                public uint dwFlags;

                public string lpSource;

                public ushort wProcessorArchitecture;

                public ushort wLangId;

                public string lpAssemblyDirectory;

                public IntPtr lpResourceName;

                public string lpApplicationName;
            }

            private static ACTCTX enableThemingActivationContext;

            private static IntPtr hActCtx;

            private static bool contextCreationSucceeded;

            private const int ACTCTX_FLAG_ASSEMBLY_DIRECTORY_VALID = 4;

            private const int ACTCTX_FLAG_RESOURCE_NAME_VALID = 8;

            private static bool IsContextActive()
            {
                IntPtr handle = IntPtr.Zero;
                if (contextCreationSucceeded && GetCurrentActCtx(out handle))
                {
                    return handle == hActCtx;
                }
                return false;
            }

            public static IntPtr Activate()
            {
                IntPtr lpCookie = IntPtr.Zero;
                if (Application.UseVisualStyles && contextCreationSucceeded && OSFeature.Feature.IsPresent(OSFeature.Themes) && !IsContextActive() && !ActivateActCtx(hActCtx, out lpCookie))
                {
                    lpCookie = IntPtr.Zero;
                }
                return lpCookie;
            }

            public static IntPtr Deactivate(IntPtr userCookie)
            {
                if (userCookie != IntPtr.Zero && OSFeature.Feature.IsPresent(OSFeature.Themes) && DeactivateActCtx(0, userCookie))
                {
                    userCookie = IntPtr.Zero;
                }
                return userCookie;
            }

            public static bool CreateActivationContext(string dllPath, int nativeResourceManifestID)
            {
                lock (typeof(ThemingScope))
                {
                    if (!contextCreationSucceeded && OSFeature.Feature.IsPresent(OSFeature.Themes))
                    {
                        enableThemingActivationContext = default(ACTCTX);
                        enableThemingActivationContext.cbSize = Marshal.SizeOf(typeof(ACTCTX));
                        enableThemingActivationContext.lpSource = dllPath;
                        enableThemingActivationContext.lpResourceName = (IntPtr)nativeResourceManifestID;
                        enableThemingActivationContext.dwFlags = 8u;
                        hActCtx = CreateActCtx(ref enableThemingActivationContext);
                        contextCreationSucceeded = hActCtx != new IntPtr(-1);
                    }
                    return contextCreationSucceeded;
                }
            }

            [DllImport("kernel32.dll")]
            private static extern IntPtr CreateActCtx(ref ACTCTX actctx);

            [DllImport("kernel32.dll")]
            private static extern bool ActivateActCtx(IntPtr hActCtx, out IntPtr lpCookie);

            [DllImport("kernel32.dll")]
            private static extern bool DeactivateActCtx(int dwFlags, IntPtr lpCookie);

            [DllImport("kernel32.dll")]
            private static extern bool GetCurrentActCtx(out IntPtr handle);
        }

        [StructLayout(LayoutKind.Sequential)]
        [SuppressUnmanagedCodeSecurity]
        internal class PROCESS_INFORMATION
        {
            public IntPtr hProcess = IntPtr.Zero;

            public IntPtr hThread = IntPtr.Zero;

            public int dwProcessId;

            public int dwThreadId;

            private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

            ~PROCESS_INFORMATION()
            {
                Close();
            }

            internal void Close()
            {
                if (hProcess != (IntPtr)0 && hProcess != INVALID_HANDLE_VALUE)
                {
                    CloseHandle(new HandleRef(this, hProcess));
                    hProcess = INVALID_HANDLE_VALUE;
                }
                if (hThread != (IntPtr)0 && hThread != INVALID_HANDLE_VALUE)
                {
                    CloseHandle(new HandleRef(this, hThread));
                    hThread = INVALID_HANDLE_VALUE;
                }
            }

            [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
            private static extern bool CloseHandle(HandleRef handle);
        }

        [ComVisible(true)]
        [Guid("e4cfef41-071d-472c-a65c-c14f59ea81eb")]
        public enum StructureChangeType
        {
            ChildAdded,
            ChildRemoved,
            ChildrenInvalidated,
            ChildrenBulkAdded,
            ChildrenBulkRemoved,
            ChildrenReordered
        }

        [ComVisible(true)]
        [Guid("76d12d7e-b227-4417-9ce2-42642ffa896a")]
        public enum ExpandCollapseState
        {
            Collapsed,
            Expanded,
            PartiallyExpanded,
            LeafNode
        }

        [Flags]
        public enum ProviderOptions
        {
            ClientSideProvider = 0x1,
            ServerSideProvider = 0x2,
            NonClientAreaProvider = 0x4,
            OverrideProvider = 0x8,
            ProviderOwnsSetFocus = 0x10,
            UseComThreading = 0x20
        }

        [ComImport]
        [ComVisible(true)]
        [Guid("fb8b03af-3bdf-48d4-bd36-1a65793be168")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface ISelectionProvider
        {
            bool CanSelectMultiple
            {
                [return: MarshalAs(UnmanagedType.Bool)]
                get;
            }

            bool IsSelectionRequired
            {
                [return: MarshalAs(UnmanagedType.Bool)]
                get;
            }

            [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN)]
            object[] GetSelection();
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [ComVisible(true)]
        [Guid("2acad808-b2d4-452d-a407-91ff1ad167b2")]
        public interface ISelectionItemProvider
        {
            bool IsSelected
            {
                [return: MarshalAs(UnmanagedType.Bool)]
                get;
            }

            IRawElementProviderSimple SelectionContainer
            {
                [return: MarshalAs(UnmanagedType.Interface)]
                get;
            }

            void Select();

            void AddToSelection();

            void RemoveFromSelection();
        }

        [ComVisible(true)]
        [Guid("1d5df27c-8947-4425-b8d9-79787bb460b8")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IRawElementProviderHwndOverride : IRawElementProviderSimple
        {
            [return: MarshalAs(UnmanagedType.Interface)]
            IRawElementProviderSimple GetOverrideProviderForHwnd(IntPtr hwnd);
        }

        [ComImport]
        [Guid("6D5140C1-7436-11CE-8034-00AA006009FA")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface IServiceProvider
        {
            [PreserveSig]
            [SuppressUnmanagedCodeSecurity]
            [SecurityCritical]
            int QueryService(ref Guid service, ref Guid riid, out IntPtr ppvObj);
        }

        [ComImport]
        [ComVisible(true)]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [CLSCompliant(false)]
        [Guid("F8B80ADA-2C44-48D0-89BE-5FF23C9CD875")]
        internal interface IAccessibleEx
        {
            [return: MarshalAs(UnmanagedType.IUnknown)]
            object GetObjectForChild(int idChild);

            [PreserveSig]
            [return: MarshalAs(UnmanagedType.I4)]
            int GetIAccessiblePair([MarshalAs(UnmanagedType.Interface)] out object ppAcc, out int pidChild);

            [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_I4)]
            int[] GetRuntimeId();

            [PreserveSig]
            [return: MarshalAs(UnmanagedType.I4)]
            int ConvertReturnedElement([In][MarshalAs(UnmanagedType.Interface)] object pIn, [MarshalAs(UnmanagedType.Interface)] out object ppRetValOut);
        }

        [ComImport]
        [ComVisible(true)]
        [Guid("d847d3a5-cab0-4a98-8c32-ecb45c59ad24")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IExpandCollapseProvider
        {
            ExpandCollapseState ExpandCollapseState { get; }

            void Expand();

            void Collapse();
        }

        [ComImport]
        [ComVisible(true)]
        [Guid("c7935180-6fb3-4201-b174-7df73adbf64a")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IValueProvider
        {
            string Value { get; }

            bool IsReadOnly
            {
                [return: MarshalAs(UnmanagedType.Bool)]
                get;
            }

            void SetValue([MarshalAs(UnmanagedType.LPWStr)] string value);
        }

        [ComImport]
        [ComVisible(true)]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("36dc7aef-33e6-4691-afe1-2be7274b3d33")]
        public interface IRangeValueProvider
        {
            double Value { get; }

            bool IsReadOnly
            {
                [return: MarshalAs(UnmanagedType.Bool)]
                get;
            }

            double Maximum { get; }

            double Minimum { get; }

            double LargeChange { get; }

            double SmallChange { get; }

            void SetValue(double value);
        }

        [ComImport]
        [ComVisible(true)]
        [Guid("D6DD68D1-86FD-4332-8666-9ABEDEA2D24C")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [CLSCompliant(false)]
        public interface IRawElementProviderSimple
        {
            ProviderOptions ProviderOptions { get; }

            IRawElementProviderSimple HostRawElementProvider { get; }

            [return: MarshalAs(UnmanagedType.IUnknown)]
            object GetPatternProvider(int patternId);

            object GetPropertyValue(int propertyId);
        }

        [ComVisible(true)]
        [Guid("670c3006-bf4c-428b-8534-e1848f645122")]
        public enum NavigateDirection
        {
            Parent,
            NextSibling,
            PreviousSibling,
            FirstChild,
            LastChild
        }

        [ComImport]
        [ComVisible(true)]
        [Guid("f7063da8-8359-439c-9297-bbc5299a7d87")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [CLSCompliant(false)]
        public interface IRawElementProviderFragment : IRawElementProviderSimple
        {
            NativeMethods.UiaRect BoundingRectangle { get; }

            IRawElementProviderFragmentRoot FragmentRoot
            {
                [return: MarshalAs(UnmanagedType.Interface)]
                get;
            }

            [return: MarshalAs(UnmanagedType.IUnknown)]
            object Navigate(NavigateDirection direction);

            int[] GetRuntimeId();

            [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN)]
            object[] GetEmbeddedFragmentRoots();

            void SetFocus();
        }

        [ComImport]
        [ComVisible(true)]
        [Guid("620ce2a5-ab8f-40a9-86cb-de3c75599b58")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [CLSCompliant(false)]
        public interface IRawElementProviderFragmentRoot : IRawElementProviderFragment, IRawElementProviderSimple
        {
            [return: MarshalAs(UnmanagedType.IUnknown)]
            object ElementProviderFromPoint(double x, double y);

            [return: MarshalAs(UnmanagedType.IUnknown)]
            object GetFocus();
        }

        [Flags]
        public enum ToggleState
        {
            ToggleState_Off = 0x0,
            ToggleState_On = 0x1,
            ToggleState_Indeterminate = 0x2
        }

        [ComImport]
        [ComVisible(true)]
        [Guid("56D00BD0-C4F4-433C-A836-1A52A57E0892")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [CLSCompliant(false)]
        public interface IToggleProvider
        {
            ToggleState ToggleState { get; }

            void Toggle();
        }

        [Flags]
        public enum RowOrColumnMajor
        {
            RowOrColumnMajor_RowMajor = 0x0,
            RowOrColumnMajor_ColumnMajor = 0x1,
            RowOrColumnMajor_Indeterminate = 0x2
        }

        [ComImport]
        [ComVisible(true)]
        [Guid("9c860395-97b3-490a-b52a-858cc22af166")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [CLSCompliant(false)]
        public interface ITableProvider
        {
            RowOrColumnMajor RowOrColumnMajor { get; }

            [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN)]
            object[] GetRowHeaders();

            [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN)]
            object[] GetColumnHeaders();
        }

        [ComImport]
        [ComVisible(true)]
        [Guid("b9734fa6-771f-4d78-9c90-2517999349cd")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [CLSCompliant(false)]
        public interface ITableItemProvider
        {
            [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN)]
            object[] GetRowHeaderItems();

            [return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN)]
            object[] GetColumnHeaderItems();
        }

        [ComImport]
        [ComVisible(true)]
        [Guid("b17d6187-0907-464b-a168-0ef17a1572b1")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [CLSCompliant(false)]
        public interface IGridProvider
        {
            int RowCount
            {
                [return: MarshalAs(UnmanagedType.I4)]
                get;
            }

            int ColumnCount
            {
                [return: MarshalAs(UnmanagedType.I4)]
                get;
            }

            [return: MarshalAs(UnmanagedType.IUnknown)]
            object GetItem(int row, int column);
        }

        [ComImport]
        [ComVisible(true)]
        [Guid("d02541f1-fb81-4d64-ae32-f520f8a6dbd1")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [CLSCompliant(false)]
        public interface IGridItemProvider
        {
            int Row
            {
                [return: MarshalAs(UnmanagedType.I4)]
                get;
            }

            int Column
            {
                [return: MarshalAs(UnmanagedType.I4)]
                get;
            }

            int RowSpan
            {
                [return: MarshalAs(UnmanagedType.I4)]
                get;
            }

            int ColumnSpan
            {
                [return: MarshalAs(UnmanagedType.I4)]
                get;
            }

            IRawElementProviderSimple ContainingGrid
            {
                [return: MarshalAs(UnmanagedType.Interface)]
                get;
            }
        }

        [ComImport]
        [ComVisible(true)]
        [Guid("54fcb24b-e18e-47a2-b4d3-eccbe77599a2")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [CLSCompliant(false)]
        public interface IInvokeProvider
        {
            void Invoke();
        }

        [ComImport]
        [ComVisible(true)]
        [Guid("2360c714-4bf1-4b26-ba65-9b21316127eb")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [CLSCompliant(false)]
        public interface IScrollItemProvider
        {
            void ScrollIntoView();
        }

        private static readonly Version VistaOSVersion = new Version(6, 0);

        public const int MB_PRECOMPOSED = 1;

        public const int SMTO_ABORTIFHUNG = 2;

        public const int LAYOUT_RTL = 1;

        public const int LAYOUT_BITMAPORIENTATIONPRESERVED = 8;

        public static readonly Guid guid_IAccessibleEx = new Guid("{F8B80ADA-2C44-48D0-89BE-5FF23C9CD875}");

        internal static bool IsVista
        {
            get
            {
                OperatingSystem oSVersion = Environment.OSVersion;
                if (oSVersion == null)
                {
                    return false;
                }
                if (oSVersion.Platform == PlatformID.Win32NT)
                {
                    return oSVersion.Version.CompareTo(VistaOSVersion) >= 0;
                }
                return false;
            }
        }

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        internal static extern uint SHLoadIndirectString(string pszSource, StringBuilder pszOutBuf, uint cchOutBuf, IntPtr ppvReserved);

        [DllImport("ole32.dll")]
        public static extern int ReadClassStg(HandleRef pStg, [In][Out] ref Guid pclsid);

        [DllImport("ole32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        internal static extern void CoTaskMemFree(IntPtr pv);

        [DllImport("user32.dll")]
        public static extern int GetClassName(HandleRef hwnd, StringBuilder lpClassName, int nMaxCount);

        public static IntPtr SetClassLong(HandleRef hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 4)
            {
                return SetClassLongPtr32(hWnd, nIndex, dwNewLong);
            }
            return SetClassLongPtr64(hWnd, nIndex, dwNewLong);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetClassLong")]
        public static extern IntPtr SetClassLongPtr32(HandleRef hwnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetClassLongPtr")]
        public static extern IntPtr SetClassLongPtr64(HandleRef hwnd, int nIndex, IntPtr dwNewLong);

        [DllImport("ole32.dll", ExactSpelling = true, PreserveSig = false)]
        public static extern IClassFactory2 CoGetClassObject([In] ref Guid clsid, int dwContext, int serverInfo, [In] ref Guid refiid);

        [DllImport("ole32.dll", ExactSpelling = true, PreserveSig = false)]
        [return: MarshalAs(UnmanagedType.Interface)]
        public static extern object CoCreateInstance([In] ref Guid clsid, [MarshalAs(UnmanagedType.Interface)] object punkOuter, int context, [In] ref Guid iid);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetLocaleInfo(int Locale, int LCType, StringBuilder lpLCData, int cchData);

        [DllImport("ole32.dll")]
        public static extern int WriteClassStm(IStream pStream, ref Guid clsid);

        [DllImport("ole32.dll")]
        public static extern int ReadClassStg(IStorage pStorage, out Guid clsid);

        [DllImport("ole32.dll")]
        public static extern int ReadClassStm(IStream pStream, out Guid clsid);

        [DllImport("ole32.dll")]
        public static extern int OleLoadFromStream(IStream pStorage, ref Guid iid, out IOleObject pObject);

        [DllImport("ole32.dll")]
        public static extern int OleSaveToStream(IPersistStream pPersistStream, IStream pStream);

        [DllImport("ole32.dll")]
        public static extern int CoGetMalloc(int dwReserved, out IMalloc pMalloc);

        [DllImport("comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool PageSetupDlg([In][Out] NativeMethods.PAGESETUPDLG lppsd);

        [DllImport("comdlg32.dll", CharSet = CharSet.Auto, EntryPoint = "PrintDlg", SetLastError = true)]
        public static extern bool PrintDlg_32([In][Out] NativeMethods.PRINTDLG_32 lppd);

        [DllImport("comdlg32.dll", CharSet = CharSet.Auto, EntryPoint = "PrintDlg", SetLastError = true)]
        public static extern bool PrintDlg_64([In][Out] NativeMethods.PRINTDLG_64 lppd);

        public static bool PrintDlg([In][Out] NativeMethods.PRINTDLG lppd)
        {
            if (IntPtr.Size == 4)
            {
                NativeMethods.PRINTDLG_32 pRINTDLG_ = lppd as NativeMethods.PRINTDLG_32;
                if (pRINTDLG_ == null)
                {
                    throw new NullReferenceException("PRINTDLG data is null");
                }
                return PrintDlg_32(pRINTDLG_);
            }
            NativeMethods.PRINTDLG_64 pRINTDLG_2 = lppd as NativeMethods.PRINTDLG_64;
            if (pRINTDLG_2 == null)
            {
                throw new NullReferenceException("PRINTDLG data is null");
            }
            return PrintDlg_64(pRINTDLG_2);
        }

        [DllImport("comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int PrintDlgEx([In][Out] NativeMethods.PRINTDLGEX lppdex);

        [DllImport("ole32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int OleGetClipboard(ref System.Runtime.InteropServices.ComTypes.IDataObject data);

        [DllImport("ole32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int OleSetClipboard(System.Runtime.InteropServices.ComTypes.IDataObject pDataObj);

        [DllImport("ole32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int OleFlushClipboard();

        [DllImport("oleaut32.dll", ExactSpelling = true)]
        public static extern void OleCreatePropertyFrameIndirect(NativeMethods.OCPFIPARAMS p);

        [DllImport("oleaut32.dll", EntryPoint = "OleCreateFontIndirect", ExactSpelling = true, PreserveSig = false)]
        public static extern IFont OleCreateIFontIndirect(NativeMethods.FONTDESC fd, ref Guid iid);

        [DllImport("oleaut32.dll", PreserveSig = false)]
        public static extern IPicture OleCreatePictureIndirect(NativeMethods.PICTDESC pictdesc, [In] ref Guid refiid, bool fOwn);

        [DllImport("oleaut32.dll", PreserveSig = false)]
        public static extern IFont OleCreateFontIndirect(NativeMethods.tagFONTDESC fontdesc, [In] ref Guid refiid);

        [DllImport("oleaut32.dll", ExactSpelling = true)]
        public static extern int VarFormat(ref object pvarIn, HandleRef pstrFormat, int iFirstDay, int iFirstWeek, uint dwFlags, [In][Out] ref IntPtr pbstr);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern int DragQueryFile(HandleRef hDrop, int iFile, StringBuilder lpszFile, int cch);

        public static int DragQueryFileLongPath(HandleRef hDrop, int iFile, StringBuilder lpszFile)
        {
            if (lpszFile != null && lpszFile.Capacity != 0 && iFile != -1)
            {
                int num = 0;
                if ((num = DragQueryFile(hDrop, iFile, lpszFile, lpszFile.Capacity)) == lpszFile.Capacity)
                {
                    int num2 = DragQueryFile(hDrop, iFile, null, 0);
                    if (num2 < 32767)
                    {
                        lpszFile.EnsureCapacity(num2);
                        num = DragQueryFile(hDrop, iFile, lpszFile, num2);
                    }
                    else
                    {
                        num = 0;
                    }
                }
                lpszFile.Length = num;
                return num;
            }
            return DragQueryFile(hDrop, iFile, lpszFile, lpszFile.Capacity);
        }

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern bool EnumChildWindows(HandleRef hwndParent, NativeMethods.EnumChildrenCallback lpEnumFunc, HandleRef lParam);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr ShellExecute(HandleRef hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowCmd);

        [DllImport("shell32.dll", BestFitMapping = false, CharSet = CharSet.Auto, EntryPoint = "ShellExecute")]
        public static extern IntPtr ShellExecute_NoBFM(HandleRef hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowCmd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int SetScrollPos(HandleRef hWnd, int nBar, int nPos, bool bRedraw);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern bool EnableScrollBar(HandleRef hWnd, int nBar, int value);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern int Shell_NotifyIcon(int message, NativeMethods.NOTIFYICONDATA pnid);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool InsertMenuItem(HandleRef hMenu, int uItem, bool fByPosition, NativeMethods.MENUITEMINFO_T lpmii);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetMenu(HandleRef hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetMenuItemInfo(HandleRef hMenu, int uItem, bool fByPosition, [In][Out] NativeMethods.MENUITEMINFO_T lpmii);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetMenuItemInfo(HandleRef hMenu, int uItem, bool fByPosition, [In][Out] NativeMethods.MENUITEMINFO_T_RW lpmii);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SetMenuItemInfo(HandleRef hMenu, int uItem, bool fByPosition, NativeMethods.MENUITEMINFO_T lpmii);

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateMenu", ExactSpelling = true)]
        private static extern IntPtr IntCreateMenu();

        public static IntPtr CreateMenu()
        {
            return System.Internal.HandleCollector.Add(IntCreateMenu(), NativeMethods.CommonHandles.Menu);
        }

        [DllImport("comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetOpenFileName([In][Out] NativeMethods.OPENFILENAME_I ofn);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern bool EndDialog(HandleRef hWnd, IntPtr result);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        public static extern int MultiByteToWideChar(int CodePage, int dwFlags, byte[] lpMultiByteStr, int cchMultiByte, char[] lpWideCharStr, int cchWideChar);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern int WideCharToMultiByte(int codePage, int flags, [MarshalAs(UnmanagedType.LPWStr)] string wideStr, int chars, [In][Out] byte[] pOutBytes, int bufferBytes, IntPtr defaultChar, IntPtr pDefaultUsed);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, EntryPoint = "RtlMoveMemory", ExactSpelling = true, SetLastError = true)]
        public static extern void CopyMemory(HandleRef destData, HandleRef srcData, int size);

        [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory", ExactSpelling = true)]
        public static extern void CopyMemory(IntPtr pdst, byte[] psrc, int cb);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "RtlMoveMemory", ExactSpelling = true)]
        public static extern void CopyMemoryW(IntPtr pdst, string psrc, int cb);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "RtlMoveMemory", ExactSpelling = true)]
        public static extern void CopyMemoryW(IntPtr pdst, char[] psrc, int cb);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, EntryPoint = "RtlMoveMemory", ExactSpelling = true)]
        public static extern void CopyMemoryA(IntPtr pdst, string psrc, int cb);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, EntryPoint = "RtlMoveMemory", ExactSpelling = true)]
        public static extern void CopyMemoryA(IntPtr pdst, char[] psrc, int cb);

        [DllImport("kernel32.dll", EntryPoint = "DuplicateHandle", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr IntDuplicateHandle(HandleRef processSource, HandleRef handleSource, HandleRef processTarget, ref IntPtr handleTarget, int desiredAccess, bool inheritHandle, int options);

        public static IntPtr DuplicateHandle(HandleRef processSource, HandleRef handleSource, HandleRef processTarget, ref IntPtr handleTarget, int desiredAccess, bool inheritHandle, int options)
        {
            IntPtr result = IntDuplicateHandle(processSource, handleSource, processTarget, ref handleTarget, desiredAccess, inheritHandle, options);
            System.Internal.HandleCollector.Add(handleTarget, NativeMethods.CommonHandles.Kernel);
            return result;
        }

        [DllImport("ole32.dll", PreserveSig = false)]
        public static extern IStorage StgOpenStorageOnILockBytes(ILockBytes iLockBytes, IStorage pStgPriority, int grfMode, int sndExcluded, int reserved);

        [DllImport("ole32.dll", PreserveSig = false)]
        public static extern IntPtr GetHGlobalFromILockBytes(ILockBytes pLkbyt);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowsHookEx(int hookid, NativeMethods.HookProc pfnhook, HandleRef hinst, int threadid);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int GetKeyboardState(byte[] keystate);

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "keybd_event", ExactSpelling = true)]
        public static extern void Keybd_event(byte vk, byte scan, int flags, int extrainfo);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int SetKeyboardState(byte[] keystate);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool UnhookWindowsHookEx(HandleRef hhook);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern short GetAsyncKeyState(int vkey);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr CallNextHookEx(HandleRef hhook, int code, IntPtr wparam, IntPtr lparam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int ScreenToClient(HandleRef hWnd, [In][Out] NativeMethods.POINT pt);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetModuleFileName(HandleRef hModule, StringBuilder buffer, int length);

        public static StringBuilder GetModuleFileNameLongPath(HandleRef hModule)
        {
            StringBuilder stringBuilder = new StringBuilder(260);
            int num = 1;
            int num2 = 0;
            while ((num2 = GetModuleFileName(hModule, stringBuilder, stringBuilder.Capacity)) == stringBuilder.Capacity && Marshal.GetLastWin32Error() == 122 && stringBuilder.Capacity < 32767)
            {
                num += 2;
                int capacity = ((num * 260 < 32767) ? (num * 260) : 32767);
                stringBuilder.EnsureCapacity(capacity);
            }
            stringBuilder.Length = num2;
            return stringBuilder;
        }

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool IsDialogMessage(HandleRef hWndDlg, [In][Out] ref NativeMethods.MSG msg);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool TranslateMessage([In][Out] ref NativeMethods.MSG msg);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr DispatchMessage([In] ref NativeMethods.MSG msg);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr DispatchMessageA([In] ref NativeMethods.MSG msg);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern IntPtr DispatchMessageW([In] ref NativeMethods.MSG msg);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int PostThreadMessage(int id, int msg, IntPtr wparam, IntPtr lparam);

        [DllImport("ole32.dll", ExactSpelling = true)]
        public static extern int CoRegisterMessageFilter(HandleRef newFilter, ref IntPtr oldMsgFilter);

        [DllImport("ole32.dll", EntryPoint = "OleInitialize", ExactSpelling = true, SetLastError = true)]
        private static extern int IntOleInitialize(int val);

        public static int OleInitialize()
        {
            return IntOleInitialize(0);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool EnumThreadWindows(int dwThreadId, NativeMethods.EnumThreadWindowsCallback lpfn, HandleRef lParam);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetExitCodeThread(IntPtr hThread, out uint lpExitCode);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendDlgItemMessage(HandleRef hDlg, int nIDDlgItem, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("ole32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int OleUninitialize();

        [DllImport("comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetSaveFileName([In][Out] NativeMethods.OPENFILENAME_I ofn);

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "ChildWindowFromPointEx", ExactSpelling = true)]
        private static extern IntPtr _ChildWindowFromPointEx(HandleRef hwndParent, POINTSTRUCT pt, int uFlags);

        public static IntPtr ChildWindowFromPointEx(HandleRef hwndParent, int x, int y, int uFlags)
        {
            POINTSTRUCT pt = new POINTSTRUCT(x, y);
            return _ChildWindowFromPointEx(hwndParent, pt, uFlags);
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, EntryPoint = "CloseHandle", ExactSpelling = true, SetLastError = true)]
        private static extern bool IntCloseHandle(HandleRef handle);

        public static bool CloseHandle(HandleRef handle)
        {
            System.Internal.HandleCollector.Remove((IntPtr)handle, NativeMethods.CommonHandles.Kernel);
            return IntCloseHandle(handle);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateCompatibleDC", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr IntCreateCompatibleDC(HandleRef hDC);

        public static IntPtr CreateCompatibleDC(HandleRef hDC)
        {
            return System.Internal.HandleCollector.Add(IntCreateCompatibleDC(hDC), NativeMethods.CommonHandles.CompatibleHDC);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BlockInput([In][MarshalAs(UnmanagedType.Bool)] bool fBlockIt);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern uint SendInput(uint nInputs, NativeMethods.INPUT[] pInputs, int cbSize);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, EntryPoint = "MapViewOfFile", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr IntMapViewOfFile(HandleRef hFileMapping, int dwDesiredAccess, int dwFileOffsetHigh, int dwFileOffsetLow, int dwNumberOfBytesToMap);

        public static IntPtr MapViewOfFile(HandleRef hFileMapping, int dwDesiredAccess, int dwFileOffsetHigh, int dwFileOffsetLow, int dwNumberOfBytesToMap)
        {
            return System.Internal.HandleCollector.Add(IntMapViewOfFile(hFileMapping, dwDesiredAccess, dwFileOffsetHigh, dwFileOffsetLow, dwNumberOfBytesToMap), NativeMethods.CommonHandles.Kernel);
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, EntryPoint = "UnmapViewOfFile", ExactSpelling = true, SetLastError = true)]
        private static extern bool IntUnmapViewOfFile(HandleRef pvBaseAddress);

        public static bool UnmapViewOfFile(HandleRef pvBaseAddress)
        {
            System.Internal.HandleCollector.Remove((IntPtr)pvBaseAddress, NativeMethods.CommonHandles.Kernel);
            return IntUnmapViewOfFile(pvBaseAddress);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetDCEx", ExactSpelling = true)]
        private static extern IntPtr IntGetDCEx(HandleRef hWnd, HandleRef hrgnClip, int flags);

        public static IntPtr GetDCEx(HandleRef hWnd, HandleRef hrgnClip, int flags)
        {
            return System.Internal.HandleCollector.Add(IntGetDCEx(hWnd, hrgnClip, flags), NativeMethods.CommonHandles.HDC);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetObject(HandleRef hObject, int nSize, [In][Out] NativeMethods.BITMAP bm);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetObject(HandleRef hObject, int nSize, [In][Out] NativeMethods.LOGPEN lp);

        public static int GetObject(HandleRef hObject, NativeMethods.LOGPEN lp)
        {
            return GetObject(hObject, Marshal.SizeOf(typeof(NativeMethods.LOGPEN)), lp);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetObject(HandleRef hObject, int nSize, [In][Out] NativeMethods.LOGBRUSH lb);

        public static int GetObject(HandleRef hObject, NativeMethods.LOGBRUSH lb)
        {
            return GetObject(hObject, Marshal.SizeOf(typeof(NativeMethods.LOGBRUSH)), lb);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetObject(HandleRef hObject, int nSize, [In][Out] NativeMethods.LOGFONT lf);

        public static int GetObject(HandleRef hObject, NativeMethods.LOGFONT lp)
        {
            return GetObject(hObject, Marshal.SizeOf(typeof(NativeMethods.LOGFONT)), lp);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetObject(HandleRef hObject, int nSize, ref int nEntries);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetObject(HandleRef hObject, int nSize, int[] nEntries);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int GetObjectType(HandleRef hObject);

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateAcceleratorTable")]
        private static extern IntPtr IntCreateAcceleratorTable(HandleRef pentries, int cCount);

        public static IntPtr CreateAcceleratorTable(HandleRef pentries, int cCount)
        {
            return System.Internal.HandleCollector.Add(IntCreateAcceleratorTable(pentries, cCount), NativeMethods.CommonHandles.Accelerator);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "DestroyAcceleratorTable", ExactSpelling = true)]
        private static extern bool IntDestroyAcceleratorTable(HandleRef hAccel);

        public static bool DestroyAcceleratorTable(HandleRef hAccel)
        {
            System.Internal.HandleCollector.Remove((IntPtr)hAccel, NativeMethods.CommonHandles.Accelerator);
            return IntDestroyAcceleratorTable(hAccel);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern short VkKeyScan(char key);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetCapture();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr SetCapture(HandleRef hwnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetFocus();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool GetCursorPos([In][Out] NativeMethods.POINT pt);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern short GetKeyState(int keyCode);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern uint GetShortPathName(string lpszLongPath, StringBuilder lpszShortPath, uint cchBuffer);

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowRgn", ExactSpelling = true)]
        private static extern int IntSetWindowRgn(HandleRef hwnd, HandleRef hrgn, bool fRedraw);

        public static int SetWindowRgn(HandleRef hwnd, HandleRef hrgn, bool fRedraw)
        {
            int num = IntSetWindowRgn(hwnd, hrgn, fRedraw);
            if (num != 0)
            {
                System.Internal.HandleCollector.Remove((IntPtr)hrgn, NativeMethods.CommonHandles.GDI);
            }
            return num;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(HandleRef hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern void GetTempFileName(string tempDirName, string prefixName, int unique, StringBuilder sb);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SetWindowText(HandleRef hWnd, string text);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GlobalAlloc(int uFlags, int dwBytes);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GlobalReAlloc(HandleRef handle, int bytes, int flags);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GlobalLock(HandleRef handle);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool GlobalUnlock(HandleRef handle);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GlobalFree(HandleRef handle);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int GlobalSize(HandleRef handle);

        [DllImport("imm32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImmSetConversionStatus(HandleRef hIMC, int conversion, int sentence);

        [DllImport("imm32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImmGetConversionStatus(HandleRef hIMC, ref int conversion, ref int sentence);

        [DllImport("imm32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr ImmGetContext(HandleRef hWnd);

        [DllImport("imm32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImmReleaseContext(HandleRef hWnd, HandleRef hIMC);

        [DllImport("imm32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr ImmAssociateContext(HandleRef hWnd, HandleRef hIMC);

        [DllImport("imm32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImmDestroyContext(HandleRef hIMC);

        [DllImport("imm32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr ImmCreateContext();

        [DllImport("imm32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImmSetOpenStatus(HandleRef hIMC, bool open);

        [DllImport("imm32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImmGetOpenStatus(HandleRef hIMC);

        [DllImport("imm32.dll", CharSet = CharSet.Auto)]
        public static extern bool ImmNotifyIME(HandleRef hIMC, int dwAction, int dwIndex, int dwValue);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr SetFocus(HandleRef hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetParent(HandleRef hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetAncestor(HandleRef hWnd, int flags);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool IsChild(HandleRef hWndParent, HandleRef hwnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool IsZoomed(HandleRef hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string className, string windowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int MapWindowPoints(HandleRef hWndFrom, HandleRef hWndTo, [In][Out] ref NativeMethods.RECT rect, int cPoints);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int MapWindowPoints(HandleRef hWndFrom, HandleRef hWndTo, [In][Out] NativeMethods.POINT pt, int cPoints);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, bool wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, int[] lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int[] wParam, int[] lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, ref int wParam, ref int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, string lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, IntPtr wParam, string lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, StringBuilder lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.TOOLINFO_T lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.TOOLINFO_TOOLTIP lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, ref NativeMethods.TBBUTTON lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, ref NativeMethods.TBBUTTONINFO lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, ref NativeMethods.TV_ITEM lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, ref NativeMethods.TV_INSERTSTRUCT lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.TV_HITTESTINFO lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.LVBKIMAGE lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(HandleRef hWnd, int msg, int wParam, ref NativeMethods.LVHITTESTINFO lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.TCITEM_T lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, ref NativeMethods.HDLAYOUT hdlayout);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, HandleRef wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, HandleRef lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, [In][Out][MarshalAs(UnmanagedType.LPStruct)] NativeMethods.PARAFORMAT lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, [In][Out][MarshalAs(UnmanagedType.LPStruct)] NativeMethods.CHARFORMATA lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, [In][Out][MarshalAs(UnmanagedType.LPStruct)] NativeMethods.CHARFORMAT2A lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, [In][Out][MarshalAs(UnmanagedType.LPStruct)] NativeMethods.CHARFORMATW lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(HandleRef hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.IUnknown)] out object editOle);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.CHARRANGE lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.FINDTEXT lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.TEXTRANGE lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.POINT lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, NativeMethods.POINT wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.REPASTESPECIAL lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.EDITSTREAM lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.EDITSTREAM64 lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, NativeMethods.GETTEXTLENGTHEX wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, [In][Out] NativeMethods.SIZE lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, [In][Out] ref NativeMethods.LVFINDINFO lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.LVHITTESTINFO lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.LVCOLUMN_T lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, [In][Out] ref NativeMethods.LVITEM lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.LVCOLUMN lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.LVGROUP lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, NativeMethods.POINT wParam, [In][Out] NativeMethods.LVINSERTMARK lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.LVINSERTMARK lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SendMessage(HandleRef hWnd, int msg, int wParam, [In][Out] NativeMethods.LVTILEVIEWINFO lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.MCHITTESTINFO lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.SYSTEMTIME lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.SYSTEMTIMEARRAY lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, [In][Out] NativeMethods.LOGFONT lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, NativeMethods.MSG lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int Msg, IntPtr wParam, [In][Out] ref NativeMethods.RECT lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int Msg, ref short wParam, ref short lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int Msg, [In][Out][MarshalAs(UnmanagedType.Bool)] ref bool wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int Msg, int wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int Msg, int wParam, [In][Out] ref NativeMethods.RECT lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int Msg, int wParam, [In][Out] ref Rectangle lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int Msg, IntPtr wParam, NativeMethods.ListViewCompareCallback pfnCompare);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessageTimeout(HandleRef hWnd, int msg, IntPtr wParam, IntPtr lParam, int flags, int timeout, out IntPtr pdwResult);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr SetParent(HandleRef hWnd, HandleRef hWndParent);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool GetWindowRect(HandleRef hWnd, [In][Out] ref NativeMethods.RECT rect);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetWindow(HandleRef hWnd, int uCmd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetDlgItem(HandleRef hWnd, int nIDDlgItem);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string modName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr DefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr DefMDIChildProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr CallWindowProc(IntPtr wndProc, IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern short GlobalDeleteAtom(short atom);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr GetProcAddress(HandleRef hModule, string lpProcName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetClassInfo(HandleRef hInst, string lpszClass, [In][Out] NativeMethods.WNDCLASS_I wc);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetClassInfo(HandleRef hInst, string lpszClass, IntPtr h);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int GetSystemMetrics(int nIndex);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int GetSystemMetricsForDpi(int nIndex, uint dpi);

        public static int TryGetSystemMetricsForDpi(int nIndex, uint dpi)
        {
            if (ApiHelper.IsApiAvailable("user32.dll", "GetSystemMetricsForDpi"))
            {
                return GetSystemMetricsForDpi(nIndex, dpi);
            }
            return GetSystemMetrics(nIndex);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SystemParametersInfo(int nAction, int nParam, ref NativeMethods.RECT rc, int nUpdate);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SystemParametersInfo(int nAction, int nParam, ref int value, int ignore);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SystemParametersInfo(int nAction, int nParam, ref bool value, int ignore);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SystemParametersInfo(int nAction, int nParam, ref NativeMethods.HIGHCONTRAST_I rc, int nUpdate);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SystemParametersInfo(int nAction, int nParam, [In][Out] NativeMethods.NONCLIENTMETRICS metrics, int nUpdate);

        [DllImport("user32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SystemParametersInfoForDpi(int nAction, int nParam, [In][Out] NativeMethods.NONCLIENTMETRICS metrics, int nUpdate, uint dpi);

        public static bool TrySystemParametersInfoForDpi(int nAction, int nParam, [In][Out] NativeMethods.NONCLIENTMETRICS metrics, int nUpdate, uint dpi)
        {
            if (ApiHelper.IsApiAvailable("user32.dll", "SystemParametersInfoForDpi"))
            {
                return SystemParametersInfoForDpi(nAction, nParam, metrics, nUpdate, dpi);
            }
            return SystemParametersInfo(nAction, nParam, metrics, nUpdate);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SystemParametersInfo(int nAction, int nParam, [In][Out] NativeMethods.LOGFONT font, int nUpdate);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SystemParametersInfo(int nAction, int nParam, bool[] flag, bool nUpdate);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetComputerName(StringBuilder lpBuffer, int[] nSize);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetUserName(StringBuilder lpBuffer, int[] nSize);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern IntPtr GetProcessWindowStation();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetUserObjectInformation(HandleRef hObj, int nIndex, [MarshalAs(UnmanagedType.LPStruct)] NativeMethods.USEROBJECTFLAGS pvBuffer, int nLength, ref int lpnLengthNeeded);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int ClientToScreen(HandleRef hWnd, [In][Out] NativeMethods.POINT pt);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int MsgWaitForMultipleObjectsEx(int nCount, IntPtr pHandles, int dwMilliseconds, int dwWakeMask, int dwFlags);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("ole32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int RegisterDragDrop(HandleRef hwnd, IOleDropTarget target);

        [DllImport("ole32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int RevokeDragDrop(HandleRef hwnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool PeekMessage([In][Out] ref NativeMethods.MSG msg, HandleRef hwnd, int msgMin, int msgMax, int remove);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool PeekMessageW([In][Out] ref NativeMethods.MSG msg, HandleRef hwnd, int msgMin, int msgMax, int remove);

        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        public static extern bool PeekMessageA([In][Out] ref NativeMethods.MSG msg, HandleRef hwnd, int msgMin, int msgMax, int remove);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool PostMessage(HandleRef hwnd, int msg, IntPtr wparam, IntPtr lparam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern short GlobalAddAtom(string atomName);

        [DllImport("oleacc.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr LresultFromObject(ref Guid refiid, IntPtr wParam, HandleRef pAcc);

        [DllImport("oleacc.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int CreateStdAccessibleObject(HandleRef hWnd, int objID, ref Guid refiid, [In][Out][MarshalAs(UnmanagedType.Interface)] ref object pAcc);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern void NotifyWinEvent(int winEvent, HandleRef hwnd, int objType, int objID);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int GetMenuItemID(HandleRef hMenu, int nPos);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetSubMenu(HandleRef hwnd, int index);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int GetMenuItemCount(HandleRef hMenu);

        [DllImport("oleaut32.dll", PreserveSig = false)]
        public static extern void GetErrorInfo(int reserved, [In][Out] ref IErrorInfo errorInfo);

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "BeginPaint", ExactSpelling = true)]
        private static extern IntPtr IntBeginPaint(HandleRef hWnd, [In][Out] ref NativeMethods.PAINTSTRUCT lpPaint);

        public static IntPtr BeginPaint(HandleRef hWnd, [In][Out][MarshalAs(UnmanagedType.LPStruct)] ref NativeMethods.PAINTSTRUCT lpPaint)
        {
            return System.Internal.HandleCollector.Add(IntBeginPaint(hWnd, ref lpPaint), NativeMethods.CommonHandles.HDC);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "EndPaint", ExactSpelling = true)]
        private static extern bool IntEndPaint(HandleRef hWnd, ref NativeMethods.PAINTSTRUCT lpPaint);

        public static bool EndPaint(HandleRef hWnd, [In][MarshalAs(UnmanagedType.LPStruct)] ref NativeMethods.PAINTSTRUCT lpPaint)
        {
            System.Internal.HandleCollector.Remove(lpPaint.hdc, NativeMethods.CommonHandles.HDC);
            return IntEndPaint(hWnd, ref lpPaint);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetDC", ExactSpelling = true)]
        private static extern IntPtr IntGetDC(HandleRef hWnd);

        public static IntPtr GetDC(HandleRef hWnd)
        {
            return System.Internal.HandleCollector.Add(IntGetDC(hWnd), NativeMethods.CommonHandles.HDC);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetWindowDC", ExactSpelling = true)]
        private static extern IntPtr IntGetWindowDC(HandleRef hWnd);

        public static IntPtr GetWindowDC(HandleRef hWnd)
        {
            return System.Internal.HandleCollector.Add(IntGetWindowDC(hWnd), NativeMethods.CommonHandles.HDC);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "ReleaseDC", ExactSpelling = true)]
        private static extern int IntReleaseDC(HandleRef hWnd, HandleRef hDC);

        public static int ReleaseDC(HandleRef hWnd, HandleRef hDC)
        {
            System.Internal.HandleCollector.Remove((IntPtr)hDC, NativeMethods.CommonHandles.HDC);
            return IntReleaseDC(hWnd, hDC);
        }

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateDC", SetLastError = true)]
        private static extern IntPtr IntCreateDC(string lpszDriver, string lpszDeviceName, string lpszOutput, HandleRef devMode);

        public static IntPtr CreateDC(string lpszDriver)
        {
            return System.Internal.HandleCollector.Add(IntCreateDC(lpszDriver, null, null, NativeMethods.NullHandleRef), NativeMethods.CommonHandles.HDC);
        }

        public static IntPtr CreateDC(string lpszDriverName, string lpszDeviceName, string lpszOutput, HandleRef lpInitData)
        {
            return System.Internal.HandleCollector.Add(IntCreateDC(lpszDriverName, lpszDeviceName, lpszOutput, lpInitData), NativeMethods.CommonHandles.HDC);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool SystemParametersInfo(int nAction, int nParam, [In][Out] IntPtr[] rc, int nUpdate);

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SendMessage")]
        public static extern IntPtr SendCallbackMessage(HandleRef hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("shell32.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern void DragAcceptFiles(HandleRef hWnd, bool fAccept);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int GetDeviceCaps(HandleRef hDC, int nIndex);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool GetScrollInfo(HandleRef hWnd, int fnBar, NativeMethods.SCROLLINFO si);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int SetScrollInfo(HandleRef hWnd, int fnBar, NativeMethods.SCROLLINFO si, bool redraw);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetActiveWindow();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr LoadLibrary(string libname);

        [DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr LoadLibraryEx(string lpModuleName, IntPtr hFile, uint dwFlags);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool FreeLibrary(HandleRef hModule);

        public static IntPtr GetWindowLong(HandleRef hWnd, int nIndex)
        {
            if (IntPtr.Size == 4)
            {
                return GetWindowLong32(hWnd, nIndex);
            }
            return GetWindowLongPtr64(hWnd, nIndex);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetWindowLong")]
        public static extern IntPtr GetWindowLong32(HandleRef hWnd, int nIndex);

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetWindowLongPtr")]
        public static extern IntPtr GetWindowLongPtr64(HandleRef hWnd, int nIndex);

        public static IntPtr SetWindowLong(HandleRef hWnd, int nIndex, HandleRef dwNewLong)
        {
            if (IntPtr.Size == 4)
            {
                return SetWindowLongPtr32(hWnd, nIndex, dwNewLong);
            }
            return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLong")]
        public static extern IntPtr SetWindowLongPtr32(HandleRef hWnd, int nIndex, HandleRef dwNewLong);

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLongPtr")]
        public static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, HandleRef dwNewLong);

        public static IntPtr SetWindowLong(HandleRef hWnd, int nIndex, NativeMethods.WndProc wndproc)
        {
            if (IntPtr.Size == 4)
            {
                return SetWindowLongPtr32(hWnd, nIndex, wndproc);
            }
            return SetWindowLongPtr64(hWnd, nIndex, wndproc);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLong")]
        public static extern IntPtr SetWindowLongPtr32(HandleRef hWnd, int nIndex, NativeMethods.WndProc wndproc);

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SetWindowLongPtr")]
        public static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, NativeMethods.WndProc wndproc);

        [DllImport("ole32.dll", PreserveSig = false)]
        public static extern ILockBytes CreateILockBytesOnHGlobal(HandleRef hGlobal, bool fDeleteOnRelease);

        [DllImport("ole32.dll", PreserveSig = false)]
        public static extern IStorage StgCreateDocfileOnILockBytes(ILockBytes iLockBytes, int grfMode, int reserved);

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "CreatePopupMenu", ExactSpelling = true)]
        private static extern IntPtr IntCreatePopupMenu();

        public static IntPtr CreatePopupMenu()
        {
            return System.Internal.HandleCollector.Add(IntCreatePopupMenu(), NativeMethods.CommonHandles.Menu);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool RemoveMenu(HandleRef hMenu, int uPosition, int uFlags);

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "DestroyMenu", ExactSpelling = true)]
        private static extern bool IntDestroyMenu(HandleRef hMenu);

        public static bool DestroyMenu(HandleRef hMenu)
        {
            System.Internal.HandleCollector.Remove((IntPtr)hMenu, NativeMethods.CommonHandles.Menu);
            return IntDestroyMenu(hMenu);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetForegroundWindow(HandleRef hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetSystemMenu(HandleRef hWnd, bool bRevert);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr DefFrameProc(IntPtr hWnd, IntPtr hWndClient, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool TranslateMDISysAccel(IntPtr hWndClient, [In][Out] ref NativeMethods.MSG msg);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern bool SetLayeredWindowAttributes(HandleRef hwnd, int crKey, byte bAlpha, int dwFlags);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetMenu(HandleRef hWnd, HandleRef hMenu);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int GetWindowPlacement(HandleRef hWnd, ref NativeMethods.WINDOWPLACEMENT placement);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern void GetStartupInfo([In][Out] NativeMethods.STARTUPINFO_I startupinfo_i);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetMenuDefaultItem(HandleRef hwnd, int nIndex, bool pos);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool EnableMenuItem(HandleRef hMenu, int UIDEnabledItem, int uEnable);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr SetActiveWindow(HandleRef hWnd);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateIC", SetLastError = true)]
        private static extern IntPtr IntCreateIC(string lpszDriverName, string lpszDeviceName, string lpszOutput, HandleRef lpInitData);

        public static IntPtr CreateIC(string lpszDriverName, string lpszDeviceName, string lpszOutput, HandleRef lpInitData)
        {
            return System.Internal.HandleCollector.Add(IntCreateIC(lpszDriverName, lpszDeviceName, lpszOutput, lpInitData), NativeMethods.CommonHandles.HDC);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool ClipCursor(ref NativeMethods.RECT rcClip);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool ClipCursor(NativeMethods.COMRECT rcClip);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr SetCursor(HandleRef hcursor);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int ShowCursor(bool bShow);

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "DestroyCursor", ExactSpelling = true)]
        private static extern bool IntDestroyCursor(HandleRef hCurs);

        public static bool DestroyCursor(HandleRef hCurs)
        {
            System.Internal.HandleCollector.Remove((IntPtr)hCurs, NativeMethods.CommonHandles.Cursor);
            return IntDestroyCursor(hCurs);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool IsWindow(HandleRef hWnd);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "DeleteDC", ExactSpelling = true, SetLastError = true)]
        private static extern bool IntDeleteDC(HandleRef hDC);

        public static bool DeleteDC(HandleRef hDC)
        {
            System.Internal.HandleCollector.Remove((IntPtr)hDC, NativeMethods.CommonHandles.HDC);
            return IntDeleteDC(hDC);
        }

        public static bool DeleteCompatibleDC(HandleRef hDC)
        {
            System.Internal.HandleCollector.Remove((IntPtr)hDC, NativeMethods.CommonHandles.CompatibleHDC);
            return IntDeleteDC(hDC);
        }

        [DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern bool GetMessageA([In][Out] ref NativeMethods.MSG msg, HandleRef hWnd, int uMsgFilterMin, int uMsgFilterMax);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern bool GetMessageW([In][Out] ref NativeMethods.MSG msg, HandleRef hWnd, int uMsgFilterMin, int uMsgFilterMax);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr PostMessage(HandleRef hwnd, int msg, int wparam, int lparam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr PostMessage(HandleRef hwnd, int msg, int wparam, IntPtr lparam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool GetClientRect(HandleRef hWnd, [In][Out] ref NativeMethods.RECT rect);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool GetClientRect(HandleRef hWnd, IntPtr rect);

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "WindowFromPoint", ExactSpelling = true)]
        private static extern IntPtr _WindowFromPoint(POINTSTRUCT pt);

        public static IntPtr WindowFromPoint(int x, int y)
        {
            POINTSTRUCT pt = new POINTSTRUCT(x, y);
            return _WindowFromPoint(pt);
        }

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr WindowFromDC(HandleRef hDC);

        [DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "DestroyWindow", ExactSpelling = true)]
        public static extern bool IntDestroyWindow(HandleRef hWnd);

        public static bool DestroyWindow(HandleRef hWnd)
        {
            return IntDestroyWindow(hWnd);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool UnregisterClass(string className, HandleRef hInstance);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetStockObject(int nIndex);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern short RegisterClass(NativeMethods.WNDCLASS_D wc);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern void PostQuitMessage(int nExitCode);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern void WaitMessage();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetWindowPlacement(HandleRef hWnd, [In] ref NativeMethods.WINDOWPLACEMENT placement);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern uint GetDpiForWindow(HandleRef hWnd);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool GetSystemPowerStatus([In][Out] ref NativeMethods.SYSTEM_POWER_STATUS systemPowerStatus);

        [DllImport("Powrprof.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int GetRegionData(HandleRef hRgn, int size, IntPtr lpRgnData);

        public unsafe static NativeMethods.RECT[] GetRectsFromRegion(IntPtr hRgn)
        {
            NativeMethods.RECT[] result = null;
            IntPtr intPtr = IntPtr.Zero;
            try
            {
                int regionData = GetRegionData(new HandleRef(null, hRgn), 0, IntPtr.Zero);
                if (regionData != 0)
                {
                    intPtr = Marshal.AllocCoTaskMem(regionData);
                    int regionData2 = GetRegionData(new HandleRef(null, hRgn), regionData, intPtr);
                    if (regionData2 == regionData)
                    {
                        NativeMethods.RGNDATAHEADER* ptr = (NativeMethods.RGNDATAHEADER*)(void*)intPtr;
                        if (ptr->iType == 1)
                        {
                            result = new NativeMethods.RECT[ptr->nCount];
                            int cbSizeOfStruct = ptr->cbSizeOfStruct;
                            for (int i = 0; i < ptr->nCount; i++)
                            {
                                result[i] = *(NativeMethods.RECT*)((byte*)(void*)intPtr + cbSizeOfStruct + Marshal.SizeOf(typeof(NativeMethods.RECT)) * i);
                            }
                            return result;
                        }
                        return result;
                    }
                    return result;
                }
                return result;
            }
            finally
            {
                if (intPtr != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(intPtr);
                }
            }
        }

        internal static bool IsComObject(object o)
        {
            return Marshal.IsComObject(o);
        }

        internal static int ReleaseComObject(object objToRelease)
        {
            return Marshal.ReleaseComObject(objToRelease);
        }

        public static object PtrToStructure(IntPtr lparam, Type cls)
        {
            return Marshal.PtrToStructure(lparam, cls);
        }

        public static void PtrToStructure(IntPtr lparam, object data)
        {
            Marshal.PtrToStructure(lparam, data);
        }

        internal static int SizeOf(Type t)
        {
            return Marshal.SizeOf(t);
        }

        internal static void ThrowExceptionForHR(int errorCode)
        {
            Marshal.ThrowExceptionForHR(errorCode);
        }

        [DllImport("clr.dll", BestFitMapping = false, CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = false)]
        internal static extern void CorLaunchApplication(uint hostType, string applicationFullName, int manifestPathsCount, string[] manifestPaths, int activationDataCount, string[] activationData, PROCESS_INFORMATION processInformation);

        [DllImport("UIAutomationCore.dll", CharSet = CharSet.Unicode)]
        internal static extern int UiaHostProviderFromHwnd(HandleRef hwnd, out IRawElementProviderSimple provider);

        [DllImport("UIAutomationCore.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr UiaReturnRawElementProvider(HandleRef hwnd, IntPtr wParam, IntPtr lParam, IRawElementProviderSimple el);

        [DllImport("UIAutomationCore.dll", CharSet = CharSet.Unicode)]
        internal static extern bool UiaClientsAreListening();

        [DllImport("UIAutomationCore.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern int UiaRaiseAutomationEvent(IRawElementProviderSimple provider, int id);

        [DllImport("UIAutomationCore.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern int UiaRaiseAutomationPropertyChangedEvent(IRawElementProviderSimple provider, int id, object oldValue, object newValue);

        [DllImport("UIAutomationCore.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern int UiaRaiseNotificationEvent(IRawElementProviderSimple provider, AutomationNotificationKind notificationKind, AutomationNotificationProcessing notificationProcessing, string notificationText, string activityId);

        [DllImport("UIAutomationCore.dll", CharSet = CharSet.Unicode)]
        internal static extern int UiaRaiseStructureChangedEvent(IRawElementProviderSimple provider, StructureChangeType structureChangeType, int[] runtimeId, int runtimeIdLen);

        public static IntPtr LoadLibraryFromSystemPathIfAvailable(string libraryName)
        {
            IntPtr result = IntPtr.Zero;
            IntPtr moduleHandle = GetModuleHandle("kernel32.dll");
            if (moduleHandle != IntPtr.Zero)
            {
                result = ((!(GetProcAddress(new HandleRef(null, moduleHandle), "AddDllDirectory") != IntPtr.Zero)) ? LoadLibrary(libraryName) : LoadLibraryEx(libraryName, IntPtr.Zero, 2048u));
            }
            return result;
        }

        [DllImport("wldp.dll", ExactSpelling = true)]
        private static extern int WldpIsDynamicCodePolicyEnabled(out int enabled);

        internal static bool IsDynamicCodePolicyEnabled()
        {
            if (!ApiHelper.IsApiAvailable("wldp.dll", "WldpIsDynamicCodePolicyEnabled"))
            {
                return false;
            }
            int enabled = 0;
            if (WldpIsDynamicCodePolicyEnabled(out enabled) == 0)
            {
                return enabled != 0;
            }
            return false;
        }
    }
}
