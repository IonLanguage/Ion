using Ion.Core;
using LLVMSharp;

namespace Ion.CodeGeneration.Structure
{
    /// <summary>
    /// Serves as a wrapper to provide direct access to
    /// key class instances such as symbol table and of course
    /// the entity's target.
    /// </summary>
    public class PipeContext<T>
    {
        public T Target { get; }

        public SymbolTable SymbolTable { get; }

        public PipeContext(T target, SymbolTable symbolTable)
        {
            this.Target = target;
            this.SymbolTable = symbolTable;
        }

        /// <summary>
        /// Derive the context, creating a new instance of it
        /// with a new provided target, but maintaining a reference
        /// to the local symbol table.
        /// </summary>
        public PipeContext<TTarget> Derive<TTarget>(TTarget target)
        {
            // Create the derived context, with a reference to the local symbol table.
            PipeContext<TTarget> context = new PipeContext<TTarget>(target, this.SymbolTable);

            // Return the created context.
            return context;
        }
    }
}
