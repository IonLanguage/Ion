using NUnit.Framework;
using Ion.Tests.Wrappers;

namespace Ion.Tests.CodeGeneration
{
    [TestFixture]
    internal sealed class ArrayTests : ConstructTest
    {
        [Test]
        public void ArrayExpr()
        {
            // Prepare the wrapper.
            this.Wrapper.Prepare("Array");

            // Invoke the driver.
            this.Wrapper.InvokeDriver();

            // Compare results.
            this.Wrapper.Compare();
        }
    }
}
