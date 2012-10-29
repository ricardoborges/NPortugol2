using NUnit.Framework;

namespace NPortugol2.Tests.Compiler
{
    [TestFixture]
    public class FunctionTestCase: BaseCompilerTestCase
    {
        [Test]
        public void Should_Parse_Typed_Function()
        {
            var module = Compile("funcao inteiro principal() fim");

            Assert.AreEqual(1, module.Functions.Count);

            Assert.AreEqual(typeof(int), module.Functions["principal"].ReturningType);

            Assert.AreEqual("principal", module.Functions["principal"].Name);
        }

        [Test]
        public void Should_Parse_Non_Typed_Function()
        {
            var module = Compile("funcao principal() fim");

            Assert.AreEqual(1, module.Functions.Count);

            Assert.AreEqual(typeof(void), module.Functions["principal"].ReturningType);

            Assert.AreEqual("principal", module.Functions["principal"].Name);
        }

        [Test]
        public void Should_Parse_Parameters_Function()
        {
            var module = Compile("funcao inteiro soma(inteiro a, inteiro b) fim");

            Assert.AreEqual(2, module.Functions["soma"].Args.Length);

            Assert.AreEqual("a", module.Functions["soma"].Args[0].Name);
            Assert.AreEqual("b", module.Functions["soma"].Args[1].Name);

            Assert.AreEqual(typeof(int), module.Functions["soma"].Args[0].Type);
            Assert.AreEqual(typeof(int), module.Functions["soma"].Args[1].Type);
        }
    }
}