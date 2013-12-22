using NPortugol2.Compiler;
using NUnit.Framework;

namespace NPortugol2.Tests.Dyn.Expression
{
    [TestFixture]
    public class SimpleAddTestCase
    {
        [Test]
        public void Should_Create_Function_Returning_Value()
        {
            var dm = new NPCompiler().CompileMethod("funcao inteiro soma() retorne 8 + 2 fim");

            var result = dm.Invoke(null, null);

            Assert.AreEqual(10, result);
        }         

        [Test]
        public void Should_Execute_Arithmetic_Function()
        {
            var result = new NPCompiler()
                .CompileMethod("funcao inteiro calc() retorne 2*5+10-9+4/2 fim")
                .Invoke(null, null);

            Assert.AreEqual(13, result);

            var result2 = new NPCompiler()
                .CompileMethod("funcao inteiro calc() retorne 2*(5+10)-9+4/2 fim")
                .Invoke(null, null);

            Assert.AreEqual(23, result2);
        }
    }
}