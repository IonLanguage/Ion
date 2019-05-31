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

            // Create the void return flag.
            bool returnsVoid = false;

            // Mark as returning void if applicable.
            if (returnType.Token.Type == TokenType.TypeVoid)
            {
                returnsVoid = true;
            }

            // Invoke identifier parser.
            string identifier = new IdentifierParser().Parse(context);

            // Invoke the formal argument parser.
            FormalArgs args = new FormalArgsParser().Parse(context);

            // Create the resulting prototype entity.
            Prototype prototype = new Prototype(identifier, args, returnType, returnsVoid);

            // Return prototype.
            return prototype;
        }
    }
}
