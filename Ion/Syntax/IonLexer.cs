#nullable enable

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Ion.Misc;
using Ion.CognitiveServices;
using Ion.Engine.Syntax;

namespace Ion.Syntax
{
    [Flags]
    public enum LexerOptions
    {
        None = 1,

        IgnoreWhitespace = 2,

        IgnoreComments = 4
    }

    /// <summary>
    /// Parses input code string and creates
    /// corresponding tokens.
    /// </summary>
    public class IonLexer : Lexer<Token, TokenType>
    {
        public LexerOptions Options { get; }

        public IonLexer(string input, LexerOptions options = LexerOptions.IgnoreComments | LexerOptions.IgnoreWhitespace) : base(input)
        {
            this.Options = options;
        }

        /// <summary>
        /// Begin the tokenization process, obtaining/extracting all
        /// possible tokens from the input string. Tokens which are
        /// unable to be identified will default to token type unknown.
        /// </summary>
        public override Token[] Tokenize()
        {
            // Ensure input contains more than zero characters.
            if (this.Input.Length <= 0)
            {
                throw new Exception("Input must contain at least one character");
            }

            // Set initial position.
            this.Position = 0;

            List<Token> tokens = new List<Token>();
            Token? nextToken = this.GetNextToken();

            // Obtain all possible tokens.
            while (nextToken != null)
            {
                // If the token is unknown, issue a warning in console.
                if (nextToken.Type == TokenType.Unknown)
                {
                    // TODO: This should be done through ErrorReporting (implement in the future).
                    Console.WriteLine($"Warning: Unexpected token type to be unknown, value: {nextToken.Value}");
                }

                // Append token value to the result list.
                tokens.Add(nextToken);

                // Continue enumeration.
                nextToken = this.GetNextToken();
            }

            return tokens.ToArray();
        }

        /// <summary>
        /// Attempt to obtain the next upcoming
        /// token.
        /// </summary>
        protected Token? GetNextToken()
        {
            // Return immediatly if position overflows.
            if (this.Position >= this.Input.Length)
            {
                return null;
            }
            else if (!this.Char.HasValue)
            {
                throw new Exception("Expected current character to not be null");
            }

            // Begin capturing the token. Identify the token as unknown initially.
            Token token = new Token(TokenType.Unknown, this.Char.Value.ToString(), this.Position);

            // Skip whitespace characters if applicable.
            if (this.Options.HasFlag(LexerOptions.IgnoreWhitespace))
            {
                // While the current character is whitespace.
                while (this.Char.HasValue && char.IsWhiteSpace(this.Char.Value))
                {
                    // Skip over the character.
                    this.Skip();
                }

                // Input terminated.
                if (!this.Char.HasValue)
                {
                    // Return null immediatly.
                    return null;
                }
            }
            // If ignore whitespace isn't enabled, then we can save it as a token.
            else if (char.IsWhiteSpace(this.Char.Value))
            {
                // Match all whitespace characters until we hit a normal character.
                if (this.MatchExpression(token, TokenType.Whitespace, Pattern.ContinuousWhitespace, out token))
                {
                    // Return the token.
                    return token;
                }
            }

            // Comments have highest priority because the division operator will catch the beginning of any comments.
            // If it starts with '/', it's a candidate.
            foreach (var pair in Constants.commentTokenTypes)
            {
                // Proceed if successful.
                if (this.MatchExpression(token, pair.Value, pair.Key, out token))
                {
                    // If the lexer should ignore comments, return the next token.
                    if (this.Options.HasFlag(LexerOptions.IgnoreComments))
                    {
                        return this.GetNextToken();
                    }

                    // Return the token.
                    return token;
                }
            }

            // Test string against simple token type values.
            foreach (var pair in Constants.simpleTokenTypes)
            {
                // Possible candidate.
                if (pair.Key.StartsWith(this.Char.Value))
                {
                    // Create initial regex.
                    Regex pattern = Util.CreateRegex(Regex.Escape(pair.Key));

                    // If the match starts with a letter, modify the regex to force either whitespace or EOF at the end.
                    if (Pattern.Identifier.IsMatch(pair.Key))
                    {
                        // Modify the regex to include whitespace/EOF/semi-colon at the end.
                        pattern = Util.CreateRegex($@"{Regex.Escape(pair.Key)}([^a-zA-Z_0-9])");
                    }

                    // If the symbol is next in the input.
                    if (this.MatchExpression(token, pair.Value, pattern, out token))
                    {
                        // Reduce the position.
                        this.Position -= token.Value.Length - pair.Key.Length;

                        // Skim the last character off.
                        token = new Token(token.Type, pair.Key, token.StartPos);

                        // Return the token.
                        return token;
                    }
                }
            }

            // Complex types support.
            foreach (var pair in Constants.complexTokenTypes)
            {
                // If it matches, return the token (already modified by the function).
                if (this.MatchExpression(token, pair.Value, pair.Key, out token))
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

        protected void SetPosition(int position)
        {
            // Catch out-of-bounds positioning.
            if (position < 0)
            {
                position = 0;
            }

            // Set the position.
            this.Position = position;
        }

        /// <summary>
        /// Checks for a positive match for a complex type or just generic regex,
        /// if positive, it'll update the referenced token to the provided type with
        /// the matched text.
        /// </summary>
        protected bool MatchExpression(Token token, TokenType type, Regex regex, out Token result)
        {
            // Substrings from the current position to get the viable matching string.
            string input = this.Input.Substring(this.Position);
            Match match = regex.Match(input);

            // If successful, return a new token with different value and type.
            if (match.Success && match.Index == 0)
            {
                // Modify the result.
                result = new Token(type, match.Value, token.StartPos);

                // Skip the capture value's amount.
                this.Skip(result.Value.Length);

                // Return true to indicate success.
                return true;
            }

            // Replace result with the original token, making no changes.
            result = token;

            // Return false to indicate failure.
            return false;
        }

        /// <summary>
        /// Skip a specific amount of characters
        /// from the current position.
        /// </summary>
        public void Skip(int amount = 1)
        {
            // TODO: Ensure overflow does not occur, also verify amount?
            // if (this.character + characters >= this.program.Length)
            // {
            //     this.character = this.program.Length - 1;
            //     return;
            // }

            this.SetPosition(this.Position + amount);
        }
    }
}
