using LlvmSharpLang.CodeGeneration;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{
    public class ParenthesesExprParser : IParser<Expr>
    {
        public Expr Parse(TokenStream stream)
        {
            // Skip parentheses start token.
            stream.Skip(TokenType.SymbolParenthesesL);

            // Parse the expression.
            Expr expr = new ExprParser().Parse(stream);

            // Skip to parentheses end token.
            stream.Skip(TokenType.SymbolParenthesesR);

            // Skip parentheses end token.
            stream.Skip();

            return expr;
        }
    }
}
