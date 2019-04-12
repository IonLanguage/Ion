using LLVMSharp;

namespace LlvmSharpLang
{
    public interface IEntity<T>
    {
        T Emit(LLVMModuleRef module);
    }
}
