using NUnit.Framework;
using Ion.Tests.Wrappers;

namespace Ion.Tests.CodeGeneration
{
    [TestFixture]
    internal sealed class StructTests : ConstructTest
    {
        [Test]
        public void Struct()
        {
            this.Wrapper.Bootstrap("Struct", 2);
        }

        [Test]
        public void StructMultipleProps()
        {
            this.Wrapper.Bootstrap("StructMultipleProps", 2);
        }

        [Test]
        public void StructPropAccess()
        {
            this.Wrapper.Bootstrap("StructPropAccess", 2);
        }
    }
}
