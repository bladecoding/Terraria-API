using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Terraria;

namespace ItemPlugin
{
    public partial class ItemForm : Form
    {
        public bool SortItemsByName { get; set; }
        public string ItemsFilter { get; set; }

        private ItemManager itemManager;

        public ItemForm()
        {
            InitializeComponent();

            SortItemsByName = true;
            ItemsFilter = string.Empty;
            itemManager = new ItemManager(lvItems);
            LoadItems();
        }

        private void LoadItems()
        {
            IEnumerable<ItemType> itemsList;

            if (SortItemsByName)
            {
                itemsList = itemManager.Items.OrderBy(x => x.Name);
            }
            else
            {
                itemsList = itemManager.Items.OrderBy(x => x.ID);
            }

            if (!string.IsNullOrWhiteSpace(ItemsFilter))
            {
                itemsList = itemsList.Where(x => x.Name.IndexOf(ItemsFilter, StringComparison.InvariantCultureIgnoreCase) >= 0);
            }

            lvItems.Items.Clear();

            foreach (ItemType item in itemsList)
            {
                lvItems.Items.Add(item.Name, item.Name).Tag = item;
            }
        }

        private void lvItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvItems.SelectedItems.Count > 0)
            {
                ItemType item = lvItems.SelectedItems[0].Tag as ItemType;
                pgItem.SelectedObject = item.CreateItem();
                btnGive.Enabled = true;
            }
        }

        private void cbSortByName_CheckedChanged(object sender, EventArgs e)
        {
            SortItemsByName = cbSortByName.Checked;
            LoadItems();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            ItemsFilter = txtFilter.Text;
            LoadItems();
        }

        private void btnGive_Click(object sender, EventArgs e)
        {
            ItemEx item = pgItem.SelectedObject as ItemEx;

            if (item != null && item.Active)
            {
                Main.player[Main.myPlayer].GetItem(Main.myPlayer, item.Item);
            }

            btnGive.Enabled = false;
        }

        private void ItemForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Visible = false;
        }
    }
}