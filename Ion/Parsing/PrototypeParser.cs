using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;
using Ion.Misc;

namespace Ion.Parsing
{
    public class PrototypeParser : IParser<Prototype>
    {
        public Prototype Parse(TokenStream stream)
        {
            // Capture identifier.
            string identifier = stream.Next(TokenType.Identifier).Value;

            // Invoke the formal argument parser.
            FormalArgs args = new FormalArgsParser().Parse(stream);

            // Default return type to void
            CodeGeneration.Type returnType = TypeFactory.Void();

            // Check if the next symbol is a colon
            if (stream.Peek().Type == TokenType.SymbolColon)
            {
                // Skip ':' for return type.
                stream.Skip(TokenType.SymbolColon);

                // Parse the return type.
                returnType = new TypeParser().Parse(stream);
            }

            // Create the resulting prototype entity.
            Prototype prototype = new Prototype(identifier, args, returnType);

            // Return prototype.
            return prototype;
        }
    }
}
