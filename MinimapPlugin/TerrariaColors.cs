// Colors from BinaryConstruct - Terraria Map Editor

using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using XNAHelpers;

namespace MinimapPlugin
{
    public static class TerrariaColors
    {
        public const int TileTypeCount = 86;
        public const int TileOtherOffset = (int)TileType.None;
        public const int WallOffset = (int)TileType.WallStone;

        public static int[] GetColors()
        {
            uint[] colors = new uint[512];

            colors[0] = 0xff916a4f; // Dirt
            colors[1] = 0xff808080; // Stone
            colors[2] = 0xff1cd85e; // Grass
            colors[3] = 0xff0d6524; // Plants
            colors[4] = 0xfffd3e03; // Torches
            colors[5] = 0xff634631; // Trees
            colors[6] = 0xff6b594e; // Iron
            colors[7] = 0xffc6561d; // Copper
            colors[8] = 0xffb9a417; // Gold
            colors[9] = 0xffd9dfdf; // Silver
            colors[10] = 0xff00fff2; // Door1
            colors[11] = 0xff00fff2; // Door2
            colors[12] = 0xffff0000; // HeartStone
            colors[13] = 0xff00fff2; // Bottle
            colors[14] = 0xff00fff2; // Table
            colors[15] = 0xff00fff2; // Chair
            colors[16] = 0xff00fff2; // Anvil
            colors[17] = 0xff00fff2; // Furnace
            colors[18] = 0xff00fff2; // Workbench
            colors[19] = 0xff6b3a18; // WoodenPlatform
            colors[20] = 0xff0d6524; // PlantsDecorative
            colors[21] = 0xffffd800; // Chest
            colors[22] = 0xff625fa7; // Demonite
            colors[23] = 0xff8d89df; // CorruptionGrass
            colors[24] = 0xff8d89df; // CorruptionPlants
            colors[25] = 0xff4b4a82; // Ebonstone
            colors[26] = 0xff9000ff; // DemonAltar
            colors[27] = 0xffc4ff14; // Sunflower
            colors[28] = 0xff8c2726; // Pot
            colors[29] = 0xff00fff2; // PiggyBank
            colors[30] = 0xff684934; // BlockWood
            colors[31] = 0xff000000; // ShadowOrb
            colors[32] = 0xff7a618f; // CorruptionVines
            colors[33] = 0xfffd3e03; // Candle
            colors[34] = 0xfffd3e03; // ChandlerCopper
            colors[35] = 0xfffd3e03; // ChandlerSilver
            colors[36] = 0xfffd3e03; // ChandlerGold
            colors[37] = 0xffdf9f89; // Meterorite
            colors[38] = 0xff909090; // BlockStone
            colors[39] = 0xffb200ff; // BlockRedStone
            colors[40] = 0xffac5b4d; // Clay
            colors[41] = 0xff545498; // BlockBlueStone
            colors[42] = 0xffef904b; // LightGlobe
            colors[43] = 0xff39a851; // BlockGreenStone
            colors[44] = 0xffb200ff; // BlockPinkStone
            colors[45] = 0xffffd514; // BlockGold
            colors[46] = 0xffe5e5e5; // BlockSilver
            colors[47] = 0xffff5900; // BlockCopper
            colors[48] = 0xff6d6d6d; // Spikes
            colors[49] = 0xff2b8fff; // CandleBlue
            colors[50] = 0xff00fff2; // Books
            colors[51] = 0xffffffff; // Web
            colors[52] = 0xff0d6524; // Vines
            colors[53] = 0xffffda38; // Sand
            colors[54] = 0x20ffffff; // Glass
            colors[55] = 0xffffae5e; // Signs
            colors[56] = 0xff5751ad; // Obsidian
            colors[57] = 0xff44444c; // Ash
            colors[58] = 0xff662222; // Hellstone
            colors[59] = 0xff5c4449; // Mud
            colors[60] = 0xff8fd71d; // UndergroundJungleGrass
            colors[61] = 0xff8fd71d; // UndergroundJunglePlants
            colors[62] = 0xff8ace1c; // UndergroundJungleVines
            colors[63] = 0xff2a82fa; // GemSapphire
            colors[64] = 0xff2a82fa; // GemRuby
            colors[65] = 0xff2a82fa; // GemEmerald
            colors[66] = 0xff2a82fa; // GemTopaz
            colors[67] = 0xff2a82fa; // GemAmethyst
            colors[68] = 0xff2a82fa; // GemDiamond
            colors[69] = 0xff5e3037; // UndergroundJungleThorns
            colors[70] = 0xff5d7fff; // UndergroundMushroomGrass
            colors[71] = 0xffb1ae83; // UndergroundMushroomPlants
            colors[72] = 0xff968f6e; // UndergroundMushroomTrees
            colors[73] = 0xff0d6524; // Plants2
            colors[74] = 0xff0d6524; // Plants3
            colors[75] = 0xffb200ff; // BlockObsidian
            colors[76] = 0xffc6001d; // BlockHellstone
            colors[77] = 0xffd50010; // Hellforge
            colors[78] = 0xff00fff2; // DecorativePot
            colors[79] = 0xff00fff2; // Bed
            colors[80] = 0xff00a500; // Cactus
            colors[81] = 0xffe55340; // Coral
            colors[82] = 0xffff7800; // HerbSprouts
            colors[83] = 0xffff7800; // HerbStalks
            colors[84] = 0xffff7800; // Herbs
            colors[85] = 0xffc0c0c0; // Tombstone

            colors[TileOtherOffset] = 0x00000000; // None
            colors[TileOtherOffset + 1] = 0xff9bd1ff; // Sky
            colors[TileOtherOffset + 2] = 0x80000cff; // Water
            colors[TileOtherOffset + 3] = 0xf0ff4800; // Lava

            colors[WallOffset] = 0xff424242; // WallStone
            colors[WallOffset + 1] = 0xff583d2e; // WallDirt
            colors[WallOffset + 2] = 0xff312545; // WallCorruption
            colors[WallOffset + 3] = 0xff4a2e1c; // WallWood
            colors[WallOffset + 4] = 0xff454545; // WallBrick
            colors[WallOffset + 5] = 0xff500000; // WallRed
            colors[WallOffset + 6] = 0xff000060; // WallBlue
            colors[WallOffset + 7] = 0xff005000; // WallGreen
            colors[WallOffset + 8] = 0xff48004F; // WallPink
            colors[WallOffset + 9] = 0xff774707; // WallGold
            colors[WallOffset + 10] = 0xff828282; // WallSilver
            colors[WallOffset + 11] = 0xff3F1907; // WallCopper
            colors[WallOffset + 12] = 0xff3F0707; // WallHellstone
            colors[WallOffset + 13] = 0xff3F0707; // WallUnknown

            for (int i = TerrariaColors.TileTypeCount; i < TerrariaColors.TileOtherOffset; i++)
            {
                colors[i] = 0xffff00ff; // Unknown
            }

            for (int i = TileOtherOffset + 4; i < TerrariaColors.WallOffset; i++)
            {
                colors[i] = 0xffff00ff; // Unknown
            }

            for (int i = WallOffset + 14; i < colors.Length; i++)
            {
                colors[i] = 0xffff00ff; // Unknown
            }

            return colors.Select(x => x.ToAbgr()).ToArray();
        }

        public static void ParseColors()
        {
            string pattern = @"(\d+)\|(.+?)\|\#(.+?)\b";
            MatchCollection matches = Regex.Matches(File.ReadAllText("colors.txt"), pattern);
            StringBuilder sb = new StringBuilder();
            foreach (Match match in matches)
            {
                if (match.Groups.Count == 4)
                {
                    sb.AppendFormat("colors[{0}] = 0x{1}; // {2}\r\n", match.Groups[1], match.Groups[3].ToString().ToLowerInvariant(), match.Groups[2]);
                }
            }
            File.WriteAllText("colors2.txt", sb.ToString());
        }
    }
}