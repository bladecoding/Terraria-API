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
        public static readonly Version ApiVersion = new Version(1, 4, 0, 0);

#if SERVER
        public const string PluginsPath = "ServerPlugins";
#else
        public const string PluginsPath = "Plugins";
#endif

        public const string PluginSettingsPath = PluginsPath + "\\Settings";

#if CLIENT
        public static XNAConsole XNAConsole { get; private set; }
#endif

        public static List<PluginContainer> Plugins = new List<PluginContainer>();
        private static List<string> FailedPlugins = new List<string>();
        static Dictionary<string, Assembly> LoadedAssemblies = new Dictionary<string, Assembly>();
        private static Assembly[] Assemblies;
        private static Main Game;

        public static void Initialize(Main main)
        {
            Game = main;

#if CLIENT
            XNAConsole = new XNAConsole(Game);
            Game.Components.Add(XNAConsole);
#endif

            Application.EnableVisualStyles();

            if (!Directory.Exists(PluginsPath))
            {
                Directory.CreateDirectory(PluginsPath);
            }

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            // Not loaded for some reason :s
            Assembly.Load("System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
            Assemblies = AppDomain.CurrentDomain.GetAssemblies();

            bool error = false;
            foreach (var f in new DirectoryInfo(PluginsPath).GetFiles("*.dll"))
            {
                try
                {
                    string name = Path.GetFileNameWithoutExtension(f.Name);
                    Assembly asm;
                    if (!LoadedAssemblies.TryGetValue(name, out asm))
                    {
                        asm = Assembly.Load(File.ReadAllBytes(f.FullName));
                        LoadedAssemblies.Add(name, asm);
                    }

                    foreach (var t in asm.GetTypes())
                    {
                        if (t.BaseType == typeof(TerrariaPlugin))
                        {
                            if (Compatible(t))
                            {
                                Plugins.Add(new PluginContainer((TerrariaPlugin)Activator.CreateInstance(t, Game)));
                            }
                            else
                            {
                                File.AppendAllText("ErrorLog.txt", "Outdated plugin: " + f.Name + " (" + t.GetType() + ")");
                                error = true;
                            }
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
                                if (Compatible(t))
                                {
                                    Plugins.Add(new PluginContainer((TerrariaPlugin)Activator.CreateInstance(t, Game), false));
                                }
                                else
                                {
                                    File.AppendAllText("ErrorLog.txt", "Outdated plugin: " + f.Name + " (" + t.GetType() + ")");
                                    error = true;
                                }
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

            //Sort the plugins so the ones with higher order get initialized first.
            Plugins.Sort((pc1, pc2) => pc1.Plugin.Order.CompareTo(pc2.Plugin.Order));

            foreach (PluginContainer p in Plugins)
            {
                p.Initialize();
                string str = string.Format("{0} v{1} ({2}) initiated.", p.Plugin.Name, p.Plugin.Version, p.Plugin.Author);
                Console.WriteLine(str);
            }

            DrawHooks.EndDrawMenu += DrawHooks_EndDrawMenu;
            ClientHooks.Chat += ClientHooks_Chat;
            GameHooks.LoadContent += GameHooks_LoadContent;
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string name = args.Name.Split(',')[0];
            string path = Path.Combine(PluginsPath, name + ".dll");
            try
            {
                if (File.Exists(path))
                {
                    Assembly asm;
                    if (!LoadedAssemblies.TryGetValue(name, out asm))
                    {
                        asm = Assembly.Load(File.ReadAllBytes(path));
                        LoadedAssemblies.Add(name, asm);
                    }
                    return asm;
                }
            }
            catch (Exception e)
            {
                File.AppendAllText("ErrorLog.txt", "Exception while trying to load: " + name + Environment.NewLine + e.Message +
                        Environment.NewLine + "Stack trace: " + Environment.NewLine + e.StackTrace);
            }
            return null;
        }

        private static bool Compatible(Type type)
        {
            var objs = type.GetCustomAttributes(typeof(APIVersionAttribute), false);
            if (objs.Length != 1)
                return false;

            var apiver = (APIVersionAttribute)objs[0];
            var ver = apiver.ApiVersion;

            return ver.Major == ApiVersion.Major && ver.Minor == ApiVersion.Minor;
        }

        private static void GameHooks_LoadContent(ContentManager obj)
        {
#if CLIENT
            XNAConsole.LoadFont(Main.fontMouseText);
#endif
        }

        public static void DeInitialize()
        {
            foreach (var p in Plugins)
                p.DeInitialize();

            foreach (var p in Plugins)
                p.Dispose();

            DrawHooks.EndDrawMenu -= DrawHooks_EndDrawMenu;
            ClientHooks.Chat -= ClientHooks_Chat;
            GameHooks.LoadContent -= GameHooks_LoadContent;
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

        private static void ClientHooks_Chat(ref string msg, HandledEventArgs e)
        {
            if (Main.netMode != 1)
                return;

            if (!msg.StartsWith("/preload"))
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
                                if (Compatible(t))
                                {
                                    Plugins.Add(new PluginContainer((TerrariaPlugin)Activator.CreateInstance(t, Game)));
                                }
                                else
                                {
                                    File.AppendAllText("ErrorLog.txt", "Outdated plugin: " + f.Name + " (" + t.GetType() + ")");
                                }
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