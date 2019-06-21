using Ion.Syntax;
using Ion.Generation;
using System.Collections.Generic;

namespace Ion.Parsing
{
    public class ImportParser : IParser<Import>
    {
        public Import Parse(ParserContext context)
        {
            // Ensure current token is import keyword.
            context.Stream.EnsureCurrent(TokenType.KeywordImport);

            // Skip import keyword.
            context.Stream.Skip();

            // Invoke path parser.
            PathResult path = new PathParser().Parse(context);

            // Ensure current token is semi-colon.
            context.Stream.EnsureCurrent(TokenType.SymbolSemiColon);

            // Skip semi-colon.
            context.Stream.Skip();

            // Create the resulting import entity.
            Import import = new Import(path);

            // Return the resulting import entity.
            return import;
        }
    }
}
