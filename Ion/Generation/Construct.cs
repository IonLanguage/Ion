namespace Ion.Generation
{
    public abstract class Construct : IrVisitable
    {
        public abstract ConstructType ConstructType { get; }

        public abstract Construct Accept(IrVisitor visitor);
    }
}
