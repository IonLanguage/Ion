namespace Ion.SyntaxAnalysis
{
    public struct Token
    {
        public static Token Empty => new Token
        {
            Type = TokenType.Empty
        };

        public TokenType Type;

        public string Value;

        public int StartPos;

        /// <summary>
        ///     The end position of this token.
        ///     Returns the start position if the value is null.
        /// </summary>
        public int EndPos
        {
            get
            {
                // No value is set.
                if (this.Value == null)
                {
                    return this.StartPos;
                }

                return this.StartPos + this.Value.Length;
            }
        }

        /// <summary>
        /// Set the token's type to the special empty signal.
        /// </summary>
        public void SetEmpty()
        {
            this.Type = TokenType.Empty;
        }

        /// <summary>
        ///     Stringify the token to be able to be inspected
        ///     in the console.
        /// </summary>
        public override string ToString()
        {
            return $"[{this.Type}:'{this.Value}'@{this.StartPos}-{this.EndPos}]";
        }
    }
}
