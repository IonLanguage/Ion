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
    internal sealed class PrototypeTests : ConstructTest
    {
        [Test]
        public void Complex()
        {
            this.Wrapper.Bootstrap("ComplexPrototype");
        }
    }
}
