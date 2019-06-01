using Ion.CodeGeneration.Helpers;
using Ion.Misc;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    // TODO: Finish implementing.
    public class Attribute : Expr
    {
        public override ExprType ExprType => ExprType.Attribute;

        public bool Native { get; }

        public Attribute(string identifier, bool native = false)
        {
            this.SetName(identifier);
            this.Native = native;
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
