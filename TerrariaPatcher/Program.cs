using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace TerrariaPatcher
{
    class Program
    {
        static readonly string TmpDir = "apitmp";
        static void Main(string[] args)
        {
            try
            {
                if (!File.Exists("Terraria.exe"))
                {
                    Output("Terraria.exe not found");
                    Console.ReadLine();
                    return;
                }

                string md5;
                using (var fs = new FileStream("Terraria.exe", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    md5 = MD5(fs);
                }
                var terrariaver = FileVersionInfo.GetVersionInfo("Terraria.exe");
                string apitmp = Path.Combine(Environment.CurrentDirectory, "apitmp");

                Output("Downloading Patches Information");
                var patches = DownloadPatches();

                string vmd5;
                if (!patches.TryGetValue(terrariaver.ProductVersion, out vmd5))
                {
                    Output("Terraria version not supported ({0})", terrariaver.ProductVersion);
                    Console.ReadLine();
                    return;
                }

                if (vmd5 != md5)
                {
                    Output("Terraria hash mismatch (already patched?).");
                    Output("Version {0}", terrariaver.ProductVersion);
                    Output("Expected {0} got {1}", vmd5, md5);
                    Console.ReadLine();
                    return;
                }

                if (!Directory.Exists(TmpDir))
                    Directory.CreateDirectory(TmpDir);

                Output("Downloading Diff");
                string patch = DownloadPatch(md5);
                File.WriteAllText(Path.Combine(TmpDir, "Terraria.diff"), patch);

                Output("Extracting Resources");
                File.WriteAllBytes(Path.Combine(TmpDir, "ildasm.exe"), Properties.Resources.ildasm);
                File.WriteAllBytes(Path.Combine(TmpDir, "IlDasmrc.dll"), Properties.Resources.IlDasmrc);
                File.WriteAllBytes(Path.Combine(TmpDir, "patch.exe"), Properties.Resources.patch);

                Output("Running IlDasm");
                var proc = new Process();
                proc.StartInfo.FileName = "apitmp/ildasm.exe";
                proc.StartInfo.Arguments = "Terraria.exe /output:" + Path.Combine(TmpDir, "Terraria.il");
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.Start();
                proc.WaitForExit();
                Log(proc.StandardOutput.ReadToEnd());
                Log(proc.StandardError.ReadToEnd());

                Output("Running Patch");
                proc = new Process();
                proc.StartInfo.FileName = "apitmp/patch.exe";
                proc.StartInfo.Arguments = "-u -o NewTerraria.il -i Terraria.diff";
                proc.StartInfo.WorkingDirectory = apitmp;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.Start();
                proc.WaitForExit();
                Log(proc.StandardOutput.ReadToEnd());
                Log(proc.StandardError.ReadToEnd());

                if (File.Exists(Path.Combine(apitmp, "NewTerraria.il.rej")))
                    Log("\n\n[[{0}]]\n\n",File.ReadAllText(Path.Combine(apitmp, "NewTerraria.il.rej")));

                Output("Running Ilasm");
                proc = new Process();
                proc.StartInfo.FileName = Path.Combine(RuntimeEnvironment.GetRuntimeDirectory(), "ilasm.exe");
                proc.StartInfo.Arguments = "NewTerraria.il /quiet /output=Terraria.exe /res=Terraria.res";
                proc.StartInfo.WorkingDirectory = apitmp;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.Start();
                proc.WaitForExit();
                Log(proc.StandardOutput.ReadToEnd());
                Log(proc.StandardError.ReadToEnd());

                Output("Finishing");
                if (File.Exists(Path.Combine(TmpDir, "Terraria.exe")))
                {
                    File.Copy(Path.Combine(TmpDir, "Terraria.exe"), "Terraria.exe", true);
                    Output("Success");
                }
                else
                {
                    Output("Failed");
                }
            }
            catch (Exception ex)
            {
                Log(ex.ToString());
                Output("Exception: " + ex.ToString());
            }
            finally
            {
                if (Directory.Exists(TmpDir))
                    Directory.Delete(TmpDir, true);
            }

            Console.ReadLine();
        }

        static void Output(string str)
        {
            if (str.Length < 1)
                return;
            Console.WriteLine(str);
            Log(str);
        }
        static void Output(string str, params object[] objs)
        {
            Output(string.Format(str, objs));
        }

        static void Log(string str)
        {
            if (str.Length < 1)
                return;
            File.AppendAllText("patchlog.txt", str + "\n");
        }
        static void Log(string str, params object[] objs)
        {
            Log(string.Format(str, objs));
        }

        static Dictionary<string, string> DownloadPatches()
        {
            var ret = new Dictionary<string, string>();

            string patches = new WebClient().DownloadString("http://dl.dropbox.com/u/29760911/TerrariaApi/patches.txt");
            var ps = patches.Split('\n', '\r');
            foreach (var p in ps)
            {
                var kv = p.Split('|');
                if (kv.Length != 2)
                    continue;

                ret.Add(kv[0], kv[1]);
            }
            return ret;
        }

        static string DownloadPatch(string md5)
        {
            return new WebClient().DownloadString("http://dl.dropbox.com/u/29760911/TerrariaApi/Terraira_" + md5 + ".diff");
        }

        static string MD5(Stream stream)
        {
            using (var sha = MD5CryptoServiceProvider.Create())
            {
                var bytes = sha.ComputeHash(stream);
                return bytes.Aggregate("", (s, b) => s + b.ToString("X2"));
            }
        }

    }
}