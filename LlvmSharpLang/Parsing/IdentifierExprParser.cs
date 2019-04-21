using System;
using System.Collections.Generic;
using LlvmSharpLang.CodeGeneration;
using LlvmSharpLang.Core;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{
    public class IdentifierExprParser : IParser<Expr>
    {
        public Expr Parse(TokenStream stream)
        {
            Token nextToken = stream.Peek();

            // Ensure captured token is an identifier.
            if (nextToken.Type != TokenType.Identifier)
            {
                throw new Exception("Expected token to be an identifier");
            }

            // Capture identifier token value.
            string identifier = nextToken.Value;

            // Variable reference.
            if (stream.Peek().Type != TokenType.SymbolParenthesesL)
            {
                // Skip identifier.
                stream.Skip(TokenType.Identifier);

                // Create and return the variable expression.
                return new VariableExpr(identifier);
            }

            // Otherwise, it's a function call.
            stream.Skip(TokenType.SymbolParenthesesL);

            // Parse the function call entity.
            FunctionCallExpr functionCallExpr = new FunctionCallExprParser().Parse(stream);

            return functionCallExpr;
        }
    }
}
