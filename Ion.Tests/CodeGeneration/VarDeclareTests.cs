using Ion.CodeGeneration;
using Ion.CodeGeneration.Structure;
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
        [Test]
        public void VarDeclare()
        {
            // Create the token stream.
            TokenStream stream = TestUtil.CreateStreamFromInputDataFile("VariableDeclaration");

            // Create a new driver instance from the token stream.
            Driver driver = new Driver(stream);

            // Ensure correct token stream length.
            Assert.AreEqual(2, stream.Count);

            // Create and emit the main function.
            Function mainFunction = driver.Module.EmitMainFunction();

            // Retrieve the main function.
            LLVMValueRef mainFunctionRef = driver.Module.SymbolTable.RetrieveFunctionOrThrow(mainFunction.Name);

            // Ensure main function reference is not null.
            Assert.That(mainFunctionRef, Is.Not.Null);

            // Invoke the variable declaration parser.
            VarDeclareExpr declaration = new VarDeclareExprParser().Parse(driver.ParserContext);

            // Create the LLVM builder reference.
            LLVMBuilderRef builder = mainFunction.Body.Current.CreateBuilder();

            // Derive the pipe context from the driver's module to emit the declaration.
            PipeContext<LLVMBuilderRef> context = driver.ModulePipeContext.Derive<LLVMBuilderRef>(builder);

            // Emit the declaration.
            declaration.Emit(context);

            // Emit the module and trim whitespace.
            string output = driver.Module.ToString().Trim();

            // Read data to be compared.
            string expected = TestUtil.ReadOutputDataFile("VariableDeclaration");

            // Compare results.
            Assert.AreEqual(expected, output);
        }
    }
}
