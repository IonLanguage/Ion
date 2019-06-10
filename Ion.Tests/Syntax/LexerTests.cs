using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Ion.Core;
using Ion.Syntax;
using Ion.NoticeReporting;
using Ion.Tests.Core;

namespace Ion.Tests.Syntax
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
            IonLexer lexer = new IonLexer(input, (LexerOptions.IgnoreWhitespace));
            Token[] tokens = lexer.Tokenize();

            // Ensure length is the same.
            Assert.AreEqual(this.sequence.Length, tokens.Length);

            // Verify sequence.
            for (int i = 0; i < tokens.Length; i++)
            {
                // Compare tokenized token to corresponding token on the sequence.
                Assert.AreEqual(sequence[i], tokens[i].Type);
            }
        }

        [Test]
        [TestCase("~")]
        [TestCase("$")]
        [TestCase("`")]
        public void NotTokenizeInvalidInput(string input)
        {
            // Create the lexer.
            IonLexer lexer = new IonLexer(input);

            // Tokenize the input.
            Token[] tokens = lexer.Tokenize();

            // Ensure token's length.
            Assert.AreEqual(tokens.Length, 1);

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
        [TestCase("Test /****/", TokenType.Identifier, TokenType.Whitespace, TokenType.CommentMultiLine)]
        public void PossibleConflictingTokens(string input, params TokenType[] expected)
        {
            // Create the lexer without any options.
            IonLexer lexer = new IonLexer(input, LexerOptions.None);

            // Tokenize the input.
            Token[] tokens = lexer.Tokenize();

            // Compare lengths.
            Assert.AreEqual(expected.Length, tokens.Length);

            // Compare results.
            for (int i = 0; i < tokens.Length; i++)
            {
                // Compare token types.
                Assert.AreEqual(expected[i], tokens[i].Type);
            }
        }
    }
}
