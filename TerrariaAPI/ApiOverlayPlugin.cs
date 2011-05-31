using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using TerrariaAPI.Hooks;

namespace TerrariaAPI
{
    public class ApiOverlayPlugin : TerrariaPlugin
    {

        public override string Name
        {
            get { return "ApiOverlay"; }
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
            get { return ""; }
        }

        public override Version APIVersion
        {
            get { return new Version(1, 1); }
        }

        string version;
        public override void Initialize()
        {
            version = string.Format("TerrariaAPI v{0}", Program.ApiVersion);
            DrawHooks.OnEndDrawMenu += DrawHooks_OnEndDraw;
        }

        void DrawHooks_OnEndDraw(SpriteBatch obj)
        {
            if (!Main.gameMenu)
                return;

            DrawFancyText(obj, version, new Vector2(10, 10), Color.White);
        }

        void DrawFancyText(SpriteBatch sb, string text, Vector2 position, Color color)
        {
            for (int n = 0x0; n < 0x5; n++)
            {
                Color color9 = Color.Black;
                if (n == 0x4)
                {
                    color9 = color;
                    color9.R = (byte)((0xff + color9.R) / 0x2);
                    color9.G = (byte)((0xff + color9.R) / 0x2);
                    color9.B = (byte)((0xff + color9.R) / 0x2);
                }
                color9.A = (byte)(color9.A * 0.3f);
                float offsetx = 0x0;
                float offsety = 0x0;
                switch (n)
                {
                    case 0x0:
                        offsetx = -2;
                        break;

                    case 0x1:
                        offsetx = 0x2;
                        break;

                    case 0x2:
                        offsety = -2;
                        break;

                    case 0x3:
                        offsety = 0x2;
                        break;
                }
                Vector2 vector7 = Main.fontMouseText.MeasureString(text);
                vector7.X *= 0.5f;
                vector7.Y *= 0.5f;
                sb.DrawString(Main.fontMouseText, text, new Vector2(position.X + offsetx, position.Y + offsety), color9);
            }

        }

        public override void DeInitialize()
        {
            DrawHooks.OnEndDraw -= DrawHooks_OnEndDraw;
        }

        public ApiOverlayPlugin(Main game)
            : base(game)
        {
        }
    }
}
