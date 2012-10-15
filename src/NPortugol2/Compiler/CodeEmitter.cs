using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using NPortugol2.VirtualMachine;

namespace NPortugol2.Compiler
{
    public class CodeEmitter
    {
        private readonly Module module;

        private List<FunctionParam> currentParams;
        private List<Instruction> currInstructions;
        private List<Symbol> currentSymbols;

        private readonly Dictionary<string, Type> TypeMap = new Dictionary<string, Type>
                                                       {
                                                           {"int", typeof (int)},
                                                           {"dec", typeof (float)},
                                                           {"tex", typeof (string)},
                                                           {"", typeof(void)}
                                                       };

        public string ErrorMessage { get; set; }

        public bool HasError { get; set; }

        public CodeEmitter()
        {
            module = new Module();
            currInstructions = new List<Instruction>();
            currentSymbols = new List<Symbol>();
        }

        public Module Module
        {
            get { return module; }
        }

        public void CreateFunctionParams(IToken type, IToken name)
        {
            var parameter = new FunctionParam {Name = name.Text, Type = TypeMap[type.Text]};

            if (currentParams == null)
                currentParams = new List<FunctionParam>();

            currentParams.Add(parameter);
        }

        public void CreateFunction(CommonTree type, IToken name)
        {
            var function = new Function
                               {
                                   Name = name.Text,
                                   ReturningType = TypeMap[type != null? type.Token.Text: ""],
                                   Params = currentParams != null? currentParams.ToArray(): new FunctionParam[]{},
                                   Instructions = currInstructions.ToArray(),
                                   Symbols = currentSymbols !=null? currentSymbols.ToArray(): new Symbol[]{}
                               };

            module.Functions.Add(function.Name, function);

            currentParams = null;
            currInstructions.Clear();
            currentSymbols.Clear();
        }

        public Type DeclareLocal(IToken ttype, IToken name)
        {
            var type = TypeMap[ttype.Text];

            DeclareLocal(type, name.Text);

            return type;
        }

        public void DeclareLocal(Type type, List<CommonTree> nodes)
        {
            if (nodes == null) return;

            foreach (var node in nodes)
            {
                DeclareLocal(type, node.Text);
            }
        }

        public void DeclareLocal(Type type, string name)
        {
            currentSymbols.Add(new Symbol
                                    {
                                        Name = name,
                                        Type = type
                                    });
        }

        public void EmitLdcI4(int value, IToken token)
        {
            currInstructions.Add(new Instruction {OpCode = OpCodes.Ldc_I4, Value = value});
        }

        public void Emit(OpCode opCode)
        {
            currInstructions.Add(new Instruction {OpCode = opCode});
        }
    }
}