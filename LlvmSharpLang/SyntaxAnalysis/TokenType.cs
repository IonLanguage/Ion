namespace LlvmSharpLang.SyntaxAnalysis
{
    public enum TokenType : int
    {
        /// <summary>
        /// Represents the starting point
        /// of the program.
        /// </summary>
        ProgramStart,

        Unknown,

        Identifier,

        Type,

        /// <summary>
        /// Represents the variable
        /// assignment operator.
        /// </summary>
        OperatorAssignment,

        /// <summary>
        /// Represents the mathematical
        /// addition operator.
        /// </summary>
        OperatorAddition,

        /// <summary>
        /// Represents the mathematical
        /// substraction operator.
        /// </summary>
        OperatorSubstraction,

        /// <summary>
        /// Represents the mathematical
        /// multiplication operator.
        /// </summary>
        OperatorMultiplication,

        /// <summary>
        /// Represents the mathematical
        /// division operator.
        /// </summary>
        OperatorDivision,

        /// <summary>
        /// Represents the mathematical
        /// exponential operator.
        /// </summary>
        OperatorExponent,

        /// <summary>
        /// Represents the mathematical
        /// unsigned remainder operator.
        /// </summary>
        OperatorModulo,

        /// <summary>
        /// Represents the escape sequence
        /// to represent special entities.
        /// </summary>
        OperatorEscape,

        /// <summary>
        /// Represents the equality comparison
        /// operator.
        /// </summary>
        OperatorEquality,

        // Functionality operators.
        OperatorPipe,

        OperatorAddressOf,

        // Literals.
        /// <summary>
        /// Represents an Int32 literal.
        /// </summary>
        LiteralInteger,

        /// <summary>
        /// Represents a Double literal.
        /// </summary>
        LiteralDecimal,

        /// <summary>
        /// Represents a single character.
        /// </summary>
        LiteralCharacter,

        /// <summary>
        /// Represents a string literal.
        /// </summary>
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
