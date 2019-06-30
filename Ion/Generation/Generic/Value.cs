using Ion.CognitiveServices;
using Ion.Syntax;
using LLVMSharp;

namespace Ion.Generation
{
    public delegate LLVMValueRef ConstantValueResolver(Type type, string value);

    public class Value : Construct
    {
        public override ConstructType ConstructType => throw new System.NotImplementedException();

        public string ValueString { get; }

        public PrimitiveType Type { get; }

        public TokenType TokenType { get; }

        public Value(PrimitiveType type, TokenType tokenType, string valueString)
        {
            this.Type = type;
            this.TokenType = tokenType;
            this.ValueString = valueString;
        }

        public LLVMValueRef Emit()
        {
            // Resolve the literal value.
            return Resolver.Literal(this.TokenType, this.ValueString, this.Type);
        }

        public override Construct Accept(IrVisitor visitor)
        {
            return visitor.VisitValue(this);
        }
    }
}
