using System.Collections.Generic;
using LLVMSharp;

namespace LlvmSharpLang.CodeGen
{
    public class Function : NamedModuleEntity<LLVMValueRef>
    {
        public List<Arg> Args { get; set; }

        public Block Body { get; set; }

        public Type ReturnType { get; set; }

        public override LLVMValueRef Emit(LLVMModuleRef module)
        {
            // Create argument type array.
            LLVMTypeRef[] argTypes = this.Args.ConvertAll<LLVMTypeRef>((Arg arg) =>
            {
                return arg.Emit(module);
            }).ToArray();

            // Create the return type.
            LLVMTypeRef returnType = LLVM.FunctionType(this.ReturnType.Emit(), argTypes, false);

            // Create the function.
            LLVMValueRef function = LLVM.AddFunction(module, this.Name, returnType);

            // Apply the body.
            this.Body.Emit(function);

            return function;
        }

        public void SetArgs(Arg[] args)
        {
            this.Args = new List<Arg>(args);
        }
    }
}
