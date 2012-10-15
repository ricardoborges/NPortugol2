using Antlr.Runtime;
using Antlr.Runtime.Tree;
using NPortugol2.Compiler;
using NPortugol2.Core;

namespace NPortugol2.Tests.Compiler
{
    public class BaseCompilerTestCase
    {
        public Module Compile(string function)
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