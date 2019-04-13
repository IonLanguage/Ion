using LLVMSharp;

namespace LlvmSharpLang.CodeGen
{
    public class Block : NamedValueEntity<LLVMBasicBlockRef>
    {

        public override LLVMBasicBlockRef Emit(LLVMValueRef context)
        {
            return LLVM.AppendBasicBlock(context, this.Name);
        }
    }
}
