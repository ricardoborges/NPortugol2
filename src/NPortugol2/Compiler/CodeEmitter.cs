using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using NPortugol2.Core;

namespace NPortugol2.Compiler
{
    public class CodeEmitter
    {
        private readonly Module module;

        private List<Arg> args;
        private List<Instruction> instructions;
        private List<Symbol> symbols;

        private readonly Dictionary<string, Type> typeMap = new Dictionary<string, Type>
                                                       {
                                                           {"inteiro", typeof (int)},
                                                           {"decimal", typeof (float)},
                                                           {"texto", typeof (string)},
                                                           {"", typeof(void)}
                                                       };

        public string ErrorMessage { get; set; }

        public bool HasError { get; set; }

        public CodeEmitter()
        {
            module = new Module();
            instructions = new List<Instruction>();
            symbols = new List<Symbol>();
            args = new List<Arg>();
        }

        public Module Module
        {
            get { return module; }
        }

        #region function

        public void CreateFunctionArg(IToken type, IToken name)
        {
            var parameter = new Arg {Name = name.Text, Type = typeMap[type.Text]};

            args.Add(parameter);
        }

        public void CreateFunction(CommonTree type, IToken name)
        {
            var function = new Function
                               {
                                   Name = name.Text,
                                   ReturningType = typeMap[type != null? type.Token.Text: ""],
                                   Args = args != null? args.ToArray(): new Arg[]{},
                                   Instructions = instructions.ToArray(),
                                   Symbols = symbols !=null? symbols.ToArray(): new Symbol[]{}
                               };

            module.Functions.Add(function.Name, function);

            Reset();
        }

        private void Reset()
        {
            args.Clear();
            instructions.Clear();
            symbols.Clear();
        }

        #endregion

        #region Locals

        public Type DeclareLocal(Type type, IToken name, object value)
        {
            DeclareLocal(type, name.Text, value);

            return type;
        }

        public Type DeclareLocal(IToken ttype, IToken name, object value)
        {
            var type = typeMap[ttype.Text];

            DeclareLocal(type, name.Text, value);

            return type;
        }
        
        public void DeclareLocal(Type type, string name, object value = null)
        {
            symbols.Add(new Symbol
                                    {
                                        Name = name,
                                        Type = type,
                                        Value = value
                                    });
        }

        #endregion

        #region Instructions

        public void EmitLdcI4(int value, IToken token)
        {
            instructions.Add(new Instruction {OpCode = OpCodes.Ldc_I4, Value = value});
        }

        public void EmitLdcR4(float value, IToken token)
        {
            instructions.Add(new Instruction { OpCode = OpCodes.Ldc_R4, Value = value });
        }

        public void EmitLdstr(string value, IToken token)
        {
            instructions.Add(new Instruction {OpCode = OpCodes.Ldstr,Value = value.Content()});
        }

        public void Stloc(string name, object value, IToken token)
        {
            instructions.Add(new Instruction { OpCode = OpCodes.Ldstr, Value = value });
        }

        public void Emit(OpCode opCode)
        {
            instructions.Add(new Instruction { OpCode = opCode });
        }
        
        public void EmitLoadVar(string name, IToken token)
        {
            if (symbols.Find(x => x.Name == name) == null)
            if (args.Find(x  => x.Name == name) == null)
                throw new Exception(string.Format("Variável {0} não declarada.", name));

            instructions.Add(new Instruction{OpCode = OpCodes.Ldloc, Value = name});
        }

        #endregion
    }
}