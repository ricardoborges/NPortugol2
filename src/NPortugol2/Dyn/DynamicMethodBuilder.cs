using System.Reflection.Emit;
using NPortugol2.Lang.Instructions;
using NPortugol2.VirtualMachine;

namespace NPortugol2.Dyn
{
    public class DynamicMethodBuilder
    {
        private Module module;
        private Function target;

        public DynamicMethodBuilder(Module module)
        {
            this.module = module;
        }

        public DynamicMethod BuildFor(string functionName)
        {
            target = module.Functions[functionName];

            var dm = new DynamicMethod(functionName, target.ReturningType, target.ParametersType);

            GenerateILCode(dm.GetILGenerator());
            
            return dm;            
        }

        private void GenerateILCode(ILGenerator gen)
        {
            foreach (var inst in target.Instructions)
            {
                switch (inst.Name)
                {
                    case "ldc.i4":
                        gen.Emit(inst.OpCode, ((Ldint)inst).Value);
                        break;

                    case "ldc.r4":
                        gen.Emit(inst.OpCode, ((Ldf)inst).Value);
                        break;
                    case "add":
                    case "ret":
                        gen.Emit(inst.OpCode);
                        break;
                }
            }            
        }
    }
}