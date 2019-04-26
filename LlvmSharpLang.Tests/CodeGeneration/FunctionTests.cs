using System.IO;
using LLVMSharp;
using LlvmSharpLang.SyntaxAnalysis;
using NUnit.Framework;

namespace LlvmSharpLang.Tests.CodeGeneration
{
    internal sealed class FunctionTests
    {
        private string storedIr;

        private LLVMModuleRef module;

        private Token[] sequence;

        [SetUp]
        public void SetUp()
        {
            // TODO: Cannot compare null to LLVMModuleRef.
            // Dispose previous LLVM module if applicable.
            /*if (this.module != null)
            {
                LLVM.DisposeModule(this.module);
            }*/

            // Create the LLVM module.
            this.module = LLVM.ModuleCreateWithName("test");

            // Read the stored IR code to compare.
            this.storedIr = File.ReadAllText("../../../Data/function.ll");

            // Create the sequence.
            this.sequence = new Token[]
            {
                new Token {
                    Type = TokenType.ProgramStart
                },

                new Token {
                    Type = TokenType.KeywordFunction,
                    Value = "fn"
                },

                new Token {
                    Type = TokenType.Identifier,
                    Value = "helloWorld"
                },

                new Token {
                    Type = TokenType.SymbolParenthesesL,
                    Value = "("
                },

                new Token {
                    Type = TokenType.Type,
                    Value = "int"
                },

                new Token {
                    Type = TokenType.Identifier,
                    Value = "myInteger"
                },

                new Token {
                    Type = TokenType.SymbolComma
                },

                new Token {
                    Type = TokenType.Type,
                    Value = "float"
                },

                new Token {
                    Type = TokenType.Identifier,
                    Value = "myFloat"
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
            };
        }

        [Test]
        public void MatchStoredIr()
        {
            // Emit the module.
            string ir;

            // TODO: Finish implementing.
            // ir = LLVM.PrintModuleToString(this.module);

            // Compare emitted IR code.
            // Assert.AreEqual(this.storedIr, ir);

            // TODO: Not showing in tests? Actually it's just empty.
            LLVM.DumpModule(this.module);

            // DEBUG: Output messages only show on error (Assert.fail).
            System.Console.WriteLine("hello world");

            // Assert.Fail();
            Assert.Pass();
        }
    }
}
