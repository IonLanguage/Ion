using System;
using LLVMSharp;
using LlvmSharpLang.CodeGeneration.Structure;
using LlvmSharpLang.CognitiveServices;
using LlvmSharpLang.Misc;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.CodeGeneration
{
    public delegate LLVMValueRef ConstantValueResolver(Type type, string value);

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
            // Ensure token is identified as a literal.
            else if (!TokenIdentifier.IsLiteral(type.Value))
            {
                throw new Exception("Unexpected non-literal token value");
            }

            return Resolvers.Literal(type.Value, this.ValueString, Resolvers.TypeFromTokenType(type.Value));
        }
    }
}
