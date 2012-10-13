using System;
using System.Collections.Generic;

namespace NPortugol2.VirtualMachine
{
    public class SymbolTable
    {
        public SymbolTable()
        {
            Symbols = new Dictionary<string, Symbol>();
        }

        public SymbolTable(Symbol[] symbols)
        {
            Symbols = new Dictionary<string, Symbol>();

            if (symbols == null) return;

            foreach (var symbol in symbols)
            {
                Symbols.Add(symbol.Name, symbol);
            }
        }

        public Dictionary<string, Symbol> Symbols { get; set; }
    }

    public class Symbol
    {
        public string Name { get; set; }

        public Type Type { get; set; }

        public object Value { get; set; }
    }

}