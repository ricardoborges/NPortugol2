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

            // Executa fun��o como DynamicMethod
            var result = new NPortugol2(script).Invoke(null, null);

            Assert.AreEqual(40, result);

            // Executa fun��o dentro da maquina virtual
            var resultFromVM = new NPortugol2(script).Exec("soma", null);

            Assert.AreEqual(result, resultFromVM);
        }
	}
}

