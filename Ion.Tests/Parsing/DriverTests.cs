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
    internal sealed class DriverTests
    {

        [Test]
        public void HasNext()
        {
            // Create the token stream.
            TokenStream stream = TestUtil.CreateStreamFromInputDataFile("Driver");

            // Create the driver.
            Driver driver = new Driver(stream);

            // Expect driver to have next.
            Assert.True(driver.HasNext);

            // Invoke the driver.
            driver.Next();

            // Expect driver to not have next.
            Assert.False(driver.HasNext);
        }

        [Test]
        public void Next()
        {
            // Create the token stream.
            TokenStream stream = TestUtil.CreateStreamFromInputDataFile("Driver");

            // Create the driver.
            Driver driver = new Driver(stream);

            // Expect driver to have next.
            Assert.True(driver.HasNext);

            // Invoke the driver.
            bool processed = driver.Next();

            // Expect driver to have processed.
            Assert.True(processed);

            // Expect driver to not have next.
            Assert.False(driver.HasNext);

            // Load expected output.
            string expected = TestUtil.ReadOutputDataFile("EmptyMainFunction");

            // Emit the driver's module.
            string output = driver.Module.Emit();

            // Assert output module IR code.
            Assert.AreEqual(expected, output);
        }
    }
}
