using NUnit.Framework;

namespace NPortugol2.Tests.Dyn.Expression
{
    [TestFixture]
    public class LocalsExpTestCase
    {
        [Test]
        public void Should_Add_Int_Locals()
        {
            var result = new NPortugol2()
                .CompileMethod("funcao inteiro soma() variavel inteiro x = 20, y = 20 retorne x + y fim")
                .Invoke(null, null);

            Assert.AreEqual(40, result);
        }
    }
}