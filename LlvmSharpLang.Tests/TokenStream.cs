using LlvmSharpLang.SyntaxAnalysis;
using NUnit.Framework;

namespace LlvmSharpLang.Tests
{
    public class TokenStreamTests
    {
        protected TokenStream stream;

        [SetUp]
        public void Setup()
        {
            this.stream = new TokenStream
            {
                new Token
                {
                    Type = TokenType.Unknown
                },

                new Token
                {
                    Type = TokenType.Type
                },

                new Token
                {
                    Type = TokenType.Identifier
                }
            };
        }

        [Test]
        public void CorrectAmountOfItems()
        {
            Assert.AreEqual(this.stream.Count, 3);
            Assert.Pass();
        }

        [Test]
        public void Skip()
        {
            Assert.DoesNotThrow(() => this.stream.Skip(TokenType.Type));
            Assert.AreEqual(this.stream.Index, 1);
            Assert.AreEqual(this.stream.Get().Type, TokenType.Type);
            Assert.Pass();
        }

        [Test]
        public void Next()
        {
            // Skip the first item.
            this.stream.Skip();

            Assert.DoesNotThrow(() =>
            {
                // Skip the 2nd item (currently on the 3rd).
                Token token = this.stream.Next(TokenType.Identifier);

                // Verify against direct index.
                Assert.AreEqual(token, this.stream[2]);
            });

            Assert.AreEqual(this.stream.Index, 2);
            Assert.AreEqual(this.stream.Get().Type, TokenType.Identifier);
            Assert.Pass();
        }
    }
}
