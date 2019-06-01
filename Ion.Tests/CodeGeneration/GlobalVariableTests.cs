using System;
using System.IO;
using System.Runtime.InteropServices;
using Ion.SyntaxAnalysis;
using NUnit.Framework;
using Ion.Tests.Core;
using Ion.Parsing;
using Ion.CodeGeneration;
using Ion.Core;
using Ion.CodeGeneration.Helpers;
using Ion.Tests.Wrappers;

namespace Ion.Tests.CodeGeneration
{
    [TestFixture]
    internal sealed class GlobalVariableTests : ConstructTest
    {
        [Test]
        public void GlobalVariable()
        {
            this.Wrapper.Bootstrap("GlobalVariable");
        }
    }
}
