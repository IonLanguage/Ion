using System;
using Ion.CodeGeneration.Helpers;
using Ion.CognitiveServices;
using Ion.Core;
using Ion.Misc;
using Ion.SyntaxAnalysis;
using Ion.Tracking;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class Type : ITypeEmitter
    {
        public Token Token { get; }

        protected readonly ContextSymbolTable symbolTable;

        protected readonly uint? arrayLength;

        public Type(ContextSymbolTable symbolTable, Token token, uint? arrayLength = null)
        {
            this.symbolTable = symbolTable;
            this.Token = token;
            this.arrayLength = arrayLength;
        }

        public LLVMTypeRef Emit()
        {
            // Create the result buffer.
            LLVMTypeRef result;

            // Use LLVM type resolver if token is a primitive type.
            if (TokenIdentifier.IsPrimitiveType(this.Token))
            {
                // Delegate to primitive type construct.
                result = new PrimitiveType(this.Token.Value).Emit();
            }
            // Otherwise, look it up on the structs dictionary, on the symbol table.
            else if (this.symbolTable.structs.Contains(this.Token.Value))
            {
                result = this.symbolTable.structs[this.Token.Value].Value;
            }
            // At this point, provided token is not a valid type.
            else
            {
                throw new Exception($"Token is not a primitive nor user-defined type: '{this.Token.Value}'");
            }

            // Convert result to an array if applicable.
            if (this.arrayLength.HasValue)
            {
                result = LLVM.ArrayType(result, this.arrayLength.Value);
            }

            // Return the resulting type.
            return result;
        }
    }
}
