using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Terraria;

namespace TerrariaAPI
{
    public static class Program
    {
        static readonly string PluginsPath = "plugins";
        static List<TerrariaPlugin> Plugins = new List<TerrariaPlugin>();
        public static void Initialize(Main main)
        {
            if (!Directory.Exists(PluginsPath))
                return;
            foreach (var f in new DirectoryInfo(PluginsPath).GetFiles("*.dll"))
            {
                var asm = Assembly.LoadFile(f.FullName);
                foreach (var t in asm.GetTypes())
                {
                    if (t.BaseType == typeof(TerrariaPlugin))
                    {
                        Plugins.Add((TerrariaPlugin)Activator.CreateInstance(t, main));
                    }
                }
            }
        }
        public static void DeInitialize()
        {
            foreach (var p in Plugins)
                p.Dispose();
        }
    }
}
