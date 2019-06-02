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
            this.Wrapper.Prepare("Lambda");
            this.Wrapper.InvokeDriver();
            
            System.Console.WriteLine(this.Wrapper.Driver.Module.Emit());
            this.Wrapper.Bootstrap("Lambda");
        }
    }
}
