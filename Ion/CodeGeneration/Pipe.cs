using System.Collections.Generic;
using Ion.CodeGeneration.Structure;
using Ion.Parsing;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class Pipe : IPipe<LLVMBuilderRef, LLVMValueRef>
    {
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

        public LLVMValueRef Emit(PipeContext<LLVMBuilderRef> context)
        {
            // Retrieve target function.
            LLVMValueRef target = context.SymbolTable.RetrieveFunctionOrThrow(this.TargetName);

            // Create the argument list.
            List<Expr> arguments = new List<Expr>(this.Arguments);

            // TODO: Callee is hard-coded.
            // Create the function call expression.
            FunctionCallExpr functionCall = new FunctionCallExpr(target, "TestCallee", arguments);

            // Emit and return the function call expression.
            return functionCall.Emit(context);
        }
    }
}
