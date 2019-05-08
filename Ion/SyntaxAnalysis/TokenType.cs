namespace Ion.SyntaxAnalysis
{
    public enum TokenType
    {
        ProgramStart,

        ProgramEnd,

        /// <summary>
        /// Represents an unidentified value.
        /// </summary>
        Unknown,

        /// <summary>
        /// Represents whitespace.
        /// </summary>
        Whitespace,

        /// <summary>
        /// Represents a single line comment.
        /// </summary>
        SingleLineComment,

        /// <summary>
        /// Represents a multi line comment.
        /// </summary>
        MultiLineComment,

        /// <summary>
        /// Represents an entity identifier.
        /// </summary>
        Identifier,

        /// <summary>
        /// Represents a double type.
        /// </summary>
        TypeDouble,

        TypeInt,

        TypeFloat,

        TypeString,

        TypeChar,

        TypeBool,

        TypeLong,

        TypeVoid,

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
        /// subtraction operator.
        /// </summary>
        OperatorSubtraction,

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
        KeywordReturn,

        KeywordExit,

        KeywordTrue,

        KeywordFalse,

        KeywordIf,

        KeywordElse,

        KeywordExternal,

        KeywordFor,

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

        SymbolMultiLineCommentR,

        SymbolBracketL,

        SymbolBracketR,

        SymbolDot
    }
}
