using System.Diagnostics;
using System.Reflection.Emit;
using NUnit.Framework;
using System;

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
            gen.Emit(OpCodes.Ldc_I4, 1);
            gen.Emit(OpCodes.Add);
            gen.Emit(OpCodes.Ret);

            var result = dm.Invoke(null, new object[] { 1, 1 });

            Assert.AreEqual(2, result);
        }   

		public class Math
		{
			public int Soma()
			{
				return 1 + 1;
			}
		}

		[Test]
		public void TestCall2()
		{
			var parameters = new[] { typeof(int), typeof(int) };

			var dm = new DynamicMethod("soma", typeof(int), parameters);

			var gen = dm.GetILGenerator();

			gen.DeclareLocal (typeof(Math));
			var ctor = typeof(Math).GetConstructors () [0];

			gen.Emit (OpCodes.Newobj, ctor);
			gen.Emit (OpCodes.Stloc, 0);
            gen.Emit (OpCodes.Ldobj, 0);

			//gen.Emit(OpCodes.Ldarg_0);
			//gen.Emit(OpCodes.Ldarg_1);
			//var soma = GetType ().GetMethod ("Soma");

			//gen.EmitCall (OpCodes.Callvirt, soma, new Type[] {  });

			gen.Emit (OpCodes.Ldc_I4, 2);

			gen.Emit(OpCodes.Ret);

			var result = dm.Invoke(null, new object[] { 1, 1 });

			//var func = (Func<int, int, int>)dm.CreateDelegate(typeof(Func<int, int, int>));

			Assert.AreEqual (2, result);
		}          

		[Test]
		public void TestCall()
		{
			var parameters = new[] { typeof(int), typeof(int) };

			var dm = new DynamicMethod("soma", typeof(int), parameters);

			var gen = dm.GetILGenerator();

			gen.Emit(OpCodes.Ldarg_0);
			gen.Emit(OpCodes.Ldarg_1);

			var soma = GetType ().GetMethod ("Soma");

			gen.Emit (OpCodes.Call, soma);

			gen.Emit(OpCodes.Ret);

			var result = dm.Invoke(null, new object[] { 1, 1 });

			Assert.AreEqual (2, result);
		}         

		public static int Soma(int a, int b)
		{
			return a + b;
		}

        [Test]
        public void Hello()
        {
            var method = @"funcao inteiro soma() retorne 1 + 1 fim";

            var dm = new NPortugol2().CompileMethod(method);

            var result = dm.Invoke(null, null);

            Assert.AreEqual(2, (int) result);
        }
    }
}