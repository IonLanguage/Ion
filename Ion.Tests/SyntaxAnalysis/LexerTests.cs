using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Ion.Core;
using Ion.SyntaxAnalysis;
using Ion.ErrorReporting;
using Ion.Tests.Core;

namespace Ion.Tests.SyntaxAnalysis
{
    internal sealed class LexerTests
    {
        private TokenType[] sequence;

        [SetUp]
        public void Setup()
        {
            this.sequence = new TokenType[] {
                TokenType.Identifier,
                TokenType.SymbolParenthesesL,
                TokenType.SymbolComma,
                TokenType.SymbolParenthesesR,
                TokenType.SymbolColon,
                TokenType.SymbolBlockL,
                TokenType.KeywordTrue,
                TokenType.KeywordFalse,
                TokenType.LiteralInteger,
                TokenType.LiteralDecimal,
                TokenType.LiteralString,
                TokenType.LiteralCharacter,
                TokenType.SymbolArrow,
                TokenType.OperatorAssignment,
                TokenType.OperatorEquality,
                TokenType.OperatorLessThan,
                TokenType.OperatorGreaterThan,
                TokenType.OperatorNot,
                TokenType.OperatorAnd,
                TokenType.OperatorOr,
                TokenType.Identifier,
                TokenType.SymbolSemiColon,
                TokenType.SymbolBlockR,
                TokenType.CommentMultiLine,
                TokenType.CommentSingleLine
            };
        }

        [Test]

        // Normal.
        [TestCase("id ( , ) : { true false 123 1.23 \"hello world\" 'a' => = == < > ! and or andor; } /*a*/ // abc")]

        // Dense.
        [TestCase("id(,):{true false 123 1.23\"hello world\"'a'=>= ==<>!and or andor;}/*a*///abc")]
        public void Tokenize(string input)
        {
            // Create lexer and tokenize the input.
            Lexer lexer = new Lexer(input, (LexerOptions.IgnoreWhitespace));
            List<Token> tokens = lexer.Tokenize();

            // Ensure length is the same.
            Assert.AreEqual(this.sequence.Length, tokens.Count);

            // Verify sequence.
            for (int i = 0; i < tokens.Count; i++)
            {
                // Compare tokenized token to corresponding token on the sequence.
                Assert.AreEqual(sequence[i], tokens[i].Type);
            }
        }

        [Test]
        [TestCase("~")]
        [TestCase("$")]
        [TestCase("#")]
        [TestCase("`")]
        public void NotTokenizeInvalidInput(string input)
        {
            // Create the lexer.
            Lexer lexer = new Lexer(input);

            // Tokenize the input.
            List<Token> tokens = lexer.Tokenize();

            // Ensure token's length.
            Assert.AreEqual(tokens.Count, 1);

            // Ensure token is unknown.
            Assert.AreEqual(tokens[0].Type, TokenType.Unknown);
        }

        [Test]
        [TestCase("andor", TokenType.Identifier)]
        [TestCase("anda", TokenType.Identifier)]
        [TestCase("and ", TokenType.OperatorAnd, TokenType.Whitespace)]
        [TestCase("and{", TokenType.OperatorAnd, TokenType.SymbolBlockL)]
        [TestCase("and(", TokenType.OperatorAnd, TokenType.SymbolParenthesesL)]
        [TestCase("and123", TokenType.Identifier)]
        [TestCase("and_", TokenType.Identifier)]
        [TestCase("and123_", TokenType.Identifier)]
        [TestCase("//", TokenType.CommentSingleLine)]
        [TestCase("/**/", TokenType.CommentMultiLine)]
        [TestCase("/** */", TokenType.CommentMultiLine)]
        [TestCase("/** **/", TokenType.CommentMultiLine)]
        [TestCase("/****/", TokenType.CommentMultiLine)]
        public void PossibleConflictingTokens(string input, params TokenType[] expected)
        {
            // Create the lexer without any options.
            Lexer lexer = new Lexer(input, LexerOptions.None);

            // Tokenize the input.
            List<Token> tokens = lexer.Tokenize();

            // Compare lengths.
            Assert.AreEqual(expected.Length, tokens.Count);

            // Compare results.
            for (int i = 0; i < tokens.Count; i++)
            {
                // Compare token types.
                Assert.AreEqual(expected[i], tokens[i].Type);
            }
        }
    }
}
