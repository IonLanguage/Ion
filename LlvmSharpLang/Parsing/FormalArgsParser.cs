using System;
using System.Collections.Generic;
using LLVMSharp;
using LlvmSharpLang.CodeGeneration;
using LlvmSharpLang.Misc;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Parsing
{
    public class FormalArgsParser : IParser<FormalArgs>
    {
        public FormalArgs Parse(TokenStream stream)
        {
            // Skip parentheses start.
            stream.Skip(TokenType.SymbolParenthesesL);

            // Create the formal args entity.
            FormalArgs args = new FormalArgs();

            // Create the loop buffer token.
            Token buffer = stream.Get();

            // Loop until parentheses end.
            while (buffer.Type != TokenType.SymbolParenthesesR)
            {
                // Continuous arguments.
                if (!args.Continuous && buffer.Type == TokenType.SymbolContinuous)
                {
                    // Set the continuous flag.
                    args.Continuous = true;

                    // Advance stream immediatly.
                    buffer = stream.Next(TokenType.SymbolParenthesesR);

                    continue;
                }
                // Continuous arguments must be final.
                else if (args.Continuous)
                {
                    throw new Exception("Unexpected token after continuous arguments");
                }

                // Invoke the arg parser.
                FormalArg arg = new FormalArgParser().Parse(stream);

                // Update the buffer.
                buffer = stream.Next();

                // Append the parsed arg.
                args.Values.Add(arg);
            }

            // Finish process. No need to skip parentheses end.
            return args;
        }
    }
}
