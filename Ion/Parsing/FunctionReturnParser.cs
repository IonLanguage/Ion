
using System;
using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class FunctionReturnParser : IParser<Expr>
    {
        public Expr Parse(TokenStream stream)
        {
            // Skip return keyword.
            stream.Skip(TokenType.KeywordReturn);

            // Look at the next token.
            Token nextToken = stream.Peek();

            // There is no return expression.
            if (nextToken.Type == TokenType.SymbolSemiColon)
            {
                // Skip to the semi-colon.
                stream.Skip(TokenType.SymbolSemiColon);

                // Return null as no expression/return value was captured.
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
