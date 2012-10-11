using NPortugol2.Lang.Instructions;
using NPortugol2.VirtualMachine;
using NUnit.Framework;

namespace NPortugol2.Tests.VirtualMachine
{
    [TestFixture]
    public class MemoryManagementTestCase
    {
        [Test]
        public void Should_Init_Int_Variable()
        {
            var init = new Init {DeclaringType = typeof (int), Names = {"x"}};

            var engine = EngineFactory.CreateFor<int>(new[] {init}, "declare");

            engine.Execute("declare");

            Assert.AreEqual(typeof(int), engine.Process.Globals.Symbols["x"].Type);
        }

        [Test]
        public void Should_Load_Int_Value_On_Stack()
        {
            var ldint = new Ldint {Value = 2};

            var engine = EngineFactory.CreateFor<int>(new[] {ldint}, "load");

            engine.Execute("load");

            Assert.AreEqual(2, engine.Process.EvalStack.Pop().Value);
        }
    }
}