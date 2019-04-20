using LlvmSharpLang.CodeGeneration;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{
    public class FunctionReturnParser : IParser<FunctionReturnExpr>
    {
        public FunctionReturnExpr Parse(TokenStream stream)
        {
            // Create the return expression entity.
            FunctionReturnExpr returnExpr = new FunctionReturnExpr();

            // Skip return keyword.
            stream.Skip(TokenType.KeywordReturn);

            Token nextToken = stream.Peek();

            // There is no return expression.
            if (nextToken.Type == TokenType.SymbolSemiColon)
            {
                return returnExpr;
            }

            // TODO: Is this cast correct?
            // Otherwise, invoke the expression parser.
            returnExpr = (FunctionReturnExpr)new ExprParser().Parse(stream);

            // Consume semi-colon after the expression.
            stream.Skip(TokenType.SymbolSemiColon);

            return returnExpr;
        }
    }
}
