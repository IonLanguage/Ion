using System;
using Ion.CodeGeneration.Helpers;
using Ion.Misc;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class IfExpr : Expr
    {
        public override ExprType ExprType => ExprType.If;

        public Expr Condition { get; }

        public Block Action { get; }

        public Block Otherwise { get; }

        public IfExpr(Expr condition, Block action, Block otherwise = null)
        {
            // Ensure condition and action are set.
            if (condition == null || action == null)
            {
                throw new ArgumentNullException("Neither Condition nor action argument may be null");
            }

            // Populate properties.
            this.Condition = condition;
            this.Action = action;
            this.Otherwise = otherwise;
        }

        // TODO: Action and alternative blocks not being handled, for debugging purposes.
        public override LLVMValueRef Emit(PipeContext<LLVMBuilderRef> context)
        {
            // Emit the condition value.
            LLVMValueRef conditionValue = this.Condition.Emit(context);

            // Create a zero-value double for the boolean comparison.
            LLVMValueRef zero = LLVM.ConstReal(PrimitiveTypeFactory.Double().Emit(), 0.0);

            // TODO: Hard-coded name.
            // Build the comparison, condition will be convered to a boolean for a 'ONE' (non-equal) comparison.
            LLVMValueRef comparison = LLVM.BuildFCmp(context.Target, LLVMRealPredicate.LLVMRealONE, conditionValue, zero, "ifcond");

            // Retrieve the parent function from the builder.
            LLVMValueRef function = LLVM.GetBasicBlockParent(LLVM.GetInsertBlock(context.Target));

            LLVMBasicBlockRef action = LLVM.AppendBasicBlock(function, "then");

            // TODO: Debugging, Ret void for action.
            LLVM.PositionBuilderAtEnd(context.Target, action);
            LLVM.BuildRetVoid(context.Target);

            LLVMBasicBlockRef otherwise = LLVM.AppendBasicBlock(function, "else");

            // TODO: Debugging, Ret void for otherwise.
            LLVM.PositionBuilderAtEnd(context.Target, otherwise);
            LLVM.BuildRetVoid(context.Target);

            LLVMBasicBlockRef merge = LLVM.AppendBasicBlock(function, "ifcont");

            // TODO: Debugging, Ret void for merge.
            LLVM.PositionBuilderAtEnd(context.Target, merge);
            LLVM.BuildRetVoid(context.Target);

            // Build the if construct.
            LLVMValueRef @if = LLVM.BuildCondBr(context.Target, comparison, action, otherwise);

            // TODO: Complete implementation, based off: https://github.com/microsoft/LLVMSharp/blob/master/KaleidoscopeTutorial/Chapter5/KaleidoscopeLLVM/CodeGenVisitor.cs#L214
            // ...

            // TODO: Debugging, not complete.
            LLVM.PositionBuilderAtEnd(context.Target, action);

            // Return the resulting LLVM value reference for futher use if applicable.
            return @if;
        }
    }
}
