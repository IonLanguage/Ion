using System.Collections.Generic;

namespace LlvmSharpLang.SyntaxAnalysis
{
    public static class Constants
    {
        public static readonly Dictionary<string, TokenType> keywordMap = new Dictionary<string, TokenType> {
            {"fn", TokenType.KeywordFn}
        };

        public static readonly Dictionary<string, TokenType> symbolMap = new Dictionary<string, TokenType> {
            {"@", TokenType.SymbolAt},
            {"(", TokenType.SymbolBlockL},
            {")", TokenType.SymbolBlockR},
            {"{", TokenType.SymbolParenthesesL},
            {"}", TokenType.SymbolParenthesesR},
            {":", TokenType.SymbolColon},
            {";", TokenType.SymbolSemiColon},
            {">", TokenType.SymbolArrow}
        };
    }

}