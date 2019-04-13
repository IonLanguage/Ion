namespace LlvmSharpLang.SyntaxAnalysis
{
    public enum TokenType : int
    {
        // General.
        Unknown,

        Id,

        // Keywords.
        KeywordFn,

        KeywordReturn,

        // Symbols.
        SymbolBlockL,

        SymbolBlockR,

        SymbolParenthesesL,

        SymbolParenthesesR,

        SymbolContinuous,

        SymbolColon,

        SymbolSemiColon,

        SymbolArrow
    }
}
