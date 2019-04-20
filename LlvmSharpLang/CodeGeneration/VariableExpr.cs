using System;
using LLVMSharp;
using LlvmSharpLang.CodeGeneration.Structure;

namespace LlvmSharpLang.CodeGeneration
{
    public class VariableExpr : Expr
    {
        public VariableExpr(string name)
        {
            this.SetName(name);
        }

        public override LLVMValueRef Emit(LLVMBuilderRef context)
        {
            // TODO: Implement.
            throw new NotImplementedException();
        }
    }
}
