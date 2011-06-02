using System;
using System.Drawing;
using System.Linq;
using Terraria;

namespace MinimapPlugin
{
    public class WorldRenderer
    {
        public Tile[,] Tiles { get; set; }
        public int SurfaceY { get; set; }
        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public int[] Colors { get; set; }

        public WorldRenderer(Tile[,] tiles, int width, int height)
        {
            SurfaceY = -1;
            Tiles = tiles;
            MaxX = width;
            MaxY = height;

            Random r = new Random();

            Colors = new int[83];

            Colors[0] = Color.FromArgb(175, 131, 101).ToArgb();
            Colors[1] = Color.Gray.ToArgb();
            Colors[2] = Color.DarkGreen.ToArgb();
            Colors[4] = Color.OrangeRed.ToArgb();
            Colors[5] = Color.RosyBrown.ToArgb();
            Colors[22] = Color.Purple.ToArgb();
            Colors[23] = Color.MediumPurple.ToArgb();
            Colors[53] = Color.Yellow.ToArgb();
            Colors[80] = Color.Blue.ToArgb();
            Colors[81] = Color.Red.ToArgb();
            Colors[82] = Color.FromArgb(1, 1, 1).ToArgb();

            // Fill other colors randomly
            for (int i = 0; i < Colors.Length; i++)
            {
                if (Colors[i] == 0)
                {
                    int c = 0;

                    do
                    {
                        c = r.Next(int.MinValue, int.MaxValue) | 0xFF << 24;
                    }
                    while (Colors.Contains(c));

                    Colors[i] = c;
                }
            }
        }

        public int[,] GenerateMinimap(int tilex, int tiley, int width, int height)
        {
            tilex -= width / 2;
            tiley -= height / 2;

            int[,] ints = new int[width, height];

            for (int y = 0; y < height; y++)
            {
                if (y + tiley < 0 || y + tiley >= MaxY)
                    continue;

                for (int x = 0; x < width; x++)
                {
                    if (x + tilex < 0 || x + tilex > MaxX)
                        continue;

                    Tile tile = Tiles[x + tilex, y + tiley];

                    if (tile != null)
                    {
                        if (tile.wall > 0 || y + tiley > SurfaceY)
                            ints[x, y] = 0xFF << 24 | 0x725138;

                        if (tile.active)
                            ints[x, y] = Colors[tile.type];

                        if (tile.liquid > 0)
                        {
                            if (tile.lava)
                            {
                                ints[x, y] = Colors[81];
                            }
                            else
                            {
                                ints[x, y] = Colors[80];
                            }
                        }
                    }
                }
            }

            return ints;
        }

        /*public Bitmap FromWalls()
        {
            var img = new Bitmap(TheWorld.MaxTilesX, TheWorld.MaxTilesY);
            var bd = img.LockBits(new Rectangle(Point.Empty, img.Size), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            for (int y = 0; y < TheWorld.MaxTilesY; y++)
            {
                for (int x = 0; x < TheWorld.MaxTilesX; x++)
                {
                    var tile = TheWorld.Tiles[x, y];
                    if (tile.Wall > 0)
                        bd.SetPixelInt(x, y, Colors[tile.Wall]);
                }
            }

            img.UnlockBits(bd);
            return img;
        }*/
    }
}