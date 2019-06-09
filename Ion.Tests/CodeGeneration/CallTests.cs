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
    internal sealed class CallTests : ConstructTest
    {
        [Test]
        public void CallWithoutArguments()
        {
            this.Wrapper.Bootstrap("CallWithoutArguments", 2);
        }

        [Test]
        public void CallWithSingleArg()
        {
            this.Wrapper.Bootstrap("CallWithSingleArg", 2);
        }
    }
}
