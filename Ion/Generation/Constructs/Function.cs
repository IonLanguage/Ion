namespace Ion.Generation
{
    public class Function : Construct
    {
        public override ConstructType ConstructType => ConstructType.Function;

        public Attribute[] Attributes { get; set; }

        public Prototype Prototype { get; set; }

        public Block Body { get; set; }

        public Function()
        {
            this.Attributes = new Attribute[] { };
        }

        public override Construct Accept(IrVisitor visitor)
        {
            return visitor.VisitFunction(this);
        }
    }
}
