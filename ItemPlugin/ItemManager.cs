using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using XNAHelpers;
using Color = Microsoft.Xna.Framework.Color;

namespace ItemPlugin
{
    public class ItemManager
    {
        public List<ItemType> Items { get; private set; }

        private ListView lv;
        private ImageList il;
        private int itemCount;

        public ItemManager(ListView listView)
        {
            lv = listView;
            il = new ImageList();
            il.ImageSize = new Size(32, 32);
            il.ColorDepth = ColorDepth.Depth32Bit;
            lv.SmallImageList = il;

            itemCount = Main.itemTexture.Length;
            Items = new List<ItemType>();

            Prepare();
        }

        private void Prepare()
        {
            AddItem("Copper Pickaxe");
            AddItem("Copper Broadsword");
            AddItem("Copper Shortsword");
            AddItem("Copper Axe");
            AddItem("Copper Hammer");
            AddItem("Copper Bow");

            for (int type = 0; type < itemCount; type++)
            {
                AddItem(type);
            }

            AddItem("Silver Pickaxe");
            AddItem("Silver Broadsword");
            AddItem("Silver Shortsword");
            AddItem("Silver Axe");
            AddItem("Silver Hammer");
            AddItem("Silver Bow");
            AddItem("Gold Pickaxe");
            AddItem("Gold Broadsword");
            AddItem("Gold Shortsword");
            AddItem("Gold Axe");
            AddItem("Gold Hammer");
            AddItem("Gold Bow");
        }

        private void AddItem(int type)
        {
            Item item = new Item();
            item.RealSetDefaults(type, true);
            AddItem(item);
        }

        private void AddItem(string name)
        {
            Item item = new Item();
            item.RealSetDefaults(name);
            AddItem(item);
        }

        private void AddItem(Item item)
        {
            if (!string.IsNullOrEmpty(item.name))
            {
                ItemType itemType = new ItemType(item.type, item.name, item.color);
                Items.Add(itemType);
                LoadIcon(itemType);
            }
        }

        private void LoadIcon(ItemType itemType)
        {
            if (itemType != null && !string.IsNullOrEmpty(itemType.Name))
            {
                Image img;
                Texture2D texture = Main.itemTexture[itemType.ID];
                img = DrawingHelper.TextureToImage(texture);

                if (itemType.Color != default(Color))
                {
                    img = DrawingHelper.ColorizeImage(img, itemType.Color);
                }

                img = DrawingHelper.ResizeImage(img, 32, 32);
                il.Images.Add(itemType.Name, img);
            }
        }
    }
}