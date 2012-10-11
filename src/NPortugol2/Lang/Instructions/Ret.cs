using NPortugol2.VirtualMachine;

namespace NPortugol2.Lang.Instructions
{
    public class Ret: Instruction
    {
        public Ret()
        {
            opCode = OpCode.ret;
        }
    }
}