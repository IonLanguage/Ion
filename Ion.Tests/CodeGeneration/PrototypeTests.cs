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
    internal sealed class PrototypeTests
    {
        private Abstraction.Module module;

        [SetUp]
        public void Setup()
        {
            // Reset symbol table completely.
            SymbolTable.HardReset();
        }

        [Test]
        public void Complex()
        {
            // Create the token stream.
            TokenStream stream = TestUtil.CreateStreamFromInputDataFile("ComplexPrototype");

            // Create the driver.
            Driver driver = new Driver(stream);

            // Expect driver to have next.
            Assert.True(driver.HasNext);

            // Invoke the driver.
            driver.Next();

            // Expect driver to not have next.
            Assert.False(driver.HasNext);

            // Read the expected output IR code.
            string expected = TestUtil.ReadOutputDataFile("ComplexPrototype");

            // Emit the driver's module.
            string output = driver.Module.ToString();

            // Compare stored IR code with the actual, emitted output.
            Assert.AreEqual(expected, output);
        }
    }
}
