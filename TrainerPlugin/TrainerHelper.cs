using System;
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

        public static Vector2 CursorPosition
        {
            get { return new Vector2(cursorPositionX, cursorPositionY); }
        }

        private static float cursorPositionX
        {
            get { return Main.screenPosition.X + Main.mouseState.X; }
        }

        private static float cursorPositionY
        {
            get { return Main.screenPosition.Y + Main.mouseState.Y; }
        }

        public static Vector2 TilePlayer
        {
            get { return new Vector2(tilePlayerX, tilePlayerY); }
        }

        private static int tilePlayerX
        {
            get { return (int)((me.position.X + me.width * 0.5f) / 16f); }
        }

        private static int tilePlayerY
        {
            get { return (int)((me.position.Y + me.height * 0.5f) / 16f); }
        }

        public static Vector2 TileTarget
        {
            get { return new Vector2(tileTargetX, tileTargetY); }
        }

        private static int tileTargetX
        {
            get { return (int)(cursorPositionX / 16f); }
        }

        private static int tileTargetY
        {
            get { return (int)(cursorPositionY / 16f); }
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

        private const int MaxLineLength = 100;

        public static void CreateLineTile(Vector2 pos1, Vector2 pos2, int type, bool isWall = false)
        {
            float width = Math.Abs(pos1.X - pos2.X);
            float height = Math.Abs(pos1.Y - pos2.Y);

            if (width > height)
            {
                // Horizontal
                int left =  (int)Math.Min(pos1.X, pos2.X);
                int right = left + (int)Math.Min(width, MaxLineLength);
                int y = (int)pos1.Y;

                for (int x = left; x <= right; x++)
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
            else
            {
                // Vertical
                int top =  (int)Math.Min(pos1.Y, pos2.Y);
                int bottom = top + (int)Math.Min(height, MaxLineLength);
                int x = (int)pos1.X;

                for (int y = top; y <= bottom; y++)
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

        public static Texture2D CreateGrid(GraphicsDevice gd, float transparency)
        {
            int[] ints = new int[16 * 16];
            int border = (Color.White * transparency).ToAbgr();

            // Right border
            for (int i = 15; i < ints.Length; i += 16)
                ints[i] = border;

            // Bottom border
            for (int i = 240; i < ints.Length; i++)
                ints[i] = border;

            return DrawingHelper.IntsToTexture(gd, ints, 16, 16);
        }

        public static void DrawGrid(SpriteBatch sb, Texture2D gridTexture)
        {
            int offx = (int)(Main.screenPosition.X) % 16;
            int offy = (int)(Main.screenPosition.Y) % 16;

            for (int y = -offy; y < Main.screenHeight + 16; y += 16)
            {
                for (int x = -offx; x < Main.screenWidth + 16; x += 16)
                {
                    sb.Draw(gridTexture, new Vector2(x, y), Color.White);
                }
            }
        }

        public static void DrawGridCursor(SpriteBatch sb, Texture2D border, float transparency = 0.5f)
        {
            int x = (int)(Main.mouseState.X - ((Main.screenPosition.X + Main.mouseState.X) % 16));
            int y = (int)(Main.mouseState.Y - ((Main.screenPosition.Y + Main.mouseState.Y) % 16));

            for (int y2 = -1; y2 <= 1; y2++)
            {
                for (int x2 = -1; x2 <= 1; x2++)
                {
                    int offsetX = x2 * 16;
                    int offsetY = y2 * 16;

                    float alpha = transparency;

                    if (x2 != 0 && y2 != 0)
                    {
                        alpha /= 5;
                    }

                    DrawingHelper.DrawBorder(sb, border, new Rectangle(x + offsetX, y + offsetY, 16, 16), alpha);
                }
            }
        }

        public static void KillWhoTouchingMe()
        {
            int offset = 10;
            Rectangle rect = new Rectangle((int)me.position.X - offset, (int)me.position.Y - offset, me.width + offset * 2, me.height + offset * 2);
            HitNPC(rect, 1337, 50);
        }

        public static void KillWhoTouchingCursor()
        {
            int offset = 10;
            Rectangle rect = new Rectangle((int)cursorPositionX - offset, (int)cursorPositionY - offset, offset * 2, offset * 2);
            HitNPC(rect, 1337, 50);
        }

        public static void HitNPC(Rectangle rect, int damage, float knockback)
        {
            NPC npc;

            for (int i = 0; i < 1000; i++)
            {
                npc = Main.npc[i];

                if (npc.active && !npc.friendly && npc.damage > 0 &&
                    rect.Intersects(new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height)))
                {
                    int direction = -1;

                    if (npc.position.X + (float)(npc.width / 2) < me.position.X + (float)(me.width / 2))
                    {
                        direction = 1;
                    }

                    int damageArmorIgnore = damage + (int)(npc.defense * 0.5);

                    npc.StrikeNPC(damageArmorIgnore, knockback, -direction);

                    if (Main.netMode != 0)
                    {
                        NetMessage.SendData((int)PacketTypes.NPCStrike, -1, -1, "", i, (float)damageArmorIgnore, knockback, (float)-direction, 0);
                    }
                }
            }
        }

        public static void DrawPartyText(SpriteBatch sb)
        {
            for (int i = 0; i < 255; i++)
            {
                if (Main.player[i].active && Main.myPlayer != i && !Main.player[i].dead &&
                    (Main.player[Main.myPlayer].team == 0 || Main.player[Main.myPlayer].team != Main.player[i].team))
                {
                    string text = Main.player[i].name;
                    if (Main.player[i].statLife < Main.player[i].statLifeMax)
                    {
                        text = string.Format("{0}: {1}/{2}", text, Main.player[i].statLife, Main.player[i].statLifeMax);
                    }
                    Vector2 vector = Main.fontMouseText.MeasureString(text);
                    float num3 = 0f;
                    if (Main.player[i].chatShowTime > 0)
                    {
                        num3 = -vector.Y;
                    }
                    float num4 = 0f;
                    float num5 = (float)Main.mouseTextColor / 255f;
                    Color color = new Color((int)((byte)((float)Main.teamColor[Main.player[i].team].R * num5)), (int)((byte)((float)Main.teamColor[Main.player[i].team].G * num5)), (int)((byte)((float)Main.teamColor[Main.player[i].team].B * num5)), (int)Main.mouseTextColor);
                    Vector2 vector2 = new Vector2((float)(Main.screenWidth / 2) + Main.screenPosition.X, (float)(Main.screenHeight / 2) + Main.screenPosition.Y);
                    float num6 = Main.player[i].position.X + (float)(Main.player[i].width / 2) - vector2.X;
                    float num7 = Main.player[i].position.Y - vector.Y - 2f + num3 - vector2.Y;
                    float num8 = (float)Math.Sqrt((double)(num6 * num6 + num7 * num7));
                    int num9 = Main.screenHeight;
                    if (Main.screenHeight > Main.screenWidth)
                    {
                        num9 = Main.screenWidth;
                    }
                    num9 = num9 / 2 - 30;
                    if (num9 < 100)
                    {
                        num9 = 100;
                    }
                    if (num8 < (float)num9)
                    {
                        vector.X = Main.player[i].position.X + (float)(Main.player[i].width / 2) - vector.X / 2f - Main.screenPosition.X;
                        vector.Y = Main.player[i].position.Y - vector.Y - 2f + num3 - Main.screenPosition.Y;
                    }
                    else
                    {
                        num4 = num8;
                        num8 = (float)num9 / num8;
                        vector.X = (float)(Main.screenWidth / 2) + num6 * num8 - vector.X / 2f;
                        vector.Y = (float)(Main.screenHeight / 2) + num7 * num8;
                    }

                    if (num4 > 0f)
                    {
                        string textDistance = "(" + (int)(num4 / 16f * 2f) + " ft)";

                        Vector2 vectorDistance = Main.fontMouseText.MeasureString(textDistance);
                        vectorDistance.X = vector.X + Main.fontMouseText.MeasureString(text).X / 2f - vectorDistance.X / 2f;
                        vectorDistance.Y = vector.Y + Main.fontMouseText.MeasureString(text).Y / 2f - vectorDistance.Y / 2f - 20f;

                        DrawingHelper.DrawTextWithShadow(sb, textDistance, vectorDistance, Main.fontMouseText, color, Color.Black);
                    }

                    DrawingHelper.DrawTextWithShadow(sb, text, vector, Main.fontMouseText, color, Color.Black);
                }
            }
        }
    }
}