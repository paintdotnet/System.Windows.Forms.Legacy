// System.Windows.Forms.MainMenu
using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    /// <summary>Represents the menu structure of a form. Although <see cref="T:System.Windows.Forms.MenuStrip" /> replaces and adds functionality to the <see cref="T:System.Windows.Forms.MainMenu" /> control of previous versions, <see cref="T:System.Windows.Forms.MainMenu" /> is retained for both backward compatibility and future use if you choose.</summary>
    [ToolboxItemFilter("System.Windows.Forms.MainMenu")]
    public class MainMenu : Menu
    {
#pragma warning disable 0649
        internal Form form;
        internal Form ownerForm;
#pragma warning restore 0649

        private RightToLeft rightToLeft = RightToLeft.Inherit;

        private EventHandler onCollapse;

        /// <summary>Gets or sets whether the text displayed by the control is displayed from right to left.</summary>
        /// <returns>One of the <see cref="T:System.Windows.Forms.RightToLeft" /> values.</returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned to the property is not a valid member of the <see cref="T:System.Windows.Forms.RightToLeft" /> enumeration.</exception>
        [Localizable(true)]
        [AmbientValue(RightToLeft.Inherit)]
        public virtual RightToLeft RightToLeft
        {
            get
            {
                if (RightToLeft.Inherit == rightToLeft)
                {
                    if (form != null)
                    {
                        return form.RightToLeft;
                    }
                    return RightToLeft.Inherit;
                }
                return rightToLeft;
            }
            set
            {
                if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
                {
                    throw new InvalidEnumArgumentException("RightToLeft");
                }
                if (rightToLeft != value)
                {
                    rightToLeft = value;
                    UpdateRtl(value == RightToLeft.Yes);
                }
            }
        }

        internal override bool RenderIsRightToLeft
        {
            get
            {
                if (RightToLeft == RightToLeft.Yes)
                {
                    if (form != null)
                    {
                        return !form.IsMirrored;
                    }
                    return true;
                }
                return false;
            }
        }

        /// <summary>Occurs when the main menu collapses.</summary>
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

        /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MainMenu" /> class without any specified menu items.</summary>
        public MainMenu()
            : base(null)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MainMenu" /> class with the specified container.</summary>
        /// <param name="container">An <see cref="T:System.ComponentModel.IContainer" /> representing the container of the <see cref="T:System.Windows.Forms.MainMenu" />.</param>
        public MainMenu(IContainer container)
            : this()
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            container.Add(this);
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MainMenu" /> with a specified set of <see cref="T:System.Windows.Forms.MenuItem" /> objects.</summary>
        /// <param name="items">An array of <see cref="T:System.Windows.Forms.MenuItem" /> objects that will be added to the <see cref="T:System.Windows.Forms.MainMenu" />.</param>
        public MainMenu(MenuItem[] items)
            : base(items)
        {
        }

        /// <summary>Creates a new <see cref="T:System.Windows.Forms.MainMenu" /> that is a duplicate of the current <see cref="T:System.Windows.Forms.MainMenu" />.</summary>
        /// <returns>A <see cref="T:System.Windows.Forms.MainMenu" /> that represents the cloned menu.</returns>
        public virtual MainMenu CloneMenu()
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.CloneMenu(this);
            return mainMenu;
        }

        /// <summary>Creates a new handle to the Menu.</summary>
        /// <returns>A handle to the menu if the method succeeds; otherwise, <see langword="null" />.</returns>
        protected override IntPtr CreateMenuHandle()
        {
            return UnsafeNativeMethods.CreateMenu();
        }

        /// <summary>Disposes of the resources, other than memory, used by the <see cref="T:System.Windows.Forms.MainMenu" />.</summary>
        /// <param name="disposing">
        ///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && form != null && (ownerForm == null || form == ownerForm))
            {
                //form.Menu = null;
            }
            base.Dispose(disposing);
        }

        /// <summary>Gets the <see cref="T:System.Windows.Forms.Form" /> that contains this control.</summary>
        /// <returns>A <see cref="T:System.Windows.Forms.Form" /> that is the container for this control. Returns <see langword="null" /> if the <see cref="T:System.Windows.Forms.MainMenu" /> is not currently hosted on a form.</returns>
        public Form GetForm()
        {
            return form;
        }

        internal Form GetFormUnsafe()
        {
            return form;
        }

        internal override void ItemsChanged(int change)
        {
            base.ItemsChanged(change);
            if (form != null)
            {
                //form.MenuChanged(change, this);
            }
        }

        internal virtual void ItemsChanged(int change, Menu menu)
        {
            if (form != null)
            {
                //form.MenuChanged(change, menu);
            }
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.MainMenu.Collapse" /> event.</summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected internal virtual void OnCollapse(EventArgs e)
        {
            if (onCollapse != null)
            {
                onCollapse(this, e);
            }
        }

        internal virtual bool ShouldSerializeRightToLeft()
        {
            if (RightToLeft.Inherit == RightToLeft)
            {
                return false;
            }
            return true;
        }

        /// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.MainMenu" />.</summary>
        /// <returns>A string that represents the current <see cref="T:System.Windows.Forms.MainMenu" />.</returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
