using System.Reflection.Emit;

namespace NPortugol2.Lang.Instructions
{
    public class Instruction
    {
        protected OpCode opCode;

        public OpCode OpCode
        {
            get { return opCode; }
        }

        public string Name
        {
            get { return OpCode.Name; }
        }
    }
}