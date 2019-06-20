namespace Ion.CodeGeneration
{
    public class Extern : Construct
    {
        public Prototype Prototype { get; }

        public override ConstructType ConstructType => ConstructType.Extern;

        public Extern(Prototype prototype)
        {
            this.Prototype = prototype;
        }

        public override Construct Accept(CodeGenVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
