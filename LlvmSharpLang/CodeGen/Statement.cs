using System;
using LLVMSharp;
using LlvmSharpLang.CodeGen.Structure;

namespace LlvmSharpLang.CodeGen
{
    public class Statement : IEntity<LLVMValueRef, LLVMBuilderRef>
    {
        public LLVMValueRef Emit(LLVMBuilderRef context)
        {
            throw new NotImplementedException();
        }
    }
}
