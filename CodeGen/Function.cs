using System.Collections.Generic;
using LLVMSharp;

namespace LlvmSharpLang.CodeGen
{
    public class Function : NamedModuleEntity<LLVMValueRef>
    {
        public FormalArg[] Args { get; set; }
        public Block Body { get; set; }

        public override LLVMValueRef Emit(LLVMModuleRef module)
        {
            LLVMTypeRef[] paramTypes = { };
            LLVMTypeRef retType = LLVM.FunctionType(LLVM.VoidType(), paramTypes, false);

            // Create the function.
            LLVMValueRef function = LLVM.AddFunction(module, this.Name, retType);

            // Apply the body.
            this.Body.Emit(function);

            return function;
        }
    }
}
