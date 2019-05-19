using System;
using Ion.CodeGeneration.Structure;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class IfExpr : Expr
    {
        public override ExprType Type => ExprType.If;

        public Expr Condition { get; }

        public Block Action { get; }

        public Block Otherwise { get; }

        public IfExpr(Expr condition, Block action, Block otherwise = null)
        {
            // Ensure condition and action are set.
            if (condition == null || action == null)
            {
                throw new ArgumentNullException("Neither Condition nor action argument may be null");
            }

            // Populate properties.
            this.Condition = condition;
            this.Action = action;
            this.Otherwise = otherwise;
        }

        public override LLVMValueRef Emit(PipeContext<LLVMBuilderRef> context)
        {
            // TODO: Implement.
            throw new NotImplementedException();
        }
    }
}
