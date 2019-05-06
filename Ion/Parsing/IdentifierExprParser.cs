using System;
using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class IdentifierExprParser : IParser<Expr>
    {
        public Expr Parse(TokenStream stream)
        {
            // Capture current token.
            Token token = stream.Get();

            // Ensure captured token is an identifier.
            stream.EnsureCurrent(TokenType.Identifier);

            // Capture identifier token value.
            string identifier = token.Value;

            // Variable reference.
            if (stream.Peek().Type != TokenType.SymbolParenthesesL)
            {
                // TODO: Should be done by an independent parser (VariableExprParser)?
                // Skip identifier.
                stream.Skip();

                // Create and return the variable expression.
                return new VariableExpr(identifier);
            }

            // Otherwise, it's a function call. Invoke the function call parser.
            FunctionCallExpr functionCallExpr = new FunctionCallExprParser().Parse(stream);

            // Return the function call entity.
            return functionCallExpr;
        }
    }
}
