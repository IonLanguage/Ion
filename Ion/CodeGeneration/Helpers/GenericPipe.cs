namespace Ion.CodeGeneration.Helpers
{
    public interface IGenericPipe<T>
    {
        T Emit(IGenericPipeContext context);
    }
}
