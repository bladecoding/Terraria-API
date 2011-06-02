using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace TexturePlugin
{
    internal static class ContentLoader
    {
        internal static Dictionary<string, Texture2D> GetTextures(GraphicsDevice gd, IFileProvider provider)
        {
            var textures = new Dictionary<string, Texture2D>();
            foreach (var f in provider.GetFiles())
            {
                var img = new Bitmap(new MemoryStream(provider.GetData(f)));
                textures.Add(f, BitmapToTexture(gd, img));
            }
            return textures;
        }

        internal static void Load(Dictionary<string, Texture2D> textures)
        {
            LoadTiles(textures);
            LoadWalls(textures);
            LoadBackgrounds(textures);
            LoadItems(textures);
            LoadNpcs(textures);
            LoadProjectiles(textures);
            LoadGores(textures);
            LoadClouds(textures);
            LoadStars(textures);
            LoadLiquids(textures);
            LoadArmors(textures);
            LoadMisc(textures);
        }

        private static Texture2D BitmapToTexture(GraphicsDevice gd, Bitmap img)
        {
            var ret = new Texture2D(gd, img.Width, img.Height);
            var bd = img.LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int[] ints = new int[img.Width * img.Height];
            byte[] bytes = new byte[bd.Stride * img.Height];
            Marshal.Copy(bd.Scan0, bytes, 0, bytes.Length);
            for (int y = 0; y < img.Height; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    int idx = (y * bd.Stride) + (x * 4);
                    int r = bytes[idx];
                    int g = bytes[idx + 1];
                    int b = bytes[idx + 2];
                    int a = bytes[idx + 3];
                    ints[(y * img.Width) + x] = (a << 24) | (r << 16) | (g << 8) | b;
                }
            }
            ret.SetData(ints);
            img.UnlockBits(bd);
            return ret;
        }

        private static Texture2D GetOrNull(Dictionary<string, Texture2D> textures, string name)
        {
            Texture2D ret;
            if (!textures.TryGetValue(name, out ret))
                return null;
            return ret;
        }

        private static void LoadTiles(Dictionary<string, Texture2D> textures)
        {
            for (int j = 0x0; j < 0x50; j++)
            {
                if (!textures.ContainsKey("Tiles_" + j))
                    continue;
                Main.tileTexture[j] = textures["Tiles_" + j];
            }
        }

        private static void LoadWalls(Dictionary<string, Texture2D> textures)
        {
            for (int k = 0x1; k < 0xe; k++)
            {
                if (!textures.ContainsKey("Wall_" + k))
                    continue;
                Main.wallTexture[k] = textures["Wall_" + k];
            }
        }

        private static void LoadBackgrounds(Dictionary<string, Texture2D> textures)
        {
            for (int m = 0x0; m < 0x7; m++)
            {
                if (!textures.ContainsKey("Background_" + m))
                    continue;
                Main.backgroundTexture[m] = textures["Background_" + m];
                Main.backgroundWidth[m] = Main.backgroundTexture[m].Width;
                Main.backgroundHeight[m] = Main.backgroundTexture[m].Height;
            }
        }

        private static void LoadItems(Dictionary<string, Texture2D> textures)
        {
            for (int n = 0x0; n < 0xeb; n++)
            {
                if (!textures.ContainsKey("Item_" + n))
                    continue;
                Main.itemTexture[n] = textures["Item_" + n];
            }
        }

        private static void LoadNpcs(Dictionary<string, Texture2D> textures)
        {
            for (int n = 0x0; n < 0x2c; n++)
            {
                if (!textures.ContainsKey("NPC_" + n))
                    continue;
                Main.npcTexture[n] = textures["NPC_" + n];
            }
        }

        private static void LoadProjectiles(Dictionary<string, Texture2D> textures)
        {
            for (int n = 0x0; n < 0x25; n++)
            {
                if (!textures.ContainsKey("Projectile_" + n))
                    continue;
                Main.projectileTexture[n] = textures["Projectile_" + n];
            }
        }

        private static void LoadGores(Dictionary<string, Texture2D> textures)
        {
            for (int n = 0x0; n < 0x49; n++)
            {
                if (!textures.ContainsKey("Gore_" + n))
                    continue;
                Main.goreTexture[n] = textures["Gore_" + n];
            }
        }

        private static void LoadClouds(Dictionary<string, Texture2D> textures)
        {
            for (int n = 0x0; n < 0x4; n++)
            {
                if (!textures.ContainsKey("Cloud_" + n))
                    continue;
                Main.cloudTexture[n] = textures["Cloud_" + n];
            }
        }

        private static void LoadStars(Dictionary<string, Texture2D> textures)
        {
            for (int n = 0x0; n < 0x5; n++)
            {
                if (!textures.ContainsKey("Star_" + n))
                    continue;
                Main.starTexture[n] = textures["Star_" + n];
            }
        }

        private static void LoadLiquids(Dictionary<string, Texture2D> textures)
        {
            for (int n = 0x0; n < 0x2; n++)
            {
                if (!textures.ContainsKey("Liquid_" + n))
                    continue;
                Main.liquidTexture[n] = textures["Liquid_" + n];
            }
        }

        private static void LoadArmors(Dictionary<string, Texture2D> textures)
        {
            for (int n = 0x1; n < 0xa; n++)
            {
                Main.armorBodyTexture[n] = GetOrNull(textures, "Armor_Body_" + n) ?? Main.armorBodyTexture[n];
                Main.armorArmTexture[n] = GetOrNull(textures, "Armor_Arm_" + n) ?? Main.armorArmTexture[n];
                Main.armorLegTexture[n] = GetOrNull(textures, "Armor_Legs_" + n) ?? Main.armorLegTexture[n];
            }
            for (int n = 0x1; n < 0xc; n++)
            {
                Main.armorHeadTexture[n] = GetOrNull(textures, "Armor_Head_" + n) ?? Main.armorHeadTexture[n];
            }
            for (int n = 0x0; n < 0x11; n++)
            {
                Main.playerHairTexture[n] = GetOrNull(textures, "Player_Hair_" + n + 1) ?? Main.playerHairTexture[n];
            }
        }

        private static void LoadMisc(Dictionary<string, Texture2D> textures)
        {
            Main.cdTexture = GetOrNull(textures, "CoolDown") ?? Main.cdTexture;
            Main.logoTexture = GetOrNull(textures, "Logo") ?? Main.logoTexture;
            Main.dustTexture = GetOrNull(textures, "Dust") ?? Main.dustTexture;
            Main.sunTexture = GetOrNull(textures, "Sun") ?? Main.sunTexture;
            Main.moonTexture = GetOrNull(textures, "Moon") ?? Main.moonTexture;
            Main.blackTileTexture = GetOrNull(textures, "Black_Tile") ?? Main.blackTileTexture;
            Main.heartTexture = GetOrNull(textures, "Heart") ?? Main.heartTexture;
            Main.bubbleTexture = GetOrNull(textures, "Bubble") ?? Main.bubbleTexture;
            Main.manaTexture = GetOrNull(textures, "Mana") ?? Main.manaTexture;
            Main.cursorTexture = GetOrNull(textures, "Cursor") ?? Main.cursorTexture;
            //Main.treeTopTexture = GetOrNull(textures, "Tree_Tops") ?? Main.treeTopTexture;
            //Main.treeBranchTexture = GetOrNull(textures, "Tree_Branches") ?? Main.treeBranchTexture;
            Main.shroomCapTexture = GetOrNull(textures, "Shroom_Tops") ?? Main.shroomCapTexture;
            Main.inventoryBackTexture = GetOrNull(textures, "Inventory_Back") ?? Main.inventoryBackTexture;
            Main.textBackTexture = GetOrNull(textures, "Text_Back") ?? Main.textBackTexture;
            Main.chatTexture = GetOrNull(textures, "Chat") ?? Main.chatTexture;
            Main.chat2Texture = GetOrNull(textures, "Chat2") ?? Main.chat2Texture;
            Main.chatBackTexture = GetOrNull(textures, "Chat_Back") ?? Main.chatBackTexture;
            Main.teamTexture = GetOrNull(textures, "Team") ?? Main.teamTexture;

            Main.playerEyeWhitesTexture = GetOrNull(textures, "Player_Eye_Whites") ?? Main.playerEyeWhitesTexture;
            Main.playerEyesTexture = GetOrNull(textures, "Player_Eyes") ?? Main.playerEyesTexture;
            Main.playerHandsTexture = GetOrNull(textures, "Player_Hands") ?? Main.playerHandsTexture;
            Main.playerHands2Texture = GetOrNull(textures, "Player_Hands2") ?? Main.playerHands2Texture;
            Main.playerHeadTexture = GetOrNull(textures, "Player_Head") ?? Main.playerHeadTexture;
            Main.playerPantsTexture = GetOrNull(textures, "Player_Pants") ?? Main.playerPantsTexture;
            Main.playerShirtTexture = GetOrNull(textures, "Player_Shirt") ?? Main.playerShirtTexture;
            Main.playerShoesTexture = GetOrNull(textures, "Player_Shoes") ?? Main.playerShoesTexture;
            Main.playerUnderShirtTexture = GetOrNull(textures, "Player_Undershirt") ?? Main.playerUnderShirtTexture;
            Main.playerUnderShirt2Texture = GetOrNull(textures, "Player_Undershirt2") ?? Main.playerUnderShirt2Texture;
            Main.chainTexture = GetOrNull(textures, "Chain") ?? Main.chainTexture;
            Main.chain2Texture = GetOrNull(textures, "Chain2") ?? Main.chain2Texture;
            Main.chain3Texture = GetOrNull(textures, "Chain3") ?? Main.chain3Texture;
            Main.chain4Texture = GetOrNull(textures, "Chain4") ?? Main.chain4Texture;
            Main.chain5Texture = GetOrNull(textures, "Chain5") ?? Main.chain5Texture;
            Main.chain6Texture = GetOrNull(textures, "Chain6") ?? Main.chain6Texture;
            Main.boneArmTexture = GetOrNull(textures, "Arm_Bone") ?? Main.boneArmTexture;
        }
    }

    interface IFileProvider
    {
        string[] GetFiles();

        byte[] GetData(string name);
    }

    internal class DirProvider : IFileProvider
    {
        DirectoryInfo Dir;

        public DirProvider(DirectoryInfo di)
        {
            Dir = di;
        }

        public string[] GetFiles()
        {
            var ret = new List<string>();
            foreach (var fi in Dir.GetFiles("*.png"))
            {
                ret.Add(Path.GetFileNameWithoutExtension(fi.Name));
            }
            return ret.ToArray();
        }

        public byte[] GetData(string name)
        {
            name += ".png";
            return File.ReadAllBytes(Path.Combine(Dir.FullName, name));
        }
    }

    internal class ZipProvider : IFileProvider
    {
        ZipFile Zip;

        internal ZipProvider(Stream stream)
        {
            Zip = new ZipFile(stream);
        }

        public string[] GetFiles()
        {
            var ret = new List<string>();
            foreach (ZipEntry e in Zip)
            {
                if (!e.IsFile)
                    continue;

                if (Path.GetExtension(e.Name) != ".png")
                    continue;

                ret.Add(Path.GetFileNameWithoutExtension(e.Name));
            }
            return ret.ToArray();
        }

        public byte[] GetData(string name)
        {
            name += ".png";
            foreach (ZipEntry e in Zip)
            {
                if (!e.IsFile)
                    continue;
                if (e.Name != name)
                    continue;

                var s = Zip.GetInputStream(e);
                var ms = new MemoryStream();
                int read = 0;
                byte[] buffer = new byte[4096];
                while ((read = s.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
            return null;
        }
    }
}