using System;
using Ion.CognitiveServices;
using Ion.SyntaxAnalysis;
using Type = Ion.CodeGeneration.Type;

namespace Ion.Parsing
{
    public class TypeParser : IParser<Type>
    {
        public Type Parse(TokenStream stream)
        {
            // Consume current type token.
            Token type = stream.Get();

            // Skip type.
            stream.Skip();

            // Ensure type value is a type.
            if (!TokenIdentifier.IsType(type))
            {
                throw new Exception($"Expected a type but got '{type.Type}'");
            }

            // Create the type.
            return new Type(type.Value);
        }
    }
}
