namespace Ion.Generation
{
    public class StructDef : Construct
    {
        public override ConstructType ConstructType => throw new System.NotImplementedException();

        public string Identifier { get; }

        public StructDefBody Body { get; }

        public StructDef(string identifier, StructDefBody body)
        {
            this.Identifier = identifier;
            this.Body = body;
        }
    }
}
