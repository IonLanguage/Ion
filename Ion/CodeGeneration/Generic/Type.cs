using System;
using Ion.CodeGeneration.Helpers;
using Ion.CognitiveServices;
using Ion.Misc;
using Ion.SyntaxAnalysis;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class Type : IGenericPipe<LLVMTypeRef>
    {
        protected readonly Token token;

        public Type(Token token)
        {
            this.token = token;
        }

        public LLVMTypeRef Emit(IGenericPipeContext context)
        {
            // Use LLVM type resolver if token is a primitive type.
            if (TokenIdentifier.IsPrimitiveType(this.token))
            {
                // Delegate to primitive type construct.
                return new PrimitiveType(this.token.Value).Emit();
            }
            // Otherwise, look it up on the structs dictionary, on the symbol table.
            else if (context.SymbolTable.structs.ContainsKey(this.token.Value))
            {
                return context.SymbolTable.structs[this.token.Value];
            }

            // At this point, provided token is not a valid type.
            throw new Exception($"Token is not a primitive nor user-defined type: '{this.token.Value}'");
        }
    }
}
