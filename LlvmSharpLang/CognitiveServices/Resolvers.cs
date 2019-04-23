using System;
using System.Collections.Generic;
using LLVMSharp;
using LlvmSharpLang.Misc;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.CognitiveServices
{
    public class Resolvers
    {
        public delegate LLVMTypeRef LlvmTypeResolver();

        public delegate CodeGeneration.Type TypeResolver();

        protected static readonly Dictionary<string, LlvmTypeResolver> llvmTypeMap = new Dictionary<string, LlvmTypeResolver> {
            {TypeName.Int32, LLVMTypeRef.Int32Type},
            {TypeName.Int64, LLVMTypeRef.Int64Type},
            {TypeName.Float, LLVMTypeRef.FloatType},
            {TypeName.Double, LLVMTypeRef.DoubleType},
            {TypeName.Boolean, LLVMTypeRef.Int1Type},
            {TypeName.Void, LLVMTypeRef.VoidType},

            // TODO: Should it be *int8? Is this correct?
            {TypeName.Character, LLVMTypeRef.Int8Type}
        };

        protected static readonly Dictionary<TokenType, TypeResolver> literalTypeMap = new Dictionary<TokenType, TypeResolver>
        {
            {TokenType.LiteralCharacter, TypeFactory.Character},

            // TODO: What about float?
            {TokenType.LiteralDecimal, TypeFactory.Double},

            // TODO: What about Int64?
            {TokenType.LiteralInteger, TypeFactory.Int32},

            // TODO: Missing string.
        };

        public static LLVMTypeRef LlvmTypeFromName(string name)
        {
            if (Resolvers.llvmTypeMap.ContainsKey(name))
            {
                return Resolvers.llvmTypeMap[name]();
            }

            throw new Exception($"Non-registered type resolver for type '{name}'");
        }

        public static CodeGeneration.Type TypeFromTokenType(TokenType tokenType)
        {
            if (Resolvers.literalTypeMap.ContainsKey(tokenType))
            {
                return Resolvers.literalTypeMap[tokenType]();
            }

            throw new Exception($"Non-registered type resolver for token type '{tokenType}'");
        }

        public static CodeGeneration.Type TypeFromToken(Token token)
        {
            return Resolvers.TypeFromTokenType(token.Type);
        }

        public static LLVMValueRef Literal(Token token, CodeGeneration.Type type)
        {
            return Resolvers.Literal(token.Type, token.Value, type);
        }

        public static LLVMValueRef Literal(TokenType tokenType, string value, CodeGeneration.Type type)
        {
            // Token value is an integer.
            if (tokenType == TokenType.LiteralInteger)
            {
                return LLVM.ConstInt(type.Emit(), ulong.Parse(value), false);
            }
            // Token value is a decimal.
            else if (tokenType == TokenType.LiteralDecimal)
            {
                return LLVM.ConstReal(type.Emit(), double.Parse(value));
            }
            // Token value is a string.
            else if (tokenType == TokenType.LiteralString)
            {
                return LLVM.ConstString(value, (uint)value.Length, false);
            }

            throw new Exception("Cannot resolve unsupported type");
        }
    }
}
