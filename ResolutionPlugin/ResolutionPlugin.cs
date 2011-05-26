using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Terraria;
using TerrariaAPI;
using TerrariaAPI.Hooks;
using System.Windows.Forms;

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

        public override string Author
        {
            get { return "High"; }
        }

        public override string Description
        {
            get { return "Lets you set Terrarias resolution"; }
        }

        bool enabled = false;

        public ResolutionPlugin(Main game)
            : base(game)
        {
            GameHooks.OnPreInitialize += GameHooks_OnPreInitialize;
            GameHooks.OnLoadContent += GameHooks_OnLoadContent;
        }

        void GameHooks_OnLoadContent(Microsoft.Xna.Framework.Content.ContentManager obj)
        {
            if (enabled)
                Main.backgroundHeight[0] = Main.screenHeight + 200;
        }

        void GameHooks_OnPreInitialize()
        {
            string cmd = Environment.CommandLine;

            var sx = Regex.Match(cmd, "-x(\\d+)");
            var sy = Regex.Match(cmd, "-y(\\d+)");

            if (!sx.Success || !sy.Success)
                return;

            int x;
            if (!int.TryParse(sx.Groups[1].Value, out x))
                return;

            int y;
            if (!int.TryParse(sy.Groups[1].Value, out y))
                return;

            Main.screenWidth = x;
            Main.screenHeight = y;
            enabled = true;
        }
    }
}
