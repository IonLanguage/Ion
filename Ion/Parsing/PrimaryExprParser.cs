using System;
using Ion.CodeGeneration;
using Ion.CognitiveServices;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class PrimaryExprParser : IParser<Expr>
    {
        public Expr Parse(TokenStream stream)
        {
            TokenType nextTokenType = stream.Peek().Type;

            // Variable declaration expression.
            if (TokenIdentifier.IsType(nextTokenType))
            {
                return new VarDeclareExprParser().Parse(stream);
            }
            // Numeric expression.
            else if (TokenIdentifier.IsNumeric(nextTokenType))
            {
                return new NumericExprParser().Parse(stream);
            }
            // Identifier expression.
            else if (nextTokenType == TokenType.Identifier)
            {
                return new IdentifierExprParser().Parse(stream);
            }
            // Parentheses expression.
            else if (nextTokenType == TokenType.SymbolParenthesesL)
            {
                return new ParenthesesExprParser().Parse(stream);
            }

            // At this point, return null.
            return null;
        }
    }

}
