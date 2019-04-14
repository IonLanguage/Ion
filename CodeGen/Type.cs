using LLVMSharp;
using LlvmSharpLang.CodeGen.Structure;
using LlvmSharpLang.Misc;

namespace LlvmSharpLang.CodeGen
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
            return Resolver.Type(this.value)();
        }
    }
}
