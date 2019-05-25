using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class StructParser : IParser<Struct>
    {
        public Struct Parse(ParserContext context)
        {
            // Ensure current token is keyword new.
            context.Stream.EnsureCurrent(TokenType.KeywordNew);

            // Skip new keyword token.
            context.Stream.Skip();

            // Invoke identifier parser.
            string identifier = new IdentifierParser().Parse(context);

            // TODO: Inline-property definitions support missing.
            // Create the resulting struct.
            Struct @struct = new Struct(identifier);

            // Return the resulting struct.
            return @struct;
        }
    }
}
