using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Microsoft.CSharp;
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
            {
                Directory.CreateDirectory(PluginsPath);
            }

            //Not loaded for some reason :s
            Assembly.Load("System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            bool error = false;
            foreach (var f in new DirectoryInfo(PluginsPath).GetFiles("*.dll"))
            {
                try
                {
                    var asm = Assembly.LoadFile(f.FullName);
                    foreach (var t in asm.GetTypes())
                    {
                        if (t.BaseType == typeof (TerrariaPlugin))
                        {

                            Plugins.Add((TerrariaPlugin) Activator.CreateInstance(t, main));

                        }
                    }
                }
                catch (Exception e)
                {
                    if (e is TargetInvocationException)
                        e = ((TargetInvocationException) e).InnerException;
                    File.AppendAllText("ErrorLog.txt",
                                       "Exception while trying to load: " + f.Name + Environment.NewLine + e.Message +
                                       Environment.NewLine + "Stack trace: " + Environment.NewLine + e.StackTrace);
                    error = true;
                }
            }


            foreach (var f in new DirectoryInfo(PluginsPath).GetFiles("*.cs"))
            {
                try
                {
                    var prov = new CSharpCodeProvider(new Dictionary<string, string>() {{"CompilerVersion", "v4.0"}});
                    var cp = new CompilerParameters();
                    cp.GenerateInMemory = true;
                    cp.GenerateExecutable = false;
                    cp.CompilerOptions = "/d:TERRARIA_API /unsafe";

                    foreach (var a in assemblies)
                    {
                        if (!cp.ReferencedAssemblies.Contains(a.Location))
                            cp.ReferencedAssemblies.Add(a.Location);
                    }
                    var r = prov.CompileAssemblyFromSource(cp, File.ReadAllText(f.FullName));
                    if (r.Errors.Count > 0)
                    {
                        for (int i = 0; i < r.Errors.Count; i++)
                        {
                            File.AppendAllText("ErrorLog.txt",
                                               "Error compiling: " + f.Name + Environment.NewLine + "Line number " +
                                               r.Errors[i].Line +
                                               ", Error Number: " + r.Errors[i].ErrorNumber +
                                               ", '" + r.Errors[i].ErrorText + ";" +
                                               Environment.NewLine + Environment.NewLine);
                            error = true;
                        }
                    }
                    else
                    {
                        foreach (var t in r.CompiledAssembly.GetTypes())
                        {
                            if (t.BaseType == typeof (TerrariaPlugin))
                            {

                                Plugins.Add((TerrariaPlugin) Activator.CreateInstance(t, main));

                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    if (e is TargetInvocationException)
                        e = ((TargetInvocationException) e).InnerException;
                    File.AppendAllText("ErrorLog.txt",
                                       "Exception while trying to load: " + f.Name + Environment.NewLine +
                                       e.Message + Environment.NewLine + "Stack trace: " +
                                       Environment.NewLine + e.StackTrace +
                                       Environment.NewLine + Environment.NewLine);
                    error = true;
                }
            }
            if (error)
                MessageBox.Show("There were errors while loading the mods, check the error logs for more details",
                                "Terraria API", MessageBoxButtons.OK, MessageBoxIcon.Error);

            error = false;
            var ver = Assembly.GetExecutingAssembly().GetName().Version;
            foreach (var p in Plugins)
            {
                if (p.APIVersion.Major != ver.Major || p.APIVersion.Minor != ver.Minor)
                {
                    File.AppendAllText("ErrorLog.txt", "Outdated plugin: " + p.Name + " (" + p.GetType() + ")");
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
