using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Terraria;
using TerrariaAPI;

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
                Image img = DrawingHelper.TextureToImage(Main.itemTexture[item.ID]);
                btnGive.Image = DrawingHelper.ResizeImage(img, 20, 20);
                btnGive.Enabled = true;
            }
        }

        private void btnGive_Click(object sender, EventArgs e)
        {
            ItemEx item = pgItem.SelectedObject as ItemEx;

            if (item != null)
            {
                Main.player[Main.myPlayer].GetItem(Main.myPlayer, item.Item);
            }
        }

        private void ItemForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Visible = false;
        }
    }
}