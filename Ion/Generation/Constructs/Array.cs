namespace Ion.Generation
{
    public class Array : Construct
    {
        public override ConstructType ConstructType => ConstructType.Array;

        public Type Type { get; }

        public Value[] Values { get; }

        public Array(Type type, Value[] values)
        {
            this.Type = type;
            this.Values = values;
        }

        public override Construct Accept(IrVisitor visitor)
        {
            return visitor.VisitArray(this);
        }
    }
}
