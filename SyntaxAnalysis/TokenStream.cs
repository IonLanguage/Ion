using System.Collections.Generic;

namespace LlvmSharpLang
{
    public class TokenStream : LinkedList<Token>
    {
        public TokenStream() : base()
        {
            //
        }

        public TokenStream(Token[] tokens) : base(tokens)
        {
            //
        }

        public bool Skip()
        {
            return this.GetEnumerator().MoveNext();
        }

        public Token Next()
        {
            this.Skip();

            return this.GetEnumerator().Current;
        }

        public Token Get()
        {
            return this.GetEnumerator().Current;
        }
    }
}
