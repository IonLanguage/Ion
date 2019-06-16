using Ion.CodeGeneration.Helpers;
using Ion.CognitiveServices;
using Ion.IR.Generation;
using Ion.Misc;
using Ion.Syntax;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class BooleanExpr : Expr
    {
        public override ExprType ExprType => ExprType.Boolean;

        public readonly TokenType tokenType;

        public readonly string value;

        public BooleanExpr(TokenType tokenType, string value)
        {
            this.tokenType = tokenType;
            this.value = value;
        }

        public override LLVMValueRef Emit(PipeContext<InstructionBuilder> context)
        {
            // Create a new primitive boolean type instance.
            PrimitiveType type = PrimitiveTypeFactory.Boolean();

            // Resolve the value.
            LLVMValueRef valueRef = Resolvers.Literal(this.tokenType, this.value, type);

            // Return the emitted value.
            return valueRef;
        }
    }
}
