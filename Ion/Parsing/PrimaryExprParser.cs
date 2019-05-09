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
            TokenType tokenType = context.Stream.Get().Type;

            // Variable declaration expression.
            if (TokenIdentifier.IsType(tokenType))
            {
                return new VarDeclareExprParser().Parse(context);
            }
            // Numeric expression.
            else if (TokenIdentifier.IsNumeric(tokenType))
            {
                return new NumericExprParser().Parse(context);
            }
            // Identifier expression.
            else if (tokenType == TokenType.Identifier)
            {
                return new IdentifierExprParser().Parse(context);
            }
            // Parentheses expression.
            else if (tokenType == TokenType.SymbolParenthesesL)
            {
                return new ParenthesesExprParser().Parse(context);
            }
            else if (tokenType == TokenType.LiteralString)
            {
                return new StringExprParser().Parse(context);
            }
            else if (TokenIdentifier.IsBoolean(tokenType))
            {
                return new BooleanExprParser().Parse(context);
            }

            // At this point, return null.
            return null;
        }
    }
}
