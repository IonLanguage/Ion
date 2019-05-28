using Ion.CodeGeneration.Helpers;
using LLVMSharp;
using System;
using System.Collections.Generic;

namespace Ion.CodeGeneration
{
    public class ArrayExpr : Expr
    {
        public override ExprType ExprType => ExprType.Array;

        public ITypeEmitter Type { get; }

        public Expr[] Values { get; }

        public ArrayExpr(ITypeEmitter type, Expr[] values)
        {
            this.Type = type;
            this.Values = values;
        }

        public override LLVMValueRef Emit(PipeContext<LLVMBuilderRef> context)
        {
            // Prepare the value buffer list.
            List<LLVMValueRef> values = new List<LLVMValueRef>();

            // Iterate and emit all the values onto the buffer list.
            foreach (Expr value in this.Values)
            {
                // Emit the value onto the context.
                values.Add(value.Emit(context));
            }

            // Create the array.
            LLVM.ConstArray(this.Type.Emit(), values.ToArray());

            // TODO: Finish implementing.
            throw new NotImplementedException();
        }
    }
}
