using System.Collections.Generic;
using LLVMSharp;

namespace LlvmSharpLang.CodeGen
{
    public class Function : NamedModuleEntity<LLVMValueRef>
    {
        public FormalArg[] Args { get; set; }

        public Block Body { get; set; }

        public Type ReturnType { get; set; }

        public override LLVMValueRef Emit(LLVMModuleRef module)
        {
            // TODO: Arg types.
            LLVMTypeRef[] argTypes = { };

            // Create the return type.
            LLVMTypeRef returnType = LLVM.FunctionType(this.ReturnType.Emit(), argTypes, false);

            // Create the function.
            LLVMValueRef function = LLVM.AddFunction(module, this.Name, returnType);

            // Apply the body.
            this.Body.Emit(function);

            return function;
        }
    }
}
