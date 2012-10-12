using System.Reflection.Emit;

namespace NPortugol2.Lang.Instructions
{
    public class Ret: Instruction
    {
        public Ret()
        {
            opCode = OpCodes.Ret;
        }
    }
}