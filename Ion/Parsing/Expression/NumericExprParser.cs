using System;
using Ion.Generation;
using Ion.CognitiveServices;
using Ion.Syntax;

namespace Ion.Parsing
{
    public class NumericExprParser : IParser<NumericExpr>
    {
        public NumericExpr Parse(ParserContext context)
        {
            // Consume numeric literal token.
            Token token = context.Stream.Current;

            // Skip numeric literal token.
            context.Stream.Skip();

            // Ensure captured token is numeric.
            if (!TokenIdentifier.IsNumeric(token))
            {
                throw new Exception($"Expected token to be classified as numeric, but got '{token.Type}'");
            }

            // Create the numeric expression entity.
            NumericExpr numericExpr = new NumericExpr(token.Type, Resolver.PrimitiveType(token), token.Value);

            // Return the numeric expression entity.
            return numericExpr;
        }
    }
}
