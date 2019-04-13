using LLVMSharp;

namespace LlvmSharpLang.CodeGen
{
    public interface IEntity<TResult, TContext>
    {
        TResult Emit(TContext context);
    }
}
