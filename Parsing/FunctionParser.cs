using System;
using System.Collections.Generic;
using LLVMSharp;

namespace LlvmSharpLang
{
    public class FunctionParser : IParser<Function>
    {
        public Function Parse(TokenStream stream)
        {
            Console.WriteLine(stream.Get().Value);
            // Skip 'fn' keyword.
            stream.Skip();

            // Capture function identifier.
            string name = stream.Next().Value;

            // Ensure name exists.
            if (String.IsNullOrEmpty(name)) {
                throw new Exception("Function identifier was null or empty");
            }

            // Create the function entity.
            Function function = new Function();

            // Set the function name.
            function.SetName(name);

            // Parse arguments.
            FormalArg[] args = new FormalArgParser().Parse(stream);

            // Assign arguments.
            function.Args = args;

            // Create the body.
            Block body = new Block();

            body.SetName("Entry");

            // Assign the body.
            function.Body = body;

            return function;
        }
    }
}
