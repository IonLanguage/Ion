namespace Ion.Generation
{
    public class Extern : Construct
    {
        public Prototype Prototype { get; }

        public override ConstructType ConstructType => ConstructType.Extern;

        public Extern(Prototype prototype)
        {
            this.Prototype = prototype;
        }

        public override Construct Accept(IrVisitor visitor)
        {
            return visitor.VisitExtern(this);
        }
    }
}
