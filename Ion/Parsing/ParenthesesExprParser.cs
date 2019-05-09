using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class ParenthesesExprParser : IParser<Expr>
    {
        public Expr Parse(ParserContext context)
        {
            // Ensure current token is parentheses start.
            context.Stream.EnsureCurrent(TokenType.SymbolParenthesesL);

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
