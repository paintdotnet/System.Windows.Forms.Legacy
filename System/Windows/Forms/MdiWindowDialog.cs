// System.Windows.Forms.MdiWindowDialog
using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Windows.Forms;

namespace System.Windows.Forms
{
    internal sealed class MdiWindowDialog : Form
    {
        private class ListItem
        {
            public Form form;

            public ListItem(Form f)
            {
                form = f;
            }

            public override string ToString()
            {
                return form.Text;
            }
        }

        private ListBox itemList;

        private Button okButton;

        private Button cancelButton;

        private TableLayoutPanel okCancelTableLayoutPanel;

        private Form active;

        public Form ActiveChildForm => active;

        public MdiWindowDialog()
        {
            InitializeComponent();
        }

        public void SetItems(Form active, Form[] all)
        {
            int selectedIndex = 0;
            for (int i = 0; i < all.Length; i++)
            {
                if (all[i].Visible)
                {
                    int num = itemList.Items.Add(new ListItem(all[i]));
                    if (all[i].Equals(active))
                    {
                        selectedIndex = num;
                    }
                }
            }
            this.active = active;
            itemList.SelectedIndex = selectedIndex;
        }

        private void ItemList_doubleClick(object source, EventArgs e)
        {
            okButton.PerformClick();
        }

        private void ItemList_selectedIndexChanged(object source, EventArgs e)
        {
            ListItem listItem = (ListItem)itemList.SelectedItem;
            if (listItem != null)
            {
                active = listItem.form;
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(System.Windows.Forms.MdiWindowDialog));
            itemList = new System.Windows.Forms.ListBox();
            okButton = new System.Windows.Forms.Button();
            cancelButton = new System.Windows.Forms.Button();
            okCancelTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            okCancelTableLayoutPanel.SuspendLayout();
            itemList.DoubleClick += new System.EventHandler(ItemList_doubleClick);
            itemList.SelectedIndexChanged += new System.EventHandler(ItemList_selectedIndexChanged);
            SuspendLayout();
            resources.ApplyResources(itemList, "itemList");
            itemList.FormattingEnabled = true;
            itemList.Name = "itemList";
            resources.ApplyResources(okButton, "okButton");
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            okButton.Name = "okButton";
            resources.ApplyResources(cancelButton, "cancelButton");
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            cancelButton.Name = "cancelButton";
            resources.ApplyResources(okCancelTableLayoutPanel, "okCancelTableLayoutPanel");
            okCancelTableLayoutPanel.ColumnCount = 2;
            okCancelTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
            okCancelTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
            okCancelTableLayoutPanel.Controls.Add(okButton, 0, 0);
            okCancelTableLayoutPanel.Controls.Add(cancelButton, 1, 0);
            okCancelTableLayoutPanel.Name = "okCancelTableLayoutPanel";
            okCancelTableLayoutPanel.RowCount = 1;
            okCancelTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            resources.ApplyResources(this, "$this");
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.Controls.Add(okCancelTableLayoutPanel);
            base.Controls.Add(itemList);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "MdiWindowDialog";
            base.ShowIcon = false;
            okCancelTableLayoutPanel.ResumeLayout(false);
            okCancelTableLayoutPanel.PerformLayout();
            base.AcceptButton = okButton;
            base.CancelButton = cancelButton;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
