using LLVMSharp;
using LlvmSharpLang.SyntaxAnalysis;
using System.Collections.Generic;
using System;

namespace LlvmSharpLang.LexicalAnalysis
{
	public class Lexer
	{
		public char current { get => this.input[this.character]; }
		public int character { get; set; }

		public string input { get; }
		public bool ignoreWhitespace { get; }

		protected static readonly Dictionary<string, TokenType> keywordMap = new Dictionary<string, TokenType> {
			{"fn", TokenType.KeywordFn}
		};

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

				if (keywordMap.ContainsKey(value))
				{
					token.Type = keywordMap[value];
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

			if (this.current == '@')
			{
				this.Skip();
				return new Token
				{
					StartPos = start,
					Type = TokenType.SymbolAt,
					Value = "@"
				};
			}

			if (this.current == '{')
			{
				this.Skip();
				return new Token
				{
					StartPos = start,
					Type = TokenType.SymbolBlockL,
					Value = "{"
				};
			}

			if (this.current == '}')
			{
				this.Skip();
				return new Token
				{
					StartPos = start,
					Type = TokenType.SymbolBlockR,
					Value = "}"
				};
			}

			if (this.current == '(')
			{
				this.Skip();
				return new Token
				{
					StartPos = start,
					Type = TokenType.SymbolParenthesesL,
					Value = "("
				};
			}

			if (this.current == ')')
			{
				this.Skip();
				return new Token
				{
					StartPos = start,
					Type = TokenType.SymbolParenthesesR,
					Value = ")"
				};
			}

			if (this.current == ':')
			{
				this.Skip();
				return new Token
				{
					StartPos = start,
					Type = TokenType.SymbolColon,
					Value = ":"
				};
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