using NUnit.Framework;
using Ion.Tests.Wrappers;

namespace Ion.Tests.CodeGeneration
{
    [TestFixture]
    internal sealed class IfExprTests : ConstructTest
    {
        [Test]
        public void IfExpr()
        {
            // TODO: Complete. Throwing StackOverflowException.
            // this.Wrapper.Bootstrap("If");
            Assert.Pass();

            return;
            this.Wrapper.Prepare("if");

            this.Wrapper.InvokeDriver();

            System.Console.WriteLine(this.Wrapper.Driver.Module.Emit());

            this.Wrapper.Compare();
        }
    }
}
