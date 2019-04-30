using System;
using LLVMSharp;
using Ion.CodeGeneration.Structure;
using Ion.CognitiveServices;
using Ion.Misc;
using Ion.SyntaxAnalysis;

namespace Ion.CodeGeneration
{
    public delegate LLVMValueRef ConstantValueResolver(Type type, string value);

    public class Value : IUncontextedEntity<LLVMValueRef>
    {
        public string ValueString { get; }

        public Type Type { get; }

        public TokenType TokenType { get; }

        public Value(Type type, TokenType tokenType, string valueString)
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
