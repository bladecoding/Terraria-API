using System;

namespace TerrariaAPI
{
    public class PluginContainer : IDisposable
    {
        public TerrariaPlugin Plugin { get; protected set; }
        public bool Initialized { get; protected set; }

        public PluginContainer(TerrariaPlugin plugin)
        {
            Plugin = plugin;
            Initialized = false;
        }

        public void Initialize()
        {
            Plugin.Initialize();
            Initialized = true;
        }

        public void DeInitialize()
        {
            Plugin.DeInitialize();
            Initialized = false;
        }

        public void Dispose()
        {
            Plugin.Dispose();
        }
    }
}