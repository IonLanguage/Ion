using NUnit.Framework;
using Ion.Core;
using Ion.NoticeReporting;
using Ion.CognitiveServices;
using Ion.Syntax;

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
        public void IsPrimitiveType(TokenType input)
        {
            Assert.True(TokenIdentifier.IsPrimitiveType(input));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(TokenType.Unknown)]
        [TestCase(TokenType.Whitespace)]
        [TestCase(TokenType.Identifier)]
        public void IsNotPrimitiveType(TokenType input)
        {
            Assert.False(TokenIdentifier.IsPrimitiveType(input));
        }

        [Test]
        [TestCase(TokenType.OperatorAddition)]
        [TestCase(TokenType.OperatorAddressOf)]
        [TestCase(TokenType.OperatorAnd)]
        [TestCase(TokenType.OperatorAssignment)]
        [TestCase(TokenType.OperatorDivision)]
        [TestCase(TokenType.OperatorEquality)]
        [TestCase(TokenType.OperatorEscape)]
        [TestCase(TokenType.OperatorExponent)]
        [TestCase(TokenType.OperatorGreaterThan)]
        [TestCase(TokenType.OperatorLessThan)]
        [TestCase(TokenType.OperatorModulo)]
        [TestCase(TokenType.OperatorMultiplication)]
        [TestCase(TokenType.OperatorNot)]
        [TestCase(TokenType.OperatorOr)]
        [TestCase(TokenType.OperatorPipe)]
        [TestCase(TokenType.OperatorSubtraction)]
        public void IsOperator(TokenType input)
        {
            Assert.True(TokenIdentifier.IsOperator(input));
        }
    }
}
