using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class PrototypeParser : IParser<Prototype>
    {
        public Prototype Parse(ParserContext context)
        {
            // Parse the return type.
            Type returnType = new TypeParser().Parse(context);

            // Capture identifier.
            string identifier = context.Stream.Get(TokenType.Identifier).Value;

            // Skip identifier.
            context.Stream.Skip();

            // Invoke the formal argument parser.
            FormalArgs args = new FormalArgsParser().Parse(context);

            // Create the resulting prototype entity.
            Prototype prototype = new Prototype(identifier, args, returnType);

            // Return prototype.
            return prototype;
        }
    }
}
