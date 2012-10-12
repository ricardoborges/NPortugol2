using System.Reflection.Emit;

namespace NPortugol2.Lang.Instructions
{
    public class Ldint: Instruction
    {
        public Ldint()
        {
            opCode = OpCodes.Ldc_I4;
        }

        public int Value { get; set; }
    }
}