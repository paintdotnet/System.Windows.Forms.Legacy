#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Reflection
{
    public static class AssemblyDetours
    {
        private static Func<Assembly, string>? getLocationHandler;

        internal static void InitializeGetLocationHandler(Func<Assembly, string> getLocationHandler)
        {
            Interlocked.CompareExchange(ref AssemblyDetours.getLocationHandler, getLocationHandler, null);
        }

        // Because the assemblies we load are patched up, Assembly.Location returns string.Empty
        // So we swap the call to Assembly::get_Location with a call to this method
        public static string GetLocation(Assembly assembly)
        {
            Func<Assembly, string> getLocationHandler = AssemblyDetours.getLocationHandler ?? (a => a.Location);
            return getLocationHandler(assembly);
        }
    }
}
