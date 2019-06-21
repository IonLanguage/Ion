namespace Ion.CodeGeneration
{
    // TODO: Finish implementing.
    public class Attribute : Construct
    {
        public override ConstructType ConstructType => ExprType.Attribute;

        public bool Native { get; }

        public Attribute(string identifier, bool native = false)
        {
            this.SetName(identifier);
            this.Native = native;
        }
    }
}
