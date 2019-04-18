using LLVMSharp;
using LlvmSharpLang.SyntaxAnalysis;
using System.Collections.Generic;
using System;

namespace LlvmSharpLang.SyntaxAnalysis
{
    [Flags]
    public enum LexerOptions
    {
        IgnoreWhitespace = 1
    }

    /// <summary>
    /// Parses input code string and creates
    /// corresponding tokens.
    /// </summary>
    public class Lexer
    {
        /// <summary>
        /// The character located at the current
        /// position in the input string.
        /// </summary>
        public char Char
        {
            get => this.Input[this.Position];
        }

        public int Position { get; set; }

        public string Input { get; }

        public LexerOptions Options { get; }

        /// <summary>
        /// Temporarily the captured string value
        /// as a buffer.
        /// </summary>
        protected string buffer;

        public Lexer(string input, LexerOptions options)
        {
            this.Input = input;
            this.Options = options;
        }

        public Lexer(string input) : this(input, 0)
        {
            //
        }

        /// <summary>
        /// Begin the tokenization process,
        /// obtaining/extracting all possible
        /// tokens from the input string.
        /// </summary>
        public List<Token> Tokenize()
        {
            List<Token> tokens = new List<Token>();
            Token? next = this.GetNextToken();

            while (next.HasValue)
            {
                // Append token value to the result list.
                tokens.Add(next.Value);

                // Continue enumeration.
                next = this.GetNextToken();
            }

            return tokens;
        }

        /// <summary>
        /// Attempt to obtain the next upcoming
        /// token.
        /// </summary>
        public Token? GetNextToken()
        {
            // Return immediatly if position overflows.
            if (this.Position > this.Input.Length - 1)
            {
                return null;
            }
            // Skip whitespace characters if applicable.
            else if (this.Options.HasFlag(LexerOptions.IgnoreWhitespace))
            {
                while (char.IsWhiteSpace(this.Char))
                {
                    this.Skip();
                }
            }

            // Begin capturing the token.
            Token token = new Token
            {
                StartPos = this.Position
            };

            // Capturing either an identifier or keyword.
            if (char.IsLetter(this.Char))
            {
                // Initialize the buffer.
                this.buffer = this.Skip();

                // Consume the value.
                while (char.IsLetterOrDigit(this.Char))
                {
                    buffer += this.Char;
                    this.Skip();
                }

                token.Value = buffer;

                // Identify the token as an identifier by default.
                token.Type = TokenType.Id;

                // If the keyword is registered, identify the token.
                if (Constants.keywords.ContainsKey(buffer))
                {
                    token.Type = Constants.keywords[buffer];
                }

                return token;
            }
            else if (char.IsDigit(this.Char))
            {
                // Initialize the buffer.
                this.buffer = this.Skip();

                while (char.IsDigit(this.Char))
                {
                    buffer += this.Char;
                    this.Skip();
                }

                token.Type = TokenType.LiteralInteger;
                token.Value = int.Parse(buffer).ToString();

                return token;
            }

            // TODO: Should be buffer instead of current char.
            if (Constants.symbols.ContainsKey(this.Char.ToString()))
            {
                token.Type = Constants.symbols[this.Char.ToString()];
                token.Value = this.Char.ToString();

                this.Skip();

                return token;
            }
            else if (this.Char == '#')
            {
                this.Skip();

                while (this.Char != '\n' && this.Char != '\r' && this.Char != -1)
                {
                    this.Skip();
                }

                if (this.Char != -1)
                {
                    return this.GetNextToken();
                }
            }

            return null;
        }

        /// <summary>
        /// Skip a specific amount of characters
        // from the current position.
        /// </summary>
        public string Skip(int amount = 1)
        {
            // TODO
            // if (this.character + characters >= this.program.Length)
            // {
            //     this.character = this.program.Length - 1;
            //     return;
            // }

            this.Position += amount;

            return this.Input.Substring(this.Position, amount);
        }
    }

}
