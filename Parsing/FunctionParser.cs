using System;
using System.Collections.Generic;
using LLVMSharp;

namespace LlvmSharpLang
{
    public class FunctionParser : IParser<Function>
    {
        public Function Parse(TokenStream stream)
        {
            // Skip 'fn' keyword.
            stream.Skip();

            // Consume function identifier.
            stream.Skip();

            string name = stream.Get().Value;
            Function fn = new Function();

            // Set the function name.
            fn.SetName(name);

            // Parse arguments.
            FormalArg[] args = new FormalArgParser().Parse(stream);

            // Assign arguments.
            fn.Args = args;

            return fn;
        }
    }
}
