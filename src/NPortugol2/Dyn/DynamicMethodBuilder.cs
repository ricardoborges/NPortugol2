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
                switch (inst.OpCode)
                {
                    case VirtualMachine.OpCode.ldint:
                        gen.Emit(OpCodes.Ldc_I4, ((Ldint)inst).Value);
                        break;

                    case VirtualMachine.OpCode.ldf:
                        gen.Emit(OpCodes.Ldc_R4, ((Ldf)inst).Value);
                        break;
                    case VirtualMachine.OpCode.add:
                        gen.Emit(OpCodes.Add);
                        break;
                    case VirtualMachine.OpCode.ret:
                        gen.Emit(OpCodes.Ret);
                        break;
                }
            }            
        }
    }
}