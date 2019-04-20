using System;
using LLVMSharp;
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
            else if (this.LeftSide == null)
            {
                throw new Exception("Expected left-side value to be set");
            }

            switch (this.Type.Value)
            {
                case OperationType.Addition:
                    {
                        // TODO
                        LLVM.BuildAdd(this.LeftSide, this.RightSide, this.Name);

                        break;
                    }

                default:
                    {
                        throw new Exception("Operation type not supported");
                    }
            }
        }
    }
}
