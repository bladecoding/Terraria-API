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
        private ItemType[] items;

        public bool SortItemsByName { get; set; }
        public int ItemCount { get; private set; }

        public ItemForm()
        {
            InitializeComponent();

            ItemCount = Main.itemTexture.Length;
            items = GetItems();

            LoadIcons();
            LoadItems();
        }

        private void LoadIcons()
        {
            ImageList il = new ImageList();
            il.ColorDepth = ColorDepth.Depth32Bit;

            for (int i = 0; i < ItemCount; i++)
            {
                Image img = DrawingHelper.TextureToImage(Main.itemTexture[i]);
                img = DrawingHelper.ResizeImage(img, 16, 16);
                il.Images.Add(img);
            }

            lvItems.SmallImageList = il;
        }

        private void LoadItems()
        {
            ItemType[] itemsList;

            if (SortItemsByName)
            {
                itemsList = items.OrderBy(x => x.Name).ToArray();
            }
            else
            {
                itemsList = items;
            }

            lvItems.Items.Clear();

            foreach (ItemType item in itemsList)
            {
                lvItems.Items.Add(item.Name, item.ID).Tag = item;
            }
        }

        private ItemType[] GetItems()
        {
            List<ItemType> items = new List<ItemType>();

            for (int i = 1; i < ItemCount; i++)
            {
                Item item = new Item();
                item.RealSetDefaults(i);
                if (!string.IsNullOrEmpty(item.name))
                {
                    items.Add(new ItemType(i, item.name));
                }
            }

            return items.ToArray();
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

        private void btnGive_Click(object sender, EventArgs e)
        {
            ItemEx item = pgItem.SelectedObject as ItemEx;

            if (item != null && item.Active)
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