using NUnit.Framework;
using LlvmSharpLang.Core;
using LlvmSharpLang.Misc;

namespace LlvmSharpLang.Tests
{
    public class StreamTests
    {
        protected Stream<int> stream;

        [SetUp]
        public void Setup()
        {
            this.stream = new Stream<int>
            {
                1,
                2
            };
        }

        [Test]
        public void Get()
        {
            Assert.AreEqual(1, this.stream.Get());
            Assert.Pass();
        }

        [Test]
        public void IndexBeZero()
        {
            Assert.AreEqual(0, this.stream.Index);
            Assert.Pass();
        }

        [Test]
        public void Peek()
        {
            Assert.AreEqual(2, this.stream.Peek());
            Assert.Pass();
        }

        [Test]
        public void Next()
        {
            Assert.AreEqual(2, this.stream.Next());
            Assert.AreEqual(1, this.stream.Index);
            Assert.AreEqual(2, this.stream.Get());
            Assert.Pass();
        }
    }
}
