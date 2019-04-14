using System;
using System.Collections.Generic;

namespace LlvmSharpLang.SyntaxAnalysis
{
    public class TokenStream : List<Token>
    {
        protected int index;

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
            this.index = 0;
            this.enumerator = this.GetEnumerator();
        }

        public bool Skip()
        {
            bool successful = this.enumerator.MoveNext();

            if (successful)
            {
                this.index++;
            }

            return successful;
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

        public Token Peek()
        {
            int nextIndex = this.index + 1;

            if (this.Capacity > nextIndex)
            {
                return this[nextIndex];
            }

            return this[this.index];
        }

        public Token Get()
        {
            return this.enumerator.Current;
        }
    }
}
