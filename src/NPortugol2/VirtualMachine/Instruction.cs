using System;
using System.Reflection.Emit;

namespace NPortugol2.VirtualMachine
{
    public class Instruction
    {
        public OpCode OpCode { get; set; }

        public Type DeclaringType { get; set; }

        public bool IsLocal { get; set; }

        public string[] Operands { get; set; }

        public object Value { get; set; }

        public int IntValue
        {
            get { return int.Parse(Value.ToString()); }
        }

        public float FloatValue
        {
            get { return float.Parse(Value.ToString()); }
        }
    }
}