using System;
using System.Collections.Generic;
using Ion.CodeGeneration.Helpers;
using Ion.Tracking.Symbols;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class CallExpr : Expr
    {
        public override ExprType ExprType => ExprType.FunctionCall;

        public string TargetName { get; }

        public List<Expr> Args { get; }

        public CallExpr(string targetName, List<Expr> args)
        {
            this.TargetName = targetName;
            this.Args = args;
        }

        public CallExpr(string targetName) : this(targetName, new List<Expr>())
        {
            //
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
            if (!context.SymbolTable.functions.Contains(this.TargetName))
            {
                throw new Exception($"Call to a non-existent function named '{this.TargetName}' performed");
            }

            // Retrieve the target function.
            FunctionSymbol target = context.SymbolTable.functions[this.TargetName];

            // Ensure argument count is correct (with continuous arguments).
            if (target.ContinuousArgs && args.Count < target.ArgumentCount - 1)
            {
                throw new Exception($"Target function requires at least {target.ArgumentCount - 1} argument(s)");
            }
            // Otherwise, expect the argument count to be exact.
            else if (args.Count != target.ArgumentCount)
            {
                throw new Exception($"Argument amount mismatch, target function requires exactly {target.ArgumentCount} argument(s)");
            }

            // Create the function call.
            LLVMValueRef functionCall = LLVM.BuildCall(context.Target, target.Value, args.ToArray(), this.Identifier);

            // Return the emitted function call.
            return functionCall;
        }
    }
}
