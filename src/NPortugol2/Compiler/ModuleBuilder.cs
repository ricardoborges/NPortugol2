using System;
using System.Collections.Generic;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using NPortugol2.VirtualMachine;

namespace NPortugol2.Compiler
{
    public class ModuleBuilder
    {
        private readonly Module module;

        private List<FunctionParam> currentParams;

        private readonly Dictionary<string, Type> TypeMap = new Dictionary<string, Type>
                                                       {
                                                           {"int", typeof (int)},
                                                           {"dec", typeof (float)},
                                                           {"tex", typeof (string)},
                                                           {"", typeof(void)}
                                                       };

        public string ErrorMessage { get; set; }

        public bool HasError { get; set; }

        public ModuleBuilder()
        {
            module = new Module();
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
                                   Params = currentParams != null? currentParams.ToArray(): new FunctionParam[]{}
                               };

            module.Functions.Add(function.Name, function);

            currentParams = null;
        }
    }
}