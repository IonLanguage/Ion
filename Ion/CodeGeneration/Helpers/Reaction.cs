namespace Ion.CodeGeneration.Helpers
{
    public interface IReaction<T>
    {
        void Invoke(T context);
    }
}
