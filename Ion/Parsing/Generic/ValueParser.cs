using System;
using Ion.CodeGeneration;
using Ion.CognitiveServices;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class ValueParser : IParser<Value>
    {
        public Value Parse(ParserContext context)
        {
            // Consume the value string.
            string value = context.Stream.Current.Value;

            // Attempt to identify value string type.
            TokenType? type = TokenIdentifier.IdentifyComplex(value);

            // Ensure type has successfully been identified.
            if (!type.HasValue)
            {
                throw new Exception("Unable to identify literal token type");
            }

            // Create and return the value.
            return new Value(Resolvers.PrimitiveType(type.Value), type.Value, value);
        }
    }
}
