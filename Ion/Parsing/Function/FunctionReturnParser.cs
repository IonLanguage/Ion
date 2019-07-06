using Ion.Generation;
using Ion.Syntax;

namespace Ion.Parsing
{
    public class FunctionReturnParser : IParser<Construct>
    {
        public Construct Parse(ParserContext context)
        {
            // Ensure current return keyword.
            context.Stream.EnsureCurrent(TokenType.KeywordReturn);

            // Skip over the return keyword.
            context.Stream.Skip();

            // Capture the current token.
            Token token = context.Stream.Current;

            // There is no return expression.
            if (token.Type == TokenType.SymbolSemiColon)
            {
                // Skip the semi-colon.
                context.Stream.Skip();

                // TODO: Should return void? Investigate.
                // Return null as no expression/return value was captured.
                return null;
            }

            // Otherwise, invoke the expression parser.
            Construct expr = new ExprParser().Parse(context);

            // Ensure current is a semi-colon.
            context.Stream.EnsureCurrent(TokenType.SymbolSemiColon);

            // Skip the semi-colon.
            context.Stream.Skip();

            // Return the expression.
            return expr;
        }
    }
}
