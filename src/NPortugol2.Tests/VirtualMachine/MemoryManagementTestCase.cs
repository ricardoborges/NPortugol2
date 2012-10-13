using NUnit.Framework;

namespace NPortugol2.Tests.VirtualMachine
{
    [TestFixture]
    public class MemoryManagementTestCase
    {
        [Test]
        public void Should_Load_Int_Value_On_Stack()
        {
            var ldint = InstFactory.Ldc_I4(2);

            var engine = EngineFactory.CreateFor<int>(new[] {ldint}, "load");

            engine.Execute("load");

            Assert.AreEqual(2, engine.Process.EvalStack.Pop().Value);
        }
    }
}