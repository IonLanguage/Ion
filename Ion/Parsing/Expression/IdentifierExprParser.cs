using System;
using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class IdentifierExprParser : IParser<Expr>
    {
        public Expr Parse(ParserContext context)
        {
            // Capture current token.
            Token token = context.Stream.Get();

            // Ensure captured token is an identifier.
            context.Stream.EnsureCurrent(TokenType.Identifier);

            // Capture identifier token value.
            string identifier = token.Value;

            // Variable reference.
            if (context.Stream.Peek().Type != TokenType.SymbolParenthesesL)
            {
                // TODO: Should be done by an independent parser (VariableExprParser)?
                // Skip identifier.
                context.Stream.Skip();

                // Create and return the variable expression.
                return new VariableExpr(identifier);
            }

            // Otherwise, it's a function call. Invoke the function call parser.
            FunctionCallExpr functionCallExpr = new FunctionCallExprParser().Parse(context);

            // Return the function call entity.
            return functionCallExpr;
        }
    }
}
