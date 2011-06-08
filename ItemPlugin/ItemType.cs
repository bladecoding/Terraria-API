using Terraria;

namespace ItemPlugin
{
    public class ItemType
    {
        public int ID { get; private set; }

        public string Name { get; private set; }

        public ItemType(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public ItemEx CreateItem()
        {
            Item item = new Item();
            item.RealSetDefaults(ID);
            return new ItemEx(item);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}