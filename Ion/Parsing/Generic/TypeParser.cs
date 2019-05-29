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
                // TODO: Invoke expression parser to capture array length.
                // TODO: arrayLength.Value = new ExprParser().Parse(context);

                // BEGIN temporary solution.

                // Ensure current token is an integer.
                context.Stream.EnsureCurrent(TokenType.LiteralInteger);

                // Capture the current integer token.
                Token integerToken = context.Stream.Get();

                // Assign the token value as the array length.
                arrayLength = uint.Parse(integerToken.Value);

                // Skip over the captured integer token.
                context.Stream.Skip();

                // END temporary solution.


                // Ensure the current token is a closing bracket.
                context.Stream.EnsureCurrent(TokenType.SymbolBracketR);

                // Skip bracket end token.
                context.Stream.Skip();
            }

            // Create the type.
            return new Type(context.SymbolTable, token, arrayLength);
        }
    }
}
