using System.Collections.Generic;
using System.Reflection.Emit;
using NPortugol2.Core;
using NPortugol2.VirtualMachine;

namespace NPortugol2.Tests
{
    public class ModuleFactory
    {
        /// <typeparam name="T">Function returning type</typeparam>
        public static Module CreateFor<T>(Instruction[] instructions, string functionName)
        {
            var function = new Function
            {
                Instructions = instructions,
                Name = functionName,
                ReturningType = typeof(T)
            };

            var module = new Module
            {
                Functions = new Dictionary<string, Function>
                                                  {
                                                      {functionName, function}
                                                  },
                Main = function,
                Name = "Default"
            };

            return module;
        }
    }

    public class InstFactory
    {
        public static Instruction Ldc_I4(int value)
        {
            return new Instruction { OpCode = OpCodes.Ldc_I4, Value = value };
        }

        public static Instruction Ldc_R4(float value)
        {
            return new Instruction { OpCode = OpCodes.Ldc_R4, Value = value };
        }

        public static Instruction Add()
        {
            return new Instruction { OpCode = OpCodes.Add };
        }

        public static Instruction Ret()
        {
            return new Instruction { OpCode = OpCodes.Ret };
        }

        public static Instruction Initblk<T>(string[] values, bool isLocal)
        {
            return new Instruction { OpCode = OpCodes.Initblk, DeclaringType = typeof(T), IsLocal = isLocal, Operands = values };
        }
    }

    public class EngineFactory
    {
        /// <typeparam name="T">Function returning type</typeparam>
        public static Engine CreateFor<T>(Instruction[] instructions, string functionName)
        {
            var function = new Function
            {
                Instructions = instructions,
                Name = functionName,
                ReturningType = typeof(T)
            };

            var module = new Module
            {
                Functions = new Dictionary<string, Function>
                                                  {
                                                      {functionName, function}
                                                  },
                Main = function,
                Name = "Default"
            };

            return new Engine().LoadModule(module);
        }
    }
}