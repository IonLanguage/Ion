using System;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class BinaryExpr : Expr
    {
        protected readonly Expr leftSide;

        protected readonly int precedence;

        protected readonly Expr rightSide;

        public BinaryExpr(Expr leftSide, Expr rightSide, int precedence)
        {
            this.leftSide = leftSide;
            this.rightSide = rightSide;
            this.precedence = precedence;
        }

        public override ExprType Type => ExprType.BinaryExpression;

        public override LLVMValueRef Emit(LLVMBuilderRef context)
        {
            // TODO: Implement.
            throw new NotImplementedException();
        }
    }
}