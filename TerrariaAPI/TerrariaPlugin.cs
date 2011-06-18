using System;
using Terraria;

namespace TerrariaAPI
{
    public abstract class TerrariaPlugin : IDisposable
    {
        public virtual string Name
        {
            get { return "None"; }
        }
        public virtual Version Version
        {
            get { return new Version(1, 0); }
        }
        public virtual string Author
        {
            get { return "None"; }
        }
        public virtual string Description
        {
            get { return "None"; }
        }

        public bool Enabled { get; set; }

        /// <summary>
        /// Order of when the plugin will be initialized.
        /// 0 = Last
        /// 1 = Second to last
        /// ...
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Terraria.Main instance
        /// </summary>
        protected Main Game { get; private set; }

        protected TerrariaPlugin(Main game)
        {
            Order = 1;
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

    /// <summary>
    /// Version(Major, Minor) of the API the plugin was built for.
    /// If major/minor do not match then the plugin will not load.
    /// This is to prevent exceptionless crashes when the hooks change.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class APIVersionAttribute : Attribute
    {
        public Version ApiVersion;

        public APIVersionAttribute(Version version)
        {
            ApiVersion = version;
        }

        public APIVersionAttribute(int major, int minor)
            : this(new Version(major, minor))
        {
        }
    }
}