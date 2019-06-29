using Ion.Generation;
using Ion.Syntax;

namespace Ion.Parsing
{
    public class StructPropertyParser : IParser<StructProperty>
    {
        protected readonly int index;

        public StructPropertyParser(int index)
        {
            this.index = index;
        }

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
            StructProperty property = new StructProperty(identifier, value, this.index);

            // Return the resulting property construct.
            return property;
        }
    }
}
