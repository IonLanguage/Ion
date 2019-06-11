using Ion.Core;
using Ion.Tests.Core;
using NUnit.Framework;

namespace Ion.Tests.Wrappers
{
    [TestFixture]
    public class ConstructTest
    {
        public ConstructWrapper Wrapper { get; private set; }

        [SetUp]
        public void Setup()
        {
            // Reset the name counter before every test.
            GlobalNameRegister.Reset();

            // Prepare a new parser test wrapper instance.
            this.Wrapper = new ConstructWrapper();
        }
    }
}
