using Ion.Syntax;
using NUnit.Framework;

namespace Ion.Tests.Syntax
{
    public class TokenStreamTests
    {
        protected TokenStream stream;

        [SetUp]
        public void Setup()
        {
            this.stream = new TokenStream
            {
                new Token(TokenType.Unknown, "", 0),
                new Token(TokenType.TypeVoid, "", 0),
                new Token(TokenType.Identifier, "", 0)
            };
        }

        [Test]
        public void CorrectAmountOfItems()
        {
            Assert.AreEqual(this.stream.Count, 3);
        }

        [Test]
        public void Skip()
        {
            Assert.DoesNotThrow(() => this.stream.Skip(TokenType.TypeVoid));
            Assert.AreEqual(this.stream.Index, 1);
            Assert.AreEqual(this.stream.Get().Type, TokenType.TypeVoid);
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
        }
    }
}
