using System;
using Ion.CodeGeneration;
using Ion.CognitiveServices;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class BooleanExprParser : IParser<BooleanExpr>
    {
        public BooleanExpr Parse(ParserContext context)
        {
            // Consume boolean literal token.
            Token token = context.Stream.Current;

            // Skip boolean literal token.
            context.Stream.Skip();

            // Ensure captured token is a boolean.
            if (!TokenIdentifier.IsBoolean(token.Type))
            {
                throw new Exception($"Expected token type to be boolean, but got '{token.Type}'");
            }

            // Create the boolean expression entity.
            BooleanExpr booleanExpr = new BooleanExpr(token.Type, token.Value);

            // Return the boolean expression entity.
            return booleanExpr;
        }
    }
}
