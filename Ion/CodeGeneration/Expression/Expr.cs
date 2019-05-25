using System;
using Ion.CodeGeneration.Helpers;
using Ion.Misc;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public enum ExprType
    {
        UnaryExpression,

        BinaryExpression,

        FunctionCall,

        FunctionReturn,

        VariableReference,

        VariableDeclaration,

        Value,

        FunctionCallArgument,

        Numeric,

        StringLiteral,

        Boolean,

        ExternalDefinition,

        Pipe,

        Lambda,

        If,

        Struct
    }

    public abstract class Expr : Named, IPipe<LLVMBuilderRef, LLVMValueRef>
    {
        // TODO: Expand this.
        public static Action<LLVMBuilderRef> Void = builder => { LLVM.BuildRetVoid(builder); };

        public abstract ExprType Type { get; }

        public string FunctionCallTarget { get; set; }

        public abstract LLVMValueRef Emit(PipeContext<LLVMBuilderRef> context);
    }
}
