using NPortugol2.Compiler;
using NUnit.Framework;

namespace NPortugol2.Tests.Dyn.Args
{
    [TestFixture]
    public class LoadArgsTestCase
    {
        [Test]
        public void Should_Load_Args()
        {
            var result = new NPCompiler()
                .CompileMethod("funcao inteiro soma(inteiro x) retorne x fim")
                .Invoke(null, new object[]{20});

            Assert.AreEqual(20, result);
        }

        [Test]
        public void Should_Load_Args2()
        {
            var result = new NPCompiler()
                .CompileMethod("funcao inteiro soma(inteiro x, inteiro y) retorne x + y fim")
                .Invoke(null, new object[] { 20, 30 });

            Assert.AreEqual(50, result);
        }
    }
}