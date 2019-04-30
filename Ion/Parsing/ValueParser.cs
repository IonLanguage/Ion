using System;
using Ion.CodeGeneration;
using Ion.CognitiveServices;
using Ion.Misc;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class ValueParser : IParser<Value>
    {
        public Value Parse(TokenStream stream)
        {
            // Consume the value string.
            string value = stream.Next().Value;

            // Attempt to identify value string type.
            TokenType? type = TokenIdentifier.IdentifyComplex(value);

            // Ensure type has successfully been identified.
            if (!type.HasValue)
            {
                throw new Exception("Unable to identify literal token type");
            }

            // Create and return the value.
            return new Value(Resolvers.TypeFromTokenType(type.Value), type.Value, value);
        }
    }
}
