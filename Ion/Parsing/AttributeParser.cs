using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class AttributeParser : IParser<Attribute>
    {
        public Attribute Parse(ParserContext context)
        {
            // Ensure current token is bracket start.
            context.Stream.EnsureCurrent(TokenType.SymbolBracketL);

            // Skip bracket start.
            context.Stream.Skip();

            // Invoke identifier parser.
            string identifier = new IdentifierParser().Parse(context);

            // Ensure current token is bracket end.
            context.Stream.EnsureCurrent(TokenType.SymbolBracketR);

            // Skip bracket end.
            context.Stream.Skip();

            // Create the attribute entity.
            Attribute attribute = new Attribute(identifier);

            // Return the attribute entity.
            return attribute;
        }
    }
}
