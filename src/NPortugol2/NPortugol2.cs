using System.Linq;
using System.Reflection.Emit;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using NPortugol2.Compiler;
using NPortugol2.Core;
using NPortugol2.Dyn;
using System.Collections.Generic;

namespace NPortugol2
{

    public class NPortugol2
    {
        /// <summary>
        /// Compila um script de funções NPortugol em DynamicMethods
        /// </summary>
		/// 
        public Module CompileScript(string script)
        {
            return BuildModule(script);
        }

        /// <summary>
        /// Compila uma função NPortugol em DynamicMethod.
        /// </summary>
        public DynamicMethod CompileMethod(string function)
        {
            var module = BuildModule(function);

            return new DynamicMethodBuilder(module).BuildFor(module.Functions.First().Value.Name);
        }

		public List<string> CompileIL (string function)
		{
			var module = BuildModule(function);

			var list = new List<string>();

			foreach(var inst in module.Functions.First().Value.Instructions)
			{
				list.Add(inst.OpCode.ToString() + " " + inst.Value);
			}

			return list;
		}

        private Module BuildModule(string function)
        {
            var input = new ANTLRStringStream(function);
            var lexer = new NPortugolLexer(input);
            var tokens = new CommonTokenStream(lexer);
            var parser = new NPortugolParser(tokens);

            var ast = parser.script();
            var tree = (CommonTree)ast.Tree;
            var nodes = new CommonTreeNodeStream(tree) { TokenStream = tokens };
            var walker = new NPortugolWalker(nodes);

            return walker.compile();
        }
    }
}