using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using NPortugol2.Core;
using NPortugol2.Dyn;

namespace NPortugol2.Compiler
{
    public class NPCompiler
    {
        /// <summary>
        /// Compiles the NPortugol function into DynamicMethod.
        /// </summary>
        public DynamicMethod CompileMethod(string function)
        {
            var module = BuildModule(function);

            return new DynamicMethodBuilder(module).BuildFor(module.Functions.First().Value.Name);
        }

        /// <summary>
        /// Compiles the NPortugol function into IL code.
        /// </summary>
        public List<string> CompileIL(string function)
        {
            var module = BuildModule(function);

            return module.Functions.First().Value.
                Instructions.Select(inst => inst.ToString()).ToList();
        }

        /// <summary>
        /// Compiles the NPortugol script
        /// </summary>
        public Module CompileScript(string script)
        {
            return BuildModule(script);
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