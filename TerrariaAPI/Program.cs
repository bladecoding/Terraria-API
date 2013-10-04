using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.CSharp;
using Microsoft.Xna.Framework.Content;
using Terraria;
using TerrariaAPI.Hooks;
using XNAHelpers;

namespace TerrariaAPI
{
    public static class Program
    {
        public static readonly Version ApiVersion = new Version(1, 7, 0, 8);

#if SERVER
        public const string PluginsPath = "ServerPlugins";
#else
        public const string PluginsPath = "Plugins";
#endif

        public const string PluginSettingsPath = PluginsPath + "\\Settings";

#if CLIENT
        public static TerrariaConsole XNAConsole { get; private set; }
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
            XNAConsole = new TerrariaConsole(Game);
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

            foreach (FileInfo f in new DirectoryInfo(PluginsPath).GetFiles("*.dll"))
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
                                AppendLog("Outdated plugin: {0} ({1})", f.Name, t);

                                error = true;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    if (e is TargetInvocationException)
                        e = ((TargetInvocationException)e).InnerException;

                    AppendLog(f.Name, e);

                    FailedPlugins.Add(f.Name);
                    error = true;
                }
            }

            foreach (FileInfo f in new DirectoryInfo(PluginsPath).GetFiles("*.cs"))
            {
                try
                {
                    Assembly asm = Compile(f.Name, File.ReadAllText(f.FullName));

                    if (asm != null)
                    {
                        foreach (Type t in asm.GetTypes())
                        {
                            if (t.BaseType == typeof(TerrariaPlugin))
                            {
                                if (Compatible(t))
                                {
                                    Plugins.Add(new PluginContainer((TerrariaPlugin)Activator.CreateInstance(t, Game), false));
                                }
                                else
                                {
                                    AppendLog("Outdated plugin: {0} ({1})", f.Name, t);

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

                    AppendLog(f.Name, e);

                    FailedPlugins.Add(f.Name);
                    error = true;
                }
            }

            Console.WriteLine("TerrariaAPI v{0}", ApiVersion);

            if (error)
            {
#if CLIENT
                MessageBox.Show("There were errors while loading the mods, check logs.txt for more details.",
                    "Terraria API", MessageBoxButtons.OK, MessageBoxIcon.Error);
#else
                Console.WriteLine("There were errors while loading the mods, check logs.txt for more details.");
#endif
            }

#if CLIENT
            GameHooks.Update += gameTime => InputManager.Update(gameTime);
#endif


            var sortedPlugins = Plugins.OrderBy(x => x.Plugin.Order).ThenBy(x => x.Plugin.Name);

            foreach (PluginContainer p in sortedPlugins)
            {
                p.Initialize();

                Console.WriteLine("{0} v{1} ({2}) initiated.", p.Plugin.Name, p.Plugin.Version, p.Plugin.Author);
            }

            if (FailedPlugins.Count > 0)
            {
                Console.WriteLine("Plugins failed to load: " + string.Join(", ", FailedPlugins));
            }

            ClientHooks.Chat += ClientHooks_Chat;
            GameHooks.LoadContent += GameHooks_LoadContent;
        }

        private static void AppendLog(string format, params object[] args)
        {
            string text = string.Format(format, args);
            Console.WriteLine(text);
            File.AppendAllText("ErrorLog.txt", text + Environment.NewLine);
        }

        private static void AppendLog(string name, Exception e)
        {
            AppendLog("Exception while trying to load: {0}\r\n{1}\r\nStack trace:\r\n{2}\r\n", name, e.Message, e.StackTrace);
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
                AppendLog(name, e);
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
                    AppendLog("Error compiling: {0}\r\nLine number: {1}, Error number: {2}, Error text: {3}\r\n",
                        name, r.Errors[i].Line, r.Errors[i].ErrorNumber, r.Errors[i].ErrorText);

                    if (addfail)
                        FailedPlugins.Add(name);
                }
                return null;
            }
            return r.CompiledAssembly;
        }

        private static void ClientHooks_Chat(ref string msg, HandledEventArgs e)
        {
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
                        foreach (Type t in asm.GetTypes())
                        {
                            if (t.BaseType == typeof(TerrariaPlugin))
                            {
                                if (Compatible(t))
                                {
                                    Plugins.Add(new PluginContainer((TerrariaPlugin)Activator.CreateInstance(t, Game), false));
                                }
                                else
                                {
                                    AppendLog("Outdated plugin: {0} ({1})", f.Name, t);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex is TargetInvocationException)
                        ex = ((TargetInvocationException)ex).InnerException;

                    AppendLog(f.Name, e);

                    FailedPlugins.Add(f.Name);
                }
            }

            for (int i = 0; i < Plugins.Count; i++)
            {
                if (!Plugins[i].Dll)
                    Plugins[i].Initialize();
            }

            e.Handled = true;
        }
    }
}