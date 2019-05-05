using System;
using Ion.Core;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class VariableExpr : Expr
    {
        public VariableExpr(string name)
        {
            this.SetName(name);
        }

        public override ExprType Type => ExprType.VariableReference;

        public override LLVMValueRef Emit(LLVMBuilderRef context)
        {
            // Ensure the variable exists in the local scope.
            if (!SymbolTable.localScope.ContainsKey(this.Name))
            {
                throw new Exception($"Reference to undefined variable named '{this.Name}'");
            }

            // Retrieve the value.
            LLVMValueRef value = SymbolTable.localScope[this.Name];

            // Return the retrieved value.
            return value;
        }
    }
}
