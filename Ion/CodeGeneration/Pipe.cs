using System.Collections.Generic;
using Ion.CodeGeneration.Structure;
using Ion.Parsing;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class Pipe : Expr
    {
        public override ExprType Type => ExprType.Pipe;

        public Expr[] Arguments { get; }

        /// <summary>
        /// The name of the target method.
        /// </summary>
        public string TargetName { get; }

        public Pipe(Expr[] arguments, string targetName)
        {
            this.Arguments = arguments;
            this.TargetName = targetName;
        }

        public override LLVMValueRef Emit(PipeContext<LLVMBuilderRef> context)
        {
            // Create the argument list.
            List<Expr> arguments = new List<Expr>(this.Arguments);

            // TODO: Callee is hard-coded.
            // Create the function call expression.
            FunctionCallExpr functionCall = new FunctionCallExpr(this.TargetName, arguments);

            // Emit and return the function call expression.
            return functionCall.Emit(context);
        }
    }
}
