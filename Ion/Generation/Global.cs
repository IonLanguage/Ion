#nullable enable

namespace Ion.Generation
{
    public class Global : Construct
    {
        public override ConstructType ConstructType => ConstructType.Global;

        public string Identifier { get; }

        public Type Type { get; }

        // TODO: Need to verify value as a constant.
        public Construct? InitialValue { get; }

        public Global(string identifier, Type type, Construct? initialValue)
        {
            this.Identifier = identifier;
            this.Type = type;
            this.InitialValue = initialValue;
        }

        public override Construct Accept(CodeGenVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
