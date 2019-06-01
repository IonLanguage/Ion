using NUnit.Framework;
using Ion.Tests.Wrappers;

namespace Ion.Tests.CodeGeneration
{
    [TestFixture]
    public class AttributeTests : ConstructTest
    {
        [Test]
        public void Attribute()
        {
            // Prepare the wrapper.
            this.Wrapper.Prepare("Attribute", "EmptyMainFunction");

            // Invoke the driver.
            this.Wrapper.InvokeDriver();

            // Compare results.
            this.Wrapper.Compare();
        }
    }
}
