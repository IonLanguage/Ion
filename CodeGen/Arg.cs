using LLVMSharp;

namespace LlvmSharpLang.CodeGen
{
    public class Arg : NamedModuleEntity<LLVMTypeRef>
    {
        public Type Type { get; set; }

        public override LLVMTypeRef Emit(LLVMModuleRef module)
        {
            return this.Type.Emit();
        }
    }
}
