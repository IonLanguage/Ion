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
            this.Wrapper.Bootstrap("Attribute", 2);
        }
    }
}
