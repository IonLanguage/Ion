using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class ExternParser : IParser<Extern>
    {
        public Extern Parse(TokenStream stream)
        {
            // Skip current extern keyword onto identifier.
            stream.Skip(TokenType.Identifier, TokenType.KeywordExternal);

            // Invoke the prototype parser.
            Prototype prototype = new PrototypeParser().Parse(stream);

            // Create the external definition entity using the parsed prototype.
            Extern external = new Extern(prototype);

            // Return the external definition entity.
            return external;
        }
    }
}
