using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.CognitiveServices
{
    public class TokenIdentifier
    {
        /// <summary>
        /// Attempt to identify a simple, corresponding
        /// token type from a string value.
        /// </summary>
        public static TokenType? IdentifySimple(string value)
        {
            // Loop through all simple token type maps.
            foreach (var tokenTypeMap in Constants.simpleTokenTypeMaps)
            {
                // Identify and return token type if applicable.
                if (tokenTypeMap.ContainsKey(value))
                {
                    return tokenTypeMap[value];
                }
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
    }
}
