using System;
using Ion.CodeGeneration.Helpers;
using Ion.CognitiveServices;
using Ion.SyntaxAnalysis;
using LLVMSharp;

namespace Ion.CodeGeneration
{
    public class PrimitiveType : IOneWayPipe<LLVMTypeRef>
    {
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
