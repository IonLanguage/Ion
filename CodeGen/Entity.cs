using LLVMSharp;

namespace LlvmSharpLang
{
    public interface IEntity<TResult, TContext>
    {
        TResult Emit(TContext context);
    }
}
