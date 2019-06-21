using Ion.Syntax;
using Ion.Generation;
using System.Collections.Generic;

namespace Ion.Parsing
{
    public class NamespaceParser : IParser<Namespace>
    {
        public Namespace Parse(ParserContext context)
        {
            // Ensure current token is namespace keyword.
            context.Stream.EnsureCurrent(TokenType.KeywordNamespace);

            // Skip namespace keyword.
            context.Stream.Skip();

            // Invoke the path parser.
            PathResult path = new PathParser().Parse(context);

            // Ensure current token is semi-colon.
            context.Stream.EnsureCurrent(TokenType.SymbolSemiColon);

            // Skip semi-colon.
            context.Stream.Skip();

            // Create the namespace entity.
            Namespace namespaceEntity = new Namespace(path);

            // Return the namespace entity.
            return namespaceEntity;
        }
    }
}
