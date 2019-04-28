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
        /// Skip the current token and ensure both
        /// the next and current token matches the provided
        /// corresponding token types.
        /// </summary>
        public bool Skip(TokenType next, TokenType? current = null)
        {
            // Ensure current token matches.
            if (current.HasValue)
            {
                TokenType currentType = this.Get().Type;

                // Ensure current token's type matches provided token type.
                if (currentType != current)
                {
                    throw new Exception($"Expected current token to be of type '{current}' but got '{currentType}'");
                }
            }

            // Skip current token.
            bool result = this.Skip();

            // Ensure next token matches.
            TokenType nextType = this.Get().Type;

            // Ensure next token's type matches provided token type.
            if (nextType != next)
            {
                throw new Exception($"Expected next token to be of type '{next}' but got '{nextType}'");
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
