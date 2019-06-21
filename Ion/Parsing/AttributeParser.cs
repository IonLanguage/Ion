using Ion.Generation;
using Ion.Syntax;

namespace Ion.Parsing
{
    public class AttributeParser : IParser<Attribute>
    {
        public Attribute Parse(ParserContext context)
        {
            // Ensure current token is bracket start.
            context.Stream.EnsureCurrent(TokenType.SymbolBracketL);

            // Skip bracket start.
            context.Stream.Skip();

            // Create the native attribute flag.
            bool native = false;

            // Attribute is native.
            if (context.Stream.Current.Type == TokenType.OperatorLessThan)
            {
                // Raise the native attribute flag.
                native = true;

                // Skip over operator less than.
                context.Stream.Skip();
            }

            // Invoke identifier parser.
            string identifier = new IdentifierParser().Parse(context);

            // If the attribute was flagged as native, skip the remaining delimiter.
            if (native)
            {
                // Ensure current token is operator greater than.
                context.Stream.EnsureCurrent(TokenType.OperatorGreaterThan);

                // Skip over operator greater then.
                context.Stream.Skip();
            }

            // Ensure current token is bracket end.
            context.Stream.EnsureCurrent(TokenType.SymbolBracketR);

            // Skip bracket end.
            context.Stream.Skip();

            // Create the attribute entity.
            Attribute attribute = new Attribute(identifier, native);

            // Return the attribute entity.
            return attribute;
        }
    }
}
