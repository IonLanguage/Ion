using Ion.CognitiveServices;
using Ion.Parsing;
using Ion.SyntaxAnalysis;

namespace Ion.CodeGeneration
{
    public class StatementParser : IParser<Expr>
    {
        public Expr Parse(ParserContext context)
        {
            // Capture the current token's type.
            TokenType currentTokenType = context.Stream.Get().Type;

            // Variable declaration expression.
            if (TokenIdentifier.IsPrimitiveType(currentTokenType))
            {
                return new VarDeclareExprParser().Parse(context);
            }
            // If expression.
            else if (currentTokenType == TokenType.KeywordIf)
            {
                return new IfExprParser().Parse(context);
            }

            // Otherwise, delegate to the primary expression parser.
            Expr expr = new PrimaryExprParser().Parse(context);

            // Return the parsed expression.
            return expr;
        }
    }
}
