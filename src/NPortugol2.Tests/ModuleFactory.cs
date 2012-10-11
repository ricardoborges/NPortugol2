﻿using System.Collections.Generic;
using NPortugol2.Lang.Instructions;
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
}