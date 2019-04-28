using System.Text.RegularExpressions;
using System.Text;
using LLVMSharp;
using LlvmSharpLang.SyntaxAnalysis;
using LlvmSharpLang.Misc;
using System.Collections.Generic;
using System;

namespace LlvmSharpLang.SyntaxAnalysis
{
    [Flags]
    public enum LexerOptions
    {
        IgnoreWhitespace = 1,

        IgnoreComments = 2
    }

    /// <summary>
    /// Parses input code string and creates
    /// corresponding tokens.
    /// </summary>
    public class Lexer
    {
        public static readonly int EOF = -1;

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
        public Lexer(string input) : this(input, (LexerOptions.IgnoreComments | LexerOptions.IgnoreWhitespace))
        {
            //
        }

        /// <summary>
        /// Begin the tokenization process, obtaining/extracting all 
        /// possible tokens from the input string. Tokens which are
        /// unable to be identified will default to token type unknown.
        /// </summary>
        public List<Token> Tokenize()
        {
            List<Token> tokens = new List<Token>();
            Token? nextToken = this.GetNextToken();

            Console.ForegroundColor = ConsoleColor.Yellow;

            // Obtain all possible tokens.
            while (nextToken.HasValue)
            {
                // If the token is unknown, issue a warning in console.
                if (nextToken.Value.Type == TokenType.Unknown)
                {
                    // TODO: This should be done through ErrorReporting (implement in the future).
                    Console.WriteLine("Warning: Unexpected token type to be unknown");
                }

                // Append token value to the result list.
                tokens.Add(nextToken.Value);

                // Continue enumeration.
                nextToken = this.GetNextToken();
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
                // Continue while whitespace is present.
                while (char.IsWhiteSpace(this.Char))
                {
                    this.Skip();
                }
            }

            // Begin capturing the token. Identify the token as unknown initially.
            Token token = new Token
            {
                StartPos = this.Position,
                Type = TokenType.Unknown,

                // Default to current character to avoid infinite loop.
                Value = this.Char.ToString()
            };

            // Comments have highest priority because the division operator will catch the beginning of any comments.
            // If it starts with '/', it's a candidate.
            foreach (var pair in Constants.commentTokenTypes)
            {
                if (this.MatchExpression(ref token, pair.Value, pair.Key))
                {
                    // If the lexer should ignore comments, return the next comment.
                    if (this.Options.HasFlag(LexerOptions.IgnoreComments))
                    {
                        return this.GetNextToken();
                    }

                    return token;
                }
            }

            // Capturing an identifier, operator or keyword.
            if (char.IsLetter(this.Char))
            {
                // Reset the buffer and position.
                int position = this.Position;
                this.buffer = this.Char.ToString();

                // Consume the value.
                while (char.IsLetterOrDigit(this.Input[position + 1]))
                {
                    position++;
                    buffer += this.Input[position];
                }

                // Assign the consumed value to the token.
                token.Value = buffer;

                // If the keyword is registered, identify the token.
                if (Constants.keywords.ContainsKey(buffer))
                {
                    // Identify the token with its corresponding keyword.
                    token.Type = Constants.keywords[buffer];

                    // Skip the length of the captured value.
                    this.Skip(token.Value.Length);

                    // Return the token.
                    return token;
                }
                // Otherwise, if the operator is registered, identify the token.
                else if (Constants.operators.ContainsKey(buffer))
                {
                    // Identify the token with its corresponding operator.
                    token.Type = Constants.operators[buffer];

                    // Skip the length of the captured value.
                    this.Skip(token.Value.Length);

                    // Return the token.
                    return token;
                }
            }

            // Test string against simple token type values.
            foreach (var pair in Constants.simpleTokenTypes)
            {
                // Possible candidate.
                if (pair.Key.StartsWith(this.Char))
                {
                    // If the symbol is next in the input.
                    if (this.MatchExpression(ref token, pair.Value, Util.CreateRegex(Regex.Escape(pair.Key))))
                    {
                        // Return the token.
                        return token;
                    }
                }
            }

            // TODO: Add comment literal support.
            // Complex types support.
            foreach (KeyValuePair<Regex, TokenType> pair in Constants.complexTokenTypes)
            {
                // If it matches, return the token (already modified by the function).
                if (this.MatchExpression(ref token, pair.Value, pair.Key, true))
                {
                    // Return the token.
                    return token;
                }
            }

            // At this point the token was not identified. Skip over any captured value.
            this.Skip(token.Value != null ? token.Value.Length : 0);

            // Return the default token. The token type defaults to unknown.
            return token;
        }

        /// <summary>
        /// Checks for a positive match for a complex type or just generic regex,
        /// if positive, it'll update the referenced token to the provided type with the matched text.
        /// </summary>
        public bool MatchExpression(ref Token token, TokenType type, Regex regex, bool modifyToken = true)
        {
            // Substrings from the current position to get the viable matching string.
            string input = this.Input.Substring(this.Position).TrimStart();
            Match match = regex.Match(input);

            // If the match is success, update the token to reflect this.
            if (match.Success && match.Index == 0)
            {
                if (modifyToken)
                {
                    token.Value = match.Value;
                    token.Type = type;

                    this.Skip(match.Value.Length);
                }
                return true;
            }

            return false;
        }

        /// <summary>
        /// Skip a specific amount of characters
        /// from the current position.
        /// </summary>
        public void Skip(int amount = 1)
        {
            // TODO
            // if (this.character + characters >= this.program.Length)
            // {
            //     this.character = this.program.Length - 1;
            //     return;
            // }

            this.Position += amount;
        }
    }

}
