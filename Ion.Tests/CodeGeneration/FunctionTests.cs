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
        private Abstraction.Module module = new Ion.Abstraction.Module();

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
        public void FunctionWithArguments()
        {
            // Create the token stream.
            TokenStream stream = new TokenStream(new Token[]
            {
                // Program starting point token.
                new Token {
                    Type = TokenType.Unknown
                },

                new Token {
                    Type = TokenType.KeywordFunction,
                    Value = "fn"
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
                    Type = TokenType.SymbolColon,
                    Value = ":"
                },

                new Token {
                    Type = TokenType.Identifier,
                    Value = "int"
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
            string expected = File.ReadAllText(TestUtil.ResolveDataPath("FunctionWithArguments.ll"));

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
                // Program starting point token.
                new Token {
                    Type = TokenType.Unknown
                },

                new Token {
                    Type = TokenType.KeywordFunction,
                    Value = "fn"
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
                    Type = TokenType.SymbolColon,
                    Value = ":"
                },

                new Token {
                    Type = TokenType.Identifier,
                    Value = "int"
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
            string expected = File.ReadAllText(TestUtil.ResolveDataPath("FunctionWithoutArguments.ll"));

            // Invoke the function parser.
            Function function = new FunctionParser().Parse(stream);

            // Emit the function.
            function.Emit(this.module.Source);

            // Emit the module.
            string output = this.module.ToString();

            System.Console.WriteLine(output);

            // Compare stored IR code with the actual, emitted output.
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void CreateMainFunction()
        {
            // Read the expected output IR code.
            string expected = File.ReadAllText(TestUtil.ResolveDataPath("CreateMainFunction.ll"));

            // Create the main function to emit.
            this.module.CreateMainFunction();

            // Emit the module.
            string output = this.module.ToString();

            // Compare stored IR code with the actual, emitted output.
            Assert.AreEqual(expected, output);
        }
    }
}
