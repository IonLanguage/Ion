namespace Ion.Generation
{
    public class StructDefProperty
    {
        public Type Type { get; }

        public string Identifier { get; }

        public StructDefProperty(Type type, string identifier)
        {
            this.Type = type;
            this.Identifier = identifier;
        }
    }
}
