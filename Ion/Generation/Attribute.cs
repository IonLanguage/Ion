namespace Ion.Generation
{
    // TODO: Finish implementing.
    public class Attribute : Construct
    {
        public override ConstructType ConstructType => ConstructType.Attribute;

        public string Identifier { get; }

        public bool Native { get; }

        public Attribute(string identifier, bool native = false)
        {
            this.Identifier = identifier;
            this.Native = native;
        }
    }
}
