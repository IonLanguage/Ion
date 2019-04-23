using System;
using LLVMSharp;
using LlvmSharpLang.CodeGeneration.Structure;

namespace LlvmSharpLang.CodeGeneration
{
    public enum ExprType
    {
        UnaryExpression,

        BinaryExpression,

        FunctionCall,

        FunctionReturn,

        Variable,

        Value,

        FunctionCallArgument,

        Numeric
    }

    public abstract class Expr : Named, IEntity<LLVMValueRef, LLVMBuilderRef>
    {
        public abstract ExprType Type { get; }

        public string FunctionCallTarget { get; set; }

        public static Action<LLVMBuilderRef> Void = (builder) =>
        {
            LLVM.BuildRetVoid(builder);
        };

        public abstract LLVMValueRef Emit(LLVMBuilderRef context);
    }
}
