using Ion.CodeGeneration.Structure;
using Ion.CognitiveServices;
using Ion.SyntaxAnalysis;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public delegate LLVMValueRef ConstantValueResolver(Type type, string value);

    public class Value : IUncontextedEntity<LLVMValueRef>
    {
        public Value(Type type, TokenType tokenType, string valueString)
        {
            Type = type;
            TokenType = tokenType;
            ValueString = valueString;
        }

        public string ValueString { get; }

        public Type Type { get; }

        public TokenType TokenType { get; }

        public LLVMValueRef Emit()
        {
            // Resolve the literal value.
            return Resolvers.Literal(TokenType, ValueString, Type);
        }
    }
}