using System;
using System.Runtime.InteropServices;
using LLVMSharp;
using LlvmSharpLang.CodeGeneration;
using LlvmSharpLang.CodeGeneration.Structure;

namespace LlvmSharpLang.Abstraction
{
    public class Module
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
            function.CreateBody();

            // Create the arguments.
            function.CreateArgs();

            // Emit the function.
            function.Emit(this.Source);

            return function;
        }

        public override string ToString()
        {
            // Print IR code to a buffer.
            IntPtr output = LLVM.PrintModuleToString(this.Source);

            // Convert buffer to a string.
            return Marshal.PtrToStringAnsi(output);
        }
    }
}
