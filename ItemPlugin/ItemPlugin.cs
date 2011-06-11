using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Terraria;
using TerrariaAPI;
using TerrariaAPI.Hooks;

namespace ItemPlugin
{
    /// <summary>
    /// F9 = Show item editor form
    /// Ctrl + B = Open bank
    /// </summary>
    public class ItemPlugin : TerrariaPlugin
    {
        public override string Name
        {
            get { return "Item"; }
        }

        public override Version Version
        {
            get { return new Version(1, 0); }
        }

        public override Version APIVersion
        {
            get { return new Version(1, 2); }
        }

        public override string Author
        {
            get { return "Jaex"; }
        }

        public override string Description
        {
            get { return "Give/Edit items"; }
        }

        private Player me
        {
            get { return Main.player[Main.myPlayer]; }
        }

        private InputManager input;
        private ItemForm itemForm;

        public ItemPlugin(Main game)
            : base(game)
        {
            input = new InputManager();
        }

        public override void Initialize()
        {
            GameHooks.LoadContent += GameHooks_LoadContent;
            GameHooks.Update += GameHooks_Update;
        }

        public override void DeInitialize()
        {
            GameHooks.LoadContent -= GameHooks_LoadContent;
            GameHooks.Update -= GameHooks_Update;
        }

        private void GameHooks_LoadContent(ContentManager obj)
        {
            itemForm = new ItemForm();
        }

        private void GameHooks_Update(GameTime obj)
        {
            if (Game.IsActive)
            {
                input.Update();

                if (input.IsKeyDown(Keys.F9, true) && itemForm != null)
                {
                    itemForm.Visible = !itemForm.Visible;
                }
                else if (input.IsControlDown && input.IsKeyDown(Keys.B, true))
                {
                    OpenBank();
                }
            }
        }

        private void OpenBank()
        {
            me.chestX = (int)((me.position.X + me.width * 0.5) / 16.0);
            me.chestY = (int)((me.position.Y + me.height * 0.5) / 16.0);
            me.chest = -2;
            Main.playerInventory = true;
            Main.PlaySound(10, -1, -1, 1);
        }
    }
}