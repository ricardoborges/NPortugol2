using NUnit.Framework;

namespace NPortugol2.Tests
{
    [TestFixture]
	public class Program
	{
        [Test]
		public void Main ()
        {
            var script =
                @"funcao inteiro soma() 
                         variavel inteiro x = 20, y = 20 
						 retorne x + y 
                  fim";

            // Executa função como DynamicMethod
            var result = new NPortugol2(script).Invoke(null, null);

            Assert.AreEqual(40, result);

            // Executa função dentro da maquina virtual
            var resultFromVM = new NPortugol2(script).Exec("soma", null);

            Assert.AreEqual(result, resultFromVM);
        }
	}
}

