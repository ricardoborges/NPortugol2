using NUnit.Framework;

namespace NPortugol2.Tests.Compiler
{
    [TestFixture]
    public class DeclareLocalTestCase: BaseCompilerTestCase
    {
        [Test]
        public void Should_Declare_Local_Var()
        {
            var module = Compile("funcao soma() variavel int x fim");
            
            Assert.AreEqual("x", module.Functions["soma"].Symbols[0].Name);
            Assert.AreEqual(typeof(int), module.Functions["soma"].Symbols[0].Type);
        }

        [Test]
        public void Should_Declare_Multiple_Local_Var()
        {
            var module = Compile(
@"funcao soma() 
       variavel int x, y, f, m, a 
fim");

            Assert.AreEqual("y", module.Functions["soma"].Symbols[1].Name);
            Assert.AreEqual("a", module.Functions["soma"].Symbols[4].Name);
            
            Assert.AreEqual(typeof(int), module.Functions["soma"].Symbols[1].Type);
            Assert.AreEqual(typeof(int), module.Functions["soma"].Symbols[4].Type);
        }
    }
}