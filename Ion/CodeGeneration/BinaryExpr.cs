using System;
using LLVMSharp;
using Ion.CodeGeneration;
using Ion.CodeGeneration.Structure;
using Ion.SyntaxAnalysis;

namespace Ion.CodeGeneration
{
    public class BinaryExpr : Expr
    {
        public override ExprType Type => ExprType.BinaryExpression;

        protected readonly Expr leftSide;

        protected readonly Expr rightSide;

        protected readonly int precedence;

        public BinaryExpr(Expr leftSide, Expr rightSide, int precedence)
        {
            this.leftSide = leftSide;
            this.rightSide = rightSide;
            this.precedence = precedence;
        }

        public override LLVMValueRef Emit(LLVMBuilderRef context)
        {
            // TODO: Implement.
            throw new NotImplementedException();
        }
    }
}
