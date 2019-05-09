namespace Ion.CodeGeneration.Structure
{
    public interface IReaction<T>
    {
        void Invoke(T context);
    }
}
