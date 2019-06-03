using System;
using System.Collections.Generic;
using Ion.CodeGeneration.Helpers;
using Ion.Tracking.Symbols;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class StructExpr : Expr, IStatement
    {
        public StatementType StatementType => StatementType.Struct;

        public override ExprType ExprType => ExprType.Struct;

        public string TargetIdentifier { get; }

        public List<StructProperty> Body { get; }

        public StructExpr(string targetIdentifier, List<StructProperty> body)
        {
            this.TargetIdentifier = targetIdentifier;
            this.Body = body;
        }

        public override LLVMValueRef Emit(PipeContext<LLVMBuilderRef> context)
        {
            // Ensure target struct exists on the symbol table.
            if (!context.SymbolTable.structs.Contains(this.TargetIdentifier))
            {
                throw new Exception($"Reference to undefined struct named '${this.TargetIdentifier}'");
            }

            // Retrieve the symbol from the symbol table.
            StructSymbol symbol = context.SymbolTable.structs[this.TargetIdentifier];

            // Retrieve the target struct's LLVM reference value from the symbol.
            LLVMTypeRef structDef = symbol.Value;

            // Create a value buffer list.
            List<LLVMValueRef> values = new List<LLVMValueRef>();

            // Populate body properties.
            foreach (StructProperty property in this.Body)
            {
                // Emit and append the value to the buffer list.
                values.Add(property.Value.Emit(context));
            }

            // Create the resulting struct assignment value.
            LLVMValueRef assignment = LLVM.ConstNamedStruct(structDef, values.ToArray());

            // Return the resulting struct assignment instruction.
            return assignment;
        }
    }
}
