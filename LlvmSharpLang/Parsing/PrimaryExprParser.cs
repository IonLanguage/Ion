using System;
using LlvmSharpLang.CodeGeneration;
using LlvmSharpLang.CognitiveServices;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{

    public class PrimaryExprParser : IParser<Expr>
    {
        public Expr Parse(TokenStream stream)
        {
            TokenType nextTokenType = stream.Peek().Type;

            // Numeric expression.
            if (TokenIdentifier.IsNumeric(nextTokenType))
            {
                // TODO: Implement the NumericExprParser, then implement it here.
                throw new NotImplementedException();
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

            // Otherwise, not supported.
            throw new Exception("Expected a primary expression");
        }
    }

}
