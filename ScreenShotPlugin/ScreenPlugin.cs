using System;
using System.Drawing;
using System.Drawing.Imaging;
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
    [APIVersion(1, 5)]
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
        TileFrame[] TreeTops;
        TileFrame[] TreeBranches;
        RawImage[] Clouds;

        public int[] Widths;
        public int[] Heights;

        private void GameHooks_LoadContent(ContentManager obj)
        {
            TileTypes = new int[Main.tileTexture.Length];
            TileTypes[27] = -1;
            TileTypes[34] = 1;
            TileTypes[35] = 1;
            TileTypes[36] = 1;
            TileTypes[50] = 1;
            TileTypes[78] = 2;

            Tiles = new TileFrame[Main.tileTexture.Length];
            for (int i = 0; i < Main.tileTexture.Length; i++)
            {
                if (TileTypes[i] == -1)
                    continue;

                if (TileTypes[i] == 0)
                {
                    Tiles[i] = new TileFrame(Main.tileTexture[i], Widths[i], Heights[i]);
                }
                else if (TileTypes[i] == 1)
                {
                    Tiles[i] = new OddTileFrame(Main.tileTexture[i], Widths[i], Heights[i]);
                }
                else if (TileTypes[i] == 2)
                {
                    Tiles[i] = new TileFrame(Main.tileTexture[i], Widths[i], Heights[i]);
                    Tiles[i].Border = 0;
                }
                Tiles[i].CreateFrames();
            }

            Walls = new WallFrame[Main.wallTexture.Length];
            for (int i = 1; i < Main.wallTexture.Length; i++)
            {
                Walls[i] = new WallFrame(Main.wallTexture[i], 32, 32);
                Walls[i].CreateFrames();
            }

            Backgrounds = new RawImage[Main.backgroundTexture.Length];
            for (int i = 1; i < Main.backgroundTexture.Length; i++)
                Backgrounds[i] = TextureHelper.TextureToRaw(Main.backgroundTexture[i]);

            TreeTops = new TileFrame[Main.treeTopTexture.Length];
            for (int i = 0; i < Main.treeTopTexture.Length; i++)
            {
                TreeTops[i] = new TileFrame(Main.treeTopTexture[i], Main.treeTopTexture[i].Width/3 - 2,
                                            Main.treeTopTexture[i].Height - 2);
                TreeTops[i].CreateFrames();
            }

            TreeBranches = new TileFrame[Main.treeBranchTexture.Length];
            for (int i = 0; i < Main.treeBranchTexture.Length; i++)
            {
                TreeBranches[i] = new TileFrame(Main.treeBranchTexture[i], 40);
                TreeBranches[i].CreateFrames();
            }

            Clouds = new RawImage[Main.cloudTexture.Length];
            for (int i = 0; i < Main.cloudTexture.Length; i++)
                Clouds[i] = TextureHelper.TextureToRaw(Main.cloudTexture[i]);
        }

        private void GameHooks_Update(GameTime obj)
        {
            input.Update();

            if (input.IsKeyDown(Keys.F12, true))
            {
                var pos = Main.player[Main.myPlayer].position;
                int width = 1000;
                int height = 200;
                Render((int)(pos.X / 16) - (width / 2), (int)(pos.Y / 16) - (height / 2), width, height);
            }
        }

        int Sky = System.Drawing.Color.FromArgb(155, 209, 255).ToArgb();

        private void RenderSky(RawImage img, int startx, int starty, int width, int height)
        {
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    img[x, y] = Sky;
                }
            }
        }

        private void RenderClouds(RawImage img, int startx, int starty, int width, int height)
        {
            var rand = new Random();
            int surface = ((int)Main.worldSurface - starty);
            if (surface > 0)
            {
                int space = surface * width;
                int count = space / 1000;
                for (int i = 0; i < count; i++)
                {
                    int x = rand.Next(0, width * 16);
                    int y = rand.Next(0, surface * 16);
                    int type = rand.Next(0, 4);
                    CopyImgTo(img, x, y, Clouds[type]);
                }
            }
        }

        private void RenderBackgrounds(RawImage img, int startx, int starty, int width, int height)
        {
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

        private void RenderWalls(RawImage img, int startx, int starty, int width, int height)
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

        private void RenderTiles(RawImage img, int startx, int starty, int width, int height)
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
                        if (TileTypes[tile.type] == -1)
                            continue;
                        var frame = Tiles[tile.type].GetFrame(tile.frameX, tile.frameY);
                        CopyImgTo(img, x * 16, y * 16, frame);
                    }
                }
            }
        }

        private void RenderTreeTops(RawImage img, int startx, int starty, int width, int height)
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

                    if (!tile.active)
                        continue;

                    if (((tile.type == 0x5) && (tile.frameY >= 0xc6)) && (tile.frameX >= 0x16))
                    {
                        int num14;
                        int num18;
                        int num13 = 0x0;
                        if (tile.frameX == 0x16)
                        {
                            if (tile.frameY == 0xdc)
                            {
                                num13 = 0x1;
                            }
                            else if (tile.frameY == 0xf2)
                            {
                                num13 = 0x2;
                            }
                            num14 = 0x0;
                            int num15 = 0x50;
                            int num16 = 0x50;
                            int num17 = 0x20;
                            num18 = tiley;
                            while (num18 < (tiley + 0x64))
                            {
                                if (Main.tile[tilex, num18].type == 0x2)
                                {
                                    num14 = 0x0;
                                    break;
                                }
                                if (Main.tile[tilex, num18].type == 0x17)
                                {
                                    num14 = 0x1;
                                    break;
                                }
                                if (Main.tile[tilex, num18].type == 0x3c)
                                {
                                    num14 = 0x2;
                                    num15 = 0x72;
                                    num16 = 0x60;
                                    num17 = 0x30;
                                    break;
                                }
                                num18++;
                            }
                            CopyImgTo(img, (x * 16) - num17, ((y + 1) * 16) - num16, TreeTops[num14].GetFrame(num13 * (num15 + 0x2), 0));
                        }
                        else if (tile.frameX == 0x2c)
                        {
                            if (tile.frameY == 0xdc)
                            {
                                num13 = 0x1;
                            }
                            else if (tile.frameY == 0xf2)
                            {
                                num13 = 0x2;
                            }
                            num14 = 0x0;
                            num18 = tiley;
                            while (num18 < (tiley + 0x64))
                            {
                                if (Main.tile[tilex + 0x1, num18].type == 0x2)
                                {
                                    num14 = 0x0;
                                    break;
                                }
                                if (Main.tile[tilex + 0x1, num18].type == 0x17)
                                {
                                    num14 = 0x1;
                                    break;
                                }
                                if (Main.tile[tilex + 0x1, num18].type == 0x3c)
                                {
                                    num14 = 0x2;
                                    break;
                                }
                                num18++;
                            }
                            CopyImgTo(img, (x * 16) - 0x18, (y * 16) - 0xC, TreeBranches[num14].GetFrame(0, num13 * 0x2a));
                        }
                        else if (tile.frameX == 0x42)
                        {
                            if (tile.frameY == 0xdc)
                            {
                                num13 = 0x1;
                            }
                            else if (tile.frameY == 0xf2)
                            {
                                num13 = 0x2;
                            }
                            num14 = 0x0;
                            for (num18 = tiley; num18 < (tiley + 0x64); num18++)
                            {
                                if (Main.tile[tilex - 0x1, num18].type == 0x2)
                                {
                                    num14 = 0x0;
                                    break;
                                }
                                if (Main.tile[tilex - 0x1, num18].type == 0x17)
                                {
                                    num14 = 0x1;
                                    break;
                                }
                                if (Main.tile[tilex - 0x1, num18].type == 0x3c)
                                {
                                    num14 = 0x2;
                                    break;
                                }
                            }
                            CopyImgTo(img, (x * 16), (y * 16) - 0xC, TreeBranches[num14].GetFrame(0x2a, num13 * 0x2a));
                        }
                    }
                }
            }
        }

        private void Render(int startx, int starty, int width, int height)
        {
            var img = new RawImage(width * 16, height * 16);

            RenderSky(img, startx, starty, width, height);
            RenderClouds(img, startx, starty, width, height);
            RenderBackgrounds(img, startx, starty, width, height);
            RenderWalls(img, startx, starty, width, height);
            RenderTreeTops(img, startx, starty, width, height);
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
            text.GetData(0, rect, ret.Data, 0, ret.Data.Length);
            AbgrToArgb(ret);
            return ret;
        }

        private static void AbgrToArgb(RawImage ints)
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

        public int Border { get; set; }

        public Texture2D Texture { get; protected set; }

        public TileFrame(Texture2D text, int frame)
            : this(text, frame, frame)
        {
        }

        public TileFrame(Texture2D text, int framewidth, int frameheight)
        {
            Texture = text;
            Border = 2;
            FrameWidth = framewidth;
            FrameHeight = frameheight;
        }

        public virtual void CreateFrames()
        {
            Columns = Texture.Width / (FrameWidth + Border);
            Rows = Texture.Height / (FrameHeight + Border);
            Frames = new RawImage[Columns, Rows];

            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    Frames[c, r] = TextureHelper.TextureToRaw(Texture, new Rectangle(c * (FrameWidth + Border), r * (FrameHeight + Border), FrameWidth, FrameHeight));
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
            Border = 4;
        }
    }

    public class OddTileFrame : TileFrame
    {
        public OddTileFrame(Texture2D text, int framewidth, int frameheight)
            : base(text, framewidth, frameheight)
        {
        }

        public override void CreateFrames()
        {
            Columns = (int)Math.Ceiling(Texture.Width / (float)(FrameWidth + Border));
            Rows = (int)Math.Ceiling(Texture.Height / (float)(FrameHeight + Border));
            Frames = new RawImage[Columns, Rows];

            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    Frames[c, r] = TextureHelper.TextureToRaw(Texture, new Rectangle(c * (FrameWidth + Border), r * (FrameHeight + Border), FrameWidth, FrameHeight));
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