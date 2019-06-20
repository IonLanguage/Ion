namespace Ion.CodeGeneration
{
    public class Global : Construct
    {
        public Type Type { get; }

        public Value Value { get; set; }

        public override ConstructType ConstructType => ConstructType.Global;

        public Global(string identifier, Type type)
        {
            this.SetName(identifier);
            this.Type = type;
        }

        public override Construct Accept(CodeGenVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
