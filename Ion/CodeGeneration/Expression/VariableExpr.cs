using System;
using Ion.CodeGeneration.Helpers;
using Ion.Core;
using Ion.Parsing;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class VariableExpr : Expr
    {
        public override ExprType ExprType => ExprType.VariableReference;

        public VariableExpr(string name)
        {
            this.SetName(name);
        }

        public VariableExpr(PathResult path) : this(path.ToString())
        {
            //
        }

        public override LLVMValueRef Emit(PipeContext<LLVMBuilderRef> context)
        {
            // Ensure the variable exists in the local scope.
            if (!context.SymbolTable.localScope.ContainsKey(this.Identifier))
            {
                throw new Exception($"Reference to undefined variable named '{this.Identifier}'");
            }

            // Retrieve the value.
            LLVMValueRef value = context.SymbolTable.localScope[this.Identifier];

            // Return the retrieved value.
            return value;
        }
    }
}
