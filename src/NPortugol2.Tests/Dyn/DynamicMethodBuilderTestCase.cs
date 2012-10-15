using NPortugol2.Dyn;
using NUnit.Framework;

namespace NPortugol2.Tests.Dyn
{
    [TestFixture]
    public class DynamicMethodBuilderTestCase
    {
        [Test]
         public void Should_Create_DM_No_Parameters()
         {
             var insts = new []
                            {
                                InstFactory.Ldc_I4(2),
                                InstFactory.Ldc_I4(2),
                                InstFactory.Add(),
                                InstFactory.Ret()
                            };

             var module = ModuleFactory.CreateFor<int>(insts, "add");

            var dm = new DynamicMethodBuilder(module).BuildFor("add");

            var value = dm.Invoke(null, null);

            Assert.AreEqual(4, value);
         }
    }
}