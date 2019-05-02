using Ion.CodeGeneration;
using Ion.Core;
using Ion.Misc;
using Ion.Parsing;
using Ion.SyntaxAnalysis;
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
            TokenStream stream = new TokenStream(new Token[] {
                // Program starting point token.
                new Token {
                    Type = TokenType.Unknown
                },

                new Token {
                    Type = TokenType.Identifier,
                    Value = "int"
                },

                new Token {
                    Type = TokenType.Identifier,
                    Value = "myLocal"
                }
            });

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

            // Emit the module.
            string output = this.module.ToString();
        }
    }
}
