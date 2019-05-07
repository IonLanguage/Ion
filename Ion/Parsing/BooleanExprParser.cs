using System;
using Ion.CodeGeneration;
using Ion.CognitiveServices;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class BooleanExprParser : IParser<BooleanExpr>
    {
        public BooleanExpr Parse(TokenStream stream)
        {
            // Consume boolean literal token.
            Token token = stream.Get();

            // Skip boolean literal token.
            stream.Skip();

            // Ensure captured token is a boolean.
            if (!TokenIdentifier.IsBoolean(token.Type))
            {
                throw new Exception($"Expected token type to be boolean, but got '{token.Type}'");
            }

            // Create the boolean expression entity.
            BooleanExpr booleanExpr = new BooleanExpr(token.Type, Resolvers.TypeFromToken(token), token.Value);

            // Return the boolean expression entity.
            return booleanExpr;
        }
    }
}
