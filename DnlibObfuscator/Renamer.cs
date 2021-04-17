using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace DnlibObfuscator
{
    class Renamer
    {
        public static Random random = new Random();

        public static string Characters = "俺01234ム仮俺56789ム仮";
        private static string RandomString(int length, string characters)
        {
            return new string(Enumerable.Repeat(characters, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static void ExecuteRenamer(ModuleDef module)
        {
            foreach (TypeDef type in module.Types)
            {
                type.Name = RandomString(15, Characters);
                type.Namespace = RandomString(15, Characters);
                if (type.IsGlobalModuleType || type.IsRuntimeSpecialName || type.IsSpecialName || type.IsWindowsRuntime || type.IsInterface)
                {
                    continue;
                }
                foreach(MethodDef method in type.Methods)
                {
                    if (method.IsConstructor || method.IsRuntimeSpecialName || method.IsRuntime || method.IsStaticConstructor || method.IsVirtual) continue;
                    method.Name = RandomString(random.Next(80, 120), Characters);
                    foreach (var field in type.Fields)
                    {
                        field.Name = RandomString(random.Next(80, 120), Characters);
                        foreach (EventDef eventdef in type.Events)
                        {
                            eventdef.Name = RandomString(random.Next(80, 120), Characters);
                            foreach (PropertyDef property in type.Properties)
                            {
                                if (property.IsRuntimeSpecialName) continue;
                                property.Name = RandomString(random.Next(80, 120), Characters);
                            }
                        }
                    }

                    foreach(ParamDef p in method.ParamDefs)
                    {
                        p.Name = RandomString(15, Characters);
                    }
                }
            }
        }
    }
}
