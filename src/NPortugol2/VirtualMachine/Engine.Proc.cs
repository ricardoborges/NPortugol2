using System;
using NPortugol2.Core;

namespace NPortugol2.VirtualMachine
{
    public enum Operation
    {
        Add,
        Sub,
        Div,
        Mul
    }

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
                case "add": ProcessArithmetic(Operation.Add); break;
                case "sub": ProcessArithmetic(Operation.Sub); break;
                case "mul": ProcessArithmetic(Operation.Mul); break;
                case "div": ProcessArithmetic(Operation.Div); break;
                case "ret": ProcessRet(); break;
            }
        }

        private void ProcessRet()
        {
            if (Process.EvalStack.Count != 1)
                throw new Exception("A pilha de operandos possui mais de um elemento. Não foi possível determinar o retorno da função.");

            Process.CallStack.Peek().Result = Process.EvalStack.Pop();
        }

        private void ProcessArithmetic(Operation op)
        {
            var first = Process.EvalStack.Pop();
            var second = Process.EvalStack.Pop();

            var type = first.Type == second.Type ? first.Type : typeof(float);

            var result = new Symbol { Name = Guid.NewGuid().ToString(), Type = type};

            switch (op)
            {
                case Operation.Add:
                    result.Value = first.Value + second.Value;
                    break;
                case Operation.Sub:
                    result.Value = first.Value - second.Value;
                    break;
                case Operation.Mul:
                    result.Value = first.Value * second.Value;
                    break;
                case Operation.Div:
                    result.Value = first.Value / second.Value;
                    break;
            }

            Process.EvalStack.Push(result);
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