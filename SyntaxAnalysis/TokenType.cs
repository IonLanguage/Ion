namespace LlvmSharpLang.SyntaxAnalysis
{
    public enum TokenType : int
    {
        // General.
        Unknown,

        Id,

        // Math operators.
        OpAssign,

        OpAdd,

        OpSub,

        OpMultiply,

        OpDivide,

        OpExponent,

        OpModulo,

        // Functionality operators.
        OpPipe,

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

        SymbolArrow,

        SymbolAt
    }
}
