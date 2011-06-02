using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using TerrariaAPI;
using TerrariaAPI.Hooks;
using Microsoft.Xna.Framework.Input;

namespace SpawnPlugin
{
    public class SpawnPlugin : TerrariaPlugin
    {
        private InputManager input = new InputManager();
        
        public override string Name
        {
            get { return "Spawn"; }
        }

        public override Version Version
        {
            get { return new Version(1, 0); }
        }

        public override string Author
        {
            get { return "Deathmax"; }
        }

        public override string Description
        {
            get { return "Plugin that spawns bosses"; }
        }

        public override Version APIVersion
        {
            get { return new Version(1, 1); }
        }

        public SpawnPlugin(Main game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            GameHooks.OnUpdate += new Action<Microsoft.Xna.Framework.GameTime>(GameHooks_OnUpdate);
        }

        void GameHooks_OnUpdate(Microsoft.Xna.Framework.GameTime obj)
        {
            var player = Main.player[Main.myPlayer];
            if (Game.IsActive)
            {
                input.Update();

                if (input.IsKeyDown(Keys.F6))
                {
                    if (input.IsKeyDown(Keys.NumPad0))
                        NetMessage.SendData(23, -1, -1, "", NewNPC(0, (int)player.position.X, (int)player.position.Y, (new Random()).Next(1, 8)), 0f, 0f, 0f);
                    //else if (input.IsKeyDown(Keys.NumPad1))
                    //    NewNPC(1, (int)player.position.X, (int)player.position.Y, (new Random()).Next(1, 8));
                    else if (input.IsKeyDown(Keys.NumPad2))
                        NetMessage.SendData(23, -1, -1, "", NewNPC(2, (int)player.position.X, (int)player.position.Y, (new Random()).Next(1, 8)), 0f, 0f, 0f);
                }
            }
        }

        public override void DeInitialize()
        {
            GameHooks.OnUpdate -= new Action<Microsoft.Xna.Framework.GameTime>(GameHooks_OnUpdate);
        }
        public static int NewNPC(int type, int x, int y, int target)
        {

            switch (type)
            {
                case 0: //World Eater
                    WorldGen.shadowOrbSmashed = true;
                    WorldGen.shadowOrbCount = 3;
                    int w = NPC.NewNPC(x, y, 13, 1);
                    Main.npc[w].target = target;
                    return w;
                    break;
                case 1: //Eye
                    Main.time = 4861;
                    Main.dayTime = false;
                    WorldGen.spawnEye = true;
                    break;
                case 2: //Skeletron
                    int enpeecee = NPC.NewNPC(x, y, 0x23, 0);
                    Main.npc[enpeecee].netUpdate = true;
                    return enpeecee;
                    break;

            }
            return -1;

        }
    }
}
