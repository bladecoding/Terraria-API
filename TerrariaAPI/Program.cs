using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.CSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using TerrariaAPI.Hooks;

namespace TerrariaAPI
{
    public static class Program
    {
        public static readonly Version ApiVersion = new Version(1, 2, 0, 0);

#if SERVER
        public const string PluginsPath = "ServerPlugins";
#else
        public const string PluginsPath = "Plugins";
#endif

        public const string PluginSettingsPath = PluginsPath + "\\Settings";

#if CLIENT
        public static XNAConsole XNAConsole { get; private set; }
#endif

        private static List<PluginContainer> Plugins = new List<PluginContainer>();
        private static List<string> FailedPlugins = new List<string>();
        private static Assembly[] Assemblies;
        private static Main Game;

        public static void Initialize(Main main)
        {
            Game = main;

            Application.EnableVisualStyles();

            if (!Directory.Exists(PluginsPath))
            {
                Directory.CreateDirectory(PluginsPath);
            }

            // Not loaded for some reason :s
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
                    File.AppendAllText("ErrorLog.txt", "Exception while trying to load: " + f.Name + Environment.NewLine + e.Message +
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
                                Plugins.Add(new PluginContainer((TerrariaPlugin)Activator.CreateInstance(t, Game), false));
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    if (e is TargetInvocationException)
                        e = ((TargetInvocationException)e).InnerException;
                    File.AppendAllText("ErrorLog.txt", "Exception while trying to load: " + f.Name + Environment.NewLine +
                        e.Message + Environment.NewLine + "Stack trace: " + Environment.NewLine + e.StackTrace + Environment.NewLine + Environment.NewLine);
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
                MessageBox.Show("Outdated plugins found. Check ErrorLog.txt for details.", "Terraria API", MessageBoxButtons.OK, MessageBoxIcon.Error);

#if CLIENT
            XNAConsole = new XNAConsole(Game);
            Game.Components.Add(XNAConsole);
#endif

            DrawHooks.EndDrawMenu += DrawHooks_EndDrawMenu;
            NetHooks.SendData += NetHooks_SendData;
            GameHooks.LoadContent += new Action<ContentManager>(GameHooks_LoadContent);
        }

        private static void GameHooks_LoadContent(ContentManager obj)
        {
#if CLIENT
            XNAConsole.LoadFont(Main.fontMouseText);
            Console.WriteLine("Testing...");
#endif
        }

        public static void DeInitialize()
        {
            foreach (var p in Plugins)
                p.DeInitialize();

            foreach (var p in Plugins)
                p.Dispose();
        }

        private static Assembly Compile(string name, string data, bool addfail = true)
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

        private static void NetHooks_SendData(SendDataEventArgs e)
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

        private static void DrawHooks_EndDrawMenu(SpriteBatch obj)
        {
            DrawingHelper.DrawTextWithShadow(obj, string.Format("TerrariaAPI v{0}", ApiVersion), new Vector2(10, 6), Main.fontMouseText, Color.White, Color.Black);

            int pos = 1;

            foreach (var p in Plugins)
            {
                if (pos > 34)
                    break;

                string str = string.Format("{0} v{1} ({2})", p.Plugin.Name, p.Plugin.Version, p.Plugin.Author);
                DrawingHelper.DrawTextWithShadow(obj, str, new Vector2(10, 6 + (24 * pos)), Main.fontMouseText, p.Initialized ? Color.White : Color.Yellow, Color.Black);
                pos++;
            }

            foreach (var f in FailedPlugins)
            {
                if (pos > 34)
                    break;

                DrawingHelper.DrawTextWithShadow(obj, f, new Vector2(10, 6 + (24 * pos)), Main.fontMouseText, Color.Red, Color.Black);
                pos++;
            }
        }
    }
}