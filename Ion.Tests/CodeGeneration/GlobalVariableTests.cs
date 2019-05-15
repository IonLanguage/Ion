using System;
using System.IO;
using System.Runtime.InteropServices;
using LLVMSharp;
using Ion.SyntaxAnalysis;
using NUnit.Framework;
using Ion.Abstraction;
using Ion.Tests.Core;
using Ion.Parsing;
using Ion.CodeGeneration;
using Ion.Core;
using Ion.CodeGeneration.Structure;

namespace Ion.Tests.CodeGeneration
{
    [TestFixture]
    internal sealed class GlobalVariableTests
    {
        private Abstraction.Module module;

        private PipeContext<LLVMModuleRef> modulePipeContext;

        private TokenStream stream;

        [SetUp]
        public void Setup()
        {
            // Create a new LLVM module instance.
            this.module = new Ion.Abstraction.Module();

            // Create a pipe context for the module.
            this.modulePipeContext = PipeContextFactory.CreateFromModule(this.module);

            // Create the sequence.
            Token[] sequence = new Token[]
            {
                new Token {
                    Type = TokenType.Identifier,
                    Value = "bool"
                },

                new Token {
                    Type = TokenType.SymbolAt,
                    Value = "@"
                },

                new Token {
                    Type = TokenType.Identifier,
                    Value = "test"
                }
            };

            // Create the stream.
            this.stream = new TokenStream(sequence);

            // Reset the name counter before every test.
            NameCounter.ResetAll();
        }

        [Test]
        public void MatchStoredIr()
        {
            // Read the expected output.
            string expected = TestUtil.ReadOutputDataFile("GlobalVariable");

            // Create a driver instance.
            Driver driver = new Driver(this.stream);

            // Invoke the global variable parser.
            GlobalVar globalVariable = new GlobalVarParser().Parse(driver.ParserContext);

            // Emit the global variable.
            globalVariable.Emit(this.modulePipeContext);

            // Emit the module.
            string output = this.module.Emit();

            // Compare results.
            Assert.AreEqual(expected, output);
        }
    }
}
