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
    internal sealed class FunctionReturn
    {
        [SetUp]
        public static void Setup()
        {
            // Reset the name counter before every test.
            NameCounter.ResetAll();
        }

        [Test]
        public void MainWithReturn()
        {
            // Create the token stream.
            TokenStream stream = TestUtil.CreateStreamFromInputDataFile("MainWithReturn");

            // Create the driver.
            Driver driver = new Driver(stream);

            // Expect the driver to have next.
            Assert.True(driver.HasNext);

            // Invoke the driver.
            driver.Next();

            // Expect the driver to not have next.
            Assert.False(driver.HasNext);

            // Read expected output.
            string expected = TestUtil.ReadOutputDataFile("MainWithReturn");

            // Emit the driver's module.
            string output = driver.Module.ToString();

            // Compare results.
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void ReturnExpression()
        {
            // Create the token stream.
            TokenStream stream = TestUtil.CreateStreamFromInputDataFile("ReturnExpression");

            // Create the driver.
            Driver driver = new Driver(stream);

            // Expect the driver to have next.
            Assert.True(driver.HasNext);

            // Invoke the driver.
            driver.Next();

            // Expect the driver to not have next.
            Assert.False(driver.HasNext);

            // Read expected output.
            string expected = TestUtil.ReadOutputDataFile("MainWithReturn");

            // Emit the driver's module.
            string output = driver.Module.ToString();

            // Compare results.
            Assert.AreEqual(expected, output);
        }
    }
}
