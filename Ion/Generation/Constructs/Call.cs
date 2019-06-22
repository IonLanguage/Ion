namespace Ion.Generation
{
    public class Call : Construct
    {
        public override ConstructType ConstructType => ConstructType.Call;

        public string TargetIdentifier { get; }

        public Construct[] Arguments { get; }

        public Call(string targetName, Construct[] arguments)
        {
            this.TargetIdentifier = targetName;
            this.Arguments = arguments;
        }

        public Call(string targetName) : this(targetName, new Construct[] { })
        {
            //
        }

        public override Construct Accept(CodeGenVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
