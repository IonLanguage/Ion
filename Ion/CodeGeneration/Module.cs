using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Ion.CodeGeneration;
using Ion.CodeGeneration.Helpers;
using Ion.Core;
using Ion.SyntaxAnalysis;
using Ion.Tracking;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class Module : IOneWayPipe<string>, IDisposable, ICloneable
    {
        public LLVMModuleRef Target { get; }

        public string Identifier { get; set; }

        public string FileName { get; }

        public ContextSymbolTable SymbolTable { get; }

        public List<string> Imports { get; }

        protected PipeContext<Module> PipeContext;

        public Module(string fileName, LLVMModuleRef target)
        {
            this.Target = target;
            this.FileName = fileName;

            // Create a new symbol table instance.
            this.SymbolTable = new ContextSymbolTable();

            // Create imports.
            this.Imports = new List<string>();

            // Create a save a pipe context as cache.
            this.PipeContext = new PipeContext<Module>(this, this);
        }

        public Module(string fileName, string identifier) : this(fileName, LLVM.ModuleCreateWithNameInContext(identifier, LLVM.GetGlobalContext()))
        {
            // TODO: Restrict to identifier pattern (use Util.ValidateIdentifier()).
            this.Identifier = identifier;
        }

        public Module(string fileName) : this(fileName, SpecialName.Entry)
        {
            //
        }

        public object Clone()
        {
            return new Module(this.FileName, LLVM.CloneModule(this.Target));
        }

        public void Dispose()
        {
            LLVM.DisposeModule(this.Target);
        }

        /// <summary>
        /// Create an empty main function with a
        /// body, empty arguments and void return
        /// type. Does not emit the function.
        /// </summary>
        public static Function CreateMainFunction()
        {
            // Create the entity.
            Function function = new Function();

            // Create the body.
            Block body = function.CreateBody();

            // Create the function's prototype.
            function.CreatePrototype();

            // Set the function's name.
            function.Prototype.SetName(SpecialName.Main);

            // Return the function.
            return function;
        }

        // TODO: Throws "AccessViolationException" (uncatchable) exception if function does not exist.
        // TODO: ... determine a way to see if the function exists first. LLVM.IsNull() and LLVMValueRef.IsNull()
        // TODO: ... all trigger the exception too.
        public LLVMValueRef GetFunction(string name)
        {
            // Retrieve the function.
            LLVMValueRef function = LLVM.GetNamedFunction(this.Target, name);

            // Return the retrieved function.
            return function;
        }

        /// <summary>
        /// Create and emit an empty main function
        /// with a body, empty arguments and void return
        /// type.
        /// </summary>
        public Function EmitMainFunction()
        {
            // Create the function.
            Function function = Module.CreateMainFunction();

            // Emit the function.
            function.Emit(this.PipeContext);

            // Return the previously created function.
            return function;
        }

        public LLVMContextRef GetContext()
        {
            return LLVM.GetModuleContext(this.Target);
        }

        /// <summary>
        /// Dump the contents of the corresponding
        /// IR code with this module to the console
        /// output.
        /// </summary>
        public void Dump()
        {
            LLVM.DumpModule(this.Target);
        }

        /// <summary>
        /// Obtain the corresponding IR code string from this
        /// module.
        /// </summary>
        public string Emit()
        {
            // Apply the module's identifier.
            LLVM.SetModuleIdentifier(this.Target, this.Identifier, this.Identifier.Length);

            // Print IR code to a buffer.
            IntPtr output = LLVM.PrintModuleToString(this.Target);

            // Convert buffer to a string.
            string outputString = Marshal.PtrToStringAnsi(output);

            // Dispose message.
            LLVM.DisposeMessage(output);

            // Trim whitespace.
            outputString = outputString.Trim();

            // Return resulting output string.
            return outputString;
        }

        public PipeContext<Module> AsPipeContext()
        {
            // Return the cached pipe context.
            return this.PipeContext;
        }
    }
}
