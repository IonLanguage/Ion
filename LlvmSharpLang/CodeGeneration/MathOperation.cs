using System;
using LLVMSharp;
using LlvmSharpLang.CodeGeneration;
using LlvmSharpLang.CodeGeneration.Structure;

namespace LlvmSharpLang.CodeGeneration
{
    public enum OperationType
    {
        Addition,

        Substraction,

        Multiplication,

        Division,

        Modulo,

        Exponent
    }

    public class MathOperation : Named, IEntity<LLVMValueRef, LLVMBuilderRef>
    {
        public OperationType? Type { get; set; }

        public LLVMValueRef? LeftSide { get; set; }

        public LLVMValueRef? RightSide { get; set; }

        public LLVMValueRef Emit(LLVMBuilderRef context)
        {
            // Ensure operation type is set.
            if (!this.Type.HasValue)
            {
                throw new Exception("Expected operation type to be set");
            }
            // Ensure at least the left side value is set.
            else if (!this.LeftSide.HasValue || !this.RightSide.HasValue)
            {
                throw new Exception("Expected left-side and right-side value to be set");
            }

            switch (this.Type.Value)
            {
                case OperationType.Addition:
                    {
                        return LLVM.BuildAdd(context, this.LeftSide.Value, this.RightSide.Value, this.Name);
                    }

                case OperationType.Substraction:
                    {
                        return LLVM.BuildSub(context, this.LeftSide.Value, this.RightSide.Value, this.Name);
                    }

                case OperationType.Multiplication:
                    {
                        // TODO: Types.
                        return LLVM.BuildMul(context, this.LeftSide.Value, this.RightSide.Value, this.Name);
                    }

                case OperationType.Division:
                    {
                        // TODO: Types.
                        return LLVM.BuildUDiv(context, this.LeftSide.Value, this.RightSide.Value, this.Name);
                    }
                case OperationType.Modulo:
                    {
                        // TODO: Types.
                        return LLVM.BuildSRem(context, this.LeftSide.Value, this.RightSide.Value, this.Name);
                    }

                default:
                    {
                        throw new Exception("Operation type not supported");
                    }
            }
        }
    }
}
