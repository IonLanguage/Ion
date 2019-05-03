using System.Collections.Generic;
using Ion.CodeGeneration.Structure;
using Ion.Misc;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public enum BlockType
    {
        Default,

        Short
    }

    public class Block : Named, IEntity<LLVMBasicBlockRef, LLVMValueRef>
    {
        public readonly List<Expr> Expressions;

        public Block()
        {
            this.Expressions = new List<Expr>();
        }

        public Expr ReturnExpr { get; set; }

        public BlockType Type { get; set; }

        // TODO: Find a better way to cache emitted values.
        public LLVMBasicBlockRef Current { get; protected set; }

        public LLVMBasicBlockRef Emit(LLVMValueRef context)
        {
            // Create the block and its corresponding builder.
            LLVMBasicBlockRef block = LLVM.AppendBasicBlock(context, this.Name);
            LLVMBuilderRef builder = LLVM.CreateBuilder();

            // Position and link the builder.
            LLVM.PositionBuilderAtEnd(builder, block);

            // Emit the expressions.
            this.Expressions.ForEach(expression => { expression.Emit(builder); });

            // No value was returned.
            if (this.ReturnExpr == null)
                LLVM.BuildRetVoid(builder);
            // Otherwise, emit the set return value.
            else
                LLVM.BuildRet(builder, this.ReturnExpr.Emit(builder));

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