using System;
using Ion.CodeGeneration.Helpers;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class Struct : Expr, IStatement
    {
        public StatementType StatementType => StatementType.Struct;

        public override ExprType Type => ExprType.Struct;

        public string TargetIdentifier { get; }

        // TODO: Inline-property definitions support missing.
        public Struct(string targetIdentifier)
        {
            this.TargetIdentifier = targetIdentifier;
        }

        public override LLVMValueRef Emit(PipeContext<LLVMBuilderRef> context)
        {
            // Ensure target struct exists on the symbol table.
            if (!context.SymbolTable.structs.Contains(this.TargetIdentifier))
            {
                throw new Exception($"Reference to undefined struct named '${this.TargetIdentifier}'");
            }

            // Otherwise, retrieve the target struct.
            LLVMTypeRef @struct = context.SymbolTable.structs[this.TargetIdentifier].Value;

            // Build the struct allocation instruction.
            LLVMValueRef value = LLVM.BuildAlloca(context.Target, @struct, this.Name);

            // Return the resulting value reference instruction.
            return value;
        }
    }
}
