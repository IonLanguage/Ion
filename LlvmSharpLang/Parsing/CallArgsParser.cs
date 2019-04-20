using System;
using System.Collections.Generic;
using LlvmSharpLang.CodeGeneration;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{
    public class CallArgsParser : IParser<CallArgExpr[]>
    {
        public CallArgExpr[] Parse(TokenStream stream)
        {
            List<CallArgExpr> args = new List<CallArgExpr>();

            // --- MERGE START ---
            // See: https://llvm.org/docs/tutorial/LangImpl02.html @ ParseIdentifierExpr()
            // Contains at least one argument.
            if (stream.Get().Type != TokenType.SymbolParenthesesL)
            {
                while (true)
                {
                    // Parse the argument.
                    CallArgExpr arg = new CallArgExprParser().Parse(stream);

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

            // Skip arguments end token.
            stream.Skip(TokenType.SymbolParenthesesR);

            return args.ToArray();
        }
    }
}
