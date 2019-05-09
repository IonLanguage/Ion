namespace Ion.CodeGeneration.Structure
{
    public interface IPipe<TResult, TContext>
    {
        TResult Emit(TContext context);
    }
}
