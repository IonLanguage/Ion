using Ion.Core;
using Ion.Tracking;
using LLVMSharp;

namespace Ion.CodeGeneration.Helpers
{
    /// <summary>
    /// Serves as a wrapper to provide direct access to
    /// key class instances such as symbol table and of course
    /// the entity's target.
    /// </summary>
    public class PipeContext<T> : IGenericPipeContext
    {
        public Module Module { get; }

        public PipeContext<Module> ModuleContext => this.Module.AsPipeContext();

        public ContextSymbolTable SymbolTable => this.Module.SymbolTable;

        public T Target { get; }

        public PipeContext(T target, Module module)
        {
            this.Target = target;
            this.Module = module;
        }

        /// <summary>
        /// Derive the context, creating a new instance of it
        /// with a new provided target, but maintaining a reference
        /// to the local symbol table.
        /// </summary>
        public PipeContext<TTarget> Derive<TTarget>(TTarget target)
        {
            // Create the derived context, with a reference to the local symbol table.
            PipeContext<TTarget> context = new PipeContext<TTarget>(target, this.Module);

            // Return the created context.
            return context;
        }
    }
}
