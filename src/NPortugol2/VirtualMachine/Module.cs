﻿using System.Collections.Generic;

namespace NPortugol2.VirtualMachine
{
    public class Module
    {
        public Module()
        {
            Name = "Default";
            Functions = new Dictionary<string, Function>();
        }

        public string Name { get; set; }

        public Function Main { get; set; }

        public Dictionary<string, Function> Functions { get; set; }
    }
}