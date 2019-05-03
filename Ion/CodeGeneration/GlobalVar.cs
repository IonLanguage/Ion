using Ion.CodeGeneration.Structure;
using Ion.Misc;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class GlobalVar : Named, IEntity<LLVMValueRef, LLVMModuleRef>
    {
        public GlobalVar(Type type)
        {
            Type = type;
        }

        public Type Type { get; }

        public Value Value { get; set; }

        public LLVMValueRef Emit(LLVMModuleRef context)
        {
            // Create the global variable.
            LLVMValueRef globalVar = LLVM.AddGlobal(context, Type.Emit(), Name);

            // Set the linkage to common.
            globalVar.SetLinkage(LLVMLinkage.LLVMCommonLinkage);

            // Assign value if applicable.
            if (Value != null) globalVar.SetInitializer(Value.Emit());

            return globalVar;
        }
    }
}