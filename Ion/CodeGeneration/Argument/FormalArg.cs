using Ion.CodeGeneration.Helpers;
using Ion.Misc;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class FormalArg : Named, IGenericPipe<LLVMTypeRef>
    {
        protected readonly Type type;

        public FormalArg(Type type)
        {
            this.type = type;
        }

        public LLVMTypeRef Emit(IGenericPipeContext context)
        {
            // Emit the type.
            LLVMTypeRef type = this.type.Emit(context);

            // Return the emitted type.
            return type;
        }
    }
}
