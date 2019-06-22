using System;
using Ion.Generation.Helpers;
using Ion.Parsing;
using LLVMSharp;

namespace Ion.Generation
{
    public class VariableExpr : Expr
    {
        public override ExprType ExprType => ExprType.VariableReference;

        public PathResult Path { get; }

        public VariableExpr(string name) : this(new PathResult(name))
        {
            this.SetName(name);
        }

        public VariableExpr(PathResult path)
        {
            this.Path = path;
            this.SetName(this.Path.ToString());
        }

        public override LLVMValueRef Emit(PipeContext<LLVMBuilderRef> context)
        {
            // Variable is nested within a struct (struct property).
            if (this.Path.nodes.Count >= 2 && context.SymbolTable.localScope.ContainsKey(this.Path.FirstNode))
            {
                // TODO: Add support for nested properties more than 1 level.
                if (this.Path.nodes.Count > 2)
                {
                    throw new NotImplementedException("Support for deeply nested nodes has not been implemented");
                }

                // Retrieve the struct from the local scope in the symbol table.
                LLVMValueRef @struct = context.SymbolTable.localScope[this.Path.FirstNode];

                // TODO: Index is hard-coded, need method to determine index from prop identifier (in local scope).
                // Create a reference to the struct's targeted property.
                LLVMValueRef reference = LLVM.BuildStructGEP(context.Target, @struct, 0, this.Identifier);

                // Return the reference.
                return reference;
            }
            // Ensure the variable exists in the local scope.
            else if (!context.SymbolTable.localScope.ContainsKey(this.Identifier))
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
