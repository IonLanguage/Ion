using Ion.CognitiveServices;
using Ion.Generation;
using Ion.Syntax;

namespace Ion.Parsing
{
    public class ParenthesesExprParser : IParser<Construct>
    {
        public Construct Parse(ParserContext context)
        {
            // Ensure current token is parentheses start.
            context.Stream.EnsureCurrent(TokenType.SymbolParenthesesL);

            // Peek at the next token.
            Token peek = context.Stream.Peek();

            // Lambda expresion.
            if (TokenIdentifier.IsType(peek, context) || peek.Type == TokenType.SymbolParenthesesR)
            {
                // Delegate to the lambda expression parser.
                return new LambdaParser().Parse(context);
            }

            // Skip the parentheses start token.
            context.Stream.Skip();

            // Parse the expression.
            Construct expr = new ExprParser().Parse(context);

            // Ensure current token is parentheses end.
            context.Stream.EnsureCurrent(TokenType.SymbolParenthesesR);

            // Skip the parentheses end token.
            context.Stream.Skip();

            // Return the expression.
            return expr;
        }
    }
}
