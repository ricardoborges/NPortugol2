using NPortugol2.Compiler;
using NUnit.Framework;
using System.Reflection.Emit;

namespace NPortugol2.Tests.Dyn.Expression
{
    [TestFixture]
    public class LocalsExpTestCase
    {
        [Test]
        public void Should_Add_Int_Locals()
        {
            var result = new NPCompiler()
				.CompileMethod("funcao inteiro soma() variavel inteiro x = 20, y = 20 retorne x + y fim")
                .Invoke(null, null);

            Assert.AreEqual(40, result);
        }
    }
}