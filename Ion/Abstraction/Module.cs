using System;
using System.Runtime.InteropServices;
using LLVMSharp;
using Ion.CodeGeneration;
using Ion.CodeGeneration.Structure;

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

        /// <summary>
        /// Create and emit an empty main function
        /// with a body.
        /// </summary>
        public Function CreateMainFunction()
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

            // Emit the function.
            function.Emit(this.Source);

            return function;
        }

        public LLVMContextRef GetContext()
        {
            return LLVM.GetModuleContext(this.Source);
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
            return Marshal.PtrToStringAnsi(output);
        }
    }
}
