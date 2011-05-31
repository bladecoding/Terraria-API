using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
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
            var r = new Random();
            Colors = new int[0x53];

            Colors[0] = 0xFF << 24 | 0x976b4b;
            Colors[1] = Color.Gray.ToArgb();
            Colors[2] = Color.DarkGreen.ToArgb();
            Colors[4] = Color.OrangeRed.ToArgb();
            Colors[5] = Color.RosyBrown.ToArgb();

            Colors[22] = Color.Purple.ToArgb();
            Colors[23] = Color.MediumPurple.ToArgb();

            Colors[53] = Color.Yellow.ToArgb();

            Colors[0x50] = Color.Blue.ToArgb();
            Colors[0x51] = Color.Red.ToArgb();
            unchecked
            {
                Colors[0x52] = (int)0xFF010101;
            }

            for (int i = 0; i < Colors.Length; i++)
            {
                if (Colors[i] != 0)
                    continue;
                int c = (int)(r.Next(int.MinValue, int.MaxValue) | 0xFF000000);
                if (Colors.Any(o => o == c))
                {
                    i--;
                    continue;
                }
                Colors[i] = c;
            }

        }
        public int[,] FromTiles(int tilex, int tiley, int width, int height)
        {
            var ints = new int[width, height];

            for (int y = 0; y < height; y++)
            {
                if (y + tiley < 0 || y + tiley >= MaxY)
                    continue;
                for (int x = 0; x < width; x++)
                {
                    if (x + tilex < 0 || x + tilex > MaxX)
                        continue;
                    var tile = Tiles[x + tilex, y + tiley];
                    if (tile == null)
                        continue;

                    if (tile.wall > 0 || y + tiley > SurfaceY)
                        ints[x, y] = 0xFF << 24 | 0x725138;
                    if (tile.active)
                        ints[x, y] = Colors[tile.type];
                    if (tile.liquid > 0)
                        ints[x, y] = Colors[0x50];
                    if (tile.lava)
                        ints[x, y] = Colors[0x51];
                }
            }

            return ints;
        }
        public Bitmap FromColors()
        {
            var img = new Bitmap(1, Colors.Length);

            for (int y = 0; y < Colors.Length; y++)
                img.SetPixel(0, y, Color.FromArgb(Colors[y]));

            return img;
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
        public void ToColors(Bitmap img)
        {
            if (img.Height != Colors.Length)
                throw new NotSupportedException();

            for (int i = 0; i < Colors.Length; i++)
            {
                Colors[i] = img.GetPixel(0, i).ToArgb();
            }
        }
    }

}
