using System.Collections.Generic;
using NUnit.Framework;
using LlvmSharpLang.Core;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Tests
{
    internal sealed class LexerTests
    {
        private TokenType[] sequence;

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
                TokenType.SymbolSemiColon,
                TokenType.SymbolBlockR
            };
        }

        [Test]

        // Normal.
        [TestCase("fn id ( , ) : { 123 1.23 \"hello world\" 'a' ; }")]

        // Dense.
        [TestCase("fn id(,):{123 1.23\"hello world\"'a';}")]
        public void Tokenize(string input)
        {
            Lexer lexer = new Lexer(input);
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
        public void StaticCreateDefault()
        {
            Assert.AreEqual(Error.Create("Test"), "GenericError: Test");
            Assert.Pass();
        }
    }
}
