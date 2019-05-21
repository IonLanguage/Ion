using System;
using System.Collections.Generic;
using Ion.CodeGeneration.Helpers;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class FunctionCallExpr : Expr
    {
        public override ExprType Type => ExprType.FunctionCall;

        public string TargetName { get; }

        public List<Expr> Args { get; }

        public FunctionCallExpr(string targetName, List<Expr> args)
        {
            this.TargetName = targetName;
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

            // Ensure the function has been emitted.
            if (!context.SymbolTable.functions.ContainsKey(this.TargetName))
            {
                throw new Exception($"Call to a non-existent function named '{this.TargetName}' performed");
            }

            // Retrieve the target function.
            LLVMValueRef target = context.SymbolTable.functions[this.TargetName];

            // Create the function call.
            LLVMValueRef functionCall = LLVM.BuildCall(context.Target, target, args.ToArray(), this.Name);

            // Return the emitted function call.
            return functionCall;
        }
    }
}
