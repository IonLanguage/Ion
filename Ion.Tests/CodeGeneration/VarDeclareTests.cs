using Ion.Generation;
using Ion.Generation.Helpers;
using Ion.Core;
using Ion.Misc;
using Ion.Parsing;
using Ion.Syntax;
using Ion.Tests.Core;
using Ion.Tests.Wrappers;
using LLVMSharp;
using NUnit.Framework;

namespace Ion.Tests.CodeGeneration
{
    [TestFixture]
    internal sealed class VarDeclareTests : ConstructTest
    {
        [Test]
        public void VarDeclare()
        {
            this.Wrapper.Bootstrap("VariableDeclaration");
        }
    }
}
