using NUnit.Framework;

namespace NPortugol2.Tests.VirtualMachine
{
    [TestFixture]
    public class ArithmeticTestCase
    {
        [Test]
        public void Should_Add_Int_Values_On_Stack()
        {
            var insts = new []
                            {
                                InstFactory.Ldc_I4(2),
                                InstFactory.Ldc_I4(2),
                                InstFactory.Add()
                            };

            var engine = EngineFactory.CreateFor<int>(insts, "add");

            engine.Execute("add");

            Assert.AreEqual(1, engine.Process.EvalStack.Count);
            Assert.AreEqual(4, engine.Process.Value);
            Assert.AreEqual(typeof(int), engine.Process.EvalStack.Peek().Type);
        }

        [Test]
        public void Should_Add_Float_Values_On_Stack()
        {
            var insts = new []
                            {
                                InstFactory.Ldc_R4(2F),
                                InstFactory.Ldc_R4(2F),
                                InstFactory.Add()
                            };

            var engine = EngineFactory.CreateFor<int>(insts, "add");

            engine.Execute("add");

            Assert.AreEqual(1, engine.Process.EvalStack.Count);
            Assert.AreEqual(4.0F, engine.Process.Value);
            Assert.AreEqual(typeof(float), engine.Process.EvalStack.Peek().Type);
        }
    }
}