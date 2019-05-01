using System.Collections.Generic;
using System;
using LLVMSharp;
using Ion.CodeGeneration.Structure;
using Ion.Misc;

namespace Ion.CodeGeneration
{
    public enum BlockType
    {
        Default,

        Short
    }

    public class Block : Named, IEntity<LLVMBasicBlockRef, LLVMValueRef>
    {
        public Expr ReturnExpr { get; set; }

        public BlockType Type { get; set; }

        public List<Expr> Expressions;

        public Block()
        {
            this.Expressions = new List<Expr>();
        }

        public LLVMBasicBlockRef Emit(LLVMValueRef context)
        {
            // Create the block and its corresponding builder.
            LLVMBasicBlockRef block = LLVM.AppendBasicBlock(context, this.Name);
            LLVMBuilderRef builder = LLVM.CreateBuilder();

            // Position and link the builder.
            LLVM.PositionBuilderAtEnd(builder, block);

            // Emit the expressions.
            this.Expressions.ForEach((Expr expression) =>
            {
                expression.Emit(builder);
            });

            // No value was returned.
            if (this.ReturnExpr == null)
            {
                LLVM.BuildRetVoid(builder);
            }
            // Otherwise, emit the set return value.
            else
            {
                LLVM.BuildRet(builder, this.ReturnExpr.Emit(builder));
            }

            return block;
        }

        public void SetNameEntry()
        {
            this.SetName(SpecialName.Entry);
        }
    }
}
