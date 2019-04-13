using System;
using System.Runtime.InteropServices;
using LLVMSharp;
using LlvmSharpLang.CodeGen;
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
            LLVMModuleRef module = LLVM.ModuleCreateWithName(SpecialName.entry);

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
            var stream = new TokenStream(new Token[] {
                new Token() {
                    Type = TokenType.KeywordFn,
                    Value = "fn"
                },

                new Token() {
                    Type = TokenType.KeywordFn,
                    Value = "helloWorld"
                },

                new Token() {
                    Type = TokenType.SymbolParenthesesL,
                    Value = "("
                },

                new Token() {
                    Type = TokenType.SymbolParenthesesR,
                    Value = ")"
                },

                new Token() {
                    Type = TokenType.SymbolColon,
                    Value = ":"
                },

                new Token() {
                    Type = TokenType.Id,
                    Value = "int"
                },

                new Token() {
                    Type = TokenType.SymbolBlockL,
                    Value = "{"
                },

                new Token() {
                    Type = TokenType.SymbolBlockR,
                    Value = "}"
                },
            });

            var function = new FunctionParser().Parse(stream);
            var val = new Expr();

            val.ExplicitValue = LLVM.ConstInt(LLVMTypeRef.Int32Type(), 7, false);

            function.Body.ReturnValue = val;

            function.Emit(module);
            // --- Tests end ---

            LLVM.DumpModule(module);
            LLVM.DisposeBuilder(builder);
            LLVM.DisposeExecutionEngine(engine);
        }
    }
}
