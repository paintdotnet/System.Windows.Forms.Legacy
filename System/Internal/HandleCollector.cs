// System.Internal.HandleCollector
using System;
using System.Internal;
using System.Threading;

namespace System.Internal
{
    internal sealed class HandleCollector
    {
        private class HandleType
        {
            internal readonly string name;

            private int initialThreshHold;

            private int threshHold;

            private int handleCount;

            private readonly int deltaPercent;

            internal HandleType(string name, int expense, int initialThreshHold)
            {
                this.name = name;
                this.initialThreshHold = initialThreshHold;
                threshHold = initialThreshHold;
                deltaPercent = 100 - expense;
            }

            internal void Add(IntPtr handle)
            {
                if (handle == IntPtr.Zero)
                {
                    return;
                }
                bool flag = false;
                int currentHandleCount = 0;
                lock (this)
                {
                    handleCount++;
                    flag = NeedCollection();
                    currentHandleCount = handleCount;
                }
                lock (internalSyncObject)
                {
                    if (HandleCollector.HandleAdded != null)
                    {
                        HandleCollector.HandleAdded(name, handle, currentHandleCount);
                    }
                }
                if (flag && flag)
                {
                    GC.Collect();
                    int millisecondsTimeout = (100 - deltaPercent) / 4;
                    Thread.Sleep(millisecondsTimeout);
                }
            }

            internal int GetHandleCount()
            {
                lock (this)
                {
                    return handleCount;
                }
            }

            internal bool NeedCollection()
            {
                if (suspendCount > 0)
                {
                    return false;
                }
                if (handleCount > threshHold)
                {
                    threshHold = handleCount + handleCount * deltaPercent / 100;
                    return true;
                }
                int num = 100 * threshHold / (100 + deltaPercent);
                if (num >= initialThreshHold && handleCount < (int)((float)num * 0.9f))
                {
                    threshHold = num;
                }
                return false;
            }

            internal IntPtr Remove(IntPtr handle)
            {
                if (handle == IntPtr.Zero)
                {
                    return handle;
                }
                int currentHandleCount = 0;
                lock (this)
                {
                    handleCount--;
                    if (handleCount < 0)
                    {
                        handleCount = 0;
                    }
                    currentHandleCount = handleCount;
                }
                lock (internalSyncObject)
                {
                    if (HandleCollector.HandleRemoved != null)
                    {
                        HandleCollector.HandleRemoved(name, handle, currentHandleCount);
                        return handle;
                    }
                    return handle;
                }
            }
        }

        private static HandleType[] handleTypes;

        private static int handleTypeCount;

        private static int suspendCount;

        private static object internalSyncObject = new object();

        internal static event HandleChangeEventHandler HandleAdded;

        internal static event HandleChangeEventHandler HandleRemoved;

        internal static IntPtr Add(IntPtr handle, int type)
        {
            handleTypes[type - 1].Add(handle);
            return handle;
        }

        internal static void SuspendCollect()
        {
            lock (internalSyncObject)
            {
                suspendCount++;
            }
        }

        internal static void ResumeCollect()
        {
            bool flag = false;
            lock (internalSyncObject)
            {
                if (suspendCount > 0)
                {
                    suspendCount--;
                }
                if (suspendCount == 0)
                {
                    for (int i = 0; i < handleTypeCount; i++)
                    {
                        lock (handleTypes[i])
                        {
                            if (handleTypes[i].NeedCollection())
                            {
                                flag = true;
                            }
                        }
                    }
                }
            }
            if (flag)
            {
                GC.Collect();
            }
        }

        internal static int RegisterType(string typeName, int expense, int initialThreshold)
        {
            lock (internalSyncObject)
            {
                if (handleTypeCount == 0 || handleTypeCount == handleTypes.Length)
                {
                    HandleType[] destinationArray = new HandleType[handleTypeCount + 10];
                    if (handleTypes != null)
                    {
                        Array.Copy(handleTypes, 0, destinationArray, 0, handleTypeCount);
                    }
                    handleTypes = destinationArray;
                }
                handleTypes[handleTypeCount++] = new HandleType(typeName, expense, initialThreshold);
                return handleTypeCount;
            }
        }

        internal static IntPtr Remove(IntPtr handle, int type)
        {
            return handleTypes[type - 1].Remove(handle);
        }
    }
}
