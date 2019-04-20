using LlvmSharpLang.CodeGeneration;
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
                return null;
            }

            // Otherwise, invoke the expression parser.
            Expr expr = new ExprParser().Parse(stream);

            // Consume semi-colon after the expression.
            stream.Skip(TokenType.SymbolSemiColon);

            return expr;
        }
    }
}
