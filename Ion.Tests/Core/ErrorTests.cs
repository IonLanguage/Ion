using NUnit.Framework;
using Ion.Core;
using Ion.ErrorReporting;

namespace Ion.Tests.Core
{
    public class ErrorTests
    {
        [Test]
        public void StaticCreateDefault()
        {
            Assert.AreEqual(Error.Create("Test"), "GenericError: Test");
        }
    }
}
