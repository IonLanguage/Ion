namespace LlvmSharpLang.SyntaxAnalysis
{
    public enum TokenType : int
    {
        // General.
        Unknown,

        Identifier,

        Type,

        // Math operators.
        OperatorAssignment,

        OperatorAddition,

        OperatorSubstraction,

        OperatorMultiplication,

        OperatorDivision,

        OperatorExponent,

        OperatorModulo,

        OperatorEscape,

        OperatorEquality,

        // Functionality operators.
        OperatorPipe,

        OperatorAddressOf,

        // Literals.
        LiteralInteger,

        LiteralDecimal,

        LiteralCharacter,

        LiteralString,

        // Keywords.
        KeywordFunction,

        KeywordReturn,

        KeywordExit,

        // Symbols.
        SymbolBlockL,

        SymbolBlockR,

        SymbolParenthesesL,

        SymbolParenthesesR,

        SymbolContinuous,

        SymbolColon,

        SymbolSemiColon,

        SymbolArrow,

        SymbolAt,

        SymbolComma
    }
}
