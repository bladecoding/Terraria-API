using System;
using System.Windows.Forms;
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
            get { return "High"; }
        }

        public override string Description
        {
            get { return "Just a simple 'trainer'"; }
        }

        private InputManager input = new InputManager();
        private TrainerForm trainerform = new TrainerForm();

        public TrainerPlugin(Main game)
            : base(game)
        {
        }

        public override void Dispose()
        {
            trainerform.Dispose();
            base.Dispose();
        }

        private void TerrariaHooks_OnUpdate(Microsoft.Xna.Framework.GameTime obj)
        {
            if (Game.IsActive)
            {
                input.Update();

                if (input.IsKeyUp(Keys.F7, true))
                {
                    trainerform.Visible = !trainerform.Visible;
                }

                if (trainerform.InfAmmo)
                {
                    for (int i = 0; i < Main.player[Main.myPlayer].inventory.Length; i++)
                    {
                        if (Main.player[Main.myPlayer].inventory[i].ammo > 0)
                            Main.player[Main.myPlayer].inventory[i].stack = 250;
                    }
                }

                if (trainerform.InfBreath)
                    Main.player[Main.myPlayer].breath = Main.player[Main.myPlayer].breathMax;

                if (trainerform.InfMana)
                    Main.player[Main.myPlayer].statMana = Main.player[Main.myPlayer].statManaMax;
            }
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
    }
}