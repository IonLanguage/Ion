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
using Ion.CodeGeneration.Structure;

namespace Ion.Tests.CodeGeneration
{
    [TestFixture]
    internal sealed class FunctionTests
    {
        private Abstraction.Module module;

        private PipeContext<LLVMModuleRef> modulePipeContext;

        [SetUp]
        public void Setup()
        {
            // Create a new LLVM module instance.
            this.module = new Ion.Abstraction.Module();

            // Create a pipe context for the module.
            this.modulePipeContext = PipeContextFactory.CreateFromModule(this.module);
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

            // Create a new driver instance.
            Driver driver = new Driver(stream);

            // Invoke the function parser.
            Function function = new FunctionParser().Parse(driver.ParserContext);

            // Emit the function.
            function.Emit(driver.ModulePipeContext);

            // Emit the module.
            string output = this.module.ToString();

            // Compare results.
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

            // Create a new driver instance.
            Driver driver = new Driver(stream);

            // Invoke the function parser.
            Function function = new FunctionParser().Parse(driver.ParserContext);

            // Emit the function.
            function.Emit(driver.ModulePipeContext);

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
