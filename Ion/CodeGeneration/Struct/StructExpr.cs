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

        public override ExprType Type => ExprType.Struct;

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
            LLVMTypeRef @struct = symbol.Value;

            // Build the struct allocation instruction.
            LLVMValueRef value = LLVM.BuildAlloca(context.Target, @struct, this.Name);

            // Populate body properties.
            foreach (StructProperty property in this.Body)
            {
                // TODO: Why does it require LLVMValueRef, instead of LLVMTypeRef when logically (?) it's the target struct?
                // LLVMValueRef reference = LLVM.BuildStructGEP(context.Target, @struct., (uint)property.Index, property.Name);
            }

            // Return the resulting value reference instruction.
            return value;
        }
    }
}
