using Ion.CodeGeneration.Structure;
using Ion.CognitiveServices;
using Ion.SyntaxAnalysis;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class BooleanExpr : Expr
    {
        public override ExprType Type => ExprType.Boolean;

        public readonly TokenType tokenType;

        public readonly Type type;

        public readonly string value;

        public BooleanExpr(TokenType tokenType, Type type, string value)
        {
            this.tokenType = tokenType;
            this.type = type;
            this.value = value;
        }

        public override LLVMValueRef Emit(PipeContext<LLVMBuilderRef> context)
        {
            // Resolve the value.
            LLVMValueRef valueRef = Resolvers.Literal(this.tokenType, this.value, this.type);

            // Return the emitted value.
            return valueRef;
        }
    }
}
