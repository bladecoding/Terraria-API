using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
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

            bool error = false;

            foreach (var f in new DirectoryInfo(PluginsPath).GetFiles("*.dll"))
            {
                var asm = Assembly.LoadFile(f.FullName);
                foreach (var t in asm.GetTypes())
                {
                    if (t.BaseType == typeof(TerrariaPlugin))
                    {
                        try
                        {
                            Plugins.Add((TerrariaPlugin)Activator.CreateInstance(t, main));
                        }
                        catch (Exception e)
                        {
                            File.WriteAllText(f.Name + "_ErrorLog.txt", "Exception while trying to load: " + f.Name + Environment.NewLine + e.Message + Environment.NewLine + "Stack trace: " + Environment.NewLine + e.StackTrace);
                            error = true;
                        }
                    }
                }
            }

            if(error)
                MessageBox.Show("There were errors while loading the mods, check the error logs for more details", "Terraria API", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void DeInitialize()
        {
            foreach (var p in Plugins)
                p.Dispose();
        }
    }
}
