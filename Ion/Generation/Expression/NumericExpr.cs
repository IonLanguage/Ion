using Ion.Syntax;

namespace Ion.Generation
{
    public class NumericExpr : Construct
    {
        public override ConstructType ConstructType => ConstructType.Numeric;

        public readonly TokenType tokenType;

        public readonly PrimitiveType type;

        public readonly string value;

        public NumericExpr(TokenType tokenType, PrimitiveType type, string value)
        {
            this.tokenType = tokenType;
            this.type = type;
            this.value = value;
        }
    }
}
