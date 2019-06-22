using Ion.Generation.Helpers;
using LLVMSharp;

namespace Ion.Generation
{
    public class VarDeclareExpr : Expr, IStatement, IContextPipe<LLVMBuilderRef, LLVMValueRef>
    {
        public override ExprType ExprType => ExprType.VariableDeclaration;

        public Type ValueType { get; protected set; }

        public Expr Value { get; set; }

        public VarDeclareExpr(Type valueType, Expr value)
        {
            this.ValueType = valueType;
            this.Value = value;
        }

        public override LLVMValueRef Emit(PipeContext<LLVMBuilderRef> context)
        {
            // Create the variable.
            LLVMValueRef variable = LLVM.BuildAlloca(context.Target, this.ValueType.Emit(), this.Identifier);

            // Assign value if applicable.
            if (this.Value != null)
            {
                // Create the store instruction.
                LLVM.BuildStore(context.Target, this.Value.Emit(context), variable);

                // Register on symbol table.
                context.SymbolTable.localScope.Add(this.Identifier, variable);
            }

            // Return the resulting variable.
            return variable;
        }

        public StatementType StatementType => StatementType.Declaration;
    }
}
