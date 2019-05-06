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

            // Skip parentheses start.
            stream.Skip();

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
                buffer = stream.Get();

                // Ensure next token is valid.
                if (buffer.Type != TokenType.SymbolComma && buffer.Type != TokenType.SymbolParenthesesR)
                {
                    throw new Exception($"Unexpected token of type '{buffer.Type}'; Expected comma or parentheses end in argument list");
                }
                // Skip the comma token.
                else if (buffer.Type == TokenType.SymbolComma)
                {
                    stream.Skip();

                    // Make sure to update the buffer after skipping the comma token.
                    buffer = stream.Get();
                }

                // Append the parsed argument.
                args.Values.Add(arg);
            }

            // Ensure current token is parentheses end.
            stream.EnsureCurrent(TokenType.SymbolParenthesesR);

            // Skip parentheses end token.
            stream.Skip();

            // Finish process.
            return args;
        }
    }
}
