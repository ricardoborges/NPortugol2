using NUnit.Framework;

namespace NPortugol2.Tests.Compiler
{
    [TestFixture]
    public class DeclareLocalTestCase: BaseCompilerTestCase
    {
        [Test]
        public void Should_Declare_Local_Var()
        {
            var module = Compile("funcao soma() variavel inteiro x fim");
            
            Assert.AreEqual("x", module.Functions["soma"].Locals[0].Name);
            Assert.AreEqual(typeof(int), module.Functions["soma"].Locals[0].Type);
        }

        [Test]
        public void Should_Declare_Multiple_Local_Var()
        {
            var module = Compile(@"funcao soma() 
                                       variavel inteiro x=342, y=1, f, m, a=2012
                                   fim");

            Assert.AreEqual(342, module.Functions["soma"].Locals[0].Value);
            Assert.AreEqual(1, module.Functions["soma"].Locals[1].Value);
            Assert.AreEqual(2012, module.Functions["soma"].Locals[4].Value);

            Assert.AreEqual("y", module.Functions["soma"].Locals[1].Name);
            Assert.AreEqual("a", module.Functions["soma"].Locals[4].Name);
            
            Assert.AreEqual(typeof(int), module.Functions["soma"].Locals[1].Type);
            Assert.AreEqual(typeof(int), module.Functions["soma"].Locals[4].Type);
        }
    }
}