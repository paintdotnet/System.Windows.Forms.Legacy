// System.Windows.Forms.CommonUnsafeNativeMethods
using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    [SuppressUnmanagedCodeSecurity]
    internal class CommonUnsafeNativeMethods
    {
        internal enum DPI_AWARENESS
        {
            DPI_AWARENESS_INVALID = -1,
            DPI_AWARENESS_UNAWARE,
            DPI_AWARENESS_SYSTEM_AWARE,
            DPI_AWARENESS_PER_MONITOR_AWARE
        }

        internal const int LOAD_LIBRARY_SEARCH_SYSTEM32 = 2048;

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetProcAddress(HandleRef hModule, string lpProcName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string modName);

        [DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr LoadLibraryEx(string lpModuleName, IntPtr hFile, uint dwFlags);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr LoadLibrary(string libname);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool FreeLibrary(HandleRef hModule);

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

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        internal static extern System.Windows.Forms.DpiAwarenessContext GetThreadDpiAwarenessContext();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        internal static extern IntPtr GetWindowDpiAwarenessContext(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        internal static extern DPI_AWARENESS GetAwarenessFromDpiAwarenessContext(IntPtr dpiAwarenessContext);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        internal static extern System.Windows.Forms.DpiAwarenessContext SetThreadDpiAwarenessContext(System.Windows.Forms.DpiAwarenessContext dpiContext);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool AreDpiAwarenessContextsEqual(System.Windows.Forms.DpiAwarenessContext dpiContextA, System.Windows.Forms.DpiAwarenessContext dpiContextB);

        public static bool TryFindDpiAwarenessContextsEqual(System.Windows.Forms.DpiAwarenessContext dpiContextA, System.Windows.Forms.DpiAwarenessContext dpiContextB)
        {
            if (dpiContextA == System.Windows.Forms.DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED && dpiContextB == System.Windows.Forms.DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED)
            {
                return true;
            }
            if (ApiHelper.IsApiAvailable("user32.dll", "AreDpiAwarenessContextsEqual"))
            {
                return AreDpiAwarenessContextsEqual(dpiContextA, dpiContextB);
            }
            return false;
        }

        public static System.Windows.Forms.DpiAwarenessContext TryGetThreadDpiAwarenessContext()
        {
            if (ApiHelper.IsApiAvailable("user32.dll", "GetThreadDpiAwarenessContext"))
            {
                return GetThreadDpiAwarenessContext();
            }
            return System.Windows.Forms.DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED;
        }

        public static System.Windows.Forms.DpiAwarenessContext TrySetThreadDpiAwarenessContext(System.Windows.Forms.DpiAwarenessContext dpiCOntext)
        {
            if (ApiHelper.IsApiAvailable("user32.dll", "SetThreadDpiAwarenessContext"))
            {
                return SetThreadDpiAwarenessContext(dpiCOntext);
            }
            return System.Windows.Forms.DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED;
        }

        internal static System.Windows.Forms.DpiAwarenessContext TryGetDpiAwarenessContextForWindow(IntPtr hWnd)
        {
            System.Windows.Forms.DpiAwarenessContext result = System.Windows.Forms.DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED;
            try
            {
                if (ApiHelper.IsApiAvailable("user32.dll", "GetWindowDpiAwarenessContext"))
                {
                    if (ApiHelper.IsApiAvailable("user32.dll", "GetAwarenessFromDpiAwarenessContext"))
                    {
                        IntPtr windowDpiAwarenessContext = GetWindowDpiAwarenessContext(hWnd);
                        DPI_AWARENESS awarenessFromDpiAwarenessContext = GetAwarenessFromDpiAwarenessContext(windowDpiAwarenessContext);
                        result = ConvertToDpiAwarenessContext(awarenessFromDpiAwarenessContext);
                        return result;
                    }
                    return result;
                }
                return result;
            }
            catch
            {
                return result;
            }
        }

        private static System.Windows.Forms.DpiAwarenessContext ConvertToDpiAwarenessContext(DPI_AWARENESS dpiAwareness)
        {
            return dpiAwareness switch
            {
                DPI_AWARENESS.DPI_AWARENESS_UNAWARE => System.Windows.Forms.DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNAWARE,
                DPI_AWARENESS.DPI_AWARENESS_SYSTEM_AWARE => System.Windows.Forms.DpiAwarenessContext.DPI_AWARENESS_CONTEXT_SYSTEM_AWARE,
                DPI_AWARENESS.DPI_AWARENESS_PER_MONITOR_AWARE => System.Windows.Forms.DpiAwarenessContext.DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2,
                _ => System.Windows.Forms.DpiAwarenessContext.DPI_AWARENESS_CONTEXT_SYSTEM_AWARE,
            };
        }
    }
}
