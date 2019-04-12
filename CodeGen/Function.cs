using LLVMSharp;

namespace LlvmSharpLang
{
    public class Function : IEntity<LLVMValueRef>
    {
        public string Name { get; protected set; } = "anonymous";

        public LLVMValueRef Emit(LLVMModuleRef module)
        {
            LLVMTypeRef[] paramTypes = {
                LLVM.Int32Type(),
                LLVM.Int32Type()
            };

            LLVMTypeRef retType = LLVM.FunctionType(LLVM.Int32Type(), paramTypes, false);

            return LLVM.AddFunction(module, this.Name, retType);
        }

        public void SetName(string name)
        {
            // TODO: Validate name here.
            this.Name = name;
        }
    }
}
