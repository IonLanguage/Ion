using System;
using System.IO;
using System.Runtime.InteropServices;
using LLVMSharp;
using Ion.SyntaxAnalysis;
using NUnit.Framework;
using Ion.Abstraction;
using Ion.Tests.Core;
using Ion.CodeGeneration;
using Ion.Parsing;
using Ion.Core;

namespace Ion.Tests.CodeGeneration
{
    [TestFixture]
    internal sealed class DriverTests
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
        public void CallWithoutArguments()
        {
            // Create the token stream.
            TokenStream stream = new TokenStream(new Token[]
            {
                // Program starting point token.
                new Token {
                    Type = TokenType.Unknown
                },

                new Token {
                    Type = TokenType.Identifier,
                    Value = "main"
                },

                new Token {
                    Type = TokenType.SymbolParenthesesL,
                    Value = "("
                },

                new Token {
                    Type = TokenType.SymbolParenthesesR,
                    Value = ")"
                },

                new Token {
                    Type = TokenType.SymbolSemiColon,
                    Value = ";"
                }
            });

            // Read the expected output IR code.
            string expected = TestUtil.ReadOutputDataFile("CallWithoutArguments");

            // Emit the main function.
            this.module.EmitMainFunction();

            // Emit the module.
            string output = this.module.ToString();

            // Compare stored IR code with the actual, emitted output.
            Assert.AreEqual(expected, output);
        }
    }
}
