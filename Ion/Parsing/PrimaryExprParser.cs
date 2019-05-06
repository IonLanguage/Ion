using Ion.CodeGeneration;
using Ion.CognitiveServices;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class PrimaryExprParser : IParser<Expr>
    {
        public Expr Parse(TokenStream stream)
        {
            // Capture current token type.
            TokenType tokenType = stream.Get().Type;

            // Variable declaration expression.
            if (TokenIdentifier.IsType(tokenType))
            {
                return new VarDeclareExprParser().Parse(stream);
            }
            // Numeric expression.
            else if (TokenIdentifier.IsNumeric(tokenType))
            {
                return new NumericExprParser().Parse(stream);
            }
            // Identifier expression.
            else if (tokenType == TokenType.Identifier)
            {
                return new IdentifierExprParser().Parse(stream);
            }
            // Parentheses expression.
            else if (tokenType == TokenType.SymbolParenthesesL)
            {
                return new ParenthesesExprParser().Parse(stream);
            }
            else if (tokenType == TokenType.LiteralString)
            {
                return new StringExprParser().Parse(stream);
            }

            // At this point, return null.
            return null;
        }
    }
}
