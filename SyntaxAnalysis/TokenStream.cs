using System.Collections.Generic;

namespace LlvmSharpLang
{
    public class TokenStream : LinkedList<Token>
    {
        public TokenStream(Token[] tokens) : base(tokens)
        {
            //
        }

        public TokenStream() : base()
        {
            //
        }
    }
}
