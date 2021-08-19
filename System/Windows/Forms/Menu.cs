// System.Windows.Forms.Menu
using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    /// <summary>Represents the base functionality for all menus. Although <see cref="T:System.Windows.Forms.ToolStripDropDown" /> and <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> replace and add functionality to the <see cref="T:System.Windows.Forms.Menu" /> control of previous versions, <see cref="T:System.Windows.Forms.Menu" /> is retained for both backward compatibility and future use if you choose.</summary>
    [ToolboxItemFilter("System.Windows.Forms")]
    [ListBindable(false)]
    public abstract class Menu : Component
    {
        private delegate bool MenuItemKeyComparer(MenuItem mi, char key);

        /// <summary>Represents a collection of <see cref="T:System.Windows.Forms.MenuItem" /> objects.</summary>
        [ListBindable(false)]
        public class MenuItemCollection : IList, ICollection, IEnumerable
        {
            private Menu owner;

            private int lastAccessedIndex = -1;

            /// <summary>Retrieves the <see cref="T:System.Windows.Forms.MenuItem" /> at the specified indexed location in the collection.</summary>
            /// <param name="index">The indexed location of the <see cref="T:System.Windows.Forms.MenuItem" /> in the collection.</param>
            /// <returns>The <see cref="T:System.Windows.Forms.MenuItem" /> at the specified location.</returns>
            /// <exception cref="T:System.ArgumentException">The <paramref name="value" /> parameter is <see langword="null" />.  
            ///  or  
            ///  The <paramref name="index" /> parameter is less than zero.  
            ///  or  
            ///  The <paramref name="index" /> parameter is greater than the number of menu items in the collection, and the collection of menu items is not <see langword="null" />.</exception>
            public virtual MenuItem this[int index]
            {
                get
                {
                    if (index < 0 || index >= owner.ItemCount)
                    {
                        throw new ArgumentOutOfRangeException("index");
                    }
                    return owner.items[index];
                }
            }

            /// <summary>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
            /// <param name="index">The zero-based index of the element to get.</param>
            /// <returns>The <see cref="T:System.Windows.Forms.MenuItem" /> at the specified index.</returns>
            object IList.this[int index]
            {
                get
                {
                    return this[index];
                }
                set
                {
                    throw new NotSupportedException();
                }
            }

            /// <summary>Gets an item with the specified key from the collection.</summary>
            /// <param name="key">The name of the item to retrieve from the collection.</param>
            /// <returns>The <see cref="T:System.Windows.Forms.MenuItem" /> with the specified key.</returns>
            public virtual MenuItem this[string key]
            {
                get
                {
                    if (string.IsNullOrEmpty(key))
                    {
                        return null;
                    }
                    int index = IndexOfKey(key);
                    if (IsValidIndex(index))
                    {
                        return this[index];
                    }
                    return null;
                }
            }

            /// <summary>Gets a value indicating the total number of <see cref="T:System.Windows.Forms.MenuItem" /> objects in the collection.</summary>
            /// <returns>The number of <see cref="T:System.Windows.Forms.MenuItem" /> objects in the collection.</returns>
            public int Count => owner.ItemCount;

            /// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
            /// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.Menu.MenuItemCollection" />.</returns>
            object ICollection.SyncRoot => this;

            /// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
            /// <returns>
            ///   <see langword="false" /> in all cases.</returns>
            bool ICollection.IsSynchronized => false;

            /// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
            /// <returns>
            ///   <see langword="false" /> in all cases.</returns>
            bool IList.IsFixedSize => false;

            /// <summary>Gets a value indicating whether the collection is read-only.</summary>
            /// <returns>
            ///   <see langword="true" /> if the collection is read-only; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
            public bool IsReadOnly => false;

            /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Menu.MenuItemCollection" /> class.</summary>
            /// <param name="owner">The <see cref="T:System.Windows.Forms.Menu" /> that owns this collection.</param>
            public MenuItemCollection(Menu owner)
            {
                this.owner = owner;
            }

            /// <summary>Adds a new <see cref="T:System.Windows.Forms.MenuItem" />, to the end of the current menu, with a specified caption.</summary>
            /// <param name="caption">The caption of the menu item.</param>
            /// <returns>A <see cref="T:System.Windows.Forms.MenuItem" /> that represents the menu item being added to the collection.</returns>
            public virtual MenuItem Add(string caption)
            {
                MenuItem menuItem = new MenuItem(caption);
                Add(menuItem);
                return menuItem;
            }

            /// <summary>Adds a new <see cref="T:System.Windows.Forms.MenuItem" /> to the end of the current menu with a specified caption and a specified event handler for the <see cref="E:System.Windows.Forms.MenuItem.Click" /> event.</summary>
            /// <param name="caption">The caption of the menu item.</param>
            /// <param name="onClick">An <see cref="T:System.EventHandler" /> that represents the event handler that is called when the item is clicked by the user, or when a user presses an accelerator or shortcut key for the menu item.</param>
            /// <returns>A <see cref="T:System.Windows.Forms.MenuItem" /> that represents the menu item being added to the collection.</returns>
            public virtual MenuItem Add(string caption, EventHandler onClick)
            {
                MenuItem menuItem = new MenuItem(caption, onClick);
                Add(menuItem);
                return menuItem;
            }

            /// <summary>Adds a new <see cref="T:System.Windows.Forms.MenuItem" /> to the end of this menu with the specified caption, <see cref="E:System.Windows.Forms.MenuItem.Click" /> event handler, and items.</summary>
            /// <param name="caption">The caption of the menu item.</param>
            /// <param name="items">An array of <see cref="T:System.Windows.Forms.MenuItem" /> objects that this <see cref="T:System.Windows.Forms.MenuItem" /> will contain.</param>
            /// <returns>A <see cref="T:System.Windows.Forms.MenuItem" /> that represents the menu item being added to the collection.</returns>
            public virtual MenuItem Add(string caption, MenuItem[] items)
            {
                MenuItem menuItem = new MenuItem(caption, items);
                Add(menuItem);
                return menuItem;
            }

            /// <summary>Adds a previously created <see cref="T:System.Windows.Forms.MenuItem" /> to the end of the current menu.</summary>
            /// <param name="item">The <see cref="T:System.Windows.Forms.MenuItem" /> to add.</param>
            /// <returns>The zero-based index where the item is stored in the collection.</returns>
            public virtual int Add(MenuItem item)
            {
                return Add(owner.ItemCount, item);
            }

            /// <summary>Adds a previously created <see cref="T:System.Windows.Forms.MenuItem" /> at the specified index within the menu item collection.</summary>
            /// <param name="index">The position to add the new item.</param>
            /// <param name="item">The <see cref="T:System.Windows.Forms.MenuItem" /> to add.</param>
            /// <returns>The zero-based index where the item is stored in the collection.</returns>
            /// <exception cref="T:System.Exception">The <see cref="T:System.Windows.Forms.MenuItem" /> being added is already in use.</exception>
            /// <exception cref="T:System.ArgumentException">The index supplied in the <paramref name="index" /> parameter is larger than the size of the collection.</exception>
            public virtual int Add(int index, MenuItem item)
            {
                if (item.Menu != null)
                {
                    if (owner is MenuItem)
                    {
                        for (MenuItem menuItem = (MenuItem)owner; menuItem != null; menuItem = (MenuItem)menuItem.Parent)
                        {
                            if (menuItem.Equals(item))
                            {
                                throw new ArgumentException("MenuItemAlreadyExists");
                            }
                            if (!(menuItem.Parent is MenuItem))
                            {
                                break;
                            }
                        }
                    }
                    if (item.Menu.Equals(owner) && index > 0)
                    {
                        index--;
                    }
                    item.Menu.MenuItems.Remove(item);
                }
                if (index < 0 || index > owner.ItemCount)
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                if (owner.items == null || owner.items.Length == owner.ItemCount)
                {
                    MenuItem[] array = new MenuItem[(owner.ItemCount < 2) ? 4 : (owner.ItemCount * 2)];
                    if (owner.ItemCount > 0)
                    {
                        Array.Copy(owner.items, 0, array, 0, owner.ItemCount);
                    }
                    owner.items = array;
                }
                Array.Copy(owner.items, index, owner.items, index + 1, owner.ItemCount - index);
                owner.items[index] = item;
                owner._itemCount++;
                item.Menu = owner;
                owner.ItemsChanged(0);
                if (owner is MenuItem)
                {
                    ((MenuItem)owner).ItemsChanged(4, item);
                }
                return index;
            }

            /// <summary>Adds an array of previously created <see cref="T:System.Windows.Forms.MenuItem" /> objects to the collection.</summary>
            /// <param name="items">An array of <see cref="T:System.Windows.Forms.MenuItem" /> objects representing the menu items to add to the collection.</param>
            public virtual void AddRange(MenuItem[] items)
            {
                if (items == null)
                {
                    throw new ArgumentNullException("items");
                }
                foreach (MenuItem item in items)
                {
                    Add(item);
                }
            }

            /// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
            /// <param name="value">The <see cref="T:System.Windows.Forms.MenuItem" /> to add to the collection.</param>
            /// <returns>The position into which the <see cref="T:System.Windows.Forms.MenuItem" /> was inserted.</returns>
            int IList.Add(object value)
            {
                if (value is MenuItem)
                {
                    return Add((MenuItem)value);
                }
                throw new ArgumentException("MenuBadMenuItem");
            }

            /// <summary>Determines if the specified <see cref="T:System.Windows.Forms.MenuItem" /> is a member of the collection.</summary>
            /// <param name="value">The <see cref="T:System.Windows.Forms.MenuItem" /> to locate in the collection.</param>
            /// <returns>
            ///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.MenuItem" /> is a member of the collection; otherwise, <see langword="false" />.</returns>
            public bool Contains(MenuItem value)
            {
                return IndexOf(value) != -1;
            }

            /// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
            /// <param name="value">The object to locate in the collection.</param>
            /// <returns>
            ///   <see langword="true" /> if the specified object is a <see cref="T:System.Windows.Forms.MenuItem" /> in the collection; otherwise, <see langword="false" />.</returns>
            bool IList.Contains(object value)
            {
                if (value is MenuItem)
                {
                    return Contains((MenuItem)value);
                }
                return false;
            }

            /// <summary>Determines whether the collection contains an item with the specified key.</summary>
            /// <param name="key">The name of the item to look for.</param>
            /// <returns>
            ///   <see langword="true" /> if the collection contains an item with the specified key, otherwise, <see langword="false" />.</returns>
            public virtual bool ContainsKey(string key)
            {
                return IsValidIndex(IndexOfKey(key));
            }

            /// <summary>Finds the items with the specified key, optionally searching the submenu items</summary>
            /// <param name="key">The name of the menu item to search for.</param>
            /// <param name="searchAllChildren">
            ///   <see langword="true" /> to search child menu items; otherwise, <see langword="false" />.</param>
            /// <returns>An array of <see cref="T:System.Windows.Forms.MenuItem" /> objects whose <see cref="P:System.Windows.Forms.Menu.Name" /> property matches the specified <paramref name="key" />.</returns>
            /// <exception cref="T:System.ArgumentNullException">
            ///   <paramref name="key" /> is <see langword="null" /> or an empty string.</exception>
            public MenuItem[] Find(string key, bool searchAllChildren)
            {
                if (key == null || key.Length == 0)
                {
                    throw new ArgumentNullException("key");
                }
                ArrayList arrayList = FindInternal(key, searchAllChildren, this, new ArrayList());
                MenuItem[] array = new MenuItem[arrayList.Count];
                arrayList.CopyTo(array, 0);
                return array;
            }

            private ArrayList FindInternal(string key, bool searchAllChildren, MenuItemCollection menuItemsToLookIn, ArrayList foundMenuItems)
            {
                if (menuItemsToLookIn == null || foundMenuItems == null)
                {
                    return null;
                }
                for (int i = 0; i < menuItemsToLookIn.Count; i++)
                {
                    if (menuItemsToLookIn[i] != null && WindowsFormsUtils.SafeCompareStrings(menuItemsToLookIn[i].Name, key, ignoreCase: true))
                    {
                        foundMenuItems.Add(menuItemsToLookIn[i]);
                    }
                }
                if (searchAllChildren)
                {
                    for (int j = 0; j < menuItemsToLookIn.Count; j++)
                    {
                        if (menuItemsToLookIn[j] != null && menuItemsToLookIn[j].MenuItems != null && menuItemsToLookIn[j].MenuItems.Count > 0)
                        {
                            foundMenuItems = FindInternal(key, searchAllChildren, menuItemsToLookIn[j].MenuItems, foundMenuItems);
                        }
                    }
                }
                return foundMenuItems;
            }

            /// <summary>Retrieves the index of a specific item in the collection.</summary>
            /// <param name="value">The <see cref="T:System.Windows.Forms.MenuItem" /> to locate in the collection.</param>
            /// <returns>The zero-based index of the item found in the collection; otherwise, -1.</returns>
            public int IndexOf(MenuItem value)
            {
                for (int i = 0; i < Count; i++)
                {
                    if (this[i] == value)
                    {
                        return i;
                    }
                }
                return -1;
            }

            /// <summary>For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
            /// <param name="value">The <see cref="T:System.Windows.Forms.MenuItem" /> to locate in the collection.</param>
            /// <returns>The zero-based index if <paramref name="value" /> is a <see cref="T:System.Windows.Forms.MenuItem" /> in the collection; otherwise -1.</returns>
            int IList.IndexOf(object value)
            {
                if (value is MenuItem)
                {
                    return IndexOf((MenuItem)value);
                }
                return -1;
            }

            /// <summary>Finds the index of the first occurrence of a menu item with the specified key.</summary>
            /// <param name="key">The name of the menu item to search for.</param>
            /// <returns>The zero-based index of the first menu item with the specified key.</returns>
            public virtual int IndexOfKey(string key)
            {
                if (string.IsNullOrEmpty(key))
                {
                    return -1;
                }
                if (IsValidIndex(lastAccessedIndex) && WindowsFormsUtils.SafeCompareStrings(this[lastAccessedIndex].Name, key, ignoreCase: true))
                {
                    return lastAccessedIndex;
                }
                for (int i = 0; i < Count; i++)
                {
                    if (WindowsFormsUtils.SafeCompareStrings(this[i].Name, key, ignoreCase: true))
                    {
                        lastAccessedIndex = i;
                        return i;
                    }
                }
                lastAccessedIndex = -1;
                return -1;
            }

            /// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
            /// <param name="index">The zero-based index at which the <see cref="T:System.Windows.Forms.MenuItem" /> should be inserted.</param>
            /// <param name="value">The <see cref="T:System.Windows.Forms.MenuItem" /> to insert into the <see cref="T:System.Windows.Forms.Menu.MenuItemCollection" />.</param>
            void IList.Insert(int index, object value)
            {
                if (value is MenuItem)
                {
                    Add(index, (MenuItem)value);
                    return;
                }
                throw new ArgumentException("value");
            }

            private bool IsValidIndex(int index)
            {
                if (index >= 0)
                {
                    return index < Count;
                }
                return false;
            }

            /// <summary>Removes all <see cref="T:System.Windows.Forms.MenuItem" /> objects from the menu item collection.</summary>
            public virtual void Clear()
            {
                if (owner.ItemCount > 0)
                {
                    for (int i = 0; i < owner.ItemCount; i++)
                    {
                        owner.items[i].Menu = null;
                    }
                    owner._itemCount = 0;
                    owner.items = null;
                    owner.ItemsChanged(0);
                    if (owner is MenuItem)
                    {
                        ((MenuItem)owner).UpdateMenuItem(force: true);
                    }
                }
            }

            /// <summary>Copies the entire collection into an existing array at a specified location within the array.</summary>
            /// <param name="dest">The destination array.</param>
            /// <param name="index">The index in the destination array at which storing begins.</param>
            public void CopyTo(Array dest, int index)
            {
                if (owner.ItemCount > 0)
                {
                    Array.Copy(owner.items, 0, dest, index, owner.ItemCount);
                }
            }

            /// <summary>Returns an enumerator that can be used to iterate through the menu item collection.</summary>
            /// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the menu item collection.</returns>
            public IEnumerator GetEnumerator()
            {
                return new WindowsFormsUtils.ArraySubsetEnumerator(owner.items, owner.ItemCount);
            }

            /// <summary>Removes a <see cref="T:System.Windows.Forms.MenuItem" /> from the menu item collection at a specified index.</summary>
            /// <param name="index">The index of the <see cref="T:System.Windows.Forms.MenuItem" /> to remove.</param>
            public virtual void RemoveAt(int index)
            {
                if (index < 0 || index >= owner.ItemCount)
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                MenuItem menuItem = owner.items[index];
                menuItem.Menu = null;
                owner._itemCount--;
                Array.Copy(owner.items, index + 1, owner.items, index, owner.ItemCount - index);
                owner.items[owner.ItemCount] = null;
                owner.ItemsChanged(0);
                if (owner.ItemCount == 0)
                {
                    Clear();
                }
            }

            /// <summary>Removes the menu item with the specified key from the collection.</summary>
            /// <param name="key">The name of the menu item to remove.</param>
            public virtual void RemoveByKey(string key)
            {
                int index = IndexOfKey(key);
                if (IsValidIndex(index))
                {
                    RemoveAt(index);
                }
            }

            /// <summary>Removes the specified <see cref="T:System.Windows.Forms.MenuItem" /> from the menu item collection.</summary>
            /// <param name="item">The <see cref="T:System.Windows.Forms.MenuItem" /> to remove.</param>
            public virtual void Remove(MenuItem item)
            {
                if (item.Menu == owner)
                {
                    RemoveAt(item.Index);
                }
            }

            /// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
            /// <param name="value">The <see cref="T:System.Windows.Forms.MenuItem" /> to remove.</param>
            void IList.Remove(object value)
            {
                if (value is MenuItem)
                {
                    Remove((MenuItem)value);
                }
            }
        }

        internal const int CHANGE_ITEMS = 0;

        internal const int CHANGE_VISIBLE = 1;

        internal const int CHANGE_MDI = 2;

        internal const int CHANGE_MERGE = 3;

        internal const int CHANGE_ITEMADDED = 4;

        /// <summary>Specifies that the <see cref="M:System.Windows.Forms.Menu.FindMenuItem(System.Int32,System.IntPtr)" /> method should search for a handle.</summary>
        public const int FindHandle = 0;

        /// <summary>Specifies that the <see cref="M:System.Windows.Forms.Menu.FindMenuItem(System.Int32,System.IntPtr)" /> method should search for a shortcut.</summary>
        public const int FindShortcut = 1;

        private MenuItemCollection itemsCollection;

        internal MenuItem[] items;

        private int _itemCount;

        internal IntPtr handle;

        internal bool created;

        private object userData;

        private string name;

        /// <summary>Gets a value representing the window handle for the menu.</summary>
        /// <returns>The HMENU value of the menu.</returns>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IntPtr Handle
        {
            get
            {
                if (handle == IntPtr.Zero)
                {
                    handle = CreateMenuHandle();
                }
                CreateMenuItems();
                return handle;
            }
        }

        /// <summary>Gets a value indicating whether this menu contains any menu items. This property is read-only.</summary>
        /// <returns>
        ///   <see langword="true" /> if this menu contains <see cref="T:System.Windows.Forms.MenuItem" /> objects; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsParent
        {
            get
            {
                if (items != null)
                {
                    return ItemCount > 0;
                }
                return false;
            }
        }

        internal int ItemCount => _itemCount;

        /// <summary>Gets a value indicating the <see cref="T:System.Windows.Forms.MenuItem" /> that is used to display a list of multiple document interface (MDI) child forms.</summary>
        /// <returns>A <see cref="T:System.Windows.Forms.MenuItem" /> that represents the menu item displaying a list of MDI child forms that are open in the application.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MenuItem MdiListItem
        {
            get
            {
                for (int i = 0; i < ItemCount; i++)
                {
                    MenuItem menuItem = items[i];
                    if (menuItem.MdiList)
                    {
                        return menuItem;
                    }
                    if (menuItem.IsParent)
                    {
                        menuItem = menuItem.MdiListItem;
                        if (menuItem != null)
                        {
                            return menuItem;
                        }
                    }
                }
                return null;
            }
        }

        /// <summary>Gets or sets the name of the <see cref="T:System.Windows.Forms.Menu" />.</summary>
        /// <returns>A string representing the name.</returns>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public string Name
        {
            get
            {
                return WindowsFormsUtils.GetComponentName(this, name);
            }
            set
            {
                if (value == null || value.Length == 0)
                {
                    name = null;
                }
                else
                {
                    name = value;
                }
                if (Site != null)
                {
                    Site.Name = name;
                }
            }
        }

        /// <summary>Gets a value indicating the collection of <see cref="T:System.Windows.Forms.MenuItem" /> objects associated with the menu.</summary>
        /// <returns>A <see cref="T:System.Windows.Forms.Menu.MenuItemCollection" /> that represents the list of <see cref="T:System.Windows.Forms.MenuItem" /> objects stored in the menu.</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [MergableProperty(false)]
        public MenuItemCollection MenuItems
        {
            get
            {
                if (itemsCollection == null)
                {
                    itemsCollection = new MenuItemCollection(this);
                }
                return itemsCollection;
            }
        }

        internal virtual bool RenderIsRightToLeft => false;

        /// <summary>Gets or sets user-defined data associated with the control.</summary>
        /// <returns>An object representing the data.</returns>
        [Localizable(false)]
        [Bindable(true)]
        [DefaultValue(null)]
        [TypeConverter(typeof(StringConverter))]
        public object Tag
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

        internal int SelectedMenuItemIndex
        {
            get
            {
                for (int i = 0; i < items.Length; i++)
                {
                    MenuItem menuItem = items[i];
                    if (menuItem != null && menuItem.Selected)
                    {
                        return i;
                    }
                }
                return -1;
            }
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Menu" /> class.</summary>
        /// <param name="items">An array of type <see cref="T:System.Windows.Forms.MenuItem" /> containing the objects to add to the menu.</param>
        protected Menu(MenuItem[] items)
        {
            if (items != null)
            {
                MenuItems.AddRange(items);
            }
        }

        internal void ClearHandles()
        {
            if (handle != IntPtr.Zero)
            {
                UnsafeNativeMethods.DestroyMenu(new HandleRef(this, handle));
            }
            handle = IntPtr.Zero;
            if (created)
            {
                for (int i = 0; i < ItemCount; i++)
                {
                    items[i].ClearHandles();
                }
                created = false;
            }
        }

        /// <summary>Copies the <see cref="T:System.Windows.Forms.Menu" /> that is passed as a parameter to the current <see cref="T:System.Windows.Forms.Menu" />.</summary>
        /// <param name="menuSrc">The <see cref="T:System.Windows.Forms.Menu" /> to copy.</param>
        protected internal void CloneMenu(Menu menuSrc)
        {
            MenuItem[] array = null;
            if (menuSrc.items != null)
            {
                int count = menuSrc.MenuItems.Count;
                array = new MenuItem[count];
                for (int i = 0; i < count; i++)
                {
                    array[i] = menuSrc.MenuItems[i].CloneMenu();
                }
            }
            MenuItems.Clear();
            if (array != null)
            {
                MenuItems.AddRange(array);
            }
        }

        /// <summary>Creates a new handle to the <see cref="T:System.Windows.Forms.Menu" />.</summary>
        /// <returns>A handle to the menu if the method succeeds; otherwise, <see langword="null" />.</returns>
        protected virtual IntPtr CreateMenuHandle()
        {
            return UnsafeNativeMethods.CreatePopupMenu();
        }

        internal void CreateMenuItems()
        {
            if (!created)
            {
                for (int i = 0; i < ItemCount; i++)
                {
                    items[i].CreateMenuItem();
                }
                created = true;
            }
        }

        internal void DestroyMenuItems()
        {
            if (created)
            {
                for (int i = 0; i < ItemCount; i++)
                {
                    items[i].ClearHandles();
                }
                while (UnsafeNativeMethods.GetMenuItemCount(new HandleRef(this, handle)) > 0)
                {
                    UnsafeNativeMethods.RemoveMenu(new HandleRef(this, handle), 0, 1024);
                }
                created = false;
            }
        }

        /// <summary>Disposes of the resources, other than memory, used by the <see cref="T:System.Windows.Forms.Menu" />.</summary>
        /// <param name="disposing">
        ///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                while (ItemCount > 0)
                {
                    MenuItem menuItem = items[--_itemCount];
                    if (menuItem.Site != null && menuItem.Site.Container != null)
                    {
                        menuItem.Site.Container.Remove(menuItem);
                    }
                    menuItem.Menu = null;
                    menuItem.Dispose();
                }
                items = null;
            }
            if (handle != IntPtr.Zero)
            {
                UnsafeNativeMethods.DestroyMenu(new HandleRef(this, handle));
                handle = IntPtr.Zero;
                if (disposing)
                {
                    ClearHandles();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>Gets the <see cref="T:System.Windows.Forms.MenuItem" /> that contains the value specified.</summary>
        /// <param name="type">The type of item to use to find the <see cref="T:System.Windows.Forms.MenuItem" />.</param>
        /// <param name="value">The item to use to find the <see cref="T:System.Windows.Forms.MenuItem" />.</param>
        /// <returns>The <see cref="T:System.Windows.Forms.MenuItem" /> that matches value; otherwise, <see langword="null" />.</returns>
        public MenuItem FindMenuItem(int type, IntPtr value)
        {
            return FindMenuItemInternal(type, value);
        }

        private MenuItem FindMenuItemInternal(int type, IntPtr value)
        {
            for (int i = 0; i < ItemCount; i++)
            {
                MenuItem menuItem = items[i];
                switch (type)
                {
                    case 0:
                        if (menuItem.handle == value)
                        {
                            return menuItem;
                        }
                        break;
                    case 1:
                        if (menuItem.Shortcut == (Shortcut)(int)value)
                        {
                            return menuItem;
                        }
                        break;
                }
                menuItem = menuItem.FindMenuItemInternal(type, value);
                if (menuItem != null)
                {
                    return menuItem;
                }
            }
            return null;
        }

        /// <summary>Returns the position at which a menu item should be inserted into the menu.</summary>
        /// <param name="mergeOrder">The merge order position for the menu item to be merged.</param>
        /// <returns>The position at which a menu item should be inserted into the menu.</returns>
        protected int FindMergePosition(int mergeOrder)
        {
            int num = 0;
            int num2 = ItemCount;
            while (num < num2)
            {
                int num3 = (num + num2) / 2;
                if (items[num3].MergeOrder <= mergeOrder)
                {
                    num = num3 + 1;
                }
                else
                {
                    num2 = num3;
                }
            }
            return num;
        }

        internal int xFindMergePosition(int mergeOrder)
        {
            int result = 0;
            for (int i = 0; i < ItemCount && items[i].MergeOrder <= mergeOrder; i++)
            {
                if (items[i].MergeOrder < mergeOrder)
                {
                    result = i + 1;
                }
                else if (mergeOrder == items[i].MergeOrder)
                {
                    result = i;
                    break;
                }
            }
            return result;
        }

        internal void UpdateRtl(bool setRightToLeftBit)
        {
            foreach (MenuItem menuItem in MenuItems)
            {
                menuItem.UpdateItemRtl(setRightToLeftBit);
                menuItem.UpdateRtl(setRightToLeftBit);
            }
        }

        /// <summary>Gets the <see cref="T:System.Windows.Forms.ContextMenu" /> that contains this menu.</summary>
        /// <returns>The <see cref="T:System.Windows.Forms.ContextMenu" /> that contains this menu. The default is <see langword="null" />.</returns>
        public ContextMenu GetContextMenu()
        {
            Menu menu = this;
            while (!(menu is ContextMenu))
            {
                if (!(menu is MenuItem))
                {
                    return null;
                }
                menu = ((MenuItem)menu).Menu;
            }
            return (ContextMenu)menu;
        }

        /// <summary>Gets the <see cref="T:System.Windows.Forms.MainMenu" /> that contains this menu.</summary>
        /// <returns>The <see cref="T:System.Windows.Forms.MainMenu" /> that contains this menu.</returns>
        public MainMenu GetMainMenu()
        {
            Menu menu = this;
            while (!(menu is MainMenu))
            {
                if (!(menu is MenuItem))
                {
                    return null;
                }
                menu = ((MenuItem)menu).Menu;
            }
            return (MainMenu)menu;
        }

        internal virtual void ItemsChanged(int change)
        {
            if ((uint)change <= 1u)
            {
                DestroyMenuItems();
            }
        }

        private IntPtr MatchKeyToMenuItem(int startItem, char key, MenuItemKeyComparer comparer)
        {
            int num = -1;
            bool flag = false;
            for (int i = 0; i < items.Length; i++)
            {
                if (flag)
                {
                    break;
                }
                int num2 = (startItem + i) % items.Length;
                MenuItem menuItem = items[num2];
                if (menuItem != null && comparer(menuItem, key))
                {
                    if (num < 0)
                    {
                        num = menuItem.MenuIndex;
                    }
                    else
                    {
                        flag = true;
                    }
                }
            }
            if (num < 0)
            {
                return IntPtr.Zero;
            }
            int high = (flag ? 3 : 2);
            return (IntPtr)NativeMethods.Util.MAKELONG(num, high);
        }

        /// <summary>Merges the <see cref="T:System.Windows.Forms.MenuItem" /> objects of one menu with the current menu.</summary>
        /// <param name="menuSrc">The <see cref="T:System.Windows.Forms.Menu" /> whose menu items are merged with the menu items of the current menu.</param>
        /// <exception cref="T:System.ArgumentException">It was attempted to merge the menu with itself.</exception>
        public virtual void MergeMenu(Menu menuSrc)
        {
            if (menuSrc == this)
            {
                throw new ArgumentException("MenuMergeWithSelf");
            }
            if (menuSrc.items != null && items == null)
            {
                MenuItems.Clear();
            }
            for (int i = 0; i < menuSrc.ItemCount; i++)
            {
                MenuItem menuItem = menuSrc.items[i];
                switch (menuItem.MergeType)
                {
                    case MenuMerge.Add:
                        MenuItems.Add(FindMergePosition(menuItem.MergeOrder), menuItem.MergeMenu());
                        break;
                    case MenuMerge.Replace:
                    case MenuMerge.MergeItems:
                        {
                            int mergeOrder = menuItem.MergeOrder;
                            int num = xFindMergePosition(mergeOrder);
                            while (true)
                            {
                                if (num >= ItemCount)
                                {
                                    MenuItems.Add(num, menuItem.MergeMenu());
                                    break;
                                }
                                MenuItem menuItem2 = items[num];
                                if (menuItem2.MergeOrder != mergeOrder)
                                {
                                    MenuItems.Add(num, menuItem.MergeMenu());
                                    break;
                                }
                                if (menuItem2.MergeType != 0)
                                {
                                    if (menuItem.MergeType != MenuMerge.MergeItems || menuItem2.MergeType != MenuMerge.MergeItems)
                                    {
                                        menuItem2.Dispose();
                                        MenuItems.Add(num, menuItem.MergeMenu());
                                    }
                                    else
                                    {
                                        menuItem2.MergeMenu(menuItem);
                                    }
                                    break;
                                }
                                num++;
                            }
                            break;
                        }
                }
            }
        }

        internal virtual bool ProcessInitMenuPopup(IntPtr handle)
        {
            MenuItem menuItem = FindMenuItemInternal(0, handle);
            if (menuItem != null)
            {
                menuItem._OnInitMenuPopup(EventArgs.Empty);
                menuItem.CreateMenuItems();
                return true;
            }
            return false;
        }

        /// <summary>Processes a command key.</summary>
        /// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference that represents the window message to process.</param>
        /// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
        /// <returns>
        ///   <see langword="true" /> if the character was processed by the control; otherwise, <see langword="false" />.</returns>
        protected internal virtual bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            return FindMenuItemInternal(1, (IntPtr)(int)keyData)?.ShortcutClick() ?? false;
        }

        /// <summary>Returns a <see cref="T:System.String" /> that represents the <see cref="T:System.Windows.Forms.Menu" /> control.</summary>
        /// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Windows.Forms.Menu" />.</returns>
        public override string ToString()
        {
            string text = base.ToString();
            return text + ", Items.Count: " + ItemCount.ToString(CultureInfo.CurrentCulture);
        }

        internal void WmMenuChar(ref Message m)
        {
            Menu menu = ((m.LParam == handle) ? this : FindMenuItemInternal(0, m.LParam));
            if (menu != null)
            {
                char key = char.ToUpper((char)NativeMethods.Util.LOWORD(m.WParam), CultureInfo.CurrentCulture);
                m.Result = menu.WmMenuCharInternal(key);
            }
        }

        internal IntPtr WmMenuCharInternal(char key)
        {
            int startItem = (SelectedMenuItemIndex + 1) % items.Length;
            IntPtr intPtr = MatchKeyToMenuItem(startItem, key, CheckOwnerDrawItemWithMnemonic);
            if (intPtr == IntPtr.Zero)
            {
                intPtr = MatchKeyToMenuItem(startItem, key, CheckOwnerDrawItemNoMnemonic);
            }
            return intPtr;
        }

        private bool CheckOwnerDrawItemWithMnemonic(MenuItem mi, char key)
        {
            if (mi.OwnerDraw)
            {
                return mi.Mnemonic == key;
            }
            return false;
        }

        private bool CheckOwnerDrawItemNoMnemonic(MenuItem mi, char key)
        {
            if (mi.OwnerDraw && mi.Mnemonic == '\0' && mi.Text.Length > 0)
            {
                return char.ToUpper(mi.Text[0], CultureInfo.CurrentCulture) == key;
            }
            return false;
        }
    }
}
