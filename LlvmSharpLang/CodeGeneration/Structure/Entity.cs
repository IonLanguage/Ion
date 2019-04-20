using LLVMSharp;

namespace LlvmSharpLang.CodeGeneration.Structure
{
    public interface IEntity<TResult, TContext>
    {
        TResult Emit(TContext context);
    }
}
