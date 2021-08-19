// System.Windows.Forms.WindowsFormsUtils
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Internal;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Internal;

namespace System.Windows.Forms
{
    internal sealed class WindowsFormsUtils
    {
        public static class EnumValidator
        {
            public static bool IsValidContentAlignment(ContentAlignment contentAlign)
            {
                if (ClientUtils.GetBitCount((uint)contentAlign) != 1)
                {
                    return false;
                }
                int num = 1911;
                return ((uint)num & (uint)contentAlign) != 0;
            }

            public static bool IsEnumWithinShiftedRange(Enum enumValue, int numBitsToShift, int minValAfterShift, int maxValAfterShift)
            {
                int num = Convert.ToInt32(enumValue, CultureInfo.InvariantCulture);
                int num2 = num >> numBitsToShift;
                if (num2 << numBitsToShift != num)
                {
                    return false;
                }
                if (num2 >= minValAfterShift)
                {
                    return num2 <= maxValAfterShift;
                }
                return false;
            }

            public static bool IsValidTextImageRelation(TextImageRelation relation)
            {
                return ClientUtils.IsEnumValid(relation, (int)relation, 0, 8, 1);
            }

            public static bool IsValidArrowDirection(ArrowDirection direction)
            {
                if ((uint)direction <= 1u || (uint)(direction - 16) <= 1u)
                {
                    return true;
                }
                return false;
            }
        }

        public class ArraySubsetEnumerator : IEnumerator
        {
            private object[] array;

            private int total;

            private int current;

            public object Current
            {
                get
                {
                    if (current == -1)
                    {
                        return null;
                    }
                    return array[current];
                }
            }

            public ArraySubsetEnumerator(object[] array, int count)
            {
                this.array = array;
                total = count;
                current = -1;
            }

            public bool MoveNext()
            {
                if (current < total - 1)
                {
                    current++;
                    return true;
                }
                return false;
            }

            public void Reset()
            {
                current = -1;
            }
        }

        internal class ReadOnlyControlCollection : Control.ControlCollection
        {
            private readonly bool _isReadOnly;

            public override bool IsReadOnly => _isReadOnly;

            public ReadOnlyControlCollection(Control owner, bool isReadOnly)
                : base(owner)
            {
                _isReadOnly = isReadOnly;
            }

            public override void Add(Control value)
            {
                if (IsReadOnly)
                {
                    throw new NotSupportedException("ReadonlyControlsCollection");
                }
                AddInternal(value);
            }

            internal virtual void AddInternal(Control value)
            {
                base.Add(value);
            }

            public override void Clear()
            {
                if (IsReadOnly)
                {
                    throw new NotSupportedException("ReadonlyControlsCollection");
                }
                base.Clear();
            }

            internal virtual void RemoveInternal(Control value)
            {
                base.Remove(value);
            }

            public override void RemoveByKey(string key)
            {
                if (IsReadOnly)
                {
                    throw new NotSupportedException("ReadonlyControlsCollection");
                }
                base.RemoveByKey(key);
            }
        }

        internal class TypedControlCollection : ReadOnlyControlCollection
        {
            private Type typeOfControl;

            private Control ownerControl;

            public TypedControlCollection(Control owner, Type typeOfControl, bool isReadOnly)
                : base(owner, isReadOnly)
            {
                this.typeOfControl = typeOfControl;
                ownerControl = owner;
            }

            public TypedControlCollection(Control owner, Type typeOfControl)
                : base(owner, isReadOnly: false)
            {
                this.typeOfControl = typeOfControl;
                ownerControl = owner;
            }

            public override void Add(Control value)
            {
                ControlPrivate.CheckParentingCycle(ownerControl, value);
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                if (IsReadOnly)
                {
                    throw new NotSupportedException("ReadonlyControlsCollection");
                }
                if (!typeOfControl.IsAssignableFrom(value.GetType()))
                {
                    throw new ArgumentException("TypedControlCollectionShouldBeOfType");
                }
                base.Add(value);
            }
        }

        internal struct DCMapping : IDisposable
        {
            private DeviceContext dc;

            private Graphics graphics;

            private Rectangle translatedBounds;

            public Graphics Graphics
            {
                get
                {
                    if (graphics == null)
                    {
                        graphics = Graphics.FromHdcInternal(dc.Hdc);
                        graphics.SetClip(new Rectangle(Point.Empty, translatedBounds.Size));
                    }
                    return graphics;
                }
            }

            public DCMapping(HandleRef hDC, Rectangle bounds)
            {
                if (hDC.Handle == IntPtr.Zero)
                {
                    throw new ArgumentNullException("hDC");
                }
                NativeMethods.POINT pOINT = new NativeMethods.POINT();
                HandleRef handleRef = NativeMethods.NullHandleRef;
                NativeMethods.RegionFlags regionFlags = NativeMethods.RegionFlags.NULLREGION;
                translatedBounds = bounds;
                graphics = null;
                dc = DeviceContext.FromHdc(hDC.Handle);
                dc.SaveHdc();
                bool viewportOrgEx = SafeNativeMethods.GetViewportOrgEx(hDC, pOINT);
                HandleRef handleRef2 = new HandleRef(null, SafeNativeMethods.CreateRectRgn(pOINT.x + bounds.Left, pOINT.y + bounds.Top, pOINT.x + bounds.Right, pOINT.y + bounds.Bottom));
                try
                {
                    handleRef = new HandleRef(this, SafeNativeMethods.CreateRectRgn(0, 0, 0, 0));
                    int clipRgn = SafeNativeMethods.GetClipRgn(hDC, handleRef);
                    NativeMethods.POINT point = new NativeMethods.POINT();
                    viewportOrgEx = SafeNativeMethods.SetViewportOrgEx(hDC, pOINT.x + bounds.Left, pOINT.y + bounds.Top, point);
                    if (clipRgn != 0)
                    {
                        NativeMethods.RECT clipRect = default(NativeMethods.RECT);
                        regionFlags = (NativeMethods.RegionFlags)SafeNativeMethods.GetRgnBox(handleRef, ref clipRect);
                        if (regionFlags == NativeMethods.RegionFlags.SIMPLEREGION)
                        {
                            NativeMethods.RegionFlags regionFlags2 = (NativeMethods.RegionFlags)SafeNativeMethods.CombineRgn(handleRef2, handleRef2, handleRef, 1);
                        }
                    }
                    else
                    {
                        SafeNativeMethods.DeleteObject(handleRef);
                        handleRef = new HandleRef(null, IntPtr.Zero);
                        regionFlags = NativeMethods.RegionFlags.SIMPLEREGION;
                    }
                    NativeMethods.RegionFlags regionFlags3 = (NativeMethods.RegionFlags)SafeNativeMethods.SelectClipRgn(hDC, handleRef2);
                }
                catch (Exception ex)
                {
                    if (ClientUtils.IsSecurityOrCriticalException(ex))
                    {
                        throw;
                    }
                    dc.RestoreHdc();
                    dc.Dispose();
                }
                finally
                {
                    viewportOrgEx = SafeNativeMethods.DeleteObject(handleRef2);
                    if (handleRef.Handle != IntPtr.Zero)
                    {
                        viewportOrgEx = SafeNativeMethods.DeleteObject(handleRef);
                    }
                }
            }

            public void Dispose()
            {
                if (graphics != null)
                {
                    graphics.Dispose();
                    graphics = null;
                }
                if (dc != null)
                {
                    dc.RestoreHdc();
                    dc.Dispose();
                    dc = null;
                }
            }
        }

        public static readonly Size UninitializedSize = new Size(-7199369, -5999471);

        private static bool _targetsAtLeast_v4_5 = true; // RunningOnCheck("TargetsAtLeast_Desktop_V4_5");

        public static readonly ContentAlignment AnyRightAlign = (ContentAlignment)1092;

        public static readonly ContentAlignment AnyLeftAlign = (ContentAlignment)273;

        public static readonly ContentAlignment AnyTopAlign = (ContentAlignment)7;

        public static readonly ContentAlignment AnyBottomAlign = (ContentAlignment)1792;

        public static readonly ContentAlignment AnyMiddleAlign = (ContentAlignment)112;

        public static readonly ContentAlignment AnyCenterAlign = (ContentAlignment)546;

        public static Point LastCursorPoint
        {
            get
            {
                int messagePos = SafeNativeMethods.GetMessagePos();
                return new Point(NativeMethods.Util.SignedLOWORD(messagePos), NativeMethods.Util.SignedHIWORD(messagePos));
            }
        }

        internal static bool TargetsAtLeast_v4_5 => _targetsAtLeast_v4_5;

        public static bool ContainsMnemonic(string text)
        {
            if (text != null)
            {
                int length = text.Length;
                int num = text.IndexOf('&', 0);
                if (num >= 0 && num <= length - 2)
                {
                    int num2 = text.IndexOf('&', num + 1);
                    if (num2 == -1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        internal static Rectangle ConstrainToScreenWorkingAreaBounds(Rectangle bounds)
        {
            return ConstrainToBounds(Screen.GetWorkingArea(bounds), bounds);
        }

        internal static Rectangle ConstrainToScreenBounds(Rectangle bounds)
        {
            return ConstrainToBounds(Screen.FromRectangle(bounds).Bounds, bounds);
        }

        internal static Rectangle ConstrainToBounds(Rectangle constrainingBounds, Rectangle bounds)
        {
            if (!constrainingBounds.Contains(bounds))
            {
                bounds.Size = new Size(Math.Min(constrainingBounds.Width - 2, bounds.Width), Math.Min(constrainingBounds.Height - 2, bounds.Height));
                if (bounds.Right > constrainingBounds.Right)
                {
                    bounds.X = constrainingBounds.Right - bounds.Width;
                }
                else if (bounds.Left < constrainingBounds.Left)
                {
                    bounds.X = constrainingBounds.Left;
                }
                if (bounds.Bottom > constrainingBounds.Bottom)
                {
                    bounds.Y = constrainingBounds.Bottom - 1 - bounds.Height;
                }
                else if (bounds.Top < constrainingBounds.Top)
                {
                    bounds.Y = constrainingBounds.Top;
                }
            }
            return bounds;
        }

        internal static string EscapeTextWithAmpersands(string text)
        {
            if (text == null)
            {
                return null;
            }
            int i = text.IndexOf('&');
            if (i == -1)
            {
                return text;
            }
            StringBuilder stringBuilder = new StringBuilder(text.Substring(0, i));
            for (; i < text.Length; i++)
            {
                if (text[i] == '&')
                {
                    stringBuilder.Append("&");
                }
                if (i < text.Length)
                {
                    stringBuilder.Append(text[i]);
                }
            }
            return stringBuilder.ToString();
        }

        internal static string GetControlInformation(IntPtr hwnd)
        {
            if (hwnd == IntPtr.Zero)
            {
                return "Handle is IntPtr.Zero";
            }
            return "";
        }

        internal static string AssertControlInformation(bool condition, Control control)
        {
            if (condition)
            {
                return string.Empty;
            }
            return GetControlInformation(control.Handle);
        }

        internal static int GetCombinedHashCodes(params int[] args)
        {
            int num = -757577119;
            for (int i = 0; i < args.Length; i++)
            {
                num = (args[i] ^ num) * -1640531535;
            }
            return num;
        }

        public static char GetMnemonic(string text, bool bConvertToUpperCase)
        {
            char result = '\0';
            if (text != null)
            {
                int length = text.Length;
                for (int i = 0; i < length - 1; i++)
                {
                    if (text[i] == '&')
                    {
                        if (text[i + 1] != '&')
                        {
                            result = ((!bConvertToUpperCase) ? char.ToLower(text[i + 1], CultureInfo.CurrentCulture) : char.ToUpper(text[i + 1], CultureInfo.CurrentCulture));
                            break;
                        }
                        i++;
                    }
                }
            }
            return result;
        }

        public static HandleRef GetRootHWnd(HandleRef hwnd)
        {
            IntPtr ancestor = UnsafeNativeMethods.GetAncestor(new HandleRef(hwnd, hwnd.Handle), 2);
            return new HandleRef(hwnd.Wrapper, ancestor);
        }

        public static HandleRef GetRootHWnd(Control control)
        {
            return GetRootHWnd(new HandleRef(control, control.Handle));
        }

        public static string TextWithoutMnemonics(string text)
        {
            if (text == null)
            {
                return null;
            }
            int i = text.IndexOf('&');
            if (i == -1)
            {
                return text;
            }
            StringBuilder stringBuilder = new StringBuilder(text.Substring(0, i));
            for (; i < text.Length; i++)
            {
                if (text[i] == '&')
                {
                    i++;
                }
                if (i < text.Length)
                {
                    stringBuilder.Append(text[i]);
                }
            }
            return stringBuilder.ToString();
        }

        public static Point TranslatePoint(Point point, Control fromControl, Control toControl)
        {
            NativeMethods.POINT pOINT = new NativeMethods.POINT(point.X, point.Y);
            UnsafeNativeMethods.MapWindowPoints(new HandleRef(fromControl, fromControl.Handle), new HandleRef(toControl, toControl.Handle), pOINT, 1);
            return new Point(pOINT.x, pOINT.y);
        }

        public static bool SafeCompareStrings(string string1, string string2, bool ignoreCase)
        {
            if (string1 == null || string2 == null)
            {
                return false;
            }
            if (string1.Length != string2.Length)
            {
                return false;
            }
            return string.Compare(string1, string2, ignoreCase, CultureInfo.InvariantCulture) == 0;
        }

        public static int RotateLeft(int value, int nBits)
        {
            nBits %= 32;
            return (value << nBits) | (value >> 32 - nBits);
        }

        public static string GetComponentName(IComponent component, string defaultNameValue)
        {
            string text = string.Empty;
            if (string.IsNullOrEmpty(defaultNameValue))
            {
                if (component.Site != null)
                {
                    text = component.Site.Name;
                }
                if (text == null)
                {
                    text = string.Empty;
                }
            }
            else
            {
                text = defaultNameValue;
            }
            return text;
        }

        /*
        [SecuritySafeCritical]
        private static bool RunningOnCheck(string propertyName)
        {
            Type type;
            try
            {
                type = typeof(object).GetTypeInfo().Assembly.GetType("System.Runtime.Versioning.BinaryCompatibility", throwOnError: false);
            }
            catch (TypeLoadException)
            {
                return false;
            }
            if (type == null)
            {
                return false;
            }
            PropertyInfo property = type.GetProperty(propertyName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            if (property == null)
            {
                return false;
            }
            return (bool)property.GetValue(null);
        }
        */
    }
}
