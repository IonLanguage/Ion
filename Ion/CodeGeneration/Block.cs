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
        public List<Expr> Expressions;

        public Block()
        {
            Expressions = new List<Expr>();
        }

        public Expr ReturnExpr { get; set; }

        public BlockType Type { get; set; }

        // TODO: Find a better way to cache emitted values.
        public LLVMBasicBlockRef Current { get; protected set; }

        public LLVMBasicBlockRef Emit(LLVMValueRef context)
        {
            // Create the block and its corresponding builder.
            LLVMBasicBlockRef block = LLVM.AppendBasicBlock(context, Name);
            LLVMBuilderRef builder = LLVM.CreateBuilder();

            // Position and link the builder.
            LLVM.PositionBuilderAtEnd(builder, block);

            // Emit the expressions.
            Expressions.ForEach(expression => { expression.Emit(builder); });

            // No value was returned.
            if (ReturnExpr == null)
                LLVM.BuildRetVoid(builder);
            // Otherwise, emit the set return value.
            else
                LLVM.BuildRet(builder, ReturnExpr.Emit(builder));

            // Cache emitted block.
            Current = block;

            return block;
        }

        public void SetNameEntry()
        {
            SetName(SpecialName.Entry);
        }
    }
}