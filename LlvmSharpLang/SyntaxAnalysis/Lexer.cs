using LLVMSharp;
using LlvmSharpLang.SyntaxAnalysis;
using System.Collections.Generic;
using System;

namespace LlvmSharpLang.SyntaxAnalysis
{
    public class Lexer
    {
        public char current { get => this.input[this.character]; }
        public int character { get; set; }

        public string input { get; }
        public bool ignoreWhitespace { get; }

        public Lexer(string input, bool ignoreWhitespace = true)
        {
            this.input = input;
            this.ignoreWhitespace = ignoreWhitespace;
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
            if (this.character > this.input.Length - 1)
            {
                return null;
            }

            if (this.ignoreWhitespace)
            {
                while (char.IsWhiteSpace(this.current))
                {
                    this.Skip();
                }
            }

            int start = this.character;
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

            if (char.IsDigit(this.current))
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

            if (this.current == '#')
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

            this.character += characters;
        }



    }

}