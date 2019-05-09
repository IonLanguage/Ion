using System;
using System.Collections.Generic;
using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class CallArgsParser : IParser<List<Expr>>
    {
        public List<Expr> Parse(ParserContext context)
        {
            // Create the argument list result.
            List<Expr> args = new List<Expr>();

            // Ensure current token is parentheses start.
            context.Stream.EnsureCurrent(TokenType.SymbolParenthesesL);

            // Skip parentheses start.
            context.Stream.Skip();

            // Contains at least one argument.
            if (context.Stream.Get().Type != TokenType.SymbolParenthesesR)
            {
                while (true)
                {
                    // Invoke the expression parser to parse the argument.
                    Expr arg = new ExprParser().Parse(context);

                    // Append the parsed argument.
                    args.Add(arg);

                    TokenType currentTokenType = context.Stream.Get().Type;

                    // Arguments ended.
                    if (currentTokenType == TokenType.SymbolParenthesesR)
                    {
                        break;
                    }
                    // Otherwise, expect a comma.
                    else if (currentTokenType != TokenType.SymbolComma)
                    {
                        throw new Exception("Unexpected token in function call argument list");
                    }

                    // Skip token.
                    context.Stream.Skip();
                }
            }

            // Ensure current token is parentheses end.
            context.Stream.EnsureCurrent(TokenType.SymbolParenthesesR);

            // Skip parentheses end.
            context.Stream.Skip();

            return args;
        }
    }
}
