namespace Ion.Generation
{
    public class StructProperty : Construct
    {
        public override ConstructType ConstructType => throw new System.NotImplementedException();

        public string Identifier { get; }

        public Construct Value { get; }

        public int Index { get; }

        public StructProperty(string identifier, Construct value, int index)
        {
            this.Identifier = identifier;
            this.Value = value;
            this.Index = index;
        }
    }
}
