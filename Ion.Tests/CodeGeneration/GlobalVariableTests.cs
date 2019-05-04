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

namespace Ion.Tests.CodeGeneration
{
    [TestFixture]
    internal sealed class GlobalVariableTests
    {
        private Abstraction.Module module;

        private TokenStream stream;

        [SetUp]
        public void Setup()
        {
            // Create a new LLVM module instance.
            this.module = new Ion.Abstraction.Module();

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
        }

        [Test]
        public void MatchStoredIr()
        {
            string expected = TestUtil.ReadOutputDataFile("GlobalVariable");

            // Invoke the global variable parser.
            GlobalVar globalVariable = new GlobalVarParser().Parse(stream);

            // Emit the global variable.
            globalVariable.Emit(this.module.Source);

            // Emit the module.
            string output = this.module.ToString();

            // Compare stored IR code with the actual, emitted output.
            Assert.AreEqual(expected, output);
        }
    }
}
