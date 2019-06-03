using System;
using System.Collections.Generic;
using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class StructBodyParser : IParser<List<StructProperty>>
    {
        public List<StructProperty> Parse(ParserContext context)
        {
            // Ensure current token is block start.
            context.Stream.EnsureCurrent(TokenType.SymbolBlockL);

            // Skip block start token.
            context.Stream.Skip();

            // Create the body's property list.
            List<StructProperty> properties = new List<StructProperty>();

            // Create the index counter.
            int indexCounter = 0;

            // Begin parsing properties.
            context.Stream.NextUntil(TokenType.SymbolBlockR, (Token token) =>
            {
                // Invoke struct property parser.
                StructProperty property = new StructPropertyParser(indexCounter).Parse(context);

                // Append the property.
                properties.Add(property);

                // Increment the index counter.
                indexCounter++;

                // Retrieve the current token's type.
                TokenType currentTokenType = context.Stream.Current.Type;

                // Ensure current token is of type block end or symbol comma.
                if (currentTokenType != TokenType.SymbolBlockR && currentTokenType != TokenType.SymbolComma)
                {
                    throw new Exception($"Expected token to be of type symbol block end or comma but got '{currentTokenType}'");
                }
                // Skip comma token.
                else if (currentTokenType == TokenType.SymbolComma)
                {
                    context.Stream.Skip();
                }

                // Signal to always update the buffer with the current token.
                return true;
            });

            // Ensure current token is block end.
            context.Stream.EnsureCurrent(TokenType.SymbolBlockR);

            // Skip symbol block end token.
            context.Stream.Skip();

            // Return the resulting struct properties.
            return properties;
        }
    }
}
