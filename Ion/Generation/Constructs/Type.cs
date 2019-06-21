using Ion.Syntax;

namespace Ion.Generation
{
    public class Type : Construct
    {
        public bool IsVoid => this.Token.Type == TokenType.TypeVoid;

        public bool IsArray => this.ArrayLength.HasValue;

        public Token Token { get; }

        public bool IsPointer { get; }

        public override ConstructType ConstructType => ConstructType.Type;

        public readonly uint? ArrayLength;

        public Type(Token token, bool isPointer = false, uint? arrayLength = null)
        {
            this.Token = token;
            this.IsPointer = isPointer;
            this.ArrayLength = arrayLength;
        }

        public override Construct Accept(CodeGenVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
