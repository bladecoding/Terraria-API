using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria;
using TerrariaAPI;
using TerrariaAPI.Hooks;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace MinimapPlugin
{
    public class MinimapPlugin : TerrariaPlugin
    {
        public override string Name
        {
            get { return "Minimap"; }
        }

        public override Version Version
        {
            get { return new Version(1, 0); }
        }

        public override string Author
        {
            get { return "high"; }
        }

        public override string Description
        {
            get { return "Its a minimap, what do you think?"; }
        }

        public MinimapPlugin(Main main)
            : base(main)
        {
            GameHooks.OnUpdate += GameHooks_OnUpdate;
            DrawHooks.OnEndDraw += DrawHooks_OnEndDraw;
        }

        public override void Dispose()
        {
            GameHooks.OnUpdate -= GameHooks_OnUpdate;
            DrawHooks.OnEndDraw -= DrawHooks_OnEndDraw;
            base.Dispose();
        }
        WorldRenderer rend;
        void DrawHooks_OnEndDraw(SpriteBatch arg1)
        {
            if (rend != null)
            {
                int width = 200;
                int height = 200;
                var img = rend.FromTiles((int)(Main.player[Main.myPlayer].position.X / 16) - 100, (int)(Main.player[Main.myPlayer].position.Y / 16) - 100, width, height);
                for (int x = 0; x < width; x++)
                    img[x, 0] = -16777216;
                for (int x = 0; x < width; x++)
                    img[x, height - 1] = -16777216;
                for (int x = 0; x < height; x++)
                    img[0, x] = -16777216;
                for (int x = 0; x < height; x++)
                    img[width - 1, x] = -16777216;

                Texture2D text = BitmapToTexture(Game.GraphicsDevice, img, width, height);
                Game.spriteBatch.Draw(text, new Vector2(Main.screenWidth - width, Main.screenHeight - height), Color.White);
            }
        }

        static Texture2D BitmapToTexture(GraphicsDevice gd, int[,] img, int width, int height)
        {
            var ret = new Texture2D(gd, width, height);
            int[] ints = new int[width * height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int c = img[x, y];
                    int a = c >> 24;
                    int b = c >> 16 & 0xFF;
                    int g = c >> 8 & 0xFF;
                    int r = c & 0xFF;
                    ints[(y * height) + x] = (a << 24) | (r << 16) | (g << 8) | b;
                }
            }
            ret.SetData(ints);
            return ret;
        }

        bool f6down = false;
        void GameHooks_OnUpdate(GameTime obj)
        {
            if (!Game.IsActive)
                return;
            if (Main.keyState.IsKeyDown(Keys.F6))
            {
                f6down = true;
            }
            else if (Main.keyState.IsKeyUp(Keys.F6) && f6down)
            {
                f6down = false;

                if (rend == null)
                    rend = new WorldRenderer(Main.tile, Main.maxTilesX, Main.maxTilesY);
                else
                    rend = null;
            }
        }
    }
}
