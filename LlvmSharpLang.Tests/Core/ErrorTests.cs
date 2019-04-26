using NUnit.Framework;
using LlvmSharpLang.Core;
using LlvmSharpLang.ErrorReporting;

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
