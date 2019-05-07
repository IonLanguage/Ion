using Ion.CodeGeneration.Structure;
using Ion.CognitiveServices;
using Ion.Misc;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class Type : Named, IUncontextedEntity<LLVMTypeRef>
    {
        protected readonly string value;

        public Type(string value)
        {
            this.value = value;
        }

        public LLVMTypeRef Emit()
        {
            return Resolvers.LlvmTypeFromName(this.value);
        }
    }
}
