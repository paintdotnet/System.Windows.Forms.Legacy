// System.Windows.Forms.MenuItem
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    /// <summary>Represents an individual item that is displayed within a <see cref="T:System.Windows.Forms.MainMenu" /> or <see cref="T:System.Windows.Forms.ContextMenu" />. Although <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> replaces and adds functionality to the <see cref="T:System.Windows.Forms.MenuItem" /> control of previous versions, <see cref="T:System.Windows.Forms.MenuItem" /> is retained for both backward compatibility and future use if you choose.</summary>
    [ToolboxItem(false)]
    [DesignTimeVisible(false)]
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    public class MenuItem : Menu
    {
        private struct MsaaMenuInfoWithId
        {
            public NativeMethods.MSAAMENUINFO msaaMenuInfo;

            public uint uniqueID;

            public MsaaMenuInfoWithId(string text, uint uniqueID)
            {
                msaaMenuInfo = new NativeMethods.MSAAMENUINFO(text);
                this.uniqueID = uniqueID;
            }
        }

        internal class MenuItemData : ICommandExecutor
        {
            internal MenuItem baseItem;

            internal MenuItem firstItem;

            private int state;

            internal int version;

            internal MenuMerge mergeType;

            internal int mergeOrder;

            internal string caption;

            internal short mnemonic;

            internal Shortcut shortcut;

            internal bool showShortcut;

            internal EventHandler onClick;

            internal EventHandler onPopup;

            internal EventHandler onSelect;

            internal DrawItemEventHandler onDrawItem;

            internal MeasureItemEventHandler onMeasureItem;

            private object userData;

            internal Command cmd;

            internal bool OwnerDraw
            {
                get
                {
                    return (State & 0x100) != 0;
                }
                set
                {
                    SetState(256, value);
                }
            }

            internal bool MdiList
            {
                get
                {
                    return HasState(131072);
                }
                set
                {
                    if ((state & 0x20000) != 0 != value)
                    {
                        SetState(131072, value);
                        for (MenuItem nextLinkedItem = firstItem; nextLinkedItem != null; nextLinkedItem = nextLinkedItem.nextLinkedItem)
                        {
                            nextLinkedItem.ItemsChanged(2);
                        }
                    }
                }
            }

            internal MenuMerge MergeType
            {
                get
                {
                    return mergeType;
                }
                set
                {
                    if (mergeType != value)
                    {
                        mergeType = value;
                        ItemsChanged(3);
                    }
                }
            }

            internal int MergeOrder
            {
                get
                {
                    return mergeOrder;
                }
                set
                {
                    if (mergeOrder != value)
                    {
                        mergeOrder = value;
                        ItemsChanged(3);
                    }
                }
            }

            internal char Mnemonic
            {
                get
                {
                    if (mnemonic == -1)
                    {
                        mnemonic = (short)WindowsFormsUtils.GetMnemonic(caption, bConvertToUpperCase: true);
                    }
                    return (char)mnemonic;
                }
            }

            internal int State => state;

            internal bool Visible
            {
                get
                {
                    return (state & 0x10000) == 0;
                }
                set
                {
                    if ((state & 0x10000) == 0 != value)
                    {
                        state = (value ? (state & -65537) : (state | 0x10000));
                        ItemsChanged(1);
                    }
                }
            }

            internal object UserData
            {
                get
                {
                    return userData;
                }
                set
                {
                    userData = value;
                }
            }

            internal MenuItemData(MenuItem baseItem, MenuMerge mergeType, int mergeOrder, Shortcut shortcut, bool showShortcut, string caption, EventHandler onClick, EventHandler onPopup, EventHandler onSelect, DrawItemEventHandler onDrawItem, MeasureItemEventHandler onMeasureItem)
            {
                AddItem(baseItem);
                this.mergeType = mergeType;
                this.mergeOrder = mergeOrder;
                this.shortcut = shortcut;
                this.showShortcut = showShortcut;
                this.caption = ((caption == null) ? "" : caption);
                this.onClick = onClick;
                this.onPopup = onPopup;
                this.onSelect = onSelect;
                this.onDrawItem = onDrawItem;
                this.onMeasureItem = onMeasureItem;
                version = 1;
                mnemonic = -1;
            }

            internal void AddItem(MenuItem item)
            {
                if (item.data != this)
                {
                    if (item.data != null)
                    {
                        item.data.RemoveItem(item);
                    }
                    item.nextLinkedItem = firstItem;
                    firstItem = item;
                    if (baseItem == null)
                    {
                        baseItem = item;
                    }
                    item.data = this;
                    item.dataVersion = 0;
                    item.UpdateMenuItem(force: false);
                }
            }

            public void Execute()
            {
                if (baseItem != null)
                {
                    baseItem.OnClick(EventArgs.Empty);
                }
            }

            internal int GetMenuID()
            {
                if (cmd == null)
                {
                    cmd = new Command(this);
                }
                return cmd.ID;
            }

            internal void ItemsChanged(int change)
            {
                for (MenuItem nextLinkedItem = firstItem; nextLinkedItem != null; nextLinkedItem = nextLinkedItem.nextLinkedItem)
                {
                    if (nextLinkedItem.menu != null)
                    {
                        nextLinkedItem.menu.ItemsChanged(change);
                    }
                }
            }

            internal void RemoveItem(MenuItem item)
            {
                if (item == firstItem)
                {
                    firstItem = item.nextLinkedItem;
                }
                else
                {
                    MenuItem nextLinkedItem = firstItem;
                    while (item != nextLinkedItem.nextLinkedItem)
                    {
                        nextLinkedItem = nextLinkedItem.nextLinkedItem;
                    }
                    nextLinkedItem.nextLinkedItem = item.nextLinkedItem;
                }
                item.nextLinkedItem = null;
                item.data = null;
                item.dataVersion = 0;
                if (item == baseItem)
                {
                    baseItem = firstItem;
                }
                if (firstItem == null)
                {
                    onClick = null;
                    onPopup = null;
                    onSelect = null;
                    onDrawItem = null;
                    onMeasureItem = null;
                    if (cmd != null)
                    {
                        cmd.Dispose();
                        cmd = null;
                    }
                }
            }

            internal void SetCaption(string value)
            {
                if (value == null)
                {
                    value = "";
                }
                if (!caption.Equals(value))
                {
                    caption = value;
                    UpdateMenuItems();
                }
            }

            internal bool HasState(int flag)
            {
                return (State & flag) == flag;
            }

            internal void SetState(int flag, bool value)
            {
                if ((state & flag) != 0 != value)
                {
                    state = (value ? (state | flag) : (state & ~flag));
                    UpdateMenuItems();
                }
            }

            internal void UpdateMenuItems()
            {
                version++;
                for (MenuItem nextLinkedItem = firstItem; nextLinkedItem != null; nextLinkedItem = nextLinkedItem.nextLinkedItem)
                {
                    nextLinkedItem.UpdateMenuItem(force: true);
                }
            }
        }

        private class MdiListUserData
        {
            public virtual void OnClick(EventArgs e)
            {
            }
        }

        private class MdiListFormData : MdiListUserData
        {
            private MenuItem parent;

            private int boundIndex;

            public MdiListFormData(MenuItem parentItem, int boundFormIndex)
            {
                boundIndex = boundFormIndex;
                parent = parentItem;
            }

            public override void OnClick(EventArgs e)
            {
                if (boundIndex == -1)
                {
                    return;
                }
                Form[] array = parent.FindMdiForms();
                if (array != null && array.Length > boundIndex)
                {
                    Form form = array[boundIndex];
                    form.Activate();
                    if (form.ActiveControl != null && !form.ActiveControl.Focused)
                    {
                        form.ActiveControl.Focus();
                    }
                }
            }
        }

        private class MdiListMoreWindowsData : MdiListUserData
        {
            private MenuItem parent;

            public MdiListMoreWindowsData(MenuItem parent)
            {
                this.parent = parent;
            }

            public override void OnClick(EventArgs e)
            {
                Form[] array = parent.FindMdiForms();
                Form activeMdiChild = parent.GetMainMenu().GetFormUnsafe().ActiveMdiChild;
                if (array == null || array.Length == 0 || activeMdiChild == null)
                {
                    return;
                }
                using MdiWindowDialog mdiWindowDialog = new MdiWindowDialog();
                mdiWindowDialog.SetItems(activeMdiChild, array);
                DialogResult dialogResult = mdiWindowDialog.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    mdiWindowDialog.ActiveChildForm.Activate();
                    if (mdiWindowDialog.ActiveChildForm.ActiveControl != null && !mdiWindowDialog.ActiveChildForm.ActiveControl.Focused)
                    {
                        mdiWindowDialog.ActiveChildForm.ActiveControl.Focus();
                    }
                }
            }
        }

        internal const int STATE_BARBREAK = 32;

        internal const int STATE_BREAK = 64;

        internal const int STATE_CHECKED = 8;

        internal const int STATE_DEFAULT = 4096;

        internal const int STATE_DISABLED = 3;

        internal const int STATE_RADIOCHECK = 512;

        internal const int STATE_HIDDEN = 65536;

        internal const int STATE_MDILIST = 131072;

        internal const int STATE_CLONE_MASK = 201579;

        internal const int STATE_OWNERDRAW = 256;

        internal const int STATE_INMDIPOPUP = 512;

        internal const int STATE_HILITE = 128;

        private Menu menu;

        private bool hasHandle;

        private MenuItemData data;

        private int dataVersion;

        private MenuItem nextLinkedItem;

        private static Hashtable allCreatedMenuItems = new Hashtable();

        private const uint firstUniqueID = 3221225472u;

        private static long nextUniqueID = 3221225472L;

        private uint uniqueID;

        private IntPtr msaaMenuInfoPtr = IntPtr.Zero;

        private bool menuItemIsCreated;

        /// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.MenuItem" /> is placed on a new line (for a menu item added to a <see cref="T:System.Windows.Forms.MainMenu" /> object) or in a new column (for a submenu item or menu item displayed in a <see cref="T:System.Windows.Forms.ContextMenu" />).</summary>
        /// <returns>
        ///   <see langword="true" /> if the menu item is placed on a new line or in a new column; <see langword="false" /> if the menu item is left in its default placement. The default is <see langword="false" />.</returns>
        [Browsable(false)]
        [DefaultValue(false)]
        public bool BarBreak
        {
            get
            {
                return (data.State & 0x20) != 0;
            }
            set
            {
                data.SetState(32, value);
            }
        }

        /// <summary>Gets or sets a value indicating whether the item is placed on a new line (for a menu item added to a <see cref="T:System.Windows.Forms.MainMenu" /> object) or in a new column (for a menu item or submenu item displayed in a <see cref="T:System.Windows.Forms.ContextMenu" />).</summary>
        /// <returns>
        ///   <see langword="true" /> if the menu item is placed on a new line or in a new column; <see langword="false" /> if the menu item is left in its default placement. The default is <see langword="false" />.</returns>
        [Browsable(false)]
        [DefaultValue(false)]
        public bool Break
        {
            get
            {
                return (data.State & 0x40) != 0;
            }
            set
            {
                data.SetState(64, value);
            }
        }

        /// <summary>Gets or sets a value indicating whether a check mark appears next to the text of the menu item.</summary>
        /// <returns>
        ///   <see langword="true" /> if there is a check mark next to the menu item; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
        /// <exception cref="T:System.ArgumentException">The <see cref="T:System.Windows.Forms.MenuItem" /> is a top-level menu or has children.</exception>
        [DefaultValue(false)]
        public bool Checked
        {
            get
            {
                return (data.State & 8) != 0;
            }
            set
            {
                if (value && (base.ItemCount != 0 || (Parent != null && Parent is MainMenu)))
                {
                    throw new ArgumentException("MenuItemInvalidCheckProperty");
                }
                data.SetState(8, value);
            }
        }

        /// <summary>Gets or sets a value indicating whether the menu item is the default menu item.</summary>
        /// <returns>
        ///   <see langword="true" /> if the menu item is the default item in a menu; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
        [DefaultValue(false)]
        public bool DefaultItem
        {
            get
            {
                return (data.State & 0x1000) != 0;
            }
            set
            {
                if (menu != null)
                {
                    if (value)
                    {
                        UnsafeNativeMethods.SetMenuDefaultItem(new HandleRef(menu, menu.handle), MenuID, pos: false);
                    }
                    else if (DefaultItem)
                    {
                        UnsafeNativeMethods.SetMenuDefaultItem(new HandleRef(menu, menu.handle), -1, pos: false);
                    }
                }
                data.SetState(4096, value);
            }
        }

        /// <summary>Gets or sets a value indicating whether the code that you provide draws the menu item or Windows draws the menu item.</summary>
        /// <returns>
        ///   <see langword="true" /> if the menu item is to be drawn using code; <see langword="false" /> if the menu item is to be drawn by Windows. The default is <see langword="false" />.</returns>
        [DefaultValue(false)]
        public bool OwnerDraw
        {
            get
            {
                return (data.State & 0x100) != 0;
            }
            set
            {
                data.SetState(256, value);
            }
        }

        /// <summary>Gets or sets a value indicating whether the menu item is enabled.</summary>
        /// <returns>
        ///   <see langword="true" /> if the menu item is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
        [Localizable(true)]
        [DefaultValue(true)]
        public bool Enabled
        {
            get
            {
                return (data.State & 3) == 0;
            }
            set
            {
                data.SetState(3, !value);
            }
        }

        /// <summary>Gets or sets a value indicating the position of the menu item in its parent menu.</summary>
        /// <returns>The zero-based index representing the position of the menu item in its parent menu.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than zero or greater than the item count.</exception>
        [Browsable(false)]
        public int Index
        {
            get
            {
                if (menu != null)
                {
                    for (int i = 0; i < menu.ItemCount; i++)
                    {
                        if (menu.items[i] == this)
                        {
                            return i;
                        }
                    }
                }
                return -1;
            }
            set
            {
                int index = Index;
                if (index >= 0)
                {
                    if (value < 0 || value >= this.menu.ItemCount)
                    {
                        throw new ArgumentOutOfRangeException("Index");
                    }
                    if (value != index)
                    {
                        Menu menu = this.menu;
                        menu.MenuItems.RemoveAt(index);
                        menu.MenuItems.Add(value, this);
                    }
                }
            }
        }

        /// <summary>Gets a value indicating whether the menu item contains child menu items.</summary>
        /// <returns>
        ///   <see langword="true" /> if the menu item contains child menu items; <see langword="false" /> if the menu is a standalone menu item.</returns>
        [Browsable(false)]
        public override bool IsParent
        {
            get
            {
                bool flag = false;
                if (data != null && MdiList)
                {
                    for (int i = 0; i < base.ItemCount; i++)
                    {
                        if (!(items[i].data.UserData is MdiListUserData))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag && FindMdiForms().Length != 0)
                    {
                        flag = true;
                    }
                    if (!flag && menu != null && !(menu is MenuItem))
                    {
                        flag = true;
                    }
                }
                else
                {
                    flag = base.IsParent;
                }
                return flag;
            }
        }

        /// <summary>Gets or sets a value indicating whether the menu item will be populated with a list of the Multiple Document Interface (MDI) child windows that are displayed within the associated form.</summary>
        /// <returns>
        ///   <see langword="true" /> if a list of the MDI child windows is displayed in this menu item; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
        [DefaultValue(false)]
        public bool MdiList
        {
            get
            {
                return (data.State & 0x20000) != 0;
            }
            set
            {
                data.MdiList = value;
                CleanListItems(this);
            }
        }

        internal Menu Menu
        {
            get
            {
                return menu;
            }
            set
            {
                menu = value;
            }
        }

        /// <summary>Gets a value indicating the Windows identifier for this menu item.</summary>
        /// <returns>The Windows identifier for this menu item.</returns>
        protected int MenuID => data.GetMenuID();

        internal bool Selected
        {
            get
            {
                if (menu == null)
                {
                    return false;
                }
                NativeMethods.MENUITEMINFO_T mENUITEMINFO_T = new NativeMethods.MENUITEMINFO_T();
                mENUITEMINFO_T.cbSize = Marshal.SizeOf(typeof(NativeMethods.MENUITEMINFO_T));
                mENUITEMINFO_T.fMask = 1;
                UnsafeNativeMethods.GetMenuItemInfo(new HandleRef(menu, menu.handle), MenuID, fByPosition: false, mENUITEMINFO_T);
                return (mENUITEMINFO_T.fState & 0x80) != 0;
            }
        }

        internal int MenuIndex
        {
            get
            {
                if (menu == null)
                {
                    return -1;
                }
                int menuItemCount = UnsafeNativeMethods.GetMenuItemCount(new HandleRef(menu, menu.Handle));
                int menuID = MenuID;
                NativeMethods.MENUITEMINFO_T mENUITEMINFO_T = new NativeMethods.MENUITEMINFO_T();
                mENUITEMINFO_T.cbSize = Marshal.SizeOf(typeof(NativeMethods.MENUITEMINFO_T));
                mENUITEMINFO_T.fMask = 6;
                for (int i = 0; i < menuItemCount; i++)
                {
                    UnsafeNativeMethods.GetMenuItemInfo(new HandleRef(menu, menu.handle), i, fByPosition: true, mENUITEMINFO_T);
                    if ((mENUITEMINFO_T.hSubMenu == IntPtr.Zero || mENUITEMINFO_T.hSubMenu == base.Handle) && mENUITEMINFO_T.wID == menuID)
                    {
                        return i;
                    }
                }
                return -1;
            }
        }

        /// <summary>Gets or sets a value indicating the behavior of this menu item when its menu is merged with another.</summary>
        /// <returns>A <see cref="T:System.Windows.Forms.MenuMerge" /> value that represents the menu item's merge type.</returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.MenuMerge" /> values.</exception>
        [DefaultValue(MenuMerge.Add)]
        public MenuMerge MergeType
        {
            get
            {
                return data.mergeType;
            }
            set
            {
                if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
                {
                    throw new InvalidEnumArgumentException("value");
                }
                data.MergeType = value;
            }
        }

        /// <summary>Gets or sets a value indicating the relative position of the menu item when it is merged with another.</summary>
        /// <returns>A zero-based index representing the merge order position for this menu item. The default is 0.</returns>
        [DefaultValue(0)]
        public int MergeOrder
        {
            get
            {
                return data.mergeOrder;
            }
            set
            {
                data.MergeOrder = value;
            }
        }

        /// <summary>Gets a value indicating the mnemonic character that is associated with this menu item.</summary>
        /// <returns>A character that represents the mnemonic character associated with this menu item. Returns the NUL character (ASCII value 0) if no mnemonic character is specified in the text of the <see cref="T:System.Windows.Forms.MenuItem" />.</returns>
        [Browsable(false)]
        public char Mnemonic => data.Mnemonic;

        /// <summary>Gets a value indicating the menu that contains this menu item.</summary>
        /// <returns>A <see cref="T:System.Windows.Forms.Menu" /> that represents the menu that contains this menu item.</returns>
        [Browsable(false)]
        public Menu Parent => menu;

        /// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.MenuItem" />, if checked, displays a radio-button instead of a check mark.</summary>
        /// <returns>
        ///   <see langword="true" /> if a radio-button is to be used instead of a check mark; <see langword="false" /> if the standard check mark is to be displayed when the menu item is checked. The default is <see langword="false" />.</returns>
        [DefaultValue(false)]
        public bool RadioCheck
        {
            get
            {
                return (data.State & 0x200) != 0;
            }
            set
            {
                data.SetState(512, value);
            }
        }

        internal override bool RenderIsRightToLeft
        {
            get
            {
                if (Parent == null)
                {
                    return false;
                }
                return Parent.RenderIsRightToLeft;
            }
        }

        /// <summary>Gets or sets a value indicating the caption of the menu item.</summary>
        /// <returns>The text caption of the menu item.</returns>
        [Localizable(true)]
        public string Text
        {
            get
            {
                return data.caption;
            }
            set
            {
                data.SetCaption(value);
            }
        }

        /// <summary>Gets or sets a value indicating the shortcut key associated with the menu item.</summary>
        /// <returns>One of the <see cref="T:System.Windows.Forms.Shortcut" /> values. The default is <see langword="Shortcut.None" />.</returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.Shortcut" /> values.</exception>
        [Localizable(true)]
        [DefaultValue(Shortcut.None)]
        public Shortcut Shortcut
        {
            get
            {
                return data.shortcut;
            }
            set
            {
                if (!Enum.IsDefined(typeof(Shortcut), value))
                {
                    throw new InvalidEnumArgumentException("value");
                }
                data.shortcut = value;
                UpdateMenuItem(force: true);
            }
        }

        /// <summary>Gets or sets a value indicating whether the shortcut key that is associated with the menu item is displayed next to the menu item caption.</summary>
        /// <returns>
        ///   <see langword="true" /> if the shortcut key combination is displayed next to the menu item caption; <see langword="false" /> if the shortcut key combination is not to be displayed. The default is <see langword="true" />.</returns>
        [DefaultValue(true)]
        [Localizable(true)]
        public bool ShowShortcut
        {
            get
            {
                return data.showShortcut;
            }
            set
            {
                if (value != data.showShortcut)
                {
                    data.showShortcut = value;
                    UpdateMenuItem(force: true);
                }
            }
        }

        /// <summary>Gets or sets a value indicating whether the menu item is visible.</summary>
        /// <returns>
        ///   <see langword="true" /> if the menu item will be made visible on the menu; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
        [Localizable(true)]
        [DefaultValue(true)]
        public bool Visible
        {
            get
            {
                return (data.State & 0x10000) == 0;
            }
            set
            {
                data.Visible = value;
            }
        }

        /// <summary>Occurs when the menu item is clicked or selected using a shortcut key or access key defined for the menu item.</summary>
        public event EventHandler Click
        {
            add
            {
                MenuItemData menuItemData = data;
                menuItemData.onClick = (EventHandler)Delegate.Combine(menuItemData.onClick, value);
            }
            remove
            {
                MenuItemData menuItemData = data;
                menuItemData.onClick = (EventHandler)Delegate.Remove(menuItemData.onClick, value);
            }
        }

        /// <summary>Occurs when the <see cref="P:System.Windows.Forms.MenuItem.OwnerDraw" /> property of a menu item is set to <see langword="true" /> and a request is made to draw the menu item.</summary>
        public event DrawItemEventHandler DrawItem
        {
            add
            {
                MenuItemData menuItemData = data;
                menuItemData.onDrawItem = (DrawItemEventHandler)Delegate.Combine(menuItemData.onDrawItem, value);
            }
            remove
            {
                MenuItemData menuItemData = data;
                menuItemData.onDrawItem = (DrawItemEventHandler)Delegate.Remove(menuItemData.onDrawItem, value);
            }
        }

        /// <summary>Occurs when the menu needs to know the size of a menu item before drawing it.</summary>
        public event MeasureItemEventHandler MeasureItem
        {
            add
            {
                MenuItemData menuItemData = data;
                menuItemData.onMeasureItem = (MeasureItemEventHandler)Delegate.Combine(menuItemData.onMeasureItem, value);
            }
            remove
            {
                MenuItemData menuItemData = data;
                menuItemData.onMeasureItem = (MeasureItemEventHandler)Delegate.Remove(menuItemData.onMeasureItem, value);
            }
        }

        /// <summary>Occurs before a menu item's list of menu items is displayed.</summary>
        public event EventHandler Popup
        {
            add
            {
                MenuItemData menuItemData = data;
                menuItemData.onPopup = (EventHandler)Delegate.Combine(menuItemData.onPopup, value);
            }
            remove
            {
                MenuItemData menuItemData = data;
                menuItemData.onPopup = (EventHandler)Delegate.Remove(menuItemData.onPopup, value);
            }
        }

        /// <summary>Occurs when the user places the pointer over a menu item.</summary>
        public event EventHandler Select
        {
            add
            {
                MenuItemData menuItemData = data;
                menuItemData.onSelect = (EventHandler)Delegate.Combine(menuItemData.onSelect, value);
            }
            remove
            {
                MenuItemData menuItemData = data;
                menuItemData.onSelect = (EventHandler)Delegate.Remove(menuItemData.onSelect, value);
            }
        }

        /// <summary>Initializes a <see cref="T:System.Windows.Forms.MenuItem" /> with a blank caption.</summary>
        public MenuItem()
            : this(MenuMerge.Add, 0, Shortcut.None, null, null, null, null, null)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MenuItem" /> class with a specified caption for the menu item.</summary>
        /// <param name="text">The caption for the menu item.</param>
        public MenuItem(string text)
            : this(MenuMerge.Add, 0, Shortcut.None, text, null, null, null, null)
        {
        }

        /// <summary>Initializes a new instance of the class with a specified caption and event handler for the <see cref="E:System.Windows.Forms.MenuItem.Click" /> event of the menu item.</summary>
        /// <param name="text">The caption for the menu item.</param>
        /// <param name="onClick">The <see cref="T:System.EventHandler" /> that handles the <see cref="E:System.Windows.Forms.MenuItem.Click" /> event for this menu item.</param>
        public MenuItem(string text, EventHandler onClick)
            : this(MenuMerge.Add, 0, Shortcut.None, text, onClick, null, null, null)
        {
        }

        /// <summary>Initializes a new instance of the class with a specified caption, event handler, and associated shortcut key for the menu item.</summary>
        /// <param name="text">The caption for the menu item.</param>
        /// <param name="onClick">The <see cref="T:System.EventHandler" /> that handles the <see cref="E:System.Windows.Forms.MenuItem.Click" /> event for this menu item.</param>
        /// <param name="shortcut">One of the <see cref="T:System.Windows.Forms.Shortcut" /> values.</param>
        public MenuItem(string text, EventHandler onClick, Shortcut shortcut)
            : this(MenuMerge.Add, 0, shortcut, text, onClick, null, null, null)
        {
        }

        /// <summary>Initializes a new instance of the class with a specified caption and an array of submenu items defined for the menu item.</summary>
        /// <param name="text">The caption for the menu item.</param>
        /// <param name="items">An array of <see cref="T:System.Windows.Forms.MenuItem" /> objects that contains the submenu items for this menu item.</param>
        public MenuItem(string text, MenuItem[] items)
            : this(MenuMerge.Add, 0, Shortcut.None, text, null, null, null, items)
        {
        }

        internal MenuItem(MenuItemData data)
            : base(null)
        {
            data.AddItem(this);
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MenuItem" /> class with a specified caption; defined event-handlers for the <see cref="E:System.Windows.Forms.MenuItem.Click" />, <see cref="E:System.Windows.Forms.MenuItem.Select" /> and <see cref="E:System.Windows.Forms.MenuItem.Popup" /> events; a shortcut key; a merge type; and order specified for the menu item.</summary>
        /// <param name="mergeType">One of the <see cref="T:System.Windows.Forms.MenuMerge" /> values.</param>
        /// <param name="mergeOrder">The relative position that this menu item will take in a merged menu.</param>
        /// <param name="shortcut">One of the <see cref="T:System.Windows.Forms.Shortcut" /> values.</param>
        /// <param name="text">The caption for the menu item.</param>
        /// <param name="onClick">The <see cref="T:System.EventHandler" /> that handles the <see cref="E:System.Windows.Forms.MenuItem.Click" /> event for this menu item.</param>
        /// <param name="onPopup">The <see cref="T:System.EventHandler" /> that handles the <see cref="E:System.Windows.Forms.MenuItem.Popup" /> event for this menu item.</param>
        /// <param name="onSelect">The <see cref="T:System.EventHandler" /> that handles the <see cref="E:System.Windows.Forms.MenuItem.Select" /> event for this menu item.</param>
        /// <param name="items">An array of <see cref="T:System.Windows.Forms.MenuItem" /> objects that contains the submenu items for this menu item.</param>
        public MenuItem(MenuMerge mergeType, int mergeOrder, Shortcut shortcut, string text, EventHandler onClick, EventHandler onPopup, EventHandler onSelect, MenuItem[] items)
            : base(items)
        {
            new MenuItemData(this, mergeType, mergeOrder, shortcut, showShortcut: true, text, onClick, onPopup, onSelect, null, null);
        }

        private static void CleanListItems(MenuItem senderMenu)
        {
            for (int num = senderMenu.MenuItems.Count - 1; num >= 0; num--)
            {
                MenuItem menuItem = senderMenu.MenuItems[num];
                if (menuItem.data.UserData is MdiListUserData)
                {
                    menuItem.Dispose();
                }
            }
        }

        /// <summary>Creates a copy of the current <see cref="T:System.Windows.Forms.MenuItem" />.</summary>
        /// <returns>A <see cref="T:System.Windows.Forms.MenuItem" /> that represents the duplicated menu item.</returns>
        public virtual MenuItem CloneMenu()
        {
            MenuItem menuItem = new MenuItem();
            menuItem.CloneMenu(this);
            return menuItem;
        }

        /// <summary>Creates a copy of the specified <see cref="T:System.Windows.Forms.MenuItem" />.</summary>
        /// <param name="itemSrc">The <see cref="T:System.Windows.Forms.MenuItem" /> that represents the menu item to copy.</param>
        protected void CloneMenu(MenuItem itemSrc)
        {
            CloneMenu((Menu)itemSrc);
            int state = itemSrc.data.State;
            new MenuItemData(this, itemSrc.MergeType, itemSrc.MergeOrder, itemSrc.Shortcut, itemSrc.ShowShortcut, itemSrc.Text, itemSrc.data.onClick, itemSrc.data.onPopup, itemSrc.data.onSelect, itemSrc.data.onDrawItem, itemSrc.data.onMeasureItem);
            data.SetState(state & 0x3136B, value: true);
        }

        internal virtual void CreateMenuItem()
        {
            if ((data.State & 0x10000) == 0)
            {
                NativeMethods.MENUITEMINFO_T mENUITEMINFO_T = CreateMenuItemInfo();
                UnsafeNativeMethods.InsertMenuItem(new HandleRef(menu, menu.handle), -1, fByPosition: true, mENUITEMINFO_T);
                hasHandle = mENUITEMINFO_T.hSubMenu != IntPtr.Zero;
                dataVersion = data.version;
                menuItemIsCreated = true;
                if (RenderIsRightToLeft)
                {
                    Menu.UpdateRtl(setRightToLeftBit: true);
                }
            }
        }

        private NativeMethods.MENUITEMINFO_T CreateMenuItemInfo()
        {
            NativeMethods.MENUITEMINFO_T mENUITEMINFO_T = new NativeMethods.MENUITEMINFO_T();
            mENUITEMINFO_T.fMask = 55;
            mENUITEMINFO_T.fType = data.State & 0x360;
            bool flag = false;
            if (menu == GetMainMenu())
            {
                flag = true;
            }
            if (data.caption.Equals("-"))
            {
                if (flag)
                {
                    data.caption = " ";
                    mENUITEMINFO_T.fType |= 64;
                }
                else
                {
                    mENUITEMINFO_T.fType |= 2048;
                }
            }
            mENUITEMINFO_T.fState = data.State & 0x100B;
            mENUITEMINFO_T.wID = MenuID;
            if (IsParent)
            {
                mENUITEMINFO_T.hSubMenu = base.Handle;
            }
            mENUITEMINFO_T.hbmpChecked = IntPtr.Zero;
            mENUITEMINFO_T.hbmpUnchecked = IntPtr.Zero;
            if (uniqueID == 0)
            {
                lock (allCreatedMenuItems)
                {
                    uniqueID = (uint)Interlocked.Increment(ref nextUniqueID);
                    allCreatedMenuItems.Add(uniqueID, new WeakReference(this));
                }
            }
            if (IntPtr.Size == 4)
            {
                if (data.OwnerDraw)
                {
                    mENUITEMINFO_T.dwItemData = AllocMsaaMenuInfo();
                }
                else
                {
                    mENUITEMINFO_T.dwItemData = (IntPtr)(int)uniqueID;
                }
            }
            else
            {
                mENUITEMINFO_T.dwItemData = AllocMsaaMenuInfo();
            }
            if (data.showShortcut && data.shortcut != 0 && !IsParent && !flag)
            {
                mENUITEMINFO_T.dwTypeData = data.caption + "\t" + TypeDescriptor.GetConverter(typeof(Keys)).ConvertToString((Keys)data.shortcut);
            }
            else
            {
                mENUITEMINFO_T.dwTypeData = ((data.caption.Length == 0) ? " " : data.caption);
            }
            mENUITEMINFO_T.cch = 0;
            return mENUITEMINFO_T;
        }

        /// <summary>Disposes of the resources (other than memory) used by the <see cref="T:System.Windows.Forms.MenuItem" />.</summary>
        /// <param name="disposing">
        ///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (menu != null)
                {
                    menu.MenuItems.Remove(this);
                }
                if (data != null)
                {
                    data.RemoveItem(this);
                }
                lock (allCreatedMenuItems)
                {
                    allCreatedMenuItems.Remove(uniqueID);
                }
                uniqueID = 0u;
            }
            FreeMsaaMenuInfo();
            base.Dispose(disposing);
        }

        internal static MenuItem GetMenuItemFromUniqueID(uint uniqueID)
        {
            WeakReference weakReference = (WeakReference)allCreatedMenuItems[uniqueID];
            if (weakReference != null && weakReference.IsAlive)
            {
                return (MenuItem)weakReference.Target;
            }
            return null;
        }

        internal static MenuItem GetMenuItemFromItemData(IntPtr itemData)
        {
            uint num = (uint)(long)itemData;
            if (num == 0)
            {
                return null;
            }
            if (IntPtr.Size == 4)
            {
                if (num < 3221225472u)
                {
                    num = ((MsaaMenuInfoWithId)Marshal.PtrToStructure(itemData, typeof(MsaaMenuInfoWithId))).uniqueID;
                }
            }
            else
            {
                num = ((MsaaMenuInfoWithId)Marshal.PtrToStructure(itemData, typeof(MsaaMenuInfoWithId))).uniqueID;
            }
            return GetMenuItemFromUniqueID(num);
        }

        private IntPtr AllocMsaaMenuInfo()
        {
            FreeMsaaMenuInfo();
            msaaMenuInfoPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MsaaMenuInfoWithId)));
            _ = IntPtr.Size;
            _ = 4;
            MsaaMenuInfoWithId msaaMenuInfoWithId = new MsaaMenuInfoWithId(data.caption, uniqueID);
            Marshal.StructureToPtr((object)msaaMenuInfoWithId, msaaMenuInfoPtr, fDeleteOld: false);
            return msaaMenuInfoPtr;
        }

        private void FreeMsaaMenuInfo()
        {
            if (msaaMenuInfoPtr != IntPtr.Zero)
            {
                Marshal.DestroyStructure(msaaMenuInfoPtr, typeof(MsaaMenuInfoWithId));
                Marshal.FreeHGlobal(msaaMenuInfoPtr);
                msaaMenuInfoPtr = IntPtr.Zero;
            }
        }

        internal override void ItemsChanged(int change)
        {
            base.ItemsChanged(change);
            if (change == 0)
            {
                if (menu != null && menu.created)
                {
                    UpdateMenuItem(force: true);
                    CreateMenuItems();
                }
                return;
            }
            if (!hasHandle && IsParent)
            {
                UpdateMenuItem(force: true);
            }
            MainMenu mainMenu = GetMainMenu();
            if (mainMenu != null && (data.State & 0x200) == 0)
            {
                mainMenu.ItemsChanged(change, this);
            }
        }

        internal void ItemsChanged(int change, MenuItem item)
        {
            if (change != 4 || data == null || data.baseItem == null || !data.baseItem.MenuItems.Contains(item))
            {
                return;
            }
            if (menu != null && menu.created)
            {
                UpdateMenuItem(force: true);
                CreateMenuItems();
            }
            else
            {
                if (data == null)
                {
                    return;
                }
                for (MenuItem firstItem = data.firstItem; firstItem != null; firstItem = firstItem.nextLinkedItem)
                {
                    if (firstItem.created)
                    {
                        MenuItem item2 = item.CloneMenu();
                        item.data.AddItem(item2);
                        firstItem.MenuItems.Add(item2);
                        break;
                    }
                }
            }
        }

        internal Form[] FindMdiForms()
        {
            Form[] array = null;
            MainMenu mainMenu = GetMainMenu();
            Form form = null;
            if (mainMenu != null)
            {
                form = mainMenu.GetFormUnsafe();
            }
            if (form != null)
            {
                array = form.MdiChildren;
            }
            if (array == null)
            {
                array = new Form[0];
            }
            return array;
        }

        private void PopulateMdiList()
        {
            data.SetState(512, value: true);
            try
            {
                CleanListItems(this);
                Form[] array = FindMdiForms();
                if (array == null || array.Length == 0)
                {
                    return;
                }
                Form activeMdiChild = GetMainMenu().GetFormUnsafe().ActiveMdiChild;
                if (MenuItems.Count > 0)
                {
                    MenuItem menuItem = (MenuItem)Activator.CreateInstance(GetType());
                    menuItem.data.UserData = new MdiListUserData();
                    menuItem.Text = "-";
                    MenuItems.Add(menuItem);
                }
                int num = 0;
                int num2 = 1;
                int num3 = 0;
                bool flag = false;
                for (int i = 0; i < array.Length; i++)
                {
                    if (!array[i].Visible)
                    {
                        continue;
                    }
                    num++;
                    if ((flag && num3 < 9) || (!flag && num3 < 8) || array[i].Equals(activeMdiChild))
                    {
                        MenuItem menuItem2 = (MenuItem)Activator.CreateInstance(GetType());
                        menuItem2.data.UserData = new MdiListFormData(this, i);
                        if (array[i].Equals(activeMdiChild))
                        {
                            menuItem2.Checked = true;
                            flag = true;
                        }
                        menuItem2.Text = string.Format(CultureInfo.CurrentUICulture, "&{0} {1}", new object[2]
                        {
                        num2,
                        array[i].Text
                        });
                        num2++;
                        num3++;
                        MenuItems.Add(menuItem2);
                    }
                }
                if (num > 9)
                {
                    MenuItem menuItem3 = (MenuItem)Activator.CreateInstance(GetType());
                    menuItem3.data.UserData = new MdiListMoreWindowsData(this);
                    menuItem3.Text = "MDIMenuMoreWindows";
                    MenuItems.Add(menuItem3);
                }
            }
            finally
            {
                data.SetState(512, value: false);
            }
        }

        /// <summary>Merges this <see cref="T:System.Windows.Forms.MenuItem" /> with another <see cref="T:System.Windows.Forms.MenuItem" /> and returns the resulting merged <see cref="T:System.Windows.Forms.MenuItem" />.</summary>
        /// <returns>A <see cref="T:System.Windows.Forms.MenuItem" /> that represents the merged menu item.</returns>
        public virtual MenuItem MergeMenu()
        {
            MenuItem menuItem = (MenuItem)Activator.CreateInstance(GetType());
            data.AddItem(menuItem);
            menuItem.MergeMenu(this);
            return menuItem;
        }

        /// <summary>Merges another menu item with this menu item.</summary>
        /// <param name="itemSrc">A <see cref="T:System.Windows.Forms.MenuItem" /> that specifies the menu item to merge with this one.</param>
        public void MergeMenu(MenuItem itemSrc)
        {
            base.MergeMenu(itemSrc);
            itemSrc.data.AddItem(this);
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.Click" /> event.</summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected virtual void OnClick(EventArgs e)
        {
            if (data.UserData is MdiListUserData)
            {
                ((MdiListUserData)data.UserData).OnClick(e);
            }
            else if (data.baseItem != this)
            {
                data.baseItem.OnClick(e);
            }
            else if (data.onClick != null)
            {
                data.onClick(this, e);
            }
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.DrawItem" /> event.</summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DrawItemEventArgs" /> that contains the event data.</param>
        protected virtual void OnDrawItem(DrawItemEventArgs e)
        {
            if (data.baseItem != this)
            {
                data.baseItem.OnDrawItem(e);
            }
            else if (data.onDrawItem != null)
            {
                data.onDrawItem(this, e);
            }
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.MeasureItem" /> event.</summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MeasureItemEventArgs" /> that contains the event data.</param>
        protected virtual void OnMeasureItem(MeasureItemEventArgs e)
        {
            if (data.baseItem != this)
            {
                data.baseItem.OnMeasureItem(e);
            }
            else if (data.onMeasureItem != null)
            {
                data.onMeasureItem(this, e);
            }
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.Popup" /> event.</summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected virtual void OnPopup(EventArgs e)
        {
            bool flag = false;
            for (int i = 0; i < base.ItemCount; i++)
            {
                if (items[i].MdiList)
                {
                    flag = true;
                    items[i].UpdateMenuItem(force: true);
                }
            }
            if (flag || (hasHandle && !IsParent))
            {
                UpdateMenuItem(force: true);
            }
            if (data.baseItem != this)
            {
                data.baseItem.OnPopup(e);
            }
            else if (data.onPopup != null)
            {
                data.onPopup(this, e);
            }
            for (int j = 0; j < base.ItemCount; j++)
            {
                items[j].UpdateMenuItemIfDirty();
            }
            if (MdiList)
            {
                PopulateMdiList();
            }
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.Select" /> event.</summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected virtual void OnSelect(EventArgs e)
        {
            if (data.baseItem != this)
            {
                data.baseItem.OnSelect(e);
            }
            else if (data.onSelect != null)
            {
                data.onSelect(this, e);
            }
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.Popup" /> event.</summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected virtual void OnInitMenuPopup(EventArgs e)
        {
            OnPopup(e);
        }

        internal virtual void _OnInitMenuPopup(EventArgs e)
        {
            OnInitMenuPopup(e);
        }

        /// <summary>Generates a <see cref="E:System.Windows.Forms.Control.Click" /> event for the <see cref="T:System.Windows.Forms.MenuItem" />, simulating a click by a user.</summary>
        public void PerformClick()
        {
            OnClick(EventArgs.Empty);
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.MenuItem.Select" /> event for this menu item.</summary>
        public virtual void PerformSelect()
        {
            OnSelect(EventArgs.Empty);
        }

        internal virtual bool ShortcutClick()
        {
            if (menu is MenuItem)
            {
                MenuItem menuItem = (MenuItem)menu;
                if (!menuItem.ShortcutClick() || menu != menuItem)
                {
                    return false;
                }
            }
            if (((uint)data.State & 3u) != 0)
            {
                return false;
            }
            if (base.ItemCount > 0)
            {
                OnPopup(EventArgs.Empty);
            }
            else
            {
                OnClick(EventArgs.Empty);
            }
            return true;
        }

        /// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.MenuItem" />.</summary>
        /// <returns>A string that represents the current <see cref="T:System.Windows.Forms.MenuItem" />. The string includes the type and the <see cref="P:System.Windows.Forms.MenuItem.Text" /> property of the control.</returns>
        public override string ToString()
        {
            string text = base.ToString();
            string text2 = string.Empty;
            if (data != null && data.caption != null)
            {
                text2 = data.caption;
            }
            return text + ", Text: " + text2;
        }

        internal void UpdateMenuItemIfDirty()
        {
            if (dataVersion != data.version)
            {
                UpdateMenuItem(force: true);
            }
        }

        internal void UpdateItemRtl(bool setRightToLeftBit)
        {
            if (menuItemIsCreated)
            {
                NativeMethods.MENUITEMINFO_T mENUITEMINFO_T = new NativeMethods.MENUITEMINFO_T();
                mENUITEMINFO_T.fMask = 21;
                mENUITEMINFO_T.dwTypeData = new string('\0', Text.Length + 2);
                mENUITEMINFO_T.cbSize = Marshal.SizeOf(typeof(NativeMethods.MENUITEMINFO_T));
                mENUITEMINFO_T.cch = mENUITEMINFO_T.dwTypeData.Length - 1;
                UnsafeNativeMethods.GetMenuItemInfo(new HandleRef(menu, menu.handle), MenuID, fByPosition: false, mENUITEMINFO_T);
                if (setRightToLeftBit)
                {
                    mENUITEMINFO_T.fType |= 24576;
                }
                else
                {
                    mENUITEMINFO_T.fType &= -24577;
                }
                UnsafeNativeMethods.SetMenuItemInfo(new HandleRef(menu, menu.handle), MenuID, fByPosition: false, mENUITEMINFO_T);
            }
        }

        internal void UpdateMenuItem(bool force)
        {
            if (menu == null || !menu.created || (!force && !(menu is MainMenu) && !(menu is ContextMenu)))
            {
                return;
            }
            NativeMethods.MENUITEMINFO_T mENUITEMINFO_T = CreateMenuItemInfo();
            UnsafeNativeMethods.SetMenuItemInfo(new HandleRef(menu, menu.handle), MenuID, fByPosition: false, mENUITEMINFO_T);
            if (hasHandle && mENUITEMINFO_T.hSubMenu == IntPtr.Zero)
            {
                ClearHandles();
            }
            hasHandle = mENUITEMINFO_T.hSubMenu != IntPtr.Zero;
            dataVersion = data.version;
            if (menu is MainMenu)
            {
                Form formUnsafe = ((MainMenu)menu).GetFormUnsafe();
                if (formUnsafe != null)
                {
                    SafeNativeMethods.DrawMenuBar(new HandleRef(formUnsafe, formUnsafe.Handle));
                }
            }
        }

        internal void WmDrawItem(ref Message m)
        {
            NativeMethods.DRAWITEMSTRUCT dRAWITEMSTRUCT = (NativeMethods.DRAWITEMSTRUCT)m.GetLParam(typeof(NativeMethods.DRAWITEMSTRUCT));
            IntPtr intPtr = ControlPrivate.SetUpPalette(dRAWITEMSTRUCT.hDC, force: false, realizePalette: false);
            try
            {
                Graphics graphics = Graphics.FromHdcInternal(dRAWITEMSTRUCT.hDC);
                try
                {
                    OnDrawItem(new DrawItemEventArgs(graphics, SystemInformation.MenuFont, Rectangle.FromLTRB(dRAWITEMSTRUCT.rcItem.left, dRAWITEMSTRUCT.rcItem.top, dRAWITEMSTRUCT.rcItem.right, dRAWITEMSTRUCT.rcItem.bottom), Index, (DrawItemState)dRAWITEMSTRUCT.itemState));
                }
                finally
                {
                    graphics.Dispose();
                }
            }
            finally
            {
                if (intPtr != IntPtr.Zero)
                {
                    SafeNativeMethods.SelectPalette(new HandleRef(null, dRAWITEMSTRUCT.hDC), new HandleRef(null, intPtr), 0);
                }
            }
            m.Result = (IntPtr)1;
        }

        internal void WmMeasureItem(ref Message m)
        {
            NativeMethods.MEASUREITEMSTRUCT mEASUREITEMSTRUCT = (NativeMethods.MEASUREITEMSTRUCT)m.GetLParam(typeof(NativeMethods.MEASUREITEMSTRUCT));
            IntPtr dC = UnsafeNativeMethods.GetDC(NativeMethods.NullHandleRef);
            Graphics graphics = Graphics.FromHdcInternal(dC);
            MeasureItemEventArgs measureItemEventArgs = new MeasureItemEventArgs(graphics, Index);
            try
            {
                OnMeasureItem(measureItemEventArgs);
            }
            finally
            {
                graphics.Dispose();
            }
            UnsafeNativeMethods.ReleaseDC(NativeMethods.NullHandleRef, new HandleRef(null, dC));
            mEASUREITEMSTRUCT.itemHeight = measureItemEventArgs.ItemHeight;
            mEASUREITEMSTRUCT.itemWidth = measureItemEventArgs.ItemWidth;
            Marshal.StructureToPtr((object)mEASUREITEMSTRUCT, m.LParam, fDeleteOld: false);
            m.Result = (IntPtr)1;
        }
    }
}
