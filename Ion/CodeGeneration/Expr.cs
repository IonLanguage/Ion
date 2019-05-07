using System;
using Ion.CodeGeneration.Structure;
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

        String,

        Boolean,

        ExternalDefinition
    }

    public abstract class Expr : Named, IEntity<LLVMValueRef, LLVMBuilderRef>
    {
        public static Action<LLVMBuilderRef> Void = builder => { LLVM.BuildRetVoid(builder); };

        public abstract ExprType Type { get; }

        public string FunctionCallTarget { get; set; }

        public abstract LLVMValueRef Emit(LLVMBuilderRef context);
    }
}