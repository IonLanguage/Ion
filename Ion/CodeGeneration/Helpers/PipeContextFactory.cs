using LLVMSharp;

namespace Ion.CodeGeneration.Helpers
{
    public static class PipeContextFactory
    {
        /// <summary>
        /// Create a pipe context with LLVM module reference
        /// as target from an abstracted module class instance.
        /// </summary>
        public static PipeContext<Module> CreateFromModule(Module module)
        {
            // Create the instance and fill parameters from the module.
            return new PipeContext<Module>(module, module);
        }
    }
}
