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
using Ion.Tests.Wrappers;

namespace Ion.Tests.CodeGeneration
{
    [TestFixture]
    internal sealed class FunctionReturnTests : ConstructTest
    {
        [Test]
        public void MainWithReturn()
        {
            this.Wrapper.Bootstrap("MainWithReturn");
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
