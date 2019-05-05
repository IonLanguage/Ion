using NUnit.Framework;
using Ion.Core;
using Ion.ErrorReporting;
using Ion.CognitiveServices;
using Ion.SyntaxAnalysis;

namespace Ion.Tests.Core
{
    public class TokenIdentifierTests
    {
        [Test]
        [TestCase(TokenType.TypeBool)]
        [TestCase(TokenType.TypeChar)]
        [TestCase(TokenType.TypeDouble)]
        [TestCase(TokenType.TypeFloat)]
        [TestCase(TokenType.TypeInt)]
        [TestCase(TokenType.TypeLong)]
        [TestCase(TokenType.TypeString)]
        [TestCase(TokenType.TypeVoid)]
        public void IsType(TokenType input)
        {
            Assert.True(TokenIdentifier.IsType(input));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(TokenType.Unknown)]
        [TestCase(TokenType.Whitespace)]
        [TestCase(TokenType.Identifier)]
        public void IsNotType(TokenType input)
        {
            Assert.False(TokenIdentifier.IsType(input));
        }
    }
}
