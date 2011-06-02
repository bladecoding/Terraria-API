using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.CSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using TerrariaAPI.Hooks;

namespace TerrariaAPI
{
    public static class Program
    {
        public static readonly Version ApiVersion = new Version(1, 1, 0, 2);
#if SERVER
        static readonly string PluginsPath = "serverplugins";
#else
        static readonly string PluginsPath = "plugins";
#endif

        static List<PluginContainer> Plugins = new List<PluginContainer>();
        static List<string> FailedPlugins = new List<string>();
        static Assembly[] Assemblies;
        static Main Game;

        public static void Initialize(Main main)
        {
            Game = main;

            if (!Directory.Exists(PluginsPath))
            {
                Directory.CreateDirectory(PluginsPath);
            }

            //Not loaded for some reason :s
            Assembly.Load("System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
            Assemblies = AppDomain.CurrentDomain.GetAssemblies();

            bool error = false;
            foreach (var f in new DirectoryInfo(PluginsPath).GetFiles("*.dll"))
            {
                try
                {
                    var asm = Assembly.Load(File.ReadAllBytes(f.FullName));
                    foreach (var t in asm.GetTypes())
                    {
                        if (t.BaseType == typeof(TerrariaPlugin))
                        {
                            Plugins.Add(new PluginContainer((TerrariaPlugin)Activator.CreateInstance(t, main)));
                        }
                    }
                }
                catch (Exception e)
                {
                    if (e is TargetInvocationException)
                        e = ((TargetInvocationException)e).InnerException;
                    File.AppendAllText("ErrorLog.txt",
                                       "Exception while trying to load: " + f.Name + Environment.NewLine + e.Message +
                                       Environment.NewLine + "Stack trace: " + Environment.NewLine + e.StackTrace);
                    FailedPlugins.Add(f.Name);
                    error = true;
                }
            }

            foreach (var f in new DirectoryInfo(PluginsPath).GetFiles("*.cs"))
            {
                try
                {
                    var asm = Compile(f.Name, File.ReadAllText(f.FullName));
                    if (asm != null)
                    {
                        foreach (var t in asm.GetTypes())
                        {
                            if (t.BaseType == typeof(TerrariaPlugin))
                            {
                                Plugins.Add(new PluginContainer((TerrariaPlugin)Activator.CreateInstance(t, Game),
                                                                false));
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    if (e is TargetInvocationException)
                        e = ((TargetInvocationException)e).InnerException;
                    File.AppendAllText("ErrorLog.txt",
                                       "Exception while trying to load: " + f.Name + Environment.NewLine +
                                       e.Message + Environment.NewLine + "Stack trace: " +
                                       Environment.NewLine + e.StackTrace +
                                       Environment.NewLine + Environment.NewLine);
                    FailedPlugins.Add(f.Name);
                    error = true;
                }
            }
            if (error)
                MessageBox.Show("There were errors while loading the mods, check the error logs for more details",
                                "Terraria API", MessageBoxButtons.OK, MessageBoxIcon.Error);

            error = false;
            foreach (var p in Plugins)
            {
                if (p.Plugin.APIVersion.Major != ApiVersion.Major || p.Plugin.APIVersion.Minor != ApiVersion.Minor)
                {
                    File.AppendAllText("ErrorLog.txt", "Outdated plugin: " + p.Plugin.Name + " (" + p.GetType() + ")");
                    error = true;
                }
                else
                {
                    p.Initialize();
                }
            }
            if (error)
                MessageBox.Show("Outdated plugins found. Check ErrorLog.txt for details.",
                                "Terraria API", MessageBoxButtons.OK, MessageBoxIcon.Error);

            DrawHooks.OnEndDrawMenu += DrawHooks_OnEndDrawMenu;
            NetHooks.OnPreSendData += NetHooks_OnPreSendData;
        }

        static Assembly Compile(string name, string data, bool addfail = true)
        {
            var prov = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v4.0" } });
            var cp = new CompilerParameters();
            cp.GenerateInMemory = true;
            cp.GenerateExecutable = false;
            cp.CompilerOptions = "/d:TERRARIA_API /unsafe";

            foreach (var a in Assemblies)
            {
                if (!cp.ReferencedAssemblies.Contains(a.Location))
                    cp.ReferencedAssemblies.Add(a.Location);
            }
            var r = prov.CompileAssemblyFromSource(cp, data);
            if (r.Errors.Count > 0)
            {
                for (int i = 0; i < r.Errors.Count; i++)
                {
                    File.AppendAllText("ErrorLog.txt",
                                       "Error compiling: " + name + Environment.NewLine + "Line number " +
                                       r.Errors[i].Line +
                                       ", Error Number: " + r.Errors[i].ErrorNumber +
                                       ", '" + r.Errors[i].ErrorText + ";" +
                                       Environment.NewLine + Environment.NewLine);
                    if (addfail)
                        FailedPlugins.Add(name);
                }
                return null;
            }
            return r.CompiledAssembly;
        }

        private static void NetHooks_OnPreSendData(SendDataEventArgs e)
        {
            if (Main.netMode != 1)
                return;

            if (e.msgType != 0x19)
                return;

            if (!e.text.StartsWith("/reload"))
                return;

            Main.NewText("Reloading plugins");

            for (int i = Plugins.Count - 1; i >= 0; i--)
            {
                if (!Plugins[i].Dll)
                {
                    Plugins[i].DeInitialize();
                    Plugins[i].Dispose();
                    Plugins.RemoveAt(i);
                }
            }

            foreach (var f in new DirectoryInfo(PluginsPath).GetFiles("*.cs"))
            {
                try
                {
                    var asm = Compile(f.Name, File.ReadAllText(f.FullName));
                    if (asm != null)
                    {
                        foreach (var t in asm.GetTypes())
                        {
                            if (t.BaseType == typeof(TerrariaPlugin))
                            {
                                Plugins.Add(new PluginContainer((TerrariaPlugin)Activator.CreateInstance(t, Game), false));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex is TargetInvocationException)
                        ex = ((TargetInvocationException)ex).InnerException;
                    File.AppendAllText("ErrorLog.txt",
                                       "Exception while trying to load: " + f.Name + Environment.NewLine +
                                       ex.Message + Environment.NewLine + "Stack trace: " +
                                       Environment.NewLine + ex.StackTrace +
                                       Environment.NewLine + Environment.NewLine);
                    FailedPlugins.Add(f.Name);
                }
            }

            for (int i = 0; i < Plugins.Count; i++)
            {
                if (!Plugins[i].Dll)
                    Plugins[i].Initialize();
            }
        }

        private static void DrawHooks_OnEndDrawMenu(SpriteBatch obj)
        {
            DrawFancyText(obj, string.Format("TerrariaAPI v{0}", ApiVersion), new Vector2(10, 6), Color.White);

            int pos = 1;
            foreach (var p in Plugins)
            {
                if (pos > 34)
                    break;
                string str = string.Format("{0} v{1} ({2})", p.Plugin.Name, p.Plugin.Version, p.Plugin.Author);
                DrawFancyText(obj, str, new Vector2(10, 6 + (24 * pos)), p.Initialized ? Color.White : Color.Yellow);
                pos++;
            }
            foreach (var f in FailedPlugins)
            {
                if (pos > 34)
                    break;
                DrawFancyText(obj, f, new Vector2(10, 6 + (24 * pos)), Color.Red);
                pos++;
            }
        }

        private static void DrawFancyText(SpriteBatch sb, string text, Vector2 position, Color color)
        {
            for (int n = 0x0; n < 0x5; n++)
            {
                Color color9 = Color.Black;
                if (n == 0x4)
                {
                    color9 = color;
                    color9.R = (byte)((0xff + color9.R) / 0x2);
                    color9.G = (byte)((0xff + color9.G) / 0x2);
                    color9.B = (byte)((0xff + color9.B) / 0x2);
                }
                color9.A = (byte)(color9.A * 0.3f);
                float offsetx = 0x0;
                float offsety = 0x0;
                switch (n)
                {
                    case 0x0:
                        offsetx = -2;
                        break;

                    case 0x1:
                        offsetx = 0x2;
                        break;

                    case 0x2:
                        offsety = -2;
                        break;

                    case 0x3:
                        offsety = 0x2;
                        break;
                }
                Vector2 vector7 = Main.fontMouseText.MeasureString(text);
                vector7.X *= 0.5f;
                vector7.Y *= 0.5f;
                sb.DrawString(Main.fontMouseText, text, new Vector2(position.X + offsetx, position.Y + offsety), color9);
            }
        }

        public static void DeInitialize()
        {
            foreach (var p in Plugins)
                p.DeInitialize();
            foreach (var p in Plugins)
                p.Dispose();
        }
    }
}