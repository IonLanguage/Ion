using System;
using System.IO;
using System.Runtime.InteropServices;
using Ion.Syntax;
using NUnit.Framework;
using Ion.Tests.Core;
using Ion.Parsing;
using Ion.Generation;
using Ion.Core;
using Ion.Generation.Helpers;
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
