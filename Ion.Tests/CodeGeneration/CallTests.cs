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
    internal sealed class CallTests : ConstructTest
    {
        [Test]
        public void CallWithoutArguments()
        {
            // Prepare the wrapper.
            this.Wrapper.Prepare("CallWithoutArguments");

            // Invoke the driver.
            this.Wrapper.InvokeDriver(2);

            // Compare results.
            this.Wrapper.Compare();
        }

        [Test]
        public void CallWithSingleArg()
        {
            // Prepare the wrapper.
            this.Wrapper.Prepare("CallWithSingleArg");

            // Invoke the driver.
            this.Wrapper.InvokeDriver(2);

            // Compare results.
            this.Wrapper.Compare();
        }
    }
}
