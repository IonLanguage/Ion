using System;
using System.IO;
using System.Runtime.InteropServices;
using LLVMSharp;
using Ion.Syntax;
using NUnit.Framework;
using Ion.Tests.Core;
using Ion.Generation;
using Ion.Parsing;
using Ion.Core;
using Ion.Tests.Wrappers;

namespace Ion.Tests.CodeGeneration
{
    [TestFixture]
    internal sealed class CommentTests : ConstructTest
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
