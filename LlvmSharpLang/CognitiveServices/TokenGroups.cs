using System.Collections.Generic;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.CognitiveServices
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
