using System;
using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class FormalArgsParser : IParser<FormalArgs>
    {
        public FormalArgs Parse(TokenStream stream)
        {
            // Ensure position.
            stream.EnsureCurrent(TokenType.SymbolParenthesesL);

            // Create the formal args entity.
            FormalArgs args = new FormalArgs();

            // Create the loop buffer token.
            Token peekBuffer = stream.Peek();

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

                    // Continue loop.
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
                    throw new Exception($"Unexpected token of type '{peekBuffer.Type}'; Expected comma or parentheses end in argument list");
                }
                // Skip comma.
                else if (peekBuffer.Type == TokenType.SymbolComma)
                {
                    stream.Skip();
                }

                // Append the parsed argument.
                args.Values.Add(arg);
            }

            // Skip parentheses end if applicable.
            if (peekBuffer.Type == TokenType.SymbolParenthesesR)
            {
                stream.Skip(TokenType.SymbolParenthesesR);
            }

            stream.Skip();

            // Finish process.
            return args;
        }
    }
}
