using Ion.Engine.Syntax;

namespace Ion.Syntax
{
    public class Token : GenericToken<TokenType>
    {
        // TODO: Start position must be specified even for empty tokens.
        public static Token Empty => new Token(TokenType.Empty, "", 0);

        public Token(TokenType type, string value, int startPos) : base(type, value, startPos)
        {
            //
        }

        /// <summary>
        /// Stringify the token to be able to be inspected
        /// in the console.
        /// </summary>
        public override string ToString()
        {
            return $"[{this.Type}:'{this.Value}'@{this.StartPos}-{this.EndPos}]";
        }
    }
}
