using System.Collections.Generic;
using LLVMSharp;

namespace LlvmSharpLang.CodeGeneration
{
    public class FunctionCallExpr : Expr
    {
        public string Callee { get; }

        public FormalArgs Args { get; }

        public FunctionCallExpr(string callee, FormalArgs args)
        {
            this.Callee = callee;
            this.Args = args;
        }

        public LLVMValueRef Emit(LLVMBuilderRef context)
        {
            LLVMValueRef functionCall = LLVM.BuildCall(context, null, this.Args.Emit(), this.Name);

            return functionCall;
        }
    }
}
