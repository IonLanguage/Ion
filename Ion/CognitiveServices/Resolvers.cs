using System;
using System.Collections.Generic;
using Ion.CodeGeneration;
using Ion.Engine.Llvm;
using Ion.Misc;
using Ion.Syntax;
using LLVMSharp;

namespace Ion.CognitiveServices
{
    public static class Resolvers
    {
        public delegate LLVMTypeRef LlvmTypeResolver();

        public delegate PrimitiveType PrimitiveTypeResolver();

        private static readonly Dictionary<string, LlvmTypeResolver> LlvmTypeMap =
            new Dictionary<string, LlvmTypeResolver>
            {
                {TypeName.Int32, LLVMTypeRef.Int32Type},
                {TypeName.Int64, LLVMTypeRef.Int64Type},
                {TypeName.Float, LLVMTypeRef.FloatType},
                {TypeName.Double, LLVMTypeRef.DoubleType},
                {TypeName.Boolean, LLVMTypeRef.Int1Type},
                {TypeName.Void, LLVMTypeRef.VoidType},
                {TypeName.Character, LLVMTypeRef.Int8Type}
            };

        private static readonly Dictionary<TokenType, PrimitiveTypeResolver> LiteralTypeMap =
            new Dictionary<TokenType, PrimitiveTypeResolver>
            {
                {TokenType.LiteralCharacter, PrimitiveTypeFactory.Character},

                // TODO: What about float?
                {TokenType.LiteralDecimal, PrimitiveTypeFactory.Double},

                // TODO: What about Int64?
                {TokenType.LiteralInteger, PrimitiveTypeFactory.Int32},

                {TokenType.LiteralString, PrimitiveTypeFactory.String},

                {TokenType.KeywordTrue, PrimitiveTypeFactory.Boolean},

                {TokenType.KeywordFalse, PrimitiveTypeFactory.Boolean}
            };

        public static LlvmType LlvmTypeFromName(string name)
        {
            // TODO: Implement functionality for pointer types.
            // Special case for string type.
            if (name == TypeName.String)
            {
                return LLVMTypeRef.PointerType(LLVMTypeRef.Int8Type(), 0).Wrap();
            }
            // Otherwise, use LLVM type map.
            else if (LlvmTypeMap.ContainsKey(name))
            {
                return LlvmTypeMap[name]().Wrap();
            }

            // Throw an exception.
            throw new Exception($"Non-registered type resolver for type '{name}'");
        }

        public static PrimitiveType PrimitiveType(TokenType tokenType)
        {
            if (Resolvers.LiteralTypeMap.ContainsKey(tokenType))
            {
                return Resolvers.LiteralTypeMap[tokenType]();
            }

            throw new Exception($"Non-registered type resolver for token type '{tokenType}'");
        }

        public static PrimitiveType PrimitiveType(Token token)
        {
            return Resolvers.PrimitiveType(token.Type);
        }

        public static LLVMValueRef Literal(Token token, PrimitiveType type)
        {
            return Resolvers.Literal(token.Type, token.Value, type);
        }

        public static LLVMValueRef Literal(TokenType tokenType, string value, PrimitiveType type)
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
            // Token value is a boolean.
            else if (TokenIdentifier.IsBoolean(tokenType))
            {
                // Create the boolean value.
                uint boolValue;

                // True.
                if (tokenType == TokenType.KeywordTrue)
                {
                    boolValue = 1;
                }
                // False.
                else if (tokenType == TokenType.KeywordFalse)
                {
                    boolValue = 0;
                }
                // Ensure token type is a boolean representative.
                else
                {
                    throw new Exception($"Unexpected boolean value; Expected true or false token type but got {tokenType}");
                }

                // Create and return the resulting constant.
                return LLVM.ConstInt(type.Emit(), boolValue, false);
            }

            // At this point, type is unsupported.
            throw new Exception("Cannot resolve unsupported type");
        }
    }
}
