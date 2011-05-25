using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Terraria;
using TerrariaAPI;
using TerrariaAPI.Hooks;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace TrainerPlugin
{
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

        public override string Author
        {
            get { return "High"; }
        }

        public override string Description
        {
            get { return "Just a simple 'trainer'"; }
        }

        TrainerForm trainerform;
        bool f7down = false;

        public TrainerPlugin(Main game)
            : base(game)
        {
            Application.EnableVisualStyles();

            GameHooks.OnUpdate += TerrariaHooks_OnUpdate;
        }

        public override void Dispose()
        {
            GameHooks.OnUpdate -= TerrariaHooks_OnUpdate;
            if (trainerform != null)
                trainerform.Dispose();
            base.Dispose();
        }

        void TerrariaHooks_OnUpdate(Microsoft.Xna.Framework.GameTime obj)
        {
            if (!Game.IsActive)
                return;
            if (Main.keyState.IsKeyDown(Keys.F7))
            {
                f7down = true;
            }
            else if (Main.keyState.IsKeyUp(Keys.F7) && f7down)
            {
                f7down = false;
                if (trainerform == null)
                    trainerform = new TrainerForm();
                trainerform.Show();
            }
        }
    }
}
