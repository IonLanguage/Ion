namespace Ion.Generation
{
    public class StructDef : Construct
    {
        public override ConstructType ConstructType => ConstructType.StructDef;

        public string Identifier { get; }

        public StructDefBody Body { get; }

        public StructDef(string identifier, StructDefBody body)
        {
            this.Identifier = identifier;
            this.Body = body;
        }
    }
}
