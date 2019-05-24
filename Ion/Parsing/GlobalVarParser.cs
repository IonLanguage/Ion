using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class GlobalVarParser : IParser<GlobalVar>
    {
        public GlobalVar Parse(ParserContext context)
        {
            // Invoke type parser.
            Type type = new TypeParser().Parse(context);

            // Expect current token to be symbol at.
            context.Stream.EnsureCurrent(TokenType.SymbolAt);

            // Skip symbol at token.
            context.Stream.Skip();

            // Invoke identifier parser.
            string identifier = new IdentifierParser().Parse(context);

            // Ensure current token is identifier.
            context.Stream.EnsureCurrent(TokenType.Identifier);

            // Skip identifier.
            context.Stream.Skip();

            // Create the global variable.
            GlobalVar globalVar = new GlobalVar(identifier, type);

            // Return the global variable.
            return globalVar;
        }
    }
}
