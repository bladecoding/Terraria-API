using System.Drawing;
using Microsoft.Xna.Framework.Graphics;

namespace MinimapPlugin
{
    public static class MinimapHelper
    {
        public static int[] GetMinimapColors()
        {
            int[] colors = new int[81];

            colors[0] = TerrariaColors.DIRT;
            colors[1] = TerrariaColors.STONE;
            colors[2] = TerrariaColors.GRASS;
            colors[3] = TerrariaColors.PLANTS;
            colors[4] = TerrariaColors.LIGHT_SOURCE;
            colors[5] = TerrariaColors.WOOD;
            colors[6] = TerrariaColors.IRON;
            colors[7] = TerrariaColors.COPPER;
            colors[8] = TerrariaColors.GOLD;
            colors[9] = TerrariaColors.SILVER;
            colors[10] = TerrariaColors.DECORATIVE;
            colors[11] = TerrariaColors.DECORATIVE;
            colors[12] = TerrariaColors.IMPORTANT;
            colors[13] = TerrariaColors.DECORATIVE;
            colors[14] = TerrariaColors.DECORATIVE;
            colors[15] = TerrariaColors.DECORATIVE;
            colors[16] = TerrariaColors.DECORATIVE;
            colors[17] = TerrariaColors.DECORATIVE;
            colors[18] = TerrariaColors.DECORATIVE;
            colors[19] = TerrariaColors.WOOD;
            colors[20] = TerrariaColors.PLANTS;
            colors[21] = TerrariaColors.IMPORTANT;
            colors[22] = TerrariaColors.CORRUPTION_STONE;
            colors[23] = TerrariaColors.CORRUPTION_GRASS;
            colors[24] = TerrariaColors.CORRUPTION_GRASS;
            colors[25] = TerrariaColors.CORRUPTION_STONE2;
            colors[26] = TerrariaColors.IMPORTANT;
            colors[27] = TerrariaColors.PLANTS;
            colors[28] = TerrariaColors.IMPORTANT;
            colors[29] = TerrariaColors.DECORATIVE;
            colors[30] = TerrariaColors.WOOD_BLOCK;
            colors[31] = TerrariaColors.IMPORTANT;
            colors[32] = TerrariaColors.CORRUPTION_VINES;
            colors[33] = TerrariaColors.LIGHT_SOURCE;
            colors[34] = TerrariaColors.LIGHT_SOURCE;
            colors[35] = TerrariaColors.LIGHT_SOURCE;
            colors[36] = TerrariaColors.LIGHT_SOURCE;
            colors[37] = TerrariaColors.METEORITE;
            colors[38] = TerrariaColors.BLOCK;
            colors[39] = TerrariaColors.BLOCK;
            colors[40] = TerrariaColors.CLAY;
            colors[41] = TerrariaColors.DUNGEON_BLUE;
            colors[42] = TerrariaColors.LIGHT_SOURCE;
            colors[43] = TerrariaColors.DUNGEON_GREEN;
            colors[44] = TerrariaColors.DUNGEON_PINK;
            colors[45] = TerrariaColors.BLOCK;
            colors[46] = TerrariaColors.BLOCK;
            colors[47] = TerrariaColors.BLOCK;
            colors[48] = TerrariaColors.SPIKES;
            colors[49] = TerrariaColors.LIGHT_SOURCE;
            colors[50] = TerrariaColors.DECORATIVE;
            colors[51] = TerrariaColors.WEB;
            colors[52] = TerrariaColors.PLANTS;
            colors[53] = TerrariaColors.SAND;
            colors[54] = TerrariaColors.DECORATIVE;
            colors[55] = TerrariaColors.DECORATIVE;
            colors[56] = TerrariaColors.OBSIDIAN;
            colors[57] = TerrariaColors.ASH;
            colors[58] = TerrariaColors.HELLSTONE;
            colors[59] = TerrariaColors.MUD;
            colors[60] = TerrariaColors.UNDERGROUNDJUNGLE_GRASS;
            colors[61] = TerrariaColors.UNDERGROUNDJUNGLE_PLANTS;
            colors[62] = TerrariaColors.UNDERGROUNDJUNGLE_VINES;
            colors[63] = TerrariaColors.GEMS;
            colors[64] = TerrariaColors.GEMS;
            colors[65] = TerrariaColors.GEMS;
            colors[66] = TerrariaColors.GEMS;
            colors[67] = TerrariaColors.GEMS;
            colors[68] = TerrariaColors.GEMS;
            colors[69] = TerrariaColors.UNDERGROUNDJUNGLE_THORNS;
            colors[70] = TerrariaColors.UNDERGROUNDMUSHROOM_GRASS;
            colors[71] = TerrariaColors.UNDERGROUNDMUSHROOM_PLANTS;
            colors[72] = TerrariaColors.UNDERGROUNDMUSHROOM_TREES;
            colors[73] = TerrariaColors.PLANTS;
            colors[74] = TerrariaColors.PLANTS;
            colors[75] = TerrariaColors.BLOCK;
            colors[76] = TerrariaColors.BLOCK;
            colors[77] = TerrariaColors.IMPORTANT;
            colors[78] = TerrariaColors.DECORATIVE;
            colors[79] = TerrariaColors.DECORATIVE;
            colors[80] = TerrariaColors.UNKNOWN;

            return colors;
        }

        public static Texture2D BitmapToTexture(GraphicsDevice gd, Bitmap img)
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

        public static Texture2D IntsToTexture(GraphicsDevice gd, int[,] img, int width, int height)
        {
            Texture2D ret = new Texture2D(gd, width, height);
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
    }
}