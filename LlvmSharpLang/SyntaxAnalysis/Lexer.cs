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

        public char current
        {
            get => this.Input[this.Char];
        }

        public int Char { get; set; }

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

        public List<Token> Tokenize()
        {
            List<Token> tokens = new List<Token>();
            Token? next = this.GetNextToken();

            while (next.HasValue)
            {
                tokens.Add(next.Value);
                next = this.GetNextToken();
            }

            return tokens;
        }

        public Token? GetNextToken()
        {
            if (this.Char > this.Input.Length - 1)
            {
                return null;
            }
            else if (this.Options.IgnoreWhitespace)
            {
                while (char.IsWhiteSpace(this.current))
                {
                    this.Skip();
                }
            }

            int start = this.Char;
            Token token = new Token { StartPos = start };

            if (char.IsLetter(this.current))
            {
                string value = this.current.ToString();

                this.Skip();

                while (char.IsLetterOrDigit(this.current))
                {
                    value += this.current;
                    this.Skip();
                }

                token.Value = value;
                token.Type = TokenType.Id;

                if (Constants.keywordMap.ContainsKey(value))

                {
                    token.Type = Constants.keywordMap[value];
                }

                return token;
            }
            else if (char.IsDigit(this.current))
            {
                string value = this.current.ToString();

                this.Skip();

                while (char.IsDigit(this.current))
                {
                    value += this.current;
                    this.Skip();
                }

                token.Type = TokenType.LiteralInteger;
                token.Value = int.Parse(value).ToString();

                return token;
            }

            if (Constants.symbolMap.ContainsKey(this.current.ToString()))
            {
                token.Type = Constants.symbolMap[this.current.ToString()];
                token.Value = this.current.ToString();

                this.Skip();
                return token;
            }
            else if (this.current == '#')
            {
                this.Skip();

                while (this.current != '\n' && this.current != '\r' && this.current != -1)
                {
                    this.Skip();
                }

                if (this.current != -1)
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

            this.Char += characters;
        }
    }

}
