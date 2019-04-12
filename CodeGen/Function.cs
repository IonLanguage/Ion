using LLVMSharp;

namespace LlvmSharpLang
{
    public struct Function : Entity<LLVMValueRef>
    {
        public string Name { get; protected set; } = "anonymous";

        public LLVMValueRef Emit()
        {
            return LLVM.AddFunction(mod, this.Name, retType);
        }

        public void SetName(string name)
        {
            // TODO: Validate name here.
            this.Name = name;
        }
    }
}
