using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using LlvmSharpLang.Core;
using LlvmSharpLang.SyntaxAnalysis;
using LlvmSharpLang.ErrorReporting;
using LlvmSharpLang.Tests.Core;

namespace LlvmSharpLang.Tests.SyntaxAnalysis
{
    internal sealed class LexerTests
    {
        private TokenType[] sequence;

        private TokenType[] fileSequence;

        [SetUp]
        public void Setup()
        {
            this.sequence = new TokenType[] {
                TokenType.KeywordFunction,
                TokenType.Identifier,
                TokenType.SymbolParenthesesL,
                TokenType.SymbolComma,
                TokenType.SymbolParenthesesR,
                TokenType.SymbolColon,
                TokenType.SymbolBlockL,
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
                TokenType.SymbolSemiColon,
                TokenType.SymbolBlockR
            };

            this.fileSequence = new TokenType[] {
                TokenType.KeywordFunction,
                TokenType.Identifier,
                TokenType.SymbolParenthesesL,
                TokenType.Identifier,
                TokenType.Identifier,
                TokenType.SymbolComma,
                TokenType.Identifier,
                TokenType.Identifier,
                TokenType.SymbolParenthesesR,
                TokenType.SymbolColon,
                TokenType.Identifier,
                TokenType.SymbolBlockL,
                TokenType.LiteralInteger,
                TokenType.SymbolSemiColon,
                TokenType.LiteralDecimal,
                TokenType.SymbolSemiColon,
                TokenType.LiteralString,
                TokenType.SymbolSemiColon,
                TokenType.LiteralCharacter,
                TokenType.SymbolSemiColon,
                TokenType.SymbolParenthesesL,
                TokenType.SymbolParenthesesR,
                TokenType.SymbolArrow,
                TokenType.SymbolBlockL,
                TokenType.SymbolBlockR,
                TokenType.SymbolSemiColon,
                TokenType.Identifier,
                TokenType.OperatorAssignment,
                TokenType.Identifier,
                TokenType.SymbolSemiColon,
                TokenType.KeywordIf,
                TokenType.SymbolParenthesesL,
                TokenType.Identifier,
                TokenType.OperatorEquality,
                TokenType.Identifier,
                TokenType.SymbolParenthesesR,
                TokenType.SymbolBlockL,
                TokenType.SymbolBlockR,
                TokenType.SymbolSemiColon,
                TokenType.KeywordIf,
                TokenType.SymbolParenthesesL,
                TokenType.Identifier,
                TokenType.OperatorLessThan,
                TokenType.Identifier,
                TokenType.OperatorAnd,
                TokenType.Identifier,
                TokenType.OperatorGreaterThan,
                TokenType.Identifier,
                TokenType.SymbolParenthesesR,
                TokenType.SymbolBlockL,
                TokenType.SymbolBlockR,
                TokenType.SymbolSemiColon,
                TokenType.KeywordIf,
                TokenType.SymbolParenthesesL,
                TokenType.OperatorNot,
                TokenType.Identifier,
                TokenType.OperatorOr,
                TokenType.OperatorNot,
                TokenType.Identifier,
                TokenType.SymbolParenthesesR,
                TokenType.SymbolBlockL,
                TokenType.SymbolBlockR,
                TokenType.SymbolSemiColon,
                TokenType.SymbolBlockR
            };
        }

        [Test]

        // Normal.
        [TestCase("fn id ( , ) : { 123 1.23 \"hello world\" 'a' => = == < > ! and or ; }")]

        // Dense.
        [TestCase("fn id(,):{123 1.23\"hello world\"'a'=>= ==<>!and or;}")]
        public void Tokenize(string input)
        {
            // Create lexer and tokenize the input.
            Lexer lexer = new Lexer(input, (
                LexerOptions.Debug
                | LexerOptions.IgnoreComments
                | LexerOptions.IgnoreWhitespace
            ));

            List<Token> tokens = lexer.Tokenize();

            // Ensure length is the same.
            Assert.AreEqual(this.sequence.Length, tokens.Count);

            // Verify sequence.
            for (int i = 0; i < tokens.Count; i++)
            {
                // Compare tokenized token to corresponding token on the sequence.
                Assert.AreEqual(sequence[i], tokens[i].Type);
            }

            Assert.Pass();
        }

        [Test]
        [TestCase("~")]
        [TestCase(".")]
        [TestCase("$")]
        [TestCase("#")]
        [TestCase("`")]
        public void NotTokenizeInvalidInput(string input)
        {
            Lexer lexer = new Lexer(input);
            List<Token> tokens = lexer.Tokenize();

            // Ensure tokens length.
            Assert.AreEqual(tokens.Count, 1);

            // Ensure token is unknown.
            Assert.AreEqual(tokens[0].Type, TokenType.Unknown);

            Assert.Pass();
        }
    }
}
