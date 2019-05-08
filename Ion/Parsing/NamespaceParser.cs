using Ion.SyntaxAnalysis;
using Ion.CodeGeneration;
using System.Collections.Generic;

namespace Ion.Parsing
{
    public class NamespaceParser : IParser<Namespace>
    {
        public Namespace Parse(TokenStream stream)
        {
            // Ensure current token is namespace keyword.
            stream.EnsureCurrent(TokenType.KeywordNamespace);

            // Skip namespace keyword.
            stream.Skip();

            // Invoke the path parser.
            List<string> path = new PathParser().Parse(stream);

            // Ensure current token is semi-colon.
            stream.EnsureCurrent(TokenType.SymbolSemiColon);

            // Skip semi-colon.
            stream.Skip();

            // Create the namespace entity.
            Namespace namespaceEntity = new Namespace(path);

            // Return the namespace entity.
            return namespaceEntity;
        }
    }
}
