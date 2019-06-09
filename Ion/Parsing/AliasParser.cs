using Ion.CodeGeneration;
using Ion.Syntax;

namespace Ion.Parsing
{
    public class AliasParser : IParser<Alias>
    {
        public Alias Parse(ParserContext context)
        {
            // Ensure current token is alias keyword.
            context.Stream.EnsureCurrent(TokenType.KeywordAlias);

            // Skip alias keyword, capture target identifier.
            string targetName = context.Stream.Next(TokenType.Identifier).Value;

            // Skip target identifier onto as keyword.
            context.Stream.Skip(TokenType.KeywordAs);

            // Skip as keyword.
            context.Stream.Skip();

            // Invoke identifier parser to capture alias name.
            string aliasName = new IdentifierParser().Parse(context);

            // Ensure current token is symbol semi-colon.
            context.Stream.EnsureCurrent(TokenType.SymbolSemiColon);

            // Skip semi-colon.
            context.Stream.Skip();

            // Create the alias construct.
            Alias alias = new Alias(targetName, aliasName);

            // Return the resulting construct.
            return alias;
        }
    }
}
