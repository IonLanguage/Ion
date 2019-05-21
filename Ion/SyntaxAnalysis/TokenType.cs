namespace Ion.SyntaxAnalysis
{
    public enum TokenType
    {

        /// <summary>
        /// Represents the start of the program.
        /// </summary>
        ProgramStart,

        /// <summary>
        /// Represents the end of the program.
        /// </summary>
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
        CommentSingleLine,

        /// <summary>
        /// Represents a multi line comment.
        /// </summary>
        CommentMultiLine,

        /// <summary>
        /// Represents an entity identifier.
        /// </summary>
        Identifier,

        /// <summary>
        /// Represents a double type.
        /// </summary>
        TypeDouble,

        /// <summary>
        /// Represents an integer type.
        /// </summary>
        TypeInt,

        /// <summary>
        /// Represents a floating point type.
        /// </summary>
        TypeFloat,


        /// <summary>
        /// Represents a string type.
        /// </summary>
        TypeString,


        /// <summary>
        /// Represents a character type.
        /// </summary>
        TypeChar,


        /// <summary>
        /// Represents a boolean type.
        /// </summary>
        TypeBool,


        /// <summary>
        /// Represents a long type.
        /// </summary>
        TypeLong,


        /// <summary>
        /// Represents a void type.
        /// </summary>
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

        /// <summary>
        /// Represents the equality less than
        /// operator.
        /// </summary>
        OperatorLessThan,

        /// <summary>
        /// Represents the equality greater than
        /// operator.
        /// </summary>
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

        KeywordNamespace,

        KeywordImport,

        KeywordAlias,

        KeywordAs,

        KeywordStruct,

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
