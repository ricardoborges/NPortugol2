using NUnit.Framework;

namespace NPortugol2.Tests.Dyn.Args
{
    [TestFixture]
    public class LoadArgsTestCase
    {
        [Test]
        public void Should_Load_Args()
        {
            var result = new NPortugol2()
                .CompileMethod("funcao int soma(int x) retorne x fim")
                .Invoke(null, new object[]{20});

            Assert.AreEqual(20, result);
        }

        [Test]
        public void Should_Load_Args2()
        {
            var result = new NPortugol2()
                .CompileMethod("funcao int soma(int x, int y) retorne x + y fim")
                .Invoke(null, new object[] { 20, 30 });

            Assert.AreEqual(50, result);
        }
    }
}