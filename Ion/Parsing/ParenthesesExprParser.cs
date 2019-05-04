using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class ParenthesesExprParser : IParser<Expr>
    {
        public Expr Parse(TokenStream stream)
        {
            // Skip to parentheses start token.
            stream.Skip(TokenType.SymbolParenthesesL);

            // Parse the expression.
            Expr expr = new ExprParser().Parse(stream);

            // Skip to parentheses end token.
            stream.Skip(TokenType.SymbolParenthesesR);

            // Return the expression.
            return expr;
        }
    }
}
