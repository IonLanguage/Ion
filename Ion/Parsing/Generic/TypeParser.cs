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

            // Skip and capture the next token.
            Token nextToken = context.Stream.Next();

            // Create the array length (and flag), defaulting to null.
            uint? arrayLength = null;

            // Determine if type is an array.
            if (nextToken.Type == TokenType.SymbolBracketL)
            {
                // Skip bracket start token.
                context.Stream.Skip();

                // TODO: Must ensure array length is integer somehow.
                // Invoke expression parser to capture array length.
                // arrayLength.Value = new ExprParser().Parse(context);
                // TODO: Hard-coded value temporarily.
                arrayLength = 3;

                // Skip length token, onto bracket end token.
                context.Stream.Skip();

                // Skip bracket end token.
                context.Stream.Skip();
            }

            // Create the type.
            return new Type(context.SymbolTable, token, arrayLength);
        }
    }
}
