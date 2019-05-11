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
    internal sealed class ExprTests
    {
        [Test]
        public void Expr()
        {
            // Create the token stream.
            TokenStream stream = TestUtil.CreateStreamFromInputDataFile("Expr");

            // Create the driver.
            Driver driver = new Driver(stream);

            // Expect driver to have next.
            Assert.True(driver.HasNext);

            // Invoke the driver.
            driver.Next();

            // Expect driver to not have next.
            Assert.False(driver.HasNext);

            // Read the expected output.
            string expected = TestUtil.ReadOutputDataFile("Expr");

            // Emit the driver's module.
            string output = driver.Module.ToString();

            // Assert results.
            Assert.AreEqual(expected, output);
        }
    }
}
