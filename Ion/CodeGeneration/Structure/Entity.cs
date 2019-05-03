namespace Ion.CodeGeneration.Structure
{
    public interface IEntity<TResult, TContext>
    {
        TResult Emit(TContext context);
    }
}