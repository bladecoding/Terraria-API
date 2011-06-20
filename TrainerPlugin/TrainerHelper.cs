using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using XNAHelpers;

namespace TrainerPlugin
{
    public static class TrainerHelper
    {
        private static Player me
        {
            get { return Main.player[Main.myPlayer]; }
        }

        private static int tilePlayerX
        {
            get { return (int)((me.position.X + me.width * 0.5f) / 16f); }
        }

        private static int tilePlayerY
        {
            get { return (int)((me.position.Y + me.height * 0.5f) / 16f); }
        }

        private static int tileTargetX
        {
            get { return (int)((Main.screenPosition.X + Main.mouseState.X) / 16f); }
        }

        private static int tileTargetY
        {
            get { return (int)((Main.screenPosition.Y + Main.mouseState.Y) / 16f); }
        }

        public static void LightCharacter()
        {
            Lighting.addLight(tilePlayerX, tilePlayerY, 1f);
        }

        public static void LightCursor()
        {
            Lighting.addLight(tileTargetX, tileTargetY, 1f);
        }

        public static void OpenBank()
        {
            me.chestX = tilePlayerX;
            me.chestY = tilePlayerY;
            me.chest = -2;
            Main.playerInventory = true;
            Main.PlaySound(10, -1, -1, 1);
        }

        public static void AddLiquidToCursor(bool isWater)
        {
            int x = tileTargetX, y = tileTargetY;

            if (Main.tile[x, y] == null)
            {
                Main.tile[x, y] = new Tile();
            }

            Main.PlaySound(19, (int)me.position.X, (int)me.position.Y, 1);
            Main.tile[x, y].lava = !isWater;
            Main.tile[x, y].liquid = 255;

            NetMessage.sendWater(x, y);
        }

        public static Texture2D CreateGrid(GraphicsDevice gd)
        {
            int[] ints = new int[16 * 16];
            int border = (Color.White * 0.1f).ToAbgr();

            // Right border
            for (int i = 15; i < ints.Length; i += 16)
                ints[i] = border;

            // Bottom border
            for (int i = 240; i < ints.Length; i++)
                ints[i] = border;

            return DrawingHelper.IntsToTexture(gd, ints, 16, 16);
        }
    }
}