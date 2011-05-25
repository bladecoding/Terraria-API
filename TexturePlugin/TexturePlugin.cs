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

namespace TexturePlugin
{
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

        TextureForm textureform;
        bool f8down = false;

        public TexturePlugin(Main game)
            : base(game)
        {
            Application.EnableVisualStyles();

            GameHooks.OnUpdate += TerrariaHooks_OnUpdate;
        }

        public override void Dispose()
        {
            GameHooks.OnUpdate -= TerrariaHooks_OnUpdate;
            if (textureform != null)
                textureform.Dispose();
            base.Dispose();
        }

        void TerrariaHooks_OnUpdate(Microsoft.Xna.Framework.GameTime obj)
        {
            if (!Game.IsActive)
                return;
            if (Main.keyState.IsKeyDown(Keys.F8))
            {
                f8down = true;
            }
            else if (Main.keyState.IsKeyUp(Keys.F8) && f8down)
            {
                f8down = false;
                if (textureform == null)
                    textureform = new TextureForm(Game.GraphicsDevice);
                textureform.Show();
            }
        }
    }
}
