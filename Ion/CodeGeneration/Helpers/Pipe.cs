namespace Ion.CodeGeneration.Helpers
{
    public interface IPipe<TContext, TOutput>
    {
        TOutput Emit(PipeContext<TContext> context);
    }
}
