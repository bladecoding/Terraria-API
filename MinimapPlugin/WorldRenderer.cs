using Microsoft.Xna.Framework;
using Terraria;
using XNAHelpers;

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

        public int[] GenerateMinimap(int tileX, int tileY, int width, int height, float zoom = 1.0f, bool showSky = true,
            bool showBorder = true, bool showCrosshair = true)
        {
            int left = tileX - (int)((width * zoom) / 2);
            int top = tileY - (int)((height * zoom) / 2);

            int[] ints = new int[width * height];

            int posX, posY, index;

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

                    index = x + (y * width);

                    if (tile != null)
                    {
                        if (tile.liquid > 0)
                        {
                            ints[index] = tile.lava ? TerrariaColors.LAVA : TerrariaColors.WATER;
                        }
                        else if (tile.active)
                        {
                            ints[index] = Colors[tile.type];
                        }
                        else if (posY >= SurfaceY || tile.wall > 0)
                        {
                            ints[index] = TerrariaColors.WALL_STONE;
                        }
                        else if (showSky)
                        {
                            ints[index] = TerrariaColors.SKY;
                        }
                    }
                }
            }

            int borderColor = Color.White.ToAbgr();

            if (showBorder)
            {
                int right = width - 1;
                int bottom = height - 1;

                for (int x = 0; x < width; x++)
                {
                    index = x + (0 * width);
                    ints[index] = borderColor;

                    index = x + (bottom * width);
                    ints[index] = borderColor;
                }

                for (int y = 0; y < height; y++)
                {
                    index = 0 + (y * width);
                    ints[index] = borderColor;

                    index = right + (y * width);
                    ints[index] = borderColor;
                }
            }

            if (showCrosshair)
            {
                int middleX = (width - 1) / 2;
                int middleY = (height - 1) / 2;

                for (int x = 0; x < width; x++)
                {
                    index = x + (middleY * width);
                    ints[index] = borderColor;
                }

                for (int y = 0; y < height; y++)
                {
                    index = middleX + (y * width);
                    ints[index] = borderColor;
                }
            }

            return ints;
        }
    }
}