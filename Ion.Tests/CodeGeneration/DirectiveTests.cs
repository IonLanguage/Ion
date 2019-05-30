using NUnit.Framework;
using Ion.Tests.Wrappers;

namespace Ion.Tests.CodeGeneration
{
    [TestFixture]
    internal sealed class DirectiveTests : ConstructTest
    {
        [Test]
        public void Directive()
        {
            // Prepare the wrapper.
            this.Wrapper.Prepare("Directive", "EmptyMainFunction");

            // Invoke the driver.
            this.Wrapper.InvokeDriver(2);

            // Compare results.
            this.Wrapper.Compare();
        }
    }
}
