namespace NPortugol2.Compiler
{
    public static class StringExtensions
    {
         public static string Content(this string content)
         {
             return content.Substring(1, content.Length - 2);
         }
    }
}