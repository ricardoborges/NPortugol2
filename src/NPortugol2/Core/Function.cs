using System;
using System.Linq;

namespace NPortugol2.Core
{
    public class Function
    {
        public string Name { get; set; }

        public Type ReturningType { get; set; }

		public Symbol[] Args { get; set; }

        public Instruction[] Instructions { get; set; }

        public Symbol[] Locals { get; set; }

        public Type[] ArgsType
        {
            get { return Args != null ? Args.Select(param => param.Type).ToArray() : null; }
        }

        public int IndexOf(string symbol)
        {
            return Array.FindIndex(Locals, x => x.Name == symbol);
        }
    }
}