using NUnit.Framework;
using Ion.Tests.Core;
using Ion.Generation;
using Ion.Tests.Wrappers;

namespace Ion.Tests.CodeGeneration
{
    [TestFixture]
    internal sealed class FunctionTests : ConstructTest
    {
        [Test]
        public void FunctionWithArguments()
        {
            this.Wrapper.Bootstrap("FunctionWithArguments");
        }

        [Test]
        public void FunctionWithoutArguments()
        {
            this.Wrapper.Bootstrap("FunctionWithoutArguments");
        }

        [Test]
        public void CreateMainFunction()
        {
            // Create a new module instance.
            Module module = new Module("Test");

            // Read the expected output IR code.
            string expected = TestUtil.ReadOutputDataFile("EmptyMainFunction");

            // Emit the main function.
            module.EmitMainFunction();

            // Emit the module.
            string output = module.Emit();

            // Compare stored IR code with the actual, emitted output.
            Assert.AreEqual(expected, output);
        }
    }
}
