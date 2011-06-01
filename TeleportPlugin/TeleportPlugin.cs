using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria;
using TerrariaAPI;
using TerrariaAPI.Hooks;

namespace TeleportPlugin
{
    public class TeleportPlugin : TerrariaPlugin
    {
        public override string Author
        {
            get { return "Fox-Face"; }
        }

        public override string Description
        {
            get { return "Plugin that allows teleportation"; }
        }

        public override string Name
        {
            get { return "Teleport"; }
        }

        public override Version Version
        {
            get { return new Version(1, 0); }
        }
        public override Version APIVersion
        {
            get { return new Version(1, 1); }
        }

        private List<TeleportLocation> teleports = new List<TeleportLocation>();
        private InputManager input = new InputManager();
        private TeleportForm telForm;

        public TeleportPlugin(Main game)
            : base(game)
        {
        }

        public void UpdateHook(GameTime gameTime)
        {
            if (!Game.IsActive)
                return;

            input.Update();

            if (input.IsKeyUp(Keys.F4, true))
            {
                if (telForm == null)
                    telForm = new TeleportForm();
                telForm.Show();
                telForm.BringToFront();
            }

            if (telForm == null || !telForm.Visible)
                return;

            telForm.curPosLabel.Text = "Current Pos: X: " + Main.player[Main.myPlayer].position.X + " Y: " + Main.player[Main.myPlayer].position.Y;
        }

        public override void Initialize()
        {
            GameHooks.OnUpdate += UpdateHook;
        }

        public override void DeInitialize()
        {
            GameHooks.OnUpdate -= UpdateHook;
        }
    }
}