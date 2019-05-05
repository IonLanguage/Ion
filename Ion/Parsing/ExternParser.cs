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

            // Create the external definition entity using the parsed prototype.
            Extern external = new Extern(prototype);

            // Return the external definition entity.
            return external;
        }
    }
}
