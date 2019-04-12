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
    }
}
