using LLVMSharp;
using LlvmSharpLang.CodeGeneration.Structure;
using LlvmSharpLang.Misc;

namespace LlvmSharpLang.CodeGeneration
{
    public class Extern : IEntity<LLVMValueRef, LLVMModuleRef>
    {
        Prototype Prototype { get; set; }

        public Extern(Prototype prototype)
        {
            this.Prototype = prototype;
        }

        public LLVMValueRef Emit(LLVMModuleRef context)
        {
            // Emit the formal arguments.
            LLVMTypeRef[] args = this.Prototype.Args.Emit();

            // TODO: Forcing Void type for return (Prototype is missing return type).
            // Emit the function type.
            LLVMTypeRef type = LLVM.FunctionType(TypeFactory.Void().Emit(), args, this.Prototype.Args.Continuous);

            // Emit the external definition to context and capture the LLVM value reference.
            LLVMValueRef external = LLVM.AddFunction(context, this.Prototype.Name, type);

            // Return the resulting LLVM value reference.
            return external;
        }
    }
}
