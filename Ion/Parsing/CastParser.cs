using Ion.Generation;
using Ion.Syntax;

namespace Ion.Parsing
{
    public class CastParser : IParser<Type>
    {
        public Type Parse(ParserContext context)
        {
            // Ensure current token is parentheses start.
            context.Stream.EnsureCurrent(TokenType.SymbolParenthesesL);

            // Skip parentheses start token.
            context.Stream.Skip();

            // Invoke type parser.
            Type target = new TypeParser().Parse(context);

            // Ensure current token is parentheses end.
            context.Stream.EnsureCurrent(TokenType.SymbolParenthesesR);

            // Skip parentheses end token.
            context.Stream.Skip();

            // Return the resulting target type.
            return target;
        }
    }
}
