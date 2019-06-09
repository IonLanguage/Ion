using System.Collections.Generic;
using Ion.Syntax;

namespace Ion.CognitiveServices
{
    public static class TokenGroups
    {
        public static readonly List<TokenType> numeric = new List<TokenType>
        {
            TokenType.LiteralDecimal,
            TokenType.LiteralInteger
        };

        public static readonly List<TokenType> boolean = new List<TokenType>
        {
            TokenType.KeywordTrue,
            TokenType.KeywordFalse
        };
    }
}
