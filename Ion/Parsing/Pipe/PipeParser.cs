using System.Collections.Generic;
using Ion.Generation;
using Ion.Syntax;
using LLVMSharp;

namespace Ion.Parsing
{
    public class PipeParser : IParser<Pipe>
    {
        public Pipe Parse(ParserContext context)
        {
            // Expect current token to be symbol colon.
            context.Stream.EnsureCurrent(TokenType.SymbolColon);

            // Skip colon symbol.
            context.Stream.Skip();

            // Invoke the pipe arguments parser.
            List<Expr> arguments = new PipeArgsParser().Parse(context);

            // Expect current token to be pipe operator.
            context.Stream.EnsureCurrent(TokenType.OperatorPipe);

            // Capture the target identifier.
            string identifier = context.Stream.Next(TokenType.Identifier).Value;

            // Skip identifier onto semi-colon.
            context.Stream.Skip(TokenType.SymbolSemiColon);

            // Create the resulting pipe entity.
            Pipe pipe = new Pipe(arguments.ToArray(), identifier);

            // Return the resulting pipe entity.
            return pipe;
        }
    }
}
