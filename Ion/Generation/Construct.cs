namespace Ion.Generation
{
    public abstract class Construct : IrVisitable
    {
        public abstract ConstructType ConstructType { get; }

        public virtual Construct Accept(IrVisitor visitor)
        {
            return visitor.VisitExtension(this);
        }

        public virtual Construct VisitChildren(IrVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
