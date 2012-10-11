using System;
using NPortugol2.Lang.Instructions;

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
            switch (instruction.OpCode)
            {
                case OpCode.init: ProcessInit((Init) instruction); break;
                case OpCode.ldint: ProcessLdint((Ldint) instruction); break;
                case OpCode.ldf: ProcessLdf((Ldf)instruction); break;
                case OpCode.add: ProcessAdd((Add)instruction); break;
            }
        }

        private void ProcessAdd(Add add)
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

        private void ProcessLdf(Ldf ldf)
        {
            Process.EvalStack.Push(new Symbol { Name = Guid.NewGuid().ToString(), Type = typeof(float), Value = ldf.Value });            
        }

        private void ProcessLdint(Ldint ldint)
        {
            Process.EvalStack.Push(new Symbol{Name = Guid.NewGuid().ToString(), Type = typeof(int), Value = ldint.Value});
        }

        private void ProcessInit(Init init)
        {
            var table = init.IsLocal ? Process.CallStack.Peek().Function.Locals : Process.Globals;

            foreach (var name in init.Names)
            {
                var symbol = new Symbol {Name = name, Type = init.DeclaringType};

                table.Symbols.Add(name, symbol);
            }
        }
    }
}