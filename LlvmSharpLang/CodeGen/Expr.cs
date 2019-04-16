using System;
using LLVMSharp;
using LlvmSharpLang.CodeGen.Structure;

namespace LlvmSharpLang.CodeGen
{
    public class Expr : IStatement, IUncontextedEntity<LLVMValueRef>
    {
        public StatementType StatementType => StatementType.Expression;

        public LLVMValueRef? ExplicitValue { get; set; }

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
