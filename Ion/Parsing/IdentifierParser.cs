using System;
using Ion.Misc;
using Ion.Syntax;

namespace Ion.Parsing
{
    public class IdentifierParser : IParser<string>
    {
        public string Parse(ParserContext context)
        {
            // Capture identifier.
            string identifier = context.Stream.Get(TokenType.Identifier).Value;

            // Validate captured identifier.
            if (!Util.ValidateIdentifier(identifier))
            {
                throw new Exception($"Invalid identifier: {identifier}");
            }

            // Skip identifier token.
            context.Stream.Skip();

            // Return the resulting identifier.
            return identifier;
        }
    }
}
