using System.Collections.Generic;
using LLVMSharp;

namespace LlvmSharpLang
{
    public class Function : NamedModuleEntity<LLVMValueRef>
    {
        public FormalArg[] Args { get; set; }
        public Block Body { get; set; }

        public override LLVMValueRef Emit(LLVMModuleRef module)
        {
            LLVMTypeRef[] paramTypes = {
                LLVM.Int32Type(),
                LLVM.Int32Type()
            };

            LLVMTypeRef retType = LLVM.FunctionType(LLVM.Int32Type(), paramTypes, false);

            // Create the function.
            LLVMValueRef function =  LLVM.AddFunction(module, this.Name, retType);

            // Apply the body.
            this.Body.Emit(function);

            return function;
        }
    }
}
