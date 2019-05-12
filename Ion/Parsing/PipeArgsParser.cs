using System;
using System.Collections.Generic;
using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class PipeArgsParser : IParser<List<Expr>>
    {
        public List<Expr> Parse(ParserContext context)
        {
            // Create the resulting argument list.
            List<Expr> arguments = new List<Expr>();

            // Parse the next value.
            Expr value = new PrimaryExprParser().Parse(context);

            // Peek next token.
            Token nextToken = context.Stream.Peek();

            // Expect either a comma or pipe symbol.
            if (nextToken.Type != TokenType.OperatorPipe && nextToken.Type != TokenType.SymbolComma)
            {
                throw new Exception($"Expected next token to be of type either comma symbol or pipe operator, but got '{nextToken.Type}'");
            }
            // There is another value.
            else if (nextToken.Type == TokenType.SymbolComma)
            {
                // Skip onto the comma token.
                context.Stream.Skip(TokenType.SymbolComma);

                // Skip comma token.
                context.Stream.Skip();

                // Recursively invoke a new pipe args parser instance.
                List<Expr> nextArguments = new PipeArgsParser().Parse(context);

                // Append resulting arguments.
                arguments.AddRange(nextArguments);
            }

            // Return the resulting argument list.
            return arguments;
        }
    }
}
