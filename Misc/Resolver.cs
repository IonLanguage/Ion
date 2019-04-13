using System;
using System.Collections.Generic;
using LLVMSharp;

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

            // TODO: Should it be *int8?
            { TypeName.Character, LLVMTypeRef.Int8Type }
        };

        public static TypeResolverDelegate Type(string type)
        {
            if (Resolver.typeMap.ContainsKey(type))
            {
                return Resolver.typeMap[type];
            }

            throw new Exception($"Non-registered type: {type}");
        }
    }
}
