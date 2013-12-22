using System;

namespace NPortugol2.VirtualMachine
{
    public class FunctionNotFound: Exception
    {
        public FunctionNotFound(string message) : base(message)
        {
        }
    }
}