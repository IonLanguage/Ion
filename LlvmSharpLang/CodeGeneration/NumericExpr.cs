namespace LlvmSharpLang.CodeGeneration
{
    public class NumericExpr : Expr
    {
        protected readonly Type type;

        protected readonly string value;

        public NumericExpr(Type type, string value)
        {
            this.type = type;
            this.value = value;
        }

        public override LLVMValueRef Emit(LLVMBuilderRef context)
        {
            //
        }
    }
}
