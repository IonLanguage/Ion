using System;
using Ion.CodeGeneration;
using Ion.CognitiveServices;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class StringExprParser : IParser<StringExpr>
    {
        public StringExpr Parse(TokenStream stream)
        {
            // Consume string literal token.
            Token token = stream.Get();

            // Skip string literal token.
            stream.Skip();

            // Ensure captured token is a string.
            if (token.Type != TokenType.LiteralString)
            {
                throw new Exception($"Expected token type to be string, but got '{token.Type}'");
            }

            // Create the numeric expression entity.
            StringExpr numericExpr = new StringExpr(token.Type, Resolvers.TypeFromToken(token), token.Value);

            // Return the numeric expression entity.
            return numericExpr;
        }
    }
}
