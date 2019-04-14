using System;
using System.Collections.Generic;
using LLVMSharp;
using LlvmSharpLang.CodeGen;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{
    public class FunctionParser : IParser<Function>
    {
        public Function Parse(TokenStream stream)
        {
            // Skip 'fn' keyword.
            stream.Skip(TokenType.KeywordFn);

            // Capture function identifier.
            string name = stream.Next(TokenType.Id).Value;

            // Ensure name exists.
            if (String.IsNullOrEmpty(name))
            {
                throw new Exception("Function identifier was null or empty");
            }

            // Create the function entity.
            Function function = new Function();

            // Set the function name.
            function.SetName(name);

            // Parse arguments.
            Args args = new ArgsParser().Parse(stream);

            // Assign arguments.
            function.Args = args;

            // Skip ':' for return type.
            stream.Skip(TokenType.SymbolColon);

            // Parse the return type.
            CodeGen.Type returnType = new TypeParser().Parse(stream);

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
