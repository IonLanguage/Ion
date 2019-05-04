using System;
using System.Runtime.InteropServices;
using Ion.CodeGeneration;
using Ion.CodeGeneration.Structure;
using LLVMSharp;

namespace Ion.Abstraction
{
    public class Module : IDisposable, ICloneable
    {
        public LLVMModuleRef Source { get; }

        public Module(LLVMModuleRef source)
        {
            this.Source = source;
        }

        public Module()
        {
            this.Source = LLVM.ModuleCreateWithName(SpecialName.Entry);
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
            var function = new Function();

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

            // Emit the function.
            function.Emit(this.Source);

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
            var outputString = Marshal.PtrToStringAnsi(output);

            // Trim whitespace.
            outputString = outputString.Trim();

            // Return resulting output string.
            return outputString;
        }
    }
}
