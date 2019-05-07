using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class AttributeParser : IParser<Attribute>
    {
        public Attribute Parse(TokenStream stream)
        {
            // Ensure current token is bracket start.
            stream.EnsureCurrent(TokenType.SymbolBracketL);

            // Skip bracket start.
            stream.Skip();

            // Capture the attribute name.
            string name = stream.Get(TokenType.Identifier).Value;

            // Skip identifier token.
            stream.Skip();

            // Create the attribute entity.
            Attribute attribute = new Attribute();

            // Assign the captured name.
            attribute.SetName(name);

            // Ensure current token is bracket end.
            stream.EnsureCurrent(TokenType.SymbolBracketR);

            // Skip bracket end.
            stream.Skip();

            // Return the attribute entity.
            return attribute;
        }
    }
}
