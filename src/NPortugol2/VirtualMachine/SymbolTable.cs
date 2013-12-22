using System.Collections.Generic;
using NPortugol2.Core;

namespace NPortugol2.VirtualMachine
{
    public class SymbolTable
    {
        public SymbolTable()
        {
            Symbols = new Dictionary<string, Symbol>();
        }

        public void Setup(Symbol[] symbols)
        {
            Symbols = new Dictionary<string, Symbol>();

            foreach (var symbol in symbols)
            {
                Symbols.Add(symbol.Name, symbol);
            }
        }

        public Dictionary<string, Symbol> Symbols { get; set; }
    }
}