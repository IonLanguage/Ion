using Ion.CodeGeneration;
using Ion.SyntaxAnalysis;

namespace Ion.CognitiveServices
{
    public static class TokenIdentifier
    {
        /// <summary>
        /// Attempt to identify a simple, corresponding
        /// token type from a string value.
        /// </summary>
        public static TokenType? IdentifySimple(string value)
        {
            // Attempt to identify and return token type.
            if (Constants.simpleTokenTypes.ContainsKey(value))
            {
                return Constants.simpleTokenTypes[value];
            }

            return null;
        }

        /// <summary>
        /// Attempt to identify a complex, corresponding
        /// token type from a string value.
        /// </summary>
        public static TokenType? IdentifyComplex(string value)
        {
            // Loop through all complex token types.
            foreach (var complexTokenType in Constants.complexTokenTypes)
            {
                // Attempt to match the complex token type's regex pattern.
                if (complexTokenType.Key.IsMatch(value))
                {
                    return complexTokenType.Value;
                }
            }

            // No match found, return null.
            return null;
        }

        /// <summary>
        /// Determine whether the string value is
        /// linked to a valid operator.
        /// </summary>
        public static bool IsOperator(string value)
        {
            return Constants.operators.ContainsKey(value);
        }

        /// <summary>
        /// Determine whether the provided token type
        /// is linked to a valid operator.
        /// </summary>
        public static bool IsOperator(TokenType tokenType)
        {
            return Constants.operators.ContainsValue(tokenType);
        }

        public static bool IsNumeric(TokenType tokenType)
        {
            return TokenGroups.numeric.Contains(tokenType);
        }

        public static bool IsNumeric(Token token)
        {
            return IsNumeric(token.Type);
        }

        public static bool IsBoolean(TokenType tokenType)
        {
            return TokenGroups.boolean.Contains(tokenType);
        }

        public static bool IsBoolean(Token token)
        {
            return IsBoolean(token.Type);
        }

        /// <summary>
        /// Determine if the provided token type
        /// is representing a type
        /// </summary>
        public static bool IsType(TokenType tokenType)
        {
            return Constants.types.ContainsValue(tokenType);
        }

        /// <summary>
        /// Determine if the provided token is
        /// representing a type
        /// </summary>
        public static bool IsType(Token token)
        {
            return IsType(token.Type);
        }

        /// <summary>
        /// Determine whether the string value is
        /// linked to a valid symbol.
        /// </summary>
        public static bool IsSymbol(string value)
        {
            return Constants.symbols.ContainsKey(value);
        }

        /// <summary>
        /// Determine whether the string value is
        /// linked to a valid keyword.
        /// </summary>
        public static bool IsKeyword(string value)
        {
            return Constants.keywords.ContainsKey(value);
        }

        /// <summary>
        /// Determine whether the provided token type
        /// falls under the literal category.
        /// </summary>
        public static bool IsLiteral(TokenType tokenType)
        {
            return tokenType == TokenType.LiteralString
                   || tokenType == TokenType.LiteralInteger
                   || tokenType == TokenType.LiteralDecimal
                   || tokenType == TokenType.LiteralCharacter;
        }
    }
}
