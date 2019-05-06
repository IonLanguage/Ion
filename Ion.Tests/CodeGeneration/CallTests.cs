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
        private Abstraction.Module module;

        [SetUp]
        public void Setup()
        {
            // Create a new LLVM module instance.
            this.module = new Ion.Abstraction.Module();

            // Reset symbol table completely.
            SymbolTable.HardReset();
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
    }
}
