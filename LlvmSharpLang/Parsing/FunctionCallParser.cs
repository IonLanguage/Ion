using System;
using System.Collections.Generic;
using LLVMSharp;
using LlvmSharpLang.CodeGeneration;
using LlvmSharpLang.Core;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{
    public class FunctionCallExprParser : IParser<FunctionCallExpr>
    {
        public FunctionCallExpr Parse(TokenStream stream)
        {
            // Capture identifier.
            string identifier = stream.Next(TokenType.Identifier).Value;

            // Ensure the function has been emitted.
            if (!CodeMap.functions.ContainsKey(identifier))
            {
                throw new Exception("Call to a non-existent function performed");
            }

            // Retrieve the target.
            LLVMValueRef target = CodeMap.functions[identifier];

            // Invoke the function call argument parser.
            List<CallArgExpr> args = new CallArgsParser().Parse(stream);

            // TODO: Callee.
            // Create the function call expression entity.
            FunctionCallExpr functionCall = new FunctionCallExpr(target, "TestCalle", args);

            return functionCall;
        }
    }
}
