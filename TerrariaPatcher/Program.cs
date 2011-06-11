using System;
using System.IO;
using Mono.Cecil;

namespace TerrariaPatcher
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (File.Exists("terraria.exe"))
            {
                var asm = AssemblyDefinition.ReadAssembly("terraria.exe");
            }
            else
            {
                Console.WriteLine("terraria.exe not found");
            }
            Console.ReadLine();
        }
    }
}