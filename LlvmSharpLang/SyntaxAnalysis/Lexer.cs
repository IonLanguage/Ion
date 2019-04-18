using LLVMSharp;
using LlvmSharpLang.SyntaxAnalysis;
using System.Collections.Generic;
using System;

namespace LlvmSharpLang.SyntaxAnalysis
{
    public struct LexerOptions
    {
        public bool IgnoreWhitespace { get; set; }
    }

    /// <summary>
    /// Parses input code string and creates
    /// corresponding tokens.
    /// </summary>
    public class Lexer
    {
        public static readonly LexerOptions defaultOptions = new LexerOptions
        {
            IgnoreWhitespace = true
        };

        public char Char
        {
            get => this.Input[this.Position];
        }

        public int Position { get; set; }

        public string Input { get; }

        public LexerOptions Options { get; }

        protected static readonly Dictionary<string, TokenType> keywordMap = new Dictionary<string, TokenType> {
            {"fn", TokenType.KeywordFn}
        };

        public Lexer(string input, LexerOptions options)
        {
            this.Input = input;
            this.Options = options;
        }

        public Lexer(string input) : this(input, Lexer.defaultOptions)
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
            else if (this.Options.IgnoreWhitespace)
            {
                while (char.IsWhiteSpace(this.Char))
                {
                    this.Skip();
                }
            }

            int start = this.Position;

            Token token = new Token
            {
                StartPos = start
            };

            if (char.IsLetter(this.Char))
            {
                string value = this.Char.ToString();

                this.Skip();

                while (char.IsLetterOrDigit(this.Char))
                {
                    value += this.Char;
                    this.Skip();
                }

                token.Value = value;

                // Identify the token as an Id by default.
                token.Type = TokenType.Id;

                // If the keyword is registered, identify the token.
                if (Constants.keywordMap.ContainsKey(value))
                {
                    token.Type = Constants.keywordMap[value];
                }

                return token;
            }
            else if (char.IsDigit(this.Char))
            {
                string value = this.Char.ToString();

                this.Skip();

                while (char.IsDigit(this.Char))
                {
                    value += this.Char;
                    this.Skip();
                }

                token.Type = TokenType.LiteralInteger;
                token.Value = int.Parse(value).ToString();

                return token;
            }

            if (Constants.symbolMap.ContainsKey(this.Char.ToString()))
            {
                token.Type = Constants.symbolMap[this.Char.ToString()];
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

        public void Skip(int characters = 1)
        {
            // if (this.character + characters >= this.program.Length)
            // {
            //     this.character = this.program.Length - 1;
            //     return;
            // }

            this.Position += characters;
        }
    }

}
