using LlvmSharpLang.CodeGen;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{
    public class FunctionReturnParser : IParser<Expr>
    {
        public Expr Parse(TokenStream stream)
        {
            // Skip return keyword.
            stream.Skip(TokenType.KeywordReturn);

            Token nextToken = stream.Peek();

            // There is no return expression.
            if (nextToken.Type == TokenType.SymbolSemiColon)
            {
                return Expr.;
            }
        }
    }
}
