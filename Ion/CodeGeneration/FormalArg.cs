using Ion.CodeGeneration.Structure;
using Ion.Misc;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class FormalArg : Named, IUncontextedEntity<LLVMTypeRef>
    {
        protected readonly Type type;

        public FormalArg(Type type)
        {
            this.type = type;
        }

        public LLVMTypeRef Emit()
        {
            // Emit the type.
            LLVMTypeRef type = this.type.Emit();

            // Return the emitted type.
            return type;
        }
    }
}
