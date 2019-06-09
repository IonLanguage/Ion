using System.Collections.Generic;
using Ion.CodeGeneration;
using Ion.CognitiveServices;
using Ion.Syntax;

namespace Ion.Parsing
{
    public class StructDefParser : IParser<StructDef>
    {
        public StructDef Parse(ParserContext context)
        {
            // Ensure current token is struct keyword.
            context.Stream.EnsureCurrent(TokenType.KeywordStruct);

            // Skip struct keyword token.
            context.Stream.Skip();

            // Invoke identifier parser.
            string identifier = new IdentifierParser().Parse(context);

            // Ensure current token is block start.
            context.Stream.EnsureCurrent(TokenType.SymbolBlockL);

            // Skip block start token.
            context.Stream.Skip();

            // Create the properties buffer list.
            List<StructDefProperty> properties = new List<StructDefProperty>();

            // Start iteration with callback.
            context.Stream.NextUntil(TokenType.SymbolBlockR, (Token token) =>
            {
                // Invoke type parser.
                Type type = new TypeParser().Parse(context);

                // Invoke identifier parser.
                string name = new IdentifierParser().Parse(context);

                // Ensure current token is symbol semi-colon.
                context.Stream.EnsureCurrent(TokenType.SymbolSemiColon);

                // Create property.
                StructDefProperty property = new StructDefProperty(type, name);

                // Attach property to the prototype.
                properties.Add(property);

                // Continue normal iteration.
                return false;
            });

            // Create the body construct.
            StructDefBody body = new StructDefBody(properties);

            // Ensure current token type is block end.
            context.Stream.EnsureCurrent(TokenType.SymbolBlockR);

            // Skip block end token.
            context.Stream.Skip();

            // Create the struct construct.
            StructDef @struct = new StructDef(identifier, body);

            // Return the resulting struct construct.
            return @struct;
        }
    }
}
