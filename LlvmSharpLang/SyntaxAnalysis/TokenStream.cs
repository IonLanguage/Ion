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

        /// <summary>
        /// Skip the current token and ensure
        /// the next token matches the provided
        /// token type.
        /// </summary>
        public bool Skip(TokenType type)
        {
            bool result = this.Skip();
            TokenType currentType = this.Get().Type;

            // Ensure next token's type matches provided token type.
            if (currentType != type)
            {
                throw new Exception($"Expected next token to be of type '{type}' but got '{currentType}'");
            }

            return result;
        }

        /// <summary>
        /// Retrieve the current token and
        /// ensure that its type matches the
        /// provided token type.
        /// </summary>
        public Token Get(TokenType type)
        {
            Token token = this.Get();

            // Ensure current token's type matches provided token type.
            if (token.Type != type)
            {
                throw new Exception($"Expected current token to be of type '{type}' but got '{token.Type}'");
            }

            return token;
        }

        /// <summary>
        /// Skip the current token and ensure
        /// the next token matches the provided
        /// token type. Returns the next token.
        /// </summary>
        public Token Next(TokenType type)
        {
            this.Skip(type);

            return this.Get();
        }
    }
}
