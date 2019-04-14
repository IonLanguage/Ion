using System;
using System.Collections.Generic;
using LLVMSharp;
using LlvmSharpLang.SyntaxAnalysis;

namespace LlvmSharpLang.Misc
{
    public class Resolver
    {
        public delegate LLVMTypeRef TypeResolverDelegate();

        protected static readonly Dictionary<string, TypeResolverDelegate> typeMap = new Dictionary<string, TypeResolverDelegate> {
            { TypeName.Int32, LLVMTypeRef.Int32Type },
            { TypeName.Int64, LLVMTypeRef.Int64Type },
            { TypeName.Float, LLVMTypeRef.FloatType },
            { TypeName.Double, LLVMTypeRef.DoubleType },
            { TypeName.Boolean, LLVMTypeRef.Int1Type },
            { TypeName.Void, LLVMTypeRef.VoidType },

            // TODO: Should it be *int8?
            { TypeName.Character, LLVMTypeRef.Int8Type }
        };

        public static TypeResolverDelegate Type(string value)
        {
            if (Resolver.typeMap.ContainsKey(value))
            {
                return Resolver.typeMap[value];
            }

            throw new Exception($"Non-registered type: {value}");
        }

        public static LLVMValueRef Literal(Token token, LLVMTypeRef type)
        {
            // Token value is an integer.
            if (token.Type == TokenType.LiteralInteger)
            {
                return LLVM.ConstInt(type, ulong.Parse(token.Value), false);
            }
            // Token value is a decimal.
            else if (token.Type == TokenType.LiteralDecimal)
            {
                return LLVM.ConstReal(type, double.Parse(token.Value));
            }
            // Token value is a string.
            else if (token.Type == TokenType.LiteralString)
            {
                return LLVM.ConstString(token.Value, (uint)token.Value.Length, false);
            }

            throw new Exception("Cannot resolve unsupported type");
        }
    }
}
