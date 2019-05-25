namespace Ion.Tracking
{
    public interface INamedSymbol
    {
        string Identifier { get; }
    }

    public abstract class NamedSymbol<T> : Symbol<T>, INamedSymbol
    {
        public string Identifier { get; }

        public NamedSymbol(string identifier, T value) : base(value)
        {
            this.Identifier = identifier;
        }
    }
}
