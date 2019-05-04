using System;
using Ion.CodeGeneration.Structure;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class Extern : IEntity<LLVMValueRef, LLVMModuleRef>
    {
        public Extern(Prototype prototype)
        {
            this.Prototype = prototype;
        }

        private Prototype Prototype { get; }

        public LLVMValueRef Emit(LLVMModuleRef context)
        {
            // Ensure prototype is set.
            if (this.Prototype == null) throw new Exception("Unexpected external definition's prototype to be null");

            // Emit the formal arguments.
            LLVMTypeRef[] args = this.Prototype.Args.Emit();

            // Emit the return type.
            LLVMTypeRef returnType = this.Prototype.ReturnType.Emit();

            // Emit the function type.
            LLVMTypeRef type = LLVM.FunctionType(returnType, args, this.Prototype.Args.Continuous);

            // Emit the external definition to context and capture the LLVM value reference.
            LLVMValueRef external = LLVM.AddFunction(context, this.Prototype.Name, type);

            // Return the resulting LLVM value reference.
            return external;
        }
    }
}
