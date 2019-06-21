using Ion.Generation;
using Ion.CognitiveServices;
using Ion.Syntax;

namespace Ion.Parsing
{
    public class ParenthesesExprParser : IParser<Expr>
    {
        public Expr Parse(ParserContext context)
        {
            // Ensure current token is parentheses start.
            context.Stream.EnsureCurrent(TokenType.SymbolParenthesesL);

            // Peek at the next token.
            Token peek = context.Stream.Peek();

            // Lambda expresion.
            if (TokenIdentifier.IsType(peek, context) || peek.Type == TokenType.SymbolParenthesesR)
            {
                // Delegate to the lambda expression parser.
                return new LambdaExprParser().Parse(context);
            }

            // Skip the parentheses start token.
            context.Stream.Skip();

            // Parse the expression.
            Expr expr = new ExprParser().Parse(context);

            // Ensure current token is parentheses end.
            context.Stream.EnsureCurrent(TokenType.SymbolParenthesesR);

            // Skip the parentheses end token.
            context.Stream.Skip();

            // Return the expression.
            return expr;
        }
    }
}
