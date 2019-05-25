using Ion.CodeGeneration.Helpers;
using Ion.Core;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class VarDeclareExpr : Expr, IStatement, IPipe<LLVMBuilderRef, LLVMValueRef>
    {
        public override ExprType Type => ExprType.VariableDeclaration;

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
            LLVMValueRef variable = LLVM.BuildAlloca(context.Target, this.ValueType.Emit(), this.Name);

            // Assign value if applicable.
            if (this.Value != null)
            {
                // Create the store instruction.
                LLVM.BuildStore(context.Target, this.Value.Emit(context), variable);

                // Register on symbol table.
                context.SymbolTable.localScope.Add(this.Name, variable);
            }

            // Return the resulting variable.
            return variable;
        }

        public StatementType StatementType => StatementType.Declaration;
    }
}
