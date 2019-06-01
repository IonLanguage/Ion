using System;
using Ion.Tests.Wrappers;
using NUnit.Framework;

namespace Ion.Tests.CodeGeneration
{
    [TestFixture]
    public class ArgTests : ConstructTest
    {
        [Test]
        public void ArgumentMismatch()
        {
            // Prepare the wrapper.
            this.Wrapper.Prepare("ArgCountMismatch");

            // Invoke the driver and expect it to throw.
            Assert.Throws<Exception>(() => this.Wrapper.InvokeDriver(2));
        }
    }
}
