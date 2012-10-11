using System;
using System.Collections.Generic;
using NPortugol2.VirtualMachine;

namespace NPortugol2.Lang.Instructions
{
    public class Init: Instruction
    {
        public Init()
        {
            opCode = OpCode.init;

            Names = new List<string>();
        }

        public Type DeclaringType { get; set; }

        public bool IsLocal { get; set; }

        public List<string> Names { get; set; }
    }
}