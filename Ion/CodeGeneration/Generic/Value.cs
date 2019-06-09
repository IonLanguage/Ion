using Ion.CodeGeneration.Helpers;
using Ion.CognitiveServices;
using Ion.Syntax;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public delegate LLVMValueRef ConstantValueResolver(Type type, string value);

    public class Value : IUncontextedEntity<LLVMValueRef>
    {
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
            return Resolvers.Literal(this.TokenType, this.ValueString, this.Type);
        }
    }
}
