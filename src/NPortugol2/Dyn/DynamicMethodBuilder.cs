﻿using System.Reflection;
using System.Reflection.Emit;
using NPortugol2.VirtualMachine;
using Module = NPortugol2.VirtualMachine.Module;

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

        public DynamicMethod BuildFor(string functionName)
        {
            target = module.Functions[functionName];

            var dm = new DynamicMethod(functionName, target.ReturningType, target.ParametersType);
            
            GenerateILCode(dm.GetILGenerator());
            
            return dm;            
        }

        private void GenerateILCode(ILGenerator gen)
        {
            if (target.Instructions == null) return;
            
            foreach (var inst in target.Instructions)
            {
                switch (inst.OpCode.Name)
                {
                    case "ldc.i4":
                        gen.Emit(inst.OpCode, (inst).IntValue);
                        break;

                    case "ldc.r4":
                        gen.Emit(inst.OpCode, (inst).FloatValue);
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