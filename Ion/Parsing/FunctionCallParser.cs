using System;
using System.Collections.Generic;
using LLVMSharp;
using Ion.CodeGeneration;
using Ion.Core;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class FunctionCallExprParser : IParser<FunctionCallExpr>
    {
        public FunctionCallExpr Parse(TokenStream stream)
        {
            // Capture identifier.
            string identifier = stream.Next(TokenType.Identifier).Value;

            // Ensure the function has been emitted.
            if (!SymbolTable.functions.ContainsKey(identifier))
            {
                throw new Exception($"Call to a non-existent function named '{identifier}' performed");
            }

            // Retrieve the target.
            LLVMValueRef target = SymbolTable.functions[identifier];

            // Invoke the function call argument parser.
            List<Expr> args = new CallArgsParser().Parse(stream);

            // TODO: Callee.
            // Create the function call expression entity.
            FunctionCallExpr functionCall = new FunctionCallExpr(target, "TestCalle", args);

            return functionCall;
        }
    }
}
