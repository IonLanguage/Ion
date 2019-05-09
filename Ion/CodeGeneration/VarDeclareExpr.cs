using Ion.CodeGeneration.Structure;
using Ion.Core;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class VarDeclareExpr : Expr, IStatement, IPipe<LLVMValueRef, LLVMBuilderRef>
    {
        public VarDeclareExpr(Type valueType, Expr value)
        {
            this.ValueType = valueType;
            this.Value = value;
        }

        public override ExprType Type => ExprType.VariableDeclaration;

        public Type ValueType { get; protected set; }

        public Expr Value { get; set; }

        public override LLVMValueRef Emit(LLVMBuilderRef context)
        {
            // Create the variable.
            LLVMValueRef variable = LLVM.BuildAlloca(context, this.ValueType.Emit(), this.Name);

            // Assign value if applicable.
            if (this.Value != null)
            {
                LLVM.BuildStore(context, this.Value.Emit(context), variable);
                SymbolTable.localScope.Add(this.Name, variable);
            }

            return variable;
        }

        public StatementType StatementType => StatementType.Declaration;
    }
}
