using LLVMSharp;

namespace Ion.CodeGeneration.Structure
{
    public static class PipeContextFactory
    {
        /// <summary>
        /// Create a pipe context with LLVM module reference
        /// as target from an abstracted module class instance.
        /// </summary>
        public static PipeContext<LLVMModuleRef> CreateFromModule(Abstraction.Module module)
        {
            // Create the instance and fill parameters from the module.
            return new PipeContext<LLVMModuleRef>(module.Source, module.SymbolTable);
        }
    }
}
