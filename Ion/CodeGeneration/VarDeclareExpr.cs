using Ion.CodeGeneration.Structure;
using Ion.Core;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class VarDeclareExpr : Expr, IStatement, IEntity<LLVMValueRef, LLVMBuilderRef>
    {
        public VarDeclareExpr(Type valueType, Expr value)
        {
            ValueType = valueType;
            Value = value;
        }

        public override ExprType Type => ExprType.VariableDeclaration;

        public Type ValueType { get; protected set; }

        public Expr Value { get; set; }

        public override LLVMValueRef Emit(LLVMBuilderRef context)
        {
            // Create the variable.
            LLVMValueRef variable = LLVM.BuildAlloca(context, ValueType.Emit(), Name);

            // Assign value if applicable.
            if (Value != null)
            {
                LLVM.BuildStore(context, Value.Emit(context), variable);
                SymbolTable.localScope.Add(Name, variable);
            }

            return variable;
        }

        public StatementType StatementType => StatementType.Declaration;
    }
}