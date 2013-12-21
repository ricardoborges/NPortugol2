using NPortugol2.Core;

namespace NPortugol2.VirtualMachine
{
    public partial class Engine
    {
        public Process Process { get; set; }

        public Engine LoadModule(Module module)
        {
            Process = new Process(module);

            return this;
        }

        public object Execute(string functionName)
        {
            if (!Process.IsRunning)
                Process.Init(functionName);

            while (Process.IsRunning)
            {
                ExecuteInstruction();
            }

            return Process.Result;
        }
    }
}