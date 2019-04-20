using System;
using LlvmSharpLang.CodeGeneration;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{

    public class PrimaryExprParser : IParser<Expr>
    {
        public Expr Parse(TokenStream stream)
        {
            switch (stream.Get().Type)
            {
                case TokenType.Identifier:
                    {
                        return new IdentifierExprParser().Parse(stream);
                    }

                case TokenType.SymbolParenthesesL:
                    {
                        return new ParenthesesExprParser().Parse(stream);
                    }

                default:
                    {
                        throw new Exception("Expected an expression");
                    }
            }
        }
    }

}
