using System;
using LLVMSharp;
using LlvmSharpLang.CodeGeneration;

namespace LlvmSharpLang.Parsing
{
    public class FunctionReturnExpr : Expr
    {
        public override ExprType Type => ExprType.FunctionReturn;

        public override LLVMValueRef Emit(LLVMBuilderRef context)
        {
            // TODO: Implement.
            throw new NotImplementedException();
        }
    }
}
