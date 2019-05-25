using System;
using System.IO;
using System.Runtime.InteropServices;
using LLVMSharp;
using Ion.SyntaxAnalysis;
using NUnit.Framework;
using Ion.Tests.Core;
using Ion.CodeGeneration;
using Ion.Parsing;
using Ion.Core;
using Ion.Tests.Wrappers;

namespace Ion.Tests.CodeGeneration
{
    [TestFixture]
    internal sealed class FunctionReturnTests : ConstructTest
    {
        [Test]
        public void MainWithReturn()
        {
            // Prepare the wrapper.
            this.Wrapper.Prepare("MainWithReturn");

            // Invoke the driver.
            this.Wrapper.InvokeDriver();

            // Compare results.
            this.Wrapper.Compare();
        }

        [Test]
        public void ReturnExpression()
        {
            // Prepare the wrapper.
            this.Wrapper.Prepare("ReturnExpression", "MainWithReturn");

            // Invoke the driver.
            this.Wrapper.InvokeDriver();

            // Compare results.
            this.Wrapper.Compare();
        }
    }
}
