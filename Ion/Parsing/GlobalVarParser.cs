using Ion.CodeGeneration;
using Ion.Syntax;

namespace Ion.Parsing
{
    public class GlobalVarParser : IParser<Global>
    {
        public Global Parse(ParserContext context)
        {
            // Invoke type parser.
            Type type = new TypeParser().Parse(context);

            // Expect current token to be symbol at.
            context.Stream.EnsureCurrent(TokenType.SymbolAt);

            // Skip symbol at token.
            context.Stream.Skip();

            // Invoke identifier parser.
            string identifier = new IdentifierParser().Parse(context);

            // Create the global variable.
            Global globalVar = new Global(identifier, type);

            // Return the global variable.
            return globalVar;
        }
    }
}
