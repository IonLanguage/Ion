using System;
using Ion.CognitiveServices;
using Ion.SyntaxAnalysis;
using Type = Ion.CodeGeneration.Type;

namespace Ion.Parsing
{
    public class TypeParser : IParser<Type>
    {
        public Type Parse(ParserContext context)
        {
            // Consume current type token.
            Token type = context.Stream.Get();

            // Skip type.
            context.Stream.Skip();

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
