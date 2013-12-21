using System;
using NPortugol2.Core;

namespace NPortugol2.VirtualMachine
{
    public class Call
    {
        public Function Function { get; set; }

        public SymbolTable Locals { get; set; } 

        public Guid ID { get; set; }

        public int IP { get; set; }

        public Symbol Result { get; set; }

        public Call(Function function)
        {
            Function = function;
            
            ID = Guid.NewGuid();
            
            Locals = new SymbolTable();
            
            if (function.Symbols !=null)
                Locals.Setup(function.Symbols);
        }

        public bool HasNext()
        {
            return Function.Instructions.Length > IP;
        }

        public Instruction Next()
        {
            return Function.Instructions[IP++];
        }
    }
}