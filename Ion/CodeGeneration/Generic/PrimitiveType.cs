using System;
using Ion.CodeGeneration.Helpers;
using Ion.CognitiveServices;
using Ion.Syntax;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class PrimitiveType : ITypeEmitter
    {
        public bool IsVoid => this.TokenValue == TypeName.Void;

        public string TokenValue { get; }

        public PrimitiveType(string tokenValue)
        {
            this.TokenValue = tokenValue;
        }

        public LLVMTypeRef Emit()
        {
            // Invoke LLVM type resolver, will automatically handle possible non-existent error.
            return Resolvers.LlvmTypeFromName(this.TokenValue);
        }
    }
}
