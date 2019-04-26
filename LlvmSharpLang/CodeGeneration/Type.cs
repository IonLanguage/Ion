using LLVMSharp;
using LlvmSharpLang.CodeGeneration.Structure;
using LlvmSharpLang.CognitiveServices;
using LlvmSharpLang.Misc;

namespace LlvmSharpLang.CodeGeneration
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
