using Ion.Syntax;

namespace Ion.Generation
{
    public class BinaryExpr : Construct
    {
        public override ConstructType ConstructType => ConstructType.BinaryExpr;

        public TokenType Operation { get; }

        public int Precedence { get; }

        public Construct LeftSide { get; }

        public Construct RightSide { get; }

        public string Identifier { get; }

        public BinaryExpr(string identifier, TokenType operation, Construct leftSide, Construct rightSide, int precedence)
        {
            this.Identifier = identifier;
            this.Operation = operation;
            this.LeftSide = leftSide;
            this.RightSide = rightSide;
            this.Precedence = precedence;
        }

        public override Construct Accept(IrVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
