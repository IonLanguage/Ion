namespace Ion.Generation
{
    public class Lambda : Construct
    {
        public override ConstructType ConstructType => ConstructType.Lambda;

        public FormalArgs Arguments { get; set; }

        public Type ReturnType { get; set; }

        public Block Body { get; set; }

        public Lambda(Type returnType, Block body)
        {
            this.ReturnType = returnType;
            this.Body = body;
        }

        public override Construct Accept(IrVisitor visitor)
        {
            return visitor.VisitLambda(this);
        }
    }
}
