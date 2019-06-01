using Ion.CodeGeneration.Helpers;
using Ion.Misc;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    // TODO: Finish implementing.
    public class Attribute : Expr
    {
        public override ExprType ExprType => ExprType.Attribute;

        public Attribute(string identifier)
        {
            this.SetName(identifier);
        }

        public override LLVMValueRef Emit(PipeContext<LLVMBuilderRef> context)
        {
            // Create a new call expression.
            CallExpr call = new CallExpr(this.Identifier);

            // Emit the call.
            LLVMValueRef result = call.Emit(context);

            // Return the resulting call reference value.
            return result;
        }
    }
}
