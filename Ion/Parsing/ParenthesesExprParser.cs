using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class ParenthesesExprParser : IParser<Expr>
    {
        public Expr Parse(TokenStream stream)
        {
            // Ensure current token is parentheses start.
            stream.EnsureCurrent(TokenType.SymbolParenthesesL);

            // Skip the parentheses start token.
            stream.Skip();

            // Parse the expression.
            Expr expr = new ExprParser().Parse(stream);

            // Ensure current token is parentheses end.
            stream.EnsureCurrent(TokenType.SymbolParenthesesR);

            // Skip the parentheses end token.
            stream.Skip();

            // Return the expression.
            return expr;
        }
    }
}
