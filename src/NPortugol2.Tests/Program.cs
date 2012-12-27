using System;

namespace NPortugol2.Tests
{
	public static class Program
	{
		public static void Main ()
		{
			var list = new NPortugol2 ().CompileIL ("funcao inteiro soma() variavel inteiro x = 20, y = 20 retorne x + y fim");

			foreach (var item in list) 
			{
				System.Console.WriteLine(item);
			}
		}
	}
}

