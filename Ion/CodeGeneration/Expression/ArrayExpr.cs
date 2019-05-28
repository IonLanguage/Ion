using Ion.CodeGeneration.Helpers;
using LLVMSharp;
using System;

namespace Ion.CodeGeneration
{
    public class ArrayExpr : Expr
    {
        public override ExprType ExprType => ExprType.Array;

        public Type Type { get; }

        public Expr[] Values { get; }

        public ArrayExpr(Type type, Expr[] values)
        {
            this.Type = type;
            this.Values = values;
        }

        public override LLVMValueRef Emit(PipeContext<LLVMBuilderRef> context)
        {
            // TODO: Implement.
            throw new NotImplementedException();
        }
    }
}
