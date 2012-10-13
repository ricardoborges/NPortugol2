using System.Reflection.Emit;
using NUnit.Framework;

namespace NPortugol2.Tests.Dyn
{
    [TestFixture]
    public class Lab
    {

        [Test]
        public void Do()
        {
            var parameters = new[] { typeof(int), typeof(int) };

            var dm = new DynamicMethod("soma", typeof(int), parameters);

            var gen = dm.GetILGenerator();

            gen.Emit(OpCodes.Ldc_I4, 1);
            gen.Emit(OpCodes.Ret);

            var result = dm.Invoke(null, new object[] { 1, 1 });

            Assert.AreEqual(1, result);
        }         
    }
}