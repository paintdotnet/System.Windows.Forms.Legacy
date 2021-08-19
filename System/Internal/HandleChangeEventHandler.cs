// System.Internal.HandleChangeEventHandler
using System;

namespace System.Internal
{
    internal delegate void HandleChangeEventHandler(string handleType, IntPtr handleValue, int currentHandleCount);
}
