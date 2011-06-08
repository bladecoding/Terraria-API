using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Terraria;

namespace ItemPlugin
{
    public partial class ItemForm : Form
    {
        public ItemForm()
        {
            InitializeComponent();

            ItemType[] items = FillItems();
            cbItemsList.Items.AddRange(items);
        }

        private ItemType[] FillItems()
        {
            List<ItemType> items = new List<ItemType>();

            for (int i = 1; i <= 238; i++)
            {
                Item item = new Item();
                item.RealSetDefaults(i);
                if (!string.IsNullOrEmpty(item.name))
                {
                    items.Add(new ItemType(i, item.name));
                }
            }

            return items.OrderBy(x => x.Name).ToArray();
        }

        private void cbItemsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbItemsList.SelectedIndex > -1)
            {
                ItemType item = cbItemsList.Items[cbItemsList.SelectedIndex] as ItemType;
                pgItem.SelectedObject = item.CreateItem();
                btnGive.Enabled = true;
            }
        }

        private void btnGive_Click(object sender, EventArgs e)
        {
            Item item = pgItem.SelectedObject as Item;

            if (item != null)
            {
                Main.player[Main.myPlayer].GetItem(Main.myPlayer, item);
            }
        }

        private void ItemForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Visible = false;
        }
    }
}