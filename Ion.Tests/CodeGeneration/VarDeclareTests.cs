using Ion.CodeGeneration;
using Ion.Core;
using Ion.Misc;
using Ion.Parsing;
using Ion.SyntaxAnalysis;
using Ion.Tests.Core;
using LLVMSharp;
using NUnit.Framework;

namespace Ion.Tests.CodeGeneration
{
    [TestFixture]
    internal sealed class VarDeclareTests
    {
        private Abstraction.Module module;

        [SetUp]
        public void Setup()
        {
            // Create a new LLVM module instance.
            this.module = new Ion.Abstraction.Module();

            // Reset symbol table along with its functions.
            SymbolTable.Reset();
            SymbolTable.functions.Clear();
        }

        [Test]
        public void VarDeclare()
        {
            // Create the token stream.
            TokenStream stream = TestUtil.CreateStreamFromInputDataFile("VariableDeclaration");

            // Ensure correct token stream length.
            Assert.AreEqual(2, stream.Count);

            // Insert stream bounds.
            stream.InsertBounds();

            // Create and emit the main function.
            Function mainFunction = this.module.EmitMainFunction();

            // Retrieve the main function.
            LLVMValueRef mainFunctionRef = mainFunction.Retrieve();

            // Ensure main function reference is not null.
            Assert.That(mainFunctionRef, Is.Not.Null);

            // Invoke the variable declaration parser.
            VarDeclareExpr declaration = new VarDeclareExprParser().Parse(stream);

            // Emit the declaration.
            declaration.Emit(mainFunction.Body.Current.CreateBuilder());

            // Emit the module and trim whitespace.
            string output = this.module.ToString().Trim();

            // Read data to be compared.
            string expected = TestUtil.ReadOutputDataFile("VariableDeclaration");

            // Compare results.
            Assert.AreEqual(expected, output);
        }
    }
}
