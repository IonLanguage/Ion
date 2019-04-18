using System.Collections.Generic;
using System.Text.RegularExpressions;

using TokenTypeMap = Dictionary<string, TokenType>;
using ComplexTokenTypeMap = Dictionary<Regex, TokenType>;

namespace LlvmSharpLang.SyntaxAnalysis
{
    public static class Constants
    {
        public static readonly TokenTypeMap keywords = new TokenTypeMap {
            {"fn", TokenType.KeywordFn}
        };

        public static readonly TokenTypeMap symbols = new TokenTypeMap {
            {"@", TokenType.SymbolAt},
            {"(", TokenType.SymbolBlockL},
            {")", TokenType.SymbolBlockR},
            {"{", TokenType.SymbolParenthesesL},
            {"}", TokenType.SymbolParenthesesR},
            {":", TokenType.SymbolColon},
            {";", TokenType.SymbolSemiColon},
            {"=>", TokenType.SymbolArrow}
        };

        public static readonly ComplexTokenTypeMap complex = new ComplexTokenTypeMap
        {
            {new Regex(@"")}
        };
    }

}
