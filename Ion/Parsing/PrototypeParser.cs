using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;
using Ion.Misc;

namespace Ion.Parsing
{
    public class PrototypeParser : IParser<Prototype>
    {
        public Prototype Parse(TokenStream stream)
        {
            // Parse the return type.
            Type returnType = new TypeParser().Parse(stream);

            // Capture identifier.
            string identifier = stream.Next(TokenType.Identifier).Value;

            // Invoke the formal argument parser.
            FormalArgs args = new FormalArgsParser().Parse(stream);

            // Create the resulting prototype entity.
            Prototype prototype = new Prototype(identifier, args, returnType);

            // Return prototype.
            return prototype;
        }
    }
}
