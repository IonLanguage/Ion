namespace Ion.CodeGeneration.Structure
{
    public interface IPipe<TInput, TOutput>
    {
        TOutput Emit(PipeContext<TInput> context);
    }
}
