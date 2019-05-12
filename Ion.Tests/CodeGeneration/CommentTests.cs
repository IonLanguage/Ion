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
using Ion.Tests.Wrappers;

namespace Ion.Tests.CodeGeneration
{
    [TestFixture]
    internal sealed class CommentTests : EntityTest
    {
        [Test]
        public void Comments()
        {
            // Prepare the wrapper.
            this.Wrapper.Prepare("Comments", "EmptyMainFunction");

            // Invoke the driver.
            this.Wrapper.InvokeDriver();

            // Compare results.
            this.Wrapper.Compare();
        }
    }
}
