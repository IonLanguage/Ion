using System.Collections.Generic;
using Ion.Generation;
using Ion.Syntax;

namespace Ion.Parsing
{
    public class FormalArgsParser : IParser<(string, Type)[]>
    {
        public (string, Type)[] Parse(ParserContext context)
        {
            // Ensure position.
            context.Stream.EnsureCurrent(TokenType.SymbolParenthesesL);

            // Skip parentheses start.
            context.Stream.Skip();

            // Create the formal args entity.
            List<(string, Type)> arguments = new List<(string, Type)>();

            // Create the loop buffer token.
            Token buffer = context.Stream.Current;

            // Loop until parentheses end.
            while (buffer.Type != TokenType.SymbolParenthesesR)
            {
                // Continuous arguments.
                if (!arguments.Continuous && buffer.Type == TokenType.SymbolContinuous)
                {
                    // Set the continuous flag.
                    arguments.Continuous = true;

                    // Advance stream immediatly.
                    buffer = context.Stream.Next(TokenType.SymbolParenthesesR);

                    // Continue loop.
                    continue;
                }
                // Continuous arguments must be final.
                else if (arguments.Continuous)
                {
                    throw new System.Exception("Unexpected token after continuous arguments");
                }

                // Invoke the arg parser.
                (string, Type) arg = new FormalArgParser().Parse(context);

                // Update the buffer.
                buffer = context.Stream.Current;

                // Ensure next token is valid.
                if (buffer.Type != TokenType.SymbolComma && buffer.Type != TokenType.SymbolParenthesesR)
                {
                    throw new System.Exception($"Unexpected token of type '{buffer.Type}'; Expected comma or parentheses end in argument list");
                }
                // Skip the comma token.
                else if (buffer.Type == TokenType.SymbolComma)
                {
                    context.Stream.Skip();

                    // Make sure to update the buffer after skipping the comma token.
                    buffer = context.Stream.Current;
                }

                // Append the parsed argument.
                arguments.Add(arg);
            }

            // Ensure current token is parentheses end.
            context.Stream.EnsureCurrent(TokenType.SymbolParenthesesR);

            // Skip parentheses end token.
            context.Stream.Skip();

            // Return results.
            return arguments.ToArray();
        }
    }
}
