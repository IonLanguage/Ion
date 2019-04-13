using System;
using LLVMSharp;

namespace LlvmSharpLang.CodeGen
{
    public class FormalArg : NamedModuleEntity<LLVMValueRef>
    {
        public LLVMTypeRef Type { get; set; }

        public override LLVMValueRef Emit(LLVMModuleRef module)
        {
            throw new NotImplementedException();
        }
    }
}
