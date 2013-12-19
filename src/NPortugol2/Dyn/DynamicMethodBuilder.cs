using System.Collections.Generic;
using System.Reflection.Emit;
using NPortugol2.Core;

namespace NPortugol2.Dyn
{
    public class DynamicMethodBuilder
    {
        private readonly Module module;
        private Function target;

        public DynamicMethodBuilder(Module module)
        {
            this.module = module;
        }

        public DynamicMethod BuildFor (string functionName)
		{
			target = module.Functions [functionName];

			var dm = new DynamicMethod (functionName, target.ReturningType, target.ArgsType);

            var gen = dm.GetILGenerator();

			foreach (var symbol in target.Locals) 
			{
				gen.DeclareLocal(symbol.Type);	
			}

            GenerateILCode(gen);
            
            return dm;            
        }

        private void GenerateILCode(ILGenerator gen)
        {
            if (target.Instructions == null) return;
            
            foreach (var inst in target.Instructions)
            {
                switch (inst.OpCode.Name)
                {
					case "ldarg":
					case "ldloc":
					case "stloc":
                    case "ldc.i4":
                        gen.Emit(inst.OpCode, inst.IntValue);
                        break;

                    case "ldc.r4":
                        gen.Emit(inst.OpCode, (inst).FloatValue);
                        break;

                    case "ldstr":
                        gen.Emit(inst.OpCode, inst.Value.ToString());
                        break;

                    case "add":
                    case "mul":
                    case "div":
                    case "sub":
                    case "ret":
                        gen.Emit(inst.OpCode);
                        break;
                }
            }            
        }
    }
}