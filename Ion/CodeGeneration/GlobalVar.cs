using Ion.CodeGeneration.Helpers;
using Ion.Misc;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class GlobalVar : Named, ITopLevelPipe
    {
        public Type Type { get; }

        public Value Value { get; set; }

        public GlobalVar(Type type)
        {
            this.Type = type;
        }

        public LLVMValueRef Emit(PipeContext<Module> context)
        {
            // Create the global variable.
            LLVMValueRef globalVar = LLVM.AddGlobal(context.Target.Target, this.Type.Emit(), this.Name);

            // Set the linkage to common.
            globalVar.SetLinkage(LLVMLinkage.LLVMCommonLinkage);

            // Assign value if applicable.
            if (this.Value != null)
            {
                globalVar.SetInitializer(this.Value.Emit());
            }

            return globalVar;
        }
    }
}
