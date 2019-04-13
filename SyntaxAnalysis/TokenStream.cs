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

        public void Reset() {
            this.enumerator = this.GetEnumerator();
        }

        public bool Skip()
        {
            return this.enumerator.MoveNext();
        }

        public Token Next()
        {
            this.Skip();

            return this.enumerator.Current;
        }

        public Token Get()
        {
            return this.enumerator.Current;
        }
    }
}
