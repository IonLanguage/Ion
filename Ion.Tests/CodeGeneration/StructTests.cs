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
            // Prepare the wrapper.
            this.Wrapper.Prepare("Struct");

            // Invoke the driver.
            this.Wrapper.InvokeDriver(2);

            System.Console.WriteLine(this.Wrapper.Driver.Module.Emit());

            // Compare results.
            this.Wrapper.Compare();
        }
    }
}
