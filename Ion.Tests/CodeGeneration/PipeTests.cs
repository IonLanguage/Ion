using System;
using System.IO;
using System.Runtime.InteropServices;
using LLVMSharp;
using Ion.Syntax;
using NUnit.Framework;
using Ion.Tests.Core;
using Ion.CodeGeneration;
using Ion.Parsing;
using Ion.Core;

namespace Ion.Tests.CodeGeneration
{
    [TestFixture]
    internal sealed class PipeTests
    {
        [SetUp]
        public static void Setup()
        {
            // Reset the name counter before every test.
            GlobalNameRegister.Reset();
        }

        [Test]
        public void SingleArgPipe()
        {
            // Create the token stream.
            TokenStream stream = TestUtil.CreateStreamFromInputDataFile("SingleArgPipe");

            // Create the driver.
            Driver driver = new Driver(stream);

            // Expect driver to have next.
            Assert.True(driver.HasNext);

            // Invoke the driver to capture the first test function.
            driver.Next();

            // Expect driver to have next.
            Assert.True(driver.HasNext);

            // Invoke the driver to capture the main.
            driver.Next();

            // Expect driver to not have next.
            Assert.False(driver.HasNext);

            // Read the expected output.
            string expected = TestUtil.ReadOutputDataFile("SingleArgPipe");

            // Emit the driver's module.
            string output = driver.Module.Emit();

            // Assert results.
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void PipeColumn()
        {
            // Create the token stream.
            TokenStream stream = TestUtil.CreateStreamFromInputDataFile("PipeColumn");

            // Create the driver.
            Driver driver = new Driver(stream);

            // Expect driver to have next.
            Assert.True(driver.HasNext);

            // Invoke the driver to capture the test function.
            driver.Next();

            // Expect driver to have next.
            Assert.True(driver.HasNext);

            // Invoke the driver to capture the main function.
            driver.Next();

            // Expect driver to not have next.
            Assert.False(driver.HasNext);

            // Read the expected output.
            string expected = TestUtil.ReadOutputDataFile("PipeColumn");

            // Emit the driver's module.
            string output = driver.Module.Emit();

            // Assert results.
            Assert.AreEqual(expected, output);
        }
    }
}
