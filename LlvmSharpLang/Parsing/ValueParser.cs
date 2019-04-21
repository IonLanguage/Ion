using System;
using LlvmSharpLang.CodeGeneration;
using LlvmSharpLang.CognitiveServices;
using LlvmSharpLang.Misc;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
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

            return new Value(Resolvers.TypeFromTokenType(type.Value), value);
        }
    }
}
