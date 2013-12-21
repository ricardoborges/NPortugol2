using System;
using System.Linq;

namespace NPortugol2.Core
{
    public class Function
    {
        public string Name { get; set; }

        public Type ReturningType { get; set; }

        public FunctionArg[] Args { get; set; }

        public Instruction[] Instructions { get; set; }

        public Symbol[] Symbols { get; set; }

        public Type[] ParametersType
        {
            get { return Args != null ? Args.Select(param => param.Type).ToArray() : null; }
        }

        public int IndexOf(string symbol)
        {
            return Array.FindIndex(Symbols, x => x.Name == symbol);
        }
    }

    public class FunctionArg
    {
        public string Name { get; set; }

        public object Value { get; set; }

        public Type Type { get; set; }
    }
}