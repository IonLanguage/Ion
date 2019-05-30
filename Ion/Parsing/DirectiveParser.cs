using Ion.CodeGeneration;
using Ion.Misc;
using Ion.SyntaxAnalysis;

namespace Ion.Parsing
{
    public class DirectiveParser : IParser<Directive>
    {
        public Directive Parse(ParserContext context)
        {
            // Ensure current token is hash symbol.
            context.Stream.EnsureCurrent(TokenType.SymbolHash);

            // Skip hash symbol token.
            context.Stream.Skip();

            // Invoke path parser to capture option key.
            PathResult key = new PathParser().Parse(context);

            // Capture string literal token.
            string value = context.Stream.Get(TokenType.LiteralString).Value;

            // Remove the quotes from the value.
            value = Util.ExtractStringLiteralValue(value);

            // Skip string literal token.
            context.Stream.Skip();

            // Create the directive construct.
            Directive directive = new Directive(key, value);

            // Return the resulting directive.
            return directive;
        }
    }
}
