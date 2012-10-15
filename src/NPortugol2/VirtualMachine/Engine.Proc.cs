using System;
using NPortugol2.Core;

namespace NPortugol2.VirtualMachine
{
    public partial class Engine
    {
        private void ExecuteInstruction()
        {
            var instruction = Process.Next();

            ExecuteInstruction(instruction);
            
            Process.FigureOutCompleted();
        }

        private void ExecuteInstruction(Instruction instruction)
        {
            switch (instruction.OpCode.Name)
            {
                case "ldc.i4": ProcessLdcI4(instruction); break;
                case "ldc.r4": ProcessLdcR4(instruction); break;
                case "add": ProcessAdd(); break;
            }
        }

        private void ProcessAdd()
        {
            bool isInt;

            var first = Process.EvalStack.Pop();
            var second = Process.EvalStack.Pop();

            isInt = first.Type == typeof(int) || second.Type == typeof(int);

            if (isInt)
            {
                var vfirst = (int)first.Value;
                var vsecond = (int)second.Value;

                Process.EvalStack.Push(new Symbol { Name = Guid.NewGuid().ToString(), Type = typeof(int), Value = vfirst + vsecond });
            }
            else
            {
                var vfirst = (float)first.Value;
                var vsecond = (float)second.Value;

                Process.EvalStack.Push(new Symbol { Name = Guid.NewGuid().ToString(), Type = typeof(float), Value = vfirst + vsecond });
            }
        }

        private void ProcessLdcR4(Instruction inst)
        {
            Process.EvalStack.Push(new Symbol { Name = Guid.NewGuid().ToString(), Type = typeof(float), Value = inst.Value });            
        }

        private void ProcessLdcI4(Instruction inst)
        {
            Process.EvalStack.Push(new Symbol{Name = Guid.NewGuid().ToString(), Type = typeof(int), Value = inst.Value});
        }

        /*
        private void ProcessInit(Instruction inst)
        {
            var table = inst.IsLocal ? Process.CallStack.Peek().Locals : Process.Globals;

            foreach (var name in inst.Operands)
            {
                var symbol = new Symbol {Name = name, Type = inst.DeclaringType};

                table.Symbols.Add(name, symbol);
            }
        }
        */
    }
}