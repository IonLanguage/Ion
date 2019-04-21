using System;
using LLVMSharp;
using LlvmSharpLang.CodeGeneration;

namespace LlvmSharpLang.Parsing
{
    public class CallArgExpr : Expr
    {
        public override ExprType Type => ExprType.FunctionCallArgument;

        public override LLVMValueRef Emit(LLVMBuilderRef context)
        {
            // TODO: Implement.
            throw new NotImplementedException();
        }
    }
}
