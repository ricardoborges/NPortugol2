using NUnit.Framework;

namespace NPortugol2.Tests.Dyn.Expression
{
    [TestFixture]
    public class SimpleAddTestCase
    {
        [Test]
        public void Should_Create_Function_Returning_Value()
        {
            var dm = new NPortugol2().CompileMethod("funcao inteiro soma() retorne 8 + 2 fim");

            var result = dm.Invoke(null, null);

            Assert.AreEqual(10, result);
        }         

        [Test]
        public void Should_Execute_Arithmetic_Function()
        {
            var result = new NPortugol2()
                .CompileMethod("funcao inteiro calc() retorne 2*5+10-9+4/2 fim")
                .Invoke(null, null);

            Assert.AreEqual(13, result);

            var result2 = new NPortugol2()
                .CompileMethod("funcao inteiro calc() retorne 2*(5+10)-9+4/2 fim")
                .Invoke(null, null);

            Assert.AreEqual(23, result2);
        }
    }
}