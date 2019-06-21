using Ion.CodeGeneration.Helpers;
using Ion.Misc;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class FormalArg : IGenericPipe<LLVMTypeRef>
    {
        public string Identifier { get; }

        protected readonly Type type;

        public FormalArg(string identifier, Type type)
        {
            this.Identifier = identifier;
            this.type = type;
        }

        public LLVMTypeRef Emit(IGenericPipeContext context)
        {
            // Emit the type.
            LLVMTypeRef type = this.type.Emit();

            // Return the emitted type.
            return type;
        }
    }
}
