using Ion.CognitiveServices;
using Ion.SyntaxAnalysis;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class NumericExpr : Expr
    {
        public readonly TokenType tokenType;

        public readonly Type type;

        public readonly string value;

        public NumericExpr(TokenType tokenType, Type type, string value)
        {
            this.tokenType = tokenType;
            this.type = type;
            this.value = value;
        }

        public override ExprType Type => ExprType.Numeric;

        public override LLVMValueRef Emit(LLVMBuilderRef context)
        {
            // Emit the value.
            LLVMValueRef valueRef = Resolvers.Literal(tokenType, value, type);

            // Return the emitted value.
            return valueRef;
        }
    }
}