using System;
using System.Linq;

namespace NPortugol2.Core
{
    public class Function
    {
        public string Name { get; set; }

        public Type ReturningType { get; set; }

        public FunctionParam[] Params { get; set; }

        public Instruction[] Instructions { get; set; }

        public Symbol[] Symbols { get; set; }

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
}