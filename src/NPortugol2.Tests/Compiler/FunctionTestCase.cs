using NUnit.Framework;

namespace NPortugol2.Tests.Compiler
{
    [TestFixture]
    public class FunctionTestCase: BaseCompilerTestCase
    {
        [Test]
        public void ShoulParseTypedFunction()
        {
            var module = Compile("funcao int principal() fim");

            Assert.AreEqual(1, module.Functions.Count);
            Assert.AreEqual(typeof(int), module.Functions["principal"].ReturningType);
            Assert.AreEqual("principal", module.Functions["principal"].Name);
        }

        [Test]
        public void ShoulParseNonTypedFunction()
        {
            var module = Compile("funcao principal() fim");

            Assert.AreEqual(1, module.Functions.Count);
            Assert.AreEqual(typeof(void), module.Functions["principal"].ReturningType);
            Assert.AreEqual("principal", module.Functions["principal"].Name);
        }
    }
}