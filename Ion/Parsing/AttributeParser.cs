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

            // Capture the attribute name.
            string name = context.Stream.Get(TokenType.Identifier).Value;

            // Skip identifier token.
            context.Stream.Skip();

            // Create the attribute entity.
            Attribute attribute = new Attribute();

            // Assign the captured name.
            attribute.SetName(name);

            // Ensure current token is bracket end.
            context.Stream.EnsureCurrent(TokenType.SymbolBracketR);

            // Skip bracket end.
            context.Stream.Skip();

            // Return the attribute entity.
            return attribute;
        }
    }
}
