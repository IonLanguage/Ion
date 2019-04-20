using LLVMSharp;
using LlvmSharpLang.CodeGeneration.Structure;

namespace LlvmSharpLang.CodeGeneration
{
    public class Arg : Named, IUncontextedEntity<LLVMTypeRef>
    {
        protected readonly Type type;

        public Arg(Type type)
        {
            this.type = type;
        }

        public LLVMTypeRef Emit()
        {
            return this.type.Emit();
        }
    }
}
