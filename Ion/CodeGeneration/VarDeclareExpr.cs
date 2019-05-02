using LLVMSharp;
using Ion.CodeGeneration;
using Ion.CodeGeneration.Structure;
using Ion.Core;
using Ion.Misc;

namespace Ion.CodeGeneration
{
    public class VarDeclareExpr : Expr, IStatement, IEntity<LLVMValueRef, LLVMBuilderRef>
    {
        public override ExprType Type => ExprType.VariableDeclaration;

        public StatementType StatementType => StatementType.Declaration;

        public Type ValueType { get; protected set; }

        public Expr Value { get; set; }

        public VarDeclareExpr(Type valueType, Expr value)
        {
            this.ValueType = valueType;
            this.Value = value;
        }

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
    }
}
