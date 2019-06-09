using Ion.Syntax;

namespace Ion.CognitiveServices
{
    public class Precedence
    {
        /// <summary>
        /// Attempt to retrieve the precedence
        /// of an operator. Returns '-1' if the
        /// provided token type is not linked to
        /// a valid operator.
        /// </summary>
        public static int Get(TokenType tokenType)
        {
            // Return the precedence linked to the provided token type if applicable.
            if (Constants.operatorPrecedence.ContainsKey(tokenType))
            {
                return Constants.operatorPrecedence[tokenType];
            }

            // Otherwise, return the default value.
            return -1;
        }

        public static int Get(Token token)
        {
            return Precedence.Get(token.Type);
        }
    }
}
