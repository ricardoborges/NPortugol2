using System.Collections.Generic;
using System.Reflection.Emit;

namespace NPortugol2.Core
{
    public class Module
    {
        public Module()
        {
            Name = "Default";
            Functions = new Dictionary<string, Function>();
        }

        public string Name { get; set; }

        public Function Main { get; set; }

        public Dictionary<string, Function> Functions { get; set; }

        public Dictionary<string, DynamicMethod> Methods { get; set; }
    }
}