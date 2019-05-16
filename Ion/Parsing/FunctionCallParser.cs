using System;
using System.Collections.Generic;
using Ion.CodeGeneration;
using Ion.Core;
using Ion.SyntaxAnalysis;
using LLVMSharp;

namespace Ion.Parsing
{
    public class FunctionCallExprParser : IParser<FunctionCallExpr>
    {
        public FunctionCallExpr Parse(ParserContext context)
        {
            // Capture identifier.
            string identifier = context.Stream.Get(TokenType.Identifier).Value;

            // Skip the identifier token onto the parentheses start token.
            context.Stream.Skip(TokenType.SymbolParenthesesL);

            // Invoke the function call argument parser.
            List<Expr> args = new CallArgsParser().Parse(context);

            // Create the function call expression entity.
            FunctionCallExpr functionCall = new FunctionCallExpr(identifier, args);

            // Return the function call expression.
            return functionCall;
        }
    }
}
