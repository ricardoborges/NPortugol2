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
            var module = Compile(@"funcao soma() 
                                       variavel int x=342, y=1, f, m, a=2012
                                   fim");

            Assert.AreEqual(342, module.Functions["soma"].Symbols[0].Value);
            Assert.AreEqual(1, module.Functions["soma"].Symbols[1].Value);
            Assert.AreEqual(2012, module.Functions["soma"].Symbols[4].Value);

            Assert.AreEqual("y", module.Functions["soma"].Symbols[1].Name);
            Assert.AreEqual("a", module.Functions["soma"].Symbols[4].Name);
            
            Assert.AreEqual(typeof(int), module.Functions["soma"].Symbols[1].Type);
            Assert.AreEqual(typeof(int), module.Functions["soma"].Symbols[4].Type);
        }
    }
}