using Ion.Misc;

namespace Ion.Tracking
{
    public abstract class NamedSymbol<T> : Symbol<T>, INamed
    {
        public string Identifier { get; }

        public NamedSymbol(string identifier, T value) : base(value)
        {
            this.Identifier = identifier;
        }
    }
}
