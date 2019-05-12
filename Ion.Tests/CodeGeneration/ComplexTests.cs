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
    internal sealed class ComplexTests
    {
        [Test]
        public void Complex()
        {
            // Create the token stream.
            TokenStream stream = TestUtil.CreateStreamFromInputDataFile("Complex");

            // Read the expected output IR code.
            string expected = TestUtil.ReadOutputDataFile("Complex");

            // Create the driver.
            Driver driver = new Driver(stream);

            // Expect driver to have next.
            Assert.True(driver.HasNext);

            // Invoke the driver once more.
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
