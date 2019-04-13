using System;
using LLVMSharp;

namespace LlvmSharpLang
{
    public class FormalArg : NamedEntity<LLVMValueRef>
    {
        public LLVMTypeRef Type { get; set; }

        public override LLVMValueRef Emit(LLVMModuleRef module)
        {
            throw new NotImplementedException();
        }
    }
}
