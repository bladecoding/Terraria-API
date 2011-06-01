using System;
using Terraria;

namespace TerrariaAPI
{
    public abstract class TerrariaPlugin : IDisposable
    {
        public abstract string Name { get; }
        public abstract Version Version { get; }
        public abstract string Author { get; }
        public abstract string Description { get; }

        /// <summary>
        /// Version(Major, Minor) of the API the plugin was built for.
        /// If major/minor do not match then the plugin will not load.
        /// This is to prevent exceptionless crashes when the hooks change.
        /// </summary>
        public abstract Version APIVersion { get; }

        /// <summary>
        /// Terraria.Main instance
        /// </summary>
        protected Main Game { get; private set; }

        protected TerrariaPlugin(Main game)
        {
            Game = game;
        }

        public virtual void Dispose()
        {
        }

        /// <summary>
        /// Called when the plugin is initialized
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// Called when the plugin is DeInitialized
        /// </summary>
        public abstract void DeInitialize();
    }
}