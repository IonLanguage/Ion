using System;
using System.IO;
using System.Runtime.InteropServices;
using Ion.SyntaxAnalysis;
using NUnit.Framework;
using Ion.Tests.Core;
using Ion.CodeGeneration;
using Ion.Parsing;
using Ion.Core;
using Ion.CodeGeneration.Helpers;
using Ion.Tests.Wrappers;

namespace Ion.Tests.CodeGeneration
{
    [TestFixture]
    internal sealed class FunctionTests : ConstructTest
    {
        [Test]
        public void FunctionWithArguments()
        {
            // Prepare the wrapper.
            this.Wrapper.Prepare("FunctionWithArguments");

            // Invoke the driver.
            this.Wrapper.InvokeDriver();

            // Compare results.
            this.Wrapper.Compare();
        }

        [Test]
        public void FunctionWithoutArguments()
        {
            // Prepare the wrapper.
            this.Wrapper.Prepare("FunctionWithoutArguments");

            // Invoke the driver.
            this.Wrapper.InvokeDriver();

            System.Console.WriteLine(this.Wrapper.Driver.Module.Emit());

            // Compare results.
            this.Wrapper.Compare();
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
