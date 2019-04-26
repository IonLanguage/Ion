using LLVMSharp;
using LlvmSharpLang.CodeGeneration.Structure;
using LlvmSharpLang.Misc;

namespace LlvmSharpLang.CodeGeneration
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
            return this.type.Emit();
        }
    }
}
