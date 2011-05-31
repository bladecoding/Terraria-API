using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Content;
using Terraria;
using TerrariaAPI;
using TerrariaAPI.Hooks;

namespace ResolutionPlugin
{
    public class ResolutionPlugin : TerrariaPlugin
    {
        public override string Name
        {
            get { return "ResolutionPlugin"; }
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
            get { return "Juzz/High"; }
        }

        public override string Description
        {
            get { return "Lets you set Terraria's resolution"; }
        }

        bool enabled = false;

        public ResolutionPlugin(Main game)
            : base(game)
        {
            
        }

        private void GameHooks_OnPreInitialize()
        {
            string cmd = Environment.CommandLine;

            if (CheckCommand(cmd, "fs", "fullscreen"))
            {
                Game.graphics.IsFullScreen = true;
            }

            if (CheckCommand(cmd, "auto"))
            {
                Size screenSize = SystemInformation.VirtualScreen.Size;
                Main.screenWidth = screenSize.Width;
                Main.screenHeight = screenSize.Height;
                enabled = true;
            }
            else
            {
                Match w = Regex.Match(cmd, @"-(?:w|width|x)\s*(\d+)", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
                Match h = Regex.Match(cmd, @"-(?:h|height|y)\s*(\d+)", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

                int width, height;

                if (w.Success && h.Success && int.TryParse(w.Groups[1].Value, out width) && int.TryParse(h.Groups[1].Value, out height))
                {
                    Main.screenWidth = width;
                    Main.screenHeight = height;
                    enabled = true;
                }
            }

            if (CheckCommand(cmd, "skip", "skipintro"))
            {
                Main.showSplash = false;
            }
        }

        private void GameHooks_OnLoadContent(ContentManager obj)
        {
            if (enabled)
            {
                Main.backgroundHeight[0] = Main.screenHeight + 200;
            }
        }

        private bool CheckCommand(string text, params string[] command)
        {
            return command.Any(x => text.IndexOf("-" + x, StringComparison.InvariantCultureIgnoreCase) >= 0);
        }

        public override void Initialize()
        {
            GameHooks.OnPreInitialize += GameHooks_OnPreInitialize;
            GameHooks.OnLoadContent += GameHooks_OnLoadContent;
        }

        public override void DeInitialize()
        {
            GameHooks.OnPreInitialize -= GameHooks_OnPreInitialize;
            GameHooks.OnLoadContent -= GameHooks_OnLoadContent;
        }
    }
}