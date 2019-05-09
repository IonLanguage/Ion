using System.Collections.Generic;
using Ion.CodeGeneration.Structure;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class FunctionCallExpr : Expr
    {
        public override ExprType Type => ExprType.FunctionCall;

        public LLVMValueRef Target { get; }

        public string Callee { get; }

        public List<Expr> Args { get; }

        public FunctionCallExpr(LLVMValueRef target, string callee, List<Expr> args)
        {
            this.Target = target;
            this.Callee = callee;
            this.Args = args;
        }

        public override LLVMValueRef Emit(PipeContext<LLVMBuilderRef> context)
        {
            // Create the resulting arguments.
            List<LLVMValueRef> args = new List<LLVMValueRef>();

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
            LLVMValueRef functionCall = LLVM.BuildCall(context.Target, this.Target, args.ToArray(), this.Name);

            // Return the emitted function call.
            return functionCall;
        }
    }
}
