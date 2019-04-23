using LLVMSharp;
using LlvmSharpLang.CognitiveServices;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.CodeGeneration
{
    public class NumericExpr : Expr
    {
        public override ExprType Type => ExprType.Numeric;

        public readonly TokenType tokenType;

        public readonly Type type;

        public readonly string value;

        public NumericExpr(TokenType tokenType, Type type, string value)
        {
            this.tokenType = tokenType;
            this.type = type;
            this.value = value;
        }

        public override LLVMValueRef Emit(LLVMBuilderRef context)
        {
            // Emit the value.
            LLVMValueRef value = Resolvers.Literal(this.tokenType, this.value, this.type);

            // Return the emitted value.
            return value;
        }
    }
}
