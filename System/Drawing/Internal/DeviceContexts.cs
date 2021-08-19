// System.Drawing.Internal.DeviceContexts
using System;
using System.Drawing;
using System.Drawing.Internal;

namespace System.Drawing.Internal
{
    internal static class DeviceContexts
    {
        [ThreadStatic]
        private static ClientUtils.WeakRefCollection activeDeviceContexts;

        internal static void AddDeviceContext(DeviceContext dc)
        {
            if (activeDeviceContexts == null)
            {
                activeDeviceContexts = new ClientUtils.WeakRefCollection();
                activeDeviceContexts.RefCheckThreshold = 20;
            }
            if (!activeDeviceContexts.Contains(dc))
            {
                dc.Disposing += OnDcDisposing;
                activeDeviceContexts.Add(dc);
            }
        }

        private static void OnDcDisposing(object sender, EventArgs e)
        {
            DeviceContext deviceContext = sender as DeviceContext;
            if (deviceContext != null)
            {
                deviceContext.Disposing -= OnDcDisposing;
                RemoveDeviceContext(deviceContext);
            }
        }

        internal static void RemoveDeviceContext(DeviceContext dc)
        {
            if (activeDeviceContexts != null)
            {
                activeDeviceContexts.RemoveByHashCode(dc);
            }
        }
    }
}
