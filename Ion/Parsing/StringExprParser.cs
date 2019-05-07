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
            // Ensure current token is string literal.
            stream.EnsureCurrent(TokenType.LiteralString);

            // Capture string literal token.
            Token token = stream.Get();

            // Skip string literal token.
            stream.Skip();

            // TODO: Hard-coded.
            // Remove string quotes.
            string value = token.Value.Substring(1, token.Value.Length - 2);

            // Create the string expression entity.
            StringExpr stringExpr = new StringExpr(token.Type, Resolvers.TypeFromToken(token), value);

            // Return the string expression entity.
            return stringExpr;
        }
    }
}
