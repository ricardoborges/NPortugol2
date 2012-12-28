using System;

namespace NPortugol2.Tests
{
	public static class Program
	{
		public static void Main ()
		{
			var dm = new NPortugol2 ().CompileMethod(
				@"funcao inteiro soma() 
                         variavel inteiro x = 20, y = 20 
						 retorne x + y 
                  fim"
				);

			System.Console.WriteLine(dm.Invoke(null, null));
		}
	}
}

