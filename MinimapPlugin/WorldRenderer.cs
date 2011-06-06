using System.Drawing;
using Terraria;

namespace MinimapPlugin
{
    public class WorldRenderer
    {
        public Tile[,] Tiles { get; private set; }
        public int MaxX { get; private set; }
        public int MaxY { get; private set; }
        public int SurfaceY { get; private set; }
        public int[] Colors { get; private set; }

        public WorldRenderer(Tile[,] tiles, int worldWidth, int worldHeight, double worldSurface)
        {
            Tiles = tiles;
            MaxX = worldWidth;
            MaxY = worldHeight;
            SurfaceY = (int)worldSurface;
            Colors = MinimapHelper.GetMinimapColors();
        }

        public int[,] GenerateMinimap(int tileX, int tileY, int width, int height, float zoom = 1.0f, bool showSky = true,
            bool showBorder = true, bool showCrosshair = true)
        {
            int left = tileX - (int)((width * zoom) / 2);
            int top = tileY - (int)((height * zoom) / 2);

            int[,] ints = new int[width, height];

            int posX, posY;

            for (int y = 0; y < height; y++)
            {
                posY = top + (int)(y * zoom);

                if (posY < 0 || posY >= MaxY)
                    continue;

                for (int x = 0; x < width; x++)
                {
                    posX = left + (int)(x * zoom);

                    if (posX < 0 || posX > MaxX)
                        continue;

                    Tile tile = Tiles[posX, posY];

                    if (tile != null)
                    {
                        if (tile.liquid > 0)
                        {
                            ints[x, y] = tile.lava ? TerrariaColors.LAVA : TerrariaColors.WATER;
                        }
                        else if (tile.active)
                        {
                            ints[x, y] = Colors[tile.type];
                        }
                        else if (posY >= SurfaceY || tile.wall > 0)
                        {
                            ints[x, y] = TerrariaColors.WALL_STONE;
                        }
                        else if (showSky)
                        {
                            ints[x, y] = TerrariaColors.SKY;
                        }
                    }
                }
            }

            int borderColor = Color.White.ToArgb();

            if (showBorder)
            {
                int right = width - 1;
                int bottom = height - 1;

                for (int x = 0; x < width; x++)
                {
                    ints[x, 0] = borderColor;
                    ints[x, bottom] = borderColor;
                }

                for (int y = 0; y < height; y++)
                {
                    ints[0, y] = borderColor;
                    ints[right, y] = borderColor;
                }
            }

            if (showCrosshair)
            {
                int middleX = width / 2;
                int middleY = height / 2;

                for (int x = 0; x < width; x++)
                {
                    ints[x, middleY] = borderColor;
                }

                for (int y = 0; y < height; y++)
                {
                    ints[middleX, y] = borderColor;
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