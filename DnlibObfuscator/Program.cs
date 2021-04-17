using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using dnlib.DotNet.Writer;

namespace DnlibObfuscator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ModuleDef mod = ModuleDefMD.Load(args[0]); //We load the dragged .exe

                Renamer.ExecuteRenamer(mod); //Execute renaming phase.

                SaveExecutable(mod, args[0]); // Save our executable
                
                Console.ReadKey();
            }
            catch
            {
                Console.WriteLine("The application is not .net or there was an error in the obfuscation. Try debugging."); //There was an error reading the .exe or in the obfuscation
                Console.ReadKey();
            }
        }

        public static void SaveExecutable(ModuleDef module, string args)
        {
            module.Write(args[0] + "-obfuscated.exe", new ModuleWriterOptions(module) //Here we save our obfuscated program
            {
                PEHeadersOptions = { NumberOfRvaAndSizes = 13 },
                Logger = DummyLogger.NoThrowInstance
            });
        }
    }
}
