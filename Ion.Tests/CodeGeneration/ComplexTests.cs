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
    internal sealed class ComplexTests : ConstructTest
    {
        [Test]
        public void Complex()
        {
            this.Wrapper.Bootstrap("Complex");
        }
    }
}
