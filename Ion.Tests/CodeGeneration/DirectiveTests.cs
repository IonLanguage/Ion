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

            // Expect correct directive key and value.
            Assert.AreEqual(this.Wrapper.Driver.Module.SymbolTable.directives["key"], "value");

            // Compare results.
            this.Wrapper.Compare();
        }
    }
}
