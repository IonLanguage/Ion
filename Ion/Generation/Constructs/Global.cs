#nullable enable

namespace Ion.Generation
{
    public class Global : Construct
    {
        public override ConstructType ConstructType => ConstructType.Global;

        public string Identifier { get; }

        public Type Type { get; }

        // TODO: Need to verify value as a constant.
        public Value? InitialValue { get; }

        public Global(string identifier, Type type, Value? initialValue = null)
        {
            this.Identifier = identifier;
            this.Type = type;
            this.InitialValue = initialValue;
        }

        public override Construct Accept(IrVisitor visitor)
        {
            return visitor.VisitGlobal(this);
        }
    }
}
