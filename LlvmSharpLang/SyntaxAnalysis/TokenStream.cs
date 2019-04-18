using System;
using System.Collections.Generic;
using LlvmSharpLang.Misc;

namespace LlvmSharpLang.SyntaxAnalysis
{
    public class TokenStream : Stream<Token>
    {

        public TokenStream() : base()
        {
            // Prepare the initial enumerator.
            this.Reset();
        }

        public TokenStream(Token[] tokens) : base(tokens)
        {
            //
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

        public Token Next(TokenType type)
        {
            this.Skip(type);

            return this.enumerator.Current;
        }
    }
}
