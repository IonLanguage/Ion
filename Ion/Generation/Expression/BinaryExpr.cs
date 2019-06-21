using System;
using Ion.Generation.Helpers;
using Ion.Syntax;
using LLVMSharp;
using static Ion.Syntax.Constants;

namespace Ion.Generation
{
    public class BinaryExpr : Expr
    {
        public override ExprType ExprType => ExprType.BinaryExpression;

        protected readonly TokenType operation;

        protected readonly int precedence;

        protected readonly Expr leftSide;

        protected readonly Expr rightSide;

        public BinaryExpr(TokenType operation, Expr leftSide, Expr rightSide, int precedence)
        {
            this.operation = operation;
            this.leftSide = leftSide;
            this.rightSide = rightSide;
            this.precedence = precedence;
        }

        public override LLVMValueRef Emit(PipeContext<LLVMBuilderRef> context)
        {
            // Ensure operation is registered.
            if (!Constants.operatorBuilderMap.ContainsKey(this.operation))
            {
                throw new Exception($"Unexpected unsupported operation: {this.operation}");
            }

            // Create the operation invoker.
            SimpleMathBuilderInvoker invoker = Constants.operatorBuilderMap[this.operation];

            // TODO: Side expressions emitting to context.
            // Invoke the operation generator.
            LLVMValueRef result = invoker(context.Target, this.leftSide.Emit(context), this.rightSide.Emit(context), this.Identifier);

            // Return resulting operation.
            return result;
        }
    }
}
