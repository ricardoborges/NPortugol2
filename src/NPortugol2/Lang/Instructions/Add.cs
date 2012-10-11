using NPortugol2.VirtualMachine;

namespace NPortugol2.Lang.Instructions
{
    public class Add: Instruction
    {
        public Add()
        {
            opCode = OpCode.add;
        }
    }
}