using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.CognitiveServices
{
    public class TokenIdentifier
    {
        /// <summary>
        /// Attempt to identify the simple, corresponding
        /// token type from a string value.
        /// </summary>
        public static TokenType? Identify(string value)
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
    }
}
