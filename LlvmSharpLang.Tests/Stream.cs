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
                2,
                3
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

        // [Test]
        // public void Peek()
        // {
        //     Assert.AreEqual(this.stream.Peek(), 2);
        //     Assert.Pass();
        // }
    }
}
