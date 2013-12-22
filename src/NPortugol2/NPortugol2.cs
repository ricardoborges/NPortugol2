using System;
using System.Reflection.Emit;
using NPortugol2.Compiler;
using NPortugol2.VirtualMachine;

namespace NPortugol2
{
    public class NPortugol2
    {
        private string _script;
        private NPCompiler _compiler;

        public NPortugol2(string script)
        {
            _script = script;
            _compiler = new NPCompiler();
        }

        public NPortugol2()
        {
            _compiler = new NPCompiler();
        }

        public NPortugol2 LoadScript(string script)
        {
            _script = script;

            return this;
        }

        /// <summary>
        /// Execute NPortugol function as Dynamic Method
        /// </summary>
        public object Invoke(object target, object[] parameters)
        {
            return CompileDynamicMethod().Invoke(target, parameters);
        }

        /// <summary>
        /// Execute NPortugol function in the Virtual Machine
        /// </summary>
        public object Exec(string functionName, object[] parameters)
        {
            return CreateVM().Execute(functionName);
        }

        public DynamicMethod CompileDynamicMethod()
        {
            return IsScriptLoaded
                       ? _compiler.CompileMethod(_script)
                       : ScriptNotFound();
        }

        public Engine CreateVM()
        {
            var engine = new Engine();
                engine.LoadModule(_compiler.CompileScript(_script));

            return engine;
        }

        private DynamicMethod ScriptNotFound()
        {
            throw new Exception("You must load a script before.");
        }

        public bool IsScriptLoaded
        {
            get { return !string.IsNullOrEmpty(_script); }
        }
    }
}