using System;
using System.Collections.Generic;
using Ion.CodeGeneration.Helpers;
using Ion.Misc;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public enum BlockType
    {
        Default,

        Short
    }

    public class Block : Named, IPipe<LLVMValueRef, LLVMBasicBlockRef>
    {
        public readonly List<Expr> Expressions;

        public Expr ReturnExpr { get; set; }

        public BlockType Type { get; set; }

        // TODO: Find a better way to cache emitted values.
        public LLVMBasicBlockRef Current { get; protected set; }

        public Block()
        {
            this.Expressions = new List<Expr>();
        }

        public LLVMBasicBlockRef Emit(PipeContext<LLVMValueRef> context)
        {
            // Create the block.
            LLVMBasicBlockRef block = LLVM.AppendBasicBlock(context.Target, this.Name);

            // Create the block's builder.
            LLVMBuilderRef builder = block.CreateBuilder();

            // Derive a context for the builder.
            PipeContext<LLVMBuilderRef> builderContext = context.Derive<LLVMBuilderRef>(builder);

            // Position and link the builder.
            LLVM.PositionBuilderAtEnd(builder, block);

            // Emit the expressions.
            foreach (var expr in this.Expressions)
            {
                expr.Emit(builderContext);
            }

            // No value was returned.
            if (this.ReturnExpr == null)
            {
                LLVM.BuildRetVoid(builder);
            }
            // Otherwise, emit the set return value.
            else
            {
                // Emit the return expression.
                LLVM.BuildRet(builder, this.ReturnExpr.Emit(builderContext));
            }

            // Cache emitted block.
            this.Current = block;

            // Return the block.
            return block;
        }

        public void SetNameEntry()
        {
            this.SetName(SpecialName.Entry);
        }
    }
}
