using System;
using System.IO;
using System.Runtime.InteropServices;
using LLVMSharp;
using Ion.SyntaxAnalysis;
using NUnit.Framework;
using Ion.Abstraction;
using Ion.Tests.Core;
using Ion.CodeGeneration;
using Ion.Parsing;
using Ion.Core;

namespace Ion.Tests.CodeGeneration
{
    [TestFixture]
    internal sealed class CallTests
    {
        [SetUp]
        public static void Setup()
        {
            // Reset the name counter before every test.
            NameCounter.ResetAll();
        }

        [Test]
        public void CallWithoutArguments()
        {
            // Create the token stream.
            TokenStream stream = TestUtil.CreateStreamFromInputDataFile("CallWithoutArguments");

            // Read the expected output IR code.
            string expected = TestUtil.ReadOutputDataFile("CallWithoutArguments");

            // Create the driver.
            Driver driver = new Driver(stream);

            // Expect driver to have next.
            Assert.True(driver.HasNext);

            // Invoke the driver.
            driver.Next();

            // Expect driver to have next.
            Assert.True(driver.HasNext);

            // Invoke the driver once more.
            driver.Next();

            // Finally, expect the driver to not have next.
            Assert.False(driver.HasNext);

            // Emit the driver's module.
            string output = driver.Module.ToString();

            // Compare stored IR code with the actual, emitted output.
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void CallWithSingleArg()
        {
            // Create the token stream.
            TokenStream stream = TestUtil.CreateStreamFromInputDataFile("CallWithSingleArg");

            // Read the expected output IR code.
            string expected = TestUtil.ReadOutputDataFile("CallWithSingleArg");

            // Create the driver.
            Driver driver = new Driver(stream);

            // Expect driver to have next.
            Assert.True(driver.HasNext);

            // Invoke the driver to process the test function.
            driver.Next();

            // Expect driver to have next.
            Assert.True(driver.HasNext);

            // Invoke the driver once more to process the main function.
            driver.Next();

            // Finally, expect the driver to not have next.
            Assert.False(driver.HasNext);

            // Emit the driver's module.
            string output = driver.Module.ToString();

            // Compare results.
            Assert.AreEqual(expected, output);
        }
    }
}
