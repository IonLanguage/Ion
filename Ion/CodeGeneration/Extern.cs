using System;
using Ion.CodeGeneration.Structure;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class Extern : IEntity<LLVMValueRef, LLVMModuleRef>
    {
        public Extern(Prototype prototype)
        {
            Prototype = prototype;
        }

        private Prototype Prototype { get; }

        public LLVMValueRef Emit(LLVMModuleRef context)
        {
            // Ensure prototype is set.
            if (Prototype == null) throw new Exception("Unexpected external definition's prototype to be null");

            // Emit the formal arguments.
            var args = Prototype.Args.Emit();

            // Emit the return type.
            LLVMTypeRef returnType = Prototype.ReturnType.Emit();

            // Emit the function type.
            LLVMTypeRef type = LLVM.FunctionType(returnType, args, Prototype.Args.Continuous);

            // Emit the external definition to context and capture the LLVM value reference.
            LLVMValueRef external = LLVM.AddFunction(context, Prototype.Name, type);

            // Return the resulting LLVM value reference.
            return external;
        }
    }
}