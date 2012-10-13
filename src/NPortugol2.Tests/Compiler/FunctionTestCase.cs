using NUnit.Framework;

namespace NPortugol2.Tests.Compiler
{
    [TestFixture]
    public class FunctionTestCase: BaseCompilerTestCase
    {
        [Test]
        public void Should_Parse_Typed_Function()
        {
            var module = Compile("funcao int principal() fim");

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
            var module = Compile("funcao int soma(int a, int b) fim");

            Assert.AreEqual(2, module.Functions["soma"].Params.Length);

            Assert.AreEqual("a", module.Functions["soma"].Params[0].Name);
            Assert.AreEqual("b", module.Functions["soma"].Params[1].Name);

            Assert.AreEqual(typeof(int), module.Functions["soma"].Params[0].Type);
            Assert.AreEqual(typeof(int), module.Functions["soma"].Params[1].Type);
        }
    }
}