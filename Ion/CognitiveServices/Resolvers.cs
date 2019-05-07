using System;
using System.Collections.Generic;
using Ion.Misc;
using Ion.SyntaxAnalysis;
using LLVMSharp;
using Type = Ion.CodeGeneration.Type;

namespace Ion.CognitiveServices
{
    public static class Resolvers
    {
        public delegate LLVMTypeRef LlvmTypeResolver();

        public delegate Type TypeResolver();

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

        private static readonly Dictionary<TokenType, TypeResolver> LiteralTypeMap =
            new Dictionary<TokenType, TypeResolver>
            {
                {TokenType.LiteralCharacter, TypeFactory.Character},

                // TODO: What about float?
                {TokenType.LiteralDecimal, TypeFactory.Double},

                // TODO: What about Int64?
                {TokenType.LiteralInteger, TypeFactory.Int32},

                {TokenType.LiteralString, TypeFactory.String},

                {TokenType.KeywordTrue, TypeFactory.Boolean},

                {TokenType.KeywordFalse, TypeFactory.Boolean}

                // TODO: Missing string.
            };

        public static LLVMTypeRef LlvmTypeFromName(string name)
        {
            // TODO: Implement functionality for pointer types.
            // Special case for string type.
            if (name == TypeName.String)
            {
                return LLVMTypeRef.PointerType(LLVMTypeRef.Int8Type(), 0);
            }
            // Otherwise, use LLVM type map.
            else if (LlvmTypeMap.ContainsKey(name))
            {
                return LlvmTypeMap[name]();
            }

            // Throw an exception.
            throw new Exception($"Non-registered type resolver for type '{name}'");
        }

        public static Type TypeFromTokenType(TokenType tokenType)
        {
            if (LiteralTypeMap.ContainsKey(tokenType))
            {
                return LiteralTypeMap[tokenType]();
            }

            throw new Exception($"Non-registered type resolver for token type '{tokenType}'");
        }

        public static Type TypeFromToken(Token token)
        {
            return TypeFromTokenType(token.Type);
        }

        public static LLVMValueRef Literal(Token token, Type type)
        {
            return Literal(token.Type, token.Value, type);
        }

        public static LLVMValueRef Literal(TokenType tokenType, string value, Type type)
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

            throw new Exception("Cannot resolve unsupported type");
        }
    }
}
