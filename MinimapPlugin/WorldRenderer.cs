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

        public WorldRenderer(Tile[,] tiles, int worldWidth, int worldHeight)
        {
            SurfaceY = -1;
            Tiles = tiles;
            MaxX = worldWidth;
            MaxY = worldHeight;
            Colors = MinimapHelper.GetMinimapColors();
        }

        public int[,] GenerateMinimap(int tilex, int tiley, int width, int height, float zoom = 1.0f)
        {
            tilex -= (int)((width * zoom) / 2);
            tiley -= (int)((height * zoom) / 2);

            int[,] ints = new int[width, height];

            int posX, posY;

            for (int y = 0; y < height; y++)
            {
                posY = (int)(y * zoom) + tiley;

                if (posY < 0 || posY >= MaxY)
                    continue;

                for (int x = 0; x < width; x++)
                {
                    posX = (int)(x * zoom) + tilex;

                    if (posX < 0 || posX > MaxX)
                        continue;

                    Tile tile = Tiles[posX, posY];

                    if (tile != null)
                    {
                        if (tile.liquid > 0)
                        {
                            if (tile.lava)
                            {
                                ints[x, y] = TerrariaColors.LAVA;
                            }
                            else
                            {
                                ints[x, y] = TerrariaColors.WATER;
                            }
                        }
                        else if (tile.active)
                        {
                            ints[x, y] = Colors[tile.type];
                        }
                        else if (posY > SurfaceY || tile.wall > 0)
                        {
                            ints[x, y] = TerrariaColors.WALL_STONE;
                        }
                        else
                        {
                            int transparency = (int)(0xFF * 0.20f);
                            ints[x, y] = TerrariaColors.SKY + (transparency << 24);
                        }
                    }
                }
            }

            // Add border

            /*int black = Color.Black.ToArgb();

            for (int x = 0; x < width; x++)
            {
                ints[x, 0] = black;
            }

            int right = width - 1;

            for (int x = 0; x < height; x++)
            {
                ints[right, x] = black;
            }*/

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