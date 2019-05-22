using Ion.CodeGeneration.Helpers;
using Ion.CognitiveServices;
using Ion.Misc;
using Ion.SyntaxAnalysis;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class BooleanExpr : Expr
    {
        public override ExprType Type => ExprType.Boolean;

        public readonly TokenType tokenType;

        public readonly string value;

        public BooleanExpr(TokenType tokenType, string value)
        {
            this.tokenType = tokenType;
            this.value = value;
        }

        public override LLVMValueRef Emit(PipeContext<LLVMBuilderRef> context)
        {
            // Resolve the value.
            LLVMValueRef valueRef = Resolvers.Literal(this.tokenType, this.value, PrimitiveTypeFactory.Boolean());

            // Return the emitted value.
            return valueRef;
        }
    }
}
