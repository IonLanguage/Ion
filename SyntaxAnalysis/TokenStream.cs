using System;
using System.Collections.Generic;

namespace LlvmSharpLang.SyntaxAnalysis
{
    public class TokenStream : LinkedList<Token>
    {
        protected IEnumerator<Token> enumerator;

        public TokenStream() : base()
        {
            // Prepare the initial enumerator.
            this.Reset();
        }

        public TokenStream(Token[] tokens) : base(tokens)
        {
            // Prepare the initial enumerator.
            this.Reset();
        }

        public void Reset()
        {
            this.enumerator = this.GetEnumerator();
        }

        public bool Skip()
        {
            return this.enumerator.MoveNext();
        }

        public bool Skip(TokenType type)
        {
            bool result = this.Skip();
            TokenType currentType = this.Get().Type;

            if (currentType != type)
            {
                throw new Exception($"Expected token of type '{type}' but got '{currentType}'");
            }

            return result;
        }

        public Token Next()
        {
            this.Skip();

            return this.enumerator.Current;
        }

        public Token Next(TokenType type)
        {
            this.Skip(type);

            return this.enumerator.Current;
        }

        public Token Get()
        {
            return this.enumerator.Current;
        }
    }
}
