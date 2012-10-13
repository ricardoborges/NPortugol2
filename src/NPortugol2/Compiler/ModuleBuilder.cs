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

        private Dictionary<string, Type> TypeMap = new Dictionary<string, Type>
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

        public void CreateFunction(CommonTree type, IToken name)
        {
            var function = new Function
                               {
                                   Name = name.Text,
                                   ReturningType = TypeMap[type != null? type.Token.Text: ""]
                               };

            module.Functions.Add(function.Name, function);
        }

        public Module Module
        {
            get { return module; }
        }
    }
}