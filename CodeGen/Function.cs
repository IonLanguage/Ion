using System.Collections.Generic;
using LLVMSharp;

namespace LlvmSharpLang
{
    public class Function : NamedEntity<LLVMValueRef>
    {
        public FormalArg[] Args { get; set; }

        public LLVMValueRef Emit(LLVMModuleRef module)
        {
            LLVMTypeRef[] paramTypes = {
                LLVM.Int32Type(),
                LLVM.Int32Type()
            };

            LLVMTypeRef retType = LLVM.FunctionType(LLVM.Int32Type(), paramTypes, false);

            return LLVM.AddFunction(module, this.Name, retType);
        }
    }
}
