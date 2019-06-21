using System;
using Ion.Generation;
using Ion.CognitiveServices;
using Ion.Syntax;

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
            return new Value(Resolver.PrimitiveType(type.Value), type.Value, value);
        }
    }
}
