namespace LlvmSharpLang.SyntaxAnalysis
{
    public enum TokenType : int
    {
        /// <summary>
        /// Represents the starting point
        /// of the program.
        /// </summary>
        ProgramStart,

        /// <summary>
        /// Represents an unidentified value.
        /// </summary>
        Unknown,

        /// <summary>
        /// Represents a single line comment (// text)
        /// </summary>
        SingleLineComment,

        /// <summary>
        /// Represents a multi line comment (/* text */)
        /// </summary>
        MultiLineComment,

        /// <summary>
        /// Represents an entity identifier. (text)
        /// </summary>
        Identifier,

        /// <summary>
        /// Represents a type name.
        /// </summary>
        Type,

        /// <summary>
        /// Represents the variable
        /// assignment operator. (=)
        /// </summary>
        OperatorAssignment,

        /// <summary>
        /// Represents the mathematical
        /// addition operator. (+)
        /// </summary>
        OperatorAddition,

        /// <summary>
        /// Represents the mathematical
        /// substraction operator. (-)
        /// </summary>
        OperatorSubstraction,

        /// <summary>
        /// Represents the mathematical
        /// multiplication operator. (*)
        /// </summary>
        OperatorMultiplication,

        /// <summary>
        /// Represents the mathematical
        /// division operator. (/)
        /// </summary>
        OperatorDivision,

        /// <summary>
        /// Represents the mathematical
        /// exponential operator. (^)
        /// </summary>
        OperatorExponent,

        /// <summary>
        /// Represents the mathematical
        /// unsigned remainder operator. (%)
        /// </summary>
        OperatorModulo,

        /// <summary>
        /// Represents the escape sequence
        /// to represent special entities. (\)
        /// </summary>
        OperatorEscape,

        /// <summary>
        /// Represents the equality comparison
        /// operator. (==)
        /// </summary>
        OperatorEquality,

        OperatorLessThan,

        OperatorGreaterThan,

        OperatorNot,

        OperatorAnd,

        OperatorOr,

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

        SymbolComma,

        SymbolSingleLineComment,

        SymbolMultiLineCommentL,

        SymbolMultiLineCommentR
    }
}
