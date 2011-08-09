using System;
using Microsoft.Xna.Framework;
using Terraria;
using TerrariaAPI;
using TerrariaAPI.Hooks;
using XNAHelpers;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace TexturePlugin
{
    [APIVersion(1, 7)]
    public class TexturePlugin : TerrariaPlugin
    {
        public override string Name
        {
            get { return "Texture Loader"; }
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

        private void TerrariaHooks_Update(GameTime gameTime)
        {
            if (Game.IsActive)
            {
                if (InputManager.IsKeyPressed(Keys.F8))
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