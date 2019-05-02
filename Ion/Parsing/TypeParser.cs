using System;
using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;
using Ion.CognitiveServices;

namespace Ion.Parsing
{
    public class TypeParser : IParser<CodeGeneration.Type>
    {
        public CodeGeneration.Type Parse(TokenStream stream)
        {
            // Consume type token.
            Token type = stream.Next();

            // Ensure type value is a type.
            if (!TokenIdentifier.IsType(type))
            {
                throw new Exception($"Expected a type but got '{type.Type}'");
            }

            // Create the type.
            return new CodeGeneration.Type(type.Value);
        }
    }
}
