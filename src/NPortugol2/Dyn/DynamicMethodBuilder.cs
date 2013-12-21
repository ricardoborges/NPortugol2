using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using NPortugol2.Core;

namespace NPortugol2.Dyn
{
    public class DynamicMethodBuilder
    {
        private readonly Module module;
        private Function target;

        private Dictionary<string, int> localsMap;

        public DynamicMethodBuilder(Module module)
        {
            this.module = module;

            localsMap = new Dictionary<string, int>();
        }

        public DynamicMethod BuildFor(string functionName)
        {
            target = module.Functions[functionName];

            var dm = new DynamicMethod(functionName, target.ReturningType, target.ParametersType);

            var gen = dm.GetILGenerator();

            for (int i = 0; i < target.Symbols.Length; i++)
            {
                gen.DeclareLocal(target.Symbols[i].Type);

                localsMap.Add(target.Symbols[i].Name, i);
            }

            var prev = target.Symbols.Length;

            for (int i = 0; i < target.Args.Length; i++)
            {
                gen.DeclareLocal(target.Args[i].Type);
                localsMap.Add(target.Args[i].Name, i + prev);
            }

            SetArgsValues(gen);

            SetLocalsValues(gen);

            GenerateILCode(gen);
            
            return dm;            
        }

        private void SetArgsValues(ILGenerator gen)
        {
            for (var i = 0; i < target.Args.Length; i++)
            {
                LoadArgValue(gen, target.Args[i], i);

                gen.Emit(OpCodes.Stloc, localsMap[target.Args[i].Name]);
            }
        }

        private void LoadArgValue(ILGenerator gen, FunctionArg parameter, int index)
        {
            switch (parameter.Type.Name)
            {
                case "Int32":
                    gen.Emit(OpCodes.Ldarg, index);

                    break;
            }
        }

        private void SetLocalsValues(ILGenerator gen)
        {
            foreach (var symbol in target.Symbols)
            {
                LoadSymbolValue(gen, symbol);

                gen.Emit(OpCodes.Stloc, localsMap[symbol.Name]);
            }
        }

        private void LoadSymbolValue(ILGenerator gen, Symbol symbol)
        {
            switch (symbol.Type.Name)
            {
                case "Int32":
                    gen.Emit(OpCodes.Ldc_I4, (int)symbol.Value);
                    break;
            }
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
                    case "ldstr":
                        gen.Emit(inst.OpCode, inst.Value.ToString());
                        break;
                    case "ldloc":
                        gen.Emit(inst.OpCode, localsMap[inst.Value.ToString()]);
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