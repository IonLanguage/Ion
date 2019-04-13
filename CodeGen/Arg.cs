using LLVMSharp;
using LlvmSharpLang.CodeGen.Structure;

namespace LlvmSharpLang.CodeGen
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
