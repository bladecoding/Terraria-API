using Microsoft.Xna.Framework;
using Terraria;

namespace ItemPlugin
{
    public class ItemType
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public Color Color { get; private set; }

        public ItemType(int id, string name, Color color)
        {
            ID = id;
            Name = name;
            Color = color;
        }

        public ItemEx CreateItem()
        {
            Item item = new Item();
            item.RealSetDefaults(Name);
            return new ItemEx(item);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}