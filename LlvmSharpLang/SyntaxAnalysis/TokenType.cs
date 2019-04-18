namespace LlvmSharpLang.SyntaxAnalysis
{
    public enum TokenType : int
    {
        // General.
        Unknown,

        Id,

        // Math operators.
        OperatorAssignment,

        OperatorAddition,

        OperatorSubstraction,

        OperatorMultiplication,

        OperatorDivision,

        OperatorExponent,

        OperatorModulo,

        OperatorEscape,

        // Functionality operators.
        OperatorPipe,

        OperatorAddressOf,

        // Literals.
        LiteralInteger,

        LiteralDecimal,

        LiteralCharacter,

        LiteralString,

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
