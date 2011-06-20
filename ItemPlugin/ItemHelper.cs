using Terraria;

namespace ItemPlugin
{
    public static class ItemHelper
    {
        private static Player me
        {
            get { return Main.player[Main.myPlayer]; }
        }

        public static Item CreateItem(string itemName, int stack = 0)
        {
            Item item = new Item();
            item.RealSetDefaults(itemName);
            if (stack > 0) item.stack = stack;
            return item;
        }

        public static Item GiveItem(string itemName, int stack = 0)
        {
            Item item = ItemHelper.CreateItem(itemName, stack);
            return me.GetItem(Main.myPlayer, item);
        }

        public static Item ThrowItem(string itemName, int stack = 0)
        {
            Item item = CreateItem(itemName, stack);
            item.position.X = me.position.X + me.width / 2f - item.width / 2f;
            item.position.Y = me.position.Y + me.height / 2f - item.height / 2f;
            item.wet = Collision.WetCollision(item.position, item.width, item.height);
            item.velocity.Y = -2f;
            item.velocity.X = 4f * me.direction + me.velocity.X;
            item.spawnTime = 0;

            if (Main.netMode == 0)
            {
                item.owner = Main.myPlayer;
                item.noGrabDelay = 100;
            }

            int num = 200;
            Main.item[num] = new Item();

            if (Main.netMode != 1)
            {
                for (int i = 0; i < 200; i++)
                {
                    if (!Main.item[i].active)
                    {
                        num = i;
                        break;
                    }
                }
            }

            if (num == 200 && Main.netMode != 1)
            {
                int num2 = 0;

                for (int j = 0; j < 200; j++)
                {
                    if (Main.item[j].spawnTime > num2)
                    {
                        num2 = Main.item[j].spawnTime;
                        num = j;
                    }
                }
            }

            Main.item[num] = item;

            if (Main.netMode == 1)
            {
                NetMessage.SendData(21, -1, -1, "", num, 0f, 0f, 0f);
            }

            return item;
        }
    }
}