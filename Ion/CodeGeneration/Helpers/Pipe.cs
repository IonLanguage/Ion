namespace Ion.CodeGeneration.Helpers
{
    public interface IPipe<TInput, TOutput>
    {
        TOutput Emit(PipeContext<TInput> context);
    }
}
