using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Terraria;
using TerrariaAPI;
using TerrariaAPI.Hooks;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace TrainerPlugin
{
    /// <summary>
    /// F7 = Show trainer form
    /// </summary>
    public class TrainerPlugin : TerrariaPlugin
    {
        public override string Name
        {
            get { return "Trainer"; }
        }

        public override Version Version
        {
            get { return new Version(1, 0); }
        }

        public override Version APIVersion
        {
            get { return new Version(1, 1); }
        }

        public override string Author
        {
            get { return "High / Jaex"; }
        }

        public override string Description
        {
            get { return "Just a simple 'trainer'"; }
        }

        private InputManager input;
        private TrainerForm trainerform;
        private TrainerSettings settings;

        public TrainerPlugin(Main game)
            : base(game)
        {
            input = new InputManager();
            settings = new TrainerSettings();
            trainerform = new TrainerForm(settings);
        }

        public override void Dispose()
        {
            trainerform.Dispose();
        }

        public override void Initialize()
        {
            Application.EnableVisualStyles();
            GameHooks.OnUpdate += TerrariaHooks_OnUpdate;
        }

        public override void DeInitialize()
        {
            GameHooks.OnUpdate -= TerrariaHooks_OnUpdate;
        }

        private void TerrariaHooks_OnUpdate(GameTime obj)
        {
            if (Game.IsActive && settings != null && settings.EnableTrainer)
            {
                input.Update();

                if (input.IsKeyDown(Keys.F7, true))
                {
                    trainerform.Visible = !trainerform.Visible;
                }

                Player me = Main.player[Main.myPlayer];

                Main.godMode = settings.GodMode;

                if (settings.InfiniteMana)
                    me.statMana = me.statManaMax;

                if (settings.InfiniteBreath)
                    me.breath = me.breathMax;

                if (settings.InfiniteAmmo)
                {
                    foreach (Item item in me.inventory)
                    {
                        if (item.ammo > 0)
                        {
                            item.stack = item.maxStack;
                        }
                    }
                }

                Main.lightTiles = settings.LightTiles;

                if (settings.LightYourCharacter)
                {
                    int x = (int)(me.position.X / 16);
                    int y = (int)(me.position.Y / 16);
                    Lighting.addLight(x, y, 1f);
                }

                if (settings.LightCursor)
                {
                    int x = (int)((Main.mouseState.X + Main.screenPosition.X) / 16f);
                    int y = (int)((Main.mouseState.Y + Main.screenPosition.Y) / 16f);
                    Lighting.addLight(x, y, 1f);
                }

                Main.debugMode = settings.DebugMode;
                Main.grabSun = settings.GrabSub;
                Main.stopSpawns = settings.StopSpawns;
                Main.dumbAI = settings.DumbAI;
            }
        }
    }
}