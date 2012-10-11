using System;
using System.Collections.Generic;
using System.Linq;
using NPortugol2.Lang.Instructions;

namespace NPortugol2.VirtualMachine
{
    public class Function
    {
        public string Name { get; set; }

        public Type ReturningType { get; set; }

        public FunctionParam[] Params { get; set; }

        public Instruction[] Instructions { get; set; }

        public SymbolTable Locals { get; set; }

        public Type[] ParametersType
        {
            get { return Params != null ? Params.Select(param => param.Type).ToArray() : null; }
        }
    }

    public class FunctionParam
    {
        public string Name { get; set; }

        public object Value { get; set; }

        public Type Type { get; set; }
    }

    public class Call
    {
        public Function Function { get; set; }

        public Guid ID { get; set; }

        public int IP { get; set; }

        public Call(Function function)
        {
            Function = function;
            
            ID = Guid.NewGuid();
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