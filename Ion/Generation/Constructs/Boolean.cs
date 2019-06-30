using Ion.Syntax;

namespace Ion.Generation
{
    public class Boolean : Construct
    {
        public override ConstructType ConstructType => ConstructType.Boolean;

        public readonly TokenType tokenType;

        public readonly string value;

        public Boolean(TokenType tokenType, string value)
        {
            this.tokenType = tokenType;
            this.value = value;
        }

        public override Construct Accept(IrVisitor visitor)
        {
            return visitor.VisitBoolean(this);
        }
    }
}
