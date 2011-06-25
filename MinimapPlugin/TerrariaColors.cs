// Credit Terraria World Viewer - http://terrariaworldviewer.codeplex.com

using Microsoft.Xna.Framework;
using XNAHelpers;

namespace MinimapPlugin
{
    public static class TerrariaColors
    {
        public const int TileTypeCount = 86;
        public const int TileOtherOffset = (int)TileType.None;
        public const int WallOffset = (int)TileType.WallStone;

        public static int DIRT = new Color(175, 131, 101).ToAbgr();
        public static int STONE = new Color(128, 128, 128).ToAbgr();
        public static int GRASS = new Color(28, 216, 94).ToAbgr();
        public static int PLANTS = new Color(13, 101, 36).ToAbgr();
        public static int LIGHT_SOURCE = new Color(253, 62, 3).ToAbgr();
        public static int IRON = new Color(189, 159, 139).ToAbgr();
        public static int COPPER = new Color(255, 149, 50).ToAbgr();
        public static int GOLD = new Color(185, 164, 23).ToAbgr();
        public static int WOOD = new Color(86, 62, 44).ToAbgr();
        public static int WOOD_BLOCK = new Color(168, 121, 87).ToAbgr();
        public static int SILVER = new Color(217, 223, 223).ToAbgr();
        public static int DECORATIVE = new Color(0, 255, 242).ToAbgr();
        public static int IMPORTANT = new Color(255, 0, 0).ToAbgr();
        public static int CORRUPTION_STONE = new Color(98, 95, 167).ToAbgr();
        public static int CORRUPTION_GRASS = new Color(141, 137, 223).ToAbgr();
        public static int CORRUPTION_STONE2 = new Color(75, 74, 130).ToAbgr();
        public static int CORRUPTION_VINES = new Color(122, 97, 143).ToAbgr();
        public static int BLOCK = new Color(178, 0, 255).ToAbgr();
        public static int METEORITE = Color.Magenta.ToAbgr();
        public static int CLAY = new Color(216, 115, 101).ToAbgr();
        public static int DUNGEON_GREEN = new Color(26, 136, 34).ToAbgr();
        public static int DUNGEON_PINK = new Color(169, 49, 117).ToAbgr();
        public static int DUNGEON_BLUE = new Color(66, 69, 194).ToAbgr();
        public static int SPIKES = new Color(109, 109, 109).ToAbgr();
        public static int WEB = new Color(255, 255, 255).ToAbgr();
        public static int SAND = new Color(255, 218, 56).ToAbgr();
        public static int OBSIDIAN = new Color(87, 81, 173).ToAbgr();
        public static int ASH = new Color(68, 68, 76).ToAbgr();
        public static int HELLSTONE = new Color(102, 34, 34).ToAbgr();
        public static int MUD = new Color(92, 68, 73).ToAbgr();
        public static int UNDERGROUNDJUNGLE_GRASS = new Color(143, 215, 29).ToAbgr();
        public static int UNDERGROUNDJUNGLE_PLANTS = new Color(143, 215, 29).ToAbgr();
        public static int UNDERGROUNDJUNGLE_VINES = new Color(138, 206, 28).ToAbgr();
        public static int UNDERGROUNDJUNGLE_THORNS = new Color(94, 48, 55).ToAbgr();
        public static int GEMS = new Color(42, 130, 250).ToAbgr();
        public static int CACTUS = Color.DarkGreen.ToAbgr();
        public static int CORAL = Color.LightPink.ToAbgr();
        public static int HERB = Color.OliveDrab.ToAbgr();
        public static int TOMBSTONE = Color.DimGray.ToAbgr();
        public static int UNDERGROUNDMUSHROOM_GRASS = new Color(93, 127, 255).ToAbgr();
        public static int UNDERGROUNDMUSHROOM_PLANTS = new Color(177, 174, 131).ToAbgr();
        public static int UNDERGROUNDMUSHROOM_TREES = new Color(150, 143, 110).ToAbgr();

        public static int LAVA = new Color(255, 72, 0).ToAbgr();
        public static int WATER = new Color(0, 12, 255).ToAbgr();
        public static int SKY = new Color(155, 209, 255).ToAbgr();

        public static int WALL_STONE = new Color(66, 66, 66).ToAbgr();
        public static int WALL_DIRT = new Color(88, 61, 46).ToAbgr();
        public static int WALL_STONE2 = new Color(61, 58, 78).ToAbgr();
        public static int WALL_WOOD = new Color(73, 51, 36).ToAbgr();
        public static int WALL_BRICK = new Color(60, 60, 60).ToAbgr();
        public static int WALL_BACKGROUND = new Color(50, 50, 60).ToAbgr();
        public static int WALL_DUNGEON_PINK = new Color(84, 25, 60).ToAbgr();
        public static int WALL_DUNGEON_BLUE = new Color(29, 31, 72).ToAbgr();
        public static int WALL_DUNGEON_GREEN = new Color(14, 68, 16).ToAbgr();

        public static int UNKNOWN = Color.Magenta.ToAbgr();
        public static int NONE = Color.Transparent.ToAbgr();

        public static int[] GetColors()
        {
            int[] colors = new int[512];

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
            colors[80] = TerrariaColors.CACTUS;
            colors[81] = TerrariaColors.CORAL;
            colors[82] = TerrariaColors.HERB;
            colors[83] = TerrariaColors.HERB;
            colors[84] = TerrariaColors.HERB;
            colors[85] = TerrariaColors.TOMBSTONE;

            colors[TileOtherOffset] = TerrariaColors.NONE;
            colors[TileOtherOffset + 1] = TerrariaColors.SKY;
            colors[TileOtherOffset + 2] = TerrariaColors.WATER;
            colors[TileOtherOffset + 3] = TerrariaColors.LAVA;

            colors[WallOffset] = TerrariaColors.WALL_STONE;
            colors[WallOffset + 1] = TerrariaColors.WALL_DIRT;
            colors[WallOffset + 2] = TerrariaColors.WALL_STONE2;
            colors[WallOffset + 3] = TerrariaColors.WALL_WOOD;
            colors[WallOffset + 4] = TerrariaColors.WALL_BRICK;
            colors[WallOffset + 5] = TerrariaColors.WALL_BRICK;
            colors[WallOffset + 6] = TerrariaColors.WALL_DUNGEON_BLUE;
            colors[WallOffset + 7] = TerrariaColors.WALL_DUNGEON_GREEN;
            colors[WallOffset + 8] = TerrariaColors.WALL_DUNGEON_PINK;
            colors[WallOffset + 9] = TerrariaColors.WALL_BRICK;
            colors[WallOffset + 10] = TerrariaColors.WALL_BRICK;
            colors[WallOffset + 11] = TerrariaColors.WALL_BRICK;
            colors[WallOffset + 12] = TerrariaColors.WALL_BRICK;
            colors[WallOffset + 13] = TerrariaColors.WALL_BACKGROUND;

            for (int i = TerrariaColors.TileTypeCount; i < TerrariaColors.TileOtherOffset; i++)
            {
                colors[i] = TerrariaColors.UNKNOWN;
            }

            for (int i = TileOtherOffset + 4; i < TerrariaColors.WallOffset; i++)
            {
                colors[i] = TerrariaColors.UNKNOWN;
            }

            for (int i = WallOffset + 14; i < colors.Length; i++)
            {
                colors[i] = TerrariaColors.UNKNOWN;
            }

            return colors;
        }
    }
}