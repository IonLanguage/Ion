using System;
using Ion.CodeGeneration.Helpers;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class LambdaExpr : Expr
    {
        public override ExprType ExprType => ExprType.Lambda;

        public FormalArgs Args { get; set; }

        public Type ReturnType { get; set; }

        public Block Body { get; set; }

        public override LLVMValueRef Emit(PipeContext<LLVMBuilderRef> context)
        {
            // Create a new function.
            Function function = new Function();

            // Emit the created function.
            LLVMValueRef result = function.Emit(context.ModuleContext);

            // TODO: Fish implementation.

            // Return the resulting lambda function.
            return result;
        }
    }
}
