using System;
using Ion.Generation;
using Ion.Syntax;

namespace Ion.Parsing
{
    public class FormalArgsParser : IParser<FormalArgs>
    {
        public FormalArgs Parse(ParserContext context)
        {
            // Ensure position.
            context.Stream.EnsureCurrent(TokenType.SymbolParenthesesL);

            // Skip parentheses start.
            context.Stream.Skip();

            // Create the formal args entity.
            FormalArgs args = new FormalArgs();

            // Create the loop buffer token.
            Token buffer = context.Stream.Current;

            // Loop until parentheses end.
            while (buffer.Type != TokenType.SymbolParenthesesR)
            {
                // Continuous arguments.
                if (!args.Continuous && buffer.Type == TokenType.SymbolContinuous)
                {
                    // Set the continuous flag.
                    args.Continuous = true;

                    // Advance stream immediatly.
                    buffer = context.Stream.Next(TokenType.SymbolParenthesesR);

                    // Continue loop.
                    continue;
                }
                // Continuous arguments must be final.
                else if (args.Continuous)
                {
                    throw new Exception("Unexpected token after continuous arguments");
                }

                // Invoke the arg parser.
                FormalArg arg = new FormalArgParser().Parse(context);

                // Update the buffer.
                buffer = context.Stream.Current;

                // Ensure next token is valid.
                if (buffer.Type != TokenType.SymbolComma && buffer.Type != TokenType.SymbolParenthesesR)
                {
                    throw new Exception($"Unexpected token of type '{buffer.Type}'; Expected comma or parentheses end in argument list");
                }
                // Skip the comma token.
                else if (buffer.Type == TokenType.SymbolComma)
                {
                    context.Stream.Skip();

                    // Make sure to update the buffer after skipping the comma token.
                    buffer = context.Stream.Current;
                }

                // Append the parsed argument.
                args.Values.Add(arg);
            }

            // Ensure current token is parentheses end.
            context.Stream.EnsureCurrent(TokenType.SymbolParenthesesR);

            // Skip parentheses end token.
            context.Stream.Skip();

            // Finish process.
            return args;
        }
    }
}
