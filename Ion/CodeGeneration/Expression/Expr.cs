using System;
using Ion.CodeGeneration.Helpers;
using Ion.Misc;
using LLVMSharp;
using Ion.IR.Generation;
using Ion.IR.Constructs;

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

        Struct,

        Array,

        Attribute
    }

    public abstract class Expr : Named, IContextPipe<InstructionBuilder, IConstruct>
    {
        // TODO: Expand this.
        public static Action<LLVMBuilderRef> Void = (builder) => LLVM.BuildRetVoid(builder);

        public abstract ExprType ExprType { get; }

        public string FunctionCallTarget { get; set; }

        public abstract IConstruct Emit(PipeContext<InstructionBuilder> context);
    }
}
