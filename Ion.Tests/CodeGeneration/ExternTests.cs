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
    internal sealed class ExternTests
    {
        [SetUp]
        public static void Setup()
        {
            // Reset the name counter before every test.
            NameCounter.ResetAll();
        }

        [Test]
        public void Extern()
        {
            // Create the token stream.
            TokenStream stream = TestUtil.CreateStreamFromInputDataFile("Extern");

            // Create the driver.
            Driver driver = new Driver(stream);

            // Expect the driver to have next.
            Assert.True(driver.HasNext);

            // Invoke the driver.
            driver.Next();

            // Expect the driver to not have next.
            Assert.False(driver.HasNext);

            // Read the expected output IR code.
            string expected = TestUtil.ReadOutputDataFile("Extern");

            // Emit the driver's module.
            string output = driver.Module.ToString();

            // Compare results.
            Assert.AreEqual(expected, output);
        }
    }
}
