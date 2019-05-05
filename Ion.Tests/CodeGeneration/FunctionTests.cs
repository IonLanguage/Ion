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
    internal sealed class FunctionTests
    {
        private Abstraction.Module module;

        [SetUp]
        public void Setup()
        {
            // Create a new LLVM module instance.
            this.module = new Ion.Abstraction.Module();

            // Reset symbol table completely.
            SymbolTable.HardReset();
        }

        [Test]
        public void FunctionWithArguments()
        {
            // Create the token stream.
            TokenStream stream = new TokenStream(new Token[]
            {
                new Token {
                    Type = TokenType.TypeInt,
                    Value = "int"
                },

                new Token {
                    Type = TokenType.Identifier,
                    Value = "test"
                },

                new Token {
                    Type = TokenType.SymbolParenthesesL,
                    Value = "("
                },

                new Token {
                    Type = TokenType.TypeInt,
                    Value = "int"
                },

                new Token {
                    Type = TokenType.Identifier,
                    Value = "firstParameter"
                },

                new Token {
                    Type = TokenType.SymbolComma
                },

                new Token {
                    Type = TokenType.TypeFloat,
                    Value = "float"
                },

                new Token {
                    Type = TokenType.Identifier,
                    Value = "secondParameter"
                },

                new Token {
                    Type = TokenType.SymbolParenthesesR,
                    Value = ")"
                },

                new Token {
                    Type = TokenType.SymbolBlockL,
                    Value = "{"
                },

                new Token {
                    Type = TokenType.SymbolBlockR,
                    Value = "}"
                }
            });

            // Read the expected output IR code.
            string expected = TestUtil.ReadOutputDataFile("FunctionWithArguments");

            // Invoke the function parser.
            Function function = new FunctionParser().Parse(stream);

            // Emit the function.
            function.Emit(this.module.Source);

            // Emit the module.
            string output = this.module.ToString();

            // Compare stored IR code with the actual, emitted output.
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void FunctionWithoutArguments()
        {
            // Create the token stream.
            TokenStream stream = new TokenStream(new Token[]
            {
                new Token {
                    Type = TokenType.TypeInt,
                    Value = "int"
                },

                new Token {
                    Type = TokenType.Identifier,
                    Value = "test"
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
                    Type = TokenType.SymbolBlockL,
                    Value = "{"
                },

                new Token {
                    Type = TokenType.SymbolBlockR,
                    Value = "}"
                }
            });

            // Read the expected output IR code.
            string expected = TestUtil.ReadOutputDataFile("FunctionWithoutArguments");

            // Invoke the function parser.
            Function function = new FunctionParser().Parse(stream);

            // Emit the function.
            function.Emit(this.module.Source);

            // Emit the module.
            string output = this.module.ToString();

            // Compare stored IR code with the actual, emitted output.
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void CreateMainFunction()
        {
            // Read the expected output IR code.
            string expected = TestUtil.ReadOutputDataFile("EmptyMainFunction");

            // Emit the main function.
            this.module.EmitMainFunction();

            // Emit the module.
            string output = this.module.ToString();

            // Compare stored IR code with the actual, emitted output.
            Assert.AreEqual(expected, output);
        }
    }
}
