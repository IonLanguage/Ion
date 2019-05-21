using Ion.CodeGeneration;
using Ion.CognitiveServices;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class StructParser : IParser<Struct>
    {
        public Struct Parse(ParserContext context)
        {
            // Ensure current token is struct keyword.
            context.Stream.EnsureCurrent(TokenType.KeywordStruct);

            // Skip struct keyword, capture struct identifier.
            string identifier = context.Stream.Next(TokenType.Identifier).Value;

            // Skip identifier token onto block start.
            context.Stream.Skip(TokenType.SymbolBlockL);

            // Skip block start token.
            context.Stream.Skip();

            // Create the prototype.
            StructPrototype prototype = new StructPrototype();

            // Start iteration with callback.
            context.Stream.NextUntil(TokenType.SymbolBlockR, (Token token) =>
            {
                // Invoke type parser.
                Type type = new TypeParser().Parse(context);

                // Invoke identifier parser.
                string name = new IdentifierParser().Parse(context);

                // Create property.
                StructProperty property = new StructProperty(type, name);

                // Attach property to the prototype.
                prototype.Properties.Add(property);
            });

            // Ensure current token type is block end.
            context.Stream.EnsureCurrent(TokenType.SymbolBlockR);

            // Skip block end token.
            context.Stream.Skip();

            // Create the struct construct.
            Struct @struct = new Struct(identifier, prototype);

            // Return the resulting struct construct.
            return @struct;
        }
    }
}
