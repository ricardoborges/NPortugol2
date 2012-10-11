using NPortugol2.Lang.Instructions;
using NUnit.Framework;

namespace NPortugol2.Tests.VirtualMachine
{
    [TestFixture]
    public class ArithmeticTestCase
    {
        [Test]
        public void Should_Add_Int_Values_On_Stack()
        {
            var insts = new Instruction[]
                            {
                                new Ldint { Value = 2 },
                                new Ldint { Value = 2 },
                                new Add()
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
            var insts = new Instruction[]
                            {
                                new Ldf { Value = 2F },
                                new Ldf { Value = 2F },
                                new Add()
                            };

            var engine = EngineFactory.CreateFor<int>(insts, "add");

            engine.Execute("add");

            Assert.AreEqual(1, engine.Process.EvalStack.Count);
            Assert.AreEqual(4.0m, engine.Process.Value);
            Assert.AreEqual(typeof(float), engine.Process.EvalStack.Peek().Type);
        }
    }
}