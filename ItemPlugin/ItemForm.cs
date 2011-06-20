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

        private Player me
        {
            get { return Main.player[Main.myPlayer]; }
        }

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
                lvItems.Items.Add(" " + item.Name, item.Name).Tag = item;
            }
        }

        private void lvItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvItems.SelectedItems.Count > 0)
            {
                ItemType type = lvItems.SelectedItems[0].Tag as ItemType;
                ItemEx item = type.CreateItem();
                pgItem.SelectedObject = item;
                nudStack.Minimum = 1;
                nudStack.Maximum = Math.Max(1, item.MaxStack);
                nudStack.Value = Math.Max(1, item.MaxStack);
                btnGive.Enabled = btnGiveQuick.Enabled = btnThrowQuick.Enabled = true;
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
                me.GetItem(Main.myPlayer, item.Item);
            }

            btnGive.Enabled = false;
        }

        private void btnGiveQuick_Click(object sender, EventArgs e)
        {
            if (lvItems.SelectedItems.Count > 0)
            {
                ItemType type = lvItems.SelectedItems[0].Tag as ItemType;
                ItemEx item = type.CreateItem();
                item.Stack = (int)nudStack.Value;
                me.GetItem(Main.myPlayer, item.Item);
            }
        }

        private void btnThrow_Click(object sender, EventArgs e)
        {
            if (lvItems.SelectedItems.Count > 0)
            {
                ItemType type = lvItems.SelectedItems[0].Tag as ItemType;
                ThrowItem(type.ID);
            }
        }

        private void ThrowItem(int itemType)
        {
            int num = Item.NewItem((int)me.position.X, (int)me.position.Y, me.width, me.height, itemType, (int)nudStack.Value, false);

            if (Main.netMode == 0)
            {
                Main.item[num].noGrabDelay = 100;
            }

            Main.item[num].velocity.Y = -2f;
            Main.item[num].velocity.X = (float)(4 * me.direction) + me.velocity.X;

            if (Main.netMode == 1)
            {
                NetMessage.SendData(21, -1, -1, "", num, 0f, 0f, 0f);
            }
        }

        private void ItemForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Visible = false;
        }
    }
}