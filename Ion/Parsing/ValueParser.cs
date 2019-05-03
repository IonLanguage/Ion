using System;
using Ion.CodeGeneration;
using Ion.CognitiveServices;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class ValueParser : IParser<Value>
    {
        public Value Parse(TokenStream stream)
        {
            // Consume the value string.
            var value = stream.Next().Value;

            // Attempt to identify value string type.
            var type = TokenIdentifier.IdentifyComplex(value);

            // Ensure type has successfully been identified.
            if (!type.HasValue) throw new Exception("Unable to identify literal token type");

            // Create and return the value.
            return new Value(Resolvers.TypeFromTokenType(type.Value), type.Value, value);
        }
    }
}