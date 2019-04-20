using System;
using System.Collections.Generic;
using LlvmSharpLang.CodeGeneration;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{
    public class IdentifierExprParser : IParser<Expr>
    {
        public Expr Parse(TokenStream stream)
        {
            // Consume identifier token value.
            string identifier = stream.Next().Value;

            // Variable reference.
            if (stream.Peek().Type != TokenType.SymbolParenthesesL)
            {
                return new VariableExpr(identifier);
            }

            // Otherwise, it's a function call.
            stream.Skip(TokenType.SymbolParenthesesL);

            // Create arguments.
            FormalArgs args = new FormalArgsParser().Parse(stream);

            // Create the function call entity.
            FunctionCallExpr functionCallExpr = new FunctionCallExpr(identifier, args);

            return functionCallExpr;
        }
    }
}
