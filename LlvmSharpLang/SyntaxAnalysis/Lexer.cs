using System.Text;
using LLVMSharp;
using LlvmSharpLang.SyntaxAnalysis;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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

        // Defaults to ignoring whitespace unless other specified.
        public Lexer(string input) : this(input, (LexerOptions.IgnoreWhitespace))
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
                int position = this.Position;
                this.buffer = this.Char.ToString();

                // Consume the value.
                while (char.IsLetterOrDigit(this.Input[position]))
                {
                    position += 1;
                    buffer += this.Input[position];
                }

                token.Value = buffer;

                // If the keyword is registered, identify the token.
                if (Constants.keywords.ContainsKey(buffer))
                {
                    token.Type = Constants.keywords[buffer];
                    return token;
                }
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

            // Complex types support.
            foreach (KeyValuePair<Regex, TokenType> pair in Constants.complexTokenTypes)
            {
                // If it matches, return the token (already modified by the function).
                if (this.MatchExpression(ref token, pair.Value, pair.Key))
                {
                    return token;
                }
            }

            return null;
        }

        /// <summary>
        /// Checks for a positive match for a complex type or just generic regex,
        /// if positive, it'll update the referenced token to the provided type with the matched text.
        /// </summary>
        public bool MatchExpression(ref Token token, TokenType type, Regex regex)
        {
            // Substrings from the current position to get the viable matching string.
            string input = this.Input.Substring(this.Position).TrimStart();
            Match match = regex.Match(input);

            // If the match is success, update the token to reflect this.
            if (match.Success && match.Index == 0)
            {
                token.Value = match.Value;
                token.Type = type;

                this.Skip(match.Value.Length);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Skip a specific amount of characters
        /// from the current position.
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

            return this.Input.Substring(this.Position - amount, amount);
        }
    }

}
