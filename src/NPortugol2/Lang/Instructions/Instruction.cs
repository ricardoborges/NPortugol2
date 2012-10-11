using NPortugol2.VirtualMachine;

namespace NPortugol2.Lang.Instructions
{
    public class Instruction
    {
        protected OpCode opCode;

        public OpCode OpCode
        {
            get { return opCode; }
        }
    }
}