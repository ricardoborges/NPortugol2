using NPortugol2.VirtualMachine;

namespace NPortugol2.Lang.Instructions
{
    public class Ldint: Instruction
    {
        public Ldint()
        {
            opCode = OpCode.ldint;
        }

        public int Value { get; set; }
    }
}