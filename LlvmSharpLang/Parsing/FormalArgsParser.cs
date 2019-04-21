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
            Token peekBuffer = stream.Peek();

            System.Console.WriteLine($"Current (GET) is: {stream.Get()}");
            System.Console.WriteLine($"Peek is: {stream.Peek()}");

            // Loop until parentheses end.
            while (peekBuffer.Type != TokenType.SymbolParenthesesR)
            {
                // Continuous arguments.
                if (!args.Continuous && peekBuffer.Type == TokenType.SymbolContinuous)
                {
                    // Set the continuous flag.
                    args.Continuous = true;

                    // Advance stream immediatly.
                    peekBuffer = stream.Next(TokenType.SymbolParenthesesR);

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
                peekBuffer = stream.Peek();

                // Ensure next token is valid.
                if (peekBuffer.Type != TokenType.SymbolComma && peekBuffer.Type != TokenType.SymbolParenthesesR)
                {
                    throw new Exception("Unexpected token; Expected comma or parentheses end in argument list");
                }
                // Skip comma.
                else if (peekBuffer.Type == TokenType.SymbolComma)
                {
                    stream.Skip();
                }

                // Append the parsed arg.
                args.Values.Add(arg);
            }

            // Skip parentheses end if applicable.
            if (peekBuffer.Type == TokenType.SymbolParenthesesR)
            {
                stream.Skip(TokenType.SymbolParenthesesR);
            }

            // Finish process.
            return args;
        }
    }
}
