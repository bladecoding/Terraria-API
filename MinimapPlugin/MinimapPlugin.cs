using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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

        public override Version APIVersion
        {
            get { return new Version(1, 1); }
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
            
        }

        void GameHooks_OnLoadContent(Microsoft.Xna.Framework.Content.ContentManager obj)
        {
            //chest = BitmapToTexture(Game.GraphicsDevice, Properties.Resources.chest);
        }

        WorldRenderer rend = null;
        InputManager input = new InputManager();
        Texture2D minimap = null;
        Texture2D chest;
        Thread renderthread;

        void DrawHooks_OnEndDraw(SpriteBatch arg1)
        {
            if (rend != null && minimap != null)
            {
                Game.spriteBatch.Draw(minimap, new Vector2(Main.screenWidth - minimap.Width, Main.screenHeight - minimap.Height), Color.White);
                for (int i = 0; i < Main.player.Length; i++)
                {
                    if (!Main.player[i].active)
                        continue;
                    int mex = (int)(Main.player[Main.myPlayer].position.X / 16);
                    int mey = (int)(Main.player[Main.myPlayer].position.Y / 16);
                    int targetx = (int)(Main.player[i].position.X / 16);
                    int targety = (int)(Main.player[i].position.Y / 16);
                    if (targetx < mex - 100)
                        continue;
                    if (targetx > mex + 100)
                        continue;
                    if (targety < mey - 100)
                        continue;
                    if (targety > mey + 100)
                        continue;

                    targetx = targetx - mex + 100;
                    targety = targety - mey + 100;

                    targetx -= Main.player[i].width / 2;
                    targety -= Main.player[i].height;
                    
                    
                    //Game.spriteBatch.Draw(chest, new Vector2(Main.screenWidth - minimap.Width + targetx, Main.screenHeight - minimap.Height + targety), Color.White);
                }
            }
        }

        void RenderMap()
        {
            while (renderthread != null)
            {
                if (rend != null)
                {
                    int width = 200;
                    int height = 200;
                    int curx = (int)(Main.player[Main.myPlayer].position.X / 16) - 100;
                    int cury = (int)(Main.player[Main.myPlayer].position.Y / 16) - 100;
                    var img = rend.FromTiles(curx, cury, width, height);
                    for (int x = 0; x < width; x++)
                        img[x, 0] = -16777216;
                    for (int x = 0; x < height; x++)
                        img[0, x] = -16777216;

                    minimap = IntsToTexture(Game.GraphicsDevice, img, width, height);
                }
                Thread.Sleep(33);
            }
        }

        static Texture2D BitmapToTexture(GraphicsDevice gd, Bitmap img)
        {
            int width = img.Width;
            int height = img.Height;
            int[,] ints = new int[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    ints[x, y] = img.GetPixel(x, y).ToArgb();
                }
            }
            return IntsToTexture(gd, ints, width, height);
        }

        static Texture2D IntsToTexture(GraphicsDevice gd, int[,] img, int width, int height)
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
                    ints[(y * width) + x] = (a << 24) | (r << 16) | (g << 8) | b;
                }
            }
            ret.SetData(ints);
            return ret;
        }
        void GameHooks_OnUpdate(GameTime obj)
        {
            if (!Game.IsActive)
                return;

            input.Update();

            if (input.IsKeyUp(Keys.F6, true))
            {
                if (rend == null)
                    rend = new WorldRenderer(Main.tile, Main.maxTilesX, Main.maxTilesY) { SurfaceY = (int)Main.worldSurface };
                else
                    rend = null;
            }
        }




        public override void Initialize()
        {
            renderthread = new Thread(RenderMap);
            renderthread.Start();
            GameHooks.OnLoadContent += GameHooks_OnLoadContent;
            GameHooks.OnUpdate += GameHooks_OnUpdate;
            DrawHooks.OnEndDraw += DrawHooks_OnEndDraw;
        }

        public override void DeInitialize()
        {
            renderthread = null;
            GameHooks.OnUpdate -= GameHooks_OnUpdate;
            DrawHooks.OnEndDraw -= DrawHooks_OnEndDraw;
        }
    }
}
