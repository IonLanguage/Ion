using Ion.CognitiveServices;
using Ion.SyntaxAnalysis;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class StringExpr : Expr
    {
        public override ExprType Type => ExprType.String;

        public readonly TokenType tokenType;

        public readonly Type type;

        public readonly string value;

        public StringExpr(TokenType tokenType, Type type, string value)
        {
            this.tokenType = tokenType;
            this.type = type;
            this.value = value;
        }

        public override LLVMValueRef Emit(LLVMBuilderRef context)
        {
            // Emit the value.
            LLVMValueRef valueRef = Resolvers.Literal(this.tokenType, this.value, this.type);

            // Return the emitted value.
            return valueRef;
        }
    }
}
