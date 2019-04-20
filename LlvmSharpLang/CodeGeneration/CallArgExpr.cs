using System;
using LLVMSharp;
using LlvmSharpLang.CodeGeneration;

namespace LlvmSharpLang.Parsing
{
    public class CallArgExpr : Expr
    {
        public override LLVMValueRef Emit(LLVMBuilderRef context)
        {
            // TODO: Implement.
            throw new NotImplementedException();
        }
    }
}
