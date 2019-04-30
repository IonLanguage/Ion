using System;
using LLVMSharp;
using LlvmSharpLang.CodeGeneration.Structure;
using LlvmSharpLang.Misc;

namespace LlvmSharpLang.CodeGeneration
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

        // TODO: Find a better way to cache emitted values.
        public LLVMBasicBlockRef Current { get; protected set; }

        public LLVMBasicBlockRef Emit(LLVMValueRef context)
        {
            // Create the block and its corresponding builder.
            LLVMBasicBlockRef block = LLVM.AppendBasicBlock(context, this.Name);
            LLVMBuilderRef builder = LLVM.CreateBuilder();

            // Position and link the builder.
            LLVM.PositionBuilderAtEnd(builder, block);

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

            // Cache emitted block.
            this.Current = block;

            return block;
        }

        public void SetNameEntry()
        {
            this.SetName(SpecialName.Entry);
        }
    }
}
