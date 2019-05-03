using System;
using Ion.CodeGeneration;
using Ion.CognitiveServices;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    /// <summary>
    ///     Parser right-side of a binary operation.
    ///     See https://llvm.org/docs/tutorial/LangImpl02.html @ ParseBinOpRHS().
    /// </summary>
    public class BinaryOpRightSideParser : IParser<Expr>
    {
        protected readonly int minimalPrecedence;

        protected Expr leftSide;

        public BinaryOpRightSideParser(Expr leftSide, int minimalPrecedence)
        {
            this.leftSide = leftSide;
            this.minimalPrecedence = minimalPrecedence;
        }

        public Expr Parse(TokenStream stream)
        {
            // If this is a binary operation, find it's precedence.
            while (true)
            {
                var firstPrecedence = Precedence.Get(stream.Get());

                /*
                If this is a binary operation that binds at least as tightly
                as the current binary operation, consume it. Otherwise, the process
                is complete.
                */
                if (firstPrecedence < minimalPrecedence) return leftSide;

                // At this point, it's a binary operation.
                TokenType binaryOperator = stream.Get().Type;

                // TODO: Should check if it's a BINARY operator, not just an operator.
                // Ensure the captured operator is validated.
                if (!TokenIdentifier.IsOperator(binaryOperator))
                    throw new Exception(
                        $"Expected token to be a binary operator but got token type '{binaryOperator}'");

                // Skip operator.
                stream.Skip();

                // Parse the right-side.
                Expr rightSide = new PrimaryExprParser().Parse(stream);

                // Ensure that the right-side was successfully parsed.
                if (rightSide == null) throw new Exception("Unable to parse the right-side of the binary expression");

                // Determine the token precedence of the current token.
                var secondPrecedence = Precedence.Get(stream.Get());

                /*
                If binary operator binds less tightly with the right-side than
                the operator after right-side, let the pending operator take the
                right-side as its left-side.
                */
                if (firstPrecedence < secondPrecedence)
                {
                    // Invoke the right-side parser.
                    rightSide = new BinaryOpRightSideParser(rightSide, firstPrecedence + 1).Parse(stream);

                    // Ensure the right-side was successfully parsed.
                    if (rightSide == null)
                        throw new Exception("Unable to parse the right-side of the binary expression");
                }

                // Create the binary expression entity.
                var binaryExpr = new BinaryExpr(leftSide, rightSide, firstPrecedence);

                // Merge left-side/right-side.
                leftSide = binaryExpr;
            }
        }
    }
}