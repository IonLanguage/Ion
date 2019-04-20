using System;
using LLVMSharp;
using LlvmSharpLang.CodeGeneration;
using LlvmSharpLang.CodeGeneration.Structure;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.CodeGeneration
{
    public delegate LLVMValueRef BinaryExprCreator(LLVMBuilderRef builder, LLVMValueRef leftSide, LLVMValueRef rightSide, string name);

    public enum OperationType
    {
        Addition,

        Substraction,

        Multiplication,

        Division,

        Modulo,

        // TODO: Implement for exponent.
        Exponent
    }

    public class BinaryExpr : Named, IEntity<LLVMValueRef, LLVMBuilderRef>
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
            // Ensure that the left-side and right-side value is set.
            else if (!this.LeftSide.HasValue || !this.RightSide.HasValue)
            {
                throw new Exception("Expected left-side and right-side value to be set");
            }
            // If the operation type is mapped, use it.
            else if (Constants.mathOperationDelegates.ContainsKey(this.Type.Value))
            {
                return Constants.mathOperationDelegates[this.Type.Value](context, this.LeftSide.Value, this.RightSide.Value, this.Name);
            }

            throw new Exception("Operation type not supported");
        }
    }
}
