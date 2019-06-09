using Ion.CodeGeneration;
using Ion.Syntax;

namespace Ion.Parsing
{
    public class ExternParser : IParser<Extern>
    {
        public Extern Parse(ParserContext context)
        {
            // Ensure current token is extern keyword.
            context.Stream.EnsureCurrent(TokenType.KeywordExternal);

            // Skip extern keyword.
            context.Stream.Skip();

            // Invoke the prototype parser.
            Prototype prototype = new PrototypeParser().Parse(context);

            // Ensure current token is a semi-colon.
            context.Stream.EnsureCurrent(TokenType.SymbolSemiColon);

            // Skip semi-colon.
            context.Stream.Skip();

            // Create the external definition entity using the parsed prototype.
            Extern external = new Extern(prototype);

            // Return the external definition entity.
            return external;
        }
    }
}
