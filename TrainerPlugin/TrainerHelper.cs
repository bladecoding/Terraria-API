using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using TerrariaAPI;
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

        public static void AddTileToCursor(int type, bool isWall = false, bool isBigBrush = false)
        {
            int x = tileTargetX, y = tileTargetY;

            if (isBigBrush)
            {
                for (int y2 = y - 1; y2 < y + 2; y2++)
                {
                    for (int x2 = x - 1; x2 < x + 2; x2++)
                    {
                        if (isWall)
                        {
                            CreateWall(x2, y2, type);
                        }
                        else
                        {
                            CreateTile(x2, y2, type);
                        }
                    }
                }
            }
            else
            {
                if (isWall)
                {
                    CreateWall(x, y, type);
                }
                else
                {
                    CreateTile(x, y, type);
                }
            }
        }

        public static void DestroyTileFromCursor(bool isWall = false, bool isBigBrush = false)
        {
            int x = tileTargetX, y = tileTargetY;

            if (isBigBrush)
            {
                for (int y2 = y - 1; y2 < y + 2; y2++)
                {
                    for (int x2 = x - 1; x2 < x + 2; x2++)
                    {
                        if (isWall)
                        {
                            DestroyWall(x2, y2);
                        }
                        else
                        {
                            DestroyTile(x2, y2);
                        }
                    }
                }
            }
            else
            {
                if (isWall)
                {
                    DestroyWall(x, y);
                }
                else
                {
                    DestroyTile(x, y);
                }
            }
        }

        public static void CreateTile(int x, int y, int type)
        {
            if (!Main.tile[x, y].active)
            {
                WorldGen.PlaceTile(x, y, type, false, false, Main.myPlayer);

                if (Main.tile[x, y].type == type && Main.netMode == 1)
                {
                    NetMessage.SendData((int)PacketTypes.Tile, -1, -1, "", (int)TileCommand.PlaceTile, (float)x, (float)y, type);
                }
            }
        }

        public static void CreateWall(int x, int y, int type)
        {
            if (Main.tile[x, y].wall != type)
            {
                WorldGen.PlaceWall(x, y, type, false);

                if (Main.tile[x, y].wall == type && Main.netMode == 1)
                {
                    NetMessage.SendData((int)PacketTypes.Tile, -1, -1, "", (int)TileCommand.PlaceWall, (float)x, (float)y, type);
                }
            }
        }

        public static void DestroyTile(int x, int y)
        {
            if (Main.tile[x, y].active)
            {
                WorldGen.KillTile(x, y, false, false, true);

                if (Main.netMode == 1)
                {
                    NetMessage.SendData((int)PacketTypes.Tile, -1, -1, "", (int)TileCommand.KillTileNoItem, (float)x, (float)y, 0f);
                }
            }
        }

        public static void DestroyWall(int x, int y)
        {
            if (Main.tile[x, y].wall > 0)
            {
                WorldGen.KillWall(x, y, false);

                if (Main.netMode == 1)
                {
                    NetMessage.SendData((int)PacketTypes.Tile, -1, -1, "", (int)TileCommand.KillWall, (float)x, (float)y, 0f);
                }
            }
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

            if (Main.netMode == 1)
            {
                NetMessage.sendWater(x, y);
            }
            else
            {
                Liquid.AddWater(x, y);
            }
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