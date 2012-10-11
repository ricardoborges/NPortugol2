using System.Reflection.Emit;

namespace NPortugol2
{
    public class NPortugol2
    {
        public DynamicMethod Compile(string function)
        {
            var dm = new DynamicMethod(string.Empty, typeof(int), null);

            return dm;
        }
    }
}