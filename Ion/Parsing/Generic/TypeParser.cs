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
            // Capture current type token.
            Token token = context.Stream.Get();

            // Ensure current token is a type.
            if (!TokenIdentifier.IsType(token, context))
            {
                throw new Exception($"Expected a type but got '{token.Type}'");
            }

            // Skip current token.
            context.Stream.Skip();

            // Create the type.
            return new Type(context.SymbolTable, token);
        }
    }
}
