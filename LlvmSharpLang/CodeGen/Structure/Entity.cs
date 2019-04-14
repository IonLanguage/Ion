using LLVMSharp;

namespace LlvmSharpLang.CodeGen.Structure
{
    public interface IEntity<TResult, TContext>
    {
        TResult Emit(TContext context);
    }
}
