using System;
using System.Collections.Generic;
using NPortugol2.Core;

namespace NPortugol2.VirtualMachine
{
    public class Process
    {
        public Process(Module module)
        {
            this.module = module;
        }

        private Module module;

        public Stack<Call> CallStack { get; set; }

        public Stack<FunctionArg> ParamStack { get; set; }

        public Stack<Symbol> EvalStack { get; set; }

        public SymbolTable Globals { get; set; }

        public bool IsRunning { get; set; }

        public Symbol Result { get; set; }

        public void Setup()
        {
            CallStack = new Stack<Call>();
            ParamStack = new Stack<FunctionArg>();
            Globals = new SymbolTable();
            EvalStack = new Stack<Symbol>();
        }

        public void Init(string functionName)
        {
            Setup();

            if (!module.Functions.ContainsKey(functionName))
                throw new FunctionNotFound(functionName);

            var function = module.Functions[functionName];
            
            CallStack.Push(new Call(function));

            IsRunning = true;
        }

        public Instruction Next()
        {
            return CallStack.Peek().Next();
        }

        public void FigureOutCompleted()
        {
            if (CallStack.Peek().HasNext()) return;
                
            var lastCall = CallStack.Pop();

            if (CallStack.Count > 0) return;
            
            IsRunning = false;

            Result = lastCall.Result;
        }
    }
}