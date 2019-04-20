using NUnit.Framework;
using LlvmSharpLang.Core;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            //
        }

        [Test]
        public void StaticCreateDefault()
        {
            Assert.AreEqual(Error.Create("Test"), "GenericError: Test");
            Assert.Pass();
        }
    }
}
