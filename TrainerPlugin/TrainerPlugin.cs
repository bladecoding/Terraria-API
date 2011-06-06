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
        private TrainerSettings trainerSettings;
        private TrainerSettings defaultSettings;

        public TrainerPlugin(Main game)
            : base(game)
        {
            input = new InputManager();
            trainerSettings = new TrainerSettings();
            defaultSettings = new TrainerSettings();
            trainerform = new TrainerForm(trainerSettings);
        }

        public override void Dispose()
        {
            trainerform.Dispose();
        }

        public override void Initialize()
        {
            GameHooks.Update += TerrariaHooks_Update;
        }

        public override void DeInitialize()
        {
            GameHooks.Update -= TerrariaHooks_Update;
        }

        private void TerrariaHooks_Update(GameTime obj)
        {
            if (Game.IsActive && trainerSettings != null)
            {
                input.Update();

                if (input.IsKeyDown(Keys.F7, true))
                {
                    trainerform.Visible = !trainerform.Visible;
                }

                if (trainerSettings.EnableTrainer)
                {
                    ApplySettings(trainerSettings);
                }
                else
                {
                    ApplySettings(defaultSettings);
                }
            }
        }

        private void ApplySettings(TrainerSettings settings)
        {
            Player me = Main.player[Main.myPlayer];

            Main.godMode = settings.GodMode;

            if (settings.InfiniteHealth)
            {
                me.statLife = me.statLifeMax;
            }

            if (settings.InfiniteMana)
            {
                me.statMana = me.statManaMax;
            }

            if (settings.InfiniteBreath)
            {
                me.breath = me.breathMax;
            }

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

            if (settings.NoFallDamage)
            {
                me.noFallDmg = true;
            }

            /*  @High: Need hook for UpdatePlayer line 516 otherwise these can't work :P

            if (settings.NoKnockback)
            {
                me.noKnockback = true;
            }

            if (settings.DoubleJump)
            {
                me.doubleJump = true;
            }

            if (settings.RocketBoots)
            {
                me.rocketBoots = true;
            }*/

            if (settings.InfiniteJump)
            {
                me.jumpAgain = true;
            }

            if (settings.NoPotionCooldown)
            {
                me.potionDelay = 0;
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
            Main.grabSun = settings.GrabSun;
            Main.stopSpawns = settings.StopSpawns;
            Main.dumbAI = settings.DumbAI;
        }
    }
}