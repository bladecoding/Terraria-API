using System;
using Microsoft.Xna.Framework;
using Terraria;
using TerrariaAPI;
using TerrariaAPI.Hooks;
using XNAHelpers;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace TexturePlugin
{
    [APIVersion(1, 5)]
    public class TexturePlugin : TerrariaPlugin
    {
        public override string Name
        {
            get { return "TextureLoader"; }
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
            get { return "Allows you to reload textures from png files"; }
        }

        private TextureForm textureform;
        private InputManager input = new InputManager();

        public TexturePlugin(Main game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            GameHooks.Update += TerrariaHooks_Update;
        }

        public override void DeInitialize()
        {
            GameHooks.Update -= TerrariaHooks_Update;
        }

        public override void Dispose()
        {
            if (textureform != null)
                textureform.Dispose();

            base.Dispose();
        }

        private void TerrariaHooks_Update(GameTime obj)
        {
            if (Game.IsActive)
            {
                input.Update();

                if (input.IsKeyDown(Keys.F8, true))
                {
                    if (textureform == null || textureform.IsDisposed)
                    {
                        textureform = new TextureForm(Game.GraphicsDevice);
                    }

                    textureform.Show();
                    textureform.BringToFront();
                }
            }
        }
    }
}