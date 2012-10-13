using NUnit.Framework;

namespace NPortugol2.Tests.Dyn.Integration
{
    [TestFixture]
    public class NPortugol2TestCase
    {
        [Test]
        public void ShouldCreateIntFunction()
        {
            var dm = new NPortugol2().Compile("funcao int principal() fim");

            Assert.AreEqual(typeof(int), dm.ReturnType);
            Assert.AreEqual("principal", dm.Name);
        }
    }
}