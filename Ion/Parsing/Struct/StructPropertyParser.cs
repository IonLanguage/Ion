using Ion.CodeGeneration;
using Ion.CodeGeneration.Helpers;
using Ion.SyntaxAnalysis;
using Ion.Tracking.Symbols;

namespace Ion.Parsing
{
    public class StructPropertyParser : IParser<StructProperty>
    {
        public StructProperty Parse(ParserContext context)
        {
            // Invoke identifier parser.
            string identifier = new IdentifierParser().Parse(context);

            // Ensure current token is symbol colon.
            context.Stream.EnsureCurrent(TokenType.SymbolColon);

            // Skip colon symbol token.
            context.Stream.Skip();

            // Invoke expression parser to capture the value.
            Expr value = new ExprParser().Parse(context);

            // Create the resulting property construct.
            StructProperty property = new StructProperty(identifier, value);

            // Return the resulting property construct.
            return property;
        }
    }
}
