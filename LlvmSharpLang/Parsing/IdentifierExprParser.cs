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
            if (stream.Peek(2).Type != TokenType.SymbolParenthesesL)
            {
                // TODO: Should be done by an independent parser (VariableExprParser).
                // Skip identifier.
                stream.Skip(TokenType.Identifier);

                // Create and return the variable expression.
                return new VariableExpr(identifier);
            }

            // Otherwise, it's a function call. Invoke the function call parser.
            FunctionCallExpr functionCallExpr = new FunctionCallExprParser().Parse(stream);

            // Return the function call entity.
            return functionCallExpr;
        }
    }
}
