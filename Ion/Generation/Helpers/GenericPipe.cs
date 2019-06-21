namespace Ion.Generation.Helpers
{
    public interface IGenericPipe<T>
    {
        T Emit(IGenericPipeContext context);
    }
}
