using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TerrariaAPI.Hooks
{
    public static class WorldHooks
    {
        public delegate void SaveWorldD(bool resettime, HandledEventArgs e);
        /// <summary>
        /// Called right before worldgen.saveWorld.
        /// Handling this will stop terraria from saving.
        /// To call the original function call worldgen.RealsaveWorld().
        /// </summary>
        public static event SaveWorldD SaveWorld;

        public static bool OnSaveWorld(bool resettime)
        {
            if (SaveWorld == null)
                return false;

            var args = new HandledEventArgs();
            SaveWorld(resettime, args);
            return args.Handled;
        }
    }
}
