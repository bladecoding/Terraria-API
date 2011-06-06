using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Terraria;
using TerrariaAPI;
using TerrariaAPI.Hooks;

namespace ResolutionPlugin
{
    /// <summary>
    /// Commands: fs, fullscreen, auto, w, width, x, h, height, y, skip, skipintro, fps
    /// </summary>
    public class ResolutionPlugin : TerrariaPlugin
    {
        public override string Name
        {
            get { return "Resolution"; }
        }

        public override Version Version
        {
            get { return new Version(2, 0); }
        }

        public override Version APIVersion
        {
            get { return new Version(1, 2); }
        }

        public override string Author
        {
            get { return "Jaex / High"; }
        }

        public override string Description
        {
            get { return "Lets you set Terraria's resolution"; }
        }

        public ResolutionPlugin(Main game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            GameHooks.Initialize += GameHooks_OnPreInitialize;
        }

        public override void DeInitialize()
        {
            GameHooks.Initialize -= GameHooks_OnPreInitialize;
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
                }
            }

            if (CheckCommand(cmd, "skip", "skipintro"))
            {
                Main.showSplash = false;
            }

            if (CheckCommand(cmd, "fps"))
            {
                Main.showFrameRate = true;
            }
        }

        private bool CheckCommand(string text, params string[] command)
        {
            return command.Any(x => text.IndexOf("-" + x, StringComparison.InvariantCultureIgnoreCase) >= 0);
        }
    }
}