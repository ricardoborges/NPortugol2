using System.Reflection.Emit;

namespace NPortugol2.Lang.Instructions
{
    public class Ldf: Instruction
    {
        public Ldf()
        {
            opCode = OpCodes.Ldc_R4;
        }

        public float Value { get; set; }
    }
}