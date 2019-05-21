using System;
using System.IO;
using System.Runtime.InteropServices;
using LLVMSharp;
using Ion.SyntaxAnalysis;
using NUnit.Framework;
using Ion.Tests.Core;
using Ion.CodeGeneration;
using Ion.Parsing;
using Ion.Core;
using Ion.CodeGeneration.Helpers;
using Ion.Tests.Wrappers;

namespace Ion.Tests.CodeGeneration
{
    [TestFixture]
    internal sealed class FunctionTests : ConstructTest
    {
        [Test]
        public void FunctionWithArguments()
        {
            // Prepare the wrapper.
            this.Wrapper.Prepare("FunctionWithArguments");

            // Invoke the driver.
            this.Wrapper.InvokeDriver();

            // Compare results.
            this.Wrapper.Compare();
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
            string output = driver.Module.Emit();

            // Compare stored IR code with the actual, emitted output.
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void CreateMainFunction()
        {
            // Create a new module instance.
            Ion.CodeGeneration.Module module = new Ion.CodeGeneration.Module("Test");

            // Read the expected output IR code.
            string expected = TestUtil.ReadOutputDataFile("EmptyMainFunction");

            // Emit the main function.
            module.EmitMainFunction();

            // Emit the module.
            string output = module.Emit();

            // Compare stored IR code with the actual, emitted output.
            Assert.AreEqual(expected, output);
        }
    }
}
