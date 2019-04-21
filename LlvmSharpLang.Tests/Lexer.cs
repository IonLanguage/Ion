using System.Collections.Generic;
using NUnit.Framework;
using LlvmSharpLang.Core;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Tests
{
    public class LexerTests
    {
        protected List<TokenType> tokenTypes;

        // [SetUp]
        public void Setup()
        {
            this.tokenTypes = new List<TokenType>() {
                TokenType.KeywordFunction,
                TokenType.Identifier,
                TokenType.SymbolBlockL,
                TokenType.SymbolBlockR,
                TokenType.SymbolColon,
                TokenType.SymbolParenthesesL,
                TokenType.LiteralInteger,
                TokenType.SymbolSemiColon,
                TokenType.LiteralString,
                TokenType.SymbolSemiColon,
                TokenType.LiteralCharacter,
                TokenType.SymbolSemiColon,
                TokenType.LiteralDecimal,
                TokenType.SymbolSemiColon,
                TokenType.OperatorEquality,
                TokenType.SymbolSemiColon,
                TokenType.SymbolParenthesesR
            };
        }

        // [Test]
        public void LexerLexes()
        {
            Lexer lexer = new Lexer("fn hello(int arg): Int { 123; \"123\"; 'a'; 1.23; ==; }");
            List<Token> tokens = lexer.Tokenize();

            tokens.ForEach(token =>
            {
                Assert.AreEqual(tokenTypes[tokens.IndexOf(token)], token.Type);
            });

            Assert.Pass();
        }

        // [Test]
        public void StaticCreateDefault()
        {
            Assert.AreEqual(Error.Create("Test"), "GenericError: Test");
            Assert.Pass();
        }
    }
}
