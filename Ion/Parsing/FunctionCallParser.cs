using System;
using Ion.CodeGeneration;
using Ion.Core;
using Ion.SyntaxAnalysis;
using LLVMSharp;

namespace Ion.Parsing
{
    public class FunctionCallExprParser : IParser<FunctionCallExpr>
    {
        public FunctionCallExpr Parse(TokenStream stream)
        {
            // Capture identifier.
            var identifier = stream.Next(TokenType.Identifier).Value;

            // Ensure the function has been emitted.
            if (!SymbolTable.functions.ContainsKey(identifier))
                throw new Exception($"Call to a non-existent function named '{identifier}' performed");

            // Retrieve the target.
            LLVMValueRef target = SymbolTable.functions[identifier];

            // Invoke the function call argument parser.
            var args = new CallArgsParser().Parse(stream);

            // TODO: Callee.
            // Create the function call expression entity.
            var functionCall = new FunctionCallExpr(target, "TestCallee", args);

            return functionCall;
        }
    }
}