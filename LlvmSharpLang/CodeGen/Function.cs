using System.Collections.Generic;
using LLVMSharp;
using LlvmSharpLang.CodeGen.Structure;
using LlvmSharpLang.Misc;

namespace LlvmSharpLang.CodeGen
{
    public class Function : Named, IEntity<LLVMValueRef, LLVMModuleRef>
    {
        public Args Args { get; set; }

        public Block Body { get; set; }

        public Type ReturnType { get; set; }

        public Function()
        {
            this.ReturnType = TypeFactory.Void;
        }

        public LLVMValueRef Emit(LLVMModuleRef context)
        {
            // Emit the argument types.
            LLVMTypeRef[] args = this.Args.Emit();

            // Emit the return type.
            LLVMTypeRef returnType = LLVM.FunctionType(this.ReturnType.Emit(), args, this.Args.Continuous);

            // Create the function.
            LLVMValueRef function = LLVM.AddFunction(context, this.Name, returnType);

            // Apply the body.
            this.Body.Emit(function);

            return function;
        }
    }
}
