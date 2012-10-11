using NPortugol2.VirtualMachine;

namespace NPortugol2.Lang.Instructions
{
    public class Ldf: Instruction
    {
        public Ldf()
        {
            opCode = OpCode.ldf;
        }

        public float Value { get; set; }
    }
}