namespace LlvmSharpLang {
    public enum TokenType : int {
        // Keywords.
        KeywordFn,

        KeywordReturn,

        // Symbols.
        SymbolBlockL,

        SymbolBlockR,

        SymbolParenthesesL,

        SymbolParenthesesR,

        SymbolColon,

        SymbolSemiColon
    }
}
