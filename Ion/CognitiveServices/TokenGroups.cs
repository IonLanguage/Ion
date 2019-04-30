using System.Collections.Generic;
using Ion.SyntaxAnalysis;

namespace Ion.CognitiveServices
{
    public static class TokenGroups
    {
        public static readonly List<TokenType> numeric = new List<TokenType>
        {
            TokenType.LiteralDecimal,
            TokenType.LiteralInteger
        };
    }
}
