using System;
using System.Runtime.InteropServices;
using LLVMSharp;

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
            LLVMModuleRef mod = LLVM.ModuleCreateWithName("EntryModule");

            LLVMTypeRef[] paramTypes = {
                LLVM.Int32Type(),
                LLVM.Int32Type()
            };

            LLVMTypeRef retType = LLVM.FunctionType(LLVM.Int32Type(), paramTypes, false);
            LLVMValueRef sum = LLVM.AddFunction(mod, "sum", retType);

            LLVMBasicBlockRef entry = LLVM.AppendBasicBlock(sum, "entry");

            LLVMBuilderRef builder = LLVM.CreateBuilder();

            LLVM.PositionBuilderAtEnd(builder, entry);

            LLVMValueRef tmp = LLVM.BuildAdd(builder, LLVM.GetParam(sum, 0), LLVM.GetParam(sum, 1), "tmp");

            LLVM.BuildRet(builder, tmp);

            if (LLVM.VerifyModule(mod, LLVMVerifierFailureAction.LLVMPrintMessageAction, out var error) != successFlag)
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

            if (LLVM.CreateMCJITCompilerForModule(out var engine, mod, options, out error) != successFlag)
            {
                Console.WriteLine($"Error: {error}");
            }

            var addMethod = (Add)Marshal.GetDelegateForFunctionPointer(LLVM.GetPointerToGlobal(engine, sum), typeof(Add));
            int result = addMethod(10, 10);

            Console.WriteLine("Result of sum is: " + result);

            LLVM.DumpModule(mod);
            LLVM.DisposeBuilder(builder);
            LLVM.DisposeExecutionEngine(engine);

            // Token tests.
            var stream = new TokenStream();
        }
    }
}
