using System.Reflection.Emit;

namespace NPortugol2.Lang.Instructions
{
    public class Add: Instruction
    {
        public Add()
        {
            opCode = OpCodes.Add;
        }
    }
}