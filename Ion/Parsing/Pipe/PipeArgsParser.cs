using System;
using System.Collections.Generic;
using Ion.Generation;
using Ion.Syntax;

namespace Ion.Parsing
{
    public class PipeArgsParser : IParser<List<Construct>>
    {
        public List<Construct> Parse(ParserContext context)
        {
            // Create the resulting argument list.
            List<Construct> arguments = new List<Construct>();

            // Parse the next value.
            Construct value = new ExprParser().Parse(context);

            // Append value to the argument list.
            arguments.Add(value);

            // Capture the current token.
            Token token = context.Stream.Current;

            // Expect either a comma or pipe symbol.
            if (token.Type != TokenType.OperatorPipe && token.Type != TokenType.SymbolComma)
            {
                throw new Exception($"Expected next token to be of type either comma symbol or pipe operator, but got '{token.Type}'");
            }
            // There is another value.
            else if (token.Type == TokenType.SymbolComma)
            {
                // Skip comma token.
                context.Stream.Skip();

                // Recursively invoke a new pipe args parser instance.
                List<Construct> nextArguments = new PipeArgsParser().Parse(context);

                // Append resulting arguments.
                arguments.AddRange(nextArguments);
            }

            // Return the resulting argument list.
            return arguments;
        }
    }
}
