using Ion.CodeGeneration;
using Ion.CognitiveServices;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class PrimaryExprParser : IParser<Expr>
    {
        public Expr Parse(ParserContext context)
        {
            // Capture current token type.
            TokenType currentTokenType = context.Stream.Get().Type;

            // Pipe operation.
            if (currentTokenType == TokenType.SymbolColon)
            {
                return new PipeParser().Parse(context);
            }
            // Numeric expression.
            else if (TokenIdentifier.IsNumeric(currentTokenType))
            {
                return new NumericExprParser().Parse(context);
            }
            // Identifier expression.
            else if (currentTokenType == TokenType.Identifier)
            {
                return new IdentifierExprParser().Parse(context);
            }
            // Parentheses expression.
            else if (currentTokenType == TokenType.SymbolParenthesesL)
            {
                return new ParenthesesExprParser().Parse(context);
            }
            // String expression.
            else if (currentTokenType == TokenType.LiteralString)
            {
                return new StringExprParser().Parse(context);
            }
            // Boolean expression.
            else if (TokenIdentifier.IsBoolean(currentTokenType))
            {
                return new BooleanExprParser().Parse(context);
            }
            // Struct expression.
            else if (currentTokenType == TokenType.KeywordNew)
            {
                return new StructExprParser().Parse(context);
            }

            // At this point, return null.
            return null;
        }
    }
}
