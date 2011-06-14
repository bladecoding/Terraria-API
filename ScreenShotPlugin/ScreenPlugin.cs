using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria;
using TerrariaAPI;
using TerrariaAPI.Hooks;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace ScreenShotPlugin
{
    public class ScreenPlugin : TerrariaPlugin
    {
        public override string Name
        {
            get { return "ScreenShot"; }
        }

        public override Version Version
        {
            get { return new Version(1, 0); }
        }

        public override Version APIVersion
        {
            get { return new Version(1, 2); }
        }

        public override string Author
        {
            get { return "High"; }
        }

        public override string Description
        {
            get { return ""; }
        }

        public ScreenPlugin(Main game)
            : base(game)
        {
            Widths = new int[100];
            Heights = new int[100];
            for (int i = 0; i < Widths.Length; i++)
            {
                Widths[i] = Heights[i] = 0x10;
            }

            //Torch/Tree
            Widths[4] = Widths[5] = 0x14;
            Heights[3] = Heights[4] = Heights[5] = Heights[0x18] = Heights[0x21] = Heights[0x31] = Heights[0x3d] = Heights[0x47] = 0x14;
            Heights[0x49] = Heights[0x4a] = 0x20;
            Heights[0xf] = Heights[0xe] = Heights[0x10] = Heights[0x11] = Heights[0x12] = Heights[0x14] = Heights[0x15] = Heights[0x1a] = Heights[0x1b] = Heights[0x20] = Heights[0x45] = Heights[0x48] = Heights[0x4d] = 0x12;
        }

        InputManager input = new InputManager();

        public override void Initialize()
        {
            GameHooks.LoadContent += GameHooks_LoadContent;
            GameHooks.Update += GameHooks_Update;
        }

        public override void DeInitialize()
        {
            GameHooks.LoadContent -= GameHooks_LoadContent;
            GameHooks.Update -= GameHooks_Update;
        }


        int[] TileTypes;
        TileFrame[] Tiles;
        WallFrame[] Walls;
        RawImage[] Backgrounds;

        public int[] Widths;
        public int[] Heights;

        void GameHooks_LoadContent(ContentManager obj)
        {
            TileTypes = new int[Main.tileTexture.Length];
            TileTypes[27] = 1;
            TileTypes[34] = 1;
            TileTypes[35] = 1;
            TileTypes[36] = 1;
            TileTypes[50] = 1;

            Tiles = new TileFrame[Main.tileTexture.Length];
            for (int i = 0; i < Main.tileTexture.Length; i++)
            {
                if (TileTypes[i] == 0)
                {
                    Tiles[i] = new TileFrame(Main.tileTexture[i], Widths[i], Heights[i]);
                }
                else
                {
                    Tiles[i] = new OddTileFrame(Main.tileTexture[i], Widths[i], Heights[i]);
                }
            }

            Walls = new WallFrame[Main.wallTexture.Length];
            for (int i = 1; i < Main.wallTexture.Length; i++)
                Walls[i] = new WallFrame(Main.wallTexture[i], 32, 32);

            Backgrounds = new RawImage[Main.backgroundTexture.Length];
            for (int i = 1; i < Main.backgroundTexture.Length; i++)
                Backgrounds[i] = TextureHelper.TextureToRaw(Main.backgroundTexture[i]);
        }

        void GameHooks_Update(GameTime obj)
        {
            input.Update();

            if (input.IsKeyDown(Keys.F12, true))
            {
                var pos = Main.player[Main.myPlayer].position;
                Render((int)(pos.X / 16) - 125, (int)(pos.Y / 16) - 125, 250, 250);
            }
        }

        int Sky = System.Drawing.Color.FromArgb(155, 209, 255).ToArgb();
        void RenderBackgrounds(RawImage img, int startx, int starty, int width, int height)
        {
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    img[x, y] = Sky;
                }
            }


            int surface = (int)Main.worldSurface - (starty + 1);
            if (surface > 0 && surface < height)
            {
                for (int x = 0; x < img.Width; x += Backgrounds[1].Width)
                {
                    CopyImgTo(img, x, surface * 16, Backgrounds[1]);
                }
            }

            surface++;
            if (surface > 0 && surface < height)
            {
                for (int y = (surface * 16); y < (height * 16) && y < img.Height; y += Backgrounds[2].Height)
                {
                    for (int x = 0; x < img.Width; x += Backgrounds[2].Width)
                    {
                        CopyImgTo(img, x, y, Backgrounds[2]);
                    }
                }
            }

            int rock = (int)Main.rockLayer - starty;
        }

        void RenderWalls(RawImage img, int startx, int starty, int width, int height)
        {
            for (int y = 0; y < height; y++)
            {
                int tiley = starty + y;
                if (tiley < 0 || tiley > Main.maxTilesY)
                    continue;
                for (int x = 0; x < width; x++)
                {
                    int tilex = startx + x;
                    if (tilex < 0 || tilex > Main.maxTilesX)
                        continue;

                    var tile = Main.tile[tilex, tiley];
                    if (tile == null)
                        continue;

                    if (tile.wall > 0)
                    {
                        var frame = Walls[tile.wall].GetFrame(tile.wallFrameX * 2, tile.wallFrameY * 2);
                        CopyImgTo(img, x * 16, y * 16, frame);
                    }
                }
            }
        }

        void RenderTiles(RawImage img, int startx, int starty, int width, int height)
        {
            for (int y = 0; y < height; y++)
            {
                int tiley = starty + y;
                if (tiley < 0 || tiley > Main.maxTilesY)
                    continue;
                for (int x = 0; x < width; x++)
                {
                    int tilex = startx + x;
                    if (tilex < 0 || tilex > Main.maxTilesX)
                        continue;

                    var tile = Main.tile[tilex, tiley];
                    if (tile == null)
                        continue;

                    if (tile.active)
                    {
                        var frame = Tiles[tile.type].GetFrame(tile.frameX, tile.frameY);
                        CopyImgTo(img, x * 16, y * 16, frame);
                    }
                }
            }
        }

        void Render(int startx, int starty, int width, int height)
        {
            var img = new RawImage(width * 16, height * 16);

            RenderBackgrounds(img, startx, starty, width, height);
            RenderWalls(img, startx, starty, width, height);
            RenderTiles(img, startx, starty, width, height);

            var bmp = new Bitmap(img.Width, img.Height);
            var data = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    data.SetPixelInt(x, y, img[x, y]);
                }
            }
            bmp.UnlockBits(data);
            bmp.Save("test.png");
        }
        public static void CopyImgTo(RawImage dest, int destx, int desty, RawImage src)
        {
            if (src == null)
                return;
            for (int y = 0; y < src.Height; y++)
            {
                for (int x = 0; x < src.Width; x++)
                {
                    if (src[x, y] != 0)
                        dest[(x + destx), (y + desty)] = src[x, y];
                }
            }
        }
    }

    public static class TextureHelper
    {
        public static RawImage TextureToRaw(Texture2D text)
        {
            var ret = new RawImage(text.Width, text.Height);
            text.GetData(ret.Data);
            AbgrToArgb(ret);
            return ret;
        }
        public static RawImage TextureToRaw(Texture2D text, Rectangle rect)
        {
            var ret = new RawImage(rect.Width, rect.Height);
            text.GetData(0, rect, ret.Data, rect.X + (rect.Y * rect.Width), ret.Data.Length);
            AbgrToArgb(ret);
            return ret;
        }
        static void AbgrToArgb(RawImage ints)
        {
            for (int y = 0; y < ints.Height; y++)
            {
                for (int x = 0; x < ints.Width; x++)
                {
                    var c = System.Drawing.Color.FromArgb(ints[x, y]);
                    ints[x, y] = (int)new Color(c.R, c.G, c.B, c.A).PackedValue;
                }
            }
        }
    }

    public class TileFrame
    {
        public RawImage[,] Frames { get; set; }

        public int Columns { get; set; }

        public int Rows { get; set; }

        public int FrameWidth { get; set; }

        public int FrameHeight { get; set; }

        public virtual int Border { get { return 2; } }

        public TileFrame(Texture2D text, int framewidth, int frameheight)
        {
            FrameWidth = framewidth;
            FrameHeight = frameheight;
            Init(text);
        }

        void Init(Texture2D text)
        {
            GetFrames(text);
        }

        protected virtual void GetFrames(Texture2D text)
        {
            Columns = text.Width / (FrameWidth + Border);
            Rows = text.Height / (FrameHeight + Border);
            Frames = new RawImage[Columns, Rows];

            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    Frames[c, r] = TextureHelper.TextureToRaw(text, new Rectangle(c * (FrameWidth + Border), r * (FrameHeight + Border), FrameWidth, FrameHeight));
                }
            }
        }

        public RawImage GetFrame(int x, int y)
        {
            x = x / (FrameWidth + Border);
            y = y / (FrameHeight + Border);
            return Frames[x, y];
        }
    }
    public class WallFrame : TileFrame
    {
        public WallFrame(Texture2D text, int framewidth, int frameheight)
            : base(text, framewidth, frameheight)
        {
        }
        public override int Border { get { return 4; } }
    }
    public class OddTileFrame : TileFrame
    {
        public OddTileFrame(Texture2D text, int framewidth, int frameheight)
            : base(text, framewidth, frameheight)
        {
        }

        protected override void GetFrames(Texture2D text)
        {
            Columns = (int)Math.Ceiling(text.Width / (float)(FrameWidth + Border));
            Rows = (int)Math.Ceiling(text.Height / (float)(FrameHeight + Border));
            Frames = new RawImage[Columns, Rows];

            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    Frames[c, r] = TextureHelper.TextureToRaw(text, new Rectangle(c * (FrameWidth + Border), r * (FrameHeight + Border), FrameWidth, FrameHeight));
                }
            }
        }
    }

    public class RawImage
    {
        public int[] Data { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public RawImage(int[] data, int width, int height)
        {
            Data = data;
            Width = width;
            Height = height;
        }
        public RawImage(int width, int height)
            : this(new int[width * height], width, height)
        {
        }

        public int GetPixel(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height)
                return 0;
            return Data[x + (y * Width)];
        }

        public void SetPixel(int x, int y, int num)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height)
                return;
            Data[x + (y * Width)] = num; ;
        }

        public int this[int x, int y]
        {
            get { return GetPixel(x, y); }
            set { SetPixel(x, y, value); }
        }
    }
}
