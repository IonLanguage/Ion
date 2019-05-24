using Ion.CodeGeneration;
using Ion.CodeGeneration.Helpers;
using Ion.Core;
using Ion.Misc;
using Ion.Parsing;
using Ion.SyntaxAnalysis;
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
            // Prepare the wrapper.
            this.Wrapper.Prepare("VariableDeclaration");

            // Invoke the driver.
            this.Wrapper.InvokeDriver();

            System.Console.WriteLine(this.Wrapper.Driver.Module.Emit());

            // Compare results.
            this.Wrapper.Compare();
        }
    }
}
