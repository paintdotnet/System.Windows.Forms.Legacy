// System.Windows.Forms.ContextMenu
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    /// <summary>Represents a shortcut menu. Although <see cref="T:System.Windows.Forms.ContextMenuStrip" /> replaces and adds functionality to the <see cref="T:System.Windows.Forms.ContextMenu" /> control of previous versions, <see cref="T:System.Windows.Forms.ContextMenu" /> is retained for both backward compatibility and future use if you choose.</summary>
    [DefaultEvent("Popup")]
    public class ContextMenu : Menu
    {
        private EventHandler onPopup;

        private EventHandler onCollapse;

        internal Control sourceControl;

        private RightToLeft rightToLeft = RightToLeft.Inherit;

        /// <summary>Gets the control that is displaying the shortcut menu.</summary>
        /// <returns>A <see cref="T:System.Windows.Forms.Control" /> that represents the control that is displaying the shortcut menu. If no control has displayed the shortcut menu, the property returns <see langword="null" />.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Control SourceControl
        {
            get
            {
                return sourceControl;
            }
        }

        /// <summary>Gets or sets a value indicating whether text displayed by the control is displayed from right to left.</summary>
        /// <returns>One of the <see cref="T:System.Windows.Forms.RightToLeft" /> values.</returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned to the property is not a valid member of the <see cref="T:System.Windows.Forms.RightToLeft" /> enumeration.</exception>
        [Localizable(true)]
        [DefaultValue(RightToLeft.No)]
        public virtual RightToLeft RightToLeft
        {
            get
            {
                if (RightToLeft.Inherit == rightToLeft)
                {
                    if (sourceControl != null)
                    {
                        return sourceControl.RightToLeft;
                    }
                    return RightToLeft.No;
                }
                return rightToLeft;
            }
            set
            {
                if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
                {
                    throw new InvalidEnumArgumentException("RightToLeft");
                }
                if (RightToLeft != value)
                {
                    rightToLeft = value;
                    UpdateRtl(value == RightToLeft.Yes);
                }
            }
        }

        internal override bool RenderIsRightToLeft => rightToLeft == RightToLeft.Yes;

        /// <summary>Occurs before the shortcut menu is displayed.</summary>
        public event EventHandler Popup
        {
            add
            {
                onPopup = (EventHandler)Delegate.Combine(onPopup, value);
            }
            remove
            {
                onPopup = (EventHandler)Delegate.Remove(onPopup, value);
            }
        }

        /// <summary>Occurs when the shortcut menu collapses.</summary>
        public event EventHandler Collapse
        {
            add
            {
                onCollapse = (EventHandler)Delegate.Combine(onCollapse, value);
            }
            remove
            {
                onCollapse = (EventHandler)Delegate.Remove(onCollapse, value);
            }
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ContextMenu" /> class with no menu items specified.</summary>
        public ContextMenu()
            : base(null)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ContextMenu" /> class with a specified set of <see cref="T:System.Windows.Forms.MenuItem" /> objects.</summary>
        /// <param name="menuItems">An array of <see cref="T:System.Windows.Forms.MenuItem" /> objects that represent the menu items to add to the shortcut menu.</param>
        public ContextMenu(MenuItem[] menuItems)
            : base(menuItems)
        {
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.ContextMenu.Popup" /> event</summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected internal virtual void OnPopup(EventArgs e)
        {
            if (onPopup != null)
            {
                onPopup(this, e);
            }
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.ContextMenu.Collapse" /> event.</summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected internal virtual void OnCollapse(EventArgs e)
        {
            if (onCollapse != null)
            {
                onCollapse(this, e);
            }
        }

        /// <summary>Processes a command key.</summary>
        /// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process.</param>
        /// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
        /// <param name="control">The control to which the command key applies.</param>
        /// <returns>
        ///   <see langword="true" /> if the character was processed by the control; otherwise, <see langword="false" />.</returns>
        protected internal virtual bool ProcessCmdKey(ref Message msg, Keys keyData, Control control)
        {
            sourceControl = control;
            return ProcessCmdKey(ref msg, keyData);
        }

        private void ResetRightToLeft()
        {
            RightToLeft = RightToLeft.No;
        }

        internal virtual bool ShouldSerializeRightToLeft()
        {
            if (RightToLeft.Inherit == rightToLeft)
            {
                return false;
            }
            return true;
        }

        /// <summary>Displays the shortcut menu at the specified position.</summary>
        /// <param name="control">A <see cref="T:System.Windows.Forms.Control" /> that specifies the control with which this shortcut menu is associated.</param>
        /// <param name="pos">A <see cref="T:System.Drawing.Point" /> that specifies the coordinates at which to display the menu. These coordinates are specified relative to the client coordinates of the control specified in the <paramref name="control" /> parameter.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="control" /> parameter is <see langword="null" />.</exception>
        /// <exception cref="T:System.ArgumentException">The handle of the control does not exist or the control is not visible.</exception>
        public void Show(Control control, Point pos)
        {
            Show(control, pos, 66);
        }

        /// <summary>Displays the shortcut menu at the specified position and with the specified alignment.</summary>
        /// <param name="control">A <see cref="T:System.Windows.Forms.Control" /> that specifies the control with which this shortcut menu is associated.</param>
        /// <param name="pos">A <see cref="T:System.Drawing.Point" /> that specifies the coordinates at which to display the menu. These coordinates are specified relative to the client coordinates of the control specified in the <paramref name="control" /> parameter.</param>
        /// <param name="alignment">A <see cref="T:System.Windows.Forms.LeftRightAlignment" /> that specifies the alignment of the control relative to the <paramref name="pos" /> parameter.</param>
        public void Show(Control control, Point pos, LeftRightAlignment alignment)
        {
            if (alignment == LeftRightAlignment.Left)
            {
                Show(control, pos, 74);
            }
            else
            {
                Show(control, pos, 66);
            }
        }

        private void Show(Control control, Point pos, int flags)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
            if (!control.IsHandleCreated || !control.Visible)
            {
                throw new ArgumentException("control");
            }
            sourceControl = control;
            OnPopup(EventArgs.Empty);
            pos = control.PointToScreen(pos);
            SafeNativeMethods.TrackPopupMenuEx(new HandleRef(this, base.Handle), flags, pos.X, pos.Y, new HandleRef(control, control.Handle), null);
        }
    }
}
