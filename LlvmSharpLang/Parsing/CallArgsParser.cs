using System;
using System.Collections.Generic;
using LlvmSharpLang.CodeGeneration;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{
    public class CallArgsParser : IParser<List<Expr>>
    {
        public List<Expr> Parse(TokenStream stream)
        {
            // Create the argument list result.
            List<Expr> args = new List<Expr>();

            // Contains at least one argument.
            if (stream.Get().Type != TokenType.SymbolParenthesesL)
            {
                while (true)
                {
                    // Invoke the expression parser to parse the argument.
                    Expr arg = new ExprParser().Parse(stream);

                    // Append the parsed argument.
                    args.Add(arg);

                    TokenType currentTokenType = stream.Get().Type;

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
                    stream.Skip();
                }
            }

            return args;
        }
    }
}
