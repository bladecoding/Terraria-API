using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerrariaAPI
{
    public struct PluginInfo
    {
        public string Name;
        public string Version;
        public string Author;
        public string Description;
    }

    class TerrariaPlugin
    {
        public PluginInfo PluginInfo;

        public TerrariaPlugin(PluginInfo info)
        {
            PluginInfo = info;
        }

        public TerrariaPlugin(string Name, string Version, string Author, string Description)
        {
            PluginInfo.Name = Name;
            PluginInfo.Version = Version;
            PluginInfo.Description = Description;
            PluginInfo.Author = Author;
        }

        /// <summary>
        /// Plugin initialization should be done in this function
        /// </summary>
        public virtual void Load()
        {

        }

        /// <summary>
        /// Anything that needs to be done before the plugin is unloaded should be done in this function
        /// </summary>
        public virtual void Unload()
        {

        }
    }
}
