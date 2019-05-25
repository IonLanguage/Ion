namespace Ion.Tracking
{
    public abstract class Symbol<T>
    {
        public T Value { get; }

        public Symbol(T value)
        {
            this.Value = value;
        }
    }
}
