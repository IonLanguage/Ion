using System;
using System.Text;
using Ion.Core;
using Ion.Misc;

namespace Ion.SyntaxAnalysis
{
    public class TokenStream : Stream<Token>
    {
        public delegate void NextUntilCallback(Token token);

        public TokenStream()
        {
            // Prepare the initial enumerator.
            this.Reset();
        }

        public TokenStream(Token[] tokens) : base(tokens)
        {
            //
        }

        /// <summary>
        /// Create a string with the values of the
        /// the tokens within the provided range.
        /// </summary>
        public string Join(int from, int to)
        {
            // Ensure list contains at least one item.
            if (this.Count == 0)
            {
                throw new IndexOutOfRangeException("List contains no items");
            }
            // Validate from range.
            else if (from < 0)
            {
                throw new IndexOutOfRangeException("From index argument must be zero or higher");
            }
            // Validate to range.
            else if (to > this.Count - 1)
            {
                throw new IndexOutOfRangeException("To index is larger than the total amount of items in the list");
            }

            // Create a new string builder class instance.
            StringBuilder builder = new StringBuilder();

            // Create the for loop with the provided range.
            for (int i = from; i < to; i++)
            {
                // Append only all the values of the tokens, along with a leading space.
                builder.Append(" " + this[i].Value);
            }

            // Retrieve the builder's resulting string.
            string result = builder.ToString();

            // Trim the result.
            result.Trim();

            // Return the resulting string.
            return result;
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
        /// Create and insert bounding start and end tokens.
        /// </summary>
        public void InsertBounds()
        {
            // Insert program start token.
            this.Insert(0, SpecialToken.ProgramStart);

            // Append program end token.
            this.Add(SpecialToken.ProgramEnd);
        }

        /// <summary>
        /// Ensure that the current token's type
        /// matches the token type provided, otherwise
        /// throw an error.
        /// </summary>
        public void EnsureCurrent(TokenType type)
        {
            Token current = this.Get();

            // Ensure current token's type matches provided token type.
            if (current.Type != type)
            {
                throw new Exception($"Expected current token to be of type '{type}' but got '{current.Type}'");
            }
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
            this.EnsureCurrent(type);

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

        public void NextUntil(TokenType type, NextUntilCallback callback)
        {
            // Create the buffer token.
            Token buffer = this.Get();

            // Initiate the loop.
            while (buffer.Type != type)
            {
                // Invoke the callback.
                callback(buffer);

                // At this point, throw an error if EOF was reached.
                if (this.IsLastItem)
                {
                    throw new IndexOutOfRangeException($"Expected '{type}' to be encountered, but got EOF");
                }

                // Update the buffer.
                buffer = this.Next();
            }
        }
    }
}
