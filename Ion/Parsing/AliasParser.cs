using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

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

            // Skip as keyword token, capture alias identifier.
            string aliasName = context.Stream.Next(TokenType.Identifier).Value;

            // Skip alias identifier token onto semi-colon;
            context.Stream.Skip(TokenType.SymbolSemiColon);

            // Skip semi-colon.
            context.Stream.Skip();

            // Create the alias construct.
            Alias alias = new Alias(targetName, aliasName);

            // Return the resulting construct.
            return alias;
        }
    }
}
