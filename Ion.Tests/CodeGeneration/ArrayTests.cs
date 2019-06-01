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
            this.Wrapper.Bootstrap("Array");
        }
    }
}
