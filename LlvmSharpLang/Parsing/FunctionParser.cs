using System;
using System.Collections.Generic;
using LLVMSharp;
using LlvmSharpLang.CodeGeneration;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{
    public class FunctionParser : IParser<Function>
    {
        public Function Parse(TokenStream stream)
        {
            // Skip the function definition keyword.
            stream.Skip(TokenType.KeywordFunction);

            // Capture function identifier.
            string name = stream.Next(TokenType.Identifier).Value;

            // Ensure name exists.
            if (String.IsNullOrEmpty(name))
            {
                throw new Exception("Unexpected function identifier to be null or empty");
            }

            // Create the function entity.
            Function function = new Function();

            // Set the function name.
            function.SetName(name);

            // Parse formal arguments.
            FormalArgs args = new FormalArgsParser().Parse(stream);

            // Assign arguments.
            function.Args = args;

            // Skip ':' for return type.
            stream.Skip(TokenType.SymbolColon);

            // Parse the return type.
            CodeGeneration.Type returnType = new TypeParser().Parse(stream);

            // Assign the return type.
            function.ReturnType = returnType;

            // Parse the body.
            Block body = new BlockParser().Parse(stream);

            // Set the name of the body block.
            body.SetEntryName();

            // Assign the body.
            function.Body = body;

            return function;
        }
    }
}
