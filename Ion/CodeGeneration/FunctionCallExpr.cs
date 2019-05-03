using System.Collections.Generic;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class FunctionCallExpr : Expr
    {
        public FunctionCallExpr(LLVMValueRef target, string callee, List<Expr> args)
        {
            this.Target = target;
            this.Callee = callee;
            this.Args = args;
        }

        public override ExprType Type => ExprType.FunctionCall;

        public LLVMValueRef Target { get; }

        public string Callee { get; }

        public List<Expr> Args { get; }

        public override LLVMValueRef Emit(LLVMBuilderRef context)
        {
            // Create the resulting arguments.
            var args = new List<LLVMValueRef>();

            // Emit the call arguments.
            foreach (Expr arg in this.Args)
            {
                // Continue if the argument is null.
                if (arg == null)
                {
                    continue;
                }

                // Emit the argument.
                LLVMValueRef? argValue = arg.Emit(context);

                // TODO: Should be reported as a warning?
                // Continue if emission failed.
                if (!argValue.HasValue)
                {
                    continue;
                }

                // Append to the resulting arguments.
                args.Add(argValue.Value);
            }

            // Create the function call.
            LLVMValueRef functionCall = LLVM.BuildCall(context, this.Target, args.ToArray(), this.Name);

            return functionCall;
        }
    }
}