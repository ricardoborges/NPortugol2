using NUnit.Framework;

namespace NPortugol2.Tests.Dyn.Integration
{
    [TestFixture]
    public class FunctionTestCase
    {
        [Test]
        public void ShouldCreateIntFunction()
        {
            var dm = new NPortugol2().CompileMethod("funcao int principal() fim");

            Assert.AreEqual(typeof(int), dm.ReturnType);
            Assert.AreEqual("principal", dm.Name);
        }

        [Test]
        public void ShouldCreateIntFunctionParams()
        {
            var dm = new NPortugol2().CompileMethod("funcao int soma(int a, int b) fim");            

            Assert.AreEqual(typeof(int), dm.ReturnType);
            Assert.AreEqual("soma", dm.Name);

            var parameters = dm.GetParameters();

            Assert.AreEqual(2, parameters.Length);

            Assert.AreEqual(typeof(int), parameters[0].ParameterType);
            Assert.AreEqual(typeof(int), parameters[0].ParameterType);
        }
    }
}