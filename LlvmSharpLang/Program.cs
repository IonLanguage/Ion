using System;
using System.Runtime.InteropServices;
using LLVMSharp;
using LlvmSharpLang.CodeGeneration;
using LlvmSharpLang.CodeGeneration.Structure;
using LlvmSharpLang.Misc;
using LlvmSharpLang.Parsing;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang
{
    internal sealed class Program
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int Add(int a, int b);

        public static void Main(string[] args)
        {
            LLVMBool successFlag = new LLVMBool(0);

            // Create the module.
            LLVMModuleRef module = LLVM.ModuleCreateWithName(SpecialName.Entry);

            LLVMTypeRef[] paramTypes = {
                LLVM.Int32Type(),
                LLVM.Int32Type()
            };

            LLVMTypeRef retType = LLVM.FunctionType(LLVM.Int32Type(), paramTypes, false);
            LLVMValueRef sum = LLVM.AddFunction(module, "sum", retType);

            LLVMBasicBlockRef entry = LLVM.AppendBasicBlock(sum, "entry");

            LLVMBuilderRef builder = LLVM.CreateBuilder();

            LLVM.PositionBuilderAtEnd(builder, entry);

            LLVMValueRef tmp = LLVM.BuildAdd(builder, LLVM.GetParam(sum, 0), LLVM.GetParam(sum, 1), "tmp");

            LLVM.BuildRet(builder, tmp);

            // Verify the module.
            if (LLVM.VerifyModule(module, LLVMVerifierFailureAction.LLVMPrintMessageAction, out var error) != successFlag)
            {
                Console.WriteLine($"Error: {error}");
            }

            LLVM.LinkInMCJIT();

            // Initialize targets.
            LLVM.InitializeX86TargetMC();
            LLVM.InitializeX86Target();
            LLVM.InitializeX86TargetInfo();
            LLVM.InitializeX86AsmParser();
            LLVM.InitializeX86AsmPrinter();

            LLVMMCJITCompilerOptions options = new LLVMMCJITCompilerOptions
            {
                NoFramePointerElim = 1
            };

            LLVM.InitializeMCJITCompilerOptions(options);

            if (LLVM.CreateMCJITCompilerForModule(out var engine, module, options, out error) != successFlag)
            {
                Console.WriteLine($"Error: {error}");
            }

            var addMethod = (Add)Marshal.GetDelegateForFunctionPointer(LLVM.GetPointerToGlobal(engine, sum), typeof(Add));
            int result = addMethod(10, 10);

            Console.WriteLine("Result of sum is: " + result);

            // --- Tests start ---
            var functionStream = new TokenStream(new Token[] {
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
                    Value = "testArg1"
                },

                new Token {
                    Type = TokenType.SymbolComma,
                    Value = null
                },

                new Token {
                    Type = TokenType.Type,
                    Value = "float"
                },

                new Token {
                    Type = TokenType.Identifier,
                    Value = "testArg2"
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
                },
            });

            var globalVarStream = new TokenStream(new Token[] {
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
                    Value = "myGlobal"
                },
            });

            var declareStream = new TokenStream(new Token[] {
                new Token {
                    Type = TokenType.Identifier,
                    Value = "int"
                },

                new Token {
                    Type = TokenType.Identifier,
                    Value = "myLocal"
                },
            });

            // Create and emit the function stream.
            new FunctionParser().Parse(functionStream).Emit(module);
            // --- Tests end ---

            // Print output IR.
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            LLVM.DumpModule(module);

            // Dispose resources.
            LLVM.DisposeBuilder(builder);
            LLVM.DisposeExecutionEngine(engine);

            // Reset the foreground color after printing output IR.
            Console.ResetColor();
        }
    }
}
