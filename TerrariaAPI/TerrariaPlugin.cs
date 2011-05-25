using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria

namespace TerrariaAPI
{
    public struct PluginInfo
    {
        public string Name;
        public Version Version;
        public string Author;
        public string Description;

        public PluginInfo(string Name, Version Version, string Author, string Description)
        {
            this.Name = Name;
            this.Version = Version;
            this.Description = Description;
            this.Author = Author;
        }
    }

    public abstract class TerrariaPlugin
    {
        public abstract PluginInfo PluginInfo { get; }
        protected Main Game { get; private set; }

        public TerrariaPlugin(Main game)
        {
            Game = game;
        }

        /// <summary>
        /// Plugin initialization should be done in this function
        /// </summary>
        public abstract void Load();

        /// <summary>
        /// Anything that needs to be done before the plugin is unloaded should be done in this function
        /// </summary>
        public abstract void Unload();
    }
}
