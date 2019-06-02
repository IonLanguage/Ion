using NUnit.Framework;
using Ion.Tests.Wrappers;

namespace Ion.Tests.CodeGeneration
{
    [TestFixture]
    internal sealed class LambdaTests : ConstructTest
    {
        [Test]
        public void Lambda()
        {
            this.Wrapper.Bootstrap("Lambda");
        }
    }
}
