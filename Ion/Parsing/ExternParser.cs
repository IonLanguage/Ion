using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class ExternParser : IParser<Extern>
    {
        public Extern Parse(TokenStream stream)
        {
            // Ensure current token is extern keyword.
            stream.EnsureCurrent(TokenType.KeywordExternal);

            // Skip extern keyword.
            stream.Skip();

            // Invoke the prototype parser.
            Prototype prototype = new PrototypeParser().Parse(stream);

            // Ensure current token is a semi-colon.
            stream.EnsureCurrent(TokenType.SymbolSemiColon);

            // Skip semi-colon.
            stream.Skip();

            // Create the external definition entity using the parsed prototype.
            Extern external = new Extern(prototype);

            // Return the external definition entity.
            return external;
        }
    }
}
