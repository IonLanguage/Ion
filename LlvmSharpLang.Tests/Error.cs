using NUnit.Framework;
using LlvmSharpLang.Core;

namespace LlvmSharpLang.Tests
{
    public class ErrorTests
    {
        [Test]
        public void StaticCreateDefault()
        {
            Assert.AreEqual(Error.Create("Test"), "GenericError: Test");
            Assert.Pass();
        }
    }
}
