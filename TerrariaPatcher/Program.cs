using System;
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
            Environment.CurrentDirectory = @"C:\Program Files (x86)\Steam\steamapps\common\terraria";
            if (File.Exists("Terraria.exe"))
            {
                string md5;
                using (var fs = new FileStream("Terraria.exe", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    md5 = MD5(fs);
                }

                if (!Directory.Exists(TmpDir))
                    Directory.CreateDirectory(TmpDir);

                string patch = DownloadPatch(md5);
                File.WriteAllText(Path.Combine(TmpDir, "Terraria.diff"), patch);

                File.WriteAllBytes(Path.Combine(TmpDir, "ildasm.exe"), Properties.Resources.ildasm);
                File.WriteAllBytes(Path.Combine(TmpDir, "IlDasmrc.dll"), Properties.Resources.IlDasmrc);
                File.WriteAllBytes(Path.Combine(TmpDir, "patch.exe"), Properties.Resources.patch);


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

                proc = new Process();
                proc.StartInfo.FileName = "apitmp/patch.exe";
                proc.StartInfo.Arguments = "-u -o NewTerraria.il -i Terraria.diff";
                proc.StartInfo.WorkingDirectory = Path.Combine(Environment.CurrentDirectory, "apitmp");
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.Start();
                proc.WaitForExit();
                Log(proc.StandardOutput.ReadToEnd());
                Log(proc.StandardError.ReadToEnd());



                proc = new Process();
                proc.StartInfo.FileName = Path.Combine(RuntimeEnvironment.GetRuntimeDirectory(), "ilasm.exe");
                proc.StartInfo.Arguments = "NewTerraria.il /quiet /output=Terraria.exe /res=Terraria.res";
                proc.StartInfo.WorkingDirectory = Path.Combine(Environment.CurrentDirectory, "apitmp");
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.Start();
                proc.WaitForExit();
                Log(proc.StandardOutput.ReadToEnd());
                Log(proc.StandardError.ReadToEnd());

                if (File.Exists(Path.Combine(TmpDir, "Terraria.exe")))
                {
                    File.Copy(Path.Combine(TmpDir, "Terraria.exe"), "Terraria.exe", true);
                    Console.WriteLine("Success");
                }
                else
                {
                    Console.WriteLine("Failed");
                }

                Directory.Delete(TmpDir, true);
            }
            else
            {
                Console.WriteLine("Terraria.exe not found");
            }
            Console.ReadLine();
        }

        static void Log(string str)
        {
            if (str.Length < 1)
                return;
            File.AppendAllText("patchlog.txt", str);
        }

        static string DownloadPatch(string md5)
        {
            return File.ReadAllText(@"C:\Program Files (x86)\Steam\steamapps\common\terraria\TerrariaAPI\Terraria\test\Terraira_88C4FEFB311D0563CBA46D1720708ADA.diff");
            //return new WebClient().DownloadString("http://shankshock.com/Terraria_" + md5 + ".diff"); ;
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