using System;
using LLVMSharp;

namespace LlvmSharpLang.CodeGen
{
    public enum BlockType
    {
        Default,

        Short
    }

    public class Block : NamedValueEntity<LLVMBasicBlockRef>
    {
        public Expr ReturnValue { get; set; }

        public BlockType Type { get; set; }

        public override LLVMBasicBlockRef Emit(LLVMValueRef context)
        {
            LLVMBasicBlockRef block = LLVM.AppendBasicBlock(context, this.Name);
            LLVMBuilderRef builder = LLVM.CreateBuilder();

            // Position and link the builder.
            LLVM.PositionBuilderAtEnd(builder, block);

            // No value was returned.
            if (this.ReturnValue == null)
            {
                LLVM.BuildRetVoid(builder);
            }

            // Otherwise, emit the set return value.
            LLVM.BuildRet(builder, this.ReturnValue.Emit());

            return block;
        }

        public void SetEntryName()
        {
            this.SetName(SpecialName.entry);
        }
    }
}
