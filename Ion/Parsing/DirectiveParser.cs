using Ion.CodeGeneration;
using Ion.CognitiveServices;
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

            // Create the value buffer.
            string value = null;

            // Capture the current token's type.
            TokenType currentTokenType = context.Stream.Current.Type;

            // Directive value is set, process it.
            if (TokenIdentifier.IsLiteral(context.Stream.Current.Type))
            {
                // Capture string literal token.
                value = context.Stream.Current.Value;

                // Remove the single/double quotes from the value if applicable.
                if (context.Stream.Current.Type == TokenType.LiteralString || currentTokenType == TokenType.LiteralCharacter)
                {
                    value = Util.ExtractStringLiteralValue(value);
                }
            }

            // Skip string literal token.
            context.Stream.Skip();

            // Create the directive construct.
            Directive directive = new Directive(key, value);

            // Return the resulting directive.
            return directive;
        }
    }
}
