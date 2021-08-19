// System.Windows.Forms.ClientUtils
using System;
using System.Collections;
using System.Security;
using System.Threading;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    internal static class ClientUtils
    {
        internal class WeakRefCollection : IList, ICollection, IEnumerable
        {
            internal class WeakRefObject
            {
                private int hash;

                private WeakReference weakHolder;

                internal bool IsAlive => weakHolder.IsAlive;

                internal object Target => weakHolder.Target;

                internal WeakRefObject(object obj)
                {
                    weakHolder = new WeakReference(obj);
                    hash = obj.GetHashCode();
                }

                public override int GetHashCode()
                {
                    return hash;
                }

                public override bool Equals(object obj)
                {
                    WeakRefObject weakRefObject = obj as WeakRefObject;
                    if (weakRefObject == this)
                    {
                        return true;
                    }
                    if (weakRefObject == null)
                    {
                        return false;
                    }
                    if (weakRefObject.Target != Target && (Target == null || !Target.Equals(weakRefObject.Target)))
                    {
                        return false;
                    }
                    return true;
                }
            }

            private int refCheckThreshold = int.MaxValue;

            private ArrayList _innerList;

            internal ArrayList InnerList => _innerList;

            public int RefCheckThreshold
            {
                get
                {
                    return refCheckThreshold;
                }
                set
                {
                    refCheckThreshold = value;
                }
            }

            public object this[int index]
            {
                get
                {
                    WeakRefObject weakRefObject = InnerList[index] as WeakRefObject;
                    if (weakRefObject != null && weakRefObject.IsAlive)
                    {
                        return weakRefObject.Target;
                    }
                    return null;
                }
                set
                {
                    InnerList[index] = CreateWeakRefObject(value);
                }
            }

            public bool IsFixedSize => InnerList.IsFixedSize;

            public int Count => InnerList.Count;

            object ICollection.SyncRoot => InnerList.SyncRoot;

            public bool IsReadOnly => InnerList.IsReadOnly;

            bool ICollection.IsSynchronized => InnerList.IsSynchronized;

            internal WeakRefCollection()
            {
                _innerList = new ArrayList(4);
            }

            internal WeakRefCollection(int size)
            {
                _innerList = new ArrayList(size);
            }

            public void ScavengeReferences()
            {
                int num = 0;
                int count = Count;
                for (int i = 0; i < count; i++)
                {
                    object obj = this[num];
                    if (obj == null)
                    {
                        InnerList.RemoveAt(num);
                    }
                    else
                    {
                        num++;
                    }
                }
            }

            public override bool Equals(object obj)
            {
                WeakRefCollection weakRefCollection = obj as WeakRefCollection;
                if (weakRefCollection == this)
                {
                    return true;
                }
                if (weakRefCollection == null || Count != weakRefCollection.Count)
                {
                    return false;
                }
                for (int i = 0; i < Count; i++)
                {
                    if (InnerList[i] != weakRefCollection.InnerList[i] && (InnerList[i] == null || !InnerList[i].Equals(weakRefCollection.InnerList[i])))
                    {
                        return false;
                    }
                }
                return true;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            private WeakRefObject CreateWeakRefObject(object value)
            {
                if (value == null)
                {
                    return null;
                }
                return new WeakRefObject(value);
            }

            private static void Copy(WeakRefCollection sourceList, int sourceIndex, WeakRefCollection destinationList, int destinationIndex, int length)
            {
                if (sourceIndex < destinationIndex)
                {
                    sourceIndex += length;
                    destinationIndex += length;
                    while (length > 0)
                    {
                        destinationList.InnerList[--destinationIndex] = sourceList.InnerList[--sourceIndex];
                        length--;
                    }
                }
                else
                {
                    while (length > 0)
                    {
                        destinationList.InnerList[destinationIndex++] = sourceList.InnerList[sourceIndex++];
                        length--;
                    }
                }
            }

            public void RemoveByHashCode(object value)
            {
                if (value == null)
                {
                    return;
                }
                int hashCode = value.GetHashCode();
                for (int i = 0; i < InnerList.Count; i++)
                {
                    if (InnerList[i] != null && InnerList[i].GetHashCode() == hashCode)
                    {
                        RemoveAt(i);
                        break;
                    }
                }
            }

            public void Clear()
            {
                InnerList.Clear();
            }

            public bool Contains(object value)
            {
                return InnerList.Contains(CreateWeakRefObject(value));
            }

            public void RemoveAt(int index)
            {
                InnerList.RemoveAt(index);
            }

            public void Remove(object value)
            {
                InnerList.Remove(CreateWeakRefObject(value));
            }

            public int IndexOf(object value)
            {
                return InnerList.IndexOf(CreateWeakRefObject(value));
            }

            public void Insert(int index, object value)
            {
                InnerList.Insert(index, CreateWeakRefObject(value));
            }

            public int Add(object value)
            {
                if (Count > RefCheckThreshold)
                {
                    ScavengeReferences();
                }
                return InnerList.Add(CreateWeakRefObject(value));
            }

            public void CopyTo(Array array, int index)
            {
                InnerList.CopyTo(array, index);
            }

            public IEnumerator GetEnumerator()
            {
                return InnerList.GetEnumerator();
            }
        }

        public static bool IsCriticalException(Exception ex)
        {
            if (!(ex is NullReferenceException) && !(ex is StackOverflowException) && !(ex is OutOfMemoryException) && !(ex is ThreadAbortException) && !(ex is IndexOutOfRangeException))
            {
                return ex is AccessViolationException;
            }
            return true;
        }

        public static bool IsSecurityOrCriticalException(Exception ex)
        {
            if (!(ex is SecurityException))
            {
                return IsCriticalException(ex);
            }
            return true;
        }

        public static int GetBitCount(uint x)
        {
            int num = 0;
            while (x != 0)
            {
                x &= x - 1;
                num++;
            }
            return num;
        }

        public static bool IsEnumValid(Enum enumValue, int value, int minValue, int maxValue)
        {
            return value >= minValue && value <= maxValue;
        }

        public static bool IsEnumValid(Enum enumValue, int value, int minValue, int maxValue, int maxNumberOfBitsOn)
        {
            return value >= minValue && value <= maxValue && GetBitCount((uint)value) <= maxNumberOfBitsOn;
        }

        public static bool IsEnumValid_Masked(Enum enumValue, int value, uint mask)
        {
            return (value & mask) == value;
        }

        public static bool IsEnumValid_NotSequential(Enum enumValue, int value, params int[] enumValues)
        {
            for (int i = 0; i < enumValues.Length; i++)
            {
                if (enumValues[i] == value)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
