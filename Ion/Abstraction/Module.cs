using System;
using System.Runtime.InteropServices;
using Ion.CodeGeneration;
using Ion.CodeGeneration.Structure;
using Ion.Core;
using LLVMSharp;

namespace Ion.Abstraction
{
    public class Module : IDisposable, ICloneable
    {
        public LLVMModuleRef Source { get; }

        public string Name { get; }

        public SymbolTable SymbolTable { get; }

        public Module(LLVMModuleRef source)
        {
            this.Source = source;

            // Create a new symbol table instance.
            this.SymbolTable = new SymbolTable();
        }

        public Module(string name)
        {
            this.Name = name;
            this.Source = LLVM.ModuleCreateWithName(this.Name);

            // Create a new symbol table instance.
            this.SymbolTable = new SymbolTable();
        }

        public Module() : this(SpecialName.Entry)
        {
            //
        }

        public object Clone()
        {
            return new Module(LLVM.CloneModule(this.Source));
        }

        public void Dispose()
        {
            LLVM.DisposeModule(this.Source);
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

            // Assign name as main.
            function.SetName(SpecialName.Main);

            // Create the body.
            Block body = function.CreateBody();

            // Set the body's name to entry.
            body.SetNameEntry();

            // Create the arguments.
            function.CreateArgs();

            // Return the function.
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
            Function function = CreateMainFunction();

            // Create pipe context for the function.
            PipeContext<LLVMModuleRef> context = new PipeContext<LLVMModuleRef>(this.Source, this.SymbolTable);

            // Emit the function.
            function.Emit(context);

            // Return the previously created function.
            return function;
        }

        public LLVMContextRef GetContext()
        {
            return LLVM.GetModuleContext(this.Source);
        }

        /// <summary>
        /// Dump the contents of the corresponding
        /// IR code with this module to the console
        /// output.
        /// </summary>
        public void Dump()
        {
            LLVM.DumpModule(this.Source);
        }

        /// <summary>
        /// Obtain the corresponding IR code from this
        /// module.
        /// </summary>
        public override string ToString()
        {
            // Print IR code to a buffer.
            IntPtr output = LLVM.PrintModuleToString(this.Source);

            // Convert buffer to a string.
            string outputString = Marshal.PtrToStringAnsi(output);

            // Dispose message.
            LLVM.DisposeMessage(output);

            // Trim whitespace.
            outputString = outputString.Trim();

            // Return resulting output string.
            return outputString;
        }
    }
}
