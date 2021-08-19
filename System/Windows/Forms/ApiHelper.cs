// System.Windows.Forms.ApiHelper
using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    internal static class ApiHelper
    {
        private static ConcurrentDictionary<Tuple<string, string>, bool> availableApis = new ConcurrentDictionary<Tuple<string, string>, bool>();

        public static bool IsApiAvailable(string libName, string procName)
        {
            bool value = false;
            if (!string.IsNullOrEmpty(libName) && !string.IsNullOrEmpty(procName))
            {
                Tuple<string, string> key = new Tuple<string, string>(libName, procName);
                if (availableApis.TryGetValue(key, out value))
                {
                    return value;
                }
                IntPtr intPtr = CommonUnsafeNativeMethods.LoadLibraryFromSystemPathIfAvailable(libName);
                if (intPtr != IntPtr.Zero)
                {
                    IntPtr procAddress = CommonUnsafeNativeMethods.GetProcAddress(new HandleRef(value, intPtr), procName);
                    if (procAddress != IntPtr.Zero)
                    {
                        value = true;
                    }
                }
                CommonUnsafeNativeMethods.FreeLibrary(new HandleRef(value, intPtr));
                availableApis[key] = value;
            }
            return value;
        }
    }
}
