using NPortugol2.Dyn;
using NPortugol2.Lang.Instructions;
using NUnit.Framework;

namespace NPortugol2.Tests.Dyn
{
    [TestFixture]
    public class DynamicMethodBuilderTestCase
    {
        [Test]
         public void Should_Create_DM_No_Parameters()
         {
             var insts = new Instruction[]
                            {
                                new Ldint { Value = 2 },
                                new Ldint { Value = 2 },
                                new Add(),
                                new Ret()
                            };

             var module = ModuleFactory.CreateFor<int>(insts, "add");

            var dm = new DynamicMethodBuilder(module).BuildFor("add");

            var value = dm.Invoke(null, null);

            Assert.AreEqual(4, value);
         }
    }
}