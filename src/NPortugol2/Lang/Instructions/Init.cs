using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace NPortugol2.Lang.Instructions
{
    public class Init: Instruction
    {
        public Init()
        {
            opCode = OpCodes.Initblk;

            Names = new List<string>();
        }

        public Type DeclaringType { get; set; }

        public bool IsLocal { get; set; }

        public List<string> Names { get; set; }
    }
}