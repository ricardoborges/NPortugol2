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
                                InstFactory.Add(),
                                InstFactory.Ret()
                            };

            var engine = EngineFactory.CreateFor<int>(insts, "add");

            engine.Execute("add");

            Assert.AreEqual(0, engine.Process.EvalStack.Count);
            Assert.AreEqual(4, engine.Process.Result.Value);
        }

        [Test]
        public void Should_Add_Float_Values_On_Stack()
        {
            var insts = new []
                            {
                                InstFactory.Ldc_R4(2F),
                                InstFactory.Ldc_R4(2F),
                                InstFactory.Add(),
                                InstFactory.Ret()
                            };

            var engine = EngineFactory.CreateFor<float>(insts, "add");

            engine.Execute("add");

            Assert.AreEqual(0, engine.Process.EvalStack.Count);
            Assert.AreEqual(4.0F, engine.Process.Result.Value);
        }

        [Test]
        public void Should_Mul_Int_Values_On_Stack()
        {
            var insts = new[]
                            {
                                InstFactory.Ldc_I4(2),
                                InstFactory.Ldc_I4(4),
                                InstFactory.Mul(),
                                InstFactory.Ret()
                            };

            var engine = EngineFactory.CreateFor<int>(insts, "mul");

            engine.Execute("mul");

            Assert.AreEqual(0, engine.Process.EvalStack.Count);
            Assert.AreEqual(8, engine.Process.Result.Value);
        }
    }
}