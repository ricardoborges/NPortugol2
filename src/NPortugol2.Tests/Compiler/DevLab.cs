using Antlr.Runtime;
using Antlr.Runtime.Tree;
using NPortugol;
using NUnit.Framework;

namespace NPortugol2.Tests.Compiler
{
    [TestFixture]
    public class DevLab
    {
        [Test]
        public void Do()
        {
            var function = @"funcao principal() fim";

            var tree = Compile(function);
        }

        public CommonTree Compile(string function)
        {
            var input = new ANTLRStringStream(function);
            var lexer = new NPortugolLexer(input);
            var tokens = new CommonTokenStream(lexer);
            var parser = new NPortugolParser(tokens);

            var ast = parser.script();
            return (CommonTree)ast.Tree;
        }
    }
}