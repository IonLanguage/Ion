using System;
using LLVMSharp;
using LlvmSharpLang.CodeGeneration.Structure;
using LlvmSharpLang.CognitiveServices;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.CodeGeneration
{
    public delegate LLVMValueRef ConstantValueDelegate(Type type, string value);

    public class Value : IUncontextedEntity<LLVMValueRef>
    {
        public string ValueString { get; }

        public Type Type { get; }

        public Value(Type type, string valueString)
        {
            this.Type = type;
            this.ValueString = valueString;
        }

        public LLVMValueRef Emit()
        {
            TokenType? type = TokenIdentifier.IdentifyComplex(this.ValueString);

            // Ensure value was identified.
            if (!type.HasValue)
            {
                throw new Exception("Unable to identify value string from complex token types");
            }
            else if (!Constants.constantValueDelegates.ContainsKey(type.Value))
            {
                throw new Exception("No resolving delegate is associated with the identified token type");
            }

            return Constants.constantValueDelegates[type.Value](this.Type, this.ValueString);
        }
    }
}
