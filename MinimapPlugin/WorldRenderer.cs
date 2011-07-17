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
        public int RockLayerY { get; private set; }
        public int HellLayerY { get; private set; }
        public int[] Colors { get; private set; }

        public WorldRenderer(Tile[,] tiles, int worldWidth, int worldHeight, double worldSurface, double rockLayer)
        {
            Tiles = tiles;
            MaxX = worldWidth;
            MaxY = worldHeight;
            SurfaceY = (int)worldSurface;
            RockLayerY = (int)rockLayer + (600 / 16);
            HellLayerY = MaxY - 195;
            Colors = TerrariaColors.GetColors();
        }

        public int[] GenerateMinimap(int tileX, int tileY, int width, int height, float zoom = 1.0f, bool drawWall = true, bool drawSky = true,
            bool showBorder = true, bool showCrosshair = true)
        {
            int left = tileX - (int)((width * zoom) / 2);
            int top = tileY - (int)((height * zoom) / 2);

            int[] tiles = new int[width * height];

            int posX, posY, tileType;

            Tile tile;

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

                    tile = Tiles[posX, posY];

                    if (tile != null)
                    {
                        if (tile.liquid > 0)
                        {
                            tileType = tile.lava ? (int)LiquidType.Lava : (int)LiquidType.Water;
                        }
                        else if (tile.active)
                        {
                            tileType = tile.type;
                        }
                        else if (drawWall)
                        {
                            if (tile.wall > 0)
                            {
                                tileType = TerrariaColors.WallOffset + tile.wall;
                            }
                            else if (posY > HellLayerY)
                            {
                                tileType = (int)WallType.WallHellstone;
                            }
                            else if (posY > RockLayerY)
                            {
                                tileType = (int)WallType.WallStone;
                            }
                            else if (posY > SurfaceY)
                            {
                                tileType = (int)WallType.WallDirt;
                            }
                            else if (drawSky)
                            {
                                tileType = (int)WallType.Sky;
                            }
                            else
                            {
                                tileType = TerrariaColors.WallOffset - 1;
                            }
                        }
                        else
                        {
                            tileType = TerrariaColors.WallOffset - 1;
                        }

                        tiles[x + y * width] = Colors[tileType];
                    }
                }
            }

            int borderColor = Color.White.ToAbgr();

            if (showBorder)
            {
                int right = width - 1;
                int bottom = (height - 1) * width;

                for (int x = 0; x < width; x++)
                {
                    tiles[x] = borderColor; // Top line
                    tiles[x + bottom] = borderColor; // Bottom line
                }

                for (int y = 0; y < height; y++)
                {
                    tiles[y * width] = borderColor; // Left line
                    tiles[right + (y * width)] = borderColor; // Right line
                }
            }

            if (showCrosshair)
            {
                int middleX = (width - 1) / 2;
                int middleY = (height - 1) / 2;

                for (int x = 0; x < width; x++)
                {
                    tiles[x + (middleY * width)] = borderColor; // Horizontal line
                }

                for (int y = 0; y < height; y++)
                {
                    tiles[middleX + (y * width)] = borderColor; // Vertical line
                }
            }

            return tiles;
        }
    }
}