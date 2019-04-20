using System;
using LLVMSharp;
using LlvmSharpLang.CodeGeneration.Structure;

namespace LlvmSharpLang.CodeGeneration
{
    public enum ExprType
    {
        Operation,

        FunctionCall
    }

    public class Expr : IStatement, IUncontextedEntity<LLVMValueRef>
    {
        public StatementType StatementType => StatementType.Expression;

        public LLVMValueRef? ExplicitValue { get; set; }

        public ExprType Type { get; set; }

        public string FunctionCallTarget { get; set; }

        public static Action<LLVMBuilderRef> Void = (builder) =>
        {
            LLVM.BuildRetVoid(builder);
        };

        public LLVMValueRef Emit()
        {
            // An explicit value has been set.
            if (this.ExplicitValue.HasValue)
            {
                return this.ExplicitValue.Value;
            }

            // TODO: Implement.
            throw new NotImplementedException();
        }
    }
}
