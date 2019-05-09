namespace Ion.CodeGeneration.Structure
{
    public interface IReaction<T>
    {
        void React(T context);
    }
}
