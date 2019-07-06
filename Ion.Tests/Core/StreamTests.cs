using Ion.Engine.Syntax;
using NUnit.Framework;

namespace Ion.Tests.Core
{
    [TestFixture]
    internal sealed class StreamTests
    {
        private Stream<int> stream;

        [SetUp]
        public void Setup()
        {
            this.stream = new Stream<int>
            {
                1,
                2,
                3
            };
        }

        [Test]
        public void Get()
        {
            Assert.AreEqual(1, this.stream.Get());
        }

        [Test]
        public void IndexBeZero()
        {
            Assert.AreEqual(0, this.stream.Index);
        }

        [Test]
        public void DoesIndexOverflow()
        {
            Assert.True(this.stream.DoesIndexOverflow(-1));
            Assert.True(this.stream.DoesIndexOverflow(3));
            Assert.False(this.stream.DoesIndexOverflow(2));
            Assert.False(this.stream.DoesIndexOverflow(1));
            Assert.False(this.stream.DoesIndexOverflow(0));
        }

        [Test]
        public void Peek()
        {
            Assert.AreEqual(2, this.stream.Peek());
        }

        [Test]
        public void PeekWithAmount()
        {
            Assert.AreEqual(2, this.stream.Peek());
            Assert.AreEqual(2, this.stream.Peek(1));
            Assert.AreEqual(3, this.stream.Peek(2));
        }

        [Test]
        public void Next()
        {
            Assert.AreEqual(2, this.stream.Next());
            Assert.AreEqual(1, this.stream.Index);
            Assert.AreEqual(2, this.stream.Get());
        }
    }
}
