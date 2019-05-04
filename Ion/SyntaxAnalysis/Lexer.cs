using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Ion.Misc;

namespace Ion.SyntaxAnalysis
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
    public class Lexer
    {
        public static readonly int EOF = -1;

        /// <summary>
        /// Temporarily the captured string value
        /// as a buffer.
        /// </summary>
        protected string buffer;

        public Lexer(string input, LexerOptions options = LexerOptions.IgnoreComments | LexerOptions.IgnoreWhitespace)
        {
            this.Input = input.Trim();
            this.Options = options;
        }

        /// <summary>
        /// The character located at the current
        /// position in the input string.
        /// </summary>
        public char Char => this.Input[this.Position];

        public int Position { get; set; }

        public string Input { get; }

        public LexerOptions Options { get; }

        // Defaults to ignoring whitespace unless other specified.

        /// <summary>
        /// Begin the tokenization process, obtaining/extracting all
        /// possible tokens from the input string. Tokens which are
        /// unable to be identified will default to token type unknown.
        /// </summary>
        public List<Token> Tokenize()
        {
            List<Token> tokens = new List<Token>();
            Token? nextToken = this.GetNextToken();

            // Obtain all possible tokens.
            while (nextToken.HasValue)
            {
                // If the token is unknown, issue a warning in console.
                if (nextToken.Value.Type == TokenType.Unknown)
                {
                    // TODO: This should be done through ErrorReporting (implement in the future).
                    Console.WriteLine($"Warning: Unexpected token type to be unknown, value: {nextToken.Value.Value}");
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
            if (this.Position > this.Input.Length - 1) return null;

            // Begin capturing the token. Identify the token as unknown initially.
            Token token = new Token
            {
                StartPos = this.Position,
                Type = TokenType.Unknown,

                // Default to current character to avoid infinite loop.
                Value = this.Char.ToString()
            };

            // Skip whitespace characters if applicable.
            if (this.Options.HasFlag(LexerOptions.IgnoreWhitespace))
            {
                // While the current character is whitespace.
                while (char.IsWhiteSpace(this.Char))
                {
                    // Skip over the character.
                    this.Skip();
                }
            }
            // If ignore whitespace isn't enabled, then we can save it as a token.
            else if (char.IsWhiteSpace(this.Char))
            {
                // Match all whitespace characters until we hit a normal character.
                if (this.MatchExpression(ref token, TokenType.Whitespace, Util.CreateRegex(@"[\s]+")))
                {
                    // Return the token
                    return token;
                }
            }

            // Comments have highest priority because the division operator will catch the beginning of any comments.
            // If it starts with '/', it's a candidate.
            foreach (var pair in Constants.commentTokenTypes)
                if (this.MatchExpression(ref token, pair.Value, pair.Key))
                {
                    // If the lexer should ignore comments, return the next comment.
                    if (this.Options.HasFlag(LexerOptions.IgnoreComments))
                    {
                        return this.GetNextToken();
                    }

                    return token;
                }

            // Test string against simple token type values.
            foreach (var pair in Constants.simpleTokenTypes)
                // Possible candidate.
                if (pair.Key.StartsWith(this.Char))
                {
                    // Create initial regex.
                    Regex pattern = Util.CreateRegex(Regex.Escape(pair.Key));

                    // Skimming involves removing the last character.
                    bool skim = false;

                    // If the match starts with a letter, modify the regex to force either whitespace or EOF at the end.
                    if (char.IsLetter(pair.Key[0]))
                    {
                        // Modify the regex to include whitespace at the end.
                        pattern = Util.CreateRegex($"{Regex.Escape(pair.Key)}(\\s|$|;)");

                        // Since the new regex will also pickup a whitespace along the way, we must skim it off.
                        skim = true;
                    }

                    // If the symbol is next in the input.
                    if (this.MatchExpression(ref token, pair.Value, pattern))
                    {
                        // If skimming is required, remove the last character from the token value.
                        if (skim)
                        {
                            // Reduce the position
                            this.Position -= token.Value.Length - pair.Key.Length;

                            // Skim the last character off.
                            token.Value = pair.Key;
                        }

                        // Return the token.
                        return token;
                    }
                }

            // TODO: Add comment literal support.
            // Complex types support.
            foreach (var pair in Constants.complexTokenTypes)
            {
                // If it matches, return the token (already modified by the function).
                if (this.MatchExpression(ref token, pair.Value, pair.Key))
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
        /// if positive, it'll update the referenced token to the provided type with
        /// the matched text.
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
